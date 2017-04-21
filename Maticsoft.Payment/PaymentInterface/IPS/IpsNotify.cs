using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.IPS
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Web.Security;

    internal class IpsNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public IpsNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["amount"]);
        }

        public override string GetOrderId()
        {
            return this.parameters["billno"];
        }

        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "ips_" + this.parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            string text1 = this.parameters["mercode"];
            string str = this.parameters["billno"];
            string str2 = this.parameters["amount"];
            string str3 = this.parameters["date"];
            string str4 = this.parameters["ipsbillno"];
            string str5 = this.parameters["succ"];
            string str6 = this.parameters["retEncodeType"];
            string text2 = this.parameters["msg"];
            string str7 = this.parameters["currency_type"];
            string text3 = this.parameters["attach"];
            string str8 = this.parameters["signature"];
            if ((((str == null) || (str2 == null)) || ((str3 == null) || (str4 == null))) || (((str5 == null) || (str6 == null)) || ((str7 == null) || (str8 == null))))
            {
                this.OnNotifyVerifyFaild();
            }
            else if (!str5.Equals("Y"))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                string password = str + str2 + str3 + str5 + str4 + str7 + payee.PrimaryKey;
                if (!str8.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower(CultureInfo.InvariantCulture)))
                {
                    this.OnNotifyVerifyFaild();
                }
                else
                {
                    this.OnPaidToMerchant();
                }
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
        }
    }
}

