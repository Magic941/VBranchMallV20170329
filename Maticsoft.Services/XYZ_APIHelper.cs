using System;
using System.Collections.Generic;
using System.Linq;


using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Maticsoft.Model;
using Newtonsoft.Json;
using System.Net;

using System.Collections.Specialized;
using System.IO;
using System.Collections;

namespace Maticsoft.Services
{
    /// <summary>
    /// 新一站的访问接口
    /// 下载产品，组合保险生成订单，
    /// </summary>
    public class XYZ_APIHelper
    {
        private static readonly string test = "api/test/viewjson?uid={0}&sid={1}&ts={2}&sig={3}&planId={4}";


        static string _uid;
        static string _sid;
        static string _appSecret;

        static string _uri="http://58.240.26.203";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseURI">新一站平台的调用地址</param>
        /// <param name="UID">联盟平台分配的用户编号</param>
        /// <param name="SID"></param>
        /// <param name="appSecret">配置表中的密匙，加密时的一个重要的参数</param>
        public XYZ_APIHelper(string uri,string uid,string sid,string appSecret)
        {
            _uri = uri;
            _uid = uid;
            _sid = sid;
            _appSecret = appSecret;

        }

        //获取卡批次编号
        public string Test()
        {
            //var ts = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            var ts = "2014-03-26 12:44:31";
            var sign = MD5Sign(_uid,_sid,ts,"","","","",_appSecret);

            var newtest = string.Format(test, _uid, _sid,ts,sign);
            var uri = _uri + "/" + newtest;
           
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");

                HttpResponseMessage x = client.GetAsync(uri).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result;

            }
        }

        private string MD5Sign(string uid,string sid,string ts,string loginId,string orderId,string policyId,string applicationId,string appSecret)
        {

            StringBuilder sign = new StringBuilder();

            if (string.IsNullOrEmpty(uid))
            {
                throw new Exception("uid 参数不能为空.");
            }
            if (string.IsNullOrEmpty(sid))
            {
                throw new Exception("sid 参数不能为空.");
            }
            if (string.IsNullOrEmpty(ts))
            {
                throw new Exception("ts 参数不能为空.");
            }
            if (string.IsNullOrEmpty(appSecret))
            {
                throw new Exception("appSecret 参数不能为空.");
            }
            sign.AppendFormat("uid={0}",uid).Append(",");
            sign.AppendFormat("sid={0}", sid).Append(",");
            sign.AppendFormat("ts={0}", ts).Append(",");

            if (!string.IsNullOrEmpty(loginId))
            {
                sign.Append(loginId);
            }
            if (!string.IsNullOrEmpty(policyId))
            {
                sign.Append(policyId);
            }
            if (!string.IsNullOrEmpty(applicationId))
            {
                sign.Append(applicationId);
            }            
            sign.Append(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(appSecret, "md5").ToUpper());

            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sign.ToString(), "md5").ToUpper();            

        }       
    }
}
