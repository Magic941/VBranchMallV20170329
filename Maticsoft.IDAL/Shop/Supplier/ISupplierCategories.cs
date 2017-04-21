/**
* SupplierCategories.cs
*
* 功 能： N/A
* 类 名： SupplierCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:48   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Supplier
{
	/// <summary>
	/// 接口层供应商(店铺)分类
	/// </summary>
	public interface ISupplierCategories
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int CategoryId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Supplier.SupplierCategories model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Supplier.SupplierCategories model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int CategoryId);
		bool DeleteList(string CategoryIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Supplier.SupplierCategories GetModel(int CategoryId);
		Maticsoft.Model.Shop.Supplier.SupplierCategories DataRowToModel(DataRow row);
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
        bool UpdateSeqByCid(int Seq, int Cid, int SupplierId);
        bool UpdatePath(Model.Shop.Supplier.SupplierCategories model);
        bool UpdateDepthAndPath(int Cid, int Depth, string Path, int SupplierId);
        bool UpdateHasChild(int cid, int SupplierId);
	    bool UpdateHasChild(int cid, int SupplierId, bool Status);
         
        int GetMaxSeqByCid(int parentId, int SupplierId);

	    /// <summary>
	    /// 同级下是否存在同名
	    /// </summary>
	    /// <param name="parentId">父节点</param>
	    /// <param name="name">名称</param>
	    /// <param name="SupplierId">供应商id</param>
	    /// <param name="categoryId">类别id</param>
	    /// <returns></returns>
	    bool IsExisted(int parentId, string name, int SupplierId, int categoryId = 0);

	    /// <summary>
	    ///根据商品id获取分类信息
	    /// </summary>
	    /// <param name="productId"></param>
	    /// <returns></returns>
	    Model.Shop.Supplier.SupplierCategories GetModelByProductId(long productId);

	    /// <summary>
	    /// 判断该分类下是否有商品
	    /// </summary>
	    /// <param name="CategoryId"></param>
	    /// <returns></returns>
	    bool IsExistsProd(int CategoryId);

	    int GetCountBySupIdEx(int depth, int supplierId);

	    #endregion  MethodEx
	} 
}
