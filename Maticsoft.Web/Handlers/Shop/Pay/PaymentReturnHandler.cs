/**
* PaymentReturnHandler.cs
*
* 功 能： 支付回调处理
* 类 名： PaymentReturnHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/05/14 17:28:15  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web;
using Maticsoft.Model.Shop.Order;

namespace Maticsoft.Web.Handlers.Shop.Pay
{
    /// <summary>
    /// 支付回调处理
    /// </summary>
    public class PaymentReturnHandler : Payment.Handler.PaymentReturnHandlerBase<OrderInfo>
    {
        #region 成员
        public const string KEY_ORDERID = "PaymentReturn_OrderId";
        public const string KEY_STATUS = "PaymentReturn_Status";
        #endregion

        /// <summary>
        /// 构造回调 设置为非异步模式
        /// </summary>
        public PaymentReturnHandler() : base(new PaymentOption(), false) { }

        /// <summary>
        /// 提示给用户信息
        /// </summary>
        /// <param name="status">支付结果</param>
        protected override void DisplayMessage(string status)
        {
            #region 设置提示信息

            //DONE: 采用网关动态参数传递区域, 解决回跳区域问题 BEN ADD 20140114
            string basePath = MvcApplication.GetCurrentRoutePath(
                this.GetwayDatas.Length > 1
                    ? Common.Globals.SafeEnum(this.GetwayDatas[1], AreaRoute.None)
                    : MvcApplication.MainAreaRoute);

            if (!string.IsNullOrWhiteSpace(this.OrderId))
                HttpContext.Current.Session[KEY_ORDERID] = this.OrderId;

            HttpContext.Current.Session[KEY_STATUS] = status;
            #endregion
            switch (status)
            {
                case "success":  //支付成功
                    #region 跳转到支付成功页面
                    HttpContext.Current.Response.Redirect(basePath + "PayResult/Success",false);
                    #endregion
                    return;
                case "gatewaynotfound": //支付网关不存在
                case "verifyfaild":     //签名验证失败
                case "fail":            //支付失败
                default:
                    #region 跳转到支付失败页面
                    HttpContext.Current.Response.Redirect(basePath + "PayResult/Fail",false);
                    #endregion
                    return;
            }
        }
    }
}