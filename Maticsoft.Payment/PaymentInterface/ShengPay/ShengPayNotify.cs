using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.ShengPay
{
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Security;

    internal class ShengPayNotify : NotifyQuery
    {
        private readonly NameValueCollection _parameters;

        public ShengPayNotify(NameValueCollection parameters)
        {
            this._parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this._parameters["PayAmount"]);
        }

        public override string GetOrderId()
        {
            return this._parameters["OrderNo"];
        }

        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "shengpay_" + this._parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }


        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            string str = this._parameters["Amount"];
            string str2 = this._parameters["PayAmount"];
            string str3 = this._parameters["OrderNo"];
            string str4 = this._parameters["SerialNo"];
            string str5 = this._parameters["Status"];
            string str6 = this._parameters["MerchantNo"];
            string str7 = this._parameters["PayChannel"];
            string str8 = this._parameters["Discount"];
            string str9 = this._parameters["SignType"];
            string str10 = this._parameters["PayTime"];
            string str11 = this._parameters["CurrencyType"];
            string str12 = this._parameters["ProductNo"];
            string str13 = this._parameters["ProductDesc"];
            string str14 = this._parameters["Remark1"];
            string str15 = this._parameters["Remark2"];
            string str16 = this._parameters["ExInfo"];
            string str17 = this._parameters["MAC"];
            string str19 = FormsAuthentication.HashPasswordForStoringInConfigFile((str + "|" + str2 + "|" + str3 + "|" + str4 + "|" + str5 + "|" + str6 + "|" + str7 + "|" + str8 + "|" + str9 + "|" + str10 + "|" + str11 + "|" + str12 + "|" + str13 + "|" + str14 + "|" + str15 + "|" + str16) + "|" + payee.PrimaryKey, "MD5");
            if ((str5 != "01") || (str17 != str19))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                this.OnPaidToMerchant();
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
            if ((context != null) && success)
            {
                context.Response.Clear();
                context.Response.Write("OK");
                context.Response.End();
            }
        }
    }
}

