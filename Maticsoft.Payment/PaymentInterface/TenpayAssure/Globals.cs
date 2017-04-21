namespace Maticsoft.Payment.PaymentInterface.TenpayAssure
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Collections.Specialized;

    internal sealed class Globals
    {
        private Globals()
        {
        }

        /** 获取大写的MD5签名结果 */
        internal static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.UTF8.GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        internal static string GetParameURL(NameValueCollection parameters, string charset)
        {
            StringBuilder sbsign = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            System.Collections.ArrayList akeys = new System.Collections.ArrayList(parameters.Keys);
            akeys.Sort();

            //签名
            foreach (string k in akeys)
            {
                string v = parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sbsign.Append(k + "=" + v + "&");
                }
            }
            sbsign.Append("key=" + parameters["key"]);
            parameters["sign"] = GetMD5(sbsign.ToString(), charset);

            foreach (string k in akeys)
            {
                string v = parameters[k];
                if (null != v && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + UrlEncode(v, charset) + "&");
                }
            }

            //去掉最后一个&
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }


        internal static bool isTenpaySign(NameValueCollection parameters, string charset)
        {
            StringBuilder sb = new StringBuilder();

            System.Collections.ArrayList akeys = new System.Collections.ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + parameters["key"]);
            string sign = GetMD5(sb.ToString(), charset);

            return parameters["sign"].ToUpper().Equals(sign);
        }


        /** 对字符串进行URL编码 */
        internal static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;
            }
        }

        /** 对字符串进行URL解码 */
        internal static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;

            }
        }

        //internal static string GetMD5(string encypStr)
        //{
        //    MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        //    byte[] bytes = Encoding.UTF8.GetBytes(encypStr);
        //    return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", "").ToUpper();
        //}
    }
}

