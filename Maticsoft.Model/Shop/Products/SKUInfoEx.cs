/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SKUs.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:34
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Maticsoft.Model.Shop.Products
{
    /// <summary>
    /// SKUInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>

    public partial class SKUInfo
    {
        private List<SKUItem> skuItemList = new List<SKUItem>();
        /// <summary>
        /// SKU子项
        /// </summary>
        public List<SKUItem> SkuItems
        {
            get { return skuItemList; }
            set { skuItemList = value; }
        }

        private string productName;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string productImageUrl;
        /// <summary>
        /// 产品主图
        /// </summary>
        public string ProductImageUrl
        {
            get { return productImageUrl; }
            set { productImageUrl = value; }
        }

        private string productThumbnailUrl;
        /// <summary>
        /// 产品缩略图
        /// </summary>
        public string ProductThumbnailUrl
        {
            get { return productThumbnailUrl; }
            set { productThumbnailUrl = value; }
        }

        private long _attributeId;
        /// <summary>
        /// 属性ID
        /// </summary>
        public long AttributeId
        {
            get { return _attributeId; }
            set { _attributeId = value; }
        }

        private string _valuesStr;
        /// <summary>
        /// 规格值（自定义规格名称）
        /// </summary>
        public string ValuesStr
        {
            get { return _valuesStr; }
            set { _valuesStr = value; }
        }

        private long _valueId;
        /// <summary>
        /// 规格值ID
        /// </summary>
        public long ValueId
        {
            get { return _valueId; }
            set { _valueId = value; }
        }

        private long _specId;
        /// <summary>
        /// 规格--sku管理ID
        /// </summary>
        public long SpecId
        {
            get { return _specId; }
            set { _specId = value; }
        }

        private decimal? _marketprice;
        /// <summary>
        /// 市场价
        /// </summary>
        public decimal? MarketPrice
        {
            set { _marketprice = value; }
            get { return _marketprice; }
        }
        private int _supplierId;
        /// <summary>
        /// 商家id
        /// </summary>
        public int SupplierId
        {
            set { _supplierId = value; }
            get { return _supplierId; }
        }

        /// <summary>
        /// SeoUrl
        /// </summary>
        public string SeoUrl
        {
            set ; get;
        }
    }
}

