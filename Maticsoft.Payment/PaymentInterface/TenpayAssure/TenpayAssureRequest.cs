using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.TenpayAssure
{
    using System;
    using System.Globalization;
    using System.Web;
    using System.Collections.Specialized;

    internal class TenpayAssureRequest : PaymentRequest
    {
        private string gatewayUrl = "https://gw.tenpay.com/gateway/pay.htm";
        private string sign_type = "MD5";
        private string input_charset = "UTF-8";
        private string sign_key_index = "1";
        private string service_version = "1.0";

        //支付参数
        private string partner;//商户号
        private string out_trade_no;// sp_billno);		//商家订单号
        private string total_fee;// (money * 100).ToString()); //商品金额,以分为单位
        private string return_url;//交易完成后跳转的URL
        private string notify_url;//接收财付通通知的URL
        private string body;//商品描述
        private string bank_type = "DEFAULT";  //银行类型(中介担保时此参数无效)
        private string spbill_create_ip;  //用户的公网ip，不是商户服务器IP
        private string fee_type = "1";//币种，1人民币
        private string subject; //商品名称(中介交易时必填)


        //业务可选参数
        private string attach = "TenpayAssure";//附加数据，原样返回
        //private string product_fee = "0";//商品费用，必须保证transport_fee + product_fee=total_fee
        private string transport_fee = "0";               //物流费用，必须保证transport_fee + product_fee=total_fee
        private string time_start;//订单生成时间，格式为yyyyMMddHHmmss
        //private string time_expire;//订单失效时间，格式为yyyymmddhhmmss
        //private string buyer_id;//买方财付通账号
        //private string goods_tag;//商品标记
        private string trade_mode = "1";//交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
        //private string transport_desc;              //物流说明
        private string trans_type = "1";//交易类型，1实物交易，2虚拟交易
        //private string agentid;//平台ID
        //private string agent_type;//代理模式，0无代理(默认)，1表示卡易售模式，2表示网店模式
        //private string seller_id;//卖家商户号，为空则等同于partner
        

        private string key = "";
        //private string transaction_id = "";

        public TenpayAssureRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.key = payee.PrimaryKey;
            this.partner = payee.SellerAccount;
            this.time_start = trade.Date.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            this.subject = Core.Globals.SubString(trade.Subject, 30, "..");   //String(32)
            this.body = Core.Globals.SubString(trade.Body, 30, "..");         //String(32)
            this.spbill_create_ip = getRealIp();
            //this.transaction_id = this.partner + trade.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + this.UnixStamp();
            this.out_trade_no = trade.OrderId;
            this.return_url = gateway.ReturnUrl;
            this.notify_url = gateway.NotifyUrl;
            this.total_fee = Convert.ToInt32((decimal)(trade.TotalMoney * 100M)).ToString(CultureInfo.InvariantCulture);
        }

        private static string getRealIp()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }

        public override void SendRequest()
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("key", this.key);
            parameters.Add("sign", string.Empty);

            parameters.Add("partner", partner); //商户号
            parameters.Add("out_trade_no", out_trade_no); //商家订单号
            parameters.Add("total_fee", total_fee); //商品金额,以分为单位
            parameters.Add("return_url", return_url); //交易完成后跳转的URL
            parameters.Add("notify_url", notify_url); //接收财付通通知的URL
            parameters.Add("body", body); //商品描述
            parameters.Add("bank_type", bank_type); //银行类型(中介担保时此参数无效)
            parameters.Add("spbill_create_ip", spbill_create_ip); //用户的公网ip，不是商户服务器IP
            parameters.Add("fee_type", fee_type); //币种，1人民币
            parameters.Add("subject", subject); //商品名称(中介交易时必填)
           
            //系统可选参数
            parameters.Add("sign_type", sign_type);
            parameters.Add("service_version", service_version);
            parameters.Add("input_charset", input_charset);
            parameters.Add("sign_key_index", sign_key_index);

            //业务可选参数
            parameters.Add("attach", attach);                      //附加数据，原样返回
            parameters.Add("time_start", time_start);            //订单生成时间，格式为yyyyMMddHHmmss
            parameters.Add("trade_mode", trade_mode);   //交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
            parameters.Add("trans_type", trans_type);
            parameters.Add("transport_fee", transport_fee);

            string url = this.gatewayUrl + "?" + Globals.GetParameURL(parameters, "utf-8");

            this.RedirectToGateway(url);
        }

        private uint UnixStamp()
        {
            TimeSpan span = (TimeSpan)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1)));
            return Convert.ToUInt32(span.TotalSeconds);
        }
    }
}

