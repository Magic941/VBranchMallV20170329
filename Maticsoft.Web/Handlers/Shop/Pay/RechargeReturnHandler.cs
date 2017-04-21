/**
* RechargeReturnHandler.cs
*
* 功 能： 充值回调处理
* 类 名： RechargeReturnHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/13 21:56:35  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web;
using Maticsoft.Payment.Model;

namespace Maticsoft.Web.Handlers.Shop.Pay
{
    /// <summary>
    /// 充值回调处理
    /// </summary>
    public class RechargeReturnHandler : Payment.Handler.RechargeReturnHandlerBase<RechargeRequestInfo, UserInfo>
    { 
        #region 成员
        public const string KEY_RECHARGEID = "RechargeReturn_RechargeId";
        public const string KEY_STATUS = "RechargeReturn_Status";
        #endregion

        /// <summary>
        /// 构造回调 设置为非异步模式
        /// </summary>
        public RechargeReturnHandler() : base(new RechargeOption(), false) { }

        /// <summary>
        /// 提示给用户信息
        /// </summary>
        /// <param name="status">支付结果</param>
        protected override void DisplayMessage(string status)
        {
            string basePath = MvcApplication.GetCurrentRoutePath(AreaRoute.Shop);
            #region 设置提示信息

            if (!string.IsNullOrWhiteSpace(this.RechargeId.ToString()))
            {
               HttpContext.Current.Session[KEY_RECHARGEID] =this.RechargeId.ToString();
            }
           
            HttpContext.Current.Session[KEY_STATUS] = status;
          
            #endregion
            switch (status)
            {
                case "success":         //支付成功
                    #region 跳转到充值成功页面
                    HttpContext.Current.Response.Redirect(basePath + "PayResult/RechargeSuccess");
                    #endregion
                    return;
                case "gatewaynotfound": //支付网关不存在
                case "verifyfaild":     //签名验证失败
                case "fail":            //支付失败
                default:
                    #region 跳转到充值失败页面
                    HttpContext.Current.Response.Redirect(basePath + "PayResult/RechargeFail");
                    #endregion
                    return;
            }
        }
    }
}