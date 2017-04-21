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
    /// UserRank
    /// </summary>
    public partial class UserRank
    {
        private readonly IUserRank dal = DAMembers.CreateUserRank();
        public UserRank()
        { }
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
        public bool Exists(int RankId)
        {
            return dal.Exists(RankId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.UserRank model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.UserRank model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int RankId)
        {

            return dal.Delete(RankId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string RankIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(RankIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.UserRank GetModel(int RankId)
        {

            return dal.GetModel(RankId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.UserRank GetModelByCache(int RankId)
        {

            string CacheKey = "UserRankModel-" + RankId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RankId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.UserRank)objModel;
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
        public List<Maticsoft.Model.Members.UserRank> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.UserRank> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.UserRank> modelList = new List<Maticsoft.Model.Members.UserRank>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.UserRank model;
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
        #region  ExtensionMethod
        /// <summary>
        /// 根据用户分数获取等级
        /// </summary>
        /// <param name="grades">用户分数</param>
        /// <returns></returns>
        public string GetUserLevel(int? grades)
        {
            if (grades.HasValue)
            {
                string name = dal.GetUserLevel(grades.Value);
                if (!String.IsNullOrWhiteSpace(name))
                {
                    return name;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        public List<Maticsoft.Model.Members.UserRank> GetListByPageExt(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        #endregion  ExtensionMethod
    }
}

