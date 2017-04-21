/**  版本信息模板在安装目录下，可自行修改。
* NoReplyMsg.cs
*
* 功 能： N/A
* 类 名： NoReplyMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/2/23 17:18:18   N/A    初版
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
	/// NoReplyMsg
	/// </summary>
	public partial class NoReplyMsg
	{
        private readonly INoReplyMsg dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (INoReplyMsg)new WeChat.SQLServerDAL.Core.NoReplyMsg() : null;
		public NoReplyMsg()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long MsgId)
		{
			return dal.Exists(MsgId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(Maticsoft.WeChat.Model.Core.NoReplyMsg model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Core.NoReplyMsg model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long MsgId)
		{
			
			return dal.Delete(MsgId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string MsgIdlist )
		{
			return dal.DeleteList(MsgIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Core.NoReplyMsg GetModel(long MsgId)
		{
			
			return dal.GetModel(MsgId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Core.NoReplyMsg GetModelByCache(long MsgId)
		{
			
			string CacheKey = "NoReplyMsgModel-" + MsgId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(MsgId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Core.NoReplyMsg)objModel;
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
		public List<Maticsoft.WeChat.Model.Core.NoReplyMsg> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Core.NoReplyMsg> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Core.NoReplyMsg> modelList = new List<Maticsoft.WeChat.Model.Core.NoReplyMsg>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Core.NoReplyMsg model;
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
        /// 更新用户状态
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(int msgId, int status)
        {
            return dal.UpdateStatus(msgId, status);
        }
        /// <summary>
        /// 添加未回复消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public long AddMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg)
        {
            return dal.AddMsg(msg);
        }
		#endregion  ExtensionMethod
	}
}

