using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;
using System.Xml;
using System;

namespace Maticsoft.Payment.PaymentInterface.AlipayWap
{
    internal class AlipayWapNotify : NotifyQuery
    {
        private string input_charset = "utf-8";
        private readonly NameValueCollection _parameters;
        private readonly NotifyMode _notifyMode;

        public AlipayWapNotify(NameValueCollection parameters, NotifyMode notifyMode)
        {
            this._parameters = parameters;
            this._notifyMode = notifyMode;
            if (_notifyMode == NotifyMode.Notify)
            {
                try
                {
                    //通知模式 XML解析notify_data数据到参数集合中
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(parameters["notify_data"]);
                    foreach (XmlNode item in xmlDoc.SelectSingleNode("/notify"))
                    {
                        this._parameters[item.Name] = item.InnerText;
                    }
                }
                catch { }
            }
        }

        private string CreateUrl(PayeeInfo payee)
        {
            return string.Format(CultureInfo.InvariantCulture, "https://mapi.alipay.com/gateway.do?service=notify_verify&partner={0}&notify_id={1}", new object[] { payee.SellerAccount, this._parameters["notify_id"] });
        }
        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "alipaywap_" + this._parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }

        public override string GetGatewayOrderId()
        {
            return this._parameters["trade_no"];
        }

        public override decimal GetOrderAmount()
        {
            if (string.IsNullOrWhiteSpace(this._parameters["total_fee"]))
                return decimal.Zero;
            return decimal.Parse(this._parameters["total_fee"]);
        }

        public override string GetOrderId()
        {
            return this._parameters["out_trade_no"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            bool flag;
            try
            {

                flag = bool.Parse(this.GetResponse(this.CreateUrl(payee), timeout));
            }
            catch
            {
                flag = false;
            }
            this._parameters.Remove(Maticsoft.Payment.Core.Globals.GATEWAY_KEY);
            foreach (string tmpParameter in Core.Globals.AlipayOtherParamKeys)
            {
                if (!string.IsNullOrEmpty(tmpParameter))
                {
                    this._parameters.Remove(tmpParameter);
                }
            }


            string[] strArray2;
            if (_notifyMode == NotifyMode.Notify)
            {
                //Wap通知的特殊处理
                strArray2 = new[]
                {
                    "service", "v", "sec_id", "notify_data"
                };
            }
            else
            {
                //根据字母a到z的顺序把参数排序
                strArray2 = Globals.BubbleSort(this._parameters.AllKeys);
            }

            string s = "";
            #region 过滤参数
            for (int i = 0; i < strArray2.Length; i++)
            {
                if ((!string.IsNullOrEmpty(this._parameters[strArray2[i]]) && (strArray2[i] != "sign")) && (strArray2[i] != "sign_type"))
                {
                    if (i == (strArray2.Length - 1))
                    {
                        s = s + strArray2[i] + "=" + this._parameters[strArray2[i]];
                    }
                    else
                    {
                        s = s + strArray2[i] + "=" + this._parameters[strArray2[i]] + "&";
                    }
                }
            }
            #endregion
            s = s + payee.PrimaryKey;
            flag = flag && this._parameters["sign"].Equals(Globals.GetMD5(s, this.input_charset));
            string str2 = this._parameters["trade_status"];
            if (flag && ((str2 == "TRADE_SUCCESS") || (str2 == "TRADE_FINISHED")))
            {
                this.OnPaidToMerchant();
            }
            else
            {
                this.OnNotifyVerifyFaild();
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
            if (context != null)
            {
                context.Response.Clear();
                context.Response.Write(success ? "success" : "fail");
                context.Response.End();
            }
        }
    }
}
