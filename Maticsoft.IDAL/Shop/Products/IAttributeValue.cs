/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IAttributeValues.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:22
// 修改标识： [Rock]            2012年6月14日 17:00:46
// 修改描述：新增 【AttributeValueManage】方法
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Products
{
	/// <summary>
	/// 接口层AttributeValue
	/// </summary>
	public interface IAttributeValue
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(long ValueId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		long Add(Maticsoft.Model.Shop.Products.AttributeValue model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Products.AttributeValue model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long ValueId);
		bool DeleteList(string ValueIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Products.AttributeValue GetModel(long ValueId);
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

        bool AttributeValueManage(Model.Shop.Products.AttributeValue model, Model.Shop.Products.DataProviderAction Action);

        DataSet GetListByAttribute(long AttributeId);
        bool DeleteImage(long valueId);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(long? AttributeId);

        DataSet GetAttributeValue(int? cateID);

	    /// <summary>
	    /// 根据商品listid和属性id获取商品属性值
	    /// </summary>
	    /// <param name="PordIDList">商品idList</param>
	    ///  <param name="attrid">属性id</param>
	    /// <returns></returns>
	    DataSet GetAttrValue(string PordIDList, int attrid);

	    /// <summary>
	    /// 是否存在该记录
	    /// </summary>
	    bool Exists(string strWhere);

	    #endregion
	} 
}
