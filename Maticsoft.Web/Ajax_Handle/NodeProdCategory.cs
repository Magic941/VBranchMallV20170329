/**
* NodeProdCategory.cs
*
* 功 能： [N/A]
* 类 名： NodeProdCategory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/20 17:01:59  Administrator    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using Maticsoft.Json;
using Maticsoft.BLL.CMS;
using Maticsoft.Common;

namespace Maticsoft.Web.Ajax_Handle
{
    public class NodeProdCategory : IHttpHandler
    {
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryBLL =new BLL.Shop.Products.CategoryInfo();

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string Action = context.Request.Params["action"];
            switch (Action)
            {
                case "GetChildNode":
                    GetChildNode(context);
                    break;
                case "GetDepthNode":
                    GetDepthNode(context);
                    break;
                case "GetParentNode":
                    GetParentNode(context);
                    break;
                default:
                    break;
            }
        }

        private void GetChildNode(HttpContext context)
        {
            string categoryId = context.Request.Params["CategoryId"];
            JsonObject json = new JsonObject();

            DataSet dsParent = categoryBLL.GetList("ParentCategoryId=" + (string.IsNullOrWhiteSpace(categoryId) ? "-1" : categoryId));
            if (dsParent.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate("DATA", dsParent.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet dsDepth;
            if (nodeId > 0)
            {
                Model.Shop.Products.CategoryInfo categoryModel = categoryBLL.GetModel(nodeId);
                dsDepth = categoryBLL.GetList("Depth=" + categoryModel.Depth);
            }
            else
            {
                dsDepth = categoryBLL.GetList("Depth=1");
            }
            if (dsDepth.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate("DATA", dsDepth.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = categoryBLL.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Model.Shop.Products.CategoryInfo categoryModel = categoryBLL.GetModel(ParentId);
                if (categoryModel != null)
                {
                    string[] strList = categoryModel.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        foreach (string str in strList)
                        {
                            DataRow[] dsParent = dt.Select(" ParentCategoryId=" + str);
                            list.Add(dsParent);
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
    }
}