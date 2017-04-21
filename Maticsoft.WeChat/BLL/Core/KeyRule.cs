/**
* KeyRule.cs
*
* 功 能： N/A
* 类 名： KeyRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:25   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.WeChat.Model.Core;
using Maticsoft.WeChat.IDAL.Core;
using System.Text;
namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// KeyRule
	/// </summary>
	public partial class KeyRule
	{
        private readonly IKeyRule dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IKeyRule)new Maticsoft.WeChat.SQLServerDAL.Core.KeyRule() : null;//暂时预留
		public KeyRule()
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
        public int Add(Maticsoft.WeChat.Model.Core.KeyRule model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.KeyRule model)
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
            return dal.DeleteList(RuleIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.KeyRule GetModel(int RuleId)
        {

            return dal.GetModel(RuleId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.KeyRule GetModelByCache(int RuleId)
        {

            string CacheKey = "KeyRuleModel-" + RuleId;
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
            return (Maticsoft.WeChat.Model.Core.KeyRule)objModel;
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
        public List<Maticsoft.WeChat.Model.Core.KeyRule> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.KeyRule> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.KeyRule> modelList = new List<Maticsoft.WeChat.Model.Core.KeyRule>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.KeyRule model;
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
	    public bool DeleteListEx(string RuleIdlist)
	    {
            return dal.DeleteListEx(RuleIdlist);
	    }

        public List<Maticsoft.WeChat.Model.Core.KeyRule> GetRuleList(string openId, string keyword, int startIndex, int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" Name like '%{0}%'", keyword);
            }
            DataSet ds = GetListByPage(strWhere.ToString(), "", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        public int GetCount(string openId, string keyword)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" Name like '%{0}%'", keyword);
            }
            return GetRecordCount(strWhere.ToString());
        }

	    #endregion  ExtensionMethod
	}
}

