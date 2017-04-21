using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Gift;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Gift;
using System.Text;
namespace Maticsoft.BLL.Shop.Gift
{
	/// <summary>
	/// ExchangeDetail
	/// </summary>
	public partial class ExchangeDetail
	{
        private readonly IExchangeDetail dal = DAShopGifts.CreateExchangeDetail();
		public ExchangeDetail()
		{}

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DetailID)
        {
            return dal.Exists(DetailID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Gift.ExchangeDetail model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Gift.ExchangeDetail model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int DetailID)
        {

            return dal.Delete(DetailID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string DetailIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(DetailIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Gift.ExchangeDetail GetModel(int DetailID)
        {

            return dal.GetModel(DetailID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Gift.ExchangeDetail GetModelByCache(int DetailID)
        {

            string CacheKey = "ExchangeDetailModel-" + DetailID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DetailID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Gift.ExchangeDetail)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Gift.ExchangeDetail> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Gift.ExchangeDetail> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Gift.ExchangeDetail> modelList = new List<Maticsoft.Model.Shop.Gift.ExchangeDetail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Gift.ExchangeDetail model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region 扩展方法
        public DataSet GetListEX(int status, string keyword,int type)
        {
            StringBuilder strWhere = new StringBuilder();
            if (status > -1)
            {
                strWhere.AppendFormat(" Status={0}", status);
            }
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" (Description like '%{0}%' or GiftName like '%{0}%' or CouponCode like '%{0}%' )", keyword);
            }
            if (type > -1)
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" Type={0}", type);
            }

            return dal.GetList(-1, strWhere.ToString(), " CreatedDate desc");
        }
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool SetStatus(int detailId, int status)
        {
            return dal.SetStatus(detailId, status);
        }

        /// <summary>
        /// 批量设置状态
        /// </summary>
        /// <param name="detailId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool SetStatusList(string detailIds, int status)
        {
            return dal.SetStatusList(detailIds, status);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Gift.ExchangeDetail> GetListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }
        #endregion
    }
}

