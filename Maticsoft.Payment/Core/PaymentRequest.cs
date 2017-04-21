/**
* PaymentRequest.cs
*
* 功 能： 支付请求抽象类
* 类 名： PaymentRequest
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System;
using System.Globalization;
using System.Web;
using Maticsoft.Payment.Model;
using Maticsoft.Common;

namespace Maticsoft.Payment.Core
{
    public abstract class PaymentRequest
    {
        private const string FormFormat = "<form id=\"payform\" name=\"payform\" action=\"{0}\" method=\"POST\">{1}</form>";
        private const string InputFormat = "<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\">";

        protected PaymentRequest()
        {
        }

        protected virtual string CreateField(string name, string strValue)
        {
            return string.Format(CultureInfo.InvariantCulture, "<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\">", new object[] { name, strValue });
        }

        protected virtual string CreateForm(string action, string content)
        {
            content = content + "<input type=\"submit\" value=\"在线支付\" style=\"display:none;\">";
            return string.Format(CultureInfo.InvariantCulture, "<form id=\"payform\" name=\"payform\" action=\"{0}\" method=\"POST\">{1}</form>", new object[] { action, content });
        }
        /// <summary>
        /// 根据请求类型来实例化，请求的实现对象
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="payee"></param>
        /// <param name="gateway"></param>
        /// <param name="trade"></param>
        /// <returns></returns>
        public static PaymentRequest Instance(string requestType, PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            if (string.IsNullOrEmpty(requestType))
            {
                return null;
            }
            object[] args = new object[] { payee, gateway, trade };
            return (Activator.CreateInstance(Type.GetType(requestType), args) as PaymentRequest);
        }

        protected virtual void RedirectToGateway(string url)
        {
            ErrorLogTxt.GetInstance("支付全路径URL").Write("支付的完整URL是："+  HttpUtility.UrlDecode(url));
            HttpContext.Current.Response.Redirect(url, true);
        }

        public abstract void SendRequest();
        protected virtual void SubmitPaymentForm(string formContent)
        {
            string s = formContent + "<script>document.forms['payform'].submit();</script>";
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
    }
}

