/**
* CMSContentHandle.cs
*
* 功 能： [N/A]
* 类 名： CMSContentHandle
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/27 13:12:07  Rock    初版
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
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Common;

namespace Maticsoft.Web.AjaxHandle
{
    public class NodeHandle : IHttpHandler
    {
        private PhotoClass photoBLL = new PhotoClass();
        private GoodsType goodBll = new GoodsType();

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
                case "GetWeixinChildNode":
                    GetWeixinChildNode(context);
                    break;
                case "GetWeixinDepthNode":
                    GetWeixinDepthNode(context);
                    break;
                case "GetWeixinParentNode":
                    GetWeixinParentNode(context);
                    break;
                default:
                    break;
            }
        }

        private void GetChildNode(HttpContext context)
        {
            string parentId = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();

            DataSet dsParent = photoBLL.GetList("ParentId=" + (string.IsNullOrWhiteSpace(parentId) ? "0" : parentId));
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
        /// <summary>
        /// 微信商品分类
        /// </summary>
        /// <param name="context"></param>
        private void GetWeixinChildNode(HttpContext context)
        {
            string parentId = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();

            DataSet dsParent = goodBll.GetList("PID=" + (string.IsNullOrWhiteSpace(parentId) ? "0" : parentId));
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
                Model.CMS.PhotoClass photoClass = photoBLL.GetModel(nodeId);
                dsDepth = photoBLL.GetList("Depth=" + photoClass.Depth);
            }
            else
            {
                dsDepth = photoBLL.GetList("Depth=1");
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

        /// <summary>
        /// 微信商品分类
        /// </summary>
        /// <param name="context"></param>
        private void GetWeixinDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet dsDepth;
            if (nodeId > 0)
            {
                Model.Shop.Products.GoodsType goodmodel = goodBll.GetModel(nodeId);
                dsDepth = goodBll.GetList("PID=" + goodmodel.PID);
            }
            else
            {
                dsDepth = goodBll.GetList("PID=0");
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
            DataSet ds = photoBLL.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Model.CMS.PhotoClass photoClass = photoBLL.GetModel(ParentId);
                if (photoClass != null)
                {

                    string[] strList = photoClass.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        foreach (string str in strList)
                        {
                            DataRow[] dsParent = dt.Select("ParentId=" + str);
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

        /// <summary>
        /// 微信商品分类
        /// </summary>
        /// <param name="context"></param>
        private void GetWeixinParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = goodBll.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Model.Shop.Products.GoodsType goodmodel = goodBll.GetModel(ParentId);
                if (goodmodel != null)
                {

                    string[] strList = goodmodel.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        foreach (string str in strList)
                        {
                            DataRow[] dsParent = dt.Select("PID=" + str);
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