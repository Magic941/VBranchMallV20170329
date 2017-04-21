using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Chinabank
{
    using System.Globalization;
    using System.Text;
    using System.Web.Security;

    internal class ChinabankRequest : PaymentRequest
    {
        private string gateway = "https://pay3.chinabank.com.cn/PayGate";
        private string key = "";
        private string remark1 = "Chinabank";
        //异步通知地址
        private string remark2 = "";
        private string v_amount = "";
        private string v_mid = "";
        private string v_moneytype = "CNY";
        private string v_oid = "";
        private string v_url = "";
        private string md5Str = "";

        /// <summary>
        /// 实例化并转成个性化的参数
        /// </summary>
        /// <param name="payee"></param>
        /// <param name="gateway"></param>
        /// <param name="trade"></param>
        public ChinabankRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.v_oid = trade.OrderId;
            this.v_amount = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
           
            this.v_mid = payee.SellerAccount;           
            this.key = payee.PrimaryKey;  
            this.v_url = gateway.ReturnUrl + "?pay=1" ;
            md5Str =  FormsAuthentication.HashPasswordForStoringInConfigFile(this.v_amount + this.v_moneytype + this.v_oid + this.v_mid + this.v_url + this.key, "MD5").ToUpper(CultureInfo.InvariantCulture);
            this.remark2 = "[url:=" + gateway.NotifyUrl+"?pay=1]";
          
        }

        public override void SendRequest()
        {
            // string text =             v_amount + v_moneytype + v_oid + v_mid + v_url + key;
           
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("v_mid", this.v_mid));
            builder.Append(this.CreateField("v_oid", this.v_oid));
            builder.Append(this.CreateField("v_amount", this.v_amount));
            builder.Append(this.CreateField("v_moneytype", this.v_moneytype));
           
            builder.Append(this.CreateField("remark1", this.remark1));
            builder.Append(this.CreateField("remark2", this.remark2));
            builder.Append(this.CreateField("v_md5info", md5Str)); 
            builder.Append(this.CreateField("v_url", this.v_url));
          
            this.SubmitPaymentForm(this.CreateForm(this.gateway,builder.ToString()));
        }

        private string CreateReturnUrlParam()
        {
            StringBuilder str = new StringBuilder();
            str.Append("v_mid=").Append(this.v_mid).Append("&");
            str.Append("v_oid=").Append(this.v_oid).Append("&");
            str.Append("v_amount=").Append(this.v_amount).Append("&");
            str.Append("v_moneytype=").Append(this.v_moneytype).Append("&");
            str.Append("remark1=").Append(this.remark1);//.Append("&");
            //str.Append("v_md5info=").Append(this.md5Str);
            return str.ToString();
        }
    }
}

