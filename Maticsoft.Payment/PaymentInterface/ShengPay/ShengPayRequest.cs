using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.ShengPay
{
    using System.Text;
    using System.Web.Security;

    internal class ShengPayRequest : PaymentRequest
    {
        private readonly string _amount = "";
        private readonly string _backUrl = "";
        private readonly string _bankCode = "";
        private readonly string _key = "";
        private readonly string _merchantNo = "";
        private readonly string _merchantUserId = "";
        private readonly string _notifyUrl = "";
        private readonly string _orderNo = "";
        private readonly string _orderTime = "";
        private readonly string _payChannel = "";
        private readonly string _postBackUrl = "";
        private readonly string _productDesc = "";
        private readonly string _productNo = "";
        private readonly string _productUrl = "";
        private readonly string _remark1 = "";
        private readonly string _remark2 = "";
        private const string CurrencyType = "RMB";
        private const string DefaultChannel = "";
        private const string GatewayUrl = "http://netpay.sdo.com/paygate/default.aspx";
        private const string NotifyUrlType = "http";
        private const string SignType = "2";
        private const string Version = "3.0";

        public ShengPayRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this._merchantNo = payee.SellerAccount;
            this._key = payee.PrimaryKey;
            this._orderNo = trade.OrderId;
            this._amount = trade.TotalMoney.ToString("F2");
            this._postBackUrl = gateway.ReturnUrl;
            this._notifyUrl = gateway.NotifyUrl;
            this._productUrl = trade.Showurl;
            this._orderTime = trade.Date.ToString("yyyyMMddHHmmss");
        }

        public override void SendRequest()
        {
            string strValue = FormsAuthentication.HashPasswordForStoringInConfigFile(("3.0" + this._amount + this._orderNo + this._merchantNo + this._merchantUserId + this._payChannel + this._postBackUrl + this._notifyUrl + this._backUrl + this._orderTime + "RMBhttp2" + this._productNo + this._productDesc + this._remark1 + this._remark2 + this._bankCode + this._productUrl) + this._key, "MD5");
            StringBuilder builder = new StringBuilder();
            builder.Append(this.CreateField("Version", "3.0"));
            builder.Append(this.CreateField("Amount", this._amount));
            builder.Append(this.CreateField("OrderNo", this._orderNo));
            builder.Append(this.CreateField("PostBackUrl", this._postBackUrl));
            builder.Append(this.CreateField("NotifyUrl", this._notifyUrl));
            builder.Append(this.CreateField("BackUrl", this._backUrl));
            builder.Append(this.CreateField("MerchantNo", this._merchantNo));
            builder.Append(this.CreateField("PayChannel", this._payChannel));
            builder.Append(this.CreateField("MerchantUserId", this._merchantUserId));
            builder.Append(this.CreateField("ProductNo", this._productNo));
            builder.Append(this.CreateField("ProductDesc", this._productDesc));
            builder.Append(this.CreateField("OrderTime", this._orderTime));
            builder.Append(this.CreateField("CurrencyType", "RMB"));
            builder.Append(this.CreateField("NotifyUrlType", "http"));
            builder.Append(this.CreateField("SignType", "2"));
            builder.Append(this.CreateField("Remark1", this._remark1));
            builder.Append(this.CreateField("Remark2", this._remark2));
            builder.Append(this.CreateField("BankCode", this._bankCode));
            builder.Append(this.CreateField("ProductUrl", this._productUrl));
            builder.Append(this.CreateField("DefaultChannel", ""));
            builder.Append(this.CreateField("MAC", strValue));
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), "http://netpay.sdo.com/paygate/default.aspx"));
        }
    }
}

