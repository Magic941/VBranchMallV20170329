using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json.RPC;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.SNS.API
{
    public partial class  SNSHandler
    {
        #region  分类接口
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Pid"></param>
        /// <param name="Type">分类类型 0：表示商品分类 1：表示图片分类  2：表示博客分类</param>
        /// <param name="Top"></param>
        /// <returns></returns>
        [JsonRpcMethod("GetCategoryList", Idempotent = false)]
        [JsonRpcHelp("社区分类")]
        public JsonObject GetCategoryList(int Pid, int Type = 0, int Top = 10)
        {
            List<Maticsoft.Model.SNS.Categories> AllList = Maticsoft.BLL.SNS.Categories.GetAllList(Type);
            List<Maticsoft.Model.SNS.Categories> cateList = AllList.Where(c => c.ParentID == Pid).ToList();
            if (Top > 0)
            {
                cateList = cateList.Take(Top).ToList();
            }
            JsonObject result = new JsonObject();
            if (cateList == null)
            {
                result.Put("status", "fail");
                result.Put("result", "productModel");
                return result;
            }
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            foreach (var item in cateList)
            {
                json = new JsonObject();
                json.Put("cid", item.CategoryId);
                json.Put("name", item.Name);
                json.Put("pid", item.ParentID);
                json.Put("sequence", item.Sequence);
                json.Put("type", item.Type);
                json.Put("desc", item.Description);
                jsonArray.Add(json);
            }
            result.Put("result", jsonArray);
            result.Put("status", "success");
            return result;
        }

        [JsonRpcMethod("GetAllList", Idempotent = false)]
        [JsonRpcHelp("社区所有分类")]
        public JsonObject GetAllList(int Type = 0)
        {
            List<Maticsoft.Model.SNS.Categories> AllList = Maticsoft.BLL.SNS.Categories.GetAllList(Type);
            JsonObject result = new JsonObject();
            if (AllList == null)
            {
                result.Put("status", "fail");
                result.Put("result", "productModel");
                return result;
            }
            List<Maticsoft.Model.SNS.Categories> firstList = AllList.Where(c => c.ParentID == 0).ToList();
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            foreach (var item in firstList)
            {
                json = new JsonObject();
                json.Put("cid", item.CategoryId);
                json.Put("name", item.Name);
                json.Put("pid", item.ParentID);
                json.Put("sequence", item.Sequence);
                json.Put("type", item.Type);
                json.Put("desc", item.Description);
                if (item.HasChildren)
                {
                    ChildList(item.CategoryId, Type, json);
                }
                jsonArray.Add(json);
            }
            result.Put("status", "success");
            result.Put("childlist", jsonArray);
            return result;
        }
        /// <summary>
        /// 递归算法
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="type"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private JsonObject ChildList(int pid, int type, JsonObject result)
        {
            List<Maticsoft.Model.SNS.Categories> AllList = Maticsoft.BLL.SNS.Categories.GetAllList(type);
            List<Maticsoft.Model.SNS.Categories> cateList = AllList.Where(c => c.ParentID == pid).ToList();
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            foreach (var item in cateList)
            {
                json = new JsonObject();
                json.Put("cid", item.CategoryId);
                json.Put("name", item.Name);
                json.Put("pid", item.ParentID);
                json.Put("sequence", item.Sequence);
                json.Put("type", item.Type);
                json.Put("desc", item.Description);
                if (item.HasChildren)
                {
                    ChildList(item.CategoryId, type, json);
                }
                jsonArray.Add(json);
            }
            result.Put("childlist", jsonArray);
            return result;
        }
        #endregion

        #region 商品列表接口
        [JsonRpcMethod("GetProductList", Idempotent = false)]
        [JsonRpcHelp("获取商品图片")]
        public JsonObject GetProductList(int Cid, int Top = 10, bool hasChild = true)
        {
            JsonObject result = new JsonObject();
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            List<Maticsoft.Model.SNS.Products> productList = productBll.GetModelList("");
            result.Put("status", "success");
            return result;
        }
        #endregion

        #region 商品分页接口
        [JsonRpcMethod("GetProPageList", Idempotent = false)]
        [JsonRpcHelp("获取商品图片分页数据")]
        public JsonObject GetProPageList(int Cid, int pageIndex = 1, int pageSize = 10, bool hasChild = true)
        {
            JsonObject result = new JsonObject();
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            Maticsoft.Model.SNS.ProductQuery Query = new Model.SNS.ProductQuery();
            Query.IsTopCategory = hasChild;
            Query.CategoryID = Cid;
            Query.Order = "popular";
            //重置页面索引
            pageIndex = pageIndex > 1 ? pageIndex : 1;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex * pageSize;
            List<Maticsoft.Model.SNS.Products> productList = productBll.GetProductByPage(Query, startIndex, endIndex);
            if (productList == null)
            {
                result.Put("status", "fail");
                result.Put("result", "productModel");
                return result;
            }
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            foreach (var item in productList)
            {
                json = new JsonObject();
                json.Put("productid", item.ProductID);
                json.Put("name", item.ProductName);
                json.Put("cid", item.CategoryID);
                json.Put("producturl", item.ProductUrl);
                json.Put("pic", item.ThumbImageUrl);
                json.Put("userid", item.CreateUserID);
                json.Put("price", item.Price);
                json.Put("nickname", item.CreatedNickName);
                json.Put("favcount", item.FavouriteCount);
                json.Put("pvcount", item.PVCount);
                json.Put("status", item.Status);
                json.Put("comcount", item.CommentCount);
                jsonArray.Add(json);
            }
            result.Put("status", "success");
            result.Put("result", jsonArray);
            return result;
        }
        #endregion

        #region 商品搜素接口

        [JsonRpcMethod("SearchProPageList", Idempotent = false)]
        [JsonRpcHelp("获取商品图片分页数据")]
        public JsonObject SearchProPageList(int Cid, string keyword, string order, int pageIndex = 1, int pageSize = 10, bool hasChild = true)
        {
            JsonObject result = new JsonObject();
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();

            keyword = Common.InjectionFilter.Filter(keyword);
            Maticsoft.ViewModel.SNS.ProductCategory Model = new ViewModel.SNS.ProductCategory();
            Maticsoft.BLL.SNS.Products ProductBll = new BLL.SNS.Products();
            Maticsoft.Model.SNS.ProductQuery Query = new Model.SNS.ProductQuery();
            Query.Order = order;
            Query.Keywords = keyword;
            Query.CategoryID = Cid;
            Query.IsTopCategory = hasChild;
            //重置页面索引
            pageIndex = pageIndex > 1 ? pageIndex : 1;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex * pageSize;
            List<Maticsoft.Model.SNS.Products> productList = productBll.GetProductByPage(Query, startIndex, endIndex);
            if (productList == null)
            {
                result.Put("status", "fail");
                result.Put("result", "productModel");
                return result;
            }
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            foreach (var item in productList)
            {
                json = new JsonObject();
                json.Put("productid", item.ProductID);
                json.Put("name", item.ProductName);
                json.Put("cid", item.CategoryID);
                json.Put("producturl", item.ProductUrl);
                json.Put("pic", item.ThumbImageUrl);
                json.Put("userid", item.CreateUserID);
                json.Put("price", item.Price);
                json.Put("nickname", item.CreatedNickName);
                json.Put("favcount", item.FavouriteCount);
                json.Put("pvcount", item.PVCount);
                json.Put("status", item.Status);
                json.Put("comcount", item.CommentCount);
                jsonArray.Add(json);
            }
            result.Put("result", jsonArray);
            result.Put("status", "success");
            return result;
        }


        #endregion

        #region 商品详情
        [JsonRpcMethod("GetProPageList", Idempotent = false)]
        [JsonRpcHelp("获取商品图片分页数据")]
        public JsonObject ProductDetail(long productid)
        {
            JsonObject result = new JsonObject();
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();

            Maticsoft.Model.SNS.Products productModel = productBll.GetModelByCache(productid);
            if (productModel == null)
            {
                result.Put("status", "fail");
                result.Put("result", "nodata");
                return result;
            }
            JsonObject json = new JsonObject();
            json.Put("productid", productModel.ProductID);
            json.Put("name", productModel.ProductName);
            json.Put("cid", productModel.CategoryID);
            json.Put("producturl", productModel.ProductUrl);
            json.Put("pic", productModel.ThumbImageUrl);
            json.Put("userid", productModel.CreateUserID);
            json.Put("price", productModel.Price);
            json.Put("nickname", productModel.CreatedNickName);
            json.Put("favcount", productModel.FavouriteCount);
            json.Put("pvcount", productModel.PVCount);
            json.Put("status", productModel.Status);
            json.Put("comcount", productModel.CommentCount);
            result.Put("result", json);
            result.Put("status", "success");
            return result;
        }

        #endregion
    }
}