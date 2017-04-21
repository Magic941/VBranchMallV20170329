/**
* RechargeHandler.cs
*
* 功 能： [N/A]
* 类 名： RechargeHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/13 22:02:38  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web.SessionState;
using System.Web;
using System;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.Shop
{
    public class RechargeHandler : HandlerBase, IRequiresSessionState
    {

        #region IHttpHandler 成员

        public override bool IsReusable
        {
            get { return false; }
        }

        public override void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";

            try
            {
                switch (action)
                {
                    case "SubmitOrder":
                        context.Response.Write(SubmitRecharge(context));
                        break;
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

        #endregion


        private char SubmitRecharge(HttpContext context)
        {
            //TODO: 1. 获取充值金额
            //TODO: 2. 获取支付方式
            //TODO: 3. 填充充值信息
            //TODO: 4. 创建充值记录
            //TODO: 5. 跳转到支付网关
            throw new NotImplementedException();
        }
    }
}