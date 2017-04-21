using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.YeePay
{
    using System.Collections.Specialized;
    using System.Web;

    internal class YeepayNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public YeepayNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["r3_Amt"]);
        }
        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "yeepay_" + this.parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }
        
        public override string GetOrderId()
        {
            return this.parameters["r6_Order"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            string sCmd = this.parameters["r0_Cmd"];
            string merchantId = this.parameters["p1_MerId"];
            string sErrorCode = this.parameters["r1_Code"];
            string sTrxId = this.parameters["r2_TrxId"];
            string amount = this.parameters["r3_Amt"];
            string cur = this.parameters["r4_Cur"];
            string productId = this.parameters["r5_Pid"];
            string orderId = this.parameters["r6_Order"];
            string userId = this.parameters["r7_Uid"];
            string mp = this.parameters["r8_MP"];
            string bType = this.parameters["r9_BType"];
            string hmac = this.parameters["hmac"];
            if (sErrorCode != "1")
            {
                this.OnNotifyVerifyFaild();
            }
            else if (!Buy.VerifyCallback(merchantId, payee.PrimaryKey, sCmd, sErrorCode, sTrxId, amount, cur, productId, orderId, userId, mp, bType, hmac))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                this.OnPaidToMerchant();
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
        }
    }
}

