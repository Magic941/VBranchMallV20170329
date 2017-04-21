/**
* RegionHandle.cs
*
* 功 能： [N/A]
* 类 名： RegionHandle
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/05/27 13:12:07  Rock    初版
* V0.02  2014/02/10 19:35:00  Ben     新增区域数据接口
*
* Copyright (c) 2014 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Data;
using System.Web;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers
{
    public class RegionHandle : IHttpHandler
    {
        private Maticsoft.BLL.Ms.Regions RegionBll = new BLL.Ms.Regions();

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
                case "GetAreaNode":
                    GetAreaNode(context);
                    break;
                case "GetAreaString":
                    GetAreaString(context);
                    break;
                default:
                    break;
            }
        }

        #region 获取省市县
        string addres = string.Empty;
        public void GetAreaString(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            
            DataSet ds = RegionBll.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Model.Ms.Regions Model = RegionBll.GetModel(ParentId);
                
                if (Model != null)
                {
                    List<DataRow[]> list = new List<DataRow[]>();
                    string[] strList = Model.Path.TrimEnd(',').Split(',');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        for (int i = 0; i < strList.Length; i++)
                        {
                             //根据Code查找对应的名字
                            if (strList[i] != "0")
                            {
                                Model.Ms.Regions Value = RegionBll.GetModel(int.Parse(strList[i]));
                                addres += Value.RegionName;
                            }
                            else {
                                if (strList.Length == 1)
                                {
                                    addres = Model.RegionName;
                                }
                            }
                            if (i==strList.Length-1&&strList.Length!=1)
                            {
                                addres += Model.RegionName;
                            }
                        }
                        
                    }
                }
            }

            context.Response.Write(addres);
        }
        #endregion

        #region GetChildNode
        private void GetChildNode(HttpContext context)
        {
            string parentId = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();

            DataSet dsParent = RegionBll.GetRegionName((string.IsNullOrWhiteSpace(parentId) ? "0" : parentId));
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
        #endregion

        #region GetDepthNode
        private void GetDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet dsDepth;
            if (nodeId > 0)
            {
                Model.Ms.Regions Model = RegionBll.GetModel(nodeId);
                dsDepth = RegionBll.GetList("Depth=" + Model.Depth);
            }
            else
            {
                dsDepth = RegionBll.GetList("Depth=1");
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
        #endregion

        #region GetParentNode
        private void GetParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = RegionBll.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Model.Ms.Regions Model = RegionBll.GetModel(ParentId);
                if (Model != null)
                {

                    string[] strList = Model.Path.TrimEnd(',').Split(',');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        foreach (string str in strList)
                        {
                            DataRow[] dsParent = null;
                            if (str == "0")
                            {
                                dsParent = dt.Select("ParentId is null");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentId=" + str + "");
                            }
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
        #endregion

        #region GetAreaNode
        private void GetAreaNode(HttpContext context)
        {
        //    int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
        //    JsonObject json = new JsonObject();
        //    DataSet ds = RegionBll.GetList("");
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        Model.Ms.Regions Model = RegionBll.GetModel(ParentId);
        //        if (Model != null)
        //        {

        //            string[] strList = Model.Path.TrimEnd(',').Split(',');
        //            string strClassID = string.Empty;
        //            if (strList.Length > 0)
        //            {
        //                List<DataRow[]> list = new List<DataRow[]>();
        //                foreach (string str in strList)
        //                {
        //                    DataRow[] dsParent = null;
        //                    if (str == "0")
        //                    {
        //                        dsParent = dt.Select("ParentId is null");
        //                    }
        //                    else
        //                    {
        //                        dsParent = dt.Select("ParentId=" + str + "");
        //                    }
        //                    list.Add(dsParent);
        //                }
        //                json.Accumulate("STATUS", "OK");
        //                json.Accumulate("DATA", list);
        //                json.Accumulate("PARENT", strList);
        //            }
        //            else
        //            {
        //                json.Accumulate("STATUS", "NODATA");
        //                context.Response.Write(json.ToString());
        //                return;
        //            }
        //        }
        //    }

        //    context.Response.Write(json.ToString());
        }
        #endregion

    }
}