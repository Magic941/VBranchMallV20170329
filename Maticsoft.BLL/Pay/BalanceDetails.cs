/**  版本信息模板在安装目录下，可自行修改。
* BalanceDetails.cs
*
* 功 能： N/A
* 类 名： BalanceDetails
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/3 14:45:12   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Pay;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Pay;
namespace Maticsoft.BLL.Pay
{
    /// <summary>
    /// BalanceDetails
    /// </summary>
    public partial class BalanceDetails
    {
        private readonly IBalanceDetails dal=DAPay.CreateBalanceDetails();
        public BalanceDetails()
        {}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long JournalNumber)
        {
            return dal.Exists(JournalNumber);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Pay.BalanceDetails model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Pay.BalanceDetails model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long JournalNumber)
        {
            
            return dal.Delete(JournalNumber);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string JournalNumberlist )
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(JournalNumberlist ,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Pay.BalanceDetails GetModel(long JournalNumber)
        {
            
            return dal.GetModel(JournalNumber);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Pay.BalanceDetails GetModelByCache(long JournalNumber)
        {
            
            string CacheKey = "BalanceDetailsModel-" + JournalNumber;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(JournalNumber);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch{}
            }
            return (Maticsoft.Model.Pay.BalanceDetails)objModel;
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
        public DataSet GetList(int Top,string strWhere,string filedOrder)
        {
            return dal.GetList(Top,strWhere,filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Pay.BalanceDetails> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Pay.BalanceDetails> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Pay.BalanceDetails> modelList = new List<Maticsoft.Model.Pay.BalanceDetails>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Pay.BalanceDetails model;
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
            return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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
        /// 分页获取数据列表
        /// </summary>
        public List<Model.Pay.BalanceDetails> GetListByPage(string strWhere, int startIndex, int endIndex)
        { 
            DataSet ds=dal.GetListByPage(strWhere, " JournalNumber desc  ", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 余额支付
        /// </summary>
        /// <param name="amount">支付金额</param>
        /// <param name="userId">支付用户</param>
        /// <param name="remark">日志信息</param>
        public bool Pay(decimal amount, int userId, string remark)
        {
            return dal.Pay(amount, userId, remark);
        }
        #endregion  ExtensionMethod
    }
}

