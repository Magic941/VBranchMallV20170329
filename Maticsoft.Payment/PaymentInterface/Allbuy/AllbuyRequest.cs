using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Allbuy
{
    using System;
    using System.Globalization;
    using System.Text;

    internal class AllbuyRequest : PaymentRequest
    {
        private string Amount = "";
        private string BackUrl = "";
        private string BillNo = "";
        private string Date = "";
        private string gatewayUrl = "http://www.allbuy.cn/newpayment/payment.asp";
        private string key = "";
        private string merchant = "";
        private string Remark = "Allbuy";

        public AllbuyRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.merchant = payee.SellerAccount;
            this.key = payee.PrimaryKey;
            this.BillNo = trade.OrderId;
            this.Amount = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.Date = Convert.ToDateTime(trade.Date).ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            this.BackUrl = gateway.ReturnUrl;
        }

        public override void SendRequest()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("merchant", this.merchant));
            builder.Append(this.CreateField("BillNo", this.BillNo));
            builder.Append(this.CreateField("Amount", this.Amount));
            builder.Append(this.CreateField("Date", this.Date));
            builder.Append(this.CreateField("Remark", this.Remark));
            builder.Append(this.CreateField("BackUrl", this.BackUrl));
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.gatewayUrl));
        }
    }
}

