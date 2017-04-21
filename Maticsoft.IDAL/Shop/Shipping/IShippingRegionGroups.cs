/**
* ShippingRegionGroups.cs
*
* 功 能： N/A
* 类 名： ShippingRegionGroups
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/8 18:17:33   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Data;
using System.Collections.Generic;
using Maticsoft.Model.Shop.Shipping;

namespace Maticsoft.IDAL.Shop.Shipping
{
	/// <summary>
	/// 接口层ShippingRegionGroups
	/// </summary>
	public interface IShippingRegionGroups
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int GroupId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Model.Shop.Shipping.ShippingRegionGroups model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Model.Shop.Shipping.ShippingRegionGroups model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int GroupId);
		bool DeleteList(string GroupIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.Shop.Shipping.ShippingRegionGroups GetModel(int GroupId);
		Model.Shop.Shipping.ShippingRegionGroups DataRowToModel(DataRow row);
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

		#endregion  MethodEx

	    /// <summary>
	    /// 保存配送地区价格
	    /// </summary>
        bool SaveShippingRegionGroups(List<Model.Shop.Shipping.ShippingRegionGroups> list);

	    /// <summary>
	    /// 清空配送地区价格
	    /// </summary>
	    bool ClearShippingRegionGroups(int modeId);

	    /// <summary>
	    /// 获取配送地区价格
	    /// </summary>
	    DataSet GetShippingRegionGroups(int modelId);


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.Shop.Shipping.ShippingRegionGroups GetShippingRegion(int modeId, int topRegionId);
	} 
}
