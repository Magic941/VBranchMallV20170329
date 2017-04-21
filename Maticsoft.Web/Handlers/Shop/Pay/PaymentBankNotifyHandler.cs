using Maticsoft.Model.Shop.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maticsoft.Web.Handlers.Shop.Pay
{
    public class PaymentBankNotifyHandler : HandlerBase
    {
        public override bool IsReusable
        {
            get { return false; }
        }

        protected string v_oid; //订单号
        protected string v_pstatus; //支付状态码
        //20（支付成功，对使用实时银行卡进行扣款的订单）；
        //30（支付失败，对使用实时银行卡进行扣款的订单）；

        protected string v_pstring; //支付状态描述
        protected string v_pmode; //支付银行
        protected string v_md5str; //MD5校验码
        protected string v_amount; //支付金额
        protected string v_moneytype; //币种		
        protected string remark1;//备注1
        protected string remark2;//备注1
        protected string status_msg;//备注1

        public override void ProcessRequest(HttpContext context)
        {
            try
            {
                string key = "zhenhaolin2014";

                v_oid = context.Request["v_oid"];
                v_pstatus = context.Request["v_pstatus"];
                v_pstring = context.Request["v_pstring"];
                v_pmode = context.Request["v_pmode"];
                v_md5str = context.Request["v_md5str"];
                v_amount = context.Request["v_amount"];
                v_moneytype = context.Request["v_moneytype"];
                remark1 = context.Request["remark1"];
                remark2 = context.Request["remark2"];



                string str = v_oid + v_pstatus + v_amount + v_moneytype + key;
                str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();

                Maticsoft.Payment.Model.IPaymentOption<OrderInfo> Option = new PaymentOption();


                OrderInfo Order = Option.GetOrderInfo(v_oid);

                if (str == v_md5str)
                {
                    if (v_pstatus.Equals("20"))
                    {
                        //支付成功
                        //商户系统的逻辑处理（例如判断金额，判断支付状态，更新订单状态等等）.......

                        //Option.PayForOrder(Order);
                        RESULT_MSG = string.Format(RESULT_MSG, DateTime.Now, "王中玉", v_oid, v_pstatus == "20" ? "支付成功" : "支付失败", v_pstring, "通过", v_amount);
                    }
                }
                else
                {
                    RESULT_MSG = string.Format(RESULT_MSG, DateTime.Now, "王中玉", v_oid, v_pstatus == "20" ? "支付成功" : "支付失败", v_pstring, "未通过", v_amount);
                }
            }
            catch (Exception ex)
            {
                RESULT_MSG = string.Format(RESULT_MSG + ",程序错误信息：{7}", DateTime.Now, "王中玉", v_oid, v_pstatus == "20" ? "支付成功" : "支付失败", v_pstring, "通过", v_amount, ex);
            }
            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("网银支付日志").Write(RESULT_MSG);
        }
    }
}