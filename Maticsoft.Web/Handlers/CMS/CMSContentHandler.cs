/**
* CMSContentHandler.cs
*
* 功 能： [N/A]
* 类 名： CMSContentHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/31 17:51:01  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.CMS
{
    public class CMSContentHandler:IHttpHandler
    {
        public const string CMS_KEY_STATUS = "STATUS";
        public const string CMS_KEY_DATA = "DATA";

        public const string CMS_STATUS_SUCCESS = "SUCCESS";
        public const string CMS_STATUS_FAILED = "FAILED";
        public const string CMS_STATUS_ERROR = "ERROR";

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //安全起见, 所有产品相关Ajax请求为POST模式
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                switch (action)
                {
                    case "DeleteFile":
                        DeleteFile(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                json.Put(CMS_KEY_STATUS, CMS_STATUS_ERROR);
                json.Put(CMS_KEY_DATA, ex);
                context.Response.Write(json.ToString());
            }
        }

        private void DeleteFile(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string filepath = context.Request.Form["FilePath"];
            if (!string.IsNullOrWhiteSpace(filepath))
            {
                string fullPath = context.Server.MapPath(filepath);
                if (Common.FileManage.DeleteFile(fullPath))
                {
                    json.Accumulate(CMS_KEY_STATUS, CMS_STATUS_SUCCESS);
                }
                else
                {
                    json.Accumulate(CMS_KEY_STATUS, CMS_STATUS_FAILED);
                }
            }
            else
            {
                json.Accumulate(CMS_KEY_STATUS, CMS_STATUS_ERROR);
            }
            context.Response.Write(json.ToString());
        }
    }
}