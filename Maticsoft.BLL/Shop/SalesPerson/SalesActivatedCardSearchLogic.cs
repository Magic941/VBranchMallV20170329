using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using Maticsoft.Model;
using Maticsoft.Services;
using Newtonsoft.Json;
using System.Transactions;

namespace Maticsoft.BLL.Shop.Card
{
    public class SalesActivatedCardSearchLogic
    {
        public string baseuri = System.Configuration.ConfigurationManager.AppSettings["CardURL"];       

      
        /// <summary>
        /// 获取卡的信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="pwd"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Maticsoft.Model.SalesPersonModel GetSalesPerson(string salesMobile, string pwd)
        {
          
            var helper = new APIHelper(baseuri);
            if (!string.IsNullOrEmpty(salesMobile) && !string.IsNullOrEmpty(pwd))
            {
                var result = helper.GetSalesPersonByMobile(salesMobile, pwd);
                return result;
            }
            return null;
        }

        public List<SalesPersonCardsReportModel> GetSalesPersonCardsReport(string salesMobile, string pwd)
        {

            var helper = new APIHelper(baseuri);
            if (!string.IsNullOrEmpty(salesMobile) && !string.IsNullOrEmpty(pwd))
            {
                var result = helper.GetSalesPersonCardsReport(salesMobile, pwd);
                return result;
            }
            return null;
        }

        public List<SalesPersonCardsSearchModel> GetSalesActivatedCardInfo(string salesMobile, string pwd, string searchCardNo, string searchCardId, string searchInsureNo, int insureNoSearchType, int curPageIndex,out int TotalRows)
        {
            var helper = new APIHelper(baseuri);
            SalesPersonQueryModel q = new SalesPersonQueryModel();
            q.salesMobile = salesMobile;
            q.salesPwd = pwd;
            q.searchCardNo = searchCardNo;
            q.searchCardId = searchCardId;
            q.searchInsureNo = searchInsureNo;
            q.insureNoSearchType = insureNoSearchType;
            q.curPageIndex = curPageIndex;
            q.pageSize = 10;

            var result = helper.GetSalesActivatedCardInfo(q);
            if (result != null)
            {
                TotalRows = result.TotalRows;
                return result.Data;
            }
            TotalRows = 0;
            return null;           
        } 
    }
}
