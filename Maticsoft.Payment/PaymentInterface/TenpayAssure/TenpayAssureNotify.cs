using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.TenpayAssure
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;
    using System.Web;

    internal class TenpayAssureNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public TenpayAssureNotify(NameValueCollection parameters)
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
            return "tenpayassure_" + this.parameters[""].ToString();//此处填写网银支付通知参数中的流水号
        }
        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            this.parameters["key"] = payee.PrimaryKey;
            string notify_id = this.parameters["notify_id"];//通知id
            string partner = this.parameters["partner"];//商户号
            string trade_state = this.parameters["trade_state"];//支付结果
            /**
            0付款成功
            1交易创建
            2收获地址填写完毕
            4卖家发货成功
            5买家收货确认，交易成功
            6交易关闭，未完成超时关闭
            7修改交易价格成功
            8买家发起退款
            9退款成功
            10退款关闭
            */
            string transaction_id = this.parameters["transaction_id"];//财付通订单号
            string total_fee = this.parameters["total_fee"];//金额,以分为单位
            string out_trade_no = this.parameters["out_trade_no"];//商户订单号
            string trade_mode = this.parameters["trade_mode"];//交易模式，1即时到账，2中介担保

            string attach = this.parameters["attach"];
            this.parameters.Remove(Core.Globals.GATEWAY_KEY);

            if ((((notify_id == null) || (partner == null) || (trade_state == null)) || ((transaction_id == null) ||
                (total_fee == null))) || (((out_trade_no == null) || (trade_mode == null)) || (attach == null)))
            {
                this.OnNotifyVerifyFaild();
            }
            else if (trade_mode != "1" && trade_mode != "2")
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                if (Globals.isTenpaySign(this.parameters, "utf-8"))
                {
                    if (trade_mode == "1" && trade_state == "0")
                    {
                        this.OnPaidToMerchant();
                        return;
                    }
                    if (trade_mode == "2" && trade_state == "0")
                    {
                        this.OnPaidToIntermediary();
                        return;
                    }
                    if (trade_mode == "2" && trade_state == "5")
                    {
                        this.OnPaidToMerchant();
                        return;
                    }
                }
                this.OnNotifyVerifyFaild();
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

