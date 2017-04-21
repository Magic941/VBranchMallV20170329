using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.AlipayAssure
{
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;

    internal class AlipayAssureNotify : NotifyQuery
    {
        private string input_charset = "utf-8";
        private NameValueCollection parameters;

        public AlipayAssureNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        private string CreateUrl(PayeeInfo payee)
        {
            return string.Format(CultureInfo.InvariantCulture, "https://mapi.alipay.com/gateway.do?service=notify_verify&partner={0}&notify_id={1}", new object[] { payee.SellerAccount, this.parameters["notify_id"] });
        }

        public override string GetGatewayOrderId()
        {
            return this.parameters["trade_no"];
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["total_fee"]);
        }

        public override string GetNotifyId()
        {
            return "alipayassure_" + this.parameters["notify_id"].ToString();
        }

        public override string GetOrderId()
        {
            return this.parameters["out_trade_no"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            bool flag;
            try
            {
                flag = bool.Parse(this.GetResponse(this.CreateUrl(payee), timeout));
            }
            catch
            {
                flag = false;
            }
            
            foreach (string tmpParameter in Core.Globals.AlipayOtherParamKeys)
            {
                if (!string.IsNullOrEmpty(tmpParameter))
                {
                    this.parameters.Remove(tmpParameter);
                }
            }
            string[] strArray2 = Globals.BubbleSort(this.parameters.AllKeys);
            string s = "";
            for (int i = 0; i < strArray2.Length; i++)
            {
                if ((!string.IsNullOrEmpty(this.parameters[strArray2[i]]) && (strArray2[i] != "sign")) && (strArray2[i] != "sign_type"))
                {
                    if (i == (strArray2.Length - 1))
                    {
                        s = s + strArray2[i] + "=" + this.parameters[strArray2[i]];
                    }
                    else
                    {
                        s = s + strArray2[i] + "=" + this.parameters[strArray2[i]] + "&";
                    }
                }
            }
            s = s + payee.PrimaryKey;
            if (flag && this.parameters["sign"].Equals(Globals.GetMD5(s, this.input_charset)))
            {
                string str2 = this.parameters["trade_status"];
                if (str2 != null)
                {
                    if (!(str2 == "WAIT_SELLER_SEND_GOODS"))
                    {
                        if (!(str2 == "TRADE_FINISHED"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        this.OnPaidToIntermediary();
                        return;
                    }
                    this.OnPaidToMerchant();
                }
            }
            else
            {
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

