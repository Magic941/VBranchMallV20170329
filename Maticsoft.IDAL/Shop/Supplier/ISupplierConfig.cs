/**
* SupplierConfig.cs
*
* 功 能： N/A
* 类 名： SupplierConfig
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
	/// 接口层供应商(店铺)配置
	/// </summary>
	public interface ISupplierConfig
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Supplier.SupplierConfig model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Supplier.SupplierConfig model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Supplier.SupplierConfig GetModel(int ID);
		Maticsoft.Model.Shop.Supplier.SupplierConfig DataRowToModel(DataRow row);
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
	    /// 根据供应商id和参数名称获取参数值 
	    /// </summary>
        /// <param name="suppId">供应商id</param>
	    /// <param name="keyName">参数名称</param>
	    /// <returns></returns>
        string GetValue(int suppId, string keyName);

	    Maticsoft.Model.Shop.Supplier.SupplierConfig GetModel(string strWhere);

	    bool Exists(string key, int sipId);

        bool UpdateEx(Maticsoft.Model.Shop.Supplier.SupplierConfig model);

	    #endregion  MethodEx
	} 
}
