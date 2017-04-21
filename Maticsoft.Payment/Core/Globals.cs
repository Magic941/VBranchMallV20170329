/**
* Globals.cs
*
* 功 能： 共通类
* 类 名： Globals
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace Maticsoft.Payment.Core
{
    internal sealed class Globals
    {
        public const string GATEWAY_KEY = "MATICSOFTGW";

        /// <summary>
        /// 是否测试模式 - 跳过支付网关
        /// </summary>
        public static bool IsPaymentTestMode
        {
            get { return Configuration.WebConfigHelper.GetConfigBool("PaymentTest"); }
        }
        /// <summary>
        /// 订单前辍
        /// </summary>
        public static string OrderPrefix
        {

            get
            {
                var prefix = Configuration.WebConfigHelper.GetConfigString("OrderPrefix");
                if (string.IsNullOrEmpty(prefix))
                    return "haolin";
                else
                    return prefix;
            }
        }

        /// <summary>
        /// 是否测试模式 - 跳过充值网关
        /// </summary>
        public static bool IsRechargeTestMode
        {
            get { return Configuration.WebConfigHelper.GetConfigBool("RechargeTest"); }
        }

#warning 支付成功结果页面[Pay/PaymentReturn_url.aspx]
        /// <summary>
        /// 支付成功URL
        /// </summary>
        /// 
        public const string PAYMENT_RETURN_URL = "Pay/Payment/Return_url.aspx?" + GATEWAY_KEY + "={0}";

#warning 支付成功网关异步通知[Pay/Payment/Notify_url.aspx]
        /// <summary>
        /// 支付成功网关异步通知URL
        /// </summary>
        public const string PAYMENT_NOTIFY_URL = "Pay/Payment/Notify_url.aspx?" + GATEWAY_KEY + "={0}";

#warning 充值成功结果页面[Pay/Recharge/Return_url.aspx]
        /// <summary>
        /// 充值成功URL
        /// </summary>
        public const string RECHARGE_RETURN_URL = "Pay/Recharge/Return_url.aspx?" + GATEWAY_KEY + "={0}";

#warning 充值成功网关异步通知[Pay/RechargeNotify_url.aspx]
        /// <summary>
        /// 充值成功网关异步通知URL
        /// </summary>
        public const string RECHARGE_NOTIFY_URL = "Pay/Recharge/Notify_url.aspx?" + GATEWAY_KEY + "={0}";

        /// <summary>
        /// Alipay支付接口扩展参数集合 - 用于自定义参数的扩展
        /// </summary>
        public static List<string> AlipayOtherParamKeys = new List<string>();

        public static string FullPath(string local)
        {
            if (string.IsNullOrEmpty(local))
            {
                return local;
            }
            if (local.ToLower(CultureInfo.InvariantCulture).StartsWith("http://"))
            {
                return local;
            }
            if (HttpContext.Current == null)
            {
                return local;
            }
            return FullPath(HostPath(HttpContext.Current.Request.Url), local);
        }

        public static string GetMd5(System.Text.Encoding encoding, string value)
        {
            //sign 加密签名
            byte[] buffer = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(
                encoding.GetBytes(value));
            System.Text.StringBuilder sign = new System.Text.StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                sign.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return sign.ToString();
        }

        public static string FullPath(string hostPath, string local)
        {
            if (!hostPath.EndsWith("/") && !local.StartsWith("/"))
            {
                return (hostPath + "/" + local);
            }
            return (hostPath + local);
        }

        public static string HostPath(Uri uri)
        {
            if (uri == null)
            {
                return string.Empty;
            }
            string str = (uri.Port == 80) ? string.Empty : (":" + uri.Port.ToString(CultureInfo.InvariantCulture));
            return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[] { uri.Scheme, uri.Host, str });
        }

        #region SafeParse
        public static bool SafeBool(object target, bool defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeBool(tmp, defaultValue);
        }
        public static bool SafeBool(string text, bool defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        public static DateTime SafeDateTime(object target, DateTime defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeDateTime(tmp, defaultValue);
        }
        public static DateTime SafeDateTime(string text, DateTime defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = time;
            }
            return defaultValue;
        }

        public static decimal SafeDecimal(object target, decimal defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeDecimal(tmp, defaultValue);
        }
        public static decimal SafeDecimal(string text, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static short SafeShort(object target, short defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeShort(tmp, defaultValue);
        }
        public static short SafeShort(string text, short defaultValue)
        {
            short num;
            if (short.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static int SafeInt(object target, int defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeInt(tmp, defaultValue);
        }
        public static int SafeInt(string text, int defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }


        public static long SafeLong(object target, long defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeLong(tmp, defaultValue);
        }
        public static long SafeLong(string text, long defaultValue)
        {
            long num;
            if (long.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static string SafeString(object target, string defaultValue)
        {
            if (null != target && "" != target.ToString())
            {
                return target.ToString();
            }
            return defaultValue;
        }

        #region SafeNullParse
        public static bool? SafeBool(object target, bool? defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeBool(tmp, defaultValue);
        }
        public static bool? SafeBool(string text, bool? defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        public static DateTime? SafeDateTime(object target, DateTime? defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeDateTime(tmp, defaultValue);
        }
        public static DateTime? SafeDateTime(string text, DateTime? defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = time;
            }
            return defaultValue;
        }

        public static decimal? SafeDecimal(object target, decimal? defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeDecimal(tmp, defaultValue);
        }
        public static decimal? SafeDecimal(string text, decimal? defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static short? SafeShort(object target, short? defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeShort(tmp, defaultValue);
        }
        public static short? SafeShort(string text, short? defaultValue)
        {
            short num;
            if (short.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static int? SafeInt(object target, int? defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeInt(tmp, defaultValue);
        }
        public static int? SafeInt(string text, int? defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static long? SafeLong(object target, long? defaultValue)
        {
            if (target == null) return defaultValue;
            string tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeLong(tmp, defaultValue);
        }
        public static long? SafeLong(string text, long? defaultValue)
        {
            long num;
            if (long.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }
        #endregion

        #region SafeEnum
        /// <summary>
        /// 将枚举数值or枚举名称 安全转换为枚举对象
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">数值or名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <remarks>转换区分大小写</remarks>
        /// <returns></returns>
        public static T SafeEnum<T>(string value, T defaultValue) where T : struct
        {
            return SafeEnum<T>(value, defaultValue, false);
        }

        /// <summary>
        /// 将枚举数值or枚举名称 安全转换为枚举对象
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">数值or名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="ignoreCase">是否忽略大小写 true 不区分大小写 | false 区分大小写</param>
        /// <returns></returns>
        public static T SafeEnum<T>(string value, T defaultValue, bool ignoreCase) where T : struct
        {
            T result;
            if (Enum.TryParse<T>(value, ignoreCase, out result))
            {
                if (Enum.IsDefined(typeof(T), result))
                {
                    defaultValue = result;
                }
            }
            return defaultValue;
        }
        #endregion

        #endregion

        #region SubString
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="target">内容</param>
        /// <param name="subLength">截取长度</param>
        /// <param name="sign">替换符号</param>
        /// <returns></returns>
        public static string SubString(object target, int subLength, string sign = null)
        {
            string str = string.Empty;
            if (!IsNullOrEmpty(target))
            {
                str = target.ToString();
                if (str.Length > subLength)
                {
                    if (!string.IsNullOrWhiteSpace(sign))
                    {
                        //截取时, 包含占位符号长度, 以保证页面不变形
                        str = str.Substring(0, subLength - sign.Length / 2);
                        str = str + sign;
                    }
                    else
                    {
                        str = str.Substring(0, subLength);
                    }
                }
            }
            return str;
        }

        #region 检查字符串是否是 null 或者空白字符,不同于.net自带的string.IsNullOrEmpty，多个空格在这里也返回true。
        /// <summary>
        /// 检查字符串是否是 null 或者空白字符,不同于.net自带的string.IsNullOrEmpty，多个空格在这里也返回true。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object target)
        {
            if (null != target && "" != target.ToString())
            {
                return target.ToString().Trim().Length == 0;
            }
            return true;
        }
        #endregion
        #endregion

        #region Base64
        /// <summary>
        /// 从适用于URL的Base64编码字符串转换为普通字符串
        /// </summary>
        public static string DecodeData4Url(string base64String)
        {
            base64String = HttpUtility.UrlDecode(base64String);
            string temp = base64String.Replace('_', '=').Replace('.', '+').Replace('-', '/');
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(temp));
        }

        /// <summary>
        /// 从普通字符串转换为适用于URL的Base64编码字符串
        /// </summary>
        public static string EncodeData4Url(string normalString)
        {
            return
                HttpUtility.UrlEncode(
                    Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(normalString))
                        .Replace('+', '.')
                        .Replace('/', '-')
                        .Replace('=', '_'));
        }
        #endregion

        #region WriteTextLog
        public static void WriteNameValue(System.Collections.Specialized.NameValueCollection parameter)
        {

            System.Text.StringBuilder log = new System.Text.StringBuilder();
            string[] keyArray = parameter.AllKeys;

            string[] valuesArray;
            foreach (string key in keyArray)
            {
                log.AppendFormat("{0}: ", key);
                valuesArray = parameter.GetValues(key);
                if (valuesArray == null) continue;
                foreach (string value in valuesArray)
                {
                    log.AppendFormat("{0}", value);
                }
                log.AppendLine();
            }

            WriteText(log);
        }
        public static void WriteText(System.Text.StringBuilder log, string fileName = "Log")
        {
            string path = GetAssemblyPath();
            string dir = string.Format("{0}/log/", path);
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            fileName = string.Format("{0}{1}_{2}.txt", dir, fileName,
                DateTime.Now.ToString("yyyyMMdd"));
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    using (System.IO.StreamWriter sw = System.IO.File.AppendText(fileName))
                    {
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                    }
                }
                else
                {
                    using (System.IO.StreamWriter sw = System.IO.File.CreateText(fileName))
                    {
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取Assembly的运行路径
        /// </summary>
        /// <returns></returns>
        private static string GetAssemblyPath()
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            System.IO.DirectoryInfo dr = new System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(path));
            if (dr.Parent != null) path = dr.Parent.FullName; //当前目录的上一级目录
            return path;
        }
        #endregion
    }
}

