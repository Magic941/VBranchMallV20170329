/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IAccessoriesValues.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:21
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
    /// 接口层AccessoriesValue
    /// </summary>
    public interface IAccessoriesValue
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int AccessoriesId, int AccessoriesValueId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Shop.Products.AccessoriesValue model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Products.AccessoriesValue model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int AccessoriesValueId);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int AccessoriesId, int AccessoriesValueId);
        bool DeleteList(string AccessoriesValueIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Products.AccessoriesValue GetModel(int AccessoriesValueId);
        Maticsoft.Model.Shop.Products.AccessoriesValue DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);

        #endregion  成员方法

        #region  ExtensionMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="AccessoriesId">组合id</param>
        /// <param name="SKU">SKU</param>
        /// <returns></returns>
        bool Exists(int AccessoriesId, string  SKU);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        ///<param name="AccessoriesId">组合id</param>
        /// <param name="SKU">SKU</param>
        /// <returns></returns>
        bool Delete(int AccessoriesId, string  SKU);
        #endregion

    }
}
