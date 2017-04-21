using Maticsoft.Payment.Handler;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.Security;
using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;
using GL.Payment.UnionPay;
using System.Collections.Generic;
using System.Linq;

namespace Maticsoft.Payment.PaymentInterface.ChinaUMS
{
    internal class ChinaUMSNotify : NotifyQuery
    {
        private UnionPayChannel paymentObject;
        private NameValueCollection parameters;

        /// <summary>
        /// 同步通知 参数列表:
        /// transId     :   交易流水
        /// merchantOrderId  :   商户订单号
        /// bankcard    :   交易卡号
        /// respCode    :   响应代码
        /// respDesc    :   响应描述
        /// amount      :   交易金额(注意:返回单位为"元",具体使用时不需要再乘以100)
        /// merId       :   商户号
        /// --------------------------------------------------------------------------------------------
        /// 异步通知 参数列表:
        /// OrderTime   :   订单时间,例如:130224
        /// LiqDate     :   清算日期,例如:20141219
        /// memo        :   例如:好邻商城-贴心服务、优惠返利、保险赠送、轻松购物订单-订单号：[20141219130221461]-在线支付-订单支付金额：0.01元
        /// IsNew       :   例如:N
        /// MerTermId   :   终端号,例如:99999999
        /// Account     :   支付卡号,例如:6214830212495238
        /// RespMsg     :   响应信息,例如:订单查询成功
        /// MerName     :   商户名称,例如:Pem文件全民捷付测试商户
        /// Signature   :   银商签名数据,例如:5ABC907FF2E3CC7D1E23B6DE8FFFA26E793637268D98F40DCF321131DF9DB443E8275D75E77240644498657D4C007CA2977B10A0597CF2155D69A56F9D7D6A7589B471A9655442097681C3D86F2F756A2115FCF4F1E92C2BBDDE5120F12ABAE496E1207B091D093579C12B19F5D731130B3064C2419BFB4039D1121EC542FEB3
        /// RespCode    :   响应码,例如:00000
        /// RefId       :   系统检索号,例如:130258290288
        /// TransId     :   交易流水号,例如:662014121926916552
        /// TransState  :   交易状态,例如:1
        /// TransType   :   交易类型,例如:NoticePay
        /// PayState    :   支付状态,例如:1
        /// Reserve     :   备用字段,例如:
        /// TransDesc   :   交易描述,例如:交易成功
        /// TransAmt    :   交易金额(注意:返回单位为"分",具体使用时需要除以100将单位转化成"元"),例如:1
        /// OrderDate   :   订单日期,例如:20141219
        /// MerOrderId  :   商户订单号,例如:HaolinShopMallTest_3806;
        /// TransCode   :   交易代码,例如:201202
        /// NotifyUrl   :   商户异步通知地址,例如:http://www.zhenhaolin.com:8080/Pay/Payment/Notify_url.aspx?MATICSOFTGW=Y2hpbmF1bXM_
        /// payType     :   支付类型,例如:1
        /// MerId       :   商户号,例如:898000093990002
        /// </summary>
        /// <param name="parameters"></param>
        public ChinaUMSNotify(NameValueCollection parameters)
        {
            paymentObject = new UnionPayChannel();
            this.parameters = parameters;
            try
            {
                foreach (string p in this.parameters.Keys)
                    if (!string.IsNullOrEmpty(p)) ErrorLogTxt.GetInstance("打印参数").Write(p + "," + this.parameters[p]);
            }
            catch (Exception e)
            {
                ErrorLogTxt.GetInstance("支付返回日志").Write("参数打印异常" + e.Message);
            }
        }

        /// <summary>
        /// 由于全民付同步返回的金额和异步返回的金额参数名称不一致
        /// 同步返回时参数名为:amount
        /// 异步返回时参数名为:transAmt
        /// 所以需要特殊处理一下
        /// </summary>
        /// <returns></returns>
        public override decimal GetOrderAmount()
        {
            return null == this.parameters["amount"] ? decimal.Parse(this.parameters["transAmt"]) / 100 : decimal.Parse(this.parameters["amount"]);
        }

        public override string GetNotifyId()
        {
            return "chinaums_" + this.parameters["transId"] == null ? "" : this.parameters["transId"].ToString();
        }

        /// <summary>
        /// 由于全民付同步返回的金额和异步返回的商户订单号参数名称不一致
        /// 同步返回时参数名为:merchantOrderId
        /// 异步返回时参数名为:merOrderId
        /// 所以需要特殊处理一下
        /// </summary>
        /// <returns></returns>
        public override string GetOrderId()
        {
            return (null == this.parameters["merchantOrderId"] ? this.parameters["merOrderId"] : this.parameters["merchantOrderId"]);
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            #region 
            //v_oid = Request["v_oid"];
            //v_pstatus = Request["v_pstatus"];
            //v_pstring = Request["v_pstring"];
            //v_pmode = Request["v_pmode"];
            //v_md5str = Request["v_md5str"];
            //v_amount = Request["v_amount"];
            //v_moneytype = Request["v_moneytype"];
            //remark1 = Request["remark1"];
            //remark2 = Request["remark2"];

            //v_oid = this.parameters["v_oid"];
            //v_pstatus = this.parameters["v_pstatus"];
            //v_pstring = this.parameters["v_pstring"];
            //v_pmode = this.parameters["v_pmode"];
            //v_md5str = this.parameters["v_md5str"];
            //v_amount = this.parameters["v_amount"];
            //v_moneytype = this.parameters["v_moneytype"];
            //remark1 = this.parameters["remark1"];
            //remark2 = this.parameters["remark2"];
            //key = payee.PrimaryKey;

            //if ((((v_oid == null) || (v_pstatus == null)) || ((v_pstring == null) || (v_pmode == null))) || (((v_md5str == null) || (v_amount == null)) || ((v_moneytype == null) || (remark1 == null))))
            //{
            //    ErrorLogTxt.GetInstance("支付返回日志").Write("空失败!");
            //    this.OnNotifyVerifyFaild();
            //}
            //else if (!v_pstatus.Equals("20"))
            //{
            //    ErrorLogTxt.GetInstance("支付返回日志").Write("状态失败!");
            //    this.OnNotifyVerifyFaild();
            //}
            //else if (!v_md5str.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(v_oid + v_pstatus + v_amount + v_moneytype + key, "MD5").ToUpper(CultureInfo.InvariantCulture)))
            //{
            //    ErrorLogTxt.GetInstance("支付返回日志").Write("加密不对失败!");
            //    this.OnNotifyVerifyFaild();
            //}
            //else
            //{
            //    ErrorLogTxt.GetInstance("支付返回日志").Write("正常!");
            //    this.OnPaidToMerchant();
            //}
            #endregion

            try
            {
                if (paymentObject.CallbackVerify(NameValueCollection2Dictionary(parameters)))
                {
                    ErrorLogTxt.GetInstance("支付返回日志").Write("正常!");
                    this.OnPaidToMerchant();
                    // 测试订单查询接口
                    //this.TestQuerySingleOrder(parameters);
                }
                else
                {
                    ErrorLogTxt.GetInstance("支付返回日志").Write("验签失败!");
                    this.OnNotifyVerifyFaild();
                }
            }
            catch (Exception ex)
            {
                ErrorLogTxt.GetInstance("支付返回日志").Write("支付失败!");
                this.OnNotifyVerifyFaild();
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

        /// <summary>
        /// 将NameValueCollection转换为Dictionary
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private Dictionary<string, string> NameValueCollection2Dictionary(NameValueCollection collection)
        {
            Dictionary<string, string> retDic = new Dictionary<string, string>();
            if (null == collection || collection.Count <= 0) return retDic;
            foreach (string key in collection.Keys)
            {
                if (!string.IsNullOrEmpty(key)) retDic.Add(key, collection[key]);
            }
            return retDic;
        }

        /// <summary>
        /// 测试订单查询接口
        /// </summary>
        /// <param name="inParams"></param>
        //private void TestQuerySingleOrder(NameValueCollection inParams)
        //{
        //    try
        //    {
        //        GL.Payment.PayMessage msg = paymentObject.QuerySingleOrder(NameValueCollection2Dictionary(inParams));
        //        ErrorLogTxt.GetInstance("全民付查询订单-返回日志").Write("查询结果:" + msg.Msg);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogTxt.GetInstance("全民付查询订单-返回日志").Write("异常:" + ex.Message);
        //    }
        //}
    }
}

