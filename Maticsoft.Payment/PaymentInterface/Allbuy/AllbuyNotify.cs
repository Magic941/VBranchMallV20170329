using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Allbuy
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Web.Security;

    internal class AllbuyNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public AllbuyNotify(NameValueCollection parameters)
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
            return "allbuy_" + this.parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }
        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            string str = this.parameters["merchant"];
            string str2 = this.parameters["billno"];
            string str3 = this.parameters["v_pstring"];
            string str4 = this.parameters["amount"];
            string str5 = this.parameters["success"];
            string str6 = this.parameters["remark"];
            string str7 = this.parameters["sign"];
            if ((((str == null) || (str2 == null)) || ((str3 == null) || (str4 == null))) || (((str5 == null) || (str6 == null)) || (str7 == null)))
            {
                this.OnNotifyVerifyFaild();
            }
            else if (!str5.Equals("Y"))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                string password = str + str2 + str4 + str5 + payee.PrimaryKey;
                if (!str7.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower(CultureInfo.InvariantCulture)))
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

