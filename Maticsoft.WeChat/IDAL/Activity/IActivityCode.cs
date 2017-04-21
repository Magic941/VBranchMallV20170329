/**  版本信息模板在安装目录下，可自行修改。
* ActivityCode.cs
*
* 功 能： N/A
* 类 名： ActivityCode
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
namespace Maticsoft.WeChat.IDAL.Activity
{
	/// <summary>
	/// 接口层ActivityCode
	/// </summary>
	public interface IActivityCode
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string ActivityCode);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(Maticsoft.WeChat.Model.Activity.ActivityCode model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.WeChat.Model.Activity.ActivityCode model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string ActivityCode);
		bool DeleteList(string ActivityCodelist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.WeChat.Model.Activity.ActivityCode GetModel(string ActivityCode);
		Maticsoft.WeChat.Model.Activity.ActivityCode DataRowToModel(DataRow row);
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
        Maticsoft.WeChat.Model.Activity.ActivityCode GetRandCode(int activityId);
        bool UpdateUser(string codeName, string userId, string userName, int status, string phone, string remark);
        bool  UpdateStatusList(string ids,int status);
		#endregion  MethodEx
	} 
}
