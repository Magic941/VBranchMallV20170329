namespace Maticsoft.Payment.PaymentInterface.WeChat.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Linq;
    using System.Web;
    using System.Text;
    using System.Collections.Specialized;
    internal class CommonUtil
    {

        public static String CreateNoncestr(int length)
        {
            String chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            String res = "";
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                res += chars[rd.Next(chars.Length - 1)];
            }
            return res;
        }

        public static String CreateNoncestr()
        {
            String chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            String res = "";
            Random rd = new Random();
            for (int i = 0; i < 16; i++)
            {
                res += chars[rd.Next(chars.Length - 1)];
            }
            return res;
        }

        public static string FormatQueryParaMap(Dictionary<string, string> parameters)
        {

            string buff = "";
            try
            {

                var result = from pair in parameters orderby pair.Key select pair;
                foreach (KeyValuePair<string, string> pair in result)
                {
                    if (pair.Key != "")
                    {
                        buff += pair.Key + "="
                                + HttpUtility.UrlEncode(pair.Value, Encoding.GetEncoding("UTF-8")) + "&";
                    }
                }
                if (buff.Length == 0 == false)
                {
                    buff = buff.Substring(0, (buff.Length - 1) - (0));
                }
            }
            catch (Exception e)
            {
                throw new SDKRuntimeException(e.Message);
            }

            return buff;
        }

        public static string FormatBizQueryParaMap(Dictionary<string, string> paraMap,
                bool urlencode)
        {

            string buff = "";
            try
            {
                var result = from pair in paraMap orderby pair.Key select pair;
                foreach (KeyValuePair<string, string> pair in result)
                {
                    if (pair.Key != "")
                    {

                        string key = pair.Key;
                        string val = pair.Value;
                        if (urlencode)
                        {
                            val = HttpUtility.UrlEncode(val, Encoding.GetEncoding("UTF-8"));
                        }
                        buff += key.ToLower() + "=" + val + "&";

                    }
                }

                if (buff.Length == 0 == false)
                {
                    buff = buff.Substring(0, (buff.Length - 1) - (0));
                }
            }
            catch (Exception e)
            {
                throw new SDKRuntimeException(e.Message);
            }
            return buff;
        }

        /// <summary>
        /// 统一支付  （参数组合）
        /// </summary>
        /// <param name="paraMap"></param>
        /// <param name="urlencode"></param>
        /// <returns></returns>
        public static string FormatBizQueryParaMapForUnifiedPay(Dictionary<string, string> paraMap)
        {
            string buff = "";
            try
            {
                var result = from pair in paraMap orderby pair.Key select pair;
                foreach (KeyValuePair<string, string> pair in result)
                {
                    if (pair.Key != "")
                    {

                        string key = pair.Key;
                        string val = pair.Value;
                        buff += key + "=" + val + "&";
                    }
                }

                if (buff.Length == 0 == false)
                {
                    buff = buff.Substring(0, (buff.Length - 1) - (0));
                }
            }
            catch (Exception e)
            {
                throw new SDKRuntimeException(e.Message);
            }
            return buff;
        }

        public static bool IsNumeric(String str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ArrayToXml(Dictionary<string, string> arr)
        {
            String xml = "<xml>";

            foreach (KeyValuePair<string, string> pair in arr)
            {
                String key = pair.Key;
                String val = pair.Value;
                if (IsNumeric(val))
                {
                    xml += "<" + key + ">" + val + "</" + key + ">";

                }
                else
                    xml += "<" + key + "><![CDATA[" + val + "]]></" + key + ">";
            }

            xml += "</xml>";
            return xml;
        }

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

        /// <summary>
        /// 将NameValueCollection转换为Dictionary
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Dictionary<string, string> NameValueCollection2Dictionary(NameValueCollection collection)
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
        /// 根据微信预支付订单号获取其创建日期
        /// </summary>
        /// <param name="prepayid"></param>
        /// <returns></returns>
        public static bool GetPrepayCreatedDate(string prepayid, out DateTime result)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"\D");
            string strTemp = reg.Replace(prepayid, string.Empty).Substring(0, 8);
            return DateTime.TryParse(string.Format("{0}-{1}-{2}", strTemp.Substring(0, 4), strTemp.Substring(4, 2), strTemp.Substring(6, 2)), out result);
        }

    }
}
