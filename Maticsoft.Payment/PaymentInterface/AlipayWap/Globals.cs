using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace Maticsoft.Payment.PaymentInterface.AlipayWap
{
    internal sealed class Globals
    {
        private Globals()
        {
        }

        internal static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }

        internal static string CreatParamUrl(string service, string partner, string key,
            string sign_type, string format, string v, string req_data, string inputCharset,
            string req_id = null)
        {
            int num;
            string[] strArray;

            if (string.IsNullOrEmpty(req_id))
            {
                strArray = new string[]
                {
                    "service=" + service,
                    "partner=" + partner,
                    "_input_charset=" + inputCharset,
                    "sec_id=" + sign_type,
                    "format=" + format,
                    "v=" + v,
                    "req_data=" + req_data
                };
            }
            else
            {
                strArray = new string[]
                {
                    "service=" + service,
                    "partner=" + partner,
                    "_input_charset=" + inputCharset,
                    "sec_id=" + sign_type,
                    "format=" + format,
                    "v=" + v,
                    "req_id=" + req_id,
                    "req_data=" + req_data
                };
            }

            string[] strArray2 = BubbleSort(strArray);
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < strArray2.Length; num++)
            {
                if (num == (strArray2.Length - 1))
                {
                    builder.Append(strArray2[num]);
                }
                else
                {
                    builder.Append(strArray2[num] + "&");
                }
            }
            builder.Append(key);
            string str = GetMD5(builder.ToString(), inputCharset);
            char[] separator = new char[] { '=' };
            StringBuilder builder2 = new StringBuilder();
            for (num = 0; num < strArray2.Length; num++)
            {
                builder2.Append(strArray2[num].Split(separator)[0] + "=" + HttpUtility.UrlEncode(strArray2[num].Split(separator)[1]) + "&");
            }
            builder2.Append("sign=" + str);
            return builder2.ToString();
        }


        internal static string CreatSendUrl(string gateway, string service, string partner, string key,
            string sign_type, string format, string v, string req_data, string inputCharset,
            string req_id = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(gateway);
            builder.Append(CreatParamUrl(service, partner, key, sign_type, format, v, req_data, inputCharset,
                req_id));
            return builder.ToString();
        }

        internal static string GetMD5(string s, string _input_charset)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 建立请求，以模拟远程HTTP的POST请求方式构造并获取支付宝的处理结果
        /// </summary>
        /// <returns>支付宝处理结果</returns>
        public static string BuildRequest(string url, string paramUrl, string input_charset)
        {
            Encoding code = Encoding.GetEncoding(input_charset);

            //把数组转换成流中所需字节数组类型
            byte[] bytesRequestData = code.GetBytes(paramUrl);

            //请求远程HTTP
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);

                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";

                //填充POST数据
                myReq.ContentLength = bytesRequestData.Length;
                using (Stream requestStream = myReq.GetRequestStream())
                {
                    requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
                    requestStream.Close();

                    //发送POST数据请求服务器
                    HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                    Stream myStream = HttpWResp.GetResponseStream();

                    //获取服务器返回信息
                    StreamReader reader = new StreamReader(myStream, code);
                    StringBuilder responseData = new StringBuilder();
                    String line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        responseData.Append(line);
                    }

                    //释放
                    myStream.Close();
                    strResult = responseData.ToString();
                }
            }
            catch (Exception exp)
            {
                strResult = "报错：" + exp.Message;
            }

            return strResult;
        }

        /// <summary>
        /// 解析远程模拟提交后返回的信息
        /// </summary>
        /// <param name="strText">要解析的字符串</param>
        /// <returns>解析结果</returns>
        public static NameValueCollection ParseResponse(string strText)
        {
            //以“&”字符切割字符串
            string[] strSplitText = strText.Split('&');
            //把切割后的字符串数组变成变量与数值组合的字典数组
            NameValueCollection param = new NameValueCollection();
            for (int i = 0; i < strSplitText.Length; i++)
            {
                //获得第一个=字符的位置
                int nPos = strSplitText[i].IndexOf('=');
                //获得字符串长度
                int nLen = strSplitText[i].Length;
                //获得变量名
                string strKey = strSplitText[i].Substring(0, nPos);
                //获得数值
                string strValue = strSplitText[i].Substring(nPos + 1, nLen - nPos - 1);
                //放入字典类数组中
                param.Add(strKey, strValue);
            }

            if (param["res_data"] != null)
            {
                //token从res_data中解析出来（也就是说res_data中已经包含token的内容）
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(param["res_data"]);
                    string strRequest_token = xmlDoc.SelectSingleNode("/direct_trade_create_res/request_token").InnerText;
                    param.Add("request_token", strRequest_token);
                }
                catch (Exception exp)
                {
                    param.Add("request_token", exp.ToString());
                }
            }

            return param;
        }
    }
}
