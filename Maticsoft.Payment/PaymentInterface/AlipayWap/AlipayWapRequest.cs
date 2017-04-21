using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.AlipayWap
{
    internal class AlipayWapRequest : PaymentRequest
    {
        private string format = "xml";
        private string v = "2.0";
        //private string merchant_url;

        private string _input_charset = "utf-8";
        private string gateway = "http://wappaygw.alipay.com/service/rest.htm?";
        private string key;
        private string notify_url;
        private string out_trade_no;
        private string partner;
        private string return_url;
        private string seller_email;
        private string sign_type = "MD5";
        private string subject;
        private string total_fee;

        public AlipayWapRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.partner = payee.SellerAccount;
            this.key = payee.PrimaryKey;
            this.seller_email = payee.EmailAddress;
            this.out_trade_no = trade.OrderId;
            this.subject = trade.Subject;
            this.total_fee = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.return_url = gateway.ReturnUrl;
            this.notify_url = gateway.NotifyUrl;
        }

        public override void SendRequest()
        {
            //请求业务参数详细
            string req_dataToken =
                "<direct_trade_create_req><notify_url>" + notify_url +
                "</notify_url><call_back_url>" + return_url +
                "</call_back_url><seller_account_name>" + seller_email +
                "</seller_account_name><out_trade_no>" + out_trade_no +
                "</out_trade_no><subject>" + subject +
                "</subject><total_fee>" + total_fee +
                "</total_fee></direct_trade_create_req>";

            //建立请求
            string sHtmlTextToken = Globals.BuildRequest(this.gateway, Globals.CreatParamUrl(
                "alipay.wap.trade.create.direct",
                this.partner, this.key, this.sign_type, this.format, this.v,
                req_dataToken, this._input_charset, DateTime.Now.ToString("yyyyMMddHHmmss")),
                this._input_charset);

            //URLDECODE返回的信息
            Encoding code = Encoding.GetEncoding(this._input_charset);
            sHtmlTextToken = System.Web.HttpUtility.UrlDecode(sHtmlTextToken, code);

            //解析远程模拟提交后返回的信息
            NameValueCollection dicHtmlTextToken = Globals.ParseResponse(sHtmlTextToken);

            //获取token
            string request_token = dicHtmlTextToken["request_token"];

            string req_data = "<auth_and_execute_req><request_token>" + request_token + "</request_token></auth_and_execute_req>";
            this.RedirectToGateway(Globals.CreatSendUrl(this.gateway, "alipay.wap.auth.authAndExecute",
                this.partner, this.key, this.sign_type, this.format, this.v, req_data, this._input_charset));
        }
    }
}
