using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.IO;
using System.Xml;
using System.Net;

namespace Maticsoft.Services
{
    public class JZMessage : IMessage
    {
        private readonly string SN;
        private readonly string PASSWORD;
        public static IMessage message;
        public static object Flag = new object();
        //private static string codeTemple = "尊敬的用户，您的注册验证码为：{0},请及时输入.";


        public static IMessage getInstance()
        {
            if (message==null)
            {
                lock (Flag)
                {
                    message = new JZMessage();
                }
            }
            return message;
        }

        JZMessage()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(GetConfigPath());
                SN = doc.DocumentElement.SelectSingleNode("SN").InnerText;
                PASSWORD = doc.DocumentElement.SelectSingleNode("Password").InnerText;
            }
            catch (Exception)
            {
                throw new NullReferenceException("THE SMS KEY-[SN] IS NOT NULL! PLEASE CHECK THE SMSSETTING.CONFIG!");
            }

            if (string.IsNullOrEmpty(SN))
            {
                throw new NullReferenceException("THE SMS KEY-[SN] IS NOT NULL! PLEASE CHECK THE SMSSETTING.CONFIG!");
            }
        }

        private string  GetConfigPath()
        {
            string configPath = ConfigurationManager.AppSettings["SmsConfigPath"];
            if (string.IsNullOrEmpty(configPath) || configPath.Trim().Length == 0)
            {
                configPath = HttpContext.Current.Request.MapPath("/Config/SmsSetting.config");
            }
            else
            {
                if (!Path.IsPathRooted(configPath))
                    configPath = HttpContext.Current.Request.MapPath(Path.Combine(configPath, "SmsSetting.config"));
                else
                    configPath = Path.Combine(configPath, "SmsSetting.config");
            }
            return configPath;
        }

        public string SendSMS(string phone, string message)
        {
            //JZService.BusinessServiceClient client = new JZService.BusinessServiceClient();
            //var result = client.sendBatchMessage(SN, PASSWORD, phone, message);
            //if (result > 0)
            //    return "1"; //代表发送成功
            //else if (result == -1)
            //    return "余额不足";
            //else if (result == -2)
            //    return "账号或密码错误";
            //else if (result == -3)
            //    return "连接服务商失败";
            //else if (result == -4)
            //    return "超时";
            //else if (result == -5)
            //    return "网络问题";
            //else if (result == -11)
            //    return "手机号不允许发送";
            //else if (result == -12)
            //    return "消息内容存在违禁词";
            //else
            //{
            //    return "其他错误";
            //}
            return SendSMSnew(phone, message);
        }

        public int SendSMS(string phone,string message ,string sn,string pwd)
        {
            JZService.BusinessServiceClient client = new JZService.BusinessServiceClient();
            var result = client.sendBatchMessage(sn, pwd, phone, message);
            return result;
        }

        public string SendMultiSMS(string phone, string message)
        {
            JZService.BusinessServiceClient client = new JZService.BusinessServiceClient();
            return null;
        }

        public string SendSMSByTime(string phone, string message, string datatime)
        {
            throw new NotImplementedException();
        }
        #region 短信发送
        public string SendSMSnew(string phone, string message)
        {
            string sendurl = "http://121.40.137.165/OpenPlatform/OpenApi";
            string ac = "1001@500712000015";			//用户名
            string authkey = ConfigurationManager.AppSettings["SmsAuthkey"];	//密钥
            string cgid = "4325";  //通道组编号
            string csid = "1";
            string m = phone;  //发送号码
            string c = message;  //签名编号 ,可以为空时，使用系统默认的编号
            string action = "sendOnce";
            StringBuilder sbTemp = new StringBuilder();
            //POST 传值
            sbTemp.Append("action=" + action + "&ac=" + ac + "&authkey=" + authkey + "&m=" + m + "&cgid=" + cgid + "&csid=" + csid + "&c=" + c);
            byte[] bTemp = System.Text.Encoding.GetEncoding("utf-8").GetBytes(sbTemp.ToString());
            string postReturn = doPostRequest(sendurl, bTemp);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postReturn);
            string result = doc.DocumentElement.Attributes["result"].Value;

            //Regex linkReg = new Regex("result=(.+)/>");
            //MatchCollection linkCollection = linkReg.Matches(postReturn);
            //string str = linkCollection[0].Groups[1].Value;
            //str = str.Replace(">", "");
            //string result = str.Replace("\"", "").Trim();

            if (result == "1")
            {
                return "1"; //代表发送成功
            }
            else
            {
                return "发送失败";
            }

        }




        //POST方式发送得结果
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }
        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }
        #endregion
    }
}
