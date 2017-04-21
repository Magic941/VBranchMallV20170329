using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.AlipayAssure
{
    using System.Globalization;

    internal class AssureRequest : PaymentRequest
    {
        private string _input_charset = "utf-8";
        private string body;
        private string gateway = "https://mapi.alipay.com/gateway.do?";
        private string key;
        private string logistics_fee = "0.00";
        private string logistics_payment = "BUYER_PAY";
        private string logistics_type = "EXPRESS";
        private string notify_url;
        private string out_trade_no;
        private string partner;
        private string payment_type = "1";
        private string price;
        private string quantity = "1";
        private string return_url;
        private string seller_email;
        private string service = "create_partner_trade_by_buyer";
        private string show_url;
        private string sign_type = "MD5";
        private string subject;

        public AssureRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.return_url = gateway.ReturnUrl;
            this.notify_url = gateway.NotifyUrl;
            this.body = trade.Body;
            this.out_trade_no = trade.OrderId;
            this.subject = trade.Subject;
            this.price = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.show_url = trade.Showurl;
            this.partner = payee.SellerAccount;
            this.key = payee.PrimaryKey;
            this.seller_email = payee.EmailAddress;
        }

        public override void SendRequest()
        {
            this.RedirectToGateway(Globals.CreatUrl(this.gateway, this.service, this.partner, this.sign_type, this.out_trade_no, this.subject, this.body, this.payment_type, this.price, this.show_url, this.seller_email, this.key, this.return_url, this._input_charset, this.notify_url, this.logistics_type, this.logistics_fee, this.logistics_payment, this.quantity));
        }
    }
}

