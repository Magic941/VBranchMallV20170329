/**
* ProductHandler.cs
*
* 功 能： Shop API
* 类 名： ProductHandler
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
using Maticsoft.Json;
using Maticsoft.Json.RPC;
using Maticsoft.Web.Handlers.API;
using Maticsoft.ViewModel.Shop;
using System.Collections.Generic;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Model.Shop.Products;
using Maticsoft.BLL.Shop.Products;

namespace Maticsoft.Web.Handlers.Shop.API
{
    public partial class ShopHandler
    {
        private BLL.Shop.Products.SKUInfo skuBll = new BLL.Shop.Products.SKUInfo();
        #region 昵称是否存在
        //[JsonRpcMethod("HasUserByNickName", Idempotent = true)]
        //[JsonRpcHelp("昵称是否存在")]
        //public JsonObject HasUserByNickName(string NickName)
        //{
        //    if (string.IsNullOrWhiteSpace(NickName)) return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
        //    Maticsoft.Accounts.Bus.User bllUser = new User();
        //    return Result.HasResult(bllUser.HasUserByNickName(NickName));
        //}
        #endregion

        #region 获取个人信息
        ///// <summary>
        ///// 获取个人信息
        ///// </summary>
        ///// <param name="UserId">用户ID</param>
        ///// <returns>用户信息</returns>
        //[JsonRpcMethod("GetUserInfo", Idempotent = false)]
        //[JsonRpcHelp("获取个人信息")]
        //public JsonObject GetUserInfo(int UserId)
        //{
        //    //超级管理员信息保护 过滤UserId=1用户
        //    if (UserId < 2) return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
        //    try
        //    {
        //        //TODO: 用户不存在 未对应
        //        return new Result(ResultStatus.Success,
        //            GetUserInfo4Json(new User(UserId)));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message), ex.StackTrace, Request);
        //        return new Result(ResultStatus.Error, ex);
        //    }
        //}

        //private JsonObject GetUserInfo4Json(User userInfo)
        //{
        //    if (userInfo == null || string.IsNullOrWhiteSpace(userInfo.UserType)) return null;
        //    JsonObject json = new JsonObject();
        //    json.Put("UserId", userInfo.UserID);
        //    json.Put("UserName", userInfo.UserName);
        //    json.Put("TrueName", userInfo.TrueName);
        //    json.Put("Phone", userInfo.Phone);
        //    json.Put("DepartmentID", userInfo.DepartmentID);
        //    json.Put("FileNo", userInfo.NickName);

        //    return json;
        //}
        #endregion

        [JsonRpcMethod("CountDownKillList", Idempotent = false)]
        [JsonRpcHelp("秒杀商品列表")]
        #region  限时抢购
        public JsonObject CountDownKillList(int? page = 1, int pageNum = 10)
        {
            if (pageNum == 0)
            {
                pageNum = 10;
            }
            if (String.IsNullOrEmpty(page.ToString()))
            {
                page = 1;
            }
            ProductListModel model = new ProductListModel();
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pageNum + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value > 1 ? startIndex + pageNum - 1 : pageNum;
            int toalCount = productManage.GetProSalesCount();
            List<Model.Shop.Products.ProductInfo> list = productManage.GetProSalesList(startIndex, endIndex,1);
            JsonObject result = new JsonObject();
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            //获取总条数
            if (toalCount < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            result.Put("list_count", toalCount);
            foreach (Maticsoft.Model.Shop.Products.ProductInfo item in list)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("title", item.ProductName);
                json.Put("pic", Maticsoft.Web.Components.FileHelper.GeThumbImage(item.ThumbnailUrl1, "T175X228_"));
                json.Put("marketprice", ((Decimal)item.MarketPrice).ToString("F"));
                json.Put("saleprice", item.ProSalesPrice.ToString("F"));
                json.Put("lefttime", item.ProSalesEndDate.ToString("yyyy年MM月dd日HH时mm分ss秒"));
                jsonArray.Add(json);
            }
            result.Put("productlist", jsonArray);
            return new Result(ResultStatus.Success, result);
        }


        #endregion

        #region 商品收藏
        [JsonRpcMethod("AjaxAddFav", Idempotent = false)]
        [JsonRpcHelp("商品收藏")]
        public JsonObject AddFav(int pId, int UserID)
        {
            if (UserID < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            if (!String.IsNullOrWhiteSpace(pId.ToString()))
            {
                int productId = Common.Globals.SafeInt(pId, 0);
                Maticsoft.BLL.Shop.Favorite favBll = new BLL.Shop.Favorite();
                JsonArray result = new JsonArray();
                Maticsoft.Model.Shop.Favorite favMode = new Maticsoft.Model.Shop.Favorite();
                favMode.CreatedDate = DateTime.Now;
                favMode.TargetId = productId;
                favMode.Type = 1;
                favMode.UserId = UserID;
                if (favBll.Add(favMode) > 0)
                {
                    result.Add("Success");
                }
                else
                {
                    result.Add("Success");
                }
                return new Result(ResultStatus.Success, result);
            }
            return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
        }
        #endregion

        #region 商品评论
        [JsonRpcMethod("productCommon", Idempotent = false)]
        [JsonRpcHelp("商品评论")]
        public JsonObject productCommon(int pId, int page = 1, int pageNum = 10)
        {
            if (pageNum == 0)
            {
                pageNum = 10;
            }
            if (String.IsNullOrEmpty(page.ToString()))
            {
                page = 1;
            }
            if (pId < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            //计算分页起始索引
            int startIndex = page > 1 ? (page - 1) * pageNum + 1 : 0;

            //计算分页结束索引
            int endIndex = page * pageNum;
            int totalCount = 0;

            //获取总条数
            totalCount = reviewsBll.GetRecordCount("Status=1 and ProductId=" + pId);

            if (totalCount < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonObject result = new JsonObject();
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            result.Put("list_count", totalCount);
            List<Maticsoft.Model.Shop.Products.ProductReviews> productReviewses = reviewsBll.GetReviewsByPage(pId, " CreatedDate desc", startIndex, endIndex);

            PagedList<Maticsoft.Model.Shop.Products.ProductReviews> lists = new PagedList<Maticsoft.Model.Shop.Products.ProductReviews>(productReviewses, page, pageNum, totalCount);
            foreach (Maticsoft.Model.Shop.Products.ProductReviews item in lists)
            {
                json = new JsonObject();
                json.Put("name", item.UserName);
                json.Put("commontime", item.CreatedDate);
                json.Put("commoncontent", item.ReviewText);
                jsonArray.Add(json);
            }
            result.Put("commonlist", jsonArray);
            return new Result(ResultStatus.Success, result);
        }
        #endregion

        [JsonRpcMethod("PRDescription", Idempotent = false)]
        [JsonRpcHelp("商品描述")]
        public JsonObject PRDescription(int pId)
        {
            if (pId < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetModel(pId);
            if (String.IsNullOrEmpty(model.ProductInfo.ToString()))
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonArray jsonArray = new JsonArray();
            jsonArray.Add(model.ProductInfo.Description);
            return new Result(ResultStatus.Success, jsonArray);
        }

        #region 商品详情
        [JsonRpcMethod("ProductDetail", Idempotent = false)]
        [JsonRpcHelp("商品详情")]
        public JsonObject ProductDetail(int pId = -1)
        {
            if (pId <= 0)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            Maticsoft.ViewModel.Shop.ProductModel model = new ProductModel();
            model.ProductInfo = productManage.GetModel(pId);
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = new List<Maticsoft.Model.Shop.Products.ProductInfo>();
            list.Add(model.ProductInfo);
            JsonArray jsonArray = new JsonArray();
            JsonObject result = new JsonObject();
            JsonObject json;
            model.ProductSkus = skuBll.GetProductSkuInfo(pId);
            if (model.ProductSkus.Count < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            string salesprice = model.ProductSkus[0].SalePrice.ToString("F");
            JsonArray picArray = new JsonArray();
            BLL.Shop.Products.ProductImage imageManage = new BLL.Shop.Products.ProductImage();
            model.ProductImages = imageManage.ProductImagesList(pId);
            JsonObject pidJson;
            ViewModel.Shop.ProductSKUModel productSKUModel = skuBll.GetProductSKUInfoByProductId(model.ProductInfo.ProductId);
            if (productSKUModel == null)
            {
                return new Result(ResultStatus.Success, null);
            }

            JsonObject attr;
            foreach (Maticsoft.Model.Shop.Products.ProductInfo item in list)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("title", item.ProductName);
                json.Put("marketprice", ((Decimal)item.MarketPrice).ToString("F"));
                json.Put("saleprice", salesprice);
                json.Put("leftTime", item.ProSalesEndDate.ToString("yyyy年MM月dd日HH时mm分ss秒")); //TimeSpan.Parse(item.ProSalesEndDate.ToShortTimeString().ToString()).TotalSeconds);
                json.Put("pic", item.ThumbnailUrl1);
                foreach (Maticsoft.Model.Shop.Products.ProductImage pro in model.ProductImages)
                {
                    pidJson = new JsonObject();
                    picArray.Add(pro.ThumbnailUrl1);
                }
                json.Put("bigPic", picArray);
                json.Put("commentCount", reviewsBll.GetRecordCount("Status=1 and ProductId=" + pId));
                JsonArray skuArray = new JsonArray();
                foreach (KeyValuePair<Maticsoft.Model.Shop.Products.AttributeInfo, SortedSet<Maticsoft.Model.Shop.Products.SKUItem>>
   attrSKUItem in productSKUModel.ListAttrSKUItems)
                {
                    foreach (Maticsoft.Model.Shop.Products.SKUItem skuItem in attrSKUItem.Value)
                    {
                        attr = new JsonObject();
                        attr.Put("id", skuItem.ValueId);
                        attr.Put("key", skuItem.AttributeName);
                        attr.Put("value", skuItem.ValueStr);
                        skuArray.Add(attr);
                    }
                }
                if (skuArray != null && skuArray.Length > 0)
                {
                    json.Put("productProperty", skuArray);
                }

                //NO SKU ERROR
                if (productSKUModel.ListSKUInfos != null &&
                    productSKUModel.ListSKUInfos.Count > 0 &&
                    productSKUModel.ListSKUItems != null)
                {
                    json.Put("hasSKU", true);
                    json.Put("hasStock", true);
                    //木有开启SKU的情况
                    if (productSKUModel.ListSKUItems.Count == 0)
                    {
                        json.Put("hasSKU", false);
                        //判断库存是否满足
                        json.Put("hasStock", true);

                        //是否开启警戒库存判断
                        bool IsOpenAlertStock =
                            Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_OpenAlertStock");
                        if (IsOpenAlertStock &&
                            productSKUModel.ListSKUInfos[0].Stock <= productSKUModel.ListSKUInfos[0].AlertStock)
                        {
                            json.Put("hasStock", false);
                        }

                        if (productSKUModel.ListSKUInfos[0].Stock < 1)
                        {
                            json.Put("hasStock", false);
                        }
                    }
                }
                SKUInfoToJson(json, productSKUModel.ListSKUInfos);
                result.Put("product", json);
            }

            return new Result(ResultStatus.Success, result);


        }

        private Json.JsonObject SKUInfoToJson(Json.JsonObject json,List<Model.Shop.Products.SKUInfo> list)
        {
            if (list == null || list.Count < 1) return null;

            Json.JsonObject jsonSKU = new Json.JsonObject();
            long[] key;
            int index;
            foreach (Model.Shop.Products.SKUInfo item in list)
            {
                if (item.SkuItems == null || item.SkuItems.Count < 1) continue;

                //无库存SKU不提供给页面
                //是否开启警戒库存判断
                bool IsOpenAlertStock = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_OpenAlertStock");
                if (IsOpenAlertStock && item.Stock <= item.AlertStock)
                {
                    continue;
                }
                if (item.Stock < 1)
                    continue;

                //组合SKU 的 ValueId
                key = new long[item.SkuItems.Count];
                index = 0;
                item.SkuItems.ForEach(xx => key[index++] = xx.ValueId);
                jsonSKU.Accumulate(string.Join(",", key), new
                {
                    sku = item.SKU,
                    count = item.Stock,
                    price = item.SalePrice
                });
            }

            //获取最小/最大价格
            list.Sort((x, y) => x.SalePrice.CompareTo(y.SalePrice));
            json.Put("defaultPrice", new
            {
                minPrice = list[0].SalePrice,
                maxPrice = list[list.Count - 1].SalePrice
            });
            json.Put("skuData", jsonSKU);
            return json;
        }
        #endregion

        [JsonRpcMethod("AddCart", Idempotent = false)]
        [JsonRpcHelp("放入购物车")]
        public JsonObject AddCart(int userID)
        {
            if (userID == 0)
            {
                userID = -1;
            }
            Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userID);
            //DONE: 获取已选中内容的购物车进行 购物车 部分商品 下单 BEN Modify 20130923
            ShoppingCartModel model = new ShoppingCartModel();
            model.AllCartInfo = cartHelper.GetShoppingCart();
            model.SelectedCartInfo = cartHelper.GetShoppingCart4Selected();
            #region 批销优惠
            try
            {
                BLL.Shop.Sales.SalesRuleProduct salesRule = new BLL.Shop.Sales.SalesRuleProduct();
                model.AllCartInfo = salesRule.GetWholeSale(model.AllCartInfo);
                model.SelectedCartInfo = salesRule.GetWholeSale(model.SelectedCartInfo);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message), ex.StackTrace, Request);
                return new Result(ResultStatus.Error, ex.Message);
            }
            #endregion
            List<ShoppingCartModel> list = new List<ShoppingCartModel>();
            list.Add(model);
            JsonObject result = new JsonObject();
            if (model.AllCartInfo.Quantity <= 0)
            {
                return new Result(ResultStatus.Success, null);
            }
            result.Put("cartitem", GetProduct(model));
            result.Put("totalPrice", model.SelectedCartInfo.TotalAdjustedPrice.ToString("F"));
            return new Result(ResultStatus.Success, result);
        }

        public JsonArray GetProduct(ShoppingCartModel model)
        {
            JsonArray jsonArray = new JsonArray();
            JsonArray valArray = new JsonArray();
            JsonObject json;
            JsonObject valJson;
            JsonObject result = new JsonObject();
            JsonArray array = new JsonArray();
            foreach (var item in model.AllCartInfo.Items)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("name", item.Name);
                json.Put("pic", Maticsoft.Web.Components.FileHelper.GeThumbImage(item.ThumbnailsUrl, "T150X150_"));
                json.Put("price", item.AdjustedPrice.ToString("F"));
                json.Put("prodNum", item.Quantity);
                if (item.SkuValues != null && item.SkuValues.Length > 0)
                {
                    foreach (string val in item.SkuValues)
                    {
                        valJson = new JsonObject();
                        valJson.Put("value", val);
                        valArray.Add(valJson);
                    }
                }
                json.Put("product_property", valArray);
                result.Put("Product", json);
                jsonArray.Add(result);
            }
            array.Add(jsonArray);
            return array;
        }

        [JsonRpcMethod("UpdateItemCount", Idempotent = false)]
        [JsonRpcHelp("修改数量")]
        //ItemId 主键
        public JsonObject UpdateItemCount(int ItemId, int count, int UserID = -1)
        {
            if (UserID < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            if (String.IsNullOrWhiteSpace(ItemId.ToString()) || String.IsNullOrWhiteSpace(count.ToString()))
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            else
            {
                int itemId = Common.Globals.SafeInt(ItemId, 0);
                int num = Common.Globals.SafeInt(count, 1);
                int userId = UserID;
                Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(userId);
                try
                {
                    cartHelper.UpdateItemQuantity(itemId, count);
                }
                catch (Exception ex)
                {
                    LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message), ex.StackTrace, Request);
                    return new Result(ResultStatus.Error, ex.Message);
                }
                return AddCart(UserID);
            }
        }

        [JsonRpcMethod("RemoveItem", Idempotent = true)]
        [JsonRpcHelp("删除商品")]
        public JsonObject RemoveItem(int ItemId, int UserID = -1)
        {
            if (UserID < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            if (String.IsNullOrWhiteSpace(ItemId.ToString()))
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            else
            {
                Maticsoft.BLL.Shop.Products.ShoppingCartHelper cartHelper = new ShoppingCartHelper(UserID);
                try
                {
                    cartHelper.RemoveItem(ItemId);
                }
                catch (Exception ex)
                {
                    LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message), ex.StackTrace, Request);
                    return new Result(ResultStatus.Error, ex.Message);
                }
                return AddCart(UserID);
            }
        }
    }
}