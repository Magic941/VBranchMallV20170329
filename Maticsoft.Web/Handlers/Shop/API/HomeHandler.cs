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
using System.Collections.Generic;
using Maticsoft.Model.Shop.Products;
using System.Linq;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Model.Shop.Order;
using Maticsoft.BLL.Shop.Order;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Model.CMS;

namespace Maticsoft.Web.Handlers.Shop.API
{
    public partial class ShopHandler
    {
        BLL.CMS.Content contentBll = new BLL.CMS.Content();
        BLL.CMS.ContentClass contentclassBll = new BLL.CMS.ContentClass();
        private BLL.Shop.Products.ProductReviews reviewsBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
        private readonly Orders _orderManage = new Orders();
        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        



        [JsonRpcMethod("SearchProductList", Idempotent = false)]
        [JsonRpcHelp("搜索商品列表")]
        public JsonObject SearchProductList(int cid = 0, int brandid = 0, string keyword = "", string orderby = "hot", string price = "0-0",
                                        int? page = 1, int pageNum = 10)
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
            keyword = Maticsoft.Common.InjectionFilter.SqlFilter(keyword);
            if (cid > 0)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo =
                    cateList.FirstOrDefault(c => c.CategoryId == cid);
                if (categoryInfo != null)
                {
                    var path_arr = categoryInfo.Path.Split('|');
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> categorysPath =
                        cateList.Where(c => path_arr.Contains(c.CategoryId.ToString())).OrderBy(c => c.Depth)
                                .ToList();
                    model.CategoryPathList = categorysPath;
                    model.CurrentCateName = categoryInfo.Name;
                }
            }
            model.CurrentCid = cid;
            model.CurrentMod = orderby;

            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pageNum + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value > 1 ? startIndex + pageNum - 1 : pageNum;
            int toalCount = productManage.GetSearchCountEx(cid, brandid, keyword, price);
            JsonObject result = new JsonObject();
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            //ViewBag.TotalCount = toalCount;
            result.Put("list_count", toalCount);
            List<Model.Shop.Products.ProductInfo> list;
            try
            {
                if (String.IsNullOrEmpty(orderby))
                {
                    orderby = "default";
                }
                if (orderby.ToString().ToLower() != "default" && orderby.ToString().ToLower() != "hot" && orderby.ToString().ToLower() != "new" && orderby.ToString().ToLower() != "price" && orderby.ToString().ToLower() != "pricedesc")
                {

                    return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
                }
                list = productManage.GetSearchListEx(cid, brandid, keyword, price, orderby, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message),
                   ex.StackTrace, Request);
                return new Result(ResultStatus.Error, ex.Message);
            }
            //获取总条数
            if (toalCount < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            if (list == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            foreach (ProductInfo item in list)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("title", item.ProductName);
                json.Put("pic", Maticsoft.Web.Components.FileHelper.GeThumbImage(item.ThumbnailUrl1, "T175X228_"));
                json.Put("marketprice", item.MarketPrice.HasValue ? item.MarketPrice.Value.ToString("F") : "0.00");
                json.Put("saleprice", item.LowestSalePrice.ToString("F"));
                json.Put("commentCount", reviewsBll.GetRecordCount("Status=1 and ProductId=" + item.ProductId));
                jsonArray.Add(json);
            }
            result.Put("productlist", jsonArray);
            return new Result(ResultStatus.Success, result);
        }

        [JsonRpcMethod("HotKeyword", Idempotent = false)]
        [JsonRpcHelp("热门搜索")]
        public JsonObject HotKeyword(int Cid = 0, int Top = 30)
        {
            if(Top==0)
            {
                Top = 10;
            }
            Maticsoft.BLL.Shop.Products.HotKeyword keywordBll = new Maticsoft.BLL.Shop.Products.HotKeyword();
            List<Maticsoft.Model.Shop.Products.HotKeyword> keywords = keywordBll.GetKeywordsList(Cid, Top);
            if (keywords == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonArray result = new JsonArray();
            foreach (HotKeyword item in keywords)
            {
                //json = new JsonObject();
                //json.Put("keywords", item.Keywords);
                //result.Add(json);
                result.Add(item.Keywords.ToString());
            }
            return new Result(ResultStatus.Success, result);
        }

        [JsonRpcMethod("ProductRec", Idempotent = false)]
        [JsonRpcHelp("促销商品")]
        public JsonObject ProductRec(int IndexRec, int Cid = 0, int Top = 5)
        {
            if (Top == 0)
            {
                Top = 10;
            }
            ProductRecType Type = Common.Globals.SafeEnum<Maticsoft.Model.Shop.Products.ProductRecType>(IndexRec.ToString(), ProductRecType.IndexRec, true);
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(Type, Cid, Top);
            List<Maticsoft.Model.Shop.Products.ProductInfo> prolist = new List<Model.Shop.Products.ProductInfo>();
            if (productList == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonArray result = new JsonArray();
            JsonObject json;
            foreach (var item in productList)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("name", item.ProductName);
                json.Put("pic", Maticsoft.Web.Components.FileHelper.GeThumbImage(item.ThumbnailUrl1, "T175X228_"));
                result.Add(json);
            }
            return new Result(ResultStatus.Success, result);
        }


        [JsonRpcMethod("ShakeProduct", Idempotent = false)]
        [JsonRpcHelp("摇摇")]
        public JsonObject ShakeProduct(int IndexRec, int Cid = 0, int Top = 1)
        {
            ProductRecType Type = Common.Globals.SafeEnum<Maticsoft.Model.Shop.Products.ProductRecType>(IndexRec.ToString(), ProductRecType.IndexRec, true);
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRanListByRec(Type, Cid, Top);
            List<Maticsoft.Model.Shop.Products.ProductInfo> prolist = new List<Model.Shop.Products.ProductInfo>();
            if (productList == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonArray result = new JsonArray();
            JsonObject json;
            foreach (var item in productList)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("title", item.ProductName);
                json.Put("pic", Components.FileHelper.GeThumbImage(item.ThumbnailUrl1, "T175X228_"));
                json.Put("marketprice", item.MarketPrice.HasValue?item.MarketPrice.Value.ToString("F"):"0.00");
                json.Put("saleprice", item.LowestSalePrice.ToString("F"));
                result.Add(json);
            }
            return new Result(ResultStatus.Success, result);
        }

       

        [JsonRpcMethod("ProductList", Idempotent = false)]
        [JsonRpcHelp("商品列表")]
        public JsonObject ProductList(int cId = 0, int brandid = 0, string attrvalues = "0", string orderby = "default", string price = "",
                                  int? page = 1, int pageNum = 10)
        {
            #region
            if (pageNum == 0)
            {
                pageNum = 10;
            }
            if (String.IsNullOrEmpty(page.ToString()))
            {
                page = 1;
            }
            if (String.IsNullOrEmpty(orderby))
            {
                orderby = "default";
            }
            ProductListModel model = new ProductListModel();
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = null;
            if (cId > 0)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                categoryInfo = cateList.FirstOrDefault(c => c.CategoryId == cId);
                if (categoryInfo != null)
                {
                    var path_arr = categoryInfo.Path.Split('|');
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> categorysPath =
                        cateList.Where(c => path_arr.Contains(c.CategoryId.ToString())).OrderBy(c => c.Depth)
                                .ToList();
                    model.CategoryPathList = categorysPath;
                    model.CurrentCateName = categoryInfo.Name;
                }
            }
            model.CurrentCid = cId;
            model.CurrentMod = orderby;
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pageNum + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value > 1 ? startIndex + pageNum - 1 : pageNum;
            int toalCount = productManage.GetProductsCountEx(cId, brandid, attrvalues, price);
            JsonObject result = new JsonObject();
            JsonArray jsonArray = new JsonArray();
            JsonObject json;
            List<Maticsoft.Model.Shop.Products.ProductInfo> prolist = new List<Model.Shop.Products.ProductInfo>();
            result.Put("list_count", toalCount);
            List<Model.Shop.Products.ProductInfo> list;
            try
            {
                if (String.IsNullOrEmpty(orderby))
                {
                    return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
                }
                if (orderby.ToString().ToLower() != "default" && orderby.ToString().ToLower() != "hot" && orderby.ToString().ToLower() != "new" && orderby.ToString().ToLower() != "price" && orderby.ToString().ToLower() != "pricedesc")
                {
                    //orderby = "default";
                    return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
                }
                list = productManage.GetProductsListEx(cId, brandid, attrvalues, price, orderby, startIndex, endIndex);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message),
                   ex.StackTrace, Request);
                return new Result(ResultStatus.Error, ex.Message);
            }
            //获取总条数
            if (toalCount < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            if (list == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            foreach (ProductInfo item in list)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("title", item.ProductName);
                json.Put("pic", Maticsoft.Web.Components.FileHelper.GeThumbImage(item.ThumbnailUrl1, "T175X228_"));
                json.Put("marketprice", item.MarketPrice.HasValue?item.MarketPrice.Value.ToString("F"):"0.00");
                json.Put("saleprice", item.LowestSalePrice.ToString("F"));
                json.Put("commentCount", reviewsBll.GetRecordCount("Status=1 and ProductId=" + item.ProductId));
                jsonArray.Add(json);
            }
            result.Put("productlist", jsonArray);
            #endregion
            JsonObject attrJson;
            JsonArray jsonAttrKey = new JsonArray();
            Maticsoft.BLL.Shop.Products.AttributeInfo attributeBll = new BLL.Shop.Products.AttributeInfo();
            List<Maticsoft.Model.Shop.Products.AttributeInfo> attributeInfo = attributeBll.GetAttributeListByCateID(cId, true);
            foreach (AttributeInfo item in attributeInfo)
            {

                attrJson = new JsonObject();
                attrJson.Put("key", item.AttributeName);
                attrJson.Put("values", GetValueInfo(item.AttributeId.ToString()));
                jsonAttrKey.Add(attrJson);
            }
            result.Put("list_filter", jsonAttrKey);
            return new Result(ResultStatus.Success, result);
        }

        public JsonArray GetValueInfo(string attributeId)
        {
            JsonObject attrJson;
            JsonArray array = new JsonArray();
            Maticsoft.BLL.Shop.Products.AttributeValue valueBll = new BLL.Shop.Products.AttributeValue();
            List<Maticsoft.Model.Shop.Products.AttributeValue> valueInfo = valueBll.GetModelList(" AttributeId=" + attributeId);
            foreach (AttributeValue value in valueInfo)
            {
                attrJson = new JsonObject();
                attrJson.Put("id", value.ValueId);
                attrJson.Put("name", value.ValueStr);
                array.Add(attrJson);
            }
            return array;
        }



        [JsonRpcMethod("CategoryList", Idempotent = false)]
        [JsonRpcHelp("类别列表")]
        public JsonObject CategoryList(int Cid, int Top = 7)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.FindAll(c => c.ParentCategoryId == Cid);
            if (categoryInfos == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            JsonObject baseJson;
            JsonArray result = new JsonArray();
            foreach (CategoryInfo item in categoryInfos)
            {
                baseJson = new JsonObject();
                baseJson.Put("id", item.CategoryId);
                baseJson.Put("title", item.Name);
                baseJson.Put("haschild", item.HasChildren);
                baseJson.Put("description", item.Description);
                baseJson.Put("pic", item.ImageUrl);
                if (item.HasChildren == true)
                {
                    baseJson.Put("childlist", GetCategory(item.CategoryId, cateList));
                }
                result.Add(baseJson);
            }
            return new Result(ResultStatus.Success, result);
        }
        public JsonArray GetCategory(int parentId, List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList)
        {
            JsonObject currjson;
            JsonArray array = new JsonArray();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.FindAll(info => info.ParentCategoryId == parentId);
            if (categoryInfos != null && categoryInfos.Count() > 0)
            {
                foreach (Maticsoft.Model.Shop.Products.CategoryInfo item in categoryInfos)
                {
                    currjson = new JsonObject();
                    currjson.Put("id", item.CategoryId);
                    currjson.Put("haschild", item.HasChildren);
                    currjson.Put("parentId", item.ParentCategoryId);
                    currjson.Put("title", item.Name);
                    currjson.Put("pic", item.ImageUrl);
                    if (item.HasChildren == true)
                    {
                        currjson.Put("childlist", GetCategory(item.CategoryId, cateList));
                    }
                    array.Add(currjson);
                }
            }
            return array;
        }

        [JsonRpcMethod("HelpList", Idempotent = false)]
        [JsonRpcHelp("帮助列表")]
        public JsonObject HelpList(int classid)
        {
            if (classid < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            Model.CMS.ContentClass classmodel;
            JsonObject json;
            JsonArray result = new JsonArray();
            List<Maticsoft.Model.CMS.ContentClass> list = contentclassBll.GetModelList(classid, out  classmodel);
            if (list == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            foreach (ContentClass item in list)
            {
                json = new JsonObject();
                json.Put("id", item.ClassID);
                json.Put("title", item.ClassName);
                json.Put("childlist", ContentTitleList(item.ClassID));
                result.Add(json);
            }
            return new Result(ResultStatus.Success, result);
        }
        //文章列表
        public JsonArray ContentTitleList(int classid)
        {

            JsonObject json;
            JsonArray array = new JsonArray();
            List<Maticsoft.Model.CMS.Content> list = contentBll.GetModelList(classid, 0);

            foreach (Content item in list)
            {
                json = new JsonObject();
                json.Put("parentid", item.ClassID);
                json.Put("title", item.Title);
                array.Add(json);
            }
            return array;
        }

        [JsonRpcMethod("HelpDetail", Idempotent = false)]
        [JsonRpcHelp("帮助内容")]
        public JsonObject HelpDetail(int classid)
        {
            if (classid < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            JsonObject json;
            JsonArray array = new JsonArray();
            JsonObject result = new JsonObject();
            List<Maticsoft.Model.CMS.Content> list = contentBll.GetModelList("ContentID=" + classid);
            if (list == null)
            {
                return new Result(ResultStatus.Success, null);
            }
            foreach (Content item in list)
            {
                json = new JsonObject();

                json.Put("title", item.Title);
                json.Put("content", item.Description);
                array.Add(json);
            }
            result.Put("help", array);
            return new Result(ResultStatus.Success, result);
        }
    }
}