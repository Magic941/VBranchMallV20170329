/**  版本信息模板在安装目录下，可自行修改。
* GoodsType.cs
*
* 功 能： N/A
* 类 名： GoodsType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/25 10:14:27   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Shop.Products
{
	/// <summary>
	/// 接口层GoodsType
	/// </summary>
	public interface IGoodsType
	{
		#region  成员方法
        /// <summary>
		/// 得到最大Sort
		/// </summary>
        int GetMaxSort();
        /// <summary>
        /// 是否存在该类型名称
        /// </summary>
        /// <returns></returns>
        bool ExistsByTypeName(string TypeName);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Shop.Products.GoodsType model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Shop.Products.GoodsType model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int GoodTypeID);
		bool DeleteList(string GoodTypeIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Shop.Products.GoodsType GetModel(int GoodTypeID);

        Maticsoft.Model.Shop.Products.GoodsType GetModelBuyPID(int PID);

		Maticsoft.Model.Shop.Products.GoodsType DataRowToModel(DataRow row);
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
        /// 获得活动分类列表数据列表
        /// </summary>
        DataSet GetGoodsActiveTypeList(string strWhere);

		#endregion  MethodEx
	} 
}
