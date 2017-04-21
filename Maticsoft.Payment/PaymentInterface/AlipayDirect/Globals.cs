using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Maticsoft.Payment.PaymentInterface.AlipayDirect
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

        /// <summary>
        /// 执行各性化的参数
        /// </summary>
        /// <param name="gateway">网关提交地址</param>
        /// <param name="service">服务的类型</param>
        /// <param name="partner">合作编号</param>
        /// <param name="sign_type">签名类型</param>
        /// <param name="out_trade_no">订单编号</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="payment_type"></param>
        /// <param name="total_fee"></param>
        /// <param name="show_url"></param>
        /// <param name="seller_email"></param>
        /// <param name="key"></param>
        /// <param name="return_url">返回url</param>
        /// <param name="_input_charset"></param>
        /// <param name="notify_url">通知url</param>
        /// <param name="extend_param"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        internal static string CreatDirectUrl(string gateway, string service, string partner, string sign_type, string out_trade_no, string subject, string body, string payment_type, string total_fee, string show_url, string seller_email, string key, string return_url, string _input_charset, string notify_url, string extend_param, string token)
        {
            int num;
            string[] strArray;
            if (string.IsNullOrEmpty(token))
            {
                strArray = new string[] { "service=" + service, "partner=" + partner, "subject=" + subject, "body=" + body, "out_trade_no=" + out_trade_no, "total_fee=" + total_fee, "show_url=" + show_url, "payment_type=" + payment_type, "seller_email=" + seller_email, "notify_url=" + notify_url, "_input_charset=" + _input_charset, "return_url=" + return_url, "extend_param=" + extend_param };
            }
            else
            {
                strArray = new string[] { "service=" + service, "partner=" + partner, "subject=" + subject, "body=" + body, "out_trade_no=" + out_trade_no, "total_fee=" + total_fee, "show_url=" + show_url, "payment_type=" + payment_type, "seller_email=" + seller_email, "notify_url=" + notify_url, "_input_charset=" + _input_charset, "return_url=" + return_url, "extend_param=" + extend_param, "token=" + token };
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
            string str = GetMD5(builder.ToString(), _input_charset);
            char[] separator = new char[] { '=' };
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(gateway);
            for (num = 0; num < strArray2.Length; num++)
            {
                builder2.Append(strArray2[num].Split(separator)[0] + "=" + HttpUtility.UrlEncode(strArray2[num].Split(separator)[1]) + "&");
            }
            builder2.Append("sign=" + str + "&sign_type=" + sign_type);
            
            return builder2.ToString();
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
    }
}
