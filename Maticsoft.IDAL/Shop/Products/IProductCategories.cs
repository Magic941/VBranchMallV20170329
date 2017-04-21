/**
* IProductCategories.cs
*
* 功 能： N/A
* 类 名： IProductCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年12月14日 11:37:09   Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Data;

namespace Maticsoft.IDAL.Shop.Products
{
    /// <summary>
    /// 接口层产品类别关联
    /// </summary>
    public interface IProductCategories
    {
        #region 成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Maticsoft.Model.Shop.Products.ProductCategories model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long produtId);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.Products.ProductCategories GetModel(long produtId);

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

        #endregion 成员方法
    
    }
}