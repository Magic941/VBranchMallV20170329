/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IAdvertisement.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/05/31 13:22:18
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Collections.Generic;
namespace Maticsoft.IDAL.Settings
{
	/// <summary>
	/// 接口层Advertisement
	/// </summary>
	public interface IAdvertisement
	{
		#region  成员方法
        int GetMaxSequence();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(Maticsoft.Model.Settings.Advertisement model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Settings.Advertisement model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        bool Delete(int AdvertisementId);
        bool DeleteList(string AdvertisementIdlist);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Maticsoft.Model.Settings.Advertisement GetModel(int AdvertisementId);
          /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Settings.Advertisement GetModelByAdvPositionId(int AdvPositionId);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int? Top,string strWhere,string filedOrder);
         /// <summary>
        /// 获得数据列表
        /// </summary>
        List<Maticsoft.Model.Settings.Advertisement> DataTableToList(DataTable dt);

		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法

        DataSet GetTransitionImg(int Aid, int ContentType, int? Num);

        DataSet SelectInfoByContentType(int ContentType);

        int IsExist(int AdvPositionId, int contentType);

        DataSet GetContentType(int AdvPositionId);

	    DataSet GetDefindCode(int AdvPositionId);

	    bool UpdateSeq(int seq, int advId);
	} 
}
