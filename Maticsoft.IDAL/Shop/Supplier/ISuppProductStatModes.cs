/**
* SuppProductStatModes.cs
*
* 功 能： N/A
* 类 名： SuppProductStatModes
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/27 18:11:59   Ben    初版
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
	/// 接口层SuppProductStatModes
	/// </summary>
	public interface ISuppProductStatModes
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
        int Add(Maticsoft.Model.Shop.Supplier.SuppProductStatModes model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Supplier.SuppProductStatModes model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int StationId);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteList(string StationIdlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Supplier.SuppProductStatModes GetModel(int StationId);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Supplier.SuppProductStatModes DataRowToModel(DataRow row);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
	    /// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx

	    /// <summary>
	    /// 根据type获得数据列表
	    /// </summary>
	    DataSet GetListByType(int supplierId, string strType);

	    /// <summary>
	    /// 是否存在该记录
	    /// </summary>
	    bool Exists(int supplierId, long productId, int type);

	    /// <summary>
	    /// 删除一条数据
	    /// </summary>
	    bool Delete(int supplierId, long productId, int type);

	    /// <summary>
	    /// 清空type下所有商品
	    /// </summary>
	    bool DeleteByType(int supplierId, int type, int categoryId);

	    /// <summary>
	    /// 推荐商品中已经添加到热卖、最新、特价中去的商品信息
	    /// </summary>
	    DataSet GetStationMode(int supplierId, int modeType, int categoryId, string pName);

	    int GetProductNoRecCount(int supplierId, int categoryId, string pName, int modeType);
	    DataSet GetProductNoRecList(int supplierId, int categoryId, string pName, int modeType, int startIndex, int endIndex);
	    
	    #endregion  MethodEx
	} 
}
