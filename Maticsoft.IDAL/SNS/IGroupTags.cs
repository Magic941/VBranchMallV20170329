/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IGroupTags.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/10/25 11:53:32
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
namespace Maticsoft.IDAL.SNS
{
	/// <summary>
	/// 接口层小组标签表
	/// </summary>
	public interface IGroupTags
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int TagID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.SNS.GroupTags model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.SNS.GroupTags model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int TagID);
		bool DeleteList(string TagIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.SNS.GroupTags GetModel(int TagID);
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

         #region MethodEx
		   /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string TagName);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int TagID, string TagName);
         /// <summary>
        /// 更新一条数据
        /// </summary>
        bool UpdateIsRecommand(int IsRecommand, string IdList);
         /// <summary>
        /// 更新一条数据
        /// </summary>
        bool UpdateStatus(int Status, string IdList);
	    #endregion
	} 
}
