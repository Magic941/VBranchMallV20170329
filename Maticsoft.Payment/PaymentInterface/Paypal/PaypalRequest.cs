using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Paypal
{
    using System.Globalization;
    using System.Text;

    internal class PaypalRequest : PaymentRequest
    {
        private string amount;
        private string business;
        private string charset = "utf-8";
        private string cmd = "_xclick";
        private string currency_code = "CNY";
        private string custom = "PayPalStandard";
        private string gatewayUrl = "https://www.paypal.com/cgi-bin/webscr";
        private string invoice;
        private string item_number;
        private string no_note = "1";
        private string no_shipping = "1";
        private string quantity = "1";
        private string return_url;
        private string rm = "2";
        private string undefined_quantity = "0";

        public PaypalRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.amount = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.invoice = trade.OrderId;
            this.item_number = trade.OrderId;
            this.return_url = gateway.ReturnUrl;
            this.business = payee.SellerAccount;
        }

        public override void SendRequest()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("cmd", this.cmd));
            builder.Append(this.CreateField("amount", this.amount));
            builder.Append(this.CreateField("invoice", this.invoice));
            builder.Append(this.CreateField("quantity", this.quantity));
            builder.Append(this.CreateField("undefined_quantity", this.undefined_quantity));
            builder.Append(this.CreateField("no_shipping", this.no_shipping));
            builder.Append(this.CreateField("return", this.return_url));
            builder.Append(this.CreateField("rm", this.rm));
            builder.Append(this.CreateField("currency_code", this.currency_code));
            builder.Append(this.CreateField("custom", this.custom));
            builder.Append(this.CreateField("business", this.business));
            builder.Append(this.CreateField("charset", this.charset));
            builder.Append(this.CreateField("no_note", this.no_note));
            builder.Append(this.CreateField("item_number", this.item_number));
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.gatewayUrl));
        }
    }
}

