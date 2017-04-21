/**
* UserShip.cs
*
* 功 能： N/A
* 类 名： UserShip
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:05   N/A    初版
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
namespace Maticsoft.IDAL.SNS
{
	/// <summary>
	/// 接口层用户的喜欢表
	/// </summary>
	public interface IUserShip
	{
		#region  成员方法
		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ActiveUserID,int PassiveUserID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(Maticsoft.Model.SNS.UserShip model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.UserShip model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ActiveUserID,int PassiveUserID);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.UserShip GetModel(int ActiveUserID,int PassiveUserID);
		Maticsoft.Model.SNS.UserShip DataRowToModel(DataRow row);
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
          		/// <summary>
		/// 获得数据列表
		/// </summary>
        List<Maticsoft.Model.SNS.UserShip> DataTableToList(DataTable dt);
		#endregion  成员方法

		#region  MethodEx
        /// <summary>
        /// 关注某人的情况
        /// </summary>

        bool FellowUser( Maticsoft.Model.SNS.UserShip model);
         bool UnFellowUser(int Userid, int FellowUserId);

         /// <summary>
		/// 获得数据列表
		/// </summary>
         List<Maticsoft.Model.SNS.UserShip> DataTableToListEx(DataTable dt);

         #region 分页获取用户所有粉丝的数据列表
         /// <summary>
         /// 分页获取用户所有粉丝的数据列表
         /// </summary>
         /// <returns></returns>
         DataSet GetListByFansPage(int userid, string orderby, int startIndex, int endIndex);
         #endregion

         #region 分页获取用户关注的所有用户数据列表
         /// <summary>
         /// 分页获取用户关注的所有用户数据列表
         /// </summary>
         /// <returns></returns>
         DataSet GetListByFellowsPage(int userid, string orderby, int startIndex, int endIndex);
         #endregion

         #region 添加关注
        /// <summary>
        /// 添加关注
        /// </summary>
        /// <param name="ActiveUserID"></param>
        /// <param name="PassiveUserID"></param>
        /// <returns></returns>
         bool AddAttention(int ActiveUserID, int PassiveUserID);
        #endregion

         #region 取消关注
         /// <summary>
         /// 取消关注
         /// </summary>
         /// <param name="ActiveUserID"></param>
         /// <param name="PassiveUserID"></param>
         /// <returns></returns>
         bool CancelAttention(int ActiveUserID, int PassiveUserID);
         #endregion

        #endregion  MethodEx
    } 
}
