using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.IPS
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web.Security;

    internal class IpsRequest : PaymentRequest
    {
        private string Amount = "";
        private string Attach = "IPS";
        private string Billno = "";
        private string Cert = "";
        private string Currency_Type = "RMB";
        private string Date = "";
        private string Gateway_Type = "01";
        private string Mer_code = "";
        private string Merchanturl = "";
        private string OrderEncodeType = "2";
        private string PostUrl = "https://pay.ips.com.cn/ipayment.aspx";
        private string RetEncodeType = "12";
        private string Rettype = "0";

        public IpsRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.Mer_code = payee.SellerAccount;
            this.Cert = payee.PrimaryKey;
            this.Merchanturl = gateway.ReturnUrl;
            this.Billno = trade.OrderId;
            this.Amount = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            DateTime date = trade.Date;
            this.Date = Convert.ToDateTime(trade.Date).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }

        public override void SendRequest()
        {
            string strValue = FormsAuthentication.HashPasswordForStoringInConfigFile(this.Billno + this.Amount + this.Date + this.Currency_Type + this.Cert, "MD5").ToLower(CultureInfo.InvariantCulture);
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("mer_code", this.Mer_code));
            builder.Append(this.CreateField("Billno", this.Billno));
            builder.Append(this.CreateField("Gateway_type", this.Gateway_Type));
            builder.Append(this.CreateField("Currency_Type", this.Currency_Type));
            builder.Append(this.CreateField("Amount", this.Amount));
            builder.Append(this.CreateField("Date", this.Date));
            builder.Append(this.CreateField("Merchanturl", this.Merchanturl));
            builder.Append(this.CreateField("Attach", this.Attach));
            builder.Append(this.CreateField("OrderEncodeType", this.OrderEncodeType));
            builder.Append(this.CreateField("RetEncodeType", this.RetEncodeType));
            builder.Append(this.CreateField("RetType", this.Rettype));
            builder.Append(this.CreateField("SignMD5", strValue));
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.PostUrl));
        }
    }
}

