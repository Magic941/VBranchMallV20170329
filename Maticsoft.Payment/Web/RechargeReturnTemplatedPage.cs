/**
* RechargeReturnTemplatedPage.cs
*
* 功 能： 支付模块回调/通知抽象基类
* 类 名： RechargeReturnTemplatedPage
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
* V0.02 2012/10/11  研发部    姚远   增加接口实现类, 减少其他模块耦合度
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System.Collections.Specialized;
using System.Globalization;
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
    /// 支付模块回调/通知抽象基类
    /// </summary>
    [System.Obsolete]
    public abstract class RechargeReturnTemplatedPage :
        RechargeReturnTemplatedPage<RechargeRequestInfo>
    {
        protected RechargeReturnTemplatedPage(bool _isBackRequest) : base(_isBackRequest) { }
    }

    /// <summary>
    /// 支付模块回调/通知抽象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Obsolete]
    public abstract class RechargeReturnTemplatedPage<T> : Page
        where T : class,IRechargeRequest, new()
    {
        protected decimal Amount;
        protected string GatewayName;
        protected int RechargeId;
        protected T RechargeRequest;
        private bool isBackRequest;
        protected NotifyQuery Notify;
        protected PaymentModeInfo paymode;

        protected RechargeReturnTemplatedPage(bool _isBackRequest)
        {
            this.isBackRequest = _isBackRequest;
        }

        protected override void CreateChildControls()
        {
            this.DoValidate();
        }

        protected virtual void DisplayMessage(string status) { }
        protected void DoValidate()
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
                //TODO: 充值返回URL 应对应为子类可写, 与Send类相同
                this.Notify.ReturnUrl = Globals.FullPath(string.Format(Globals.PAYMENT_RETURN_URL, this.GatewayName));
            }
            this.RechargeId = int.Parse(this.Notify.GetOrderId(), CultureInfo.InvariantCulture);
            this.Amount = this.Notify.GetOrderAmount();
            this.RechargeRequest = this.GetRechargeRequest(this.RechargeId);
            if (this.RechargeRequest == null)
            {
                this.ResponseStatus(true, "success");
            }
            else
            {
                this.Amount = this.RechargeRequest.RechargeBlance;
                this.paymode = PaymentModeManage.GetPaymentModeById(this.RechargeRequest.PaymentTypeId);
                if (this.paymode == null)
                {
                    this.ResponseStatus(false, "gatewaynotfound");
                }
                else
                {
                    PayeeInfo payee = new PayeeInfo
                    {
                        EmailAddress = this.paymode.EmailAddress,
                        Partner = this.paymode.Partner,
                        Password = this.paymode.Password,
                        PrimaryKey = this.paymode.SecretKey,
                        SecondKey = this.paymode.SecondKey,
                        SellerAccount = this.paymode.MerchantCode
                    };
                    this.Notify.PaidToIntermediary += new NotifyEventHandler(this.notify_PaidToIntermediary);
                    this.Notify.PaidToMerchant += new NotifyEventHandler(this.notify_PaidToMerchant);
                    this.Notify.NotifyVerifyFaild += new NotifyEventHandler(this.notify_NotifyVerifyFaild);
                    this.Notify.VerifyNotify(0x7530, payee);
                }
            }
        }

        private void notify_NotifyVerifyFaild(NotifyQuery sender)
        {
            this.ResponseStatus(false, "verifyfaild");
        }

        private void notify_PaidToIntermediary(NotifyQuery sender)
        {
            this.ResponseStatus(false, "waitconfirm");
        }

        private void notify_PaidToMerchant(NotifyQuery sender)
        {
            if (this.PayForRechargeRequest(RechargeRequest))
            {
                this.ResponseStatus(true, "success");
            }
            else
            {
                this.ResponseStatus(false, "fail");
            }
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

        #region 子类实现
        /// <summary>
        /// 获取充值信息
        /// </summary>
        /// <returns></returns>
        protected abstract T GetRechargeRequest(long rechargeId);

        /// <summary>
        /// 更新充值信息-完成付款
        /// </summary>
        /// <param name="rechargeRequest">充值对象</param>
        /// <returns>是否成功</returns>
        protected abstract bool PayForRechargeRequest(T rechargeRequest);
        #endregion
    }
}

