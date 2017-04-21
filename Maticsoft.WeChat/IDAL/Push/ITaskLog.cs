/**  版本信息模板在安装目录下，可自行修改。
* TaskLog.cs
*
* 功 能： N/A
* 类 名： TaskLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/7 17:57:54   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.WeChat.IDAL.Push
{
	/// <summary>
	/// 接口层TaskLog
	/// </summary>
	public interface ITaskLog
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int TaskId,string UserName);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(Maticsoft.WeChat.Model.Push.TaskLog model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.WeChat.Model.Push.TaskLog model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int TaskId,string UserName,string tableName);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Maticsoft.WeChat.Model.Push.TaskLog GetModel(int TaskId, string UserName, string tableName);
		Maticsoft.WeChat.Model.Push.TaskLog DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
        DataSet GetList(string strWhere, string tableName);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder, string tableName);
        int GetRecordCount(string strWhere, string tableName);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, string tableName);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx
        bool Add(int taskId, string userName);
		#endregion  MethodEx
	} 
}
