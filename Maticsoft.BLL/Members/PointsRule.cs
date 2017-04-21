using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Members;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
using System.Text;
namespace Maticsoft.BLL.Members
{
	/// <summary>
	/// 积分规则
	/// </summary>
	public partial class PointsRule
	{
        private readonly IPointsRule dal = DAMembers.CreatePointsRule();
		public PointsRule()
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
        public bool Exists(int RuleId)
        {
            return dal.Exists(RuleId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.PointsRule model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.PointsRule model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int RuleId)
        {
            return dal.Delete(RuleId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string RuleIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(RuleIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.PointsRule GetModel(int RuleId)
        {
            return dal.GetModel(RuleId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.PointsRule GetModelByCache(int RuleId)
        {

            string CacheKey = "PointsRuleModel-" + RuleId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RuleId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.PointsRule)objModel;
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
        public List<Maticsoft.Model.Members.PointsRule> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.PointsRule> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.PointsRule> modelList = new List<Maticsoft.Model.Members.PointsRule>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.PointsRule model;
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
        /// <summary>
        /// 根据关键字获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public DataSet GetListByKeyWord(string keyword)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat("  name like '%{0}%'", keyword);
                return dal.GetList(strWhere.ToString());
            }
            else
            {
                return GetAllList();
            }
           
        }
        /// <summary>
        /// 获取积分规则名称
        /// </summary>
        /// <param name="ruleaction"></param>
        /// <returns></returns>
        public string GetRuleName(int ruleid)
        {
            if (ruleid ==(int)Maticsoft.Model.Members.Enum.PointRule.Order)
            {
                    return "完成订单";
            }
            return dal.GetRuleName(ruleid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.PointsRule GetModel(int actionId,int userId)
        {
            //根据用户获取对应的规则 暂时只针对后台用户
            int targetid = 0;
            int targetType =0;
            return dal.GetModel(actionId, targetid, targetType);
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ActionId,int targetId=0,int targetType=0)
        {
            return dal.Exists(ActionId, targetId, targetType);
        }

        public bool ExistsActionId(int ActionId)
        {
            return dal.ExistsActionId(ActionId);
        }
        public List<Maticsoft.Model.Members.PointsRule> GetListByPageExt(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }
        #endregion
    }
}

