using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Members;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
namespace Maticsoft.BLL.Members
{
	/// <summary>
	/// 积分限制
	/// </summary>
	public partial class PointsLimit
	{
        private readonly IPointsLimit dal = DAMembers.CreatePointsLimit();
		public PointsLimit()
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
        public bool Exists(int LimitID)
        {
            return dal.Exists(LimitID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.PointsLimit model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.PointsLimit model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int LimitID)
        {

            return dal.Delete(LimitID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string LimitIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(LimitIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.PointsLimit GetModel(int LimitID)
        {

            return dal.GetModel(LimitID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.PointsLimit GetModelByCache(int LimitID)
        {

            string CacheKey = "PointsLimitModel-" + LimitID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(LimitID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.PointsLimit)objModel;
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
        public List<Maticsoft.Model.Members.PointsLimit> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.PointsLimit> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.PointsLimit> modelList = new List<Maticsoft.Model.Members.PointsLimit>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.PointsLimit model;
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

        #region  扩展方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string name)
        {
           int count= GetRecordCount("Name='" + name+"'");
           if (count > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.PointsLimit> GetAllLimitList()
        {
            DataSet ds = dal.GetList("");
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 是否存在该记录(更新操作是调用)
        /// </summary>
        public bool Exists(string name,int limitid)
        {
            int count = GetRecordCount("Name='" + name + "' and  LimitID!=" + limitid);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据(事务删除，更新积分规则引用的该限制)
        /// </summary>
        public bool DeleteEX(int PointsLimitID)
        {
            return dal.DeleteEX(PointsLimitID);
        }

        public bool ExistsLimit(int limitid)
        {
            return dal.ExistsLimit(limitid);
        }

        public List<Maticsoft.Model.Members.PointsLimit> GetListByPageExt(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        public bool ExistsName(string name)
        {
            return dal.ExistsName(name);
        }
        #endregion
    }
}

