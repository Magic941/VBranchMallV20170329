/**
* ShoppingCarts.cs
*
* 功 能： N/A
* 类 名： ShoppingCarts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 11:18:08   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Products
{
    /// <summary>
    /// ShoppingCarts:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ShoppingCartItem : ShoppingCart.Model.CartItemInfo
    {
        public ShoppingCartItem()
        { }
        #region Model

        private string _unit;
        private string _saleDes;
        private string _description;

        private string[] _skuValues;
        private string _skuImageUrl;

        private decimal _adjustedprice;
        private string _attributes;
        private int _weight;
        private decimal? _deduct;
        private int _points;
        private int? _productlineid;
        private int? _supplierid;
        private string _suppliername;
        private int _ActiveID;

        public int ActiveID
        {
            get { return _ActiveID; }
            set { _ActiveID = value; }
        }
        private int _ActiveType;

        public int ActiveType
        {
            get { return _ActiveType; }
            set { _ActiveType = value; }
        }

        /// <summary>
        /// SKU值集合
        /// </summary>
        public string[] SkuValues
        {
            get { return _skuValues; }
            set { _skuValues = value; }
        }

        /// <summary>
        /// SKU图片URL
        /// </summary>
        public string SkuImageUrl
        {
            get { return _skuImageUrl; }
            set { _skuImageUrl = value; }
        }

        /// <summary>
        /// 促销说明
        /// </summary>
        public string SaleDes
        {
            get { return _saleDes; }
            set { _saleDes = value; }
        }
        /// <summary>
        /// 商品说明
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        /// <summary>
        /// 调整后的价格
        /// </summary>
        public decimal AdjustedPrice
        {
            set { _adjustedprice = value; }
            get { return _adjustedprice; }
        }

        /// <summary>
        /// 属性
        /// </summary>
        public string Attributes
        {
            set { _attributes = value; }
            get { return _attributes; }
        }
        /// <summary>
        /// 商品重量
        /// </summary>
        public int Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 扣除金额
        /// </summary>
        public decimal? Deduct
        {
            set { _deduct = value; }
            get { return _deduct; }
        }
        /// <summary>
        /// 商品所赠的积分
        /// </summary>
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        /// <summary>
        /// 商品线ID
        /// </summary>
        public int? ProductLineId
        {
            set { _productlineid = value; }
            get { return _productlineid; }
        }
        /// <summary>
        /// 供货商ID
        /// </summary>
        public int? SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 供货商名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }

        /// <summary>
        /// 团购的ID
        /// </summary>
        public int GroupBuyId
        {
            set;
            get;
        }
        #endregion Model

        # region 扩展属性

        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }

        public decimal SubTotal
        {
            get
            {
                return Quantity * SellPrice;
            }
        }

        #endregion
    }
}

