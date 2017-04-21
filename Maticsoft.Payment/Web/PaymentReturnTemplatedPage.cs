/**
* PaymentReturnTemplatedPage.cs
*
* 功 能： 支付接口回调/通知抽象基类
* 类 名： PaymentReturnTemplatedPage
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
* V0.02 2012/10/11  研发部    姚远   增加接口实现类, 减少其他模块耦合度
* V0.03 2013/05/07  研发部    姚远   增加测试模式
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using Maticsoft.Payment.BLL;
using Maticsoft.Payment.Configuration;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Handler;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.Web
{
    /// <summary>
    /// 支付接口回调/通知抽象基类
    /// </summary>
    /// <typeparam name="T">订单信息</typeparam>
    [System.Obsolete]
    public abstract class PaymentReturnTemplatedPage<T> : Page where T : class, IOrderInfo
    {
        protected decimal Amount;
        protected string GatewayName;
        private bool isBackRequest;
        protected NotifyQuery Notify;
        protected T Order;
        protected string OrderId;

        protected IPaymentOption<T> Option;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_isBackRequest">是否为支付网关异步回调请求</param>
        protected PaymentReturnTemplatedPage(IPaymentOption<T> option, bool _isBackRequest)
        {
            this.Option = option;
            this.isBackRequest = _isBackRequest;
        }

        protected override void CreateChildControls()
        {
            this.DoValidate();
        }

        protected virtual void DisplayMessage(string status) { }
        protected virtual void DoValidate()
        {
            PayConfiguration config = PayConfiguration.GetConfig();
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add(this.Page.Request.Form);
            parameters.Add(this.Page.Request.QueryString);
            string tmpGatewayName = this.Page.Request.Params[Globals.GATEWAY_KEY];
            if (string.IsNullOrEmpty(tmpGatewayName))
            {
                this.ResponseStatus(false, "gatewaynotfound");
                return;
            }
            this.GatewayName = tmpGatewayName.ToLower();
            GatewayProvider provider = config.Providers[this.GatewayName] as GatewayProvider;
            if (provider == null)
            {
                this.ResponseStatus(false, "gatewaynotfound");
                return;
            }
            this.Notify = NotifyQuery.Instance(provider.NotifyType, parameters);
            if (this.isBackRequest)
            {
                this.Notify.ReturnUrl = Globals.FullPath(string.Format(Option.ReturnUrl, this.GatewayName));
            }
            this.Amount = this.Notify.GetOrderAmount();
            this.OrderId = this.Notify.GetOrderId();
            this.Order = this.GetOrderInfo(this.OrderId);
            if (this.Order == null)
            {
                this.ResponseStatus(false, "ordernotfound");
            }
            else if (this.Order.PaymentStatus == PaymentStatus.Prepaid)
            {
                this.ResponseStatus(true, "success");
            }
            else
            {
                //设置支付网关生成的订单ID
                this.Order.GatewayOrderId = this.Notify.GetGatewayOrderId();

                PaymentModeInfo paymentMode = this.GetPaymentMode(this.Order.PaymentTypeId);
                if (paymentMode == null)
                {
                    this.ResponseStatus(false, "gatewaynotfound");
                }
                else
                {
                    #region 测试模式
                    //DONE: 测试模式埋点
                    if (Globals.IsPaymentTestMode)
                    {
                        string sign = this.Page.Request.Params["sign"];
                        if (string.IsNullOrWhiteSpace(sign))
                            this.ResponseStatus(false, "<TestMode> no sign");

                        System.Text.StringBuilder url = new System.Text.StringBuilder(
                            Globals.FullPath(string.Format(Option.ReturnUrl, this.GatewayName)));
                        url.AppendFormat("&out_trade_no={0}", this.OrderId);
                        url.AppendFormat("&total_fee={0}", this.Amount);

                        if (sign != Globals.GetMd5(System.Text.Encoding.UTF8, url.ToString()))
                            this.ResponseStatus(false, "<TestMode> Unauthorized sign");

                        //效验通过
                        PaidToSite();
                        return;
                    }
                    #endregion

                    PayeeInfo payee = new PayeeInfo
                    {
                        EmailAddress = paymentMode.EmailAddress,
                        Partner = paymentMode.Partner,
                        Password = paymentMode.Password,
                        PrimaryKey = paymentMode.SecretKey,
                        SecondKey = paymentMode.SecondKey,
                        SellerAccount = paymentMode.MerchantCode
                    };
                    this.Notify.NotifyVerifyFaild += new NotifyEventHandler(this.notify_NotifyVerifyFaild);
                    this.Notify.PaidToIntermediary += new NotifyEventHandler(this.notify_PaidToIntermediary);
                    this.Notify.PaidToMerchant += new NotifyEventHandler(this.notify_PaidToMerchant);
                    this.Notify.VerifyNotify(0x7530, payee);
                }
            }
        }

        private void notify_NotifyVerifyFaild(NotifyQuery sender)
        {
            this.ResponseStatus(false, "verifyfaild");
        }

        private void PaidToSite()
        {
            if (this.Order.PaymentStatus == PaymentStatus.Prepaid)
            {
                this.ResponseStatus(true, "success");
            }
            else
            {
                if (OrderProcessor.CheckAction(this.Order, OrderActions.BUYER_PAY) && this.PayForOrder(this.Order))
                {
                    this.ResponseStatus(true, "success");
                }
                else
                {
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
            //Clear Page
            if (this.isBackRequest || !success)
            {
                this.Controls.Clear();
            }
            if (this.isBackRequest)
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
        #endregion

        #region 子类实现
        /// <summary>
        /// 根据订单ID 获取订单信息
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns>订单对象</returns>
        protected abstract T GetOrderInfo(string orderId);

        /// <summary>
        /// 更新订单-完成付款
        /// </summary>
        /// <param name="order">订单</param>
        /// <returns>是否成功</returns>
        protected abstract bool PayForOrder(T order);
        #endregion
    }
}

