using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Web.Components.Setting.Shop;
using Maticsoft.Model.Shop.Shipping;
using System.Linq;
using Maticsoft.BLL.Members;
using System.Web;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public delegate void _Elapsed(Model.Shop.Order.OrderInfo orderInfo);
    public static class EnlargeTimer
    {
        public static _Elapsed _Elapsed;
    }

    public class OrderController : ShopControllerBaseUser
    {
        private readonly BLL.Shop.Shipping.ShippingType _shippingTypeManage = new BLL.Shop.Shipping.ShippingType();
        private readonly BLL.Shop.Shipping.ShippingAddress _addressManage = new BLL.Shop.Shipping.ShippingAddress();
        private readonly BLL.Shop.Order.Orders _orderManage = new BLL.Shop.Order.Orders();

        private readonly BLL.Shop.Shipping.ShippingRegionGroups _shippingRegionManage = new BLL.Shop.Shipping.ShippingRegionGroups();
        private readonly BLL.Ms.Regions _regionManage = new BLL.Ms.Regions();
        private readonly BLL.Shop.Shipping.ShippingAddress _shippingAddressManage = new BLL.Shop.Shipping.ShippingAddress();
        Maticsoft.BLL.Shop.Shipping.ShippingType _shippingTypeBLL = new BLL.Shop.Shipping.ShippingType();
        Maticsoft.BLL.Members.UsersExp UexpBLL = new UsersExp();

        private readonly BLL.Shop.Shipping.Shop_freefreight _feeBLL = new BLL.Shop.Shipping.Shop_freefreight();
        private readonly BLL.Shop.ActivityManage.AMPBLL _ampBLL = new BLL.Shop.ActivityManage.AMPBLL();

        public ActionResult Index()
        {
            return View();
        }

        #region 提交订单
        /// <summary>
        /// 提交订单,订单相关数据显示页
        /// </summary>
        public ActionResult SubmitOrder(string sku, int count = 1, int c = -1, int g = -1, int a = -1,
            string viewName = "SubmitOrder")
        {
            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = new ShoppingCartInfo();
            BLL.Shop.Products.SKUInfo skuManage = new BLL.Shop.Products.SKUInfo();
            if (a > 0)
            {
                #region 组合优惠套装
                BLL.Shop.Products.ProductAccessorie prodAcceBll = new BLL.Shop.Products.ProductAccessorie();
                ProductAccessorie prodAcceModel = prodAcceBll.GetModel(a);
                if (prodAcceModel == null || prodAcceModel.Type != 2) return new RedirectResult("/");
                List<SKUInfo> skulist = skuManage.GetSKUListByAcceId(a, 0);
                if (skulist == null || skulist.Count < 2) return new RedirectResult("/");//每组商品最少有两条数据
                cartInfo = GetCartInfo4SKU(skulist, prodAcceModel);

                #endregion
            }
            else if (string.IsNullOrWhiteSpace(sku))
            {
                int userId = currentUser == null ? -1 : currentUser.UserID;
                Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new BLL.Shop.Products.ShoppingCartHelper(userId);
                cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart4Selected());
            }
            else
            {
                #region 指定SKU提交订单 此功能已投入使用
                //TODO: 未支持多个SKU BEN ADD 2013-06-23
                BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
                SKUInfo skuInfo = skuManage.GetModelBySKU(sku);
                if (skuInfo == null) return new RedirectResult("/");

                ProductInfo productInfo = productManage.GetModel(skuInfo.ProductId);
                if (productInfo == null) return new RedirectResult("/");

                #region 限时抢购
                ProductInfo proSaleInfo = null;
                if (c > 0)
                {
                    proSaleInfo = productManage.GetProSaleModel(c);
                    if (proSaleInfo == null) return new RedirectResult("/");
                    //活动已过期 重定向到单品页
                    if (DateTime.Now > proSaleInfo.ProSalesEndDate)
                        return RedirectToAction("ProSaleDetail", "Product", new { area = "Shop", id = c });
                }
                #endregion

                #region 团购
                ProductInfo groupBuyInfo = null;
                if (g > 0)
                {
                    groupBuyInfo = productManage.GetGroupBuyModel(g);
                    if (groupBuyInfo == null) return new RedirectResult("/");

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate || DateTime.Now < groupBuyInfo.GroupBuy.StartDate)
                    {
                        if (groupBuyInfo.GroupBuy.PromotionType != 1)
                        {
                            return RedirectToAction("GroupBuyDetail", "Product", new { area = "Shop", id = g });
                        }
                        else
                        {
                            return RedirectToAction("GroupBuyDetailHaoLi", "Product", new { area = "Shop", id = g });
                        }
                    }
                    //抢购数量已超过
                    if (groupBuyInfo.GroupBuy.MaxCount <= groupBuyInfo.GroupBuy.BuyCount)
                    {
                        return RedirectToAction("GroupBuyDetailHaoLi", "Product", new { area = "Shop", id = g });
                    }
                }
                #endregion
                //对数据进行活动计算
                cartInfo = GetCartInfo4SKU(productInfo, skuInfo, count, proSaleInfo, groupBuyInfo);

                #endregion
            }

            #region 优惠券
            Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new CouponInfo();
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> infoModelList = infoBll.GetCouponList(currentUser.UserID, 1);
            ViewBag.infoModelList = infoModelList;
            ViewBag.Group = g;

            #endregion


            if (cartInfo.Quantity < 1)
                return Redirect(ViewBag.BasePath + "ShoppingCart/CartInfo");

            if (c < 1 && g < 1 && a < 1)    //限时抢购/团购/组合优惠套装　不参与批销优惠
            {
                #region 批销优惠
                try
                {
                    BLL.Shop.Sales.SalesRuleProduct salesRule = new BLL.Shop.Sales.SalesRuleProduct();
                    cartInfo = salesRule.GetWholeSale(cartInfo);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                #endregion
            }

            #region 账户余额
            decimal ux = UexpBLL.GetUserBalance(currentUser.UserID);
            ViewBag.Balance = ux.ToString("0.00");
            #endregion


            #region 运费
            //运费统一由 ShowPayAndShip 计算
            ViewBag.Freight = 0;
            #endregion

            ViewBag.TotalQuantity = cartInfo.Quantity;
            ViewBag.TotalAdjustedPrice = cartInfo.TotalAdjustedPrice;
            ViewBag.ProductTotal = cartInfo.TotalSellPrice;
            ViewBag.TotalPrice = cartInfo.TotalAdjustedPrice + ViewBag.Freight;
            //促销 - 批销规则优惠
            ViewBag.TotalPromPrice = cartInfo.TotalSellPrice - cartInfo.TotalAdjustedPrice;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "提交订单 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion



            return View(viewName, cartInfo);
        }

        #region GetCartInfo4SKU
        private ShoppingCartInfo GetCartInfo4SKU(ProductInfo productInfo, SKUInfo skuInfo, int quantity, ProductInfo proSaleInfo = null, ProductInfo groupBuyInfo = null)
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
            cartItem.UserId = currentUser.UserID;

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
                //是抢购 增加该抢购数据 2014-11-16
                cartItem.GroupBuyId = groupBuyInfo.GroupBuy.GroupBuyId;
                cartItem.PromotionType = 1;
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
            cartItem.Points = (int)(productInfo.Points.HasValue ? productInfo.Points.Value : 0);
            cartInfo.Items.Add(cartItem);
            return _ampBLL.GetTotalPriceAfterActivity(cartInfo);
        }
        private ShoppingCartInfo GetCartInfo4SKU(List<SKUInfo> skulist, ProductAccessorie model)
        {
            decimal totalPrice = 0M;//原　　价
            decimal dealsPrices = 0M;//总优惠金额
            foreach (var item in skulist)
            {
                totalPrice += item.SalePrice;
            }
            dealsPrices = totalPrice - model.DiscountAmount;
            decimal dealsPrice = dealsPrices / skulist.Count;//单个商品优惠的金额
            ShoppingCartInfo cartInfo = new ShoppingCartInfo();
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
                cartItem.UserId = currentUser.UserID;

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
                cartItem.Weight = item.Weight.HasValue ? item.Weight.Value : 0;

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

                cartInfo.Items.Add(cartItem);
            }
            return cartInfo;
        }
        #endregion



        public ActionResult SubmitSuccess(string id, string viewName = "SubmitSuccess")
        {
            if (string.IsNullOrWhiteSpace(id))
                return Redirect("/");

            long orderId = Common.Globals.SafeLong(id, -1);
            if (orderId < 1) return Content("ERROR_NOTSAFEORDERID");
            Model.Shop.Order.OrderInfo orderInfo = _orderManage.GetModel(orderId);
            if (orderInfo == null) return Content("ERROR_NOORDERINFO");

            //Safe
            if (orderInfo.BuyerID != currentUser.UserID) return Redirect(ViewBag.BasePath + "UserCenter/Orders");
            string PaymentTimeLimit = System.Configuration.ConfigurationManager.AppSettings["PaymentTimeLimit"].ToString();
            ViewBag.PaymentTimeLimit = PaymentTimeLimit;

            ViewBag.OrderId = orderInfo.OrderId;
            //订单编号
            ViewBag.OrderCode = orderInfo.OrderCode;
            //应付金额
            ViewBag.OrderAmount = orderInfo.Amount;
            System.Timers.Timer PaymentTimeLimitTimer = new System.Timers.Timer(1000);

            #region 加载支付方式
            // 支付方式
            ViewBag.OrderPaymentType = orderInfo.PaymentTypeId;
            ViewBag.PaymentModes = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Web);
            #endregion

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "订单提交成功 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            #region 生成订单二维码链接
            ViewBag.CodeUrl = GetCodeUrl(orderInfo.OrderId);
            #endregion

            return View(viewName);
        }


        #region SubmitFail 暂未使用
        [System.Obsolete]
        public ActionResult SubmitFail(string id, string viewName = "SubmitFail")
        {
            if (string.IsNullOrWhiteSpace(id))
                return Redirect("/");

            if (!string.IsNullOrWhiteSpace(id))
            {
                long orderId = Common.Globals.SafeLong(id, -1);
                if (orderId < 1) return Content("ERROR_NOTSAFEORDERID");
                Model.Shop.Order.OrderInfo orderInfo = _orderManage.GetModel(orderId);
                if (orderInfo == null) return Content("ERROR_NOORDERINFO");

                Web.LogHelp.AddErrorLog("Shop >> SubmitFail >> OrderId[" + orderId + "] Status[" + orderInfo.OrderStatus + "]",
                    "SubmitOrderFail", "Shop >> OrderController >> SubmitFail");
                ViewBag.OrderId = orderInfo.OrderId;
            }

            return View(viewName);
        }
        #endregion
        #endregion

        #region 查看订单明细
        /// <summary>
        /// 查看订单明细
        /// </summary>
        public ActionResult OrderInfo(long OrderId = -1, string viewName = "OrderInfo")
        {
            Maticsoft.Model.Shop.Order.OrderInfo orderModel = _orderManage.GetModelInfo(OrderId);

            //Safe
            if (orderModel == null ||
                orderModel.BuyerID != currentUser.UserID
                ) return Redirect(ViewBag.BasePath + "UserCenter/Orders");

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "查看订单详细信息" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View(viewName, orderModel);
        }
        #endregion

        #region 收货人
        public ActionResult ShowAddress(int addressId = -1, string viewName = "_ShowAddress")
        {
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = null;

            if (addressId > 0)
            {
                model = _addressManage.GetModel(addressId);
                if (model != null && model.UserId != CurrentUser.UserID)
                {
                    LogHelp.AddInvadeLog(
                        string.Format("非法获取收货人数据|当前用户:{0}|获取收货地址:{1}|_Maticsoft.Web.Areas.Shop.Controllers.OrderController.ShowAddress",
                            CurrentUser.UserID, addressId), System.Web.HttpContext.Current.Request);
                    return View(viewName);
                }
            }
            else
            {
                //默认读取当前用户设置为
                List<Maticsoft.Model.Shop.Shipping.ShippingAddress> listAddress =
                    _addressManage.GetModelList(" UserId=" + currentUser.UserID);
                if (listAddress != null && listAddress.Count > 0)
                {
                    //读取默认收货地址
                    //model = listAddress.Find(info => info.IsDefault);

                    //读取默认第一个收货地址
                    model = listAddress[0];
                }
            }

            //用户从未设置
            if (model == null) return View(viewName);

            return View(viewName, model);
        }

        public ActionResult AddressInfo(int addressId = -1, bool isModify = false, string viewName = "_AddressInfo")
        {
            //DONE: 地址同时支持多项选择 BEN ADD 2013-06-21 | DONE 20130830
            Maticsoft.ViewModel.Shop.ShippingAddressModel addressModel = new ViewModel.Shop.ShippingAddressModel();
            addressModel.ListAddress = _addressManage.GetModelList(" UserId=" + currentUser.UserID);

            ViewBag.IsModify = isModify;

            //检索上个页面选中的 收货人 编辑使用
            if (isModify && addressId > 0 && addressModel.ListAddress != null && addressModel.ListAddress.Count > 0)
            {
                addressModel.CurrentAddress = addressModel.ListAddress.Find(info => info.ShippingId == addressId);
            }
            //默认加载当前用户信息作为收货人 或 曾经选择的 收货地址 DB不存在
            if (!isModify && addressId < 0 && addressModel.CurrentAddress == null)
            {
                //TODO: 加载 UserEx 扩展表信息 BEN ADD 2013-06-21
                addressModel.CurrentAddress = new Model.Shop.Shipping.ShippingAddress
                {
                    ShippingId = addressId,
                    ShipName = CurrentUser.TrueName,
                    UserId = CurrentUser.UserID,
                    EmailAddress = CurrentUser.Email,
                    CelPhone = CurrentUser.Phone
                };
            }
            //默认空
            if (addressModel.CurrentAddress == null)
            {
                addressModel.CurrentAddress = new Model.Shop.Shipping.ShippingAddress { ShippingId = addressId };
            }

            return View(viewName, addressModel);
        }

        [HttpPost]
        public ActionResult SubmitAddressInfo(FormCollection form)
        {
            bool isModify = Common.Globals.SafeBool(form["IsModify"], false);
            Model.Shop.Shipping.ShippingAddress model = new Model.Shop.Shipping.ShippingAddress
            {
                ShippingId = Common.Globals.SafeInt(form["CurrentAddress.ShippingId"], -1),
                UserId = Common.Globals.SafeInt(form["CurrentAddress.UserId"], -1),
                ShipName = form["CurrentAddress.ShipName"],
                RegionId = Common.Globals.SafeInt(form["CurrentAddress.RegionId"], -1),
                Address = form["CurrentAddress.Address"],
                CelPhone = form["CurrentAddress.CelPhone"],
                Zipcode = form["CurrentAddress.Zipcode"]
            };

            //DONE: 跳转加addressId参数
            if (model.ShippingId > 0)
            {
                if (isModify)
                {
                    _addressManage.Update(model);
                }
                return RedirectToAction("ShowAddress", new { addressId = model.ShippingId });
            }
            else if (currentUser != null)
            {
                model.UserId = currentUser.UserID;
                model.ShippingId = _addressManage.Add(model);
                if (model.ShippingId > 0)
                {
                    return RedirectToAction("ShowAddress", new { addressId = model.ShippingId });
                }
            }
            return RedirectToAction("AddressInfo");
        }
        #endregion

        #region 支付和配送方式

        #region PayAndShipInfo
        public ActionResult PayAndShipInfo(int payId = -1, int shipId = -1, string viewName = "_PayAndShipInfo")
        {
            Maticsoft.ViewModel.Shop.PayAndShip payAndShip = new ViewModel.Shop.PayAndShip();
            #region 支付
            //TODO: 未使用缓存 BEN ADD 20130620
            payAndShip.ListPaymentMode = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Web);
            if (payId > 0)
            {
                //TODO: 未使用缓存 BEN ADD 20130620
                payAndShip.CurrentPaymentMode = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModeById(payId);
                //根据选择支付获取配送
                payAndShip.ListShippingType = _shippingTypeManage.GetListByPay(payId);
            }
            else if (payAndShip.ListPaymentMode != null && payAndShip.ListPaymentMode.Count > 0)
            {
                //默认当前第一个支付方式
                payAndShip.CurrentPaymentMode = payAndShip.ListPaymentMode[0];
                //获取配送
                payAndShip.ListShippingType = _shippingTypeManage.GetListByPay(
                    payAndShip.CurrentPaymentMode.ModeId);
            }
            else
            {
                payAndShip.CurrentPaymentMode = new Payment.Model.PaymentModeInfo
                {
                    ModeId = -1,
                    Name = "当前网站未设置任何支付方式"
                };
                payAndShip.ListPaymentMode = new List<Payment.Model.PaymentModeInfo>
                {
                    payAndShip.CurrentPaymentMode
                };
            }
            #endregion
            #region 配送
            if (shipId > 0)
            {
                payAndShip.CurrentShippingType = _shippingTypeManage.GetModelByCache(shipId);
            }
            else if (payAndShip.ListShippingType != null && payAndShip.ListShippingType.Count > 0)
            {
                //默认当前第一个配送方式
                payAndShip.CurrentShippingType = payAndShip.ListShippingType[0];
            }
            else
            {
                payAndShip.CurrentShippingType = new Model.Shop.Shipping.ShippingType
                {
                    ModeId = -1,
                    Name = "当前支付方式未设置任何配送",
                    Description = "请选择其它支付方式"
                };
                payAndShip.ListShippingType = new List<Model.Shop.Shipping.ShippingType>
                {
                    payAndShip.CurrentShippingType
                };
            }
            #endregion
            return View(viewName, payAndShip);
        }
        #endregion

        #region ShowPayAndShip
        public ActionResult ShowPayAndShip(int payId = -1, int shipId = -1, int addrId = -1,
            string sku = null, int count = 1, int c = -1, int g = -1, string viewName = "_ShowPayAndShip")
        {
            #region 支付

            Maticsoft.ViewModel.Shop.PayAndShip payAndShip = new ViewModel.Shop.PayAndShip();
            payAndShip.ListPaymentMode =
                Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Web);
            if (payId > 0)
            {
                //TODO: 未使用缓存 BEN ADD 20130620
                payAndShip.CurrentPaymentMode = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModeById(payId);
                //获取配送
                payAndShip.ListShippingType = _shippingTypeManage.GetListByPay(
                    payAndShip.CurrentPaymentMode.ModeId);
            }
            else if (payAndShip.ListPaymentMode != null && payAndShip.ListPaymentMode.Count > 0)
            {
                //默认当前第一个支付方式
                payAndShip.CurrentPaymentMode = payAndShip.ListPaymentMode[0];

                //获取配送
                payAndShip.ListShippingType = _shippingTypeManage.GetListByPay(
                    payAndShip.CurrentPaymentMode.ModeId);
            }
            else
            {
                payAndShip.CurrentPaymentMode = new Payment.Model.PaymentModeInfo
                {
                    ModeId = -1,
                    Name = "未选择支付方式"
                };
            }

            #endregion

            #region 配送

            if (shipId > 0)
            {
                payAndShip.CurrentShippingType = _shippingTypeManage.GetModelByCache(shipId);
            }
            else if (payAndShip.ListShippingType != null && payAndShip.ListShippingType.Count > 0)
            {
                //默认当前第一个配送方式
                payAndShip.CurrentShippingType = payAndShip.ListShippingType[0];
            }
            else
            {
                payAndShip.CurrentShippingType = new Model.Shop.Shipping.ShippingType
                {
                    ModeId = -1,
                    Name = "未选择配送方式"
                };
            }

            #endregion

            #region 运费

            ShoppingCartInfo cartInfo;
            if (string.IsNullOrWhiteSpace(sku))
            {
                //加载购物车
                int userId = currentUser == null ? -1 : currentUser.UserID;
                BLL.Shop.Products.ShoppingCartHelper cartHelper = new BLL.Shop.Products.ShoppingCartHelper(userId);
                cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
            }
            else
            {
                #region 指定SKU提交订单 此功能已投入使用

                //TODO: 未支持多个SKU BEN ADD 2013-06-23
                BLL.Shop.Products.SKUInfo skuManage = new BLL.Shop.Products.SKUInfo();
                BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
                SKUInfo skuInfo = skuManage.GetModelBySKU(sku);
                if (skuInfo == null)
                {
                    //终止计算运费, 运费0 在提交订单环节提示用户
                    ViewBag.Freight = 0;
                    return View(viewName, payAndShip);
                }

                ProductInfo productInfo = productManage.GetModel(skuInfo.ProductId);
                if (productInfo == null)
                {
                    //终止计算运费, 运费0 在提交订单环节提示用户
                    ViewBag.Freight = 0;
                    return View(viewName, payAndShip);
                }

                #region 限时抢购

                ProductInfo proSaleInfo = null;
                if (c > 0)
                {
                    proSaleInfo = productManage.GetProSaleModel(c);
                    if (proSaleInfo == null)
                    {
                        //终止计算运费, 运费0 在提交订单环节提示用户
                        ViewBag.Freight = 0;
                        return View(viewName, payAndShip);
                    }
                }

                #endregion

                #region 团购
                ProductInfo groupBuyInfo = null;
                if (g > 0)
                {
                    groupBuyInfo = productManage.GetGroupBuyModel(g);
                    if (groupBuyInfo == null)
                    {
                        //终止计算运费, 运费0 在提交订单环节提示用户
                        ViewBag.Freight = 6;
                        return View(viewName, payAndShip);
                    }

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                        return RedirectToAction("GroupBuyDetail", "Product", new { area = "MShop", id = g });
                    if (DateTime.Now < groupBuyInfo.GroupBuy.StartDate)
                        return RedirectToAction("GroupBuyDetail", "Product", new { area = "MShop", id = g });
                }
                #endregion

                cartInfo = _ampBLL.GetTotalPriceAfterActivity(GetCartInfo4SKU(productInfo, skuInfo, count, proSaleInfo, groupBuyInfo));

                #endregion
            }

            #region 区域差异运费计算

            Model.Shop.Shipping.ShippingAddress shippingAddress = addrId < 1
                ? null
                : _shippingAddressManage.GetModel(addrId);

            //默认收货地址
            if (shippingAddress == null && currentUser != null)
            {
                List<Maticsoft.Model.Shop.Shipping.ShippingAddress> listAddress =
                    _addressManage.GetModelList(" UserId=" + currentUser.UserID);
                if (listAddress != null && listAddress.Count > 0)
                    shippingAddress = listAddress[0];
            }

            //有收货地址 且 已选择配送 计算差异运费
            if (shippingAddress != null && payAndShip.CurrentShippingType.ModeId > 0)
            {
                Model.Ms.Regions regionInfo = _regionManage.GetModelByCache(shippingAddress.RegionId);
                Model.Shop.Shipping.ShippingType shippingType = payAndShip.CurrentShippingType;

                int topRegionId;
                if (regionInfo.Depth > 1)
                {
                    topRegionId = Common.Globals.SafeInt(regionInfo.Path.Split(new[] { ',' })[1], -1);
                }
                else
                {
                    topRegionId = regionInfo.RegionId;
                }
                Model.Shop.Shipping.ShippingRegionGroups shippingRegion =
                    _shippingRegionManage.GetShippingRegion(shippingType.ModeId, topRegionId);
                /*按商家计算运费*/
                var query = from rec in cartInfo.Items.Where(m => m.Selected == true) group rec by rec.SupplierId into s select new { weight = s.Sum(m => (m.Weight * m.Quantity)), supplier = s.Key, price = s.Sum(m => m.AdjustedPrice) };


                Maticsoft.BLL.shop.freight.Shop_ProductsFreight sff = new BLL.shop.freight.Shop_ProductsFreight();



                string FreightFee = "[";

                foreach (var item in query.ToList())
                {
                    List<Maticsoft.Model.shop.Freight.Shop_ProductsFreight> sfflist = new List<Model.shop.Freight.Shop_ProductsFreight>();

                    cartInfo.TotalWeight = item.weight;

                    cartInfo.Items.Where(m => m.SupplierId == item.supplier).ToList().ForEach
                        (
                            y =>
                            {
                                var z = sff.GetModel(y.ProductId);
                                if (z != null)
                                {
                                    sfflist.Add(z);
                                }
                            }
                        );

                    if (g > 0)
                    {
                        FreightFee += "{\"supplier\":" + item.supplier + ",\"fee\":\"0\"},";
                    }
                    else
                    {
                        if (sfflist.Count == cartInfo.Items.Where(m => m.SupplierId == item.supplier).Count() && g > 0)
                        {
                            FreightFee += "{\"supplier\":" + item.supplier + ",\"fee\":" + sfflist.Max(y => y.Freight).ToString() + "},";
                        }
                        else
                        {
                            FreightFee += "{\"supplier\":" + item.supplier + ",\"fee\":" + _feeBLL.CalFreeShipping(topRegionId, cartInfo.SupplierPriceList.Where(m => m.SupplierId == item.supplier).First().TotalSellPrice, cartInfo, shippingType, shippingRegion).ToString("0.00") + "},";
                        }
                    }
                }
                FreightFee = FreightFee.Substring(0, FreightFee.Length - 1);
                FreightFee += "]";
                ViewBag.FreightFeeList = FreightFee;
            }
            else
            {
                ViewBag.Freight = decimal.Zero;
            }

            #endregion

            #endregion

            return View(viewName, payAndShip);
        }
        [HttpPost]
        public void getFreight(int payId = -1, int shipId = -1, int addrId = -1,
            string sku = null, int count = 1, int c = -1, int g = -1, int supplierID = 1, string conponcode = null)
        {

            Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = null;
            if (!String.IsNullOrWhiteSpace(conponcode))
            {
                string code = conponcode;

                Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new CouponInfo();
                infoModel = infoBll.GetCouponInfo(code);
            }
            #region 支付

            Maticsoft.ViewModel.Shop.PayAndShip payAndShip = new ViewModel.Shop.PayAndShip();
            payAndShip.ListPaymentMode =
                Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Web);
            if (payId > 0)
            {
                //TODO: 未使用缓存 BEN ADD 20130620
                payAndShip.CurrentPaymentMode = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModeById(payId);
            }
            else if (payAndShip.ListPaymentMode != null && payAndShip.ListPaymentMode.Count > 0)
            {
                //默认当前第一个支付方式
                payAndShip.CurrentPaymentMode = payAndShip.ListPaymentMode[0];

                //获取配送
                payAndShip.ListShippingType = _shippingTypeManage.GetListByPay(
                    payAndShip.CurrentPaymentMode.ModeId);
            }
            else
            {
                payAndShip.CurrentPaymentMode = new Payment.Model.PaymentModeInfo
                {
                    ModeId = -1,
                    Name = "未选择支付方式"
                };
            }

            #endregion

            #region 配送

            if (shipId > 0)
            { 
                //默认第一个配送方式
                payAndShip.CurrentShippingType = _shippingTypeManage.GetModelByCache(shipId);
               
            }
            else if (payAndShip.ListShippingType != null && payAndShip.ListShippingType.Count > 0)
            {
                //默认当前第一个配送方式
                payAndShip.CurrentShippingType = payAndShip.ListShippingType[0];
            }
            else
            {
                payAndShip.CurrentShippingType = new Model.Shop.Shipping.ShippingType
                {
                    ModeId = -1,
                    Name = "未选择配送方式"
                };
            }

            #endregion

            #region 运费

            ShoppingCartInfo cartInfo = new ShoppingCartInfo();
            if (string.IsNullOrWhiteSpace(sku))
            {
                //加载购物车
                int userId = currentUser == null ? -1 : currentUser.UserID;
                BLL.Shop.Products.ShoppingCartHelper cartHelper = new BLL.Shop.Products.ShoppingCartHelper(userId);
                cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
            }
            #region 区域差异运费计算

            Model.Shop.Shipping.ShippingAddress shippingAddress = addrId < 1
                ? null
                : _shippingAddressManage.GetModel(addrId);

            //默认收货地址
            if (shippingAddress == null && currentUser != null)
            {
                List<Maticsoft.Model.Shop.Shipping.ShippingAddress> listAddress =
                    _addressManage.GetModelList(" UserId=" + currentUser.UserID);
                if (listAddress != null && listAddress.Count > 0)
                    shippingAddress = listAddress[0];
            }

            //有收货地址 且 已选择配送 计算差异运费
            decimal freight = decimal.Zero;
            if (shippingAddress != null && payAndShip.CurrentShippingType.ModeId > 0)
            {
                Model.Ms.Regions regionInfo = _regionManage.GetModelByCache(shippingAddress.RegionId);
                Model.Shop.Shipping.ShippingType shippingType = payAndShip.CurrentShippingType;

                int topRegionId;
                if (regionInfo.Depth > 1)
                {
                    topRegionId = Common.Globals.SafeInt(regionInfo.Path.Split(new[] { ',' })[1], -1);
                }
                else
                {
                    topRegionId = regionInfo.RegionId;
                }
                Model.Shop.Shipping.ShippingRegionGroups shippingRegion =
                    _shippingRegionManage.GetShippingRegion(shippingType.ModeId, topRegionId);
                var query = from rec in cartInfo.Items.Where(m => m.Selected == true) group rec by rec.SupplierId into s select new { weight = s.Sum(m => (m.Weight * m.Quantity)), supplier = s.Key, price = s.Sum(m => m.AdjustedPrice) };
                Maticsoft.BLL.shop.freight.Shop_ProductsFreight sff = new BLL.shop.freight.Shop_ProductsFreight();
                string FreightFee = "{data:[";

                foreach (var item in query.ToList())
                {
                    List<Maticsoft.Model.shop.Freight.Shop_ProductsFreight> sfflist = new List<Model.shop.Freight.Shop_ProductsFreight>();

                    cartInfo.TotalWeight = item.weight;

                    cartInfo.Items.Where(m => m.SupplierId == item.supplier).ToList().ForEach
                        (
                            y =>
                            {
                                var z = sff.GetModel(y.ProductId);
                                if (z != null)
                                {
                                    sfflist.Add(z);
                                }
                            }
                        );

                    if (sfflist.Count == cartInfo.Items.Where(m => m.SupplierId == item.supplier).Count() && g > 0)
                    {
                        FreightFee += "{\"supplier\":" + item.supplier + ",\"fee\":" + sfflist.Max(y => y.Freight).ToString() + "},";
                    }
                    else
                    {
                        FreightFee += "{\"supplier\":" + item.supplier + ",\"fee\":" + _feeBLL.CalFreeShipping(topRegionId, cartInfo.SupplierPriceList.Where(m => m.SupplierId == item.supplier).First().TotalSellPrice, cartInfo, shippingType, shippingRegion).ToString("0.00") + "},";
                    }
                }
                FreightFee = FreightFee.Substring(0, FreightFee.Length - 1);

                FreightFee += "}";
                Response.Write(FreightFee);
            }
            #endregion

            #endregion
        }

        #endregion
        #endregion

        #region 优惠券
        public ActionResult AjaxGetCoupon(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ConponCode"]))
            {
                string code = Fm["ConponCode"];
                decimal totalPrice = Common.Globals.SafeDecimal(Fm["TotalPrice"], 0);


                Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new CouponInfo();
                Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = infoBll.GetCouponInfo(code);


                if (code.Trim() == "0")
                {
                    return Content("0" + "|" + totalPrice.ToString("F"));
                }

                if (infoModel == null)
                {
                    return Content("No");
                }
                if (infoModel.Status == 2)
                {
                    return Content("Used");
                }
                if (infoModel.LimitPrice >= totalPrice)
                {
                    return Content("Limit");
                }
                string priceStr = string.Empty;

                if (totalPrice - infoModel.CouponPrice < 7)
                {
                    priceStr = "6";
                }
                else if (totalPrice - infoModel.CouponPrice > 0)
                {
                    priceStr = (totalPrice - infoModel.CouponPrice).ToString("F");
                }
                else
                {
                    //先紧急修复运费的不显示问题
                    priceStr = "6";
                }

                return Content(infoModel.CouponPrice.ToString("F") + "|" + priceStr);
            }
            return Content("False");
        }
        #endregion

        #region 微信订单扫码支付

        public string GetCodeUrl(long orderId)
        {
            const string ORDER_PREFIX = "HaolinShopMallWX_"; // 订单前缀
            string strAppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, "AA");
            string strPartnerId = string.Empty;
            string strKey = string.Empty;

            string strDescription = string.Empty;
            string strTradeNo = string.Empty;
            string strTotalFee = string.Empty;
            string strCreateIp = string.Empty;
            string strNotifyUrl = string.Empty;

            // 支付方式必须为微信支付
            //Maticsoft.Payment.Model.PaymentModeInfo paymentMode = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModeById(4);
            Maticsoft.Payment.Model.PaymentModeInfo paymentMode = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModeByName("wechat");

            if (null == paymentMode) return string.Empty;
            strPartnerId = paymentMode.MerchantCode;
            strKey = paymentMode.SecretKey;

            //获取支付网关，在配置文件中Gateway.config获取支付网关信息
            strNotifyUrl = FullPath(string.Format(new Maticsoft.Web.Handlers.Shop.Pay.PaymentOption().NotifyUrl, EncodeData4Url("wechat")));
            Maticsoft.Model.Shop.Order.OrderInfo tempOrderInfo = _orderManage.GetModel(orderId);
            if (null == tempOrderInfo) return string.Empty;
            //tempOrderInfo.Amount = 0.01M; // 测试时改成1分钱,测试完后需删除
            strDescription = "健康商城订单 - 订单号: [" + tempOrderInfo.OrderCode + "] - " + "在线支付 - 订单支付金额" + ": " + tempOrderInfo.Amount.ToString("0.00") + " 元";
            strTradeNo = ORDER_PREFIX + tempOrderInfo.OrderId;
            strTotalFee = (tempOrderInfo.Amount * 100).ToString("F0");
            //本地测试需要把getrealip改为127.0.0.1
            // strCreateIp = "127.0.0.1";
            strCreateIp = getRealIp();

            // 调用统一支付接口生成微信预支付订单
            Maticsoft.Payment.PaymentInterface.WeChat.Utils.UnifiedWxPayHelper unifiedWxPayHelper = Payment.PaymentInterface.WeChat.Utils.UnifiedWxPayHelper.CreateUnifiedHelper(strAppId, strPartnerId, strKey);
            Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage.UnifiedPrePayMessage rtnPrepayModel = Maticsoft.Payment.PaymentInterface.WeChat.Utils.UnifiedWxPayHelper.UnifiedPrePay(unifiedWxPayHelper.CreateNativePrePayPackage(strDescription, strTradeNo, strTotalFee, strCreateIp, strNotifyUrl));
            if (null != rtnPrepayModel && rtnPrepayModel.ReturnSuccess && rtnPrepayModel.ResultSuccess)
            {
                return rtnPrepayModel.Code_Url;
            }
            return string.Empty;
        }

        private string FullPath(string local)
        {
            if (string.IsNullOrEmpty(local))
            {
                return local;
            }
            if (local.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("http://"))
            {
                return local;
            }
            if (System.Web.HttpContext.Current == null)
            {
                return local;
            }
            return FullPath(HostPath(System.Web.HttpContext.Current.Request.Url), local);
        }

        private string FullPath(string hostPath, string local)
        {
            if (!hostPath.EndsWith("/") && !local.StartsWith("/"))
            {
                return (hostPath + "/" + local);
            }
            return (hostPath + local);
        }

        private string HostPath(Uri uri)
        {
            if (uri == null)
            {
                return string.Empty;
            }
            string str = (uri.Port == 80) ? string.Empty : (":" + uri.Port.ToString(System.Globalization.CultureInfo.InvariantCulture));
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[] { uri.Scheme, uri.Host, str });
        }

        private string getRealIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        private static string EncodeData4Url(string normalString)
        {
            return
                HttpUtility.UrlEncode(
                    Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(normalString))
                        .Replace('+', '.')
                        .Replace('/', '-')
                        .Replace('=', '_'));
        }

        #endregion

        [HttpPost]
        public ActionResult Immediatepayment(int OrderId, int PaymentTypeId)
        {
            if (OrderId > 0)
            {
                Maticsoft.BLL.Shop.Order.Orders orderBll = new BLL.Shop.Order.Orders();
                Maticsoft.Model.Shop.Order.OrderInfo ordermodel = new Model.Shop.Order.OrderInfo();
                ordermodel.OrderId = OrderId;
                ordermodel.PaymentTypeId = PaymentTypeId;

                Maticsoft.Payment.Model.PaymentModeInfo paymodel = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModeById(ordermodel.PaymentTypeId);
                if (paymodel != null)
                {
                    ordermodel.PaymentTypeName = paymodel.Name;
                    ordermodel.PaymentGateway = paymodel.Gateway;
                }

                if (orderBll.UpdateGetaway(ordermodel))
                {
                    return Content("True");
                }
                else
                {
                    return Content("False");
                }
            }
            return Content("False");
        }
    }
}
