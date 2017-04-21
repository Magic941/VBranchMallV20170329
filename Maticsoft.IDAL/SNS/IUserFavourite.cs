/**
* UserFavourite.cs
*
* 功 能： N/A
* 类 名： UserFavourite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:04   N/A    初版
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
	/// 接口层用户的喜欢表
	/// </summary>
	public interface IUserFavourite
	{
		#region  成员方法
		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(int CreatedUserID, int Type, int TargetID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.SNS.UserFavourite model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.UserFavourite model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int FavouriteID);
		bool DeleteList(string FavouriteIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.UserFavourite GetModel(int FavouriteID);
		Maticsoft.Model.SNS.UserFavourite DataRowToModel(DataRow row);
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
        bool AddEx(Maticsoft.Model.SNS.UserFavourite FavModell,int TopicId, int ReplyId);

        /// <summary>
        ///根据用户的id得到喜欢的数据，分页
        /// </summary>
        /// <param name="UserId"></param>
        DataSet GetFavListByPage(int UserId, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 删除喜欢的动作
        /// </summary>
        bool DeleteEx(int UserId, int TargetId, int Type);
		#endregion  MethodEx
	} 
}
