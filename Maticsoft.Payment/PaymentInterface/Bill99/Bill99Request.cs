using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Bill99
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web.Security;

    internal class Bill99Request : PaymentRequest
    {
        private string bgUrl;
        private string ext1 = "99Bill";
        private string gateway = "https://www.99bill.com/gateway/recvMerchantInfoAction.htm";
        private string inputCharset = "1";
        private string key = "";
        private string language = "1";
        private string merchantAcctId;
        private string orderAmount;
        private string orderId;
        private string orderTime;
        private string payType = "00";
        private string pid = "";
        private string productName = "";
        private string productNum = "";
        private string redoFlag = "1";
        private string signType = "1";
        private string version = "v2.0";

        public Bill99Request(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.merchantAcctId = payee.SellerAccount;
            this.key = payee.PrimaryKey;
            this.productName = trade.OrderId;
            this.productNum = "1";
            if (!string.IsNullOrEmpty(payee.Partner))
            {
                this.pid = payee.Partner;
            }
            this.bgUrl = gateway.NotifyUrl;
            this.orderAmount = Convert.ToInt32((decimal) (trade.TotalMoney * 100M)).ToString(CultureInfo.InvariantCulture);
            this.orderId = trade.OrderId;
            this.orderTime = trade.Date.ToString("yyyyMMddhhmmss", CultureInfo.InvariantCulture);
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

        public override void SendRequest()
        {
            string returnStr = "";
            returnStr = this.appendParam(returnStr, "inputCharset", this.inputCharset);
            returnStr = this.appendParam(returnStr, "bgUrl", this.bgUrl);
            returnStr = this.appendParam(returnStr, "version", this.version);
            returnStr = this.appendParam(returnStr, "language", this.language);
            returnStr = this.appendParam(returnStr, "signType", this.signType);
            returnStr = this.appendParam(returnStr, "merchantAcctId", this.merchantAcctId);
            returnStr = this.appendParam(returnStr, "orderId", this.orderId);
            returnStr = this.appendParam(returnStr, "orderAmount", this.orderAmount);
            returnStr = this.appendParam(returnStr, "orderTime", this.orderTime);
            returnStr = this.appendParam(returnStr, "productName", this.productName);
            returnStr = this.appendParam(returnStr, "productNum", this.productNum);
            returnStr = this.appendParam(returnStr, "ext1", this.ext1);
            returnStr = this.appendParam(returnStr, "payType", this.payType);
            returnStr = this.appendParam(returnStr, "redoFlag", this.redoFlag);
            returnStr = this.appendParam(returnStr, "pid", this.pid);
            string strValue = FormsAuthentication.HashPasswordForStoringInConfigFile(this.appendParam(returnStr, "key", this.key), "MD5").ToUpper();
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("inputCharset", this.inputCharset));
            builder.Append(this.CreateField("bgUrl", this.bgUrl));
            builder.Append(this.CreateField("version", this.version));
            builder.Append(this.CreateField("language", this.language));
            builder.Append(this.CreateField("signType", this.signType));
            builder.Append(this.CreateField("merchantAcctId", this.merchantAcctId));
            builder.Append(this.CreateField("orderId", this.orderId));
            builder.Append(this.CreateField("orderAmount", this.orderAmount));
            builder.Append(this.CreateField("orderTime", this.orderTime));
            builder.Append(this.CreateField("productName", this.productName));
            builder.Append(this.CreateField("productNum", this.productNum));
            builder.Append(this.CreateField("ext1", this.ext1));
            builder.Append(this.CreateField("payType", this.payType));
            builder.Append(this.CreateField("redoFlag", this.redoFlag));
            builder.Append(this.CreateField("pid", this.pid));
            builder.Append(this.CreateField("signMsg", strValue));
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.gateway));
        }
    }
}

