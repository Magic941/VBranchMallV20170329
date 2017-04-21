using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GL.Payment.Common;



namespace GL.Payment.UnionPay
{
    /// <summary>
    /// 银联无卡支付
    /// </summary>
    public class UnionPayChannel : PayChannel
    {
        /// <summary>
        /// 付款操作
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-07
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="productName">商品名称</param>
        /// <param name="amount">支付金额</param>
        /// <returns></returns>
        public override PayMessage Payment(string orderId, string productName, decimal amount)
        {
            var message = new PayMessage() { IsSuccess = true };
            var client = new HttpClient(UnionPayConfig.OrderUrl);
            var now = DateTime.Now;
            // 参数组装
            var inParams = new Dictionary<string, string>
            {
                {"TransCode", "201201"},
                {"OrderTime", now.ToString("HHmmss")},
                {"OrderDate", now.ToString("yyyyMMdd")},
                {"MerOrderId", orderId}, 
                {"TransType", "NoticePay"},
                {"TransAmt", (amount*100).ToString("F0")},
                {"MerId", UnionPayConfig.MerId},
                {"MerTermId", UnionPayConfig.MerTermId},
                {"NotifyUrl", UnionPayConfig.NotifyUrl},
                {"Reserve", orderId},
                {"OrderDesc", productName},
                {"EffectiveTime", "0"}
            };
            var signContent = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", inParams["OrderTime"], inParams["EffectiveTime"], inParams["OrderDate"], inParams["MerOrderId"], inParams["TransType"], inParams["TransAmt"], inParams["MerId"], inParams["MerTermId"], inParams["NotifyUrl"], inParams["Reserve"], inParams["OrderDesc"]);
            var merSign = RSAUtils.RsaSign(signContent, UnionPayConfig.PrivateKey);
            inParams.Add("MerSign", merSign);
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(inParams);
            client.PostingData.Add("jsonString", jsonString);
            //Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("全民付-返回日志").Write("下单请求报文:" + jsonString);
            var result = client.GetString();
            //Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("全民付-返回日志").Write("下单响应报文:" + result);
            if (string.IsNullOrEmpty(result))
            {
                message.IsSuccess = false;
                message.Msg = "调用支付下单接口无数据返回。";
                return message;
            }
            var dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            // 判断是否下单成功
            var respCode = dictionary["RespCode"];
            if (respCode != "00000")
            {
                message.IsSuccess = false;
                message.Msg = dictionary["RespMsg"];
                return message;
            }
            // 组装验签字符串
            var content = string.Format("{0}{1}{2}{3}{4}{5}", dictionary["MerOrderId"], dictionary["ChrCode"],
                          dictionary["TransId"], dictionary["Reserve"].Trim(), dictionary["RespCode"], dictionary["RespMsg"].Trim());
            var r = RSAUtils.Verify(content, dictionary["Signature"], UnionPayConfig.PublicKey);
            if (!r)
            {
                message.IsSuccess = false;
                message.Msg = "下单成功，返回数据签名验证失败。";
                return message;
            }
            var chrCode = dictionary["ChrCode"];
            var transId = dictionary["TransId"];
            inParams.Add("ChrCode", chrCode);
            inParams.Add("TransId", transId);
            // 将返回消息添加到集合中
            inParams.Add("RespCode", respCode);
            inParams.Add("Msg", string.IsNullOrEmpty(message.Msg) ? string.Empty : message.Msg);
            merSign = RSAUtils.RsaSign(string.Format("{0}{1}", transId, chrCode), UnionPayConfig.PrivateKey);
            inParams["MerSign"] = merSign;
            // 处理各个支付渠道对应的订单编码
            // 以下处理是根据自己业务处理，添加业务订单号
            if (!inParams.ContainsKey("OrderId"))
                inParams.Add("OrderId", inParams["MerOrderId"]);
            var data = new Dictionary<string, string>
            {
                {"MerSign",merSign},
                {"ChrCode", chrCode},
                {"TransId",transId},
                {"MerchantId",UnionPayConfig.MerId}
            };
            message.Data = data;
            message.OtherData = inParams;
            return message;
        }

        /// <summary>
        /// 订单查询
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-06
        /// </summary>
        /// <param name="inParams">查询参数集合（k1:v1,k2:v2）</param>
        /// <returns></returns>
        public override PayMessage QuerySingleOrder(Dictionary<string, string> inParams)
        {
            var message = new PayMessage() { IsSuccess = true, Msg = "" };
            var now = DateTime.Now;
            var dict = new Dictionary<string, string>
            {
                {"TransCode", "201203"},
                {"ReqTime", now.ToString("yyyyMMddHHmmss")},
                {"OrderDate", inParams["OrderDate"]},
                {"MerOrderId", inParams["MerOrderId"]},
                {"TransId", inParams["TransId"]},
                {"MerId", UnionPayConfig.MerId},
                {"MerTermId", UnionPayConfig.MerTermId},
                {"Reserve", inParams["Reserve"]}
            };
            var content = string.Format("{0}{1}{2}{3}{4}{5}{6}", dict["ReqTime"], dict["OrderDate"], dict["MerOrderId"], dict["TransId"], dict["MerId"], dict["MerTermId"], dict["Reserve"]);
            var sign = RSAUtils.RsaSign(content, UnionPayConfig.PrivateKey);
            dict.Add("MerSign", sign);
            var client = new HttpClient(UnionPayConfig.QueryOrderUrl);
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
            client.PostingData.Add("jsonString", jsonString);
            //Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("全民付-返回日志").Write("查询请求报文:" + jsonString);
            var result = client.GetString();
            //Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("全民付-返回日志").Write("查询响应报文:" + result);
            if (string.IsNullOrEmpty(result))
            {
                message.IsSuccess = false;
                message.Msg = "调用支付查询订单接口无数据返回。";
                return message;
            }
            var dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            var respCode = dictionary["RespCode"];
            // 判断是否下单成功
            if (respCode != "00000")
            {
                message.IsSuccess = false;
                message.Msg = dictionary["RespMsg"];
                return message;
            }
            var transState = dictionary["TransState"];
            if (transState != "1")
            {
                message.IsSuccess = false;
                message.Msg = string.Format("订单查询成功，银联订单系统返回交易状态为：【{0}】，交易状态说明（0:新订单 1:付款成功 2:付款失败 3:支付中）", transState);
                return message;
            }
            // 验签（RefId：系统检索号 支付成功会存在）
            // 组装验签字符串
            var verifySignContent = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}", dictionary["OrderTime"], dictionary["OrderDate"], dictionary["MerOrderId"], dictionary["TransType"], dictionary["TransAmt"], dictionary["MerId"], dictionary["MerTermId"], dictionary["TransId"], dictionary["TransState"], dictionary["RefId"], dictionary["Reserve"], dictionary["RespCode"], dictionary["RespMsg"]);
            var r = RSAUtils.Verify(verifySignContent, dictionary["Signature"], UnionPayConfig.PublicKey);
            if (!r)
            {
                message.IsSuccess = false;
                message.Msg = "订单查询成功，返回数据签名验证失败。";
                return message;
            }
            message.Data = dictionary;
            return message;
        }

        /// <summary>
        /// 验证支付成功后台服务通知
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-06
        /// </summary>
        /// <param name="inParams">银联传送过来的参数信息</param>
        /// <returns></returns>
        public override bool CallbackVerify(Dictionary<string, string> inParams)
        {
            //Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("全民付-返回日志").Write("通知请求报文:" + Newtonsoft.Json.JsonConvert.SerializeObject(inParams));
            // 组装验签内容信息
            var signField = new string[] { "OrderTime", "OrderDate", "MerOrderId", "TransType", "TransAmt", "MerId", "MerTermId", "TransId", "TransState", "RefId", "Account", "TransDesc", "Reserve" };
            var sbSign = new StringBuilder();
            foreach (var s in signField)
            {
                var r = inParams.Keys.Any(n => String.Equals(n, s.ToLower(), StringComparison.CurrentCultureIgnoreCase));
                if (!r) continue;
                var dict = inParams.FirstOrDefault(n => String.Equals(n.Key, s.ToLower(), StringComparison.CurrentCultureIgnoreCase));
                sbSign.Append(dict.Value);
            }
            // 判断验签是否成功
            var result = RSAUtils.Verify(sbSign.ToString(), inParams["Signature"], UnionPayConfig.PublicKey);
            if (result)
            {
                // 验签成功后，向银联服务发送接收通知消息响应请求
                this.NotifyResponse(inParams);
            }
            return result;
        }

        /// <summary>
        /// 将接收到的通知信息向银联服务器响应
        /// 创建用户：shiyuankao
        /// 创建时间：2014-08-06
        /// </summary>
        /// <param name="inParams">银联传送过来的参数信息</param>
        private void NotifyResponse(Dictionary<string, string> inParams)
        {
            try
            {
                var signField = new string[] { "OrderTime", "OrderDate", "MerOrderId", "TransType", "TransAmt", "MerId", "MerTermId", "TransId", "TransState", "RefId", "Account", "TransDesc", "Reserve" };
                var sbSign = new StringBuilder();
                foreach (var s in signField)
                {
                    sbSign.Append(inParams[s]);
                }
                var merSign = RSAUtils.RsaSign(sbSign.ToString(), UnionPayConfig.PrivateKey);
                var responseObject = new
                {
                    TransCode = "201202",
                    MerOrderId = inParams["MerOrderId"],
                    TransType = "NoticePay",
                    MerId = inParams["MerId"],
                    MerTermId = inParams["MerTermId"],
                    TransId = inParams["TransId"],
                    MerPlatTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    MerOrderState = "00",//00销账成功 11销账失败
                    Reserve = inParams["Reserve"],
                    MerSign = merSign
                };
                var response = HttpContext.Current.Response;
                //Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("全民付-返回日志").Write("通知响应报文:" + Newtonsoft.Json.JsonConvert.SerializeObject(responseObject));
                response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(responseObject));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
