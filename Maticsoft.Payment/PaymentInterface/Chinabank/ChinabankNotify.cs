using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.Chinabank
{
    using Maticsoft.Payment.Handler;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Web.Security;

    internal class ChinabankNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        protected string key;

        protected string v_oid; //订单号
        protected string v_pstatus; //支付状态码

        protected string v_pstring; //支付状态描述
        protected string v_pmode; //支付银行
        protected string v_md5str; //MD5校验码
        protected string v_amount; //支付金额
        protected string v_moneytype; //币种		
        protected string remark1;//备注1
        protected string remark2;//备注1
        protected string status_msg;//备注1

        public ChinabankNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
            try
            {
                foreach (var p in this.parameters.Keys)
                    ErrorLogTxt.GetInstance("打印参数").Write(p.ToString() + "," + this.parameters[p.ToString()]);
            }
            catch (Exception e) {

                ErrorLogTxt.GetInstance("支付返回日志").Write("参数打印异常"+e.Message);
            
            }
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["v_amount"]);
        }
        public override string GetNotifyId()
        {
            //ErrorLogTxt.GetInstance("流水号").Write("开始打印流水号");
            //ErrorLogTxt.GetInstance("流水号").Write("流水号："+this.parameters["v_idx"]+";流水号若为空则出错");
            return "chinabank_" + this.parameters["TranSerialNum"] == null ? "v_idx_error" : this.parameters["TranSerialNum"].ToString();
            //return "111";
        }

        public override string GetOrderId()
        {
            return this.parameters["v_oid"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            //v_oid = Request["v_oid"];
            //v_pstatus = Request["v_pstatus"];
            //v_pstring = Request["v_pstring"];
            //v_pmode = Request["v_pmode"];
            //v_md5str = Request["v_md5str"];
            //v_amount = Request["v_amount"];
            //v_moneytype = Request["v_moneytype"];
            //remark1 = Request["remark1"];
            //remark2 = Request["remark2"];

            v_oid = this.parameters["v_oid"];
            v_pstatus = this.parameters["v_pstatus"];
            v_pstring = this.parameters["v_pstring"];
            v_pmode = this.parameters["v_pmode"];
            v_md5str = this.parameters["v_md5str"];
            v_amount = this.parameters["v_amount"];
            v_moneytype = this.parameters["v_moneytype"];
            remark1 = this.parameters["remark1"];
            key = payee.PrimaryKey;

            if ((((v_oid == null) || (v_pstatus == null)) || ((v_pstring == null) || (v_pmode == null))) || (((v_md5str == null) || (v_amount == null)) || ((v_moneytype == null) || (remark1 == null))))
            {
                ErrorLogTxt.GetInstance("支付返回日志").Write("空失败!");
                this.OnNotifyVerifyFaild();
            }
            else if (!v_pstatus.Equals("20"))
            {
                ErrorLogTxt.GetInstance("支付返回日志").Write("状态失败!");
                this.OnNotifyVerifyFaild();
            }
            else if (!v_md5str.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(v_oid + v_pstatus + v_amount + v_moneytype + key, "MD5").ToUpper(CultureInfo.InvariantCulture)))
            {
                ErrorLogTxt.GetInstance("支付返回日志").Write("加密不对失败!");
                this.OnNotifyVerifyFaild();
            }
            else
            {
                ErrorLogTxt.GetInstance("支付返回日志").Write("正常!");
                this.OnPaidToMerchant();
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
            if (context != null)
            {
                context.Response.Clear();
                context.Response.Write(success ? "ok" : "error");
                context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}

