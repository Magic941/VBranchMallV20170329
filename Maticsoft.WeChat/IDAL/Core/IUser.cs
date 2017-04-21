/**
* User.cs
*
* 功 能： N/A
* 类 名： User
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:22   N/A    初版
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
	/// 接口层User
	/// </summary>
	public interface IUser
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.WeChat.Model.Core.User model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.WeChat.Model.Core.User model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.WeChat.Model.Core.User GetModel(int ID);
		Maticsoft.WeChat.Model.Core.User DataRowToModel(DataRow row);
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
	    bool DeleteUser(string OpenId, string userName);

	    bool Exists(string OpenId, string userName);

	    bool UpdateEx(Maticsoft.WeChat.Model.Core.User model);

	    bool CancelUser(string OpenId, string userName);

	    bool UpdateGroup(int groupId, string ids);

        string GetNickName(string userName, string OpenId);

        bool UpdateNick(string userName, string OpenId, string nickName);

        bool UpdateNick(int Id, string nickName);

	    Maticsoft.WeChat.Model.Core.User GetUser(string OpenId, string userName);

        bool UpdateMsgTime(string openId, string userName, DateTime date);

        bool IsCanSend(string user,int hours);

        string GetNickName(string userName);
        bool UpdateUser(Maticsoft.WeChat.Model.Core.User model);

        DataSet GetDayCount(string strWhere);
        DataSet GetCancelCount(string strWhere);
	    #endregion  MethodEx
	} 
}
