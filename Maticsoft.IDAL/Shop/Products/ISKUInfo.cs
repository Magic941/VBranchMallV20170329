/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ISKUs.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:34
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
    /// 接口层SKUInfo
	/// </summary>
	public interface ISKUInfo
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(long SkuId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		long Add(Maticsoft.Model.Shop.Products.SKUInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Products.SKUInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long SkuId);
		bool DeleteList(string SkuIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Products.SKUInfo GetModel(long SkuId);
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


        DataSet GetSKUListByPage(string strWhere, string orderby, int startIndex, int endIndex, out int dataCount, long productId);

        DataSet PrductsSkuInfo(long prductId);

        DataSet ProductsSkuInfo(string strWhere);

	    bool Exists(string skuCode, long prductId);


	    int GetStockById(long productId);

        int GetStockBySKU(string SKU, bool IsOpenAS);

	    /// <summary>
	    /// 得到一个对象实体
	    /// </summary>
	    Maticsoft.Model.Shop.Products.SKUInfo GetModelBySKU(string sku);

      
	    /// <summary>
	    /// 是否存在该记录
	    /// </summary>
	    /// <remarks>添加组合商品时，判断这个sku是否是自己的</remarks>
	    bool ExistsEx(string SKU, long prductId);

	    /// <summary>
	    /// 获得数据列表
	    /// </summary>
	    DataSet GetListInnerJoinProd(string strWhere);

	    /// <summary>
	    /// 获取SKU数据列表
	    /// </summary>
	    DataSet GetSKUList(string strWhere, int AccessoriesId, string orderby, long productId);

        DataSet GetSKUListByCid(int cid);
	} 
}
