
using Maticsoft.Model.Shop.Order;
namespace Maticsoft.Web.Handlers.Shop.Pay
{
    /// <summary>
    /// 发送支付请求
    /// </summary>
    public class SendPaymentHandler : Payment.Handler.SendPaymentHandlerBase<OrderInfo>
    {
        #region 成员
        private readonly BLL.Shop.Order.Orders _orderManage = new BLL.Shop.Order.Orders();
        private const string MSG_ERRORLOG ="SendPaymentHandler >> Verificationn[{0}] 操作用户[{1}] 已阻止非法方式支付订单!";
        public const string KEY_ORDERID = "SendPayment_OrderId";
        public const string KEY_ACCESSMETHOD = "SendPayment_AccessMethod";//访问方式  手机还是电脑
        #endregion

        #region 构造
        public SendPaymentHandler()
            : base(new PaymentOption())
        {
            #region 设置网站名称
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.System);
            base.HostName = webSiteSet.WebName;
            #endregion
        }
        #endregion

        #region 验证请求是否合法
        /// <summary>
        /// 验证请求是否合法
        /// </summary>
        protected override bool VerifySendPayment(System.Web.HttpContext context)
        {
            #region 验证请求是否合法
            string[] orderIds = Payment.OrderProcessor.GetQueryString4OrderIds(context.Request);
            if (orderIds == null || orderIds.Length < 1) return false;
            long orderId = Common.Globals.SafeLong(orderIds[0], -1);
            if (orderId < -1) return false;
            if (!context.User.Identity.IsAuthenticated)
            {
                //未登录
                context.Response.Redirect("/Account/Login");
                return false;
            }
            Maticsoft.Accounts.Bus.User currentUser;
            if (context.Session[Common.Globals.SESSIONKEY_USER] == null)
            {
                currentUser = new Maticsoft.Accounts.Bus.User(
                    new Maticsoft.Accounts.Bus.AccountsPrincipal(context.User.Identity.Name));
                context.Session[Common.Globals.SESSIONKEY_USER] = currentUser;
            }
            else
            {
                currentUser = (Maticsoft.Accounts.Bus.User)context.Session[Common.Globals.SESSIONKEY_USER];
            }
            Model.Shop.Order.OrderInfo orderInfo = _orderManage.GetModel(orderId);
            if (orderInfo == null)
            {
                Web.LogHelp.AddErrorLog(string.Format(MSG_ERRORLOG, orderId, currentUser.UserID),
                    "非法操作订单", "Shop >> SendPaymentHandler >> Verification >> OrderInfo Is NULL");
                Maticsoft.Common.ErrorLogTxt.GetInstance("微信支付异常日志").Write("Maticsoft.Payment.Handler.SendPaymentHandlerBase__VerifySendPayment__获取订单信息异常,跳转至首页,订单ID为" + orderId);
                context.Response.Redirect("/");
                return false;
            }
            if (orderInfo.BuyerID != currentUser.UserID)
            {
                Web.LogHelp.AddErrorLog(string.Format(MSG_ERRORLOG, orderId, currentUser.UserID),
                    "非法操作订单", "Shop >> SendPaymentHandler >> Verification >> Check BuyerID");
                Maticsoft.Common.ErrorLogTxt.GetInstance("微信支付异常日志").Write("Maticsoft.Payment.Handler.SendPaymentHandlerBase__VerifySendPayment__订单购买者ID不等于当前用户ID,跳转至首页");
                context.Response.Redirect("/");
                return false;
            }
            Payment.Model.PaymentModeInfo paymentMode =
                Payment.BLL.PaymentModeManage.GetPaymentModeById(orderInfo.PaymentTypeId);
            if (paymentMode == null)
            {
                Web.LogHelp.AddErrorLog(string.Format(MSG_ERRORLOG, orderId, currentUser.UserID),
                    "非法操作订单", "Shop >> SendPaymentHandler >> Verification >> PaymentModeInfo Is NULL");
                Maticsoft.Common.ErrorLogTxt.GetInstance("微信支付异常日志").Write("Maticsoft.Payment.Handler.SendPaymentHandlerBase__VerifySendPayment__获取支付模式信息异常,跳转至首页");
                context.Response.Redirect("/");
                return false;
            }
            #endregion
            string basePath = "/";
            string u = context.Request.ServerVariables["HTTP_USER_AGENT"];

            string area = context.Request.QueryString["Area"];
            if (!string.IsNullOrWhiteSpace(area))
            {
                basePath = string.Format("/{0}/", area);
            }
            //向网关写入请求发起源的Area
            this.GatewayDatas.Add(area);
            //微信支付 向网关写入 APPID
            if (paymentMode.Gateway == "wechat")
            {
                string weChatAppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, "AA");

                this.GatewayDatas.Add(weChatAppId);
            }
            //if (u.ToLower().Contains("android") || u.ToLower().Contains("mobile"))//手机访问
            //{
            //    if (!paymentMode.DrivePath.Contains("|2|"))//不能手机支付
            //    {
            //        context.Session[KEY_ORDERID] = orderInfo.OrderId.ToString();
            //        context.Response.Redirect("/m/PayResult/MFail");
            //        return false;
            //    }
            //}
            //else//电脑访问
            //{
            //    if (!paymentMode.DrivePath.Contains("|1|")) //不能电脑支付
            //    {
            //        context.Session[KEY_ORDERID] = orderInfo.OrderId.ToString();
            //        context.Response.Redirect("/PayResult/MFail");
            //        return false;
            //    }
            //}
            return true;
        }
        #endregion

        #region 美化交易信息

        protected override Payment.Model.TradeInfo GetTrade(string orderIdStr,decimal orderTotal, OrderInfo order)
        {
            Payment.Model.TradeInfo info = new Payment.Model.TradeInfo();
            info.Body = HostName + "订单-订单号：[" + order.OrderCode + "]";
            info.BuyerEmailAddress = order.BuyerEmail;
            info.Date = order.OrderDate;
            //多订单编号，已经加了前辍
            info.OrderId = orderIdStr;
            info.Showurl = Common.Globals.HostPath(System.Web.HttpContext.Current.Request.Url);
            info.Subject = HostName + "订单-订单号：[" + order.OrderCode + "]-" +
                           "在线支付-订单支付金额" +
                           "：" + orderTotal.ToString("0.00") + "元";
            info.TotalMoney = orderTotal;
            return info;
        }

        #endregion
    }
}