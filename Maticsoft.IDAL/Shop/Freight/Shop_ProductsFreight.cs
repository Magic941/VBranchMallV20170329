﻿/**  版本信息模板在安装目录下，可自行修改。
* Shop_freefreight.cs
*
* 功 能： N/A
* 类 名： Shop_freefreight
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/10/22 16:32:56   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.shop.Freight
{

    /// <summary>
    /// 接口层Shop_ProductsFreight
    /// </summary>
    public interface IShop_ProductsFreight
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ProductCode, string SKU);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Maticsoft.Model.shop.Freight.Shop_ProductsFreight model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.shop.Freight.Shop_ProductsFreight model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string ProductCode, string SKU);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.shop.Freight.Shop_ProductsFreight GetModel(string ProductCode, string SKU);
        Maticsoft.Model.shop.Freight.Shop_ProductsFreight DataRowToModel(DataRow row);
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
        #region  MethodEx
        Maticsoft.Model.shop.Freight.Shop_ProductsFreight GetModel(long productid);
        #endregion  MethodEx
    } 
}
