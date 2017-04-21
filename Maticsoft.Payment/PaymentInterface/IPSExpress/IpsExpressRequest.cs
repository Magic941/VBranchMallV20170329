using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.IPSExpress
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web.Security;

    internal class IpsExpressRequest : PaymentRequest
    {
        private string Amount;
        private string BackUrl;
        private string BillNo;
        private string Merchant;
        private string MerPassword;
        private string PostUrl = "http://express.ips.com.cn/pay/payment.asp";
        private string Remark = "IPSExpress";

        public IpsExpressRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.Merchant = payee.SellerAccount;
            this.BillNo = trade.OrderId;
            this.Amount = trade.TotalMoney.ToString("F", CultureInfo.InvariantCulture);
            this.BackUrl = gateway.ReturnUrl;
            this.MerPassword = payee.PrimaryKey;
        }

        public override void SendRequest()
        {
            string strValue = FormsAuthentication.HashPasswordForStoringInConfigFile(this.Merchant + this.BillNo + this.Amount + this.Remark + this.MerPassword, "MD5").ToLower(CultureInfo.InvariantCulture);
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("Merchant", this.Merchant));
            builder.Append(this.CreateField("BillNo", this.BillNo));
            builder.Append(this.CreateField("Amount", this.Amount));
            builder.Append(this.CreateField("Remark", this.Remark));
            builder.Append(this.CreateField("BackUrl", this.BackUrl));
            builder.Append(this.CreateField("Sign", strValue));
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00018\" checked>中国工商银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00021\">招商银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00003\">中国建设银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00017\">中国农业银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00013\">民生银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00030\">光大银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00016\">兴业银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00111\">中国银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00211\">交通银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00311\">交通银行上海" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00411\">广东发展银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00023\">深圳发展银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00032\">浦东发展银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00511\">中信实业银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00611\">广州商业银行" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00711\">邮政储蓄" + Environment.NewLine);
            builder.Append("<input type=\"radio\" name=\"PayBank\" value=\"00811\">华夏银行" + Environment.NewLine);
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.PostUrl));
        }
    }
}

