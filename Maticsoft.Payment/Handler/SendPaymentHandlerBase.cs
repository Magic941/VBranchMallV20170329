/**
* SendPaymentHandlerBase.cs
*
* 功 能： 支付请求接口抽象基类
* 类 名： SendPaymentHandlerBase
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
* V0.02 2012/10/11  研发部    姚远   增加接口实现类, 减少其他模块耦合度
* V0.03 2013/05/07  研发部    姚远   增加测试模式
* V0.04 2014/01/10  研发部    姚远   新增网关自定义(动态)参数功能
*                                    并对网关信息以特殊Base64密文传输
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System.Web;
using System.Web.SessionState;
using Maticsoft.Payment.BLL;
using Maticsoft.Payment.Configuration;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;
using System.Collections.Generic;


namespace Maticsoft.Payment.Handler
{
    /// <summary>
    /// 支付请求接口抽象基类
    /// </summary>
    /// <typeparam name="T">订单信息</typeparam>
    public abstract class SendPaymentHandlerBase<T> : IHttpHandler, IRequiresSessionState
        where T : class, IOrderInfo, new()
    {
        protected string HostName = "好邻商城";
        protected IPaymentOption<T> Option;
        protected List<string> GatewayDatas = new List<string>();

        /// <summary>
        /// 构造支付请求接口，构造函数如何传递
        /// </summary>
        /// <param name="option">支付网关回调URL参数</param>
        protected SendPaymentHandlerBase(IPaymentOption<T> option)
        {
            this.Option = option;
        }

        #region 获取支付网关

        /// <summary>
        /// 获取支付网关
        /// </summary>
        protected virtual GatewayInfo GetGateway(string gatewayName)
        {
            //GatewayDatas 区域
            GatewayInfo info = new GatewayInfo();

            //写入网关名称和附加参数，代表不同的网关，用编号来申明 如 bank applipayredirect
            GatewayDatas.Insert(0, gatewayName);
            //针对URL 特殊Base64方法
            info.Data =  Globals.EncodeData4Url(string.Join("|", GatewayDatas));
            info.DataList = GatewayDatas;
            info.ReturnUrl = Globals.FullPath(string.Format(Option.ReturnUrl, info.Data));
            info.NotifyUrl = Globals.FullPath(string.Format(Option.NotifyUrl, info.Data));
            return info;
        }

        #endregion

        #region 获取收款人信息

        /// <summary>
        /// 获取收款人信息
        /// </summary>
        protected virtual PayeeInfo GetPayee(PaymentModeInfo paymode)
        {
            if (paymode == null)
            {
                return null;
            }
            PayeeInfo info = new PayeeInfo();
            info.EmailAddress = paymode.EmailAddress;
            info.Partner = paymode.Partner;
            info.Password = paymode.Password;
            info.PrimaryKey = paymode.SecretKey;
            info.SecondKey = paymode.SecondKey;
            info.SellerAccount = paymode.MerchantCode;
            return info;
        }

        #endregion

        #region 获取交易信息 - 多个订单合并支付处理

        /// <summary>
        /// 获取交易信息，订单ID可以直接转成编号组成
        /// </summary>
        protected virtual TradeInfo GetTrade(string orderIdStr, decimal totalMoney, T order)
        {
            TradeInfo info = new TradeInfo();
            info.OrderCode = order.OrderCode;
            info.Body = HostName + "订单 - 订单号: [" + orderIdStr + "]";
            info.BuyerEmailAddress = order.BuyerEmail;
            info.Date = order.OrderDate;
            //所有订单号，可以在这里增加前辍，然后再组合
            info.OrderId = orderIdStr;
            info.Showurl = Globals.HostPath(HttpContext.Current.Request.Url);
            info.Subject = HostName + "订单 - 订单号: [" + orderIdStr + "] - " +
                           "在线支付 - 订单支付金额" +
                           ": " + totalMoney.ToString("0.00") + " 元";
            info.TotalMoney = totalMoney;
            return info;
        }

        #endregion

        #region 获取支付信息

        /// <summary>
        /// 获取支付信息
        /// </summary>
        protected virtual PaymentModeInfo GetPaymentMode(int paymentTypeId)
        {
            return PaymentModeManage.GetPaymentModeById(paymentTypeId);
        }

        #endregion

        #region 获取订单支付金额

        /// <summary>
        /// 获取订单支付金额,  如果是多单支付，可能是各单的总和，目前合单支付相当于做了一半的工作 20141209
        /// </summary>
        /// <param name="orderIds">订单ID - 合并支付使用</param>
        /// <param name="orderInfo">订单信息</param>
        /// <returns>支付金额</returns>
        protected virtual decimal GetOrderTotalMoney(string[] orderIds, T orderInfo)
        {
            //从订单信息中 返回订单支付金额
            return orderInfo.Amount;
        }

        #endregion

        #region 验证支付请求是否合法

        /// <summary>
        /// 验证支付请求是否合法
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>是否合法</returns>
        protected virtual bool VerifySendPayment(HttpContext context)
        {
            return true;
        }

        #endregion

        #region IHttpHandler 成员

        public virtual bool IsReusable
        {
            get { return false; }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            //Safe
            if (!VerifySendPayment(context)) return;
            //订单ID字符串
            string orderIdStr = string.Empty;
            //获取全部订单ID,这种方法可以合单支付，订单格式为 id;id来组成。 在回退处理中并未有多单处理能力，这块需要优化 2014-8-3
            string[] orderIds = OrderProcessor.GetQueryString4OrderIds(context.Request, out orderIdStr);

            //订单ID NULL ERROR返回首页
            if (orderIds == null || orderIds.Length < 1)
            {
                //Add ErrorLog..
                Maticsoft.Common.ErrorLogTxt.GetInstance("微信支付异常日志").Write("Maticsoft.Payment.Handler.SendPaymentHandlerBase__ProcessRequest__获取订单ID异常,跳转至首页");
                HttpContext.Current.Response.Redirect("~/");
                return;
            }
            //合并支付 订单支付信息以第一份订单为主
            T orderInfo = Option.GetOrderInfo(orderIds[0]);
            if (orderInfo == null) return;
            //计算订单支付金额，所有订单总支付额
            decimal totalMoney = this.GetOrderTotalMoney(orderIds, orderInfo);
            if (totalMoney < 0) return;
            if (orderInfo.PaymentStatus != PaymentStatus.NotYet)
            {
                //订单已支付
                context.Response.Write(
                    HttpContext.GetGlobalResourceObject("Resources", "IDS_ErrorMessage_SentPayment").ToString());
                return;
            }
          
            //根据支付ID取支付方式，这里可以缓存
            PaymentModeInfo paymentMode = PaymentModeManage.GetPaymentModeById(orderInfo.PaymentTypeId);
            if (paymentMode == null || string.IsNullOrWhiteSpace(paymentMode.Gateway))
            {
                //订单历史的支付方式不存在
                context.Response.Write(
                    HttpContext.GetGlobalResourceObject("Resources", "IDS_ErrorMessage_NoPayment").ToString());
                return;
            }
            string getwayName = paymentMode.Gateway.ToLower();

            //获取支付网关，在配置文件中Gateway.config获取支付网关信息
            GatewayProvider provider =
                PayConfiguration.GetConfig().Providers[getwayName] as GatewayProvider;
            if (provider == null) return;
            //支付网关,配置文件Gateway.config
            GatewayInfo gatewayInfo = this.GetGateway(getwayName);
            //交易信息,重新组织一个对象实体。
            TradeInfo tradeInfo = this.GetTrade(orderIdStr, totalMoney, orderInfo);
            #region 测试模式
            //DONE: 测试模式埋点
            if (Globals.IsPaymentTestMode && getwayName != "cod"  && getwayName != "advanceaccount")
            {
                System.Text.StringBuilder url = new System.Text.StringBuilder(gatewayInfo.ReturnUrl);
                url.AppendFormat("&out_trade_no={0}", tradeInfo.OrderId);
                url.AppendFormat("&total_fee={0}", tradeInfo.TotalMoney);
                url.AppendFormat("&sign={0}", Globals.GetMd5(System.Text.Encoding.UTF8, url.ToString()));

                //Maticsoft.Common.ErrorLogTxt.GetInstance("支付日志").Write("订单号为[" + orderInfo.OrderCode + "]支付方式为：" + orderInfo.PaymentTypeId);
                //Maticsoft.Common.ErrorLogTxt.GetInstance("支付日志").Write(url.ToString());
                HttpContext.Current.Response.Redirect(
                    gatewayInfo.ReturnUrl.Contains("?")
                        ? url.ToString()
                        : url.ToString().Replace("&out_trade_no", "?out_trade_no"), true);

                return;
            }
            #endregion

            string action = HttpContext.Current.Request.QueryString["action"];
            //Maticsoft.Common.ErrorLogTxt.GetInstance("支付日志").Write("订单号为[" + tradeInfo.OrderCode + "]支付方式为：微信0,action:" + action);
            //发送支付请求,请求核心类，可以被多种支付方式实例化，有自已请求的类
            PaymentRequest.Instance(
                provider.RequestType,
                this.GetPayee(paymentMode),
                gatewayInfo,
                tradeInfo
                ).SendRequest();
        }

        #endregion

    }
}
