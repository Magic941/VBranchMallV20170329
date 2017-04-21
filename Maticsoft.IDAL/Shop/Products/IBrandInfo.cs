/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IBrands.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/12 10:02:41
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
	/// 接口层BrandInfo
	/// </summary>
	public interface IBrandInfo
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();        
        /// <summary>
        /// 得到最大顺序
        /// </summary>
        int GetMaxDisplaySequence();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int BrandId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Products.BrandInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Products.BrandInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int BrandId);
		bool DeleteList(string BrandIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Products.BrandInfo GetModel(int BrandId);
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
        /// 添加品牌
        /// </summary>
        bool CreateBrandsAndTypes(Model.Shop.Products.BrandInfo model, Model.Shop.Products.DataProviderAction action);

        DataSet GetListByProductTypeId(int ProductTypeId,int top);

        DataSet GetListByProductTypeId(out int rowCount, out int pageCount, int ProductTypeId, int PageIndex, int PageSize,int action);

        Model.Shop.Products.BrandInfo GetRelatedProduct(int brandsId);

        Model.Shop.Products.BrandInfo GetRelatedProduct(int? brandsId, int? ProductTypeId);

        DataSet GetBrandsListByCateId(int? cateId);

	    DataSet GetBrandsByCateId(int cateId, bool IsChild,int Top);

	    #endregion
	} 
}
