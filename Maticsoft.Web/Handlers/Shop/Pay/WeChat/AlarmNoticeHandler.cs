/**
* AlarmNoticeHandler.cs
*
* 功 能： 微信用户维权接口
* 类 名： AlarmNoticeHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/04 19:14:12  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web;
using Maticsoft.Model.Shop.Order;

namespace Maticsoft.Web.Handlers.Shop.Pay.WeChat
{
    /// <summary>
    /// 微信用户维权接口
    /// </summary>
    public class AlarmNoticeHandler : HandlerBase, System.Web.SessionState.IRequiresSessionState
    {
        #region HandlerBase 成员
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            try
            {
                AlarmNotice(context);
            }
            catch (System.Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
        }

        public override bool IsReusable
        {
            get { return false; }
        }
        #endregion

        private void AlarmNotice(HttpContext context)
        {
            Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
            context.Response.Write("success");
        }

    }
}