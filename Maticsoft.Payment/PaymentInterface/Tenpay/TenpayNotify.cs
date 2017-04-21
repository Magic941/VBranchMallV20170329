using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Tenpay
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;

    internal class TenpayNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public TenpayNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return (decimal.Parse(this.parameters["total_fee"], CultureInfo.InvariantCulture) / 100M);
        }

        public override string GetOrderId()
        {
            return this.parameters["out_trade_no"];
        }
        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "tenpay_" + this.parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            this.parameters["key"] = payee.PrimaryKey;
            string notify_id = this.parameters["notify_id"];//通知id
            string partner = this.parameters["partner"];//商户号
            string trade_state = this.parameters["trade_state"];//支付结果
            string transaction_id = this.parameters["transaction_id"];//财付通订单号
            string total_fee = this.parameters["total_fee"];//金额,以分为单位
            string out_trade_no = this.parameters["out_trade_no"];//商户订单号
            string trade_mode = this.parameters["trade_mode"];//交易模式，1即时到账，2中介担保
            string attach = this.parameters["attach"];
            
            
            if ((((notify_id == null) || (partner == null) || (trade_state == null)) || ((transaction_id == null) ||
                (total_fee == null))) || (((out_trade_no == null) || (trade_mode == null)) || (attach == null)))
            { 
                this.OnNotifyVerifyFaild();
            }
            else if (!trade_mode.Equals("1") || !trade_state.Equals("0"))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                if (!Globals.isTenpaySign(this.parameters, "utf-8"))
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
            if (context != null)
            {
                context.Response.Clear();
                context.Response.Write(success ? "success" : "fail");
                context.Response.End();
            }
        }
    }
}

