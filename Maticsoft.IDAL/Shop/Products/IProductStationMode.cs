/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IProductStationModes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:28
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Products
{
	/// <summary>
	/// 接口层ProductStationMode
	/// </summary>
	public interface IProductStationMode
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int StationId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Products.ProductStationMode model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Products.ProductStationMode model);

        bool Update2(Maticsoft.Model.Shop.Products.ProductStationMode model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int StationId);
		bool DeleteList(string StationIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Products.ProductStationMode GetModel(int StationId);

        /// <summary>
        /// 根据商品点好查询表内是否存在该条数据(李永琴)
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns></returns>
        Maticsoft.Model.Shop.Products.ProductStationMode GetProductStationModel(int ProductId, int type);

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

        #region New成员方法
        DataSet GetListByType(string strType);
        bool Exists(int productId, int type);
        bool Delete(int productId, int type);
        bool DeleteByType(int type, int categoryId);

	    DataSet GetStationMode(int modeType, int categoryId, string pName,int supplierId);

	    #endregion
	} 
}
