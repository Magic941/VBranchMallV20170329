/**
* UserFavAlbum.cs
*
* 功 能： N/A
* 类 名： UserFavAlbum
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:03   N/A    初版
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
	/// 接口层用户关注专辑表
	/// </summary>
	public interface IUserFavAlbum
	{
		#region  成员方法
	

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int AlbumID,int UserID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.SNS.UserFavAlbum model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.UserFavAlbum model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int AlbumID,int UserID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.UserFavAlbum GetModel(int ID);
		Maticsoft.Model.SNS.UserFavAlbum DataRowToModel(DataRow row);
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
        int FavAlbum(int AlbumId, int UserId);
        int UnFavAlbum(int AlbumId, int UserId);
		#endregion  MethodEx
	} 
}
