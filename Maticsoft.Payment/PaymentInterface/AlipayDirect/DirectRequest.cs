using System.Globalization;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.AlipayDirect
{
    internal class DirectRequest : PaymentRequest
    {
        private string _input_charset = "utf-8";
        private string body;
        private string extend_param = "isv^sh31";
        private string gateway = "https://mapi.alipay.com/gateway.do?";
        private string key;
        private string notify_url;
        /// <summary>
        /// 订单编号
        /// </summary>
        private string out_trade_no;
        private string partner;
        private string payment_type = "1";
        private string return_url;
        private string seller_email;
        private string service = "create_direct_pay_by_user";
        private string show_url;
        private string sign_type = "MD5";
        private string subject;
        private string token;
        private string total_fee;

        //从配置中初始化参数
        public DirectRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.partner = payee.SellerAccount;
            this.key = payee.PrimaryKey;
            this.seller_email = payee.EmailAddress;
            this.body = trade.Body;
            this.out_trade_no = trade.OrderId;
            this.subject = trade.Subject;
            this.total_fee = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.return_url = gateway.ReturnUrl;
            this.notify_url = gateway.NotifyUrl;
            this.show_url = trade.Showurl;
            this.token = trade.Token;
        }

        public override void SendRequest()
        {
            this.RedirectToGateway(Globals.CreatDirectUrl(this.gateway, this.service, 
                this.partner, this.sign_type, this.out_trade_no, this.subject,
                this.body, this.payment_type, this.total_fee, this.show_url,
                this.seller_email, this.key, this.return_url, this._input_charset,
                this.notify_url, this.extend_param, this.token));
        }
    }
}
