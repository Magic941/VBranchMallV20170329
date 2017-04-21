/**
* ICartItemInfo.cs
*
* 功 能： 购物车项目接口
* 类 名： ICartItemInfo
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2013/05/08  研发部    姚远   初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/


using System;

namespace Maticsoft.ShoppingCart.Model
{
    /// <summary>
    /// 购物车项目接口
    /// </summary>
    [Serializable]
    public class CartItemInfo
    {
        private int _itemid;
        private int _userid;
        private string _sku;
        private int _quantity;
        private CartItemType _itemtype = CartItemType.None;
        private long _productId;
        private string _name;
        private string _thumbnailsurl;

        private decimal _costprice;
        private decimal _costprice2;
        private decimal _sellprice;
        private decimal _marketPrice;

        private bool _selected = true;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 购物车Id
        /// </summary>
        public int ItemId
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// SKU
        /// </summary>
        /// <remarks>商品SKU</remarks>
        public string SKU
        {
            set { _sku = value; }
            get { return _sku; }
        }
        /// <summary>
        /// 项目类型
        /// </summary>
        public CartItemType ItemType
        {
            set { _itemtype = value; }
            get { return _itemtype; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 商品Id
        /// </summary>
        public long ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbnailsUrl
        {
            set { _thumbnailsurl = value; }
            get { return _thumbnailsurl; }
        }
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }
        /// <summary>
        /// 第二成本价
        /// </summary>
        public decimal CostPrice2
        {
            set { _costprice2 = value; }
            get { return _costprice2; }
        }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal SellPrice
        {
            set { _sellprice = value; }
            get { return _sellprice; }
        }
        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MarketPrice
        {
            set { _marketPrice = value; }
            get { return _marketPrice; }
        }

        /// <summary>
        /// 是否被选中
        /// </summary>
        /// <remarks>购物车环节使用 默认true 已选中</remarks>
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public int QuantityLimit { get; set; }

        public int PromotionType { get; set; }
    }
}

