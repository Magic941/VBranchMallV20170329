/**
* UserHandler.cs
*
* 功 能： 商城 API
* 类 名： UserHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/12/24 17:04:23  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Members;
using Maticsoft.Json;
using Maticsoft.Json.RPC;
using Maticsoft.Model.Members;
using Maticsoft.Web.Handlers.API;
using Maticsoft.Model.Shop.Products;
using System.Collections.Generic;
using Maticsoft.Model.Shop.Order;
using Maticsoft.Common;
using System.Data;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.Model.Ms;
using Maticsoft.BLL.Shop.Order;
using Webdiyer.WebControls.Mvc;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Model.Shop.Shipping;

namespace Maticsoft.Web.Handlers.Shop.API
{
    public partial class ShopHandler
    {
        private readonly BLL.Ms.Regions _regionManage = new BLL.Ms.Regions();
        private readonly BLL.Shop.Shipping.ShippingAddress _shippingAddressManage =
           new BLL.Shop.Shipping.ShippingAddress();
        private readonly BLL.Shop.Shipping.ShippingRegionGroups _shippingRegionManage =
            new BLL.Shop.Shipping.ShippingRegionGroups();
        private readonly BLL.Shop.Shipping.ShippingType _shippingTypeManage = new BLL.Shop.Shipping.ShippingType();
        private Maticsoft.BLL.Ms.Regions RegionBll = new BLL.Ms.Regions();
        private Maticsoft.BLL.Shop.Coupon.CouponInfo couponBll = new CouponInfo();
        private readonly BLL.Shop.Shipping.ShippingAddress _addressManage = new BLL.Shop.Shipping.ShippingAddress();
        private readonly BLL.Shop.Shipping.Shop_freefreight _feeBLL = new BLL.Shop.Shipping.Shop_freefreight();
        private readonly BLL.Shop.ActivityManage.AMPBLL _ampBLL = new BLL.Shop.ActivityManage.AMPBLL();

        [JsonRpcMethod("SubmitOrderInfo", Idempotent = true)]
        [JsonRpcHelp("结算信息")]

        public JsonObject SubmitOrderInfo(int userID, int shippingAddressId, int shippingTypeId, int paymentModeId, string conpon)
        {
            if (userID <= 0)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            JsonObject result = new JsonObject();

            OrderInfo mainOrder = new OrderInfo();
            #region 购买人
            //Maticsoft.Accounts.Bus.User currentUser = new User(new Maticsoft.Accounts.Bus.AccountsPrincipal(userName));
            //if (currentUser == null)
            //{
            //    return new Result(ResultStatus.Success, null);
            //}
            BLL.Shop.Products.ShoppingCartHelper shoppingCartHelper = new BLL.Shop.Products.ShoppingCartHelper(userID);
            Model.Shop.Products.ShoppingCartInfo shoppingCartInfo = _ampBLL.GetTotalPriceAfterActivity(shoppingCartHelper.GetShoppingCart4Selected());
            if (shoppingCartInfo == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            #endregion

            #region 收货人
            if (shippingAddressId < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            Model.Shop.Shipping.ShippingAddress shippingAddress = _shippingAddressManage.GetModel(shippingAddressId);
            if (shippingAddress == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            Model.Ms.Regions regionInfo = _regionManage.GetModelByCache(shippingAddress.RegionId);
            if (regionInfo == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonObject json = new JsonObject();
            JsonArray shipAddressArray = new JsonArray();
            json.Put("id", shippingAddress.RegionId);
            json.Put("name", shippingAddress.ShipName);
            json.Put("addressArea", shippingAddress.RegionFullName);
            json.Put("emial", shippingAddress.EmailAddress);
            json.Put("cellphone", shippingAddress.CelPhone);
            json.Put("telphone", shippingAddress.TelPhone);
            json.Put("zipcode", shippingAddress.Zipcode);
            json.Put("address", shippingAddress.Address);
            shipAddressArray.Add(json);
            result.Put("address_info", shipAddressArray);
            #endregion
            #region 获取配送信息
            JsonObject shipJson = new JsonObject();
            if (shippingTypeId < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            Model.Shop.Shipping.ShippingType shippingType = _shippingTypeManage.GetModel(shippingTypeId);
            if (shippingType == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonArray spArray = new JsonArray();
            shipJson.Put("shipid", shippingType.ModeId);
            shipJson.Put("shipname", shippingType.Name);
            shipJson.Put("status", 0);
            shipJson.Put("companyname", shippingType.ExpressCompanyName);
            shipJson.Put("companyabb", shippingType.ExpressCompanyEn);
            spArray.Add(shipJson);
            result.Put("address_ShipType", spArray);
            #endregion
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

            #region 获取支付数据
            JsonObject payJson = new JsonObject();
            if (paymentModeId < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            Payment.Model.PaymentModeInfo paymentModeInfo = Payment.BLL.PaymentModeManage.GetPaymentModeById(paymentModeId);
            if (paymentModeInfo == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonArray payArray = new JsonArray();
            payJson.Put("typeId", paymentModeInfo.ModeId);
            payJson.Put("typeName", paymentModeInfo.Name);
            payJson.Put("gateway", paymentModeInfo.Gateway);
            payArray.Add(payJson);
            result.Put("payment_info", payArray);
            #endregion
            #region 获取优惠劵数据
            mainOrder.CouponAmount = 0;
            Model.Shop.Coupon.CouponInfo infoModel = couponBll.GetCouponInfo(conpon);
            if (infoModel != null)
            {
                mainOrder.CouponAmount = infoModel.CouponPrice;
                mainOrder.CouponCode = infoModel.CouponCode;
                mainOrder.CouponName = infoModel.CouponName;
                mainOrder.CouponValue = infoModel.CouponPrice;
                mainOrder.CouponValueType = 1;
            }
            #endregion
            #region 订单信息
            JsonObject orderObject = new JsonObject();
            //ShoppingCartInfo shoppingCartInfo =
            mainOrder.FreightAdjusted = mainOrder.FreightActual = mainOrder.Freight = _feeBLL.CalFreeShipping(topRegionId, shoppingCartInfo.TotalSellPrice, shoppingCartInfo, shippingType, shippingRegion);
            #region 订单价格

            //订单商品总价(无任何优惠)
            mainOrder.ProductTotal = shoppingCartInfo.TotalSellPrice;

            //订单总成本价 = 项目总成本价 + 实际运费
            mainOrder.OrderCostPrice = shoppingCartInfo.TotalCostPrice + mainOrder.FreightActual;

            //订单总金额(无任何优惠) 商品总价 + 运费
            mainOrder.OrderTotal = shoppingCartInfo.TotalSellPrice + mainOrder.Freight.Value;

            //订单最终支付金额 = 项目调整后总售价 + 调整后运费-优惠券价格
            mainOrder.Amount = shoppingCartInfo.TotalAdjustedPrice + mainOrder.FreightAdjusted.Value - mainOrder.CouponAmount.Value;

            #endregion
            JsonArray orderArray = new JsonArray();
            orderObject.Put("freight", mainOrder.FreightActual);
            orderObject.Put("totalprice", mainOrder.ProductTotal);
            orderObject.Put("payprice", mainOrder.Amount);
            orderArray.Add(orderObject);
            result.Put("checkout _addup", orderArray);
            #endregion
            return new Result(ResultStatus.Success, result);
        }



        #region GetCartInfo4SKU
        private ShoppingCartInfo GetCartInfo4SKU(int UserID, ProductInfo productInfo, SKUInfo skuInfo, int quantity, ProductInfo proSaleInfo = null, ProductInfo groupBuyInfo = null)
        {
            ShoppingCartInfo cartInfo = new ShoppingCartInfo();
            //TODO: 未支持多个SKU BEN ADD 2013-06-23
            ShoppingCartItem cartItem = new ShoppingCartItem();
            cartItem.MarketPrice = productInfo.MarketPrice.HasValue ? productInfo.MarketPrice.Value : 0;
            cartItem.Name = productInfo.ProductName;
            cartItem.Quantity = quantity < 1 ? 1 : quantity;
            cartItem.SellPrice = skuInfo.SalePrice;
            cartItem.AdjustedPrice = skuInfo.SalePrice;
            cartItem.SKU = skuInfo.SKU;
            cartItem.ProductId = skuInfo.ProductId;
            cartItem.UserId = UserID;

            #region 限时抢购价格处理
            if (proSaleInfo != null)
            {
                //重置价格为 限时抢购价
                cartItem.AdjustedPrice = proSaleInfo.ProSalesPrice;
            }
            #endregion

            #region 团购价格处理
            if (groupBuyInfo != null)
            {
                //重置价格为 限时抢购价
                cartItem.AdjustedPrice = groupBuyInfo.GroupBuy.Price;
            }
            #endregion

            #region 商家
            if (productInfo.SupplierId > 0)
            {
                BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(productInfo.SupplierId);
                if (supplierInfo != null)
                {
                    cartItem.SupplierId = supplierInfo.SupplierId;
                    cartItem.SupplierName = supplierInfo.Name;
                }
            }
            #endregion

            BLL.Shop.Products.SKUInfo skuManage = new BLL.Shop.Products.SKUInfo();
            //DONE: 根据SkuId 获取SKUItem值和图片数据 BEN 2013-06-30
            List<SKUItem> listSkuItems = skuManage.GetSKUItemsBySkuId(skuInfo.SkuId);
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
            cartItem.ThumbnailsUrl = productInfo.ThumbnailUrl1;

            cartItem.CostPrice = skuInfo.CostPrice.HasValue ? skuInfo.CostPrice.Value : 0;
            cartItem.Weight = skuInfo.Weight.HasValue ? skuInfo.Weight.Value : 0;
            cartItem.Points = (int)(productInfo.Points.HasValue ? productInfo.Points : 0);
            cartInfo.Items.Add(cartItem);
            return cartInfo;
        }
        #endregion

        [JsonRpcMethod("SubmitSuccess", Idempotent = false)]
        [JsonRpcHelp("订单提交成功")]
        public JsonObject SubmitSuccess(string id, int UserID)
        {
            if (UserID < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            if (string.IsNullOrWhiteSpace(id))
            {
                return new Result(ResultStatus.Success, null);
            }
            long orderId = Common.Globals.SafeLong(id, -1);
            if (orderId < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            Model.Shop.Order.OrderInfo orderInfo = _orderManage.GetModel(orderId);
            if (orderInfo == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            //Safe
            if (orderInfo.BuyerID != UserID)
            {
                return new Result(ResultStatus.Success, null);
            }
            List<Model.Shop.Order.OrderInfo> list = new List<Model.Shop.Order.OrderInfo>();
            list.Add(orderInfo);
            JsonObject json;
            JsonArray array = new JsonArray();
            JsonObject result = new JsonObject();
            foreach (OrderInfo item in list)
            {
                json = new JsonObject();
                json.Put("orderid", item.OrderId);
                json.Put("price", item.Amount);
                json.Put("paymentid", item.PaymentTypeId);
                array.Add(json);
            }
            result.Put("orderinfo", array);
            return new Result(ResultStatus.Success, result);
        }


        #region 订单列表
        [JsonRpcMethod("OrderList", Idempotent = false)]
        [JsonRpcHelp("订单列表")]
        public JsonObject OrderList(int page = 1, int pageNum = 10, int UserID = -1)
        {
            if (pageNum == 0)
            {
                pageNum = 10;
            }
            if (page == 0)
            {
                page = 1;
            }
            if (UserID < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            Orders orderBll = new Orders();
            BLL.Shop.Order.OrderItems itemBll = new BLL.Shop.Order.OrderItems();

            //计算分页起始索引
            int startIndex = page > 1 ? (page - 1) * pageNum + 1 : 0;

            //计算分页结束索引
            int endIndex = page * pageNum;
            int toalCount = 0;

            string where = " BuyerID=" + UserID +
#if true //方案二 统一提取主订单, 然后加载子订单信息 在View中根据订单支付状态和是否有子单对应展示
                //主订单
                           " AND OrderType=1";
#else   //方案一 提取数据时 过滤主/子单数据 View中无需对应 [由于不够灵活此方案作废]
                           //主订单 无子订单
                           " AND ((OrderType = 1 AND HasChildren = 0) " +
                           //子订单 已支付 或 货到付款/银行转账 子订单
                           "OR (OrderType = 2 AND (PaymentStatus > 1 OR (PaymentGateway='cod' OR PaymentGateway='bank')) ) " +
                           //主订单 有子订单 未支付的主订单 非 货到付款/银行转账 子订单
                           "OR (OrderType = 1 AND HasChildren = 1 AND PaymentStatus < 2 AND PaymentGateway<>'cod' AND PaymentGateway<>'bank'))";
#endif

            //获取总条数
            toalCount = orderBll.GetRecordCount(where);
            if (toalCount < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonObject result = new JsonObject();
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            result.Put("list_count", toalCount);
            List<OrderInfo> orderList = orderBll.GetListByPageEX(where, "", startIndex, endIndex);
            if (orderList != null && orderList.Count > 0)
            {
                foreach (OrderInfo item in orderList)
                {
                    //有子订单 已支付 或 货到付款/银行转账 子订单 - 加载子单
                    if (item.HasChildren && (item.PaymentStatus > 1 || (item.PaymentGateway == "cod" || item.PaymentGateway == "bank")))
                    {
                        item.SubOrders = orderBll.GetModelList(" ParentOrderId=" + item.OrderId);
                        item.SubOrders.ForEach(
                            info => info.OrderItems = itemBll.GetModelList(" OrderId=" + info.OrderId));
                    }
                    else
                    {
                        item.OrderItems = itemBll.GetModelList(" OrderId=" + item.OrderId);
                    }
                }
            }
            PagedList<OrderInfo> lists = new PagedList<OrderInfo>(orderList, page, pageNum, toalCount);
            foreach (OrderInfo item in lists)
            {
                json = new JsonObject();
                json.Put("orderid", item.OrderCode);
                json.Put("status", Common.Globals.SafeEnum<Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus>(item.OrderStatus.ToString(), Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.UnHandle, true));
                json.Put("time", item.CreatedDate);
                json.Put("price", item.Amount.ToString("F"));
                json.Put("ShipName", item.ShipName);
                jsonArray.Add(json);
            }
            result.Put("orderlist", jsonArray);
            return new Result(ResultStatus.Success, result);
        }
        #endregion

        #region 订单详情
        [JsonRpcMethod("OrderDetail", Idempotent = false)]
        [JsonRpcHelp("订单列表")]
        public JsonObject OrderDetail(long id = -1, int UserID = -1)
        {
            OrderInfo orderModel = _orderManage.GetModelInfo(id);
            //Safe
            if (UserID < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            if (orderModel == null || orderModel.BuyerID != UserID)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonObject orderinfojson = new JsonObject();
            JsonArray orderinfoarray = new JsonArray();
            JsonObject result = new JsonObject();
            orderinfojson.Put("orderid", orderModel.OrderCode);
            orderinfojson.Put("status", Common.Globals.SafeEnum<Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus>(orderModel.OrderStatus.ToString(), Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.UnHandle, true));
            orderinfojson.Put("time", orderModel.CreatedDate);
            orderinfojson.Put("price", orderModel.Amount.ToString("F"));
            orderinfojson.Put("ShipName", orderModel.ShipName);
            orderinfoarray.Add(orderinfojson);
            result.Put("order_info", orderinfoarray);

            JsonObject addressjson = new JsonObject();
            JsonArray addressinfo = new JsonArray();
            addressjson.Put("name", orderModel.ShipName);
            addressjson.Put("id", orderModel.RegionId);
            addressjson.Put("addressArea", orderModel.ShipRegion);
            addressjson.Put("cellphone", orderModel.ShipCellPhone);
            addressjson.Put("address", orderModel.ShipAddress);
            addressinfo.Add(addressjson);
            result.Put("address_info", addressinfo);

            JsonObject payjson = new JsonObject();
            JsonArray payinfo = new JsonArray();
            payjson.Put("type", orderModel.PaymentTypeName);
            payjson.Put("yunfei", orderModel.FreightAdjusted.HasValue ? orderModel.FreightAdjusted.Value.ToString("F") : "0.00");
            payjson.Put("shipmodename", orderModel.RealShippingModeName);//配送方式
            payjson.Put("shipordernumber", string.IsNullOrWhiteSpace(orderModel.ShipOrderNumber) ? "无" : orderModel.ShipOrderNumber);//物流单号
            payinfo.Add(payjson);
            result.Put("payment_info", payinfo);

            JsonArray productinfo = new JsonArray();
            JsonObject projson ;
            JsonObject propertyjson ;
            if (orderModel != null && orderModel.OrderItems.Count > 0)
            {
                foreach (var item in orderModel.OrderItems)
                {
                    projson = new JsonObject();
                    projson.Put("SKU", item.SKU);
                    projson.Put("Name", item.Name);
                    projson.Put("SellPrice", item.SellPrice.ToString("F"));
                    projson.Put("Quantity", item.Quantity);
                    if (!string.IsNullOrWhiteSpace(item.Attribute))
                    {
                        string[] tmpAttr = item.Attribute.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        JsonArray property = new JsonArray();
                        foreach (string val in tmpAttr)
                        {
                            propertyjson = new JsonObject();
                            propertyjson.Put("value", val);
                            property.Add(propertyjson);
                            projson.Put("product_property", property);
                        }
                    }
                    productinfo.Add(projson);
                }
                result.Put("productlist", productinfo);
            }
            return new Result(ResultStatus.Success, result);

        }
        #endregion




        [JsonRpcMethod("ExpressList", Idempotent = false)]
        [JsonRpcHelp("物流信息")]
        public JsonObject ExpressList(long OrderId = -1)
        {
            if (OrderId < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            JsonObject result = new JsonObject();
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            List<Maticsoft.ViewModel.Shop.Express> expressList = Maticsoft.Web.Components.ExpressHelper.GetExpress(OrderId);
            if (expressList == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            foreach (Express item in expressList)
            {
                json = new JsonObject();
                json.Put("Content", item.Content);
                json.Put("date", item.Date);
                jsonArray.Add(json);
            }
            result.Put("logistics", jsonArray);
            return new Result(ResultStatus.Success, result);
        }

        [JsonRpcMethod("CancelOrder", Idempotent = true)]
        [JsonRpcHelp("取消订单")]
        public JsonObject CancelOrder(long Id, string userName, int UserID)
        {
            if (UserID <= 0)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            User currentUser = new Maticsoft.Accounts.Bus.User(
                    new Maticsoft.Accounts.Bus.AccountsPrincipal(userName));
            if (currentUser == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonObject json = new JsonObject();
            Orders orderBll = new Orders();
            long orderId = Globals.SafeLong(Id, 0);
            OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
            if (orderInfo == null || orderInfo.BuyerID != UserID)
            {
                json.Put("result", "Error");
            }

            if (OrderManage.CancelOrder(orderInfo, currentUser))
            {
                json.Put("result", "Success");
            }
            return new Result(ResultStatus.Success, json);
        }

        [JsonRpcMethod("addressSave", Idempotent = false)]
        [JsonRpcHelp("保存地址")]
        public JsonObject addressSave(int id, int UserId, string name, string phonenumber, string areaid, string areadetail, string zipcode, string celphone)
        {
            if (id < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            if (UserId<1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            Model.Shop.Shipping.ShippingAddress model = new Model.Shop.Shipping.ShippingAddress
            {
                ShippingId = Common.Globals.SafeInt(id, -1),
                UserId = Common.Globals.SafeInt(UserId, -1),
                ShipName = name,
                RegionId = Common.Globals.SafeInt(areaid, -1),
                Address = areadetail,
                CelPhone = celphone,
                Zipcode = zipcode
            };

            //DONE: 跳转加addressId参数
            if (model.ShippingId > 0)
            {
                return new Result(ResultStatus.Success, null);
            }
            Maticsoft.ViewModel.Shop.ShippingAddressModel addressModel = new ViewModel.Shop.ShippingAddressModel();
            addressModel.ListAddress = _addressManage.GetModelList(" UserId=" + UserId);
            model.UserId = UserId;

            model.ShippingId = _addressManage.Add(model);
            JsonArray array = new JsonArray();
            JsonObject json = new JsonObject();
            JsonObject result = new JsonObject();
            if (model.ShippingId > 0)
            {
                Maticsoft.Model.Shop.Shipping.ShippingAddress shipmodel = null;
                if (model.ShippingId > 0)
                {
                    shipmodel = _addressManage.GetModel(model.ShippingId);
                }
                else
                {
                    //默认读取当前用户设置为
                    List<Maticsoft.Model.Shop.Shipping.ShippingAddress> listAddress =
                    _addressManage.GetModelList(" UserId=" + UserId);
                    if (listAddress != null && listAddress.Count > 0)
                    {
                        //读取默认收货地址
                        //model = listAddress.Find(info => info.IsDefault);

                        //读取默认第一个收货地址
                        shipmodel = listAddress[0];
                    }
                }
                //用户从未设置
                if (shipmodel == null)
                {
                    return new Result(ResultStatus.Success, null);
                }

                List<ShippingAddress> list = new List<ShippingAddress>();
                list.Add(shipmodel);
                foreach (ShippingAddress item in list)
                {
                    json = new JsonObject();
                    json.Put("id", item.RegionId);
                    json.Put("phonenumber", item.CelPhone);
                    json.Put("areadetail", item.Address);
                    json.Put("zipcode", item.Zipcode);
                    json.Put("provicename", item.RegionFullName);
                    array.Add(json);
                }
                result.Put("addresslist", array);
            }
            return new Result(ResultStatus.Success, result);
        }

        #region
        [JsonRpcMethod("DelShippAddress", Idempotent = false)]
        [JsonRpcHelp("删除地址")]
        public JsonObject DelShippAddress(int id,int UserID)
        {
            if (id < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }

            BLL.Shop.Shipping.ShippingAddress addressManage = new BLL.Shop.Shipping.ShippingAddress();
            Model.Shop.Shipping.ShippingAddress model = addressManage.GetModel(id);
            if (model != null && UserID == model.UserId)
            {
                try
                {
                    addressManage.Delete(id);
                    return new Result(ResultStatus.Success, "Success");
                }
                catch (Exception ex)
                {
                    LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message), ex.StackTrace, Request);
                    return new Result(ResultStatus.Error, ex);
                }
               
            }
            return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
        }
        #endregion


        [JsonRpcMethod("Address", Idempotent = false)]
        [JsonRpcHelp("地址列表")]
        public JsonObject Address()
        {
            List<Maticsoft.Model.Ms.Regions> list = RegionBll.GetProvinceList();
            if (list == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonArray result = new JsonArray();
            JsonObject baseJson;
            foreach (Regions item in list)
            {
                baseJson = new JsonObject();
                baseJson.Put("id", item.RegionId);
                baseJson.Put("name", item.RegionName);
                baseJson.Put("depth", item.Depth);
                baseJson.Put("childlist", GetDataByParentId("ParentId=" + item.RegionId));
                result.Add(baseJson);
            }
            return new Result(ResultStatus.Success,result);
        }
        public JsonArray GetDataByParentId(string strWhere)
        {
            JsonObject currjson;
            JsonArray array = new JsonArray();
            List<Maticsoft.Model.Ms.Regions> list = RegionBll.GetListInfo(strWhere);
            if (list == null)
            {
                return null;
            }
            if (list != null && list.Count > 0)
            {
                foreach (Regions item in list)
                {
                    currjson = new JsonObject();
                    currjson.Put("id", item.RegionId);
                    currjson.Put("name", item.RegionName);
                    currjson.Put("parentid", item.ParentId);
                    currjson.Put("depth", item.Depth);
                    currjson.Put("childlist", GetDataByParentId("ParentId=" + item.RegionId));
                    array.Add(currjson);
                }
            }
            return array;
        }
    }
}