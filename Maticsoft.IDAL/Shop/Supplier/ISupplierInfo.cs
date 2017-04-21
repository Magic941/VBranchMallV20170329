/**
* Suppliers.cs
*
* 功 能： N/A
* 类 名： Suppliers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:50   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
namespace Maticsoft.IDAL.Shop.Supplier
{
	/// <summary>
	/// 接口层供应商
	/// </summary>
	public interface ISupplierInfo
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int SupplierId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Supplier.SupplierInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Supplier.SupplierInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int SupplierId);
		bool DeleteList(string SupplierIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Supplier.SupplierInfo GetModel(int SupplierId);
		Maticsoft.Model.Shop.Supplier.SupplierInfo DataRowToModel(DataRow row);
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

        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strWhere);

        /// <summary>
        /// 供应商名称是否已存在
        /// </summary>
        bool Exists(string Name);

        /// <summary>
        /// 供应商名称是否已存在
        /// </summary>
        bool Exists(string Name, int SupplierID);
		#endregion  MethodEx

	    DataSet GetStatisticsSupply(int supplierId);
	    DataSet GetStatisticsSales(int supplierId, int year);

	    /// <summary>
	    /// 店铺名称是否已存在
	    /// </summary>
	    bool ExistsShopName(string Name);

	    /// <summary>
	    /// 店铺名称是否已存在
	    /// </summary>
	    bool ExistsShopName(string Name, int SupplierID);
        
	    /// <summary>
	    /// 得到一个对象实体
	    /// </summary>
	    Maticsoft.Model.Shop.Supplier.SupplierInfo GetModelByUserId(int UserId);

	    /// <summary>
	    /// 关闭店铺 
	    /// </summary>
	    bool CloseShop(int SupplierId);

	    /// <summary>
	    /// 根据店铺名称得到店铺model
	    /// </summary>
	    /// <param name="ShopName"></param>
	    /// <returns></returns>
	    Maticsoft.Model.Shop.Supplier.SupplierInfo GetModelByShopName(string ShopName);

	    /// <summary>
	    /// 推荐
	    /// </summary>
	    /// <param name="SupplierId"></param>
	    /// <param name="Rec"></param>
	    /// <returns></returns>
	    bool SetRec(int SupplierId, int Rec);

	    /// <summary>
	    /// 删除一条数据 事物删除该商家的所以关联数据
	    /// </summary>
	    /// <param name="SupplierId"></param>
	    /// <returns></returns>
	    bool DeleteEx(int SupplierId);

        DataSet GetSupplierByPosition(double latitudeLow, double longitudeLow, double latitudeHigh,
                                     double longitudeHigh, double range);


        bool Update(Model.Shop.Supplier.SupplierInfo supplierInfo,int SupplierId,List<int> idList);


        bool Add(int supplierId,  List<int> idList);
	} 
}
