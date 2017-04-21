//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using Newtonsoft.Json;
//using Maticsoft.Common;
//using Maticsoft.Model;


//namespace Maticsoft.Web.App_Code
//{
//    public class APIHelper
//    {
//        private static readonly string GetCardAd = "api/CardActivApi/GetCard?cardno=";
//        private static readonly string GetCardTypeAd = "api/CardActivApi/GetCardType?cardTypeNo=";
//        private static readonly string ActiveCardAd = "api/CardActivApi/UpdateCard";
//        private static string URI;

//        public static void GetKey()
//        {
//            URI = BLL.SysManage.ConfigSystem.GetValueByCache("CardURL");
//        }


//        public static string GetCardInfo(string CardNo)
//        {
//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri(URI);
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//                HttpResponseMessage x = client.GetAsync(GetCardAd + CardNo).Result;

//                x.EnsureSuccessStatusCode();

//                return x.Content.ReadAsStringAsync().Result;
//            }
//        }

//        //获得卡类型信息
//        public static string GetCardTypeInfo(string CardTypeNo)
//        {
//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri(URI);
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//                HttpResponseMessage x = client.GetAsync(GetCardTypeAd + CardTypeNo).Result;

//                x.EnsureSuccessStatusCode();

//                return x.Content.ReadAsStringAsync().Result;
//            }
//        }


//        //激活卡信息
//        public static void ActiveCard(Shop_CardUserInfo userinfo)
//        {

//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri(URI);
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                //net40
//                //var requestJson = JsonConvert.SerializeObject(userinfo);
//                //HttpContent context = new StringContent(requestJson);

//                //var y = client.PostAsync(ActiveCardAd, context);
                
//                //net45
//                var x = client.PostAsJsonAsync(ActiveCardAd, userinfo).Result;
//                x.EnsureSuccessStatusCode();
//                x.Dispose();
//            }
//        }
//    }
//}