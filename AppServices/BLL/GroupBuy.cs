using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppServices.BLL
{
    public class GroupBuy
    {
        private readonly Maticsoft.IDAL.Shop.PromoteSales.IGroupBuy dal = Maticsoft.DALFactory.DAShopProSales.CreateGroupBuy();
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
     
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public AppServices.Models.GroupBuy GetProductGroupByInfo(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToModel(ds.Tables[0].Rows[0]);
                }
            }
            return null;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<AppServices.Models.GroupBuy> GetModelListToday(string strWhere)
        {
            string CacheKey = strWhere;

            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);

            if (objModel == null)
            {
                try
                {
                    DataSet ds = dal.GetListToday(strWhere);
                    objModel = DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("Cache_HorsTime");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<AppServices.Models.GroupBuy>)objModel;
            //DataSet ds = dal.GetListToday(strWhere);
            //return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<AppServices.Models.GroupBuy> DataTableToList(DataTable dt)
        {
            List<AppServices.Models.GroupBuy> modelList = new List<AppServices.Models.GroupBuy>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                AppServices.Models.GroupBuy model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AppServices.Models.GroupBuy DataRowToModel(DataRow row)
        {
            AppServices.Models.GroupBuy model = new AppServices.Models.GroupBuy();
            if (row != null)
            {
                if (row["GroupBuyId"] != null && row["GroupBuyId"].ToString() != "")
                {
                    model.GroupBuyId = int.Parse(row["GroupBuyId"].ToString());
                }
                if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["FinePrice"] != null && row["FinePrice"].ToString() != "")
                {
                    model.FinePrice = decimal.Parse(row["FinePrice"].ToString());
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["EndDate"] != null && row["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if (row["MaxCount"] != null && row["MaxCount"].ToString() != "")
                {
                    model.MaxCount = int.Parse(row["MaxCount"].ToString());
                }
                if (row["GroupCount"] != null && row["GroupCount"].ToString() != "")
                {
                    model.GroupCount = int.Parse(row["GroupCount"].ToString());
                }
                if (row["BuyCount"] != null && row["BuyCount"].ToString() != "")
                {
                    model.BuyCount = int.Parse(row["BuyCount"].ToString());
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["RegionId"] != null)
                {
                    model.RegionId = Maticsoft.Common.Globals.SafeInt(row["RegionId"], -1);
                }
               
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["ProductCategory"] != null)
                {
                    model.ProductCategory = row["ProductCategory"].ToString();
                }
                if (row["GroupBuyImage"] != null)
                {
                    model.GroupBuyImage = row["GroupBuyImage"].ToString();
                }
                if (row["CategoryId"] != null)
                {
                    model.CategoryId = Maticsoft.Common.Globals.SafeInt(row["CategoryId"], 0);
                }
                if (row["CategoryPath"] != null)
                {
                    model.CategoryPath = row["CategoryPath"].ToString();
                }
                if (row["GroupBase"] != null)
                {
                    model.GroupBase = Maticsoft.Common.Globals.SafeInt(row["GroupBase"], 0);
                }
                if (row.Table.Columns.Contains("Subhead"))
                {
                    if (row["Subhead"] != null)
                    {
                        model.Subhead = row["Subhead"].ToString();
                    }
                }
              
                if (row.Table.Columns.Contains("MarketPrice"))
                {
                    if (row["MarketPrice"] != null)
                    {
                        model.MarketPrice = decimal.Parse(row["MarketPrice"].ToString());
                    }
                }
                if (row.Table.Columns.Contains("PromotionType"))
                {
                    if (row["PromotionType"] != null)
                    {
                        model.PromotionType = Maticsoft.Common.Globals.SafeInt(row["PromotionType"], 0);
                    }
                }
                if (row.Table.Columns.Contains("PromotionLimitQu"))
                {
                    if (row["PromotionLimitQu"] != null)
                    {
                        model.PromotionLimitQu = Maticsoft.Common.Globals.SafeInt(row["PromotionLimitQu"], 1);
                    }
                }
                if (row.Table.Columns.Contains("GroupBase"))
                {
                    if (row["GroupBase"] != null)
                    {
                        model.GroupBase = Maticsoft.Common.Globals.SafeInt(row["GroupBase"], 1);
                    }
                }
                if (row.Table.Columns.Contains("LeastbuyNum"))
                {
                    if (row["LeastbuyNum"] != null)
                    {
                        model.LeastbuyNum = Maticsoft.Common.Globals.SafeInt(row["LeastbuyNum"], 1);
                    }
                }
            }
            return model;
        }

    }
}