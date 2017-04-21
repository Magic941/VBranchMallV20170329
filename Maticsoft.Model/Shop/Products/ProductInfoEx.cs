/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Products.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:27
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.Model.Shop.Products
{
    /// <summary>
    /// ProductInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public partial class ProductInfo
    {
        private List<ProductImage> productImageList = new List<ProductImage>();
        /// <summary>
        /// 商品图片
        /// </summary>
        public List<ProductImage> ProductImages
        {
            get { return productImageList; }
            set { productImageList = value; }
        }

        private List<AttributeInfo> attributeInfoList = new List<AttributeInfo>();
        /// <summary>
        /// 属性信息
        /// </summary>
        public List<AttributeInfo> AttributeInfos
        {
            get { return attributeInfoList; }
            set { attributeInfoList = value; }
        }

        public string AttributeInfoNames
        {
            get {

                var attrbuteinfonames = new StringBuilder();

                if (this.AttributeInfos != null && this.AttributeInfos.Count > 0)
                {
                    foreach (var a in AttributeInfos)
                    {
                        attrbuteinfonames.Append(a.AttributeName).Append(",");
                    }
                }
                return attrbuteinfonames.ToString();
            
            }
        }

        private List<SKUInfo> skuInfoList = new List<SKUInfo>();
        /// <summary>
        /// SKU信息
        /// </summary>
        public List<SKUInfo> SkuInfos
        {
            get { return skuInfoList; }
            set { skuInfoList = value; }
        }

        private List<ProductAccessorie> productAccessorieList = new List<ProductAccessorie>();
        /// <summary>
        /// 配件名称
        /// </summary>
        public List<ProductAccessorie> ProductAccessories
        {
            get { return productAccessorieList; }
            set { productAccessorieList = value; }
        }
        private  List<AccessoriesValue>  accessorieValueList = new List<AccessoriesValue>();
        /// <summary>
        /// 配件商品列表
        /// </summary>
        public  List<AccessoriesValue> AccessorieValue
        {
            get { return accessorieValueList; }
            set { accessorieValueList = value; }
        }

        private List<RelatedProduct> relatedProductList = new List<RelatedProduct>();
        /// <summary>
        /// 相关商品
        /// </summary>
        public List<RelatedProduct> RelatedProducts
        {
            get { return relatedProductList; }
            set { relatedProductList = value; }
        }

        private string[] _product_Categories;
        public  string[] Product_Categories
        {
            get { return _product_Categories; }
            set { _product_Categories = value; }
        }

        private string[] _relatedProductId;
        public  string[] RelatedProductId
        {
            get { return _relatedProductId; }
            set { _relatedProductId = value; }
        }

        private List<int> _packageId;
        public List<int> PackageId
        {
            get { return _packageId; }
            set { _packageId = value; }
        }

        private string _searchProductCategories;

        public  string SearchProductCategories
        {
            get { return _searchProductCategories; }
            set { _searchProductCategories = value; }
        }
        public bool isRec { get; set; }

        public bool isNow { get; set; }    
    
        public bool isHot { get; set; }

        public bool isLowPrice { get; set; }

    }
}

