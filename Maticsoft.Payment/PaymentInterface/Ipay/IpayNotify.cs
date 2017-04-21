using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Ipay
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Web.Security;

    internal class IpayNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public IpayNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["v_amount"]);
        }

        public override string GetOrderId()
        {
            return this.parameters["v_oid"];
        }
        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "ipay_" + this.parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            string str = this.parameters["v_date"];
            string str2 = this.parameters["v_mid"];
            string str3 = this.parameters["v_oid"];
            string str4 = this.parameters["v_amount"];
            string str5 = this.parameters["v_status"];
            string str6 = this.parameters["v_md5"];
            if ((((str == null) || (str2 == null)) || ((str3 == null) || (str4 == null))) || ((str5 == null) || (str6 == null)))
            {
                this.OnNotifyVerifyFaild();
            }
            else if (!str5.Equals("00"))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                string str7 = FormsAuthentication.HashPasswordForStoringInConfigFile(str + str2 + str3 + str4 + str5 + payee.PrimaryKey, "MD5").ToLower(CultureInfo.InvariantCulture);
                if (!str6.Equals(str7))
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

