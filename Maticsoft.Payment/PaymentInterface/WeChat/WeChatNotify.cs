using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.WeChat
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Xml;
    using System.IO;
    using System.Net;
    using System.Text;
    using System;
    using Maticsoft.Payment.PaymentInterface.WeChat.Utils;
    using System.Collections.Generic;

    public class WeChatNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public WeChatNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return (decimal.Parse(this.parameters["total_fee"], CultureInfo.InvariantCulture) / 100M);
        }
        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "wechat_" + this.parameters["transaction_id"].ToString();//此处填写网银支付通知参数中的流水号
        }

        public override string GetOrderId()
        {
            return this.parameters["out_trade_no"];
        }

        protected string key2 = "";

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            #region
            try
            {
                UnifiedWxPayHelper model = UnifiedWxPayHelper.CreateUnifiedHelper(this.parameters["appid"], payee.Partner, payee.PrimaryKey);
                NameValueCollection tempNvc = new NameValueCollection();
                foreach (string key in this.parameters.Keys)
                {
                    if (key.ToLower() != "sign")
                    {
                        tempNvc.Add(key, this.parameters[key]);
                    }
                }
                if (model.ValidateMD5Signature(CommonUtil.NameValueCollection2Dictionary(tempNvc), this.parameters["sign"]))
                {
                    //处理通知
                    this.OnPaidToMerchant();
                }
                else
                {
                    Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("支付返回日志").Write("验签失败!");
                    this.OnNotifyVerifyFaild();
                }
            }
            catch (Exception ex)
            {
                Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("支付返回日志").Write("支付失败!");
                this.OnNotifyVerifyFaild();
            }
            #endregion
        }

        public override void WriteBack(HttpContext context, bool success)
        {
            if (context != null)
            {
                context.Response.Clear();
                context.Response.Write(string.Format(@"<xml><return_code><![CDATA[{0}]]></return_code>
                    <return_msg><![CDATA[{1}]]></return_msg></xml>", success ? "SUCCESS" : "FAIL", success ? string.Empty : "处理异常"));
            }
        }

        /// <summary>
        /// 自动发货通知
        /// </summary>
        protected virtual void AutoDeliverNotify()
        {

        }

        protected virtual string SetPayHelperBase(Utils.WxPayHelper wxPayHelper)
        {
            NameValueCollection postData = LoadPostData();
            if (postData == null) return string.Empty;

            if (string.IsNullOrWhiteSpace(postData["openid"]) ||
                string.IsNullOrWhiteSpace(parameters["transaction_id"]) ||
                string.IsNullOrWhiteSpace(parameters["out_trade_no"]))
            {
                return string.Empty;
            }

            System.Collections.Generic.Dictionary<string, string> bizObj = new System.Collections.Generic.Dictionary<string, string>();

            bizObj.Add("openid", postData["openid"]);
            bizObj.Add("transid", parameters["transaction_id"]);
            bizObj.Add("out_trade_no", parameters["out_trade_no"]);

            bizObj.Add("deliver_status", "1");
            bizObj.Add("deliver_msg", "OK");

            //先设置基本信息
            wxPayHelper.SetAppId(postData["appid"]);
            wxPayHelper.SetAppKey(this.key2);
            wxPayHelper.SetSignType("SHA1");

            string tmp = wxPayHelper.CreateDeliverNotifyXml(bizObj);
            //Core.Globals.WriteText(new System.Text.StringBuilder(tmp));
            return tmp;
        }

        protected static NameValueCollection LoadPostData()
        {
            NameValueCollection postData = new NameValueCollection();
            if (HttpContext.Current.Request.InputStream.Length < 1) return null;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Request.InputStream);
            XmlNode root = xmlDoc.SelectSingleNode("xml");
            if (root == null) return null;
            XmlNodeList xnl = root.ChildNodes;

            foreach (XmlNode xnf in xnl)
            {
                //StringBuilder sb = new System.Text.StringBuilder();
                //Core.Globals.WriteText(sb.AppendFormat("{0}:{1},", xnf.Name, xnf.InnerText));
                postData.Add(xnf.Name, xnf.InnerText);
            }
            return postData;
        }

        protected string GetResponse(string url, string data, int? timeout = null)
        {
            string str;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (timeout.HasValue)
                {
                    request.Timeout = timeout.Value;
                }

                if (!string.IsNullOrWhiteSpace(data))
                {
                    request.Method = "POST";
                    //request.ServicePoint.Expect100Continue = false;
                    request.ContentType = "application/x-www-form-urlencoded";
                    using (StreamWriter requestWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        requestWriter.Write(data);
                    }
                }

                using (Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    StringBuilder builder = new StringBuilder();
                    while (-1 != reader.Peek())
                    {
                        builder.Append(reader.ReadLine());
                    }
                    str = builder.ToString();
                    reader.Close();
                    responseStream.Close();
                }
            }
            catch (Exception exception)
            {
                str = "Error:" + exception.Message;
            }
            return str;
        }
    }
}

