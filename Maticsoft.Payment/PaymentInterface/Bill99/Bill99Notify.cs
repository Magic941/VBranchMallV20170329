using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Bill99
{
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Security;

    internal class Bill99Notify : NotifyQuery
    {
        private NameValueCollection parameters;

        public Bill99Notify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        private string appendParam(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    string str = returnStr;
                    returnStr = str + "&" + paramId + "=" + paramValue;
                }
                return returnStr;
            }
            if (paramValue != "")
            {
                returnStr = paramId + "=" + paramValue;
            }
            return returnStr;
        }

        public override decimal GetOrderAmount()
        {
            return (decimal.Parse(this.parameters["payAmount"]) / 100M);
        }

        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "bill99_" + this.parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }

        public override string GetOrderId()
        {
            return this.parameters["orderId"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            string paramValue = this.parameters["merchantAcctId"];
            string str2 = this.parameters["version"];
            string str3 = this.parameters["language"];
            string str4 = this.parameters["signType"];
            string str5 = this.parameters["payType"];
            string str6 = this.parameters["bankId"];
            string str7 = this.parameters["orderId"];
            string str8 = this.parameters["orderTime"];
            string str9 = this.parameters["orderAmount"];
            string str10 = this.parameters["dealId"];
            string str11 = this.parameters["bankDealId"];
            string str12 = this.parameters["dealTime"];
            string str13 = this.parameters["payAmount"];
            string str14 = this.parameters["fee"];
            string str15 = this.parameters["ext1"];
            string str16 = this.parameters["ext2"];
            string str17 = this.parameters["payResult"];
            string str18 = this.parameters["errCode"];
            string str19 = this.parameters["signMsg"].ToUpper();
            if (!str17.Equals("10"))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                string returnStr = "";
                returnStr = this.appendParam(returnStr, "merchantAcctId", paramValue);
                returnStr = this.appendParam(returnStr, "version", str2);
                returnStr = this.appendParam(returnStr, "language", str3);
                returnStr = this.appendParam(returnStr, "signType", str4);
                returnStr = this.appendParam(returnStr, "payType", str5);
                returnStr = this.appendParam(returnStr, "bankId", str6);
                returnStr = this.appendParam(returnStr, "orderId", str7);
                returnStr = this.appendParam(returnStr, "orderTime", str8);
                returnStr = this.appendParam(returnStr, "orderAmount", str9);
                returnStr = this.appendParam(returnStr, "dealId", str10);
                returnStr = this.appendParam(returnStr, "bankDealId", str11);
                returnStr = this.appendParam(returnStr, "dealTime", str12);
                returnStr = this.appendParam(returnStr, "payAmount", str13);
                returnStr = this.appendParam(returnStr, "fee", str14);
                returnStr = this.appendParam(returnStr, "ext1", str15);
                returnStr = this.appendParam(returnStr, "ext2", str16);
                returnStr = this.appendParam(returnStr, "payResult", str17);
                returnStr = this.appendParam(returnStr, "errCode", str18);
                string str21 = FormsAuthentication.HashPasswordForStoringInConfigFile(this.appendParam(returnStr, "key", payee.PrimaryKey), "MD5").ToUpper();
                if (!str19.Equals(str21))
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
            if (context != null)
            {
                int index = base.ReturnUrl.IndexOf("?");
                if (index > 0)
                {
                    base.ReturnUrl = base.ReturnUrl.Substring(0, index);
                }
                context.Response.Clear();
                context.Response.Write(success ? string.Format("<result>1</result><redirecturl>{0}</redirecturl>", base.ReturnUrl) : string.Format("<result>0</result><redirecturl>{0}</redirecturl>", base.ReturnUrl));
                context.Response.End();
            }
        }
    }
}

