using System;
using System.Web.Mvc;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Model.Shop.Products;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Maticsoft.Components.Setting;
using System.Collections.Generic;
using Maticsoft.Json;
using System.Linq;
using Maticsoft.Web.Handlers;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class ShoppingCartController : ShopControllerBase
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new Maticsoft.BLL.Shop.Products.SKUInfo();
        private readonly BLL.Shop.ActivityManage.AMPBLL _ampBLL = new BLL.Shop.ActivityManage.AMPBLL();
        public ActionResult CartInfo()
        {
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
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            int count = cartHelper.GetShoppingCart().Quantity;
            return Content(count.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 购物车列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CartList(string viewName = "_CartList")
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            //DONE: 获取已选中内容的购物车进行 购物车 部分商品 下单 BEN Modify 20130923
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
        /// 普通商品添加购物车
        /// </summary>
        public ActionResult AddCart(string sku, int count = 1, string viewName = "AddCart")
        {
            BLL.Shop.PromoteSales.GroupBuy GB = new BLL.Shop.PromoteSales.GroupBuy();
            if (string.IsNullOrWhiteSpace(sku)) return RedirectToAction("Index", "Home");
            if (count < 1) count = 1;   //Safe Reset Count

            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            Maticsoft.Model.Shop.Products.ShoppingCartItem cartItem = new ShoppingCartItem();
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            Maticsoft.Model.Shop.Products.SKUInfo skuInfo = skuBll.GetModelBySKU(sku);
            Maticsoft.Model.Shop.PromoteSales.GroupBuy GB_Model = new Model.Shop.PromoteSales.GroupBuy();

            int groupbuyid = 0;
            if (!string.IsNullOrEmpty(Request.Params["g"]))
            {
                groupbuyid = int.Parse(Request.Params["g"].ToString());
                GB_Model = GB.GetModel(groupbuyid);
            }
            //NOSKU
            if (skuInfo == null) return Content("NOSKU");
            model.ProductSkus = new List<Model.Shop.Products.SKUInfo> { skuInfo };
            model.ProductInfo = productBll.GetModelByCache(skuInfo.ProductId);
            if (groupbuyid > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo groupBuyInfo = productBll.GetGroupBuyModel(groupbuyid);
                
                if (groupBuyInfo == null) return null;

                //活动已过期 重定向到单品页
                if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                    throw new ArgumentNullException("活动已过期");

                //重置价格为 限时抢购价
                cartItem.AdjustedPrice = groupBuyInfo.GroupBuy.Price;
            }
            else
            {
                cartItem.SellPrice = cartItem.AdjustedPrice = model.ProductSkus[0].SalePrice;
            }
            cartItem.GroupBuyId = 0;
            cartItem.PromotionType = -1;
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
                cartItem.CostPrice2 = model.ProductSkus[0].CostPrice2.HasValue ? model.ProductSkus[0].CostPrice2.Value : 0;
                cartItem.MarketPrice = model.ProductInfo.MarketPrice.HasValue ? model.ProductInfo.MarketPrice.Value : 0;
                cartItem.Weight = model.ProductSkus[0].Weight.HasValue ? model.ProductSkus[0].Weight.Value : 0;
                cartItem.Unit = model.ProductInfo.Unit;
                cartItem.Points = (int)(model.ProductInfo.Points.HasValue ? model.ProductInfo.Points : 0);
                cartHelper.AddItem(cartItem);

                Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
                //TODO: 添加购物车如果要展示, 这里的价格需提示优惠价格 BEN ADD 2013-06-24
                ViewBag.TotalPrice = cartInfo.TotalSellPrice;
                ViewBag.ItemCount = cartInfo.Quantity;
            }
            ViewBag.Title = "添加购物车";
            return RedirectToAction("CartInfo");
        }

        /// <summary>
        /// 购物车必须要有sku
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="count"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProSaleAddCart(string sku, int count = 1, string viewName = "AddCart")
        {
            JsonObject json = new JsonObject();
            BLL.Shop.PromoteSales.GroupBuy GB = new BLL.Shop.PromoteSales.GroupBuy();
            if (string.IsNullOrWhiteSpace(sku)) return RedirectToAction("Index", "Home");
            if (count < 1) count = 1;   //Safe Reset Count

            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            Maticsoft.Model.Shop.Products.ShoppingCartItem cartItem = new ShoppingCartItem();
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            Maticsoft.Model.Shop.Products.SKUInfo skuInfo = skuBll.GetModelBySKU(sku);
            Maticsoft.Model.Shop.PromoteSales.GroupBuy GB_Model = new Model.Shop.PromoteSales.GroupBuy();

            int groupbuyid = 0;
            if (!string.IsNullOrEmpty(Request.Params["g"]))
            {
                groupbuyid = int.Parse(Request.Params["g"].ToString());
                GB_Model = GB.GetModel(groupbuyid);
                cartItem.GroupBuyId = groupbuyid;
                cartItem.PromotionType = GB_Model.PromotionType;
            }
            else
            {
                cartItem.PromotionType = -1;
            }
            //NOSKU
            if (skuInfo == null) return Content("NOSKU");
            model.ProductSkus = new List<Model.Shop.Products.SKUInfo> { skuInfo };
            model.ProductInfo = productBll.GetModelByCache(skuInfo.ProductId);
            if (groupbuyid > 0)
            {
                Maticsoft.Model.Shop.Products.ProductInfo groupBuyInfo = productBll.GetGroupBuyModel(groupbuyid);
                if (groupBuyInfo == null) return null;

                //活动已过期 重定向到单品页
                if (DateTime.Now > groupBuyInfo.GroupBuy.EndDate)
                {
                    json.Accumulate("result", "False");
                    json.Accumulate("Msg", "活动已过期");
                    return Content(json.ToString());
                }
                //重置价格为 限时抢购价
                cartItem.SellPrice = cartItem.AdjustedPrice = groupBuyInfo.GroupBuy.Price;
            }
            else
            {
                cartItem.SellPrice = cartItem.AdjustedPrice = model.ProductSkus[0].SalePrice;
            }
            if (model.ProductInfo != null && model.ProductSkus != null)
            {
                //非团购类的,都要限制 20141004 wuwg,限制一单多买
                if (GB_Model.PromotionType !=-1)
                {
                    int buyLimit = GB.GetGroupBuyLimit(currentUser.UserID, int.Parse(model.ProductInfo.ProductId.ToString()));
                    var groupmodel = GB.GetPromotionLimitQu(int.Parse(model.ProductInfo.ProductId.ToString()));
                    ShoppingCartInfo shopcart = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart4Selected());
                    if (shopcart.Items.Count != 0)
                    {

                        var x = shopcart.Items.Where(q => q.PromotionType!=-1).ToList();
                        if (x.Count > 0)
                        {
                            json.Accumulate("result", "False");
                            json.Accumulate("Msg", "不能同时添加多个活动商品,请分多次下单!");
                            return Content(json.ToString());
                        }


                        if (shopcart.Items.Where(m => m.ProductId == model.ProductInfo.ProductId).ToList().Count != 0)
                        {
                            int alreadyBuy = shopcart.Items.Where(m => m.ProductId == model.ProductInfo.ProductId).ToList().First().Quantity;

                            if (alreadyBuy+groupmodel.BuyCount>groupmodel.MaxCount)
                            {
                                var s = groupmodel.MaxCount - (alreadyBuy + groupmodel.BuyCount);
                                json.Accumulate("result", "False");
                                json.Accumulate("Msg", "超过活动限购数量");
                                return Content(json.ToString());
                            }
                            //大于帐号限购值
                            if (alreadyBuy >= buyLimit)
                            {
                                json.Accumulate("result", "False");
                                json.Accumulate("Msg", "已达到限购值");
                                return Content(json.ToString());
                            }
                        }
                    }
                    cartItem.QuantityLimit = buyLimit;

                }
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
                cartItem.Weight = model.ProductSkus[0].Weight.HasValue ? model.ProductSkus[0].Weight.Value : 0;
                cartItem.Unit = model.ProductInfo.Unit;
                cartItem.Points = (int)(model.ProductInfo.Points.HasValue ? model.ProductInfo.Points : 0);
                cartHelper.AddItem(cartItem);

                Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
                //TODO: 添加购物车如果要展示, 这里的价格需提示优惠价格 BEN ADD 2013-06-24
                ViewBag.TotalPrice = cartInfo.TotalSellPrice;
                ViewBag.ItemCount = cartInfo.Quantity;
            }
            ViewBag.Title = "添加购物车";
            json.Accumulate("result", "True");
            json.Accumulate("Msg", "/ShoppingCart/CartInfo");
            return Content(json.ToString());
        }

        #region Ajax 方法

        /// <summary>
        /// 移除订单项
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
        public ActionResult ClearShopCart()
        {
            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            cartHelper.ClearShoppingCart();
            return Content("Yes");
        }

        /// <summary>
        /// 选择购物项
        /// </summary>
        [HttpPost]
        public ActionResult SelectedItem(int id)
        {
            int itemId = Common.Globals.SafeInt(id, -1);
            if (itemId < 1) return Content("NOITEMID");

            int userId = currentUser == null ? -1 : currentUser.UserID;
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
            if (cartInfo == null || cartInfo.Quantity < 1) return Content("NOITEMS");

            cartInfo[itemId].Selected = !cartInfo[itemId].Selected;
            cartHelper.SaveShoppingCart(cartInfo);

            return Content("OK");
        }
        [HttpPost]
        public ActionResult SelectedItemAll(string itemALl)
        {
            if (itemALl == "") return Content("OK");

            itemALl = itemALl.Substring(0,itemALl.Length-1);
            string[] itemList = itemALl.Split(',');
            foreach (var item in itemList)
            {
                int itemId = Common.Globals.SafeInt(item, -1);
                if (itemId < 1) return Content("NOITEMID");

                int userId = currentUser == null ? -1 : currentUser.UserID;
                Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
                Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo = _ampBLL.GetTotalPriceAfterActivity(cartHelper.GetShoppingCart());
                if (cartInfo == null || cartInfo.Quantity < 1) return Content("NOITEMS");

                cartInfo[itemId].Selected = !cartInfo[itemId].Selected;
                cartHelper.SaveShoppingCart(cartInfo);
            }
            return Content("OK");
        }

        #endregion
    }
}
