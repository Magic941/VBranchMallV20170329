/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：RelatedProducts.cs
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
namespace Maticsoft.Model.Shop.Products
{
    /// <summary>
    /// RelatedProducts:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RelatedProduct
    {
        public RelatedProduct()
        { }
        #region Model
        private long _relatedid;
        private long _productid;
        /// <summary>
        /// 
        /// </summary>
        public long RelatedId
        {
            set { _relatedid = value; }
            get { return _relatedid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        #endregion Model

    }
}

