using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Ipay
{
    using System.Globalization;
    using System.Text;
    using System.Web.Security;

    internal class IpayRequest : PaymentRequest
    {
        private string gateway = "http://www.ipay.cn/4.0/bank.shtml";
        private string v_amount = "";
        private string v_email = "";
        private string v_key = "";
        private string v_mid = "";
        private string v_mobile = "";
        private string v_oid = "";
        private string v_url = "";

        public IpayRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.v_oid = trade.OrderId;
            this.v_amount = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.v_email = trade.BuyerEmailAddress;
            this.v_url = gateway.ReturnUrl;
            this.v_mid = payee.SellerAccount;
            this.v_key = payee.PrimaryKey;
        }

        public override void SendRequest()
        {
            string strValue = FormsAuthentication.HashPasswordForStoringInConfigFile(this.v_mid + this.v_oid + this.v_amount + this.v_email + this.v_mobile + this.v_key, "MD5").ToLower(CultureInfo.InvariantCulture);
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("v_mid", this.v_mid));
            builder.Append(this.CreateField("v_oid", this.v_oid));
            builder.Append(this.CreateField("v_amount", this.v_amount));
            builder.Append(this.CreateField("v_email", this.v_email));
            builder.Append(this.CreateField("v_mobile", this.v_mobile));
            builder.Append(this.CreateField("v_md5", strValue));
            builder.Append(this.CreateField("v_url", this.v_url));
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.gateway));
        }
    }
}

