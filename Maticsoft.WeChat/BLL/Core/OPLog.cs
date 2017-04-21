/**  版本信息模板在安装目录下，可自行修改。
* OPLog.cs
*
* 功 能： N/A
* 类 名： OPLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/6 18:34:25   N/A    初版
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
using Maticsoft.WeChat.Model.Core;
using Maticsoft.WeChat.IDAL.Core;
namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// OPLog
	/// </summary>
	public partial class OPLog
	{
        private readonly IOPLog dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IOPLog)new WeChat.SQLServerDAL.Core.OPLog(): null;
		public OPLog()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(Maticsoft.WeChat.Model.Core.OPLog model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Core.OPLog model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Core.OPLog GetModel(long ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Core.OPLog GetModelByCache(long ID)
		{
			
			string CacheKey = "OPLogModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Core.OPLog)objModel;
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
		public List<Maticsoft.WeChat.Model.Core.OPLog> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Core.OPLog> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Core.OPLog> modelList = new List<Maticsoft.WeChat.Model.Core.OPLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Core.OPLog model;
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
        /// 添加微信浏览日志
        /// </summary>
        /// <param name="user"></param>
        /// <param name="openid"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool AddViewLog(string user,string openid,string url,string remark="")
        {
            if (String.IsNullOrWhiteSpace(user) || String.IsNullOrWhiteSpace(openid))
            {
                return false;
            }
            Maticsoft.WeChat.BLL.Core.OPLog logBll = new OPLog();
            Maticsoft.WeChat.Model.Core.OPLog logModel = new Model.Core.OPLog();
            logModel.OPTime = DateTime.Now;
            logModel.OpenId = openid;
            logModel.OPType = 0;
            logModel.Url = url;
            logModel.UserName = user;
            logModel.Remark = remark;
            return logBll.Add(logModel)>0;
        }
        /// <summary>
        /// 添加点击事件日志
        /// </summary>
        /// <param name="user"></param>
        /// <param name="openid"></param>
        /// <param name="actionId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static bool AddClickLog(string user, string openid, int actionId,string remark="")
        {
            if (String.IsNullOrWhiteSpace(user) || String.IsNullOrWhiteSpace(openid))
            {
                return false;
            }
            Maticsoft.WeChat.BLL.Core.OPLog logBll = new OPLog();
            Maticsoft.WeChat.Model.Core.OPLog logModel = new Model.Core.OPLog();
            logModel.OPTime = DateTime.Now;
            logModel.OpenId = openid;
            logModel.OPType = 1;
            logModel.ActionId = actionId;
            logModel.UserName = user;
            logModel.Remark = remark;
            return logBll.Add(logModel) > 0;
        }
		#endregion  ExtensionMethod
	}
}

