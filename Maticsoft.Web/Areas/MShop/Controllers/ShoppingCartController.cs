using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Maticsoft.Json;
using Maticsoft.Web.Handlers;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class ShoppingCartController : MShopControllerBase
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new Maticsoft.BLL.Shop.Products.SKUInfo();
        private readonly BLL.Shop.ActivityManage.AMPBLL _ampBLL = new BLL.Shop.ActivityManage.AMPBLL();
        public ActionResult CartInfo()
        {
            #region Remove MShop SubmitOrderSession SKU/COUNT/CountDown/GroupBuy
            Session.Remove("SubmitOrder_SKU");
            Session.Remove("SubmitOrder_COUNT");
            Session.Remove("SubmitOrder_CountDown");
            Session.Remove("SubmitOrder_GroupBuy");
            #endregion

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "购物车信息" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        /// <summary>
        /// 获取购物车数量
        /// </summary>
        public ActionResult GetCartCount()
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            return Content(cartHelper.GetShoppingCart().Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }
        /// <summary>
        /// 购物车列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CartList(string viewName = "_CartList")
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            ShoppingCartModel model = new ShoppingCartModel();
            model.AllCartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
            model.SelectedCartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart4Selected());

            #region 批销优惠
            try
            {
                BLL.Shop.Sales.SalesRuleProduct salesRule = new BLL.Shop.Sales.SalesRuleProduct();
                model.AllCartInfo = salesRule.GetWholeSale(model.AllCartInfo);
                model.SelectedCartInfo = salesRule.GetWholeSale(model.SelectedCartInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
            return View(viewName, model);
        }

        /// <summary>
        /// 添加购物车  
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCartOld(string sku, int count = 1, string viewName = "AddCart")
        {
            if (string.IsNullOrWhiteSpace(sku)) return RedirectToAction("Index", "Home");
            if (count < 1) count = 1;   //Safe Reset Count

            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            Maticsoft.Model.Shop.Products.ShoppingCartItem cartItem = new ShoppingCartItem();
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();

            Model.Shop.Products.SKUInfo skuInfo = skuBll.GetModelBySKU(sku);

            //NOSKU
            if (skuInfo == null) return Content("NOSKU");

            model.ProductSkus = new List<Model.Shop.Products.SKUInfo> { skuInfo };
            model.ProductInfo = productBll.GetModelByCache(skuInfo.ProductId);
            if (model.ProductInfo != null && model.ProductSkus != null)
            {
                cartItem.Name = model.ProductInfo.ProductName;
                cartItem.Quantity = count;
                cartItem.SKU = model.ProductSkus[0].SKU;
                cartItem.ProductId = model.ProductInfo.ProductId;
                cartItem.UserId = userId;
                #region 商家
                if (model.ProductInfo.SupplierId > 0)
                {
                    BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                    Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(model.ProductInfo.SupplierId);
                    if (supplierInfo != null)
                    {
                        cartItem.SupplierId = supplierInfo.SupplierId;
                        cartItem.SupplierName = supplierInfo.Name;
                    }
                }
                #endregion
                //DONE: 根据SkuId 获取SKUItem值和图片数据 BEN 2013-06-30
                List<Model.Shop.Products.SKUItem> listSkuItems = skuBll.GetSKUItemsBySkuId(skuInfo.SkuId);
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
                cartItem.ThumbnailsUrl = model.ProductInfo.ThumbnailUrl1;

                cartItem.CostPrice = model.ProductSkus[0].CostPrice.HasValue ? model.ProductSkus[0].CostPrice.Value : 0;
                cartItem.MarketPrice = model.ProductInfo.MarketPrice.HasValue ? model.ProductInfo.MarketPrice.Value : 0;

                cartItem.SellPrice = cartItem.AdjustedPrice = model.ProductSkus[0].SalePrice;
                cartItem.Weight = model.ProductSkus[0].Weight.HasValue ? model.ProductSkus[0].Weight.Value : 0;
                cartItem.Unit = model.ProductInfo.Unit;
                cartItem.Points = (int)(model.ProductInfo.Points.HasValue ? model.ProductInfo.Points : 0);
                cartHelper.AddItem(cartItem);

                Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = cartHelper.GetShoppingCart();
                //TODO: 添加购物车如果要展示, 这里的价格需提示优惠价格 BEN ADD 2013-06-24
                ViewBag.TotalPrice = cartInfo.TotalSellPrice;
                ViewBag.ItemCount = cartInfo.Quantity;
            }
            ViewBag.Title = "添加购物车";
            return RedirectToAction("CartInfo");
            //return View(viewName, model);
        }

        /// <summary>
        /// 添加购物车  
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCart(string sku, int count = 1, int ActiveID = -1, int ActiveType = -1, string viewName = "AddCart")
        {
            if (string.IsNullOrWhiteSpace(sku)) return RedirectToAction("Index", "Home");
            if (count < 1) count = 1;   //Safe Reset Count

            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            Maticsoft.Model.Shop.Products.ShoppingCartItem cartItem = new ShoppingCartItem();
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();

            Model.Shop.Products.SKUInfo skuInfo = skuBll.GetModelBySKU(sku);

            //NOSKU
            if (skuInfo == null) return Content("NOSKU");

            model.ProductSkus = new List<Model.Shop.Products.SKUInfo> { skuInfo };
            model.ProductInfo = productBll.GetModelByCache(skuInfo.ProductId);
            if (model.ProductInfo != null && model.ProductSkus != null)
            {
                cartItem.Name = model.ProductInfo.ProductName;
                cartItem.Quantity = count;
                cartItem.SKU = model.ProductSkus[0].SKU;
                cartItem.ProductId = model.ProductInfo.ProductId;
                cartItem.UserId = userId;
                #region 商家
                if (model.ProductInfo.SupplierId > 0)
                {
                    BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                    Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(model.ProductInfo.SupplierId);
                    if (supplierInfo != null)
                    {
                        cartItem.SupplierId = supplierInfo.SupplierId;
                        cartItem.SupplierName = supplierInfo.Name;
                    }
                }
                #endregion
                //DONE: 根据SkuId 获取SKUItem值和图片数据 BEN 2013-06-30
                List<Model.Shop.Products.SKUItem> listSkuItems = skuBll.GetSKUItemsBySkuId(skuInfo.SkuId);
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
                cartItem.ThumbnailsUrl = model.ProductInfo.ThumbnailUrl1;

                cartItem.CostPrice = model.ProductSkus[0].CostPrice.HasValue ? model.ProductSkus[0].CostPrice.Value : 0;
                cartItem.MarketPrice = model.ProductInfo.MarketPrice.HasValue ? model.ProductInfo.MarketPrice.Value : 0;

                cartItem.SellPrice = cartItem.AdjustedPrice = model.ProductSkus[0].SalePrice;
                cartItem.Weight = model.ProductSkus[0].Weight.HasValue ? model.ProductSkus[0].Weight.Value : 1;
                cartItem.Unit = model.ProductInfo.Unit;
                cartItem.Points = (int)(model.ProductInfo.Points.HasValue ? model.ProductInfo.Points : 0);

                //微信购买的商品价格处理
                if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.WeiXin)
                {
                    BLL.Shop.PromoteSales.WeiXinGroupBuy bll = new BLL.Shop.PromoteSales.WeiXinGroupBuy();
                    Model.Shop.PromoteSales.WeiXinGroupBuy WeiXinGroupBuyInfo = bll.GetModel(ActiveID);
                    if (WeiXinGroupBuyInfo != null)
                    {
                        cartItem.SaleDes = "微信购买";
                        cartItem.ActiveID = ActiveID;
                        cartItem.ActiveType = ActiveType;
                        if (WeiXinGroupBuyInfo.Price != 0)
                        {
                            cartItem.AdjustedPrice = WeiXinGroupBuyInfo.Price;
                        }
                    }
                }

                //小时购商品入口
                if (ActiveType == (int)Maticsoft.Web.Areas.MShop.Controllers.ProductController.ActiveTypeEnum.GroupBuy)
                {
                    BLL.Shop.PromoteSales.GroupBuy bll = new BLL.Shop.PromoteSales.GroupBuy();
                    Model.Shop.PromoteSales.GroupBuy GroupBuyInfo = bll.GetModel(ActiveID);
                    if (GroupBuyInfo != null)
                    {
                        cartItem.SaleDes = "微信购买";
                        cartItem.ActiveID = ActiveID;
                        cartItem.ActiveType = ActiveType;
                        if (GroupBuyInfo.Price != 0)
                        {
                            cartItem.AdjustedPrice = GroupBuyInfo.Price;
                        }
                    }
                }

                cartHelper.AddItem(cartItem);

                Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = cartHelper.GetShoppingCart();
                //TODO: 添加购物车如果要展示, 这里的价格需提示优惠价格 BEN ADD 2013-06-24
                ViewBag.TotalPrice = cartInfo.TotalSellPrice;
                ViewBag.ItemCount = cartInfo.Quantity;
            }
            ViewBag.Title = "添加购物车";
            return RedirectToAction("CartInfo");
            //return View(viewName, model);
        }

        #region Ajax 方法
        /// <summary>
        /// 移除订单项
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult RemoveItem(FormCollection Fm)
        {
            if (String.IsNullOrWhiteSpace(Fm["ItemIds"]))
            {
                return Content("No");
            }
            else
            {
                string itemIds = Fm["ItemIds"];
                var item_arr = itemIds.Split(',');
                int userId = currentUser == null ? -1 : currentUser.UserID;
                Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
                foreach (var item in item_arr)
                {
                    int itemId = Common.Globals.SafeInt(item, 0);
                    cartHelper.RemoveItem(itemId);
                }
                return Content("Yes");
            }
        }

        /// <summary>
        /// 更新购物车项数量
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult UpdateItemCount(FormCollection Fm)
        {
            if (String.IsNullOrWhiteSpace(Fm["ItemId"]) || String.IsNullOrWhiteSpace(Fm["Count"]))
            {
                return Content("No");
            }
            else
            {
                int itemId = Common.Globals.SafeInt(Fm["ItemId"], 0);
                int count = Common.Globals.SafeInt(Fm["Count"], 1);
                int userId = currentUser == null ? -1 : currentUser.UserID;
                Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
                cartHelper.UpdateItemQuantity(itemId, count);
                return Content("Yes");
            }
        }
        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult ClearShopCart()
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            cartHelper.ClearShoppingCart();
            return Content("Yes");
        }


        /// <summary>
        ///  商品列表页直接加入购物车
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCart(FormCollection Fm)
        {
            JsonObject json = new JsonObject();
            long productid = Common.Globals.SafeLong(Fm["Productid"], -1);
            int count = Common.Globals.SafeInt(Fm["Count"], 1);
            if (count < 1) count = 1;   //Safe Reset Count
            string sku = Common.Globals.SafeString(Fm["Sku"], "");
            if (productid <= 0 && String.IsNullOrWhiteSpace(sku))
            {
                json.Accumulate(HandlerBase.KEY_STATUS, HandlerBase.STATUS_FAILED);
                json.Accumulate(HandlerBase.KEY_DATA, "NO");
                return Content(json.ToString());
            }

            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            Maticsoft.Model.Shop.Products.ShoppingCartItem cartItem = new ShoppingCartItem();
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            Model.Shop.Products.SKUInfo skuInfo = null;
            if (!String.IsNullOrWhiteSpace(sku))
            {
                skuInfo = skuBll.GetModelBySKU(sku);
            }
            else
            {
                ProductSKUModel prouctsku = skuBll.GetProductSKUInfoByProductId(productid);
                if (prouctsku == null || prouctsku.ListSKUInfos == null || prouctsku.ListSKUInfos.Count <= 0)
                {
                    json.Accumulate(HandlerBase.KEY_STATUS, HandlerBase.STATUS_FAILED);
                    json.Accumulate(HandlerBase.KEY_DATA, "NOSKU");
                    return Content(json.ToString());
                }
                skuInfo = prouctsku.ListSKUInfos[0];
            }
            //NOSKU
            if (skuInfo == null)
            {
                json.Accumulate(HandlerBase.KEY_STATUS, HandlerBase.STATUS_FAILED);
                json.Accumulate(HandlerBase.KEY_DATA, "NOSKU");
                return Content(json.ToString());
            }
            #region 判断库存
            int minStock = 0;//最小库存
            if (BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_OpenAlertStock"))//开启警戒库存
            {
                minStock = skuInfo.AlertStock;
            }
            if ((skuInfo.Stock - count) < minStock)
            {
                json.Accumulate(HandlerBase.KEY_STATUS, HandlerBase.STATUS_FAILED);
                json.Accumulate(HandlerBase.KEY_DATA, "NOSTOCK");//库存不足
                return Content(json.ToString());
            }
            #endregion

            model.ProductSkus = new List<Model.Shop.Products.SKUInfo> { skuInfo };
            model.ProductInfo = productBll.GetModelByCache(skuInfo.ProductId);
            if (model.ProductInfo != null && model.ProductSkus != null)
            {
                cartItem.Name = model.ProductInfo.ProductName;
                cartItem.Quantity = count;
                cartItem.SKU = model.ProductSkus[0].SKU;
                cartItem.ProductId = model.ProductInfo.ProductId;
                cartItem.UserId = userId;
                #region 商家
                if (model.ProductInfo.SupplierId > 0)
                {
                    BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
                    Model.Shop.Supplier.SupplierInfo supplierInfo = supplierManage.GetModelByCache(model.ProductInfo.SupplierId);
                    if (supplierInfo != null)
                    {
                        cartItem.SupplierId = supplierInfo.SupplierId;
                        cartItem.SupplierName = supplierInfo.Name;
                    }
                }
                #endregion
                //DONE: 根据SkuId 获取SKUItem值和图片数据 BEN 2013-06-30
                List<Model.Shop.Products.SKUItem> listSkuItems = skuBll.GetSKUItemsBySkuId(skuInfo.SkuId);
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
                cartItem.ThumbnailsUrl = model.ProductInfo.ThumbnailUrl1;

                cartItem.CostPrice = model.ProductSkus[0].CostPrice.HasValue ? model.ProductSkus[0].CostPrice.Value : 0;
                cartItem.MarketPrice = model.ProductInfo.MarketPrice.HasValue ? model.ProductInfo.MarketPrice.Value : 0;

                cartItem.SellPrice = cartItem.AdjustedPrice = model.ProductSkus[0].SalePrice;
                cartItem.Weight = model.ProductSkus[0].Weight.HasValue ? model.ProductSkus[0].Weight.Value : 0;
                cartItem.Points = (int)(model.ProductInfo.Points.HasValue ? model.ProductInfo.Points : 0);
                cartItem.Unit = model.ProductInfo.Unit;
                cartHelper.AddItem(cartItem);
                ShoppingCartInfo cartInfo = cartHelper.GetShoppingCart();
                if (cartInfo != null && cartInfo.Items != null && cartInfo.Items.Count > 0)
                {
                    json.Accumulate(HandlerBase.KEY_STATUS, HandlerBase.STATUS_SUCCESS);
                    json.Accumulate(HandlerBase.KEY_DATA, cartInfo.Items[cartInfo.Items.Count - 1].ItemId.ToString());//itemid
                    return Content(json.ToString());
                }
            }
            json.Accumulate(HandlerBase.KEY_STATUS, HandlerBase.STATUS_FAILED);
            json.Accumulate(HandlerBase.KEY_DATA, "NO");
            return Content(json.ToString());
        }

        #endregion

        /// <summary>
        /// 获取购物车商品金额
        /// </summary>
        public ActionResult GetCartTotalPrice()
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            JsonObject json = new JsonObject();
            json.Accumulate("Quantity", cartHelper.GetShoppingCart().Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            json.Accumulate("TotalAdjustedPrice", cartHelper.GetShoppingCart().TotalAdjustedPrice.ToString("F"));
            return Content(json.ToString());
        }
        /// <summary>
        /// 购物车列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCartList()
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = cartHelper.GetShoppingCart();

            #region 批销优惠
            try
            {
                BLL.Shop.Sales.SalesRuleProduct salesRule = new BLL.Shop.Sales.SalesRuleProduct();
                cartInfo = salesRule.GetWholeSale(cartInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

            JsonObject json = new JsonObject();
            json.Accumulate(HandlerBase.KEY_STATUS, HandlerBase.STATUS_SUCCESS);
            json.Accumulate(HandlerBase.KEY_DATA, cartInfo.Items);
            return Content(json.ToString());
        }

    }
}
