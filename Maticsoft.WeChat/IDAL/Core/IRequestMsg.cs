/**
* RequestMsg.cs
*
* 功 能： N/A
* 类 名： RequestMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:17   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.WeChat.IDAL.Core
{
	/// <summary>
	/// 接口层RequestMsg
	/// </summary>
	public interface IRequestMsg
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(long UserMsgId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		long Add(Maticsoft.WeChat.Model.Core.RequestMsg model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.WeChat.Model.Core.RequestMsg model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long UserMsgId);
		bool DeleteList(string UserMsgIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.WeChat.Model.Core.RequestMsg GetModel(long UserMsgId);
		Maticsoft.WeChat.Model.Core.RequestMsg DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
