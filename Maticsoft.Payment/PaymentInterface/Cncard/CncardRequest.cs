using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Cncard
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web.Security;

    internal class CncardRequest : PaymentRequest
    {
        private string c_language = "0";
        private string c_memo1 = "Cncard";
        private string c_memo2 = "";
        private string c_mid = "";
        private string c_moneytype = "0";
        private string c_order = "";
        private string c_orderamount = "";
        private string c_pass = "";
        private string c_paygate = "";
        private string c_retflag = "1";
        private string c_returl = "";
        private string c_ymd = "";
        private string gatewayUrl = "https://www.cncard.net/purchase/getorder.asp";
        private string notifytype = "0";

        public CncardRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.c_order = trade.OrderId;
            this.c_orderamount = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.c_ymd = Convert.ToDateTime(trade.Date).ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            this.c_returl = gateway.ReturnUrl;
            this.c_mid = payee.SellerAccount;
            this.c_pass = payee.PrimaryKey;
        }

        public override void SendRequest()
        {
            string strValue = FormsAuthentication.HashPasswordForStoringInConfigFile(this.c_mid + this.c_order + this.c_orderamount + this.c_ymd + this.c_moneytype + this.c_retflag + this.c_returl + this.c_paygate + this.c_memo1 + this.c_memo2 + this.notifytype + this.c_language + this.c_pass, "MD5").ToLower();
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("c_mid", this.c_mid));
            builder.Append(this.CreateField("c_order", this.c_order));
            builder.Append(this.CreateField("c_orderamount", this.c_orderamount));
            builder.Append(this.CreateField("c_ymd", this.c_ymd));
            builder.Append(this.CreateField("c_moneytype", this.c_moneytype));
            builder.Append(this.CreateField("c_retflag", this.c_retflag));
            builder.Append(this.CreateField("c_returl", this.c_returl));
            builder.Append(this.CreateField("c_paygate", this.c_paygate));
            builder.Append(this.CreateField("c_memo1", this.c_memo1));
            builder.Append(this.CreateField("c_memo2", this.c_memo2));
            builder.Append(this.CreateField("c_language", this.c_language));
            builder.Append(this.CreateField("notifytype", this.notifytype));
            builder.Append(this.CreateField("c_signstr", strValue));
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.gatewayUrl));
        }
    }
}

