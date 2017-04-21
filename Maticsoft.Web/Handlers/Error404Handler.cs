/**
* Error404Handler.cs
*
* 功 能： [N/A]
* 类 名： Error404Handler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/31 10:55:29  Ben    初版
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

namespace Maticsoft.Web.Handlers
{
    public class Error404Handler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string from = context.Request.QueryString["aspxerrorpath"];
            int index = from.LastIndexOf("/") + 1;
            string search = from.Substring(index).Replace(".aspx", string.Empty).Replace("-", " ");
        }

        #endregion
    }
}