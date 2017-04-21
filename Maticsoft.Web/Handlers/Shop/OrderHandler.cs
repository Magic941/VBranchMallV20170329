/**
* SubmitOrderHandler.cs
*
* 功 能： 生成订单
* 类 名： SubmitOrderHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/20 17:06:54  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.BLL.Shop.PromoteSales;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Order;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Json;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Maticsoft.ViewModel.Order;


namespace Maticsoft.Web.Handlers.Shop
{
    public class OrderHandler : HandlerBase, IRequiresSessionState
    {
        private readonly BLL.Ms.Regions _regionManage = new BLL.Ms.Regions();
        private readonly BLL.Shop.Shipping.ShippingType _shippingTypeManage = new BLL.Shop.Shipping.ShippingType();

        private readonly BLL.Shop.Shipping.ShippingAddress _shippingAddressManage =
            new BLL.Shop.Shipping.ShippingAddress();

        private readonly BLL.Shop.Shipping.ShippingRegionGroups _shippingRegionManage =
            new BLL.Shop.Shipping.ShippingRegionGroups();

        private BLL.Shop.Products.ProductInfo _productInfoManage = new BLL.Shop.Products.ProductInfo();
        private BLL.Shop.Products.SKUInfo _skuInfoManage = new BLL.Shop.Products.SKUInfo();
        private Maticsoft.BLL.Shop.Coupon.CouponInfo couponBll = new CouponInfo();
        private Maticsoft.BLL.Shop_CardUserInfo _cardUserInfoBLL = new BLL.Shop_CardUserInfo();

        private Maticsoft.BLL.Shop.PromoteSales.GroupBuy groupBuyBll = new GroupBuy();
        private BLL.Shop.Shipping.Shop_freefreight _feeBLL = new BLL.Shop.Shipping.Shop_freefreight();
        private readonly BLL.Shop.ActivityManage.AMPBLL _ampBLL = new BLL.Shop.ActivityManage.AMPBLL();

        #region IHttpHandler 成员

        public override bool IsReusable
        {
            get { return false; }
        }

        public override void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";

            try
            {
                switch (action)
                {
                    case "SubmitOrder":
                        context.Response.Write(SubmitOrder(context));
                        break;
                    case "SubmitOrderNew":
                        context.Response.Write(SubmitOrderWeiXin(context));
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                //json.Put(KEY_STATUS, STATUS_ERROR);
                json.Put(KEY_STATUS, ex.Message);
                json.Put(KEY_DATA, ex);
                context.Response.Write(json.ToString());
            }
        }

        #endregion

        #region 提交订单

        private string SubmitOrder(HttpContext context)
        {
            JsonObject result = new JsonObject();

            #region 获取基础数据

            Payment.Model.PaymentModeInfo paymentModeInfo = GetPaymentModeInfo(context);
            if (paymentModeInfo == null)
            {
                result.Accumulate(KEY_STATUS, "NOPAYMENTMODEINFO");
                return result.ToString();
            }

            //获取健康卡系统编号
            int CardSysId = _cardUserInfoBLL.GetDefaultCardsysID(CurrentUser.UserName);

            //获取健康卡号
            string cardno = _cardUserInfoBLL.GetDefaultCardNo(CurrentUser.UserName);

            #endregion

            #region 1.获取购买人

            //DONE: 1.获取购买人
            Maticsoft.Accounts.Bus.User userBuyer = GetBuyerUserInfo(context);
            if (userBuyer == null)
            {
                result.Accumulate(KEY_STATUS, STATUS_NOLOGIN);
                return result.ToString();
            }
            if (userBuyer.UserType == "AA")
            {
                result.Accumulate(KEY_STATUS, STATUS_UNAUTHORIZED);
                return result.ToString();
            }

            #endregion

            #region 2.获取购物车

            //DONE: 2.获取购物车
            BLL.Shop.Products.ShoppingCartHelper shoppingCartHelper;
            ShoppingCartInfo shoppingCartInfo;
            try
            {
                //购物车
                shoppingCartInfo = _ampBLL.GetTotalPriceAfterActivity(GetShoppingCart(context, userBuyer, out shoppingCartHelper));
            }
            catch (ArgumentNullException)
            {
                result.Accumulate(KEY_STATUS, "PROSALEEXPIRED");
                return result.ToString();
            }
            if (shoppingCartInfo == null || shoppingCartInfo.Items == null || shoppingCartInfo.Items.Count < 1)
            {
                result.Accumulate(KEY_STATUS, "NOSHOPPINGCARTINFO");
                return result.ToString();
            }
            //DONE: 2.1 Check 商品库存
            List<ShoppingCartItem> noStockList = new List<ShoppingCartItem>();
            foreach (ShoppingCartItem item in shoppingCartInfo.Items)
            {
                //检查购买数量是否大于库存
                if (item.Quantity < 1 || item.Quantity > _skuInfoManage.GetStockBySKU(item.SKU))
                {
                    noStockList.Add(item);
                }
            }
            if (noStockList.Count > 0)
            {
                result.Accumulate(KEY_STATUS, "NOSTOCK");
                result.Accumulate(KEY_DATA, noStockList);
                //自动移除Cookie/DB购物车中的无库存项目
                if (shoppingCartHelper != null)
                {
                    //ShoppingCartInfo tmpCartInfo = shoppingCartHelper.GetShoppingCart();
                    noStockList.ForEach(info =>
                        {
                            //TODO: 仅自动删除无库存商品 此处需要DB真实库存
                            //ShoppingCartItem item = tmpCartInfo[info.ItemId];
                            //if (item==null) return;
                            //if (info.Quantity >= item.Quantity)
                            //{
                            shoppingCartHelper.RemoveItem(info.ItemId);
                            //}
                            //else
                            //{
                            //    shoppingCartHelper.UpdateItemQuantity(info.ItemId, xx);
                            //}
                        });
                }
                return result.ToString();
            }

            #endregion

            #region 2.1优惠券使用验证
            string couponCode = context.Request.Form["Coupon"];
            Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = couponBll.GetCouponInfo(couponCode);
            //5.1优惠券限制验证
            if (infoModel != null)
            {
                if (infoModel == null)
                {
                    result.Accumulate(KEY_STATUS, "NOCOUPON");
                    result.Accumulate(STATUS_FAILED, "没有此优惠劵");
                    return result.ToString();
                }
                if (infoModel.Status == 2)
                {
                    result.Accumulate(KEY_STATUS, "NOCOUPON");
                    result.Accumulate(STATUS_FAILED, "此优惠劵已使用");
                    return result.ToString();

                }
                if (shoppingCartInfo.TotalAdjustedPriceNew <= infoModel.LimitPrice)
                {
                    result.Accumulate(KEY_STATUS, "NOCOUPON");
                    result.Accumulate(STATUS_FAILED, "此优惠劵没有达到最低购买金额");
                    return result.ToString();
                }
            }
            #endregion

            #region 3.获取收货人

            //DONE: 3.获取收货人
            Model.Shop.Shipping.ShippingAddress shippingAddress = GetShippingAddress(context);
            if (shippingAddress == null)
            {
                result.Accumulate(KEY_STATUS, "NOSHIPPINGADDRESS");
                return result.ToString();
            }
            Model.Ms.Regions regionInfo = _regionManage.GetModelByCache(shippingAddress.RegionId);
            if (regionInfo == null)
            {
                result.Accumulate(KEY_STATUS, "NOREGIONINFO");
                return result.ToString();
            }
            #endregion

            #region 4.获取配送(物流)

            //DONE: 4.获取配送(物流)
            List<SupplierFreight> SupplierFreightList = null;

            if (!string.IsNullOrEmpty(context.Request.Form["supplierFreight"]))
            {
                SupplierFreightList = new JavaScriptSerializer().Deserialize<List<SupplierFreight>>(context.Request.Form["supplierFreight"].ToString());
            }
            else
            {
                SupplierFreightList = new List<SupplierFreight>();
            }


            Model.Shop.Shipping.ShippingType shippingType = new Model.Shop.Shipping.ShippingType();
            if (SupplierFreightList.Count == 1)
            {
                shippingType = GetShippingType(SupplierFreightList.First().ShippingTypeID);
                if (shippingType == null)
                {
                    result.Accumulate(KEY_STATUS, "NOSHIPPINGTYPE");
                    return result.ToString();
                }
            }

            #endregion

            #region 5.生成订单
            //DONE: 5.生成订单
            OrderInfo mainOrder = new OrderInfo();
            #region 填充订单数据

            #region 基础数据

            mainOrder.CreatedDate = DateTime.Now;
            Maticsoft.Web.Areas.MShop.Controllers.AccountController api = new Maticsoft.Web.Areas.MShop.Controllers.AccountController();
            Random ran = new Random();
            int RandKey = ran.Next(1000, 9999);
            mainOrder.OrderCode ="HL"+ mainOrder.CreatedDate.ToString("yyyyMMddHHmmssfff")+RandKey.ToString();
            mainOrder.CardSysId = CardSysId;
            mainOrder.CardNo = cardno;

            #region 支付信息

            mainOrder.PaymentTypeId = paymentModeInfo.ModeId;
            mainOrder.PaymentTypeName = paymentModeInfo.Name;
            mainOrder.PaymentGateway = paymentModeInfo.Gateway;

            result.Accumulate("GATEWAY", mainOrder.PaymentGateway);

            #endregion

            #region 重量/运费/积分

            #region 区域差异运费计算
            int topRegionId;
            if (regionInfo.Depth > 1)
            {
                topRegionId = Globals.SafeInt(regionInfo.Path.Split(new[] { ',' })[1], -1);
            }
            else
            {
                topRegionId = regionInfo.RegionId;
            }

            Model.Shop.Shipping.ShippingRegionGroups shippingRegion = _shippingRegionManage.GetShippingRegion(shippingType.ModeId, topRegionId);
            #endregion

            mainOrder.Weight = shoppingCartInfo.TotalWeight;
            //主订单运费为子订单运费之和
            if (SupplierFreightList.Count == 1)
            {
                //在计算运费之前如果使用了优惠券则减掉优惠券的金额
                var CouponValue = (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);

                if (CouponValue > shoppingCartInfo.TotalAdjustedPrice)
                {
                    mainOrder.Amount = 0;
                }
                else
                {
                    mainOrder.Amount = shoppingCartInfo.TotalAdjustedPrice - CouponValue;
                }
                int groupbuyId = Common.Globals.SafeInt(context.Request.Form["GroupBuyId"], -1);
                if (groupbuyId > 0)
                {
                    mainOrder.FreightAdjusted = mainOrder.FreightActual = mainOrder.Freight = 0;
                }
                else
                {
                    shoppingCartInfo.TotalWeight = shoppingCartInfo.TotalWeight;
                    mainOrder.FreightAdjusted = mainOrder.FreightActual = mainOrder.Freight = _feeBLL.CalFreeShipping(topRegionId, mainOrder.Amount, shoppingCartInfo, shippingType, shippingRegion);
                }
                //订单总成本价 = 商品总成本价
                mainOrder.OrderCostPrice = shoppingCartInfo.TotalCostPrice;
                mainOrder.OrderCostPrice2 = shoppingCartInfo.TotalCostPrice2;

                //订单总金额(无任何优惠) 商品总价 + 运费
                mainOrder.OrderTotal = shoppingCartInfo.Items.Where(m => m.Selected == true).Sum(m => m.AdjustedPrice * m.Quantity) + mainOrder.Freight.Value;

                //订单的最后支付金额 =减去优惠券后的总价+调整后的运费
                mainOrder.Amount += mainOrder.FreightAdjusted.Value;
            }

            mainOrder.OrderPoint = shoppingCartInfo.TotalPoints;
            #endregion

            #region 优惠券数据

            mainOrder.CouponAmount = 0;
            if (infoModel != null)
            {
                mainOrder.CouponAmount = infoModel.CouponPrice;
                mainOrder.CouponCode = infoModel.CouponCode;
                mainOrder.CouponName = infoModel.CouponName;
                mainOrder.CouponValue = infoModel.CouponPrice;
                mainOrder.CouponValueType = 1;
            }

            #endregion

            #region 订单价格

            //订单商品总价(无任何优惠)
            mainOrder.ProductTotal = shoppingCartInfo.TotalSellPrice;

            if (mainOrder.Amount < 0)
            {
                LogHelp.AddInvadeLog(string.Format("非法订单金额|{0}|_Maticsoft.Web.Handlers.Shop.OrderHandler.SubmitOrder",mainOrder.Amount.ToString("F2")), HttpContext.Current.Request);
                result.Accumulate(KEY_STATUS, "ILLEGALORDERAMOUNT");
                return result.ToString();
            }

            #endregion

            #region 限时抢购
            int proSaleId = Common.Globals.SafeInt(context.Request.Form["ProSaleId"], -1);
            if (proSaleId > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo proSaleInfo = _productInfoManage.GetProSaleModel(proSaleId);
                if (proSaleInfo != null)
                {
                    mainOrder.ActivityName = string.Format("限时抢购[{0}]", proSaleInfo.CountDownId);
                    //活动优惠金额 = 总金额(含运费无任何优惠) - 最终支付(含运费优惠后)
                    mainOrder.ActivityFreeAmount = mainOrder.OrderTotal - mainOrder.Amount;
                    mainOrder.ActivityStatus = 1;
                }
            }
            #endregion

            #region 团购数据
            //订单中一次只能提交一个团购商品或抢购商品 20141004
            if (shoppingCartInfo.Items.Any(x => x.PromotionType > -1))
            {
                int groupbuyId = Common.Globals.SafeInt(context.Request.Form["GroupBuyId"], -1);
                
                if (groupbuyId > 0)
                {
                    var groupProduct = groupBuyBll.GetModelByGroupID(groupbuyId);
                    if (groupProduct != null && groupProduct.PromotionType == 1)
                    {
                        mainOrder.GroupBuyId = groupProduct.GroupBuyId;
                        //查看用户购买数量限制,一个帐号买一个派送只能一件
                        if (groupProduct.MaxCount > 0 && groupProduct.PromotionType == 1)
                        {
                            if (groupBuyBll.GetGroupBuyLimt4Hour(userBuyer.UserID) == 1)
                            {
                                throw new Exception("一个账号在每个时段只能限购一个抢购中的产品！");
                            }
                            if (groupProduct.BuyCount >= groupProduct.MaxCount)
                            {
                                throw new Exception("您抢购的商品已达到允许抢购的上限!");
                            }
                            if(mainOrder.OrderCode.IndexOf("WX") >= 0)
                                Maticsoft.Common.ErrorLogTxt.GetInstance("团购并发日志Web").Write("订单已执行了并发控制" + mainOrder.OrderCode);
                        }
                    }
                }
                mainOrder.GroupBuyStatus = 1;

            }
            //不能通过该参数传，直接从团购过来用该参数
            int groupBuyId = Common.Globals.SafeInt(context.Request.Form["GroupBuyId"], -1);
            if (groupBuyId > 0)
            {
                Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = groupBuyBll.GetModelByCache(groupBuyId);
                if (buyModel != null)
                {
                    mainOrder.GroupBuyId = buyModel.GroupBuyId;
                    mainOrder.GroupBuyPrice = buyModel.Price;
                    mainOrder.GroupBuyStatus = 1;
                }
            }

            #endregion

            mainOrder.OrderType = 1;

            //DONE: 货到付款 下单后直接进入 6.正在处理 流程 其它流程不变 BEN MODIFY 20131205
            mainOrder.OrderStatus = mainOrder.PaymentGateway == "cod" ? 1 : 0;

            #endregion

            #region 购买人信息

            mainOrder.BuyerID = userBuyer.UserID;
            mainOrder.BuyerName = userBuyer.UserName;
            //TODO: 用户Email为空时, 暂以默认Email下单 BEN ADD 20130701
            mainOrder.BuyerEmail = string.IsNullOrWhiteSpace(userBuyer.Email) ? "pay@maticsoft.com" : userBuyer.Email;
            mainOrder.BuyerCellPhone = userBuyer.Phone;

            #endregion

            #region 拆单对象
            Dictionary<int, List<OrderItems>> dicSuppOrderItems = new Dictionary<int, List<OrderItems>>();
            #endregion

            Dictionary<long, int> dicProsItems = new Dictionary<long, int>();

            #region 购物车 -> 订单项目
            OrderItems tmpOrderItem;
            //购物车 -> 订单项目
            shoppingCartInfo.Items.ForEach(item =>
            {
                tmpOrderItem = new OrderItems
                 {
                     //TODO: 警告: 商品信息根据Cookie获取, 暂未与DB及时同步
                     Name = item.Name,
                     SKU = item.SKU,
                     Quantity = item.Quantity,
                     ShipmentQuantity = item.Quantity,
                     ThumbnailsUrl = item.ThumbnailsUrl,
                     Points = item.Points,
                     Weight = item.Weight,
                     ProductId = item.ProductId,
                     Description = item.Description,
                     CostPrice = item.CostPrice,
                     CostPrice2 = item.CostPrice2,
                     SellPrice = item.SellPrice,
                     AdjustedPrice = item.AdjustedPrice,
                     Deduct = item.SellPrice - item.AdjustedPrice,

                     //商家信息
                     SupplierId = item.SupplierId,
                     SupplierName = item.SupplierName
                 };


                //marke,促销类型是团购
                if (item.PromotionType > -1)
                {

                    if (dicProsItems.ContainsKey(item.ProductId))
                    {
                        dicProsItems[item.ProductId] += item.Quantity;
                    }
                    else
                    {
                        dicProsItems.Add(item.ProductId, item.Quantity);
                    }
                }

                //将SKU信息记录到订单项目的Attribute中 简单记录 逗号分割, 复杂的可以为Json结构
                if (item.SkuValues != null && item.SkuValues.Length > 0)
                {
                    tmpOrderItem.Attribute = string.Join(",", item.SkuValues);
                }

                //填充订单项
                mainOrder.OrderItems.Add(tmpOrderItem);

                //填充商家订单项
                if (tmpOrderItem.SupplierId.HasValue && tmpOrderItem.SupplierId.Value > 0)
                {
                    if (dicSuppOrderItems.ContainsKey(tmpOrderItem.SupplierId.Value))
                    {
                        dicSuppOrderItems[tmpOrderItem.SupplierId.Value].Add(tmpOrderItem);
                    }
                    else
                    {
                        dicSuppOrderItems.Add(tmpOrderItem.SupplierId.Value,
                            new List<OrderItems> { tmpOrderItem });
                    }
                }
                else
                {
                    if (dicSuppOrderItems.ContainsKey(0))
                    {
                        dicSuppOrderItems[0].Add(tmpOrderItem);
                    }
                    else
                    {
                        dicSuppOrderItems.Add(0,
                            new List<OrderItems> { tmpOrderItem });
                    }
                }
            });

            #endregion

            #region 收货人信息

            mainOrder.RegionId = shippingAddress.RegionId;
            mainOrder.ShipRegion = shippingAddress.RegionFullName; //_regionManage.GetFullNameById4Cache(regionInfo.RegionId);
            mainOrder.ShipName = shippingAddress.ShipName;
            mainOrder.ShipEmail = shippingAddress.EmailAddress;
            mainOrder.ShipCellPhone = shippingAddress.CelPhone;
            mainOrder.ShipTelPhone = shippingAddress.TelPhone;
            mainOrder.ShipAddress = shippingAddress.Address;
            mainOrder.ShipZipCode = shippingAddress.Zipcode;

            #endregion

            #region 配送信息(物流)
            if (shippingType.ModeId != 0)
            {
                mainOrder.ShippingModeId = shippingType.ModeId;
                mainOrder.ShippingModeName = shippingType.Name;
                mainOrder.RealShippingModeId = shippingType.ModeId;
                mainOrder.RealShippingModeName = shippingType.Name;
            }

            mainOrder.ShippingStatus = 0;
            //mainOrder.ExpressCompanyName = shippingType.ExpressCompanyName;
            //mainOrder.ExpressCompanyAbb = shippingType.ExpressCompanyEn;

            #endregion

            #region 自动拆单
            int subOrderIndex = 1;
            int g = Globals.SafeInt(context.Request.Form["GroupBuyId"], -1);
            //判断是否购买了多个商家的商品, 并进行拆单
            if (dicSuppOrderItems.Count > 1)
            {
                decimal? totalFreight = 0;
                #region 拆单逻辑
                foreach (KeyValuePair<int, List<OrderItems>> item in dicSuppOrderItems)
                {
                    //根据主订单构造子订单
                    OrderInfo subOrder = new OrderInfo(mainOrder);

                    #region Reset 重量/运费/积分/价格
                    subOrder.Weight = 0;
                    subOrder.FreightAdjusted =
                        subOrder.FreightActual =
                            subOrder.Freight = 0;
                    subOrder.OrderPoint = 0;

                    subOrder.ProductTotal = 0;
                    subOrder.OrderCostPrice = 0;
                    subOrder.OrderCostPrice2 = 0;
                    subOrder.OrderOptionPrice = 0;
                    subOrder.OrderProfit = 0;
                    subOrder.Amount = 0;
                    subOrder.CardSysId = CardSysId;
                    #endregion

                    #region 重新计算 重量/积分/价格
                    item.Value.ForEach(info =>
                    {
                        subOrder.Weight += info.Weight * info.Quantity;
                        subOrder.OrderPoint += info.Points * info.Quantity;

                        //订单商品总价(无优惠)
                        subOrder.ProductTotal += info.SellPrice * info.Quantity;
                        //订单总成本价 = 项目总成本价
                        subOrder.OrderCostPrice += info.CostPrice * info.Quantity;

                        subOrder.OrderCostPrice2 += info.CostPrice2 * info.Quantity;
                        //订单最终支付金额 = 商品总价
                        subOrder.Amount += info.AdjustedPrice * info.Quantity;
                    });

                    var c = (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);
                    var avgCouponValue = c / dicSuppOrderItems.Count;

                    Maticsoft.BLL.shop.freight.Shop_ProductsFreight sff = new BLL.shop.freight.Shop_ProductsFreight();
                    List<Maticsoft.Model.shop.Freight.Shop_ProductsFreight> sfflist = new List<Model.shop.Freight.Shop_ProductsFreight>();

                    var supplierlist = shoppingCartInfo.Items.Where(q => q.SupplierId == item.Key).ToList();
                    foreach (var y in supplierlist)
                    {
                        var z = sff.GetModel(y.ProductId);
                        if (z!=null)
                        {
                            sfflist.Add(z);
                        }

                    }    

                    int shippingTypeId = SupplierFreightList.Where(m => m.SupplierId == item.Key).First().ShippingTypeID;
                    Maticsoft.Model.Shop.Shipping.ShippingType ShippingType = _shippingTypeManage.GetModel(shippingTypeId);
                    Model.Shop.Shipping.ShippingRegionGroups SubshippingRegion =
                    _shippingRegionManage.GetShippingRegion(shippingTypeId, topRegionId);
                    shoppingCartInfo.TotalWeight = int.Parse(subOrder.Weight.ToString());
                    if (g > 0)
                    {
                        subOrder.FreightAdjusted =
                          subOrder.FreightActual =
                              subOrder.Freight = 0;
                    }
                    else
                    {
                        if (sfflist.Count == shoppingCartInfo.Items.Where(l => l.SupplierId == item.Key).Count() && g > 0)
                        {
                            subOrder.FreightAdjusted =
                            subOrder.FreightActual =
                                subOrder.Freight = sfflist.Max(z => z.Freight);
                        }
                        else
                        {
                            subOrder.FreightAdjusted =
                          subOrder.FreightActual =
                              subOrder.Freight = _feeBLL.CalFreeShipping(topRegionId, shoppingCartInfo.SupplierPriceList.Where(m => m.SupplierId == item.Key).First().TotalAdjustedPrice - avgCouponValue, shoppingCartInfo, ShippingType, shippingRegion);
                        }
                    }
              
                    totalFreight += subOrder.Freight;
                    //发票信息
                    string Remark = SupplierFreightList.Where(m => m.SupplierId == item.Key).First().Remark;
                    subOrder.Remark = Remark;

                    //订单总金额(含优惠) 商品总价 + 运费
                    subOrder.OrderTotal = subOrder.ProductTotal + subOrder.Freight.Value;
                    //订单最终支付金额 = 商品总价(含优惠) + 调整后运费
                    subOrder.Amount += subOrder.FreightAdjusted.Value;

                    //TODO: 均分主订单的优惠给子订单, 作为退款使用
                    #endregion

                    //订单项目
                    subOrder.OrderItems = item.Value;

                    subOrder.OrderType = 2;

                    #region 填充子单商家信息
                    subOrder.SupplierId = item.Key;
                    if (!FillSellerInfo(subOrder))
                    {
                        result.Accumulate(KEY_STATUS, "NOSUPPLIERINFO");
                        return result.ToString();
                    }
                    #endregion

                    #region 子订单基础数据
                    //DONE: 防止运算过快产生相同订单号
                    subOrder.CreatedDate = mainOrder.CreatedDate.AddMilliseconds(subOrderIndex++);
                    subOrder.OrderCode = subOrder.CreatedDate.ToString("yyyyMMddHHmmssfff");
                    subOrder.RealShippingModeId = shippingTypeId;
                    subOrder.RealShippingModeName = ShippingType.Name;
                    subOrder.ShippingModeId = shippingTypeId;
                    subOrder.ShippingModeName = ShippingType.Name;
                    subOrder.ExpressCompanyName = ShippingType.ExpressCompanyName;
                    subOrder.ExpressCompanyAbb = ShippingType.ExpressCompanyEn;
                    #endregion

                    mainOrder.SubOrders.Add(subOrder);
                }

                #endregion
                //计算总运费
                mainOrder.FreightAdjusted = mainOrder.FreightActual = mainOrder.Freight = totalFreight;
                //订单总成本价 = 商品总成本价
                mainOrder.OrderCostPrice = shoppingCartInfo.TotalCostPrice;

                //订单总金额(无任何优惠) 商品总价 + 运费
                mainOrder.OrderTotal = shoppingCartInfo.TotalSellPrice + mainOrder.Freight.Value;

                //订单最终支付金额 = 项目调整后总售价 + 调整后运费-优惠券价格

                var CouponValue = (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);

                if (CouponValue > shoppingCartInfo.TotalAdjustedPrice)
                {
                    mainOrder.Amount = mainOrder.FreightAdjusted.Value;
                }
                else
                {
                    mainOrder.Amount = shoppingCartInfo.TotalAdjustedPrice + mainOrder.FreightAdjusted.Value - (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);
                }
                mainOrder.HasChildren = true;   //有子订单
            }
            else
            {
                //没有购买多个商家的商品
                mainOrder.SupplierId = shoppingCartInfo.Items[0].SupplierId;
                mainOrder.SupplierName = shoppingCartInfo.Items[0].SupplierName;
                mainOrder.HasChildren = false;  //无子订单

                //在计算运费之前如果使用了优惠券则减掉优惠券的金额
                var CouponValue = (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);
                mainOrder.Amount = shoppingCartInfo.TotalAdjustedPrice;// -CouponValue;

                shoppingCartInfo.TotalWeight = shoppingCartInfo.TotalWeight;

                Maticsoft.BLL.shop.freight.Shop_ProductsFreight sff = new BLL.shop.freight.Shop_ProductsFreight();
                List<Maticsoft.Model.shop.Freight.Shop_ProductsFreight> sfflist = new List<Model.shop.Freight.Shop_ProductsFreight>();

               //判断单商户中包含的特殊产品邮费是否跟购物车所在的数量一致
                foreach (var item in shoppingCartInfo.Items)
                {

                    var x = sff.GetModel(item.ProductId);
                    if (x != null)
                    {
                        sfflist.Add(x);
                    }
                }
                if (g > 0)
                {
                    mainOrder.FreightAdjusted =
                      mainOrder.FreightActual =
                          mainOrder.Freight = 0;
                }
                else
                {
                    if (sfflist.Count == shoppingCartInfo.Items.Count && g > 0)
                    {
                        mainOrder.FreightAdjusted =
                           mainOrder.FreightActual =
                           mainOrder.Freight = sfflist.Max(x => x.Freight);
                    }
                    else
                    {
                        mainOrder.FreightAdjusted =
                        mainOrder.FreightActual =
                        mainOrder.Freight = _feeBLL.CalFreeShipping(topRegionId, mainOrder.Amount, shoppingCartInfo, shippingType, shippingRegion);
                    }
                }

                //订单总成本价 = 商品总成本价
                mainOrder.OrderCostPrice = shoppingCartInfo.TotalCostPrice;
                mainOrder.OrderCostPrice2 = shoppingCartInfo.TotalCostPrice2;

                //订单总金额(无任何优惠) 商品总价 + 运费
                mainOrder.OrderTotal = shoppingCartInfo.Items.Where(m => m.Selected == true).Sum(m => m.AdjustedPrice * m.Quantity) + mainOrder.Freight.Value;

                //订单的最后支付金额 =减去优惠券后的总价+调整后的运费
                mainOrder.Amount += mainOrder.FreightAdjusted.Value;

                if (CouponValue >= mainOrder.Amount-mainOrder.FreightAdjusted.Value)
                {
                    mainOrder.Amount = mainOrder.FreightAdjusted.Value;
                }
                else
                {
                    mainOrder.Amount = mainOrder.Amount - CouponValue;
                }
            }
            #endregion




            #region 填充主单商家信息
            if (!FillSellerInfo(mainOrder))
            {
                result.Accumulate(KEY_STATUS, "NOSUPPLIERINFO");
                return result.ToString();
            }
            #endregion

            #region 订单分销逻辑

            #endregion
            #endregion

            #region 执行事务-创建订单
            try
            {
                mainOrder.OrderId = BLL.Shop.Order.OrderManage.CreateOrder(mainOrder);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog("订单创建失败: " + ex.Message, ex.StackTrace, context.Request);
                throw;
                //result.Accumulate(STATUS_ERROR, ex.Message);
                //  return result.ToString();
            }
            #endregion

            #endregion

            #region 6.提示生成成功, 进行支付

            //DONE: 6.提示生成成功, 进行支付
            result.Accumulate(KEY_DATA, new
                {
                    mainOrder.OrderId,
                    mainOrder.OrderCode,
                    mainOrder.Amount,
                    mainOrder.PaymentTypeId,
                    mainOrder.PaymentTypeName
                });

            if (mainOrder.OrderId == -1)
            {
                result.Accumulate(KEY_STATUS, STATUS_FAILED);
                return result.ToString();
            }
            //更新优惠券信息
            if (!String.IsNullOrWhiteSpace(couponCode))
            {
                couponBll.UseCoupon(couponCode, mainOrder.BuyerID, mainOrder.BuyerEmail);
            }

            //清空Cookie/DB购物车
            if (shoppingCartHelper != null)
            {
                shoppingCartHelper.ClearShoppingCart();
            }

            result.Accumulate(KEY_STATUS, STATUS_SUCCESS);

            #endregion

            return result.ToString();
        }

        #region 获取购物车数据

        private ShoppingCartInfo GetShoppingCart(HttpContext context, Maticsoft.Accounts.Bus.User userBuyer, out BLL.Shop.Products.ShoppingCartHelper shoppingCartHelper)
        {
            ShoppingCartInfo shoppingCartInfo = null;
            string jsonSkuStr = context.Request.Form["SkuInfos"];
            int proSaleId = Globals.SafeInt(context.Request.Form["ProSaleId"], -1);
            int groupBuyId = Globals.SafeInt(context.Request.Form["GroupBuyId"], -1);
            int acceId = Globals.SafeInt(context.Request.Form["AcceId"], -1);
            int ActiveID = Globals.SafeInt(context.Request.Form["activeid"], -1);
            int ActiveType = Globals.SafeInt(context.Request.Form["activetype"], -1);
            if (acceId > 0)//组合优惠套装
            {
                shoppingCartHelper = null;
                BLL.Shop.Products.SKUInfo skuManage = new BLL.Shop.Products.SKUInfo();
                BLL.Shop.Products.ProductAccessorie prodAcceBll = new BLL.Shop.Products.ProductAccessorie();
                ProductAccessorie prodAcceModel = prodAcceBll.GetModel(acceId);
                if (prodAcceModel == null || prodAcceModel.Type != 2)
                {
                    return null;
                }
                List<SKUInfo> skulist = skuManage.GetSKUListByAcceId(acceId, 0);
                if (skulist == null || skulist.Count < 2)//每组商品最少有两条数据
                {
                    return null;
                }

                decimal totalPrice = 0;//原　　价
                decimal dealsPrices = 0;//总优惠金额
                foreach (var item in skulist)
                {
                    totalPrice += item.SalePrice;
                }
                dealsPrices = totalPrice - prodAcceModel.DiscountAmount;
                decimal dealsPrice = dealsPrices / skulist.Count;//单个商品优惠的金额
                shoppingCartInfo = new ShoppingCartInfo();
                ShoppingCartItem cartItem;
                foreach (var item in skulist)
                {
                    cartItem = new ShoppingCartItem();
                    cartItem.MarketPrice = item.MarketPrice.HasValue ? item.MarketPrice.Value : 0;
                    cartItem.Name = item.ProductName;
                    cartItem.Quantity = 1;
                    cartItem.SellPrice = item.SalePrice;
                    cartItem.AdjustedPrice = item.SalePrice - dealsPrice;
                    cartItem.SKU = item.SKU;
                    cartItem.ProductId = item.ProductId;
                    cartItem.UserId = userBuyer.UserID;

                    //DONE: 根据SkuId 获取SKUItem值和图片数据 BEN 2013-06-30
                    List<SKUItem> listSkuItems = item.SkuItems;
                    if (listSkuItems != null && listSkuItems.Count > 0)
                    {
                        cartItem.SkuValues = new string[listSkuItems.Count];
                        int index = 0;
                        listSkuItems.ForEach(xx =>
                        {
                            cartItem.SkuValues[index++] = xx.ValueStr;
                            if (!string.IsNullOrWhiteSpace(xx.ImageUrl))
                            {
                                cartItem.SkuImageUrl = xx.ImageUrl;
                            }
                        });
                    }
                    //TODO: 未使用SKU缩略图 BEN ADD 2013-06-30
                    cartItem.ThumbnailsUrl = item.ProductThumbnailUrl;
                    cartItem.CostPrice = item.CostPrice.HasValue ? item.CostPrice.Value : 0;
                    cartItem.CostPrice2 = item.CostPrice2.HasValue ? item.CostPrice2.Value : 0;
                    cartItem.Weight = item.Weight.HasValue ? item.Weight.Value : 1;

                    #region 商家
                    if (item.SupplierId > 0)
                    {
                        BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                        Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(item.SupplierId);
                        if (supplierInfo != null)
                        {
                            cartItem.SupplierId = supplierInfo.SupplierId;
                            cartItem.SupplierName = supplierInfo.Name;
                        }
                    }
                    #endregion

                    shoppingCartInfo.Items.Add(cartItem);
                }
            }
            else if (string.IsNullOrWhiteSpace(jsonSkuStr))  //判断 是否使用 Cookie 加载购物车
            {
                shoppingCartHelper = new BLL.Shop.Products.ShoppingCartHelper(userBuyer.UserID);
                //DONE: 获取已选中内容的购物车进行 购物车 部分商品 下单 BEN Modify 20130923
                shoppingCartInfo = shoppingCartHelper.GetShoppingCart4Selected();
            }
            else
            {
                //使用 SKU 加载购物车
                shoppingCartHelper = null;
                //DONE: 目前是 SkuId 应为 SKU | DONE 20130901
                JsonArray jsonSkuArray;
                try
                {
                    jsonSkuArray = Json.Conversion.JsonConvert.Import<JsonArray>(jsonSkuStr);
                }
                catch (Exception ex)
                {
                    LogHelp.AddInvadeLog(
                       string.Format("非法SKU数据|{0}|_Maticsoft.Web.Handlers.Shop.OrderHandler.GetShoppingCart.JsonConvertSku",
                           ex.Message), HttpContext.Current.Request);
                    return null;
                }
                if (jsonSkuArray == null || jsonSkuArray.Length < 1) return null;
                JsonObject jsonSku = jsonSkuArray.GetObject(0);

                //TODO: 暂不支持多SKU下单
                string sku = jsonSku["SKU"].ToString();
                int count = Globals.SafeInt(jsonSku["Count"].ToString(), 1);
                Maticsoft.Model.Shop.Products.SKUInfo skuInfo = _skuInfoManage.GetModelBySKU(sku);
                if (skuInfo == null) return null;
                Maticsoft.Model.Shop.Products.ProductInfo productInfo = _productInfoManage.GetModel(skuInfo.ProductId);
                if (productInfo == null) return null;
                Maticsoft.Model.Shop.Products.ShoppingCartItem itemInfo = new ShoppingCartItem();
                itemInfo.MarketPrice = productInfo.MarketPrice.HasValue ? productInfo.MarketPrice.Value : 0;
                itemInfo.Name = productInfo.ProductName;
                itemInfo.Quantity = count;
                itemInfo.SellPrice = skuInfo.SalePrice;
                itemInfo.AdjustedPrice = skuInfo.SalePrice;
                itemInfo.SKU = skuInfo.SKU;
                itemInfo.ProductId = skuInfo.ProductId;
                itemInfo.UserId = userBuyer.UserID;

                #region 微信商品列表
                Model.Shop.PromoteSales.WeiXinGroupBuy WeiXinGroupBuyInfo = null;
                if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.WeiXin)
                {
                    BLL.Shop.PromoteSales.WeiXinGroupBuy bll = new BLL.Shop.PromoteSales.WeiXinGroupBuy();
                    WeiXinGroupBuyInfo = bll.GetModel(ActiveID);
                    if (WeiXinGroupBuyInfo != null)
                    {
                        //重置价格为 微信价格处理
                        if (WeiXinGroupBuyInfo.Price > 0)
                        {
                            itemInfo.AdjustedPrice = WeiXinGroupBuyInfo.Price;
                        }
                        itemInfo.SaleDes = "微信购买";
                        itemInfo.ActiveID = WeiXinGroupBuyInfo.GroupBuyId;
                        itemInfo.ActiveType = (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.WeiXin;
                    }
                }

                #endregion

                #region 限时抢购处理
                if (proSaleId > 0)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo proSaleInfo = _productInfoManage.GetProSaleModel(proSaleId);
                    if (proSaleInfo == null) return null;

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > proSaleInfo.ProSalesEndDate)
                        throw new ArgumentNullException("活动已过期");
                  

                    //重置价格为 限时抢购价
                    itemInfo.AdjustedPrice = proSaleInfo.ProSalesPrice;
                }
                #endregion

                #region 团购处理
                if (groupBuyId > 0)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo groupBuyInfo = _productInfoManage.GetGroupBuyModel(groupBuyId);
                    if (groupBuyInfo == null) return null;

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                        throw new ArgumentNullException("活动已过期");
                    //活动未开始 重定向到单品页
                    if (DateTime.Now < groupBuyInfo.GroupBuy.StartDate)
                        throw new ArgumentNullException("活动未开始");

                    //重置价格为 限时抢购价
                    itemInfo.AdjustedPrice = groupBuyInfo.GroupBuy.Price;
                }
                #endregion

                //DONE: 根据SkuId 获取SKUItem值和图片数据 BEN 2013-06-30
                List<Model.Shop.Products.SKUItem> listSkuItems = _skuInfoManage.GetSKUItemsBySkuId(skuInfo.SkuId);
                if (listSkuItems != null && listSkuItems.Count > 0)
                {
                    itemInfo.SkuValues = new string[listSkuItems.Count];
                    int index = 0;
                    listSkuItems.ForEach(xx =>
                    {
                        itemInfo.SkuValues[index++] = xx.ValueStr;
                        if (!string.IsNullOrWhiteSpace(xx.ImageUrl))
                        {
                            itemInfo.SkuImageUrl = xx.ImageUrl;
                        }
                    });
                }

                itemInfo.ThumbnailsUrl = productInfo.ThumbnailUrl1;
                itemInfo.CostPrice = skuInfo.CostPrice.HasValue ? skuInfo.CostPrice.Value : 0;
                itemInfo.CostPrice2 = skuInfo.CostPrice2.HasValue ? skuInfo.CostPrice2.Value : 0;
                itemInfo.Weight = skuInfo.Weight.HasValue ? skuInfo.Weight.Value : 0;
                itemInfo.Points = (int)(productInfo.Points.HasValue ? productInfo.Points.Value : 0);

                #region 商家Id
                BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(productInfo.SupplierId);
                if (supplierInfo != null)
                {
                    itemInfo.SupplierId = supplierInfo.SupplierId;
                    itemInfo.SupplierName = supplierInfo.Name;
                }
                #endregion

                shoppingCartInfo = new ShoppingCartInfo();
                shoppingCartInfo.Items.Add(itemInfo);
            }

            #region 批销优惠
            if (acceId < 1 && proSaleId < 1 && groupBuyId < 1) //限时抢购/团购/组合优惠套装　不参与批销优惠
            {
                try
                {
                    BLL.Shop.Sales.SalesRuleProduct salesRule = new BLL.Shop.Sales.SalesRuleProduct();
                    shoppingCartInfo = salesRule.GetWholeSale(shoppingCartInfo);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            #endregion

            return shoppingCartInfo;
        }

        #endregion

        #region 获取支付数据
        private Payment.Model.PaymentModeInfo GetPaymentModeInfo(HttpContext context)
        {
            int paymentModeId = Globals.SafeInt(context.Request.Form["PaymentModeId"], -1);
            if (paymentModeId < 1) return null;
            return Payment.BLL.PaymentModeManage.GetPaymentModeById(paymentModeId);
        }
        #endregion

        #region 获取购买人数据

        private Maticsoft.Accounts.Bus.User GetBuyerUserInfo(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return null;
            }
            Maticsoft.Accounts.Bus.User currentUser;
            if (context.Session[Globals.SESSIONKEY_USER] == null)
            {
                currentUser = new Maticsoft.Accounts.Bus.User(
                    new Maticsoft.Accounts.Bus.AccountsPrincipal(context.User.Identity.Name));
                context.Session[Globals.SESSIONKEY_USER] = currentUser;
            }
            else
            {
                currentUser = (Maticsoft.Accounts.Bus.User)context.Session[Globals.SESSIONKEY_USER];
            }
            return currentUser;
        }

        #endregion

        #region 获取收货人数据

        private Model.Shop.Shipping.ShippingAddress GetShippingAddress(HttpContext context)
        {
            int shippingAddressId = Globals.SafeInt(context.Request.Form["ShippingAddressId"], -1);
            if (shippingAddressId < 1) return null;
            return _shippingAddressManage.GetModel(shippingAddressId);
        }

        #endregion

        #region 获取配送信息

        private Model.Shop.Shipping.ShippingType GetShippingType(int ShippingID)
        {
            int shippingTypeId = Globals.SafeInt(ShippingID, -1);
            if (shippingTypeId < 1) return null;
            return _shippingTypeManage.GetModel(shippingTypeId);
        }

        #endregion

        #region 填充卖家信息

        private bool FillSellerInfo(OrderInfo orderInfo)
        {
            if (orderInfo.SupplierId.HasValue && orderInfo.SupplierId.Value > 0)
            {
                BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(orderInfo.SupplierId.Value);
                if (supplierInfo == null) return false;

                orderInfo.SupplierName = supplierInfo.Name;

                #region 填充子单卖家信息

                orderInfo.SellerID = supplierInfo.UserId;
                //DONE: 卖家名称使用店铺名称
                orderInfo.SellerName = supplierInfo.ShopName;
                orderInfo.SellerEmail = supplierInfo.ContactMail;
                orderInfo.SellerCellPhone = supplierInfo.CellPhone;

                #endregion
            }
            else
            {
                orderInfo.SupplierId = null;
                orderInfo.SupplierName = null;

                orderInfo.SellerID = null;
                orderInfo.SellerName = null;
                orderInfo.SellerEmail = null;
                orderInfo.SellerCellPhone = null;
            }
            return true;
        }

        #endregion

        #region 微信提交订单
        private string SubmitOrderWeiXin(HttpContext context)
        {
            int ActiveID = Globals.SafeInt(context.Request.Form["activeid"], -1);
            int ActiveType = Globals.SafeInt(context.Request.Form["activetype"], -1);
            JsonObject result = new JsonObject();

            #region 获取基础数据
            int groupbuyId = -1;
            if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
            {
                groupbuyId = ActiveID;
            }
            Payment.Model.PaymentModeInfo paymentModeInfo = GetPaymentModeInfo(context);
            if (paymentModeInfo == null)
            {
                result.Accumulate(KEY_STATUS, "NOPAYMENTMODEINFO");
                return result.ToString();
            }

            //获取健康卡系统编号
            int CardSysId = _cardUserInfoBLL.GetDefaultCardsysID(CurrentUser.UserName);

            //获取健康卡号
            string cardno = _cardUserInfoBLL.GetDefaultCardNo(CurrentUser.UserName);

            #endregion

            #region 1.获取购买人

            //DONE: 1.获取购买人
            Maticsoft.Accounts.Bus.User userBuyer = GetBuyerUserInfo(context);
            if (userBuyer == null)
            {
                result.Accumulate(KEY_STATUS, STATUS_NOLOGIN);
                return result.ToString();
            }
            if (userBuyer.UserType == "AA")
            {
                result.Accumulate(KEY_STATUS, STATUS_UNAUTHORIZED);
                return result.ToString();
            }

            #endregion

            #region 2.获取购物车

            //DONE: 2.获取购物车
            BLL.Shop.Products.ShoppingCartHelper shoppingCartHelper;
            ShoppingCartInfo shoppingCartInfo;
            try
            {
                //购物车
                shoppingCartInfo = _ampBLL.GetTotalPriceAfterActivity(GetShoppingCartWeiXin(context, userBuyer, out shoppingCartHelper));
            }
            catch (ArgumentNullException)
            {
                result.Accumulate(KEY_STATUS, "PROSALEEXPIRED");
                return result.ToString();
            }
            if (shoppingCartInfo == null ||
                shoppingCartInfo.Items == null ||
                shoppingCartInfo.Items.Count < 1)
            {
                result.Accumulate(KEY_STATUS, "NOSHOPPINGCARTINFO");
                return result.ToString();
            }
            //DONE: 2.1 Check 商品库存
            List<ShoppingCartItem> noStockList = new List<ShoppingCartItem>();
            foreach (ShoppingCartItem item in shoppingCartInfo.Items)
            {
                //检查购买数量是否大于库存
                if (item.Quantity < 1 || item.Quantity > _skuInfoManage.GetStockBySKU(item.SKU))
                {
                    noStockList.Add(item);
                }
            }
            if (noStockList.Count > 0)
            {
                result.Accumulate(KEY_STATUS, "NOSTOCK");
                result.Accumulate(KEY_DATA, noStockList);
                //自动移除Cookie/DB购物车中的无库存项目
                if (shoppingCartHelper != null)
                {
                    noStockList.ForEach(info =>
                    {
                       shoppingCartHelper.RemoveItem(info.ItemId);
                    });
                }
                return result.ToString();
            }

            #endregion

            #region 3.获取收货人

            //DONE: 3.获取收货人
            Model.Shop.Shipping.ShippingAddress shippingAddress = GetShippingAddress(context);
            if (shippingAddress == null)
            {
                result.Accumulate(KEY_STATUS, "NOSHIPPINGADDRESS");
                return result.ToString();
            }
            Model.Ms.Regions regionInfo = _regionManage.GetModelByCache(shippingAddress.RegionId);
            if (regionInfo == null)
            {
                result.Accumulate(KEY_STATUS, "NOREGIONINFO");
                return result.ToString();
            }
            #endregion

            #region 4.获取配送(物流)

            //DONE: 4.获取配送(物流)
            List<SupplierFreight> SupplierFreightList = null;

            if (!string.IsNullOrEmpty(context.Request.Form["supplierFreight"]))
            {
                SupplierFreightList = new JavaScriptSerializer().Deserialize<List<SupplierFreight>>(context.Request.Form["supplierFreight"].ToString());
            }
            else
            {
                SupplierFreightList = new List<SupplierFreight>();
            }

            Model.Shop.Shipping.ShippingType shippingType = new Model.Shop.Shipping.ShippingType();
            if (SupplierFreightList.Count == 1)
            {
                shippingType = GetShippingType(SupplierFreightList.First().ShippingTypeID);
                if (shippingType == null)
                {
                    result.Accumulate(KEY_STATUS, "NOSHIPPINGTYPE");
                    return result.ToString();
                }
            }

            #endregion

            #region 5.生成订单
            //DONE: 5.生成订单
            OrderInfo mainOrder = new OrderInfo();
            #region 填充订单数据

            #region 基础数据

            mainOrder.CreatedDate = DateTime.Now;
            Maticsoft.Web.Areas.MShop.Controllers.AccountController api = new Maticsoft.Web.Areas.MShop.Controllers.AccountController();
            Random ran = new Random();
            int RandKey = ran.Next(1000, 9999);
            mainOrder.OrderCode = "WX" + mainOrder.CreatedDate.ToString("yyyyMMddHHmmssfff") + RandKey.ToString();
            mainOrder.OrderOptType = 1;

            //mainOrder.Remark = orderRemark;
            mainOrder.CardSysId = CardSysId;
            mainOrder.CardNo = cardno;

            #region 支付信息

            mainOrder.PaymentTypeId = paymentModeInfo.ModeId;
            mainOrder.PaymentTypeName = paymentModeInfo.Name;
            mainOrder.PaymentGateway = paymentModeInfo.Gateway;

            result.Accumulate("GATEWAY", mainOrder.PaymentGateway);

            #endregion
            #region 优惠券数据

            mainOrder.CouponAmount = 0;
            //  mainOrder.Amount = shoppingCartInfo.TotalAdjustedPriceNew;
            shoppingCartInfo.TotalWeight = shoppingCartInfo.TotalWeight;

            //真实优惠金额
            decimal priceStr = 0;
            string couponCode = context.Request.Form["Coupon"];
            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = couponBll.GetCouponInfo(couponCode);
                if (infoModel != null)
                {

                    if (infoModel == null)
                    {
                        result.Accumulate(KEY_STATUS, "NOCOUPON");
                        result.Accumulate(STATUS_FAILED, "没有此优惠劵");

                        return result.ToString();

                    }
                    if (infoModel.Status == 2)
                    {
                        result.Accumulate(KEY_STATUS, "NOCOUPON");
                        result.Accumulate(STATUS_FAILED, "此优惠劵已使用");
                        return result.ToString();

                    }
                    if (infoModel.LimitPrice >= shoppingCartInfo.TotalAdjustedPriceNew)
                    {

                        result.Accumulate(STATUS_FAILED, "此优惠劵没有达到最低购买金额");
                        return result.ToString();
                    }

                    decimal defaultprice = 0.01m;
                    if (shoppingCartInfo.TotalAdjustedPriceNew - infoModel.CouponPrice <= 0)
                    {
                        priceStr = shoppingCartInfo.TotalAdjustedPriceNew;

                    }
                    else
                    {
                        priceStr = infoModel.CouponPrice;
                    }

                    mainOrder.CouponAmount = priceStr;
                    mainOrder.CouponCode = infoModel.CouponCode;
                    mainOrder.CouponName = infoModel.CouponName;
                    mainOrder.CouponValue = infoModel.CouponPrice;
                    mainOrder.CouponValueType = 1;
                }
            }

            #endregion
            #region 重量/运费/积分

            #region 区域差异运费计算
            int topRegionId;
            if (regionInfo.Depth > 1)
            {
                topRegionId = Globals.SafeInt(regionInfo.Path.Split(new[] { ',' })[1], -1);
            }
            else
            {
                topRegionId = regionInfo.RegionId;
            }

            Model.Shop.Shipping.ShippingRegionGroups shippingRegion =
                _shippingRegionManage.GetShippingRegion(shippingType.ModeId, topRegionId);
            #endregion

            mainOrder.Weight = shoppingCartInfo.TotalWeight;
            //主订单运费为子订单运费之和
            if (SupplierFreightList.Count == 1)
            {

                //在计算运费之前如果使用了优惠券则减掉优惠券的金额
                var CouponValue = (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);

                if (CouponValue > shoppingCartInfo.TotalAdjustedPriceNew)
                {
                    mainOrder.Amount = 0;
                }
                else
                {
                    mainOrder.Amount = shoppingCartInfo.TotalAdjustedPriceNew - CouponValue;
                }


                shoppingCartInfo.TotalWeight = shoppingCartInfo.TotalWeight;

                if (groupbuyId > 0)
                {
                    mainOrder.FreightAdjusted =
                    mainOrder.FreightActual = mainOrder.Freight = 0;
                }
                else
                {
                    mainOrder.FreightAdjusted =
                    mainOrder.FreightActual = mainOrder.Freight = _feeBLL.CalFreeShipping(topRegionId, mainOrder.Amount - CouponValue, shoppingCartInfo, shippingType, shippingRegion);

                }

                //订单总成本价 = 商品总成本价
                mainOrder.OrderCostPrice = shoppingCartInfo.TotalCostPrice;

                mainOrder.OrderCostPrice2 = shoppingCartInfo.TotalCostPrice2;

                //订单总金额(无任何优惠) 商品总价 + 运费
                mainOrder.OrderTotal = shoppingCartInfo.Items.Where(m => m.Selected == true).Sum(m => m.AdjustedPrice * m.Quantity) + mainOrder.Freight.Value;

                //订单的最后支付金额 =减去优惠券后的总价+调整后的运费
                mainOrder.Amount += mainOrder.FreightAdjusted.Value;
            }

            mainOrder.OrderPoint = shoppingCartInfo.TotalPoints;
            #endregion




            #region 订单价格

            //订单商品总价(无任何优惠)
            mainOrder.ProductTotal = shoppingCartInfo.TotalSellPrice;



            if (mainOrder.Amount < 0)
            {
                LogHelp.AddInvadeLog(
                    string.Format("非法订单金额|{0}|_Maticsoft.Web.Handlers.Shop.OrderHandler.SubmitOrder",
                        mainOrder.Amount.ToString("F2")), HttpContext.Current.Request);
                result.Accumulate(KEY_STATUS, "ILLEGALORDERAMOUNT");
                return result.ToString();
            }

            #endregion

            #region 限时抢购
            int proSaleId = Common.Globals.SafeInt(context.Request.Form["ProSaleId"], -1);
            if (proSaleId > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo proSaleInfo = _productInfoManage.GetProSaleModel(proSaleId);
                if (proSaleInfo != null)
                {
                    mainOrder.ActivityName = string.Format("限时抢购[{0}]", proSaleInfo.CountDownId);
                    //活动优惠金额 = 总金额(含运费无任何优惠) - 最终支付(含运费优惠后)
                    mainOrder.ActivityFreeAmount = mainOrder.OrderTotal - mainOrder.Amount;
                    mainOrder.ActivityStatus = 1;
                }
            }
            #endregion

            #region 团购数据
            //订单中一次只能提交一个团购商品或抢购商品 20141004
            if (shoppingCartInfo.Items.Any(x => x.PromotionType > -1))
            {
                if (groupbuyId > 0)
                {
                    var groupProduct = groupBuyBll.GetModelByGroupID(groupbuyId);
                    if (groupProduct != null && groupProduct.PromotionType == 1)
                    {
                        mainOrder.GroupBuyId = groupProduct.GroupBuyId;
                        //查看用户购买数量限制,一个帐号买一个派送只能一件
                        if (groupProduct.MaxCount > 0 && groupProduct.PromotionType == 1)
                        {
                            if (groupBuyBll.GetGroupBuyLimt4Hour(userBuyer.UserID) == 1)
                            {
                                throw new Exception("一个账号在每个时段只能限购一个抢购中的产品！");
                            }
                            if (groupProduct.BuyCount >= groupProduct.MaxCount)
                            {
                                throw new Exception("您抢购的商品已达到允许抢购的上限!");
                            }
                            if (mainOrder.OrderCode.IndexOf("WX") >= 0)
                                Maticsoft.Common.ErrorLogTxt.GetInstance("团购并发日志Web").Write("订单已执行了并发控制" + mainOrder.OrderCode);
                        }
                    }
                }
                mainOrder.GroupBuyStatus = 1;
            }
            //不能通过该参数传，直接从团购过来用该参数

            if (groupbuyId > 0)
            {
                Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = groupBuyBll.GetModelByCache(groupbuyId);
                if (buyModel != null)
                {
                    mainOrder.GroupBuyId = buyModel.GroupBuyId;
                    mainOrder.GroupBuyPrice = buyModel.Price;
                    mainOrder.GroupBuyStatus = 1;
                }
            }

            #endregion

            mainOrder.OrderType = 1;

            //DONE: 货到付款 下单后直接进入 6.正在处理 流程 其它流程不变 BEN MODIFY 20131205
            mainOrder.OrderStatus = mainOrder.PaymentGateway == "cod" ? 1 : 0;

            #endregion

            #region 购买人信息

            mainOrder.BuyerID = userBuyer.UserID;
            mainOrder.BuyerName = userBuyer.UserName;
            //TODO: 用户Email为空时, 暂以默认Email下单 BEN ADD 20130701
            mainOrder.BuyerEmail = string.IsNullOrWhiteSpace(userBuyer.Email) ? "pay@maticsoft.com" : userBuyer.Email;
            mainOrder.BuyerCellPhone = userBuyer.Phone;

            #endregion

            #region 拆单对象
            Dictionary<int, List<OrderItems>> dicSuppOrderItems = new Dictionary<int, List<OrderItems>>();
            #endregion

            Dictionary<long, int> dicProsItems = new Dictionary<long, int>();

            #region 购物车 -> 订单项目
            OrderItems tmpOrderItem;
            //购物车 -> 订单项目
            shoppingCartInfo.Items.ForEach(item =>
            {
                tmpOrderItem = new OrderItems
                {
                    //TODO: 警告: 商品信息根据Cookie获取, 暂未与DB及时同步
                    Name = item.Name,
                    SKU = item.SKU,
                    Quantity = item.Quantity,
                    ShipmentQuantity = item.Quantity,
                    ThumbnailsUrl = item.ThumbnailsUrl,
                    Points = item.Points,
                    Weight = item.Weight,
                    ProductId = item.ProductId,
                    Description = item.Description,
                    CostPrice = item.CostPrice,
                    CostPrice2 = item.CostPrice2,
                    SellPrice = item.SellPrice,
                    AdjustedPrice = item.AdjustedPrice,
                    Deduct = item.SellPrice - item.AdjustedPrice,

                    //商家信息
                    SupplierId = item.SupplierId,
                    SupplierName = item.SupplierName,
                    //活动ID
                    ActiveID = item.ActiveID,
                    ActiveType = item.ActiveType

                };


                //marke,促销类型是团购
                if (item.PromotionType > -1)
                {

                    if (dicProsItems.ContainsKey(item.ProductId))
                    {
                        dicProsItems[item.ProductId] += item.Quantity;
                    }
                    else
                    {
                        dicProsItems.Add(item.ProductId, item.Quantity);
                    }
                }

                //将SKU信息记录到订单项目的Attribute中 简单记录 逗号分割, 复杂的可以为Json结构
                if (item.SkuValues != null && item.SkuValues.Length > 0)
                {
                    tmpOrderItem.Attribute = string.Join(",", item.SkuValues);
                }

                //填充订单项
                mainOrder.OrderItems.Add(tmpOrderItem);

                //填充商家订单项
                if (tmpOrderItem.SupplierId.HasValue && tmpOrderItem.SupplierId.Value > 0)
                {
                    if (dicSuppOrderItems.ContainsKey(tmpOrderItem.SupplierId.Value))
                    {
                        dicSuppOrderItems[tmpOrderItem.SupplierId.Value].Add(tmpOrderItem);
                    }
                    else
                    {
                        dicSuppOrderItems.Add(tmpOrderItem.SupplierId.Value,
                            new List<OrderItems> { tmpOrderItem });
                    }
                }
                else
                {
                    if (dicSuppOrderItems.ContainsKey(0))
                    {
                        dicSuppOrderItems[0].Add(tmpOrderItem);
                    }
                    else
                    {
                        dicSuppOrderItems.Add(0,
                            new List<OrderItems> { tmpOrderItem });
                    }
                }
            });

            #endregion

            #region 收货人信息

            mainOrder.RegionId = shippingAddress.RegionId;
            mainOrder.ShipRegion = shippingAddress.RegionFullName; //_regionManage.GetFullNameById4Cache(regionInfo.RegionId);
            mainOrder.ShipName = shippingAddress.ShipName;
            mainOrder.ShipEmail = shippingAddress.EmailAddress;
            mainOrder.ShipCellPhone = shippingAddress.CelPhone;
            mainOrder.ShipTelPhone = shippingAddress.TelPhone;
            mainOrder.ShipAddress = shippingAddress.Address;
            mainOrder.ShipZipCode = shippingAddress.Zipcode;

            #endregion

            #region 配送信息(物流)
            if (shippingType.ModeId != 0)
            {
                mainOrder.ShippingModeId = shippingType.ModeId;
                mainOrder.ShippingModeName = shippingType.Name;
                mainOrder.RealShippingModeId = shippingType.ModeId;
                mainOrder.RealShippingModeName = shippingType.Name;
            }
            mainOrder.ShippingStatus = 0;
            #endregion

            #region 自动拆单
            int subOrderIndex = 1;
            //判断是否购买了多个商家的商品, 并进行拆单
            if (dicSuppOrderItems.Count > 1)
            {
                decimal? totalFreight = 0;
                #region 拆单逻辑
                foreach (KeyValuePair<int, List<OrderItems>> item in dicSuppOrderItems)
                {
                    //根据主订单构造子订单
                    OrderInfo subOrder = new OrderInfo(mainOrder);

                    #region Reset 重量/运费/积分/价格
                    subOrder.Weight = 0;
                    subOrder.FreightAdjusted = subOrder.FreightActual = subOrder.Freight = 0;
                    subOrder.OrderPoint = 0;
                    subOrder.ProductTotal = 0;
                    subOrder.OrderCostPrice = 0;
                    subOrder.OrderCostPrice2 = 0;
                    subOrder.OrderOptionPrice = 0;
                    subOrder.OrderProfit = 0;
                    subOrder.Amount = 0;
                    subOrder.CardSysId = CardSysId;
                    subOrder.OrderOptType = 1;
                    #endregion

                    #region 重新计算 重量/积分/价格
                    item.Value.ForEach(info =>
                    {
                        subOrder.Weight += info.Weight * info.Quantity;
                        subOrder.OrderPoint += info.Points * info.Quantity;

                        //订单商品总价(无优惠)
                        subOrder.ProductTotal += info.SellPrice * info.Quantity;
                        //订单总成本价 = 项目总成本价
                        subOrder.OrderCostPrice += info.CostPrice * info.Quantity;
                        subOrder.OrderCostPrice2 += info.CostPrice2 * info.Quantity;
                        //订单最终支付金额 = 商品总价
                        subOrder.Amount += info.AdjustedPrice * info.Quantity;
                    });

                    var c = (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);
                    var avgCouponValue = Math.Floor((c * subOrder.Amount / shoppingCartInfo.TotalAdjustedPriceNew));
                    subOrder.CouponAmount = avgCouponValue;
                    Maticsoft.BLL.shop.freight.Shop_ProductsFreight sff = new BLL.shop.freight.Shop_ProductsFreight();
                    List<Maticsoft.Model.shop.Freight.Shop_ProductsFreight> sfflist = new List<Model.shop.Freight.Shop_ProductsFreight>();

                    var supplierlist = shoppingCartInfo.Items.Where(q => q.SupplierId == item.Key).ToList();
                    decimal SuppTotalAdjustedPrice = 0;
                    foreach (var y in supplierlist)
                    {
                        var z = sff.GetModel(y.ProductId);
                        if (z != null && ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
                        {
                            sfflist.Add(z);
                        }
                        SuppTotalAdjustedPrice = SuppTotalAdjustedPrice + (y.AdjustedPrice * y.Quantity);
                    }

                    int shippingTypeId = SupplierFreightList.Where(m => m.SupplierId == item.Key).First().ShippingTypeID;
                    Maticsoft.Model.Shop.Shipping.ShippingType ShippingType = _shippingTypeManage.GetModel(shippingTypeId);
                    Model.Shop.Shipping.ShippingRegionGroups SubshippingRegion = _shippingRegionManage.GetShippingRegion(shippingTypeId, topRegionId);
                    shoppingCartInfo.TotalWeight = int.Parse(subOrder.Weight.ToString());
                    if (groupbuyId > 0)
                    {
                        subOrder.FreightAdjusted = subOrder.FreightActual = subOrder.Freight = 0;
                    }
                    else
                    {
                        if (sfflist.Count == shoppingCartInfo.Items.Where(l => l.SupplierId == item.Key).Count())
                        {
                            subOrder.FreightAdjusted = subOrder.FreightActual = subOrder.Freight = sfflist.Max(z => z.Freight);
                        }
                        else
                        {
                            subOrder.FreightAdjusted = subOrder.FreightActual = subOrder.Freight = _feeBLL.CalFreeShipping(topRegionId, SuppTotalAdjustedPrice - avgCouponValue, shoppingCartInfo, ShippingType, shippingRegion);
                        }
                    }

                    totalFreight += subOrder.Freight;
                    //发票信息
                    string Remark = SupplierFreightList.Where(m => m.SupplierId == item.Key).First().Remark;
                    subOrder.Remark = Remark;

                    //订单总金额(含优惠) 商品总价 + 运费
                    subOrder.OrderTotal = subOrder.ProductTotal + subOrder.Freight.Value;
                    //订单最终支付金额 = 商品总价(含优惠) + 调整后运费-优惠劵金额
                    subOrder.Amount += subOrder.FreightAdjusted.Value - avgCouponValue;

                    //TODO: 均分主订单的优惠给子订单, 作为退款使用
                    #endregion

                    //订单项目
                    subOrder.OrderItems = item.Value;

                    subOrder.OrderType = 2;

                    #region 填充子单商家信息
                    subOrder.SupplierId = item.Key;
                    if (!FillSellerInfo(subOrder))
                    {
                        result.Accumulate(KEY_STATUS, "NOSUPPLIERINFO");
                        return result.ToString();
                    }
                    #endregion

                    #region 子订单基础数据
                    //DONE: 防止运算过快产生相同订单号
                    subOrder.CreatedDate = mainOrder.CreatedDate.AddMilliseconds(subOrderIndex++);

                    subOrder.OrderCode = "WX" + subOrder.CreatedDate.ToString("yyyyMMddHHmmssfff") + ran.Next(1000, 9999).ToString();
                    subOrder.RealShippingModeId = shippingTypeId;
                    subOrder.RealShippingModeName = ShippingType.Name;
                    subOrder.ShippingModeId = shippingTypeId;
                    subOrder.ShippingModeName = ShippingType.Name;
                    subOrder.ExpressCompanyName = ShippingType.ExpressCompanyName;
                    subOrder.ExpressCompanyAbb = ShippingType.ExpressCompanyEn;
                    #endregion

                    mainOrder.SubOrders.Add(subOrder);
                }

                #endregion

                #region yjx 全场免邮
                // totalFreight = 0; 
                #endregion

                //计算总运费
                mainOrder.FreightAdjusted =
                    mainOrder.FreightActual =
                    mainOrder.Freight = totalFreight;
                //订单总成本价 = 商品总成本价
                mainOrder.OrderCostPrice = shoppingCartInfo.TotalCostPrice;
                mainOrder.OrderCostPrice2 = shoppingCartInfo.TotalCostPrice2;

                //订单总金额(无任何优惠) 商品总价 + 运费
                mainOrder.OrderTotal = shoppingCartInfo.TotalSellPrice + mainOrder.Freight.Value;

                //订单最终支付金额 = 项目调整后总售价 + 调整后运费-优惠券价格

                var CouponValue = (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);

                if (CouponValue > shoppingCartInfo.TotalAdjustedPriceNew)
                {
                    mainOrder.Amount = mainOrder.FreightAdjusted.Value;
                }
                else
                {
                    mainOrder.Amount = shoppingCartInfo.TotalAdjustedPriceNew + mainOrder.FreightAdjusted.Value - (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);
                }
                mainOrder.HasChildren = true;   //有子订单
            }
            else
            {
                //没有购买多个商家的商品
                mainOrder.SupplierId = shoppingCartInfo.Items[0].SupplierId;
                mainOrder.SupplierName = shoppingCartInfo.Items[0].SupplierName;
                mainOrder.HasChildren = false;  //无子订单

                //在计算运费之前如果使用了优惠券则减掉优惠券的金额
                var CouponValue = (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);

                if (CouponValue > shoppingCartInfo.TotalAdjustedPriceNew)
                {
                    mainOrder.Amount = 0;
                }
                else
                {
                    mainOrder.Amount = shoppingCartInfo.TotalAdjustedPriceNew - CouponValue;
                }


                shoppingCartInfo.TotalWeight = shoppingCartInfo.TotalWeight;

                Maticsoft.BLL.shop.freight.Shop_ProductsFreight sff = new BLL.shop.freight.Shop_ProductsFreight();
                List<Maticsoft.Model.shop.Freight.Shop_ProductsFreight> sfflist = new List<Model.shop.Freight.Shop_ProductsFreight>();

                //判断单商户中包含的特殊产品邮费是否跟购物车所在的数量一致
                foreach (var item in shoppingCartInfo.Items)
                {

                    var x = sff.GetModel(item.ProductId);
                    if (x != null && ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
                    {
                        sfflist.Add(x);
                    }
                }
                if (groupbuyId > 0)
                {
                    mainOrder.FreightAdjusted =
                            mainOrder.FreightActual =
                                mainOrder.Freight = 0;
                }
                else
                {
                    if (sfflist.Count == shoppingCartInfo.Items.Count)
                    {
                        mainOrder.FreightAdjusted =
                           mainOrder.FreightActual =
                           mainOrder.Freight = sfflist.Max(x => x.Freight);


                    }
                    else
                    {
                        mainOrder.FreightAdjusted =
                        mainOrder.FreightActual =
                        mainOrder.Freight = _feeBLL.CalFreeShipping(topRegionId, mainOrder.Amount, shoppingCartInfo, shippingType, shippingRegion);


                    }
                }


                //订单总成本价 = 商品总成本价
                mainOrder.OrderCostPrice = shoppingCartInfo.TotalCostPrice;
                mainOrder.OrderCostPrice2 = shoppingCartInfo.TotalCostPrice2;

                //订单总金额(无任何优惠) 商品总价 + 运费
                mainOrder.OrderTotal = shoppingCartInfo.Items.Where(m => m.Selected == true).Sum(m => m.AdjustedPrice * m.Quantity) + mainOrder.Freight.Value;

                //订单的最后支付金额 =减去优惠券后的总价+调整后的运费
                mainOrder.Amount = shoppingCartInfo.TotalAdjustedPriceNew + mainOrder.FreightAdjusted.Value - (mainOrder.CouponAmount == null ? 0 : mainOrder.CouponAmount.Value);
            }
            #endregion

            #region 填充主单商家信息
            if (!FillSellerInfo(mainOrder))
            {
                result.Accumulate(KEY_STATUS, "NOSUPPLIERINFO");
                return result.ToString();
            }
            #endregion

            #region 订单分销逻辑

            #endregion
            #endregion

            #region 执行事务-创建订单
            try
            {
                mainOrder.OrderId = BLL.Shop.Order.OrderManage.CreateOrder(mainOrder);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog("订单创建失败: " + ex.Message, ex.StackTrace, context.Request);
                throw;
            }
            #endregion

            #endregion

            #region 6.提示生成成功, 进行支付

            //DONE: 6.提示生成成功, 进行支付
            result.Accumulate(KEY_DATA, new
            {
                mainOrder.OrderId,
                mainOrder.OrderCode,
                mainOrder.Amount,
                mainOrder.PaymentTypeId,
                mainOrder.PaymentTypeName
            });

            if (mainOrder.OrderId == -1)
            {
                result.Accumulate(KEY_STATUS, STATUS_FAILED);
                return result.ToString();
            }
            //更新优惠券信息
            if (!String.IsNullOrWhiteSpace(couponCode))
            {
                couponBll.UseCoupon(couponCode, mainOrder.BuyerID, mainOrder.BuyerEmail);
            }

            //清空Cookie/DB购物车
            if (shoppingCartHelper != null)
            {
                shoppingCartHelper.ClearShoppingCart();
            }

            result.Accumulate(KEY_STATUS, STATUS_SUCCESS);

            #endregion

            return result.ToString();
        }
        #endregion

        #region 微信购物车处理


        private ShoppingCartInfo GetShoppingCartWeiXin(HttpContext context, Maticsoft.Accounts.Bus.User userBuyer,
           out BLL.Shop.Products.ShoppingCartHelper shoppingCartHelper)
        {
            ShoppingCartInfo shoppingCartInfo = null;
            string jsonSkuStr = context.Request.Form["SkuInfos"];
            int proSaleId = Globals.SafeInt(context.Request.Form["ProSaleId"], -1);
            int groupBuyId = Globals.SafeInt(context.Request.Form["GroupBuyId"], -1);
            int acceId = Globals.SafeInt(context.Request.Form["AcceId"], -1);
            int ActiveID = Globals.SafeInt(context.Request.Form["activeid"], -1);
            int ActiveType = Globals.SafeInt(context.Request.Form["activetype"], -1);
            if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
            {
                groupBuyId = ActiveID;
            }
            if (acceId > 0)//组合优惠套装
            {
                shoppingCartHelper = null;
                BLL.Shop.Products.SKUInfo skuManage = new BLL.Shop.Products.SKUInfo();
                BLL.Shop.Products.ProductAccessorie prodAcceBll = new BLL.Shop.Products.ProductAccessorie();
                ProductAccessorie prodAcceModel = prodAcceBll.GetModel(acceId);
                if (prodAcceModel == null || prodAcceModel.Type != 2)
                {
                    return null;
                }
                List<SKUInfo> skulist = skuManage.GetSKUListByAcceId(acceId, 0);
                if (skulist == null || skulist.Count < 2)//每组商品最少有两条数据
                {
                    return null;
                }

                decimal totalPrice = 0;//原　　价
                decimal dealsPrices = 0;//总优惠金额
                foreach (var item in skulist)
                {
                    totalPrice += item.SalePrice;
                }
                dealsPrices = totalPrice - prodAcceModel.DiscountAmount;
                decimal dealsPrice = dealsPrices / skulist.Count;//单个商品优惠的金额
                shoppingCartInfo = new ShoppingCartInfo();
                ShoppingCartItem cartItem;
                foreach (var item in skulist)
                {
                    cartItem = new ShoppingCartItem();
                    cartItem.MarketPrice = item.MarketPrice.HasValue ? item.MarketPrice.Value : 0;
                    cartItem.Name = item.ProductName;
                    cartItem.Quantity = 1;
                    cartItem.SellPrice = item.SalePrice;
                    cartItem.AdjustedPrice = item.SalePrice - dealsPrice;
                    cartItem.SKU = item.SKU;
                    cartItem.ProductId = item.ProductId;
                    cartItem.UserId = userBuyer.UserID;

                    //DONE: 根据SkuId 获取SKUItem值和图片数据 BEN 2013-06-30
                    List<SKUItem> listSkuItems = item.SkuItems;
                    if (listSkuItems != null && listSkuItems.Count > 0)
                    {
                        cartItem.SkuValues = new string[listSkuItems.Count];
                        int index = 0;
                        listSkuItems.ForEach(xx =>
                        {
                            cartItem.SkuValues[index++] = xx.ValueStr;
                            if (!string.IsNullOrWhiteSpace(xx.ImageUrl))
                            {
                                cartItem.SkuImageUrl = xx.ImageUrl;
                            }
                        });
                    }
                    //TODO: 未使用SKU缩略图 BEN ADD 2013-06-30
                    cartItem.ThumbnailsUrl = item.ProductThumbnailUrl;
                    cartItem.CostPrice = item.CostPrice.HasValue ? item.CostPrice.Value : 0;
                    cartItem.CostPrice2 = item.CostPrice2.HasValue ? item.CostPrice2.Value : 0;
                    cartItem.Weight = item.Weight.HasValue ? item.Weight.Value : 1;

                    #region 商家
                    if (item.SupplierId > 0)
                    {
                        BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                        Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(item.SupplierId);
                        if (supplierInfo != null)
                        {
                            cartItem.SupplierId = supplierInfo.SupplierId;
                            cartItem.SupplierName = supplierInfo.Name;
                        }
                    }
                    #endregion

                    shoppingCartInfo.Items.Add(cartItem);
                }
            }
            else if (string.IsNullOrWhiteSpace(jsonSkuStr))  //判断 是否使用 Cookie 加载购物车
            {
                shoppingCartHelper = new BLL.Shop.Products.ShoppingCartHelper(userBuyer.UserID);
                //DONE: 获取已选中内容的购物车进行 购物车 部分商品 下单 BEN Modify 20130923
                shoppingCartInfo = shoppingCartHelper.GetShoppingCart4Selected();
            }
            else
            {
                //使用 SKU 加载购物车
                shoppingCartHelper = null;
                //DONE: 目前是 SkuId 应为 SKU | DONE 20130901
                JsonArray jsonSkuArray;
                try
                {
                    jsonSkuArray = Json.Conversion.JsonConvert.Import<JsonArray>(jsonSkuStr);
                }
                catch (Exception ex)
                {
                    LogHelp.AddInvadeLog(
                       string.Format("非法SKU数据|{0}|_Maticsoft.Web.Handlers.Shop.OrderHandler.GetShoppingCart.JsonConvertSku",
                           ex.Message), HttpContext.Current.Request);
                    return null;
                }
                if (jsonSkuArray == null || jsonSkuArray.Length < 1) return null;
                JsonObject jsonSku = jsonSkuArray.GetObject(0);

                //TODO: 暂不支持多SKU下单
                string sku = jsonSku["SKU"].ToString();
                int count = Globals.SafeInt(jsonSku["Count"].ToString(), 1);
                Maticsoft.Model.Shop.Products.SKUInfo skuInfo = _skuInfoManage.GetModelBySKU(sku);
                if (skuInfo == null) return null;
                Maticsoft.Model.Shop.Products.ProductInfo productInfo = _productInfoManage.GetModel(skuInfo.ProductId);
                if (productInfo == null) return null;
                Maticsoft.Model.Shop.Products.ShoppingCartItem itemInfo = new ShoppingCartItem();
                itemInfo.MarketPrice = productInfo.MarketPrice.HasValue ? productInfo.MarketPrice.Value : 0;
                itemInfo.Name = productInfo.ProductName;
                itemInfo.Quantity = count;
                itemInfo.SellPrice = skuInfo.SalePrice;
                itemInfo.AdjustedPrice = skuInfo.SalePrice;
                itemInfo.SKU = skuInfo.SKU;
                itemInfo.ProductId = skuInfo.ProductId;
                itemInfo.UserId = userBuyer.UserID;

                #region 微信商品列表
                Model.Shop.PromoteSales.WeiXinGroupBuy WeiXinGroupBuyInfo = null;
                if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.WeiXin)
                {
                    BLL.Shop.PromoteSales.WeiXinGroupBuy bll = new BLL.Shop.PromoteSales.WeiXinGroupBuy();
                    WeiXinGroupBuyInfo = bll.GetModel(ActiveID);
                    if (WeiXinGroupBuyInfo != null)
                    {
                        // return new RedirectResult("/");
                        //重置价格为 微信价格处理
                        if (WeiXinGroupBuyInfo.Price > 0)
                        {
                            itemInfo.AdjustedPrice = WeiXinGroupBuyInfo.Price;
                        }
                        itemInfo.SaleDes = "微信购买";
                        itemInfo.ActiveID = WeiXinGroupBuyInfo.GroupBuyId;
                        itemInfo.ActiveType = (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.WeiXin;
                    }
                }

                #endregion

                #region 限时抢购处理
                if (proSaleId > 0)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo proSaleInfo = _productInfoManage.GetProSaleModel(proSaleId);
                    if (proSaleInfo == null) return null;

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > proSaleInfo.ProSalesEndDate)
                        throw new ArgumentNullException("活动已过期");
                   

                    //重置价格为 限时抢购价
                    itemInfo.AdjustedPrice = proSaleInfo.ProSalesPrice;
                }
                #endregion

                #region 团购处理
                if (groupBuyId > 0)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo groupBuyInfo = _productInfoManage.GetGroupBuyModel(groupBuyId);
                    if (groupBuyInfo == null) return null;

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                        throw new ArgumentNullException("活动已过期");
                    //活动已过期 重定向到单品页
                    if (DateTime.Now < groupBuyInfo.GroupBuy.StartDate)
                        throw new ArgumentNullException("活动未开始");

                    //重置价格为 限时抢购价
                    itemInfo.AdjustedPrice = groupBuyInfo.GroupBuy.Price;
                    itemInfo.SaleDes = "微信购买";
                    itemInfo.ActiveID = groupBuyInfo.GroupBuy.GroupBuyId;
                    itemInfo.ActiveType = (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy;
                }
                #endregion

                //DONE: 根据SkuId 获取SKUItem值和图片数据 BEN 2013-06-30
                List<Model.Shop.Products.SKUItem> listSkuItems = _skuInfoManage.GetSKUItemsBySkuId(skuInfo.SkuId);
                if (listSkuItems != null && listSkuItems.Count > 0)
                {
                    itemInfo.SkuValues = new string[listSkuItems.Count];
                    int index = 0;
                    listSkuItems.ForEach(xx =>
                    {
                        itemInfo.SkuValues[index++] = xx.ValueStr;
                        if (!string.IsNullOrWhiteSpace(xx.ImageUrl))
                        {
                            itemInfo.SkuImageUrl = xx.ImageUrl;
                        }
                    });
                }

                itemInfo.ThumbnailsUrl = productInfo.ThumbnailUrl1;
                itemInfo.CostPrice = skuInfo.CostPrice.HasValue ? skuInfo.CostPrice.Value : 0;
                itemInfo.CostPrice2 = skuInfo.CostPrice2.HasValue ? skuInfo.CostPrice2.Value : 0;
                itemInfo.Weight = skuInfo.Weight.HasValue ? skuInfo.Weight.Value : 0;
                itemInfo.Points = (int)(productInfo.Points.HasValue ? productInfo.Points.Value : 0);

                #region 商家Id
                BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(productInfo.SupplierId);
                if (supplierInfo != null)
                {
                    itemInfo.SupplierId = supplierInfo.SupplierId;
                    itemInfo.SupplierName = supplierInfo.Name;
                }
                #endregion

                shoppingCartInfo = new ShoppingCartInfo();
                shoppingCartInfo.Items.Add(itemInfo);
            }

            #region 批销优惠
            if (acceId < 1 && proSaleId < 1 && groupBuyId < 1) //限时抢购/团购/组合优惠套装　不参与批销优惠
            {
                try
                {
                    BLL.Shop.Sales.SalesRuleProduct salesRule = new BLL.Shop.Sales.SalesRuleProduct();
                    shoppingCartInfo = salesRule.GetWholeSale(shoppingCartInfo);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            #endregion

            return shoppingCartInfo;
        }


        #endregion

        #endregion

    }
}