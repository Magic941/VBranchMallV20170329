/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IAttributes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:22
// 修改标识： [Rock]  2012年6月14日 17:08:19
// 修改描述： 新增  AttributeManage 放法
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
namespace Maticsoft.IDAL.Shop.Products
{
	/// <summary>
	/// 接口层AttributeInfo
	/// </summary>
	public interface IAttributeInfo
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(long AttributeId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		long Add(Maticsoft.Model.Shop.Products.AttributeInfo model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Products.AttributeInfo model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(long AttributeId);
		bool DeleteList(string AttributeIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Products.AttributeInfo GetModel(long AttributeId);
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
        bool AttributeManage(Model.Shop.Products.AttributeInfo model, Model.Shop.Products.DataProviderAction Action);
        DataSet GetList(long? Typeid, Model.Shop.Products.SearchType searchType);

        bool ChangeImageStatue(long AttributeId, Model.Shop.Products.ProductAttributeModel status);

	    List<Model.Shop.Products.AttributeInfo> GetAttributeInfoList(int? typeId, Model.Shop.Products.SearchType searchType);

        DataSet GetAttribute(int? cateID);

        bool IsExistDefinedAttribute(int typeId, long? attId);

        DataSet GetProductAttributes(long productId);

	    DataSet GetAttributesByCate(int cateID, bool IsChild);


	    bool IsExistName(int typeId, string name);

        List<Model.Shop.Products.AttributeInfo> GetAttributeInfoListByProductId(long productId);

	   
	    #endregion

	} 
}
