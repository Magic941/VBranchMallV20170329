using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;
using Maticsoft.Common;
using System.Data;

namespace Maticsoft.Web.Handlers.SNS
{
    public class CategoriesHandler : HandlerBase
    {
        //
        // GET: /SNSCat/

        //定义一个全局变量(方便构造树形节点使用)
        public List<Maticsoft.Model.SNS.Categories> CategoriesList;

        #region IHttpHandler 成员

        public override bool IsReusable
        {
            get { return false; }
        }

        public override void ProcessRequest(HttpContext context)
        {
            //安全起见, 所有产品相关Ajax请求为POST模式
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";

            try
            {
                switch (action)
                {
                    #region 社区商品分类

                    case "GetSNSChildNode":
                        GetSNSChildNode(context);
                        break;

                    case "GetSNSDepthNode":
                        GetSNSDepthNode(context);
                        break;

                    case "GetSNSParentNode":
                        GetSNSParentNode(context);
                        break;

                    case "IsExistedSNSCate":
                        IsExistedSNSCate(context);
                        break;
                    case "GetSNSProductNodes":
                        GetSNSProductNodes(context);
                        break;
                    case "SetCategory":
                        SetCategory(context);
                        break;
                    case "GetCategoryInfo":
                        GetCategoryInfo(context);
                        break;
                    case "DeleteCategory":
                        DeleteCategory(context);
                        break;
                    #endregion 社区商品分类

                    #region 淘宝商品分类

                    case "GetTaoBaoChildNode":
                        GetTaoBaoChildNode(context);
                        break;

                    case "GetTaoBaoDepthNode":
                        GetTaoBaoDepthNode(context);
                        break;

                    case "GetTaoBaoParentNode":
                        GetTaoBaoParentNode(context);
                        break;

                    case "IsExistedTaoBaoCate":
                        IsExistedTaoBaoCate(context);
                        break;

                    #endregion 淘宝商品分类

                    #region 社区图片分类

                    case "GetPhotoChildNode":
                        GetPhotoChildNode(context);
                        break;

                    case "GetPhotoDepthNode":
                        GetPhotoDepthNode(context);
                        break;

                    case "GetPhotoParentNode":
                        GetPhotoParentNode(context);
                        break;

                    case "IsExistedPhotoCate":
                        IsExistedPhotoCate(context);
                        break;

                    #endregion 社区商品分类

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                json.Put(KEY_STATUS, STATUS_ERROR);
                json.Put(KEY_DATA, ex);
                context.Response.Write(json.ToString());
            }
        }

        #endregion IHttpHandler 成员

        #region 社区商品分类

        private Maticsoft.BLL.SNS.Categories SNSCateBll = new BLL.SNS.Categories();
        private Maticsoft.BLL.SNS.Products ProductsBll = new BLL.SNS.Products();
        private void IsExistedSNSCate(HttpContext context)
        {
            string CategoryIdStr = context.Request.Params["CategoryId"];
            int cateId = Globals.SafeInt(CategoryIdStr, -2);
            JsonObject json = new JsonObject();
            if (SNSCateBll.IsExistedCate(cateId))
            {
                json.Put(KEY_STATUS, STATUS_SUCCESS);
            }
            else
            {
                json.Put(KEY_STATUS, STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void GetSNSChildNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = SNSCateBll.GetCategorysByParentId(parentId);
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate(KEY_DATA, ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetSNSDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.SNS.Categories> list;
            if (nodeId > 0)
            {
                Maticsoft.Model.SNS.Categories model = SNSCateBll.GetModel(nodeId);
                list = SNSCateBll.GetCategorysByDepth(model.Depth, 0);
            }
            else
            {
                list = SNSCateBll.GetCategorysByDepth(1, 0);
            }
            if (list.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.CategoryId, info.Name }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }

        private void GetSNSParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = SNSCateBll.GetList("Status=1 and Type=0");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Maticsoft.Model.SNS.Categories model = SNSCateBll.GetModel(ParentId);
                if (model != null)
                {
                    string[] strList = model.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strList.Length; i++)
                        {
                            DataRow[] dsParent = null;
                            if (i == 0)
                            {
                                dsParent = dt.Select("ParentId=0");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentId=" + strList[i-1]);
                            }
                            if (dsParent.Length > 0)
                            {
                                list.Add(dsParent);
                            }
                        }
                        json.Accumulate("STATUS", "OK");
                        json.Accumulate("DATA", list);
                        json.Accumulate("PARENT", strList);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NODATA");
                        context.Response.Write(json.ToString());
                        return;
                    }
                }
            }

            context.Response.Write(json.ToString());
        }

        public void GetCategoryInfo(HttpContext context)
        {
            string categoryId = context.Request.Params["CID"];
            int type = Common.Globals.SafeInt(context.Request.Params["Type"], 0);
            JsonObject json = new JsonObject();
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                //先获取全部的分类
                List<Maticsoft.Model.SNS.Categories> AllCateList = SNSCateBll.GetAllCateByCache(type);
                List<Maticsoft.Model.SNS.Categories> list =
                    AllCateList.Where(c => c.ParentID == Globals.SafeInt(categoryId, 0)).ToList();
                    //SNSCateBll.GetCategoryList(Globals.SafeInt(categoryId, 0), type);

                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(info => data.Add(new JsonObject(new string[] { "CategoryId", "Name", "ParentID", "HasChildren" }, new object[] { info.CategoryId, info.Name, info.ParentID, info.HasChildren})));

                    json.Put("STATUS", "Success");
                    json.Put("DATA", data);
                }
                else
                {
                    json.Put("STATUS", "Fail");
                }
            }
            else
            {
                json.Put("STATUS", "Error");
            }
            context.Response.Write(json.ToString());
        }

        private void DeleteCategory(HttpContext context)
        {
            string categoryId = context.Request.Params["CID"];
            JsonObject json = new JsonObject();
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                SNSCateBll.DeleteCategory(Common.Globals.SafeInt(categoryId,0));

                json.Put("STATUS", "Success");
            }
            else
            {
                json.Put("STATUS", "Error");
            }
            context.Response.Write(json.ToString());
        }
        //private void SwaoSequence(HttpContext context)
        //{
        //    string fromId = context.Request.Params["FID"];
        //    string toId = context.Request.Params["TID"];
        //    JsonObject json = new JsonObject();
        //    if (!string.IsNullOrWhiteSpace(fromId) && !string.IsNullOrWhiteSpace(toId))
        //    {
        //        if (SNSCateBll.SwapSequence(Globals.SafeInt(fromId, 0), Globals.SafeInt(toId, 0)))
        //        {
        //            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
        //        }
        //        else
        //        {
        //            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
        //        }
        //    }
        //    else
        //    {
        //        json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
        //    }
        //    context.Response.Write(json.ToString());
        //}


        private void GetSNSProductNodes(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            CategoriesList = new List<Model.SNS.Categories>();
            //首先获取顶级的分类
            List<Maticsoft.Model.SNS.Categories> parentlist = SNSCateBll.GetCategorysByDepth(1, 0);

            if (parentlist.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            //构造树形结构
            foreach (var item in parentlist)
            {
                item.Name = "╋" + item.Name;
                CategoriesList.Add(item);
                string blank = "├";
                BindNode(item.CategoryId, blank);
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            CategoriesList.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.CategoryId, info.Name }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }
        //构造节点
        private void BindNode(int parentid, string blank)
        {
            List<Maticsoft.Model.SNS.Categories> list = SNSCateBll.GetListByParentId(parentid);

            foreach (var item in list)
            {
                //string permissionid=r["PermissionID"].ToString();
                item.Name = blank + "『" + item.Name + "』";
                string blank2 = blank + "─";
                CategoriesList.Add(item);
                BindNode(item.CategoryId, blank2);
            }
        }

        private void SetCategory(HttpContext context)
        {
            int ProductId = Globals.SafeInt(context.Request.Params["ProductId"], 0);
            int CategoryId = Globals.SafeInt(context.Request.Params["CategoryId"], 0);
            JsonObject json = new JsonObject();
            if (ProductId > 0 && CategoryId > 0)
            {
                if (ProductsBll.UpdateEX(ProductId, CategoryId))
                {
                    json.Accumulate("STATUS", "OK");
                }
            }
            context.Response.Write(json.ToString());
        }
        #endregion 社区商品分类

        #region 淘宝商品分类

        private Maticsoft.BLL.SNS.CategorySource taoBaoCateBll = new BLL.SNS.CategorySource();

        private void IsExistedTaoBaoCate(HttpContext context)
        {
            string CategoryIdStr = context.Request.Params["CategoryId"];
            int cateId = Globals.SafeInt(CategoryIdStr, -2);
            JsonObject json = new JsonObject();
            if (taoBaoCateBll.IsExistedCate(cateId))
            {
                json.Put(KEY_STATUS, STATUS_SUCCESS);
            }
            else
            {
                json.Put(KEY_STATUS, STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void GetTaoBaoChildNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = taoBaoCateBll.GetCategorysByParentId(parentId);
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate(KEY_DATA, ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetTaoBaoDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.SNS.CategorySource> list;
            if (nodeId > 0)
            {
                Maticsoft.Model.SNS.CategorySource model = taoBaoCateBll.GetModel(3, nodeId);
                list = taoBaoCateBll.GetCategorysByDepth(model.Depth);
            }
            else
            {
                list = taoBaoCateBll.GetCategorysByDepth(1);
            }
            if (list.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.CategoryId, info.Name }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }

        private void GetTaoBaoParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = taoBaoCateBll.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Maticsoft.Model.SNS.CategorySource model = taoBaoCateBll.GetModel(3, ParentId);
                if (model != null)
                {
                    string[] strList = model.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strList.Length; i++)
                        {
                            DataRow[] dsParent = null;
                            if (i == 0)
                            {
                                dsParent = dt.Select("ParentId=0");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentId=" + strList[i-1]);
                            }
                            if (dsParent.Length > 0)
                            {
                                list.Add(dsParent);
                            }
                        }
                        json.Accumulate("STATUS", "OK");
                        json.Accumulate("DATA", list);
                        json.Accumulate("PARENT", strList);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NODATA");
                        context.Response.Write(json.ToString());
                        return;
                    }
                }
            }

            context.Response.Write(json.ToString());
        }

        #endregion 淘宝商品分类

        #region 社区图片分类

        private Maticsoft.BLL.SNS.Categories PhotoCateBll = new BLL.SNS.Categories();

        private void IsExistedPhotoCate(HttpContext context)
        {
            string CategoryIdStr = context.Request.Params["CategoryId"];
            int cateId = Globals.SafeInt(CategoryIdStr, -2);
            JsonObject json = new JsonObject();
            if (PhotoCateBll.IsExistedCate(cateId))
            {
                json.Put(KEY_STATUS, STATUS_SUCCESS);
            }
            else
            {
                json.Put(KEY_STATUS, STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void GetPhotoChildNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = PhotoCateBll.GetCategorysByParentId(parentId);
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate(KEY_DATA, ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetPhotoDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.SNS.Categories> list;
            if (nodeId > 0)
            {
                Maticsoft.Model.SNS.Categories model = PhotoCateBll.GetModel(nodeId);
                list = PhotoCateBll.GetCategorysByDepth(model.Depth, 1);
            }
            else
            {
                list = PhotoCateBll.GetCategorysByDepth(1, 1);
            }
            if (list.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.CategoryId, info.Name }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }

        private void GetPhotoParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = PhotoCateBll.GetList(" Type=1 and Status=1");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Maticsoft.Model.SNS.Categories model = PhotoCateBll.GetModel(ParentId);
                if (model != null)
                {
                    string[] strList = model.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strList.Length; i++)
                        {
                            DataRow[] dsParent = null;
                            if (i == 0)
                            {
                                dsParent = dt.Select("ParentId=0");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentId=" + strList[i-1]);
                            }
                            if (dsParent.Length > 0)
                            {
                                list.Add(dsParent);
                            }
                        }
                        json.Accumulate("STATUS", "OK");
                        json.Accumulate("DATA", list);
                        json.Accumulate("PARENT", strList);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NODATA");
                        context.Response.Write(json.ToString());
                        return;
                    }
                }
            }

            context.Response.Write(json.ToString());
        }

        #endregion 社区图片分类


        
    }
}
