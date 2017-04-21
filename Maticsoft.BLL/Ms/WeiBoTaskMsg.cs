/**
* WeiBoTaskMsg.cs
*
* 功 能： N/A
* 类 名： WeiBoTaskMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/22 20:27:24   N/A    初版
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
using Maticsoft.Model.Ms;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Ms;
namespace Maticsoft.BLL.Ms
{
	/// <summary>
	/// WeiBoTaskMsg
	/// </summary>
	public partial class WeiBoTaskMsg
	{
		private readonly IWeiBoTaskMsg dal=DAMs.CreateWeiBoTaskMsg();
		public WeiBoTaskMsg()
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
		public bool Exists(int WeiBoTaskId)
		{
			return dal.Exists(WeiBoTaskId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Ms.WeiBoTaskMsg model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Ms.WeiBoTaskMsg model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int WeiBoTaskId)
		{
			
			return dal.Delete(WeiBoTaskId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string WeiBoTaskIdlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(WeiBoTaskIdlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Ms.WeiBoTaskMsg GetModel(int WeiBoTaskId)
		{
			
			return dal.GetModel(WeiBoTaskId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Ms.WeiBoTaskMsg GetModelByCache(int WeiBoTaskId)
		{
			
			string CacheKey = "WeiBoTaskMsgModel-" + WeiBoTaskId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(WeiBoTaskId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Ms.WeiBoTaskMsg)objModel;
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
		public List<Maticsoft.Model.Ms.WeiBoTaskMsg> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Ms.WeiBoTaskMsg> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Ms.WeiBoTaskMsg> modelList = new List<Maticsoft.Model.Ms.WeiBoTaskMsg>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Ms.WeiBoTaskMsg model;
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
        public int AddEx(Maticsoft.Model.Ms.WeiBoMsg model)
        {
            return dal.AddEx(model);
        }
        /// <summary>
        /// 微博发送任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool  RunTask(int taskId)
        {
            Maticsoft.Model.Ms.WeiBoTaskMsg taskMsgModel = GetModel(taskId);
            if (taskMsgModel != null)
            {
                return dal.RunTask(taskMsgModel);
            }
            return true;
        }
		#endregion  ExtensionMethod
	}
}

