/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：IProductTypes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:31
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
    /// 接口层ProductType
    /// </summary>
    public interface IProductType
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int TypeId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Shop.Products.ProductType model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Products.ProductType model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int TypeId);
        bool DeleteList(string TypeIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Products.ProductType GetModel(int TypeId);
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


        #region NewMethod

        System.Collections.Generic.List<Maticsoft.Model.Shop.Products.ProductType> GetProductTypes();

        bool ProductTypeManage(Model.Shop.Products.ProductType model, Model.Shop.Products.DataProviderAction Action, out int Typeid);

        bool DeleteManage(int? TypeId, long? AttributeId, long? ValueId);

        bool SwapSeqManage(int? TypeId, long? AttributeId, long? ValueId, Model.Shop.Products.SwapSequenceIndex zIndex, bool UsageMode);
        #endregion
    }
}
