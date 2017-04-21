/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ISKUItems.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:32
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
	/// 接口层SKUItem
	/// </summary>
	public interface ISKUItem
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(long SkuId,long AttributeId,long ValueId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(Maticsoft.Model.Shop.Products.SKUItem model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Products.SKUItem model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long SkuId,long AttributeId,long ValueId);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Products.SKUItem GetModel(long SkuId,long AttributeId,long ValueId);
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

        #region NewMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long? SkuId, long? AttributeId, long? ValueId);

        DataSet AttributeValuesInfo(long productId);
        #endregion

	    DataSet GetSKUItem4AttrValByProductId(long productId);
	    DataSet GetSKUItem4AttrValBySkuId(long skuId);
	} 
}
