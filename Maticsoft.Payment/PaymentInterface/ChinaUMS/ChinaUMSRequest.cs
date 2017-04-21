using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;
using GL.Payment;
using GL.Payment.UnionPay;
using Maticsoft.Payment.BLL;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Security;
using Maticsoft.Payment.Handler;

namespace Maticsoft.Payment.PaymentInterface.ChinaUMS
{
    internal class ChinaUMSRequest : PaymentRequest
    {
        //异步通知接口
        private string returnUrl;
        private string notifyUrl;
        private string orderId;
        private decimal orderPayAmount;
        private  UnionPayChannel paymentObject;

       // private string paymentUrl = "";

        private string orderDesc;

        /// <summary>
        /// 实例化并转成个性化的参数
        /// </summary>
        /// <param name="payee"></param>
        /// <param name="gateway"></param>
        /// <param name="trade"></param>
        public ChinaUMSRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            returnUrl = gateway.ReturnUrl;
            notifyUrl = gateway.NotifyUrl;
            orderId = trade.OrderId;
            orderPayAmount = trade.TotalMoney;
            orderDesc = trade.Subject;

            paymentObject = new UnionPayChannel();
        }

        public override void SendRequest()
        {
            var result =  paymentObject.Payment(orderId, orderDesc, orderPayAmount);
            //保存这个订单
            if (result.IsSuccess)
            {
                var paymentResult = (Dictionary<string, string>)result.Data;
                var paymentResult2 = (Dictionary<string, string>)result.OtherData;

                var order = new ChinaUMSOrder();
                order.IsSuccess = 1;
                order.TransCode = paymentResult2["TransCode"];
                order.OrderDate = System.DateTime.Now;
                order.MerOrderId = paymentResult2["MerOrderId"];
                order.TransType = paymentResult2["TransType"];
                order.TransAmt = System.Convert.ToDecimal(paymentResult2["TransAmt"]) * 0.01M;
                order.MerId = paymentResult2["MerId"];
                order.MerTermId = paymentResult2["MerTermId"];
                order.NotifyUrl = paymentResult2["NotifyUrl"];
                order.Reserve = paymentResult2["Reserve"];
                order.OrderDesc = paymentResult2["OrderDesc"];
                order.EffectiveTime = System.Convert.ToInt16(paymentResult2["EffectiveTime"]);
                order.RespCode = paymentResult2["RespCode"];
                order.Msg = paymentResult2["Msg"];
                order.OrderId = paymentResult2["OrderId"];



                order.ChrCode = paymentResult["ChrCode"];
                order.TransId = paymentResult["TransId"];

                PaymentModeManage.SaveChinaUMSOrder(order);

                //跳转到银商收银台
                StringBuilder sbContent = new StringBuilder();
                sbContent.Append(this.CreateField("merSign", paymentResult["MerSign"]));
                sbContent.Append(this.CreateField("chrCode", paymentResult["ChrCode"]));
                sbContent.Append(this.CreateField("tranId", paymentResult["TransId"]));
                sbContent.Append(this.CreateField("url", this.returnUrl));
                this.SubmitPaymentForm(this.CreateForm(UnionPayConfig.PaymentURL, sbContent.ToString()));
            }
            else
            {
                // 下单失败处理
                ErrorLogTxt.GetInstance("支付返回日志").Write(result.Msg);
                System.Web.HttpContext.Current.Response.Redirect(Maticsoft.Payment.Core.Globals.FullPath(@"/"), false);
            }
        }

      
    }
}

