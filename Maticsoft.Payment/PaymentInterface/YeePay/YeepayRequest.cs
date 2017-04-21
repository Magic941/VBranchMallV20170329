using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.YeePay
{
    using System.Globalization;

    internal class YeepayRequest : PaymentRequest
    {
        private string addressFlag = "";
        private string amount = "";
        private string cur = "";
        private string frpId = "";
        private string keyValue = "";
        private string merchantCallbackURL = "";
        private string merchantId = "";
        private string orderId = "";
        private string payerContact = "";
        private string pid = "";
        private string productDesc = "";
        private string productId = "";
        private string sMctProperties = "YeePay";

        public YeepayRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.merchantId = payee.SellerAccount;
            this.keyValue = payee.PrimaryKey;
            this.pid = payee.Partner;
            this.amount = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.orderId = trade.OrderId;
            this.payerContact = trade.BuyerEmailAddress;
            this.productDesc = trade.Subject;
            this.cur = gateway.CurrencyType;
            this.merchantCallbackURL = gateway.ReturnUrl;
        }

        public override void SendRequest()
        {
            this.RedirectToGateway(Buy.CreateUrl(this.merchantId, this.keyValue, this.orderId, this.amount, this.cur, this.productId, this.merchantCallbackURL, this.addressFlag, this.sMctProperties, this.frpId));
        }
    }
}

