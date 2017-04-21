using Maticsoft.Payment.PaymentInterface.WeChat.Models;
using Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Maticsoft.Payment.PaymentInterface.WeChat.Utils
{
    public class UnifiedWxPayHelper
    {
        #region 构造函数、私有变量
        private UnifiedWxPayHelper()
        {
            this.parameters = new Dictionary<string, string>();
        }

        private Dictionary<string, string> parameters;
        public string AppId;
        private string Key;
        public string PartnerId;

        #endregion

        #region 创建UnifiedPrePayModel
        public static UnifiedWxPayHelper CreateUnifiedHelper(string appId, string partnerId, string key)
        {
            UnifiedWxPayHelper wxPayModel = new UnifiedWxPayHelper();
            //先设置基本信息
            wxPayModel.SetAppId(appId);
            wxPayModel.SetKey(key);
            wxPayModel.SetPartnerId(partnerId);

            return wxPayModel;
        }
        #endregion

        #region 参数操作相关
        public void SetAppId(string str)
        {
            AppId = str;
        }

        public void SetPartnerId(string str)
        {
            PartnerId = str;
        }

        public void SetKey(string str)
        {
            Key = str;
        }

        #endregion

        #region 生成订单详情package、支付签名

        /// <summary>
        /// MD5签名
        /// </summary>
        /// <returns></returns>
        public string GetCftPackage(Dictionary<string, string> bizObj)
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new Exception("Key为空！");
            }
            string unSignParaString = CommonUtil.FormatBizQueryParaMapForUnifiedPay(bizObj);
            return MD5SignUtil.Sign(unSignParaString, Key);
        }

        public bool ValidateMD5Signature(Dictionary<string, string> bizObj, string sign)
        {
            string signValue = GetCftPackage(bizObj);
            return signValue == sign;
        }

        #endregion

        #region 生成 预支付 请求参数（XML）
        /// <summary>
        /// 生成 预支付 请求参数（XML）
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tradeNo"></param>
        /// <param name="totalFee"></param>
        /// <param name="createIp"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string CreatePrePayPackage(string description, string tradeNo, string totalFee, string createIp, string notifyUrl, string openid)
        {
            Dictionary<string, string> nativeObj = new Dictionary<string, string>();
            nativeObj.Add("appid", AppId);
            nativeObj.Add("mch_id", PartnerId);
            nativeObj.Add("nonce_str", CommonUtil.CreateNoncestr());
            nativeObj.Add("body", description);
            nativeObj.Add("out_trade_no", tradeNo);
            nativeObj.Add("total_fee", totalFee);
            nativeObj.Add("spbill_create_ip", createIp);
            nativeObj.Add("notify_url", notifyUrl);
            nativeObj.Add("trade_type", "JSAPI");
            nativeObj.Add("openid", openid);
            //nativeObj.Add("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); // 可选参数
            //nativeObj.Add("time_expire", DateTime.Now.AddYears(50).ToString("yyyyMMddHHmmss")); // 可选参数
            nativeObj.Add("sign", GetCftPackage(nativeObj));
            return DictionaryToXmlString(nativeObj);
        }

        /// <summary>
        /// 生成 预支付 请求参数（XML） - 原生支付(模式二)
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tradeNo"></param>
        /// <param name="totalFee"></param>
        /// <param name="createIp"></param>
        /// <param name="notifyUrl"></param>
        /// <returns></returns>
        public string CreateNativePrePayPackage(string description, string tradeNo, string totalFee, string createIp, string notifyUrl)
        {
            Dictionary<string, string> nativeObj = new Dictionary<string, string>();
            nativeObj.Add("appid", AppId);
            nativeObj.Add("mch_id", PartnerId);
            nativeObj.Add("nonce_str", CommonUtil.CreateNoncestr());
            nativeObj.Add("body", description);
            nativeObj.Add("out_trade_no", tradeNo);
            nativeObj.Add("total_fee", totalFee);
            nativeObj.Add("spbill_create_ip", createIp);
            nativeObj.Add("notify_url", notifyUrl);
            nativeObj.Add("trade_type", "NATIVE");
            nativeObj.Add("product_id", tradeNo);
            nativeObj.Add("sign", GetCftPackage(nativeObj));
            return DictionaryToXmlString(nativeObj);
        }

        #endregion

        #region 创建订单查询 XML
        /// <summary>
        /// 创建订单查询 XML
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public string CreateOrderQueryXml(string orderNo)
        {
            Dictionary<string, string> nativeObj = new Dictionary<string, string>();

            nativeObj.Add("appid", AppId);
            nativeObj.Add("mch_id", PartnerId);
            nativeObj.Add("out_trade_no", orderNo);
            nativeObj.Add("nonce_str", CommonUtil.CreateNoncestr());
            nativeObj.Add("sign", GetCftPackage(nativeObj));

            return DictionaryToXmlString(nativeObj);
        }
        #endregion

        #region 创建订单退款 XML
        /// <summary>
        /// 创建订单退款 XML
        /// </summary>
        /// <param name="orderNo">商户订单号</param>
        /// <param name="transactionId">微信订单号</param>
        /// <param name="totalFee">总金额</param>
        /// <param name="refundNo">退款订单号</param>
        /// <param name="refundFee">退款金额</param>
        /// <returns></returns>
        public string CreateOrderRefundXml(string orderNo, string transactionId, string totalFee, string refundNo, string refundFee)
        {
            Dictionary<string, string> nativeObj = new Dictionary<string, string>();

            nativeObj.Add("appid", AppId);
            nativeObj.Add("mch_id", PartnerId);
            nativeObj.Add("nonce_str", CommonUtil.CreateNoncestr());
            if (string.IsNullOrEmpty(transactionId))
            {
                if (string.IsNullOrEmpty(orderNo))
                    throw new Exception("缺少订单号！");
                nativeObj.Add("out_trade_no", orderNo);
            }
            else
            {
                nativeObj.Add("transaction_id", transactionId);
            }

            nativeObj.Add("out_refund_no", refundNo);
            nativeObj.Add("total_fee", totalFee);
            nativeObj.Add("refund_fee", refundFee);
            nativeObj.Add("op_user_id", PartnerId); //todo:配置

            nativeObj.Add("sign", GetCftPackage(nativeObj));

            return DictionaryToXmlString(nativeObj);
        }

        #endregion

        #region dictionary与XmlDocument相互转换
        /// <summary>
        /// dictionary转为xml 字符串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static string DictionaryToXmlString(Dictionary<string, string> dic)
        {
            StringBuilder xmlString = new StringBuilder();
            xmlString.Append("<xml>");
            foreach (string key in dic.Keys)
            {
                xmlString.Append(string.Format("<{0}><![CDATA[{1}]]></{0}>", key, dic[key]));
            }
            xmlString.Append("</xml>");
            return xmlString.ToString();
        }

        /// <summary>
        /// xml字符串 转换为  dictionary
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Dictionary<string, string> XmlToDictionary(string xmlString)
        {
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(xmlString);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            var nodes = document.FirstChild.ChildNodes;

            foreach (System.Xml.XmlNode item in nodes)
            {
                dic.Add(item.Name, item.InnerText);
            }
            return dic;
        }
        #endregion

        #region V3

        #region V3 统一支付接口

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="isApp"></param>
        /// <returns></returns>
        public static UnifiedPrePayMessage UnifiedPrePay(string postData)
        {
            return HttpClientHelper.PostXmlResponse<UnifiedPrePayMessage>(WeiXinConst.WeiXin_Pay_UnifiedPrePayUrl, postData);
        }

        #endregion

        #region V3 订单查询

        /// <summary>
        /// V3 统一订单查询接口
        /// </summary>
        /// <param name="postXml"></param>
        /// <returns></returns>
        public static UnifiedOrderQueryMessage UnifiedOrderQuery(string postXml)
        {
            string url = WeiXinConst.WeiXin_Pay_UnifiedOrderQueryUrl;
            UnifiedOrderQueryMessage message = HttpClientHelper.PostXmlResponse<UnifiedOrderQueryMessage>(url, postXml);
            return message;
        }

        #endregion

        #region V3 申请退款

        /// <summary>
        /// 申请退款（V3接口）
        /// </summary>
        /// <param name="postData">请求参数</param>
        /// <param name="certPath">证书路径</param>
        /// <param name="certPwd">证书密码</param>
        public static bool Refund(string postData, string certPath, string certPwd)
        {
            string url = WeiXinConst.WeiXin_Pay_UnifiedOrderRefundUrl;
            RefundMessage message = RefundHelper.PostXmlResponse<RefundMessage>(url, postData, certPath, certPwd);
            return message.Success;
        }

        #endregion

        #endregion

        #region 获取AccessToken

        /// <summary>
        /// 获取Access_Token
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            string url = string.Format(WeiXinConst.AccessToken_Url, this.AppId, this.Key);
            string result = HttpClientHelper.GetResponse(url);
            return result;
        }

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 根据用户Code获取用户信息（包括OpenId的简单信息）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WeiXinUserSampleInfo GetUserSampleInfo(string code)
        {
            string url = string.Format(string.Format(WeiXinConst.User_GetOpenIdUrl, this.AppId, this.Key, "{0}"), code);
            WeiXinUserSampleInfo info = HttpClientHelper.GetResponse<WeiXinUserSampleInfo>(url);
            return info;
        }

        /// <summary>
        /// 根据用户Code获取用户信息（包括OpenId的简单信息）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetUserOpenId(string code)
        {
            return GetUserSampleInfo(code).OpenId;
        }

        #endregion
        
    }
}
