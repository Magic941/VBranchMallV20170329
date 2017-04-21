/**
* GroupUsers.cs
*
* 功 能： N/A
* 类 名： GroupUsers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:44   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.SNS
{
	/// <summary>
	/// 接口层小组人员表
	/// </summary>
	public interface IGroupUsers
	{
		#region  成员方法
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int GroupID,int UserID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(Maticsoft.Model.SNS.GroupUsers model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.GroupUsers model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int GroupID,int UserID);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.GroupUsers GetModel(int GroupID,int UserID);
		Maticsoft.Model.SNS.GroupUsers DataRowToModel(DataRow row);
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
        /// <summary>
        /// 更新用户状态
        /// </summary>
        bool UpdateStatus(int GroupID, int UserID, int Status);
        /// <summary>
        /// 用户加入小组
        /// </summary>
        bool AddEx(Maticsoft.Model.SNS.GroupUsers model);
        /// <summary>
        /// 更新推荐状态
        /// </summary>
        bool UpdateRecommand(int GroupID, int UserID, int Recommand);
        /// <summary>
        /// 删除小组用户
        /// </summary>
        bool DeleteEx(int GroupId, int UserID);
        /// <summary>
        /// 删除小组用户
        /// </summary>
        bool DeleteEx(int GroupId, string UserID); 
        /// <summary>
        /// 删除小组用户
        /// </summary>
        bool UpdateRole(int GroupID, int UserID, int Role);

        /// <summary>
        /// 根据提帖子的id对该组的状态进行更新
        /// </summary>
        bool UpdateStatusByTopicIds(string Ids,int Status);

         /// <summary>
        /// 根据帖子回复对用户禁言
        /// </summary>
        bool UpdateStatusByTopicReplyIds(string Ids, int Status);
		#endregion  MethodEx
	} 
}
