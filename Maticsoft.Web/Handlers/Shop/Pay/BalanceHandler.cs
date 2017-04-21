/**
* BalanceHandler.cs
*
* 功 能： 余额支付
* 类 名： BalanceHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/01/09 14:22:45  Ben     初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
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
    /// 余额支付
    /// </summary>
    public class BalanceHandler : HandlerBase, System.Web.SessionState.IRequiresSessionState
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
                Pay(context);
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

        private void Pay(HttpContext context)
        {
            #region 获取GetwayData特殊Base64数据
            string data = context.Request.QueryString["data"];
            if (string.IsNullOrWhiteSpace(data))
            {
                context.Response.Redirect("/");
                return;
            }
            string[] getwayDatas = Common.DEncrypt.Base64.Decode4Url(data).Split(new[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (getwayDatas.Length < 1)
            {
                context.Response.Redirect("/");
                return;
            }
            #endregion

            //DONE: 采用网关动态参数传递区域, 解决回跳区域问题 BEN ADD 20140114
            string basePath = MvcApplication.GetCurrentRoutePath(
                getwayDatas.Length > 1
                    ? Common.Globals.SafeEnum(getwayDatas[1], AreaRoute.None)
                    : MvcApplication.MainAreaRoute);


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
            OrderInfo orderInfo = _paymentOption.GetOrderInfo(orderId);
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
            //非未支付订单, 终止支付
            if (orderInfo.PaymentStatus != 0)
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_UNAUTHORIZED;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            #endregion

            #region 效验用户余额
            BLL.Members.UsersExp userExpManage = new BLL.Members.UsersExp();
            decimal balance = userExpManage.GetUserBalance(currentUser.UserID);
            if (balance < orderInfo.Amount)
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = "NOBALANCE";
                context.Response.Redirect(basePath + "PayResult/NoBalance");
                return;
            }
            #endregion

            #region 进行余额支付
            BLL.Pay.BalanceDetails balanceManage = new BLL.Pay.BalanceDetails();
            if (!balanceManage.Pay(orderInfo.Amount, currentUser.UserID,
                string.Format("支付订单 [{0}] ", orderInfo.OrderCode)))
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_FAILED;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            //余额支付成功 调用支付订单
            else if (!_paymentOption.PayForOrder(orderInfo,""))
            {
                context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_FAILED;
                context.Response.Redirect(basePath + "PayResult/Fail");
                return;
            }
            #endregion

            context.Session[PaymentReturnHandler.KEY_STATUS] = STATUS_SUCCESS.ToLower();
            context.Response.Redirect(basePath + "PayResult/Success");
        }
    }
}