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

namespace Maticsoft.Services
{
    public class TeamAPI
    {
        private static readonly string GetMyCardsAPI = "api/GetMyCardsAPI/GetMyCardList";
        private static readonly string GetMyInComeAPI = "api/GetMyInComeAPI/GetMyInComeList";
        private static readonly string GetMyNongHu = "api/GetMyCardsAPI/GetNongHu";
        private static readonly string GetMyInCome = "api/GetMyCardsAPI/GetPerformanceList";

        private static readonly string GetSalePerson = "api/SalesPersonAPI/GetSalesPersonModel";
        private static string URI;

        //初始化
        public TeamAPI(string baseURI)
        {
            URI = baseURI;
        }

        /// <summary>
        /// 获取粉丝 
        /// </summary>
        /// <param name="salesNo">营销员编号</param>
        /// <returns>粉丝 </returns>
        public List<Maticsoft.Model.Team.AppMember> GetMyMembers(string salesPhone)
        {
            var uri = URI + "/" + GetMyCardsAPI;
            StringBuilder v = new StringBuilder();
            v.Append(uri + "?salesPhone=").Append(salesPhone);

            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(v.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();

                return (List<Maticsoft.Model.Team.AppMember>)JsonConvert.DeserializeObject(result, typeof(List<Maticsoft.Model.Team.AppMember>));
            }
        }
        // SalesPerson         GetSalePersonModel   
        public Maticsoft.Model.Team.SalesPersonModel GetSalePersonModel(string cardNo)
        {
            var uri = URI + "/" + GetSalePerson;
            StringBuilder v = new StringBuilder();
            v.Append(uri + "?cardNo=").Append(cardNo);

            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(v.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();
                return (Maticsoft.Model.Team.SalesPersonModel)JsonConvert.DeserializeObject(result, typeof(Maticsoft.Model.Team.SalesPersonModel));
            }
        }


        /// <summary>
        /// 获取收入
        /// </summary>
        /// <param name="salesNo">营销员编号</param>
        /// <returns>收入 </returns>
        public List<Maticsoft.Model.Team.AppInCome> GetMyInComeList(string salesPhone)
        {
            var uri = URI + "/" + GetMyInComeAPI;
            StringBuilder v = new StringBuilder();
            v.Append(uri + "?salesPhone=").Append(salesPhone);

            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(v.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();

                return (List<Maticsoft.Model.Team.AppInCome>)JsonConvert.DeserializeObject(result, typeof(List<Maticsoft.Model.Team.AppInCome>));
            }
        }

        /// <summary>
        /// 粉丝龙虎榜
        /// </summary>
        /// <param name="nongHutype"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Team.SalesPersonBillboard> GetNongHu(int nongHutype)
        {
            var uri = URI + "/" + GetMyNongHu;
            StringBuilder v = new StringBuilder();
            v.Append(uri + "?nongHutype=").Append(nongHutype);

            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(v.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();

                return (List<Maticsoft.Model.Team.SalesPersonBillboard>)JsonConvert.DeserializeObject(result, typeof(List<Maticsoft.Model.Team.SalesPersonBillboard>));
            }
        }


        /// <summary>
        /// 我的业绩
        /// </summary>
        /// <param name="salesNo">营销员编号</param>
        /// <returns>收入 </returns>
        public List<Maticsoft.Model.Team.SalesPersonIncome> GetMyachievements(ref int totalcount, string where, int Page, int PageSize, out decimal totalprice, out decimal totalnopay, out decimal totalpay)
        {
            totalprice = 0;
            totalnopay = 0;
            totalpay = 0;
            var uri = URI + "/" + GetMyInCome;
            StringBuilder v = new StringBuilder();
            List<Maticsoft.Model.Team.SalesPersonIncome> lists = null;
            v.Append(uri + "?where=" + where + "&Page=" + Page + "&PageSize=" + PageSize);

            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(v.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();
                lists = (List<Maticsoft.Model.Team.SalesPersonIncome>)JsonConvert.DeserializeObject(result, typeof(List<Maticsoft.Model.Team.SalesPersonIncome>));
            }
            if (lists != null)
            {
                lists.ForEach(d => d.Id = (d.InComeStatus == 3 ? 0 : 1));
                var list = lists.OrderByDescending(e => e.Id).ToList();

                int sumPage = 0;
                totalcount = list.Count;

                if (lists.Count % PageSize == 0)
                {
                    sumPage = list.Count / PageSize;
                }
                else
                {
                    sumPage = list.Count / PageSize + 1;
                }

                int int_StartRow = (Page - 1) * PageSize;
                int int_EndRow = (Page) * PageSize;
                var a = list.Skip(int_StartRow).Take(PageSize).ToList();
                totalprice = list.Sum(b => b.Income);
                totalpay = list.Where(l => l.InComeStatus == 3).Sum(l => l.Income);
                totalnopay = totalprice - totalpay;
                return a;
            }
            else
            {
                totalcount = 0;
                return lists;
            }




        }
    }
}
