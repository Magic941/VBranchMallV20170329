/**  版本信息模板在安装目录下，可自行修改。
* Shippers.cs
*
* 功 能： N/A
* 类 名： Shippers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/5 10:13:21   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Shop
{
	/// <summary>
	/// 接口层Shippers
	/// </summary>
	public interface IShippers
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ShipperId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Shippers model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
        bool Update(Maticsoft.Model.Shop.Shippers model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ShipperId);
		bool DeleteList(string ShipperIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Maticsoft.Model.Shop.Shippers GetModel(int ShipperId);
        Maticsoft.Model.Shop.Shippers DataRowToModel(DataRow row);
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
	    /// 是否存在该记录
	    /// </summary>
        /// <param name="shipperTag">发货人标签</param>
	    /// <param name="shipperId">id</param>
	    /// <returns></returns>
        bool Exists(string shipperTag, int shipperId = 0);

	    /// <summary>
	    /// 设置或取消默认值
	    /// </summary>
	    /// <param name="shipperId"></param>
	    /// <param name="isDefault"></param>
	    /// <returns></returns>
	    bool UpdateIsDefault(int shipperId, bool isDefault);


	    #endregion  MethodEx
	} 
}
