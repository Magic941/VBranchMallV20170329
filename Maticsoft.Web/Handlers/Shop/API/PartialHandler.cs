/**
* PartialHandler.cs
*
* 功 能： Shop 部分类API
* 类 名： PartialHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/17 17:04:23  GW    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.SessionState;
using Maticsoft.Json.RPC.Web;
using Maticsoft.Json.RPC;
using Maticsoft.Json;
using Maticsoft.Web.Handlers.API;
using Maticsoft.Model.Settings;
using System.Collections.Generic;
using Maticsoft.Model.Shop.Products;
using System.Linq;
using System.Text.RegularExpressions;

namespace Maticsoft.Web.Handlers.Shop.API
{
    public partial class ShopHandler : Maticsoft.Web.Handlers.API.HandlerBase
    {
        #region 获取广告位
        [JsonRpcMethod("AdDetail", Idempotent = true)]
        [JsonRpcHelp("根据广告位Id获取广告位")]
        public JsonArray AdDetail(int Aid)
        {
            Maticsoft.BLL.Settings.Advertisement bll = new Maticsoft.BLL.Settings.Advertisement();
            List<Advertisement> list = bll.GetListByAidCache(Aid);
            Json.JsonArray array = new JsonArray();
            JsonObject json;
            JsonObject result = new JsonObject();
            if (list == null)
            {
                return null;
            }
            foreach (Advertisement item in list)
            {
                json = new JsonObject();
                json.Put("id",item.AdvertisementId);
                json.Put("title",item.AlternateText);
                json.Put("pic",item.FileUrl);
                json.Put("url", item.NavigateUrl);
                array.Add(json);
            }
            return array;
        }
        #endregion

        [JsonRpcMethod("HomeProductList", Idempotent = false)]
        [JsonRpcHelp("产品列表")]
        public JsonObject ProductList(int Cid, int IndexRec, int Top = 10)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = cateList.FirstOrDefault(c => c.CategoryId == Cid);
            JsonObject result = new JsonObject();
            JsonArray productArray = new JsonArray();
            if (categoryInfo == null)
            {
                return null;
            }
            Json.JsonArray jsonArray = new JsonArray();
            result.Put("key", categoryInfo.Name);
            
            Maticsoft.Model.Shop.Products.ProductRecType iIndexRec = Common.Globals.SafeEnum<Maticsoft.Model.Shop.Products.ProductRecType>(IndexRec.ToString(), ProductRecType.IndexRec, true);
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(iIndexRec, Cid, Top);
            if (productList == null)
            {
                return null;
            }
            JsonObject json;
            foreach (ProductInfo item in productList)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("title", item.ProductName);
                json.Put("pic", Maticsoft.Web.Components.FileHelper.GeThumbImage(item.ThumbnailUrl1, "T175X228_"));
                json.Put("saleprice", item.LowestSalePrice.ToString("F"));
                jsonArray.Add(json);
            }
            result.Put("value", jsonArray);
            //productArray.Add(result);
            return result;
        }

        [JsonRpcMethod("HomeCategoryList", Idempotent = false)]
        [JsonRpcHelp("分类请求列表-左菜单")]
        public JsonArray Category(int parentId = 0, int Top = 10)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.FindAll(c => c.ParentCategoryId == parentId);

            JsonObject json;
            Json.JsonArray array = new JsonArray();
            JsonObject result = new JsonObject();
            foreach (CategoryInfo item in categoryInfos)
            {
                json = new JsonObject();
                json.Put("id", item.CategoryId);
                json.Put("name", item.Name);
                //json.Put("isleadnode", item.HasChildren);
                //json.Put("parentId", parentId);
                json.Put("pic", item.ImageUrl);
                //json.Put("tag", item.Path);
                array.Add(json);
            }
            return array;
        }

        #region 首页
        [JsonRpcMethod("HomeIndex", Idempotent = false)]
        [JsonRpcHelp("首页")]
        public JsonObject HomeIndex(int aid, string cid, int IndexRec, int parentId, int top)
        {
            string cidProduct = "";
            #region
            if (aid < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            if (cid == null)
            {
                cidProduct = "2,61,31";
            }
            else
            {
                MatchCollection mc = Regex.Matches(cid, @"\d+");
                foreach (Match item in mc)
                {
                    cidProduct += item.Value + ",";
                }
            }
            string[] pro = cidProduct.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (IndexRec < 0)
            {
                IndexRec = 0;
            }
            if (parentId < 0)
            {
                parentId = 0;
            }
            if (top < 1)
            {
                top = 10;
            }
            #endregion
            JsonObject result = new JsonObject();
            JsonArray adArray = AdDetail(aid);
            if (adArray == null || adArray.Length < 1)
            {
                result.Put("home_banner", null);
            }
            else
            {
                result.Put("home_banner", adArray);
            }
            JsonArray cateArray = Category(parentId,top);
            result.Put("leftmenu", cateArray);
            JsonArray prArray=null;
            JsonArray array = new JsonArray();
            JsonObject resultProduct;
            for (int i = 0; i < pro.Length; i++)
            {
                resultProduct = ProductList(Common.Globals.SafeInt(pro[i], 0), IndexRec, top);
                if(resultProduct!=null&&resultProduct.Count>0)
                {
                    prArray = new JsonArray();
                    array.Add(resultProduct);
                }
            }
            result.Put("galleryproduct", array);
            return new Result(ResultStatus.Success,result);
        }
        #endregion
    }
}