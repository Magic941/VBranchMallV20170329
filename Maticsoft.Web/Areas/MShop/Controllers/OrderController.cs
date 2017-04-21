using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.BLL.SysManage;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Web.Components.Setting.Shop;
using Maticsoft.Model.Shop.Shipping;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class OrderController : MShopControllerBaseUser
    {
        private readonly BLL.Shop.Shipping.ShippingType _shippingTypeManage = new BLL.Shop.Shipping.ShippingType();
        private readonly BLL.Shop.Shipping.ShippingAddress _addressManage = new BLL.Shop.Shipping.ShippingAddress();
        private readonly BLL.Shop.Order.Orders _orderManage = new BLL.Shop.Order.Orders();

        private readonly BLL.Shop.Shipping.ShippingRegionGroups _shippingRegionManage = new BLL.Shop.Shipping.ShippingRegionGroups();
        private readonly BLL.Ms.Regions _regionManage = new BLL.Ms.Regions();
        private readonly BLL.Shop.Shipping.ShippingAddress _shippingAddressManage = new BLL.Shop.Shipping.ShippingAddress();
        private readonly BLL.Shop.Shipping.Shop_freefreight _feeBLL = new BLL.Shop.Shipping.Shop_freefreight();
        private readonly BLL.Shop.ActivityManage.AMPBLL _ampBLL = new BLL.Shop.ActivityManage.AMPBLL();
        private Maticsoft.BLL.Shop.Coupon.CouponInfo couponBll = new CouponInfo();

        public ActionResult Index()
        {
            return View();
        }
        #region 微信订单提交
        /// <summary>
        /// 提交订单
        /// </summary>
        public ActionResult SubmitOrder(string sku, int count = 1, int c = 0, int g = 0, int ActiveID = -1, int ActiveType = -1,
            string viewName = "SubmitOrderNew")
        {
           
            //手机版由于页面反复跳转, 采用Session保存SKU数据
            if (!string.IsNullOrWhiteSpace(sku))
            {
                if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
                {
                    g = ActiveID;
                }
                Session["SubmitOrder_SKU"] = sku;
                Session["SubmitOrder_COUNT"] = count;
                Session["SubmitOrder_CountDown"] = c;
                Session["SubmitOrder_GroupBuy"] = g;
                Session["ActiveID"] = ActiveID;
                Session["ActiveType"] = ActiveType;
            }
            else if (!string.IsNullOrWhiteSpace(Session["SubmitOrder_SKU"] as string))
            {
                sku = Session["SubmitOrder_SKU"] as string;
                if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
                {
                    g = ActiveID;
                }
                count = Common.Globals.SafeInt(Session["SubmitOrder_COUNT"], 1);
                c = Common.Globals.SafeInt(Session["SubmitOrder_CountDown"], 0);
                g = Common.Globals.SafeInt(Session["SubmitOrder_GroupBuy"], 0);
                ActiveID = Common.Globals.SafeInt(Session["ActiveID"], -1);
                ActiveType = Common.Globals.SafeInt(Session["ActiveType"], -1);
            }
            ViewBag.SkuInfo = sku;
            ViewBag.SkuCount = count;
            ViewBag.ProSale = c;
            ViewBag.GroupBuy = g;
            ViewBag.ActiveID = ActiveID;
            ViewBag.ActiveType = ActiveType;

            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = new ShoppingCartInfo();

            if (string.IsNullOrWhiteSpace(sku))
            {
                int userId = currentUser == null ? -1 : currentUser.UserID;
                Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new BLL.Shop.Products.ShoppingCartHelper(userId);
                cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
            }
            else
            {
                #region 指定SKU提交订单 此功能已投入使用
                //TODO: 未支持多个SKU BEN ADD 2013-06-23
                Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new Maticsoft.BLL.Shop.Products.SKUInfo();
                Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
                Maticsoft.Model.Shop.Products.SKUInfo skuInfo = skuBll.GetModelBySKU(sku);
                if (skuInfo == null)
                {
                    return new RedirectResult("/");
                }

                Maticsoft.Model.Shop.Products.ProductInfo productInfo = productBll.GetModel(skuInfo.ProductId);
                if (productInfo == null)
                {
                    return new RedirectResult("/");
                }

                #region 限时抢购
                ProductInfo proSaleInfo = null;
                if (c > 0)
                {
                    proSaleInfo = productBll.GetProSaleModel(c);
                    if (proSaleInfo == null) return new RedirectResult("/");
                    //活动已过期 重定向到单品页
                    if (DateTime.Now > proSaleInfo.ProSalesEndDate)
                        return RedirectToAction("ProSaleDetail", "Product", new { area = "MShop", id = c });
                
                }
                #endregion

                #region 团购
                ProductInfo groupBuyInfo = null;
                if (g > 0)
                {
                    groupBuyInfo = productBll.GetGroupBuyModel(g);
                    if (groupBuyInfo == null) return new RedirectResult("/");

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                        return RedirectToAction("SalesGroupBuy", "ProSales", new { ProductId = 0, ActiveID = ActiveID, ActiveType = ActiveType, ViewName = "SalesGroupBuy" });
                    //活动已过期 重定向到单品页
                    if (DateTime.Now < groupBuyInfo.GroupBuy.StartDate)
                        return RedirectToAction("SalesGroupBuy", "ProSales", new { ProductId = 0, ActiveID = ActiveID, ActiveType = ActiveType, ViewName = "SalesGroupBuy" });

                }
                #endregion

                #region 微信商品列表
                Model.Shop.PromoteSales.WeiXinGroupBuy WeiXinGroupBuyInfo = null;
                if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.WeiXin)
                {
                    BLL.Shop.PromoteSales.WeiXinGroupBuy bll = new BLL.Shop.PromoteSales.WeiXinGroupBuy();
                    WeiXinGroupBuyInfo = bll.GetModel(ActiveID);
                    if (WeiXinGroupBuyInfo == null)
                    {
                        // return new RedirectResult("/");
                    }
                }

                #endregion


                cartInfo = GetCartInfo4SKU(productInfo, skuInfo, count, proSaleInfo, groupBuyInfo, WeiXinGroupBuyInfo);

                #endregion
            }

            if (cartInfo.Items.Count < 1)
                return Redirect(ViewBag.BasePath + "ShoppingCart/CartInfo");

            #region 批销优惠

            if (c < 1 && g < 1)    //限时抢购/团购　不参与批销优惠
            {
                try
                {
                    BLL.Shop.Sales.SalesRuleProduct salesRule = new BLL.Shop.Sales.SalesRuleProduct();
                    cartInfo = salesRule.GetWholeSale(cartInfo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #endregion

            ViewBag.infoModelList = null;
            #region 优惠券
            if (ActiveType != (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
            {
                Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new CouponInfo();
                List<Maticsoft.Model.Shop.Coupon.CouponInfo> infoModelList = infoBll.GetCouponList(currentUser.UserID, 1);
                ViewBag.infoModelList = infoModelList;
            }

            #endregion


            #region 运费
            //运费由 ShowPayAndShip 输出
            ViewBag.Freight = 0;
            #endregion

            ViewBag.TotalQuantity = cartInfo.Quantity;
            ViewBag.TotalAdjustedPrice = cartInfo.TotalAdjustedPriceNew;
            ViewBag.ProductTotal = cartInfo.TotalSellPriceNew;
            ViewBag.TotalPrice = cartInfo.TotalAdjustedPriceNew + ViewBag.Freight;
            //促销 - 批销规则优惠
            ViewBag.TotalPromPrice = cartInfo.TotalSellPriceNew - cartInfo.TotalAdjustedPriceNew;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "提交订单 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            #region 获取支付方式
            List<Maticsoft.Payment.Model.PaymentModeInfo> PaymentModeInfolist = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Wap);
            ViewBag.PaymentModeInfolist = PaymentModeInfolist;
            #endregion
            #region 获取第一个配送方式
            ViewBag.ShippingType = 0;
            List<Maticsoft.Model.Shop.Shipping.ShippingType> ShippingTypeList = _shippingTypeManage.GetModelList("");
            if (ShippingTypeList != null)
            {
                if (ShippingTypeList.Count > 0)
                {
                    ViewBag.ShippingType = ShippingTypeList[0].ModeId;
                }
            }

            #endregion
            return View(viewName, cartInfo);
        }
        #endregion

        #region 提交订单
        /// <summary>
        /// 提交订单
        /// </summary>
        public ActionResult SubmitOrderOld(string sku, int count = 1, int c = 0, int g = 0,
            string viewName = "SubmitOrder")
        {
            //手机版由于页面反复跳转, 采用Session保存SKU数据
            if (!string.IsNullOrWhiteSpace(sku))
            {
                Session["SubmitOrder_SKU"] = sku;
                Session["SubmitOrder_COUNT"] = count;
                Session["SubmitOrder_CountDown"] = c;
                Session["SubmitOrder_GroupBuy"] = g;
            }
            else if (!string.IsNullOrWhiteSpace(Session["SubmitOrder_SKU"] as string))
            {
                sku = Session["SubmitOrder_SKU"] as string;
                count = Common.Globals.SafeInt(Session["SubmitOrder_COUNT"], 1);
                c = Common.Globals.SafeInt(Session["SubmitOrder_CountDown"], 0);
                g = Common.Globals.SafeInt(Session["SubmitOrder_GroupBuy"], 0);
            }
            ViewBag.SkuInfo = sku;
            ViewBag.SkuCount = count;
            ViewBag.ProSale = c;
            ViewBag.GroupBuy = g;

            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = new ShoppingCartInfo();

            if (string.IsNullOrWhiteSpace(sku))
            {
                int userId = currentUser == null ? -1 : currentUser.UserID;
                Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new BLL.Shop.Products.ShoppingCartHelper(userId);
                cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
            }
            else
            {
                #region 指定SKU提交订单 此功能已投入使用
                //TODO: 未支持多个SKU BEN ADD 2013-06-23
                Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new Maticsoft.BLL.Shop.Products.SKUInfo();
                Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
                Maticsoft.Model.Shop.Products.SKUInfo skuInfo = skuBll.GetModelBySKU(sku);
                if (skuInfo == null)
                {
                    return new RedirectResult("/");
                }

                Maticsoft.Model.Shop.Products.ProductInfo productInfo = productBll.GetModel(skuInfo.ProductId);
                if (productInfo == null)
                {
                    return new RedirectResult("/");
                }

                #region 限时抢购
                ProductInfo proSaleInfo = null;
                if (c > 0)
                {
                    proSaleInfo = productBll.GetProSaleModel(c);
                    if (proSaleInfo == null) return new RedirectResult("/");
                    //活动已过期 重定向到单品页
                    if (DateTime.Now > proSaleInfo.ProSalesEndDate)
                        return RedirectToAction("ProSaleDetail", "Product", new { area = "MShop", id = c });
                  
                }
                #endregion

                #region 团购
                ProductInfo groupBuyInfo = null;
                if (g > 0)
                {
                    groupBuyInfo = productBll.GetGroupBuyModel(g);
                    if (groupBuyInfo == null) return new RedirectResult("/");

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                        return RedirectToAction("GroupBuyDetail", "Product", new { area = "MShop", id = g });
                    if (DateTime.Now < groupBuyInfo.GroupBuy.StartDate)
                        return RedirectToAction("GroupBuyDetail", "Product", new { area = "MShop", id = g });
                }
                #endregion

                cartInfo = GetCartInfo4SKU(productInfo, skuInfo, count, proSaleInfo, groupBuyInfo);

                #endregion
            }
            #region 优惠券
            Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new CouponInfo();
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> infoModelList = infoBll.GetCouponList(currentUser.UserID, 1);
            ViewBag.infoModelList = infoModelList;

            #endregion
            if (cartInfo.Items.Count < 1)
                return Redirect(ViewBag.BasePath + "ShoppingCart/CartInfo");

            #region 批销优惠

            if (c < 1 && g < 1)    //限时抢购/团购　不参与批销优惠
            {
                try
                {
                    BLL.Shop.Sales.SalesRuleProduct salesRule = new BLL.Shop.Sales.SalesRuleProduct();
                    cartInfo = salesRule.GetWholeSale(cartInfo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #endregion

            #region 运费
            //运费由 ShowPayAndShip 输出
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

        #region GetCartInfo4SKU 微信列表

        private ShoppingCartInfo GetCartInfo4SKU(ProductInfo productInfo, SKUInfo skuInfo, int quantity, ProductInfo proSaleInfo = null, ProductInfo groupBuyInfo = null, Model.Shop.PromoteSales.WeiXinGroupBuy WeiXinGroupBuyInfo = null)
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
            }
            #endregion

            #region 微信价格处理
            if (WeiXinGroupBuyInfo != null)
            {
                //重置价格为 微信价格处理
                if (WeiXinGroupBuyInfo.Price > 0)
                {
                    cartItem.AdjustedPrice = WeiXinGroupBuyInfo.Price;
                }
                cartItem.SaleDes = "微信购买";
                cartItem.ActiveID = WeiXinGroupBuyInfo.GroupBuyId;
                cartItem.ActiveType = (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.WeiXin;
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


        private void RemoveSubmitSession()
        {
            #region Remove MShop SubmitOrderSession SKU/COUNT/CountDown/GroupBuy
            Session.Remove("SubmitOrder_SKU");
            Session.Remove("SubmitOrder_COUNT");
            Session.Remove("SubmitOrder_CountDown");
            Session.Remove("SubmitOrder_GroupBuy");
            #endregion
        }

        public ActionResult RemoveSubmitOrderSession()
        {
            RemoveSubmitSession();
            return Content(string.Empty);
        }

        #region SubmitSuccess
        public ActionResult SubmitSuccess(string id, string viewName = "SubmitSuccess")
        {
            if (string.IsNullOrWhiteSpace(id))
                return Redirect("/");

            long orderId = Common.Globals.SafeLong(id, -1);
            if (orderId < 1) return Content("ERROR_NOTSAFEORDERID");
            Model.Shop.Order.OrderInfo orderInfo = _orderManage.GetModel(orderId);
            if (orderInfo == null) return Content("ERROR_NOORDERINFO");
            //Remove MShop SubmitOrderSession SKU/COUNT/CountDown/GroupBuy
            RemoveSubmitSession();
            //Safe
            if (orderInfo.BuyerID != currentUser.UserID) return Redirect(ViewBag.BasePath + "UserCenter/Orders");
            ViewBag.OrderId = orderInfo.OrderId;
            //订单编号
            ViewBag.OrderCode = orderInfo.OrderCode;
            //应付金额
            ViewBag.OrderAmount = orderInfo.Amount;
            ////收货人
            //ViewBag.ShipName = orderInfo.ShipName;
            ////项目数量
            //ViewBag.ItemsCount = _orderItemManage.GetOrderItemCountByOrderId(orderId);
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "订单提交成功 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            #region 获取支付方式
            List<Maticsoft.Payment.Model.PaymentModeInfo> PaymentModeInfolist = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Wap);
            ViewBag.PaymentModeInfolist = PaymentModeInfolist;
            #endregion

            return View(viewName);
        }

        //立即支付(liyongqin)
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



        #endregion

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
            //补全区域地址
            int regionId = orderModel.RegionId.HasValue ? orderModel.RegionId.Value : -1;
            orderModel.ShipAddress = _regionManage.GetRegionNameByRID(regionId) + "　" + orderModel.ShipAddress;
            return View(viewName, orderModel);
        }
        #endregion

        #region 收货人
        public ActionResult ShowAddress(string viewName = "_ShowAddress")
        {

            List<Maticsoft.Model.Shop.Shipping.ShippingAddress> listAddress =
                _addressManage.GetModelList(" UserId=" + currentUser.UserID);

            //用户从未设置
            if (listAddress == null || listAddress.Count < 1) return View(viewName);

            //补全区域地址
            listAddress[0].Address = _regionManage.GetRegionNameByRID(listAddress[0].RegionId) + "　" + listAddress[0].Address;
            return View(viewName, listAddress);
        }

        public ActionResult AddressInfo(int id = -1, string viewName = "AddressInfo")
        {
            Maticsoft.Model.Shop.Shipping.ShippingAddress addressModel;
            #region TODO 地址同时支持多项选择
            //TODO: 地址同时支持多项选择 BEN ADD 2013-06-21
            //List<Maticsoft.Model.Shop.Shipping.ShippingAddress> listAddress =
            //    addressBll.GetModelList(" UserId=" + currentUser.UserID);
            #endregion

            if (id > 0)
            {
                addressModel = _addressManage.GetModel(id);
                if (addressModel != null && addressModel.UserId != CurrentUser.UserID)
                {
                    LogHelp.AddInvadeLog(
                        string.Format(
                            "非法获取收货人数据|当前用户:{0}|获取收货地址:{1}|_Maticsoft.Web.Areas.MShop.Controllers.OrderController.AddressInfo",
                            CurrentUser.UserID, id), System.Web.HttpContext.Current.Request);
                    return View(viewName, new Maticsoft.Model.Shop.Shipping.ShippingAddress());
                }
            }
            else
            {
                //默认加载当前用户信息作为收货人
                //TODO: 加载 UserEx 扩展表信息 BEN ADD 2013-06-21
                addressModel = new Model.Shop.Shipping.ShippingAddress
                {
                    ShipName = CurrentUser.TrueName,
                    UserId = CurrentUser.UserID,
                    EmailAddress = CurrentUser.Email,
                    CelPhone = CurrentUser.Phone
                };
            }
            return View(viewName, addressModel);
        }
        [HttpPost]
        public ActionResult SubmitAddressInfo(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            if (model.ShippingId > 0)
            {
                if (_addressManage.Update(model))
                {
                    return Content("True");
                }
            }
            else if (currentUser != null)
            {
                model.UserId = currentUser.UserID;
                if (_addressManage.Add(model) > 0)
                {
                    return Content("True");
                }
            }
            return Content("False");
        }
        #endregion

        #region 支付和配送方式



        #region PayAndShipInfo
        public ActionResult PayAndShipInfo(string viewName = "PayAndShipInfo")
        {
            Maticsoft.ViewModel.Shop.PayAndShip payAndShip = new ViewModel.Shop.PayAndShip();
            #region 支付
            //TODO: 未使用缓存 BEN ADD 20130620
            payAndShip.ListPaymentMode = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Wap);
            int payId = Common.Globals.SafeInt(Request.Params["payId"], 0);
            int shipId = Common.Globals.SafeInt(Request.QueryString["shipId"], 0);
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
                payAndShip.ListShippingType = _shippingTypeManage.GetListByPay(payAndShip.CurrentPaymentMode.ModeId);
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

            ViewBag.PayId = payId;
            ViewBag.ShipId = shipId;
            return View(viewName, payAndShip);
        }
        #endregion

        #region ShowPayAndShip
        public ActionResult ShowPayAndShip(string viewName = "_ShowPayAndShip")
        {
            Maticsoft.ViewModel.Shop.PayAndShip payAndShip = new ViewModel.Shop.PayAndShip();

            int payId = Common.Globals.SafeInt(Request.Params["payId"], 0);
            int shipId = Common.Globals.SafeInt(Request.QueryString["shipId"], 0);
            int addrId = Common.Globals.SafeInt(Request.QueryString["addrId"], 0);

            #region 支付
            payAndShip.ListPaymentMode = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.Wap);
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
            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = new ShoppingCartInfo();
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new BLL.Shop.Products.ShoppingCartHelper(userId);

            string sku = Session["SubmitOrder_SKU"] as string;
            if (string.IsNullOrWhiteSpace(sku))
            {
                cartInfo = cartHelper.GetShoppingCart();
            }
            else
            {
                #region 指定SKU提交订单 此功能已投入使用
                int count = Common.Globals.SafeInt(Session["SubmitOrder_COUNT"], 1);
                int c = Common.Globals.SafeInt(Session["SubmitOrder_CountDown"], -1);
                int g = Common.Globals.SafeInt(Session["SubmitOrder_GroupBuy"], -1);

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
                        ViewBag.Freight = 0;
                        return View(viewName, payAndShip);
                    }

                    //活动已过期 重定向到单品页
                    if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                        return RedirectToAction("GroupBuyDetail", "Product", new { area = "MShop", id = g });
                    if (DateTime.Now < groupBuyInfo.GroupBuy.StartDate)
                        return RedirectToAction("GroupBuyDetail", "Product", new { area = "MShop", id = g });
                }
                #endregion

                cartInfo = GetCartInfo4SKU(productInfo, skuInfo, count, proSaleInfo, groupBuyInfo);
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
                ViewBag.Freight = _feeBLL.CalFreeShipping(topRegionId, cartInfo.TotalSellPrice, cartInfo, shippingType, shippingRegion);
            }
            else
            {
                ViewBag.Freight = decimal.Zero;
            }

            #endregion
            #endregion

            return View(viewName, payAndShip);
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
                decimal defaultprice = 0.0m;
                Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new CouponInfo();
                Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = infoBll.GetCouponInfo(code);
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
                if (totalPrice - infoModel.CouponPrice <= 0)
                {
                    priceStr = (totalPrice - defaultprice).ToString("F");

                }
                else if (totalPrice - infoModel.CouponPrice > 0)
                {
                    priceStr = infoModel.CouponPrice.ToString("F");
                }
                else
                {
                    //先紧急修复运费的不显示问题
                    priceStr = (totalPrice - defaultprice).ToString("F");
                }

                return Content(infoModel.CouponPrice.ToString("F") + "|" + priceStr);
            }
            return Content("False");
        }
        #endregion

        #region 优惠券
        public ActionResult AjaxCouponCompareCartInfo(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ConponCode"]))
            {
                string code = Fm["ConponCode"];
                decimal totalPrice = Common.Globals.SafeDecimal(Fm["TotalPrice"], 0);
                decimal defaultprice = 0.01m;
                Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new CouponInfo();
                Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = infoBll.GetCouponInfo(code);
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
                if (totalPrice - infoModel.CouponPrice <= 0)
                {
                    priceStr = (totalPrice - defaultprice).ToString("F");

                }
                else if (totalPrice - infoModel.CouponPrice > 0)
                {
                    priceStr = infoModel.CouponPrice.ToString("F");
                }
                else
                {
                    //先紧急修复运费的不显示问题
                    priceStr = (totalPrice - defaultprice).ToString("F");
                }

                return Content(infoModel.CouponPrice.ToString("F") + "|" + priceStr);
            }
            return Content("False");
        }


        #endregion

        #region 获取起送价格
        public ActionResult GetSentPrices()
        {
            return Content(ConfigSystem.GetDecimalValueByCache("Opertors_SentPrices").ToString());
        }
        #endregion

        #region 通过商品总价和地区 计算运费
        /// <summary>
        /// 通过配送方式和地区 计算运费
        /// </summary>
        /// <param name="ModeId"></param>
        /// <param name="addrId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult getFreightNew(int ModeId, int addrId, string couponCode = "")
        {


            var res = new JsonResult();
            decimal freight = decimal.Zero;
            string FreightFee = "";
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。  
            var Status = "OK";
            var Message = "";
            var josn = new object();

            #region 配送
            Maticsoft.Model.Shop.Shipping.ShippingType shippingType = new ShippingType();
            if (ModeId > 0)
            {
                shippingType = _shippingTypeManage.GetModelByCache(ModeId);
            }
            if (shippingType == null)
            {
                Status = "NO";
                Message = "请选择配送方式";
                josn = new { StatusStr = Status, MessageStr = Message };
                res.Data = josn;
                return res;
            }


            #endregion

            Maticsoft.ViewModel.Shop.PayAndShip payAndShip = new ViewModel.Shop.PayAndShip();



            //有收货地址 且 已选择配送 计算差异运费
            #region 运费
            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = new ShoppingCartInfo();
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new BLL.Shop.Products.ShoppingCartHelper(userId);

            string sku = Session["SubmitOrder_SKU"] as string;
            if (string.IsNullOrWhiteSpace(sku))
            {
                cartInfo = cartHelper.GetShoppingCart();
            }
            else
            {
                #region 指定SKU提交订单 此功能已投入使用
                int count = Common.Globals.SafeInt(Session["SubmitOrder_COUNT"], 1);
                int c = Common.Globals.SafeInt(Session["SubmitOrder_CountDown"], -1);
                int g = Common.Globals.SafeInt(Session["SubmitOrder_GroupBuy"], -1);

                //TODO: 未支持多个SKU BEN ADD 2013-06-23
                BLL.Shop.Products.SKUInfo skuManage = new BLL.Shop.Products.SKUInfo();
                BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
                SKUInfo skuInfo = skuManage.GetModelBySKU(sku);
                if (skuInfo == null)
                {
                    //终止计算运费, 运费0 在提交订单环节提示用户

                    Status = "NO";
                    Message = "请选择商品，此商品不存在";
                    josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                    res.Data = josn;
                    return res;
                }

                ProductInfo productInfo = productManage.GetModel(skuInfo.ProductId);
                if (productInfo == null)
                {
                    //终止计算运费, 运费0 在提交订单环节提示用户

                    Status = "NO";
                    Message = "请选择商品，此商品不存在";
                    josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                    res.Data = josn;
                    return res;
                }

                #region 限时抢购
                ProductInfo proSaleInfo = null;
                if (c > 0)
                {
                    proSaleInfo = productManage.GetProSaleModel(c);
                    if (proSaleInfo == null)
                    {
                        //终止计算运费, 运费0 在提交订单环节提示用户
                        Status = "NO";
                        Message = "请选择商品，此限时抢购商品不存在";
                        josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                        res.Data = josn;
                        return res;
                    }
                    if (DateTime.Now > proSaleInfo.EndDate)
                    {
                        Status = "NO";
                        Message = "请选择商品，此限时抢购商品活动已结束";
                        josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                        res.Data = josn;
                        return res;
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
                        Status = "NO";
                        Message = "请选择商品，此团购商品不存在";
                        josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                        res.Data = josn;
                        return res;
                    }

                    ////活动已过期 重定向到单品页
                    if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                    {
                        Status = "NO";
                        Message = "请选择商品，此团购商品活动已结束";
                        josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                        res.Data = josn;
                        return res;
                    }
                    if (DateTime.Now < groupBuyInfo.GroupBuy.StartDate)
                    {
                        Status = "NO";
                        Message = "请选择商品，此团购商品活动未开始";
                        josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                        res.Data = josn;
                        return res;
                    }
                    //    return RedirectToAction("GroupBuyDetail", "Product", new { area = "MShop", id = g });
                }
                #endregion

                cartInfo = GetCartInfo4SKU(productInfo, skuInfo, count, proSaleInfo, groupBuyInfo);
                #endregion
            }

            if (cartInfo == null)
            {
                //终止计算运费, 运费0 在提交订单环节提示用户

                Status = "NO";
                Message = "没有结算的商品";
                josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                res.Data = josn;
                return res;
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

            #region 计算优惠劵 金额
            decimal CouponPrice = 0;
            if (!string.IsNullOrWhiteSpace(couponCode))
            {

                Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = couponBll.GetCouponInfo(couponCode);
                if (infoModel != null)
                {
                    //计算一下优惠金额

                    if (infoModel.Status == 1 && (infoModel.LimitPrice < cartInfo.TotalAdjustedPriceNew))
                    {
                        if (cartInfo.TotalAdjustedPriceNew - infoModel.CouponPrice <= 0)
                        {
                            CouponPrice = cartInfo.TotalAdjustedPriceNew;

                        }
                        else
                        {
                            CouponPrice = infoModel.CouponPrice;
                        }
                    }



                }

            }
            #endregion

            //有收货地址 且 已选择配送 计算差异运费
            if (shippingAddress != null && shippingType.ModeId > 0)
            {
                Model.Ms.Regions regionInfo = _regionManage.GetModelByCache(shippingAddress.RegionId);
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
                //freight = _feeBLL.CalFreeShipping(topRegionId, cartInfo.TotalSellPrice, cartInfo, shippingType, shippingRegion);
                /*按商家计算运费*/
                var query = from rec in cartInfo.Items.Where(m => m.Selected == true) group rec by rec.SupplierId into s select new { weight = s.Sum(m => (m.Weight * m.Quantity)), supplier = s.Key, price = s.Sum(m => m.AdjustedPrice * m.Quantity) };


                Maticsoft.BLL.shop.freight.Shop_ProductsFreight sff = new BLL.shop.freight.Shop_ProductsFreight();



                FreightFee = "[";
                int k = 1;
                decimal supperCouponPricetotal = 0;
                foreach (var item in query.ToList())
                {
                    //1.针对特许商品是否包邮 ，购物车是否所有商品都免邮
                    List<Maticsoft.Model.shop.Freight.Shop_ProductsFreight> sfflist = new List<Model.shop.Freight.Shop_ProductsFreight>();

                    cartInfo.TotalWeight = item.weight;

                    cartInfo.Items.Where(m => m.SupplierId == item.supplier).ToList().ForEach
                        (
                            y =>
                            {
                                var z = sff.GetModel(y.ProductId);
                                if (z != null && y.ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
                                {
                                    sfflist.Add(z);
                                }
                            }
                        );
                    decimal supperCouponPrice = 0;

                    if (k == query.Count())
                    {
                        supperCouponPrice = CouponPrice - supperCouponPrice;
                    }
                    else
                    {
                        supperCouponPrice = Math.Floor((item.price / cartInfo.TotalAdjustedPriceNew) * CouponPrice);
                    }
                    int g = Common.Globals.SafeInt(Session["SubmitOrder_GroupBuy"], -1);
                    supperCouponPricetotal += supperCouponPrice;
                    if (g > 0)
                    {
                        FreightFee += "{\"supplier\":" + item.supplier + ",\"fee\":\"0\"},";
                    }
                    else
                    {
                        if (sfflist.Count == cartInfo.Items.Where(m => m.SupplierId == item.supplier).Count())
                        {
                            FreightFee += "{\"supplier\":" + item.supplier + ",\"fee\":" + sfflist.Max(y => y.Freight).ToString() + "},";
                        }
                        else
                        {
                            //2.针对特许商品是否包邮 ，购物车是否所有商品都免邮

                            FreightFee += "{\"supplier\":" + item.supplier + ",\"fee\":" + _feeBLL.CalFreeShipping(topRegionId, item.price - supperCouponPrice, cartInfo, shippingType, shippingRegion).ToString("0.00") + "},";
                        }
                    }
                }
                FreightFee = FreightFee.Substring(0, FreightFee.Length - 1);
                FreightFee += "]";
            }
            else
            {
                Status = "NO";
                Message = "请选择地区";
                josn = new { StatusStr = Status, MessageStr = Message, Freight = 6 };
                res.Data = josn;
                return res;
            }

            #endregion
            #endregion

            josn = new { StatusStr = Status, MessageStr = Message, FreightStr = FreightFee };
            res.Data = josn;
            return res;
        }
        #endregion

        #region 订单物流跟踪
        public ActionResult Ship()
        {
            ViewBag.EName = Request["ename"].ToString();
            ViewBag.Code = Request["code"].ToString();
            Maticsoft.BLL.Shop.Shipping.Express bll = new BLL.Shop.Shipping.Express();
            List<Maticsoft.Model.Shop.Shipping.LastData> model = new List<LastData>();
            List<Maticsoft.Model.Shop.Shipping.Shop_Express> list = bll.GetListModel("ExpressCode='" + ViewBag.Code + "'", "UpdateTime",0);
            if (list.Count > 0)
            {
                model = comm.JsonToObject<List<Maticsoft.Model.Shop.Shipping.LastData>>(list[0].ExpressContent.ToString());
            }
            return View(model);
        }
        #endregion

    }
}
