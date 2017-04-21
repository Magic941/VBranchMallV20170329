/**
* SendPaymentHandler.cs
*
* 功 能： 微信支付发起接口
* 类 名： SendPaymentHandler
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
using System.Text.RegularExpressions;

namespace Maticsoft.Web.Handlers.Shop.Pay.WeChat
{
    /// <summary>
    /// 微信支付发起接口
    /// </summary>
    public class SendPaymentHandler : HandlerBase, System.Web.SessionState.IRequiresSessionState
    {
        #region 成员
        private readonly Payment.Model.IPaymentOption<OrderInfo> _paymentOption = new PaymentOption();
        #endregion

        #region HandlerBase 成员
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                PayWeChat(context);
            }
            catch (System.Exception ex)
            {
                Json.JsonObject json = new Json.JsonObject();
                json.Put(KEY_STATUS, STATUS_ERROR);
                json.Put(KEY_DATA, ex.Message);
                context.Response.Write(json.ToString());
            }
        }

        public override bool IsReusable
        {
            get { return false; }
        }
        #endregion

        private void PayWeChat(HttpContext context)
        {
            string userid = "0";
            
            if (CurrentUser != null)
            {
                userid = CurrentUser.UserID.ToString();
            }
            #region 获取GetwayData特殊Base64数据
            string data = context.Request.QueryString["data"];
            if (string.IsNullOrWhiteSpace(data))
            {
                Maticsoft.Common.ErrorLogTxt.GetInstance("微信支付异常日志").Write("Maticsoft.Web.Handlers.Shop.Pay.WeChat.SendPaymentHandler__PayWeChat__参数data为空,跳转至首页");
               
                context.Response.Redirect(string.Format("/m{0}/",userid));
                return;
            }
            string[] getwayDatas = Common.DEncrypt.Base64.Decode4Url(data).Split(new[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (getwayDatas.Length < 1)
            {
                Maticsoft.Common.ErrorLogTxt.GetInstance("微信支付异常日志").Write("Maticsoft.Web.Handlers.Shop.Pay.WeChat.SendPaymentHandler__PayWeChat__获取网关数据异常,跳转至首页");
                context.Response.Redirect(string.Format("/m{0}/", userid));
                return;
            }
            #endregion
            //DONE: 采用网关动态参数传递区域, 解决回跳区域问题 BEN ADD 20140114
            //string basePath = MvcApplication.GetCurrentRoutePath(
            // getwayDatas.Length > 1
            //     ? Common.Globals.SafeEnum(getwayDatas[1], AreaRoute.None)
            //     : MvcApplication.MainAreaRoute, userid);
            string basePath = MvcApplication.GetCurrentRoutePath(
                getwayDatas.Length > 1
                    ? Common.Globals.SafeEnum(getwayDatas[1], AreaRoute.None)
                    : MvcApplication.MainAreaRoute, userid);

            Maticsoft.Accounts.Bus.User currentUser = this.CurrentUser;
            //未登录
            if (currentUser == null)
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_NODATA;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            //获取订单ID
            string orderId = context.Request.QueryString["orderid"];
            if (string.IsNullOrWhiteSpace(orderId))
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_NODATA;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            context.Session[PaymentReturnHandler.KEY_ORDERID] = orderId;
            //获取订单信息
            OrderInfo orderInfo = _paymentOption.GetOrderInfo(new System.Text.RegularExpressions.Regex(@"\D").Replace(orderId,""));
            if (orderInfo == null)
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_NODATA;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            #region 效验订单信息
            // 非自己订单不能支付
            if (orderInfo.BuyerID != currentUser.UserID)
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_UNAUTHORIZED;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            //非未支付订单, 终止执行
            if (orderInfo.PaymentStatus != 0)
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_UNAUTHORIZED;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            //非微信支付订单, 终止执行
            if (orderInfo.PaymentGateway != "wechat")
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_UNAUTHORIZED;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            #endregion
            context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_SUCCESS.ToLower();

            context.Server.TransferRequest(basePath + "PayWeChat/Pay", true);
        }

        #region 获取字符串中的数字
        /// <summary>
        /// 获取字符串中的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public long GetNumberInt(string str)
        {
            long result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace(str, "\\D+", "");

                // 如果是数字，则转换为decimal类型 
                if (Regex.IsMatch(str, @"^\d*$"))
                {
                    result = long.Parse(str);
                }
            }
            return result;
        }
        #endregion
    }
}