/**  版本信息模板在安装目录下，可自行修改。
* ActivityDetail.cs
*
* 功 能： N/A
* 类 名： ActivityDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:16   N/A    初版
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
using Maticsoft.WeChat.Model.Activity;
using Maticsoft.WeChat.IDAL.Activity;
namespace Maticsoft.WeChat.BLL.Activity
{
	/// <summary>
	/// ActivityDetail
	/// </summary>
	public partial class ActivityDetail
	{
        private readonly IActivityDetail dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IActivityDetail)new Maticsoft.WeChat.SQLServerDAL.Activity.ActivityDetail() : null;//暂时预留
		public ActivityDetail()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long DetailId)
		{
			return dal.Exists(DetailId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(Maticsoft.WeChat.Model.Activity.ActivityDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Activity.ActivityDetail model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long DetailId)
		{
			
			return dal.Delete(DetailId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string DetailIdlist )
		{
			return dal.DeleteList(DetailIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Activity.ActivityDetail GetModel(long DetailId)
		{
			
			return dal.GetModel(DetailId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Activity.ActivityDetail GetModelByCache(long DetailId)
		{
			
			string CacheKey = "ActivityDetailModel-" + DetailId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DetailId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Activity.ActivityDetail)objModel;
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
		public List<Maticsoft.WeChat.Model.Activity.ActivityDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Activity.ActivityDetail> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Activity.ActivityDetail> modelList = new List<Maticsoft.WeChat.Model.Activity.ActivityDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Activity.ActivityDetail model;
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
        /// 获取用户参与活动的总次数
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUserTotal(int activityId, string userName)
        {
            return GetRecordCount("ActivityId=" + activityId + " and UserName='" + userName + "'");
        }
        /// <summary>
        /// 每个人的参与次数
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetEachCount(int activityId, string userName)
        {
            return dal.GetEachCount(activityId, userName);
        }
        /// <summary>
        /// 获取活动每天参与次数
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public int GetDayTotal(int activityId)
        {
            return dal.GetDayTotal(activityId);
        }
		#endregion  ExtensionMethod
	}
}

