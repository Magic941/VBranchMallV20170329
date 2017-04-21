/**  版本信息模板在安装目录下，可自行修改。
* BalanceDrawRequest.cs
*
* 功 能： N/A
* 类 名： BalanceDrawRequest
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/4 15:27:04   N/A    初版
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
	/// BalanceDrawRequest
	/// </summary>
	public partial class BalanceDrawRequest
	{
		private readonly IBalanceDrawRequest dal=DAPay.CreateBalanceDrawRequest();
		public BalanceDrawRequest()
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
		public long Add(Maticsoft.Model.Pay.BalanceDrawRequest model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Pay.BalanceDrawRequest model)
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
		public Maticsoft.Model.Pay.BalanceDrawRequest GetModel(long JournalNumber)
		{
			
			return dal.GetModel(JournalNumber);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Pay.BalanceDrawRequest GetModelByCache(long JournalNumber)
		{
			
			string CacheKey = "BalanceDrawRequestModel-" + JournalNumber;
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
			return (Maticsoft.Model.Pay.BalanceDrawRequest)objModel;
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
		public List<Maticsoft.Model.Pay.BalanceDrawRequest> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Pay.BalanceDrawRequest> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Pay.BalanceDrawRequest> modelList = new List<Maticsoft.Model.Pay.BalanceDrawRequest>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Pay.BalanceDrawRequest model;
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
        /// 增加一条数据
        /// </summary>
        public bool AddEx(Maticsoft.Model.Pay.BalanceDrawRequest model)
        {
            BLL.Members.UsersExp userBll = new Members.UsersExp();
            decimal balance = userBll.GetUserBalance(model.UserID);
            return dal.AddEx(model,balance);
        }
    
	    /// <summary>
	    /// 获得提现金额 (状态为未审核的)
	    /// </summary>
	    public decimal GetBalanceDraw(int userid)
	    {
	        return dal.GetBalanceDraw(userid, 1);
	    }
        /// <summary>
		/// 分页获取数据列表
		/// </summary>
		public List<Model.Pay.BalanceDrawRequest> GetListByPage(string strWhere, int startIndex, int endIndex)
		{
            DataSet ds= dal.GetListByPage(strWhere, " JournalNumber desc", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
		}

	    /// <summary>
	    /// 获得数据列表 与users表内联
	    /// </summary>
        public DataSet GetListUser(string strWhere, string filedOrder)
	    {
	        return dal.GetListUser(strWhere, filedOrder);
	    }
        /// <summary>
        /// 获得数据列表 与Supplier表内联
        /// </summary>
        public DataSet GetListSupplier(string strWhere, string filedOrder)
        {
            return dal.GetListSupplier(strWhere, filedOrder);
        }
        ///// <summary>
        ///// 批量修改状态
        ///// </summary>
        //public bool UpdateStatus(string JournalNumberlist, int Status)
        //{
        //    return  dal.UpdateStatus(JournalNumberlist, Status);
        //}
	    /// <summary>
	    /// 修改状态
	    /// </summary>
	    /// <param name="model"></param>
	    /// <param name="Status">状态</param>
	    /// <returns></returns>
	    public bool UpdateStatus(Model.Pay.BalanceDrawRequest model, int Status)
	    {
	        if (model == null)
	        {
                return false;
	        }
            BLL.Members.UsersExp userBll = new Members.UsersExp();
            decimal balance = userBll.GetUserBalance(model.UserID);
	        return dal.UpdateStatus(model, Status, balance);
	    }

        public int GetTotalCount(string startTime, string endTime)
        {
            return dal.GetTotalcount(startTime, endTime);
        }
        public int GetTotalAmount(string startTime, string endTime)
        {
            return dal.GetTotalAmount(startTime, endTime);
        }
	    #endregion  ExtensionMethod
	}
}

