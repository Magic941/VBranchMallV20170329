using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.IPSExpress
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Web.Security;

    internal class IpsExpressNotify : NotifyQuery
    {
        private string Amount;
        private string BillNo;
        private string Merchant;
        private NameValueCollection parameters;
        private string Remark;
        private string Sign;
        private string Success;

        public IpsExpressNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["Amount"]);
        }
        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "ipsexpress_" + this.parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }

        public override string GetOrderId()
        {
            return this.parameters["BillNo"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            this.Merchant = this.parameters["Merchant"];
            this.BillNo = this.parameters["BillNo"];
            this.Amount = this.parameters["Amount"];
            this.Success = this.parameters["Success"];
            this.Remark = this.parameters["Remark"];
            this.Sign = this.parameters["Sign"];
            if ((((this.Merchant == null) || (this.BillNo == null)) || ((this.Amount == null) || (this.Success == null))) || ((this.Remark == null) || (this.Sign == null)))
            {
                this.OnNotifyVerifyFaild();
            }
            else if (!this.Success.Equals("Y"))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                string password = this.Merchant + this.BillNo + this.Amount + this.Remark + this.Success + payee.PrimaryKey;
                if (!this.Sign.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower(CultureInfo.InvariantCulture)))
                {
                    this.OnNotifyVerifyFaild();
                }
                else
                {
                    this.OnPaidToMerchant();
                }
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
        }
    }
}

