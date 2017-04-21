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
    public class APIHelper
    {
        private static readonly string GetCardAd = "api/GetCardApi/GetCard?cardno={0}&cardPw={1}";
           private static readonly string GetCardTypeAd = "api/GetCardTypeApi/GetCardType?cardTypeNo=";
        private static readonly string ActiveCardAd = "api/CardActivApi/ActiveUserInfo";

        private static readonly string ActiveCardAd2 = "api/DrvieCardActivApi/ActiveDriveCardUserInfo";
        //post提交获取用户信息
        private static readonly string GetActiveUserInfosApi = "api/CardActivApi/GetActiveUserInfos?cardNo={0}&pwd={1}";

        private static readonly string GetCardBatchAd = "api/GetCardApplyBatchApi/GetCardApplyBatch";
        private static readonly string GetCardInDataAd = "api/GetIsEndYearApi/GetIsEndYear?cardno=";
        private static readonly string CheckCardActiveAd = "api/CheckCardActiveApi/GetCardActive?cardNo=";
        private static readonly string UpdateHaolinCardAd = "api/UpdateUserInfoApi/UpdateUserInfo";
        private static readonly string UpdateUserNameAd = "api/UpdateUserNameApi/UpName?oldUserName={0}&newUserName={1}";
        private static readonly string GetAutoActiveApi = "api/AutoActiveApi/AutoActiveCard";
        private static readonly string GetSalesNoApi = "api/GetSalesNoApi/GetSalesNoByCardNo";
        private static readonly string AddCardUserInfoApi = "api/AddUserInfoApi/Add";

        //---------------------------------------------------------------------------------------------------------
        //营销员报表查询相关
        //根据手机号和密码获取营销员  GetSalesPersonCardsReport
        private static readonly string BindSalesPersonApi = "api/SalesPersonApi/BindSalesPerson?salesMobile={0}&pwd={1}";

        private static readonly string GetSalesPersonCardsReportApi = "api/SalesPersonApi/GetSalesPersonCardsReport?salesMobile2={0}&pwd={1}";

        private static readonly string GetSalesActivatedCardInfoApi = "api/SalesPersonApi/GetSalesActivatedCardInfo";

        private static readonly string ApiGetJobTypes = "api/GetCardApi/GetJobTypes?insuranceCompanyCode={0}";

        //------------------------------------------------------------------------------------------------------------
        private static string URI;

        /// <summary>
        /// BaseURI=BLL.SysManage.ConfigSystem.GetValueByCache("CardURL");
        /// </summary>
        /// <param name="baseURI"></param>
        public APIHelper(string baseURI)
        {
            URI = baseURI;
        }

        //获取卡批次编号
        public List<string> GetCardBatch()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage x = client.GetAsync(GetCardBatchAd).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result.Split(',').ToList();
            }
        }

        /// <summary>
        /// 判断卡的有效期
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns>1：有效，0：无效</returns>
        public int GetIsEndYear(string CardNo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage x = client.GetAsync(GetCardInDataAd).Result;

                x.EnsureSuccessStatusCode();

                var y = x.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(y))
                {
                    return int.Parse(y.Trim());
                }

                return 0;
            }
        }

        /// <summary>
        /// 激活并获得卡的信息
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string GetCardInfo(string CardNo, string pwd)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ad = string.Format(GetCardAd, CardNo, pwd);

                HttpResponseMessage x = client.GetAsync(ad).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// 获得卡类型信息
        /// </summary>
        /// <param name="CardTypeNo">卡类型编号</param>
        /// <returns></returns>
        public string GetCardTypeInfo(string CardTypeNo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage x = client.GetAsync(GetCardTypeAd + CardTypeNo).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// 根据卡号和密码获取用户信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string GetActiveUserInfos(string cardNo, string pwd)
        {
            using (var client = new HttpClient())
            {

                var uri = URI + "/" + GetActiveUserInfosApi;
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ad = string.Format(uri, cardNo, pwd);

                HttpResponseMessage x = client.GetAsync(ad).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result;
            }
            return "";
        }


        //增加卡用户信息
        public string ActiveCard(Shop_CardUserInfo userinfo)
        {            
            var uri = URI + "/"+ActiveCardAd;
            using (var client = new WebClient())
            {
               var st = client.UploadValues(uri, GetValue(userinfo));
               return Encoding.GetEncoding("utf-8").GetString(st).Trim(); 
              
            }
        }

        /// <summary>
        /// 驾车卡激活
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public string ActiveCard2(DriveCardInfoModel userinfo)
        {
            var uri2 = URI + "/" + ActiveCardAd2;
            using (var client = new WebClient())
            {
                //NameValueCollection a = new NameValueCollection();
                //a.Add("id", "1");
                var st = client.UploadValues(uri2, GetValue(userinfo));                
                //var st = client.UploadValues(uri2, a);
                return Encoding.GetEncoding("utf-8").GetString(st).Trim();

            }
        }

        /// <summary>
        /// 转换传递值为名值对,时间格式过不去接口检查
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public NameValueCollection GetValue(Shop_CardUserInfo userInfo)
        {
            NameValueCollection a = new NameValueCollection();
            var allproperties = userInfo.GetType().GetProperties();
            foreach (var item in allproperties)
            {
                var val = item.GetValue(userInfo);

                if (val != null && !typeof(ICollection).IsAssignableFrom(val.GetType()))
                {
                    if (!typeof(DateTime).IsAssignableFrom(val.GetType()))
                    {
                        a.Add(item.Name, Convert.ToString(val));
                    }
                    else
                    {
                        a.Add(item.Name, ((DateTime)val).ToString("yyyy-MM-dd hh:mm:ss"));
                    }
                }
                else
                {
                    if (val != null)
                        a.Add(item.Name, JsonConvert.SerializeObject(val));
                }
            }
            return a;
        }

        public NameValueCollection GetValue(DriveCardInfoModel userInfo)
        {
            NameValueCollection a = new NameValueCollection();
            var allproperties = userInfo.GetType().GetProperties();
            foreach (var item in allproperties)
            {
                var val = item.GetValue(userInfo);

                if (val != null && !typeof(ICollection).IsAssignableFrom(val.GetType()))
                {
                    if (!typeof(DateTime).IsAssignableFrom(val.GetType()))
                    {
                        a.Add(item.Name, Convert.ToString(val));
                    }
                    else
                    {
                        a.Add(item.Name, ((DateTime)val).ToString("yyyy-MM-dd hh:mm:ss"));
                    }
                }
                else
                {
                    if (val != null)
                        a.Add(item.Name, JsonConvert.SerializeObject(val));
                }
            }
            return a;
        }

        public string UpdateHaolinCard(Shop_CardUserInfo userinfo)
        {
             var uri = URI + "/"+UpdateHaolinCardAd;
             using (var client = new WebClient())
             {

                 var st = client.UploadValues(uri, GetValue(userinfo));
                 return Encoding.GetEncoding("utf-8").GetString(st).Trim();
                  
                 //using (var client = new HttpClient())
                 //{
                 //    client.BaseAddress = new Uri(URI);
                 //    client.DefaultRequestHeaders.Accept.Clear();
                 //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 //    var x = client.PostAsJsonAsync(UpdateHaolinCardAd, userinfo).Result;
                 //    x.EnsureSuccessStatusCode();
                 //    return x.Content.ReadAsStringAsync().Result;
                 //}
             }

        }

        //更新用户名
        public string UpdateUserName(string oldusername,string newusername)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ad = string.Format(UpdateUserNameAd, oldusername, newusername);

                HttpResponseMessage x = client.GetAsync(ad).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result;
            }

        }


        /// <summary>
        /// 判断卡是否被激活
        /// </summary>
        /// <param name="cardNo">卡编号</param>
        /// <returns></returns>
        public string CheckCardIsActive(string cardNo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage x = client.GetAsync(CheckCardActiveAd + cardNo).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result;
            }
        }

        public string GetAutoActive(string SalesMobile,string Mobile,string Name,string UserNo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage x = client.GetAsync(string.Format(GetAutoActiveApi, Mobile,SalesMobile,Name,UserNo)).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result;
            }

            //var uri = URI + "/" + GetAutoActiveApi;
            //using (var client = new WebClient())
            //{
            //    NameValueCollection mvc = new NameValueCollection();
            //    mvc.Add("mobel", Mobile);
            //    mvc.Add("salesMobel", SalesMobile);
            //    mvc.Add("userName", Name);
            //    mvc.Add("userNo", UserNo);

            //    var st = client.UploadValues(uri, mvc);
            //    return Encoding.GetEncoding("utf-8").GetString(st).Trim();
            //}
        }

        /// <summary>
        /// 自动注册 
        /// </summary>
        /// <param name="salesMobel">推荐人手机</param>
        /// <param name="mobel">用户手机</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="userNo">用户NO</param>
        /// <returns>返回UserInfo</returns>
        public Shop_CardUserInfo ShowAutoActive(string SalesMobile, string Mobile, string Name, string UserNo)
        {
            var uri = URI + "/" + GetAutoActiveApi;            
            StringBuilder v = new StringBuilder();
            v.Append(uri + "?mobel=").Append(Mobile).Append("&");
            v.Append("salesMobel=").Append(SalesMobile).Append("&");
            v.Append("userName=").Append(Name).Append("&");
            v.Append("userNo=").Append(UserNo);

            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(v.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();

                return (Shop_CardUserInfo)JsonConvert.DeserializeObject(result, typeof(Shop_CardUserInfo));
            }
        }

        /// <summary>
        /// 通过卡号，获得销售员编号 
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <returns>返回销售员NO</returns>
        public string GetSalesNoByCardNo(string cardNo)
        {
            var uri = URI + "/" + GetSalesNoApi;
            StringBuilder v = new StringBuilder();
            v.Append(uri + "?cardNo=").Append(cardNo);

            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(v.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();

                return result;
            }
        } 

        public string AddCardUserInfo(Shop_CardUserInfo userinfo)
        {
            var uri = URI + "/" + AddCardUserInfoApi;
            using (var client = new WebClient())
            {
                var st = client.UploadValues(uri, GetValue(userinfo));
                return Encoding.GetEncoding("utf-8").GetString(st).Trim();
            }
        }

        public string ReadJson(string Json,string Name)
        {
            JsonReader reader = new JsonTextReader(new StringReader(Json));
            int tag = 0;
            string Result = null;
            while (reader.Read())
            {
                if (tag == 1)
                {
                    if (reader.Value != null)
                    {
                        Result = reader.Value.ToString();
                        break;
                    }
                    else
                    {
                        Result = null;
                    }
                }
                if (reader.Value != null && reader.Value.ToString() == Name)
                {
                    tag = 1;
                }
            }
            return Result;
        }

        /*获取营销人员*/
        public SalesPersonModel GetSalesPersonByMobile(string mobile, string pwd)
        {
            var uri = URI + "/" + BindSalesPersonApi;

            var ad = string.Format(uri, mobile, pwd);
            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(ad.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();
                return (SalesPersonModel)JsonConvert.DeserializeObject(result, typeof(SalesPersonModel));
                
            }
        }


        public List<SalesPersonCardsReportModel> GetSalesPersonCardsReport(string mobile, string pwd)
        {
            //return null;
            var uri = URI + "/" + GetSalesPersonCardsReportApi;

            var ad = string.Format(uri, mobile, pwd);
            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(ad.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();
                return (List<SalesPersonCardsReportModel>)JsonConvert.DeserializeObject(result, typeof(List<SalesPersonCardsReportModel>));

            }
        }

        /// <summary>
        /// post数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SalesPersonQueryReturnModel GetSalesActivatedCardInfo(SalesPersonQueryModel query)
        {
            var uri = URI + "/" + GetSalesActivatedCardInfoApi;
           // var ad = string.Format(uri, query.salesMobile, query.salesPwd, searchCardNo, searchCardId, searchInsureNo, insureNoSearchType, curPageIndex, pageSize);
            using (WebClient webClient = new WebClient())
            {
                var st = webClient.UploadValues(uri, GetValue(query));           
             
                var result = Encoding.GetEncoding("utf-8").GetString(st).Trim();
                return (SalesPersonQueryReturnModel)JsonConvert.DeserializeObject(result, typeof(SalesPersonQueryReturnModel));

            }
        }

        public NameValueCollection GetValue(SalesPersonQueryModel query)
        {
            NameValueCollection a = new NameValueCollection();
            var allproperties = query.GetType().GetProperties();
            foreach (var item in allproperties)
            {
                var val = item.GetValue(query);              
                a.Add(item.Name, Convert.ToString(val));
                   
            }
            return a;
        }

        /// <summary>
        /// 获取职业类型
        /// </summary>
        /// <returns></returns>
        public string GetJobTypes(string insuranceCompanyCode)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ad = string.Format(ApiGetJobTypes, insuranceCompanyCode);

                HttpResponseMessage x = client.GetAsync(ad).Result;

                x.EnsureSuccessStatusCode();

                return x.Content.ReadAsStringAsync().Result;
            }
        }

    }
}
