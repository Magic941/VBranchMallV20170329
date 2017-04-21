using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.AlipayDirect
{
    internal class AlipayDirectNotify : NotifyQuery
    {
        private string input_charset = "utf-8";
        private NameValueCollection parameters;

        public AlipayDirectNotify(NameValueCollection parameters)
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
        /// <summary>
        /// 获取支付流水号
        /// </summary>
        /// <returns></returns>
        public override string GetNotifyId()
        {
            return "alipaydirect_" + this.parameters["notify_id"].ToString();//此处填写网银支付通知参数中的流水号
        }
        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["total_fee"]);
        }

        public override string GetOrderId()
        {
            return this.parameters["out_trade_no"];
        }
        /// <summary>
        /// 阿里个性化的较验，每种支付方式都有自已的地址和较验方式，可以通过配置获取。
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="payee"></param>
        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            bool flag;
            try
            {

                //测试是先置为true;
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
            flag = flag && this.parameters["sign"].Equals(Globals.GetMD5(s, this.input_charset));
            string str2 = this.parameters["trade_status"];
            if (flag && ((str2 == "TRADE_SUCCESS") || (str2 == "TRADE_FINISHED")))
            {
                this.OnPaidToMerchant();
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
