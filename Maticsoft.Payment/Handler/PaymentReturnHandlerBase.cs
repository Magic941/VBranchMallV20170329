/**
* PaymentReturnHandlerBase.cs
*
* 功 能： 支付回调/通知抽象Handler
* 类 名： PaymentReturnHandlerBase


*/

using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using Maticsoft.Payment.BLL;
using Maticsoft.Payment.Configuration;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Globalization;
using System.Xml;

namespace Maticsoft.Payment.Handler
{
    /// <summary>
    /// 支付回调/通知抽象Handler
    /// </summary>
    /// <typeparam name="T">订单信息</typeparam>
    public abstract class PaymentReturnHandlerBase<T> : IHttpHandler, IRequiresSessionState
        where T : class, IOrderInfo
    {
        protected string GatewayName;
        protected string[] GetwayDatas;
        protected NotifyQuery Notify;
        protected T Order;
        protected string RESULT_MSG = "通知时间:{0},支付宝交易号:{1},交易金额:{2},交易状态:{3},订单号:{4},收货人:{5}";

        private readonly bool _isNotify;
        private decimal _amount;
        private string _orderId;
        private HttpContext requestContext;

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Amount
        {
            get { return _amount; }
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        protected string OrderId
        {
            get { return _orderId; }
        }

        protected IPaymentOption<T> Option;

        /// <summary>
        /// 构造支付回调/通知Handler
        /// </summary>
        /// <param name="option">支付网关回调URL参数</param>
        /// <param name="isNotify">是否为支付网关异步回调请求</param>
        protected PaymentReturnHandlerBase(IPaymentOption<T> option, bool isNotify)
        {
            requestContext = HttpContext.Current;
            Option = option;
            this._isNotify = isNotify;
        }

        protected PaymentModeInfo PaymentMode;

        protected virtual void ExecuteResult(bool success, string status) { }
        protected virtual void DisplayMessage(string status) { }
        /// <summary>
        /// 开始返回处理订单信息
        /// </summary>
        protected void DoValidate()
        {
            PayConfiguration config = PayConfiguration.GetConfig();
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add(HttpContext.Current.Request.QueryString);
            parameters.Add(HttpContext.Current.Request.Form);
            string gatewayData = parameters[Globals.GATEWAY_KEY];
            if (string.IsNullOrEmpty(gatewayData))
            {
                this.ResponseStatus(false, "gatewaydatanotfound");
                return;
            }
            parameters.Remove(Globals.GATEWAY_KEY);
            //获取GetwayData特殊Base64数据
            GetwayDatas = Globals.DecodeData4Url(gatewayData).Split(new[] { '|' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (GetwayDatas.Length < 1)
            {
                this.ResponseStatus(false, "gatewaydatanotfound");
                return;
            }
            this.GatewayName = GetwayDatas[0].ToLower();
            GatewayProvider provider = config.Providers[this.GatewayName] as GatewayProvider;
            if (provider == null)
            {
                this.ResponseStatus(false, "gatewaynotfound");
                return;
            }
            if (string.IsNullOrWhiteSpace(provider.NotifyType))
            {
                this.ResponseStatus(false, "notifytypenotfound");
                return;
            }
            if (provider.UseNotifyMode)     //DONE: 使用差异通知模式 BEN NEW MODE 20131016
            {
                this.Notify = NotifyQuery.Instance(provider.NotifyType, parameters,
                    _isNotify ? NotifyMode.Notify : NotifyMode.Callback);
            }
            else                            //回调和通知均使用同一种模式
            {
                // 因为微信支付返回的结果为XML格式,需要转换成NameValueCollection
                if (string.Equals("wechat", this.GatewayName))
                {
                    NameValueCollection nvc = new NameValueCollection();
                    string xmlString = GetXmlString(requestContext.Request);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlString);
                    string sign = string.Empty;
                    foreach (XmlNode node in doc.FirstChild.ChildNodes)
                    {
                        nvc.Add(node.Name, node.InnerText);
                    }
                    parameters = nvc;
                    this.Notify = NotifyQuery.Instance(provider.NotifyType, nvc);
                }
                else
                {
                    this.Notify = NotifyQuery.Instance(provider.NotifyType, parameters);
                }
            }
            if (this._isNotify)
            {
                this.Notify.ReturnUrl = Globals.FullPath(string.Format(Option.ReturnUrl, gatewayData));
            }
            #region 测试模式
            if (Globals.IsPaymentTestMode)
            {
                this._orderId = parameters["out_trade_no"];
            }
            #endregion
            else
            {
                this._orderId = this.Notify.GetOrderId();
            }
            string orderid = this._orderId;
            //多订单，需要把订单号分离出来，拿到多个订单号
            if (this._orderId.IndexOf("_") > 0)
            {
                Regex rex = new Regex(@"\D");// 非数字的正则表达式
                orderid = rex.Replace(orderid, "");//2010022813492
                this._orderId = orderid;
            }
            this.Order = Option.GetOrderInfo(orderid);
            if (this.Order == null)
            {
                this.ResponseStatus(false, "ordernotfound");
                return;
            }
            #region 测试模式
            if (Globals.IsPaymentTestMode)
            {
                this._amount = Globals.SafeDecimal(parameters["total_fee"], -1);
            }
            #endregion
            else
            {
                this._amount = this.Notify.GetOrderAmount() == decimal.Zero
                    ? this.Order.Amount
                    : this.Notify.GetOrderAmount();
            }
            //设置支付网关生成的订单ID
            this.Order.GatewayOrderId = this.Notify.GetGatewayOrderId();
            // 解决C网订单使用微信扫码支付的问题,因为扫码支付必须使用微信支付网关,获取微信Primarykey,否则后面导致验证签名时失败
            if (string.Equals("wechat", this.GatewayName.ToLower()))
            {
                PaymentMode = this.GetPaymentMode(this.GatewayName);
            }
            else
            {
                PaymentMode = this.GetPaymentMode(this.Order.PaymentTypeId);
            }
            if (PaymentMode == null)
            {
                this.ResponseStatus(false, "gatewaynotfound");
                return;
            }
            if (this.Order.PaymentStatus == PaymentStatus.Prepaid)
            {
                this.ResponseStatus(true, "success");
            }
            else
            {
                #region 测试模式
                //DONE: 测试模式埋点
                if (Globals.IsPaymentTestMode)
                {
                    string sign = HttpContext.Current.Request.QueryString["sign"];
                    if (string.IsNullOrWhiteSpace(sign))
                        this.ResponseStatus(false, "<TestMode> no sign");

                    System.Text.StringBuilder url = new System.Text.StringBuilder(
                        Globals.FullPath(string.Format(Option.ReturnUrl, gatewayData)));
                    url.AppendFormat("&out_trade_no={0}", this._orderId);
                    url.AppendFormat("&total_fee={0}", this._amount);

                    if (sign != Globals.GetMd5(System.Text.Encoding.UTF8, url.ToString()))
                        this.ResponseStatus(false, "<TestMode> Unauthorized sign");

                    //效验通过
                    PaidToSite();
                    return;
                }
                #endregion
                PayeeInfo payee = new PayeeInfo
                {
                    EmailAddress = PaymentMode.EmailAddress,
                    Partner = PaymentMode.Partner,
                    Password = PaymentMode.Password,
                    PrimaryKey = PaymentMode.SecretKey,
                    SecondKey = PaymentMode.SecondKey,
                    SellerAccount = PaymentMode.MerchantCode
                };
                this.Notify.NotifyVerifyFaild += new NotifyEventHandler(this.notify_NotifyVerifyFaild);
                this.Notify.PaidToIntermediary += new NotifyEventHandler(this.notify_PaidToIntermediary);
                this.Notify.PaidToMerchant += new NotifyEventHandler(this.notify_PaidToMerchant);
                try
                {
                    // 银联商务全民付同步通知不需要进行验证
                    if (string.Equals(this.GatewayName.ToLower(), "chinaums", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (this._isNotify)
                        {
                            this.Notify.VerifyNotify(0x7530, payee);
                        }
                        else
                        {
                            try
                            {
                                this.ResponseStatus(true, "success");
                            }
                            catch (Exception ex)
                            {
                                this.ResponseStatus(false, "fail");
                            }
                        }
                    }
                    else
                    {
                        this.Notify.VerifyNotify(0x7530, payee);
                    }
                }
                catch (Exception e)
                {
                    ErrorLogTxt.GetInstance("支付返回日志").Write("校验请求发生异常" + e.Message);
                }
            }
        }

        private void notify_NotifyVerifyFaild(NotifyQuery sender)
        {
            this.ResponseStatus(false, "verifyfaild");
        }

        private void PaidToSite()
        {
            ErrorLogTxt.GetInstance("王中玉").Write("异步回调开始。。。");
            if (this.Order.PaymentStatus == PaymentStatus.Prepaid)
            {
                ErrorLogTxt.GetInstance("王中玉").Write("成功");
                this.ResponseStatus(true, "success");
            }
            else
            {
                ErrorLogTxt.GetInstance("王中玉").Write("流水号" + this.Notify.GetNotifyId());
                if (OrderProcessor.CheckAction(this.Order, OrderActions.BUYER_PAY) && Option.PayForOrder(this.Order, this.Notify.GetNotifyId()))
                {
                    ErrorLogTxt.GetInstance("王中玉").Write("成功");
                    this.ResponseStatus(true, "success");
                }
                else
                {
                    ErrorLogTxt.GetInstance("王中玉").Write("失败");
                    this.ResponseStatus(false, "fail");
                }
            }
        }

        private void notify_PaidToIntermediary(NotifyQuery sender)
        {
            PaidToSite();
        }

        private void notify_PaidToMerchant(NotifyQuery sender)
        {
            PaidToSite();
        }

        private void ResponseStatus(bool success, string status)
        {
            ErrorLogTxt.GetInstance("支付返回日志").Write("返回的状态是：" + status);
            ExecuteResult(success, status);
            //Clear Page
            if (this._isNotify || !success)
            {
                HttpContext.Current.Response.Clear();
            }
            if (this._isNotify)
            {
                this.Notify.WriteBack(HttpContext.Current, success);
            }
            else
            {
                this.DisplayMessage(status);
            }
        }

        #region 获取支付信息
        /// <summary>
        /// 根据支付ID 获取支付信息
        /// </summary>
        /// <param name="paymentTypeId"></param>
        /// <returns></returns>
        protected virtual PaymentModeInfo GetPaymentMode(int paymentTypeId)
        {
            return PaymentModeManage.GetPaymentModeById(paymentTypeId);
        }

        protected virtual PaymentModeInfo GetPaymentMode(string gateway)
        {
            return PaymentModeManage.GetPaymentMode(gateway);
        }
        #endregion

        #region IHttpHandler 成员

        public virtual bool IsReusable
        {
            get { return false; }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            try
            {
                requestContext = context;
                ErrorLogTxt.GetInstance("订单支付返回日志").Write("返回的上一地址全路径是：" + HttpContext.Current.Request.UrlReferrer);
                ErrorLogTxt.GetInstance("订单支付返回日志").Write("返回的地址全路径是：" + HttpContext.Current.Request.Url);
                this.DoValidate();
                RESULT_MSG = string.Format(RESULT_MSG, context.Request.QueryString["notify_time"], context.Request.QueryString["trade_no"], context.Request.QueryString["total_fee"], context.Request.QueryString["trade_status"],
                                                       context.Request.QueryString["out_trade_no"], context.Request.QueryString["receive_name"]);
                ErrorLogTxt.GetInstance("订单支付返回日志").Write(RESULT_MSG);

            }
            catch (System.Exception ex)
            {
                RESULT_MSG = string.Format(RESULT_MSG + "程序错误信息：{6}", context.Request.QueryString["notify_time"], context.Request.QueryString["trade_no"], context.Request.QueryString["total_fee"], context.Request.QueryString["trade_status"],
                                                        context.Request.QueryString["out_trade_no"], context.Request.QueryString["receive_name"], ex.Message);
                ErrorLogTxt.GetInstance("订单支付返回日志异常").Write(RESULT_MSG);
            }

        }

        #endregion

        private string GetXmlString(HttpRequest request)
        {
            using (System.IO.Stream stream = request.InputStream)
            {
                Byte[] postBytes = new Byte[stream.Length];
                stream.Read(postBytes, 0, (Int32)stream.Length);
                return System.Text.Encoding.UTF8.GetString(postBytes);
            }
        }
    }


}

