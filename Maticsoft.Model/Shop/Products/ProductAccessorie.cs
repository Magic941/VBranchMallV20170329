/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductAccessories.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:24
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
    /// ProductAccessories:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProductAccessorie
    {
        #region Model
        private int _accessoriesid;
        private long _productid;
        private string _name;
        private int _maxquantity = 0;
        private int _minquantity = 0;
        private int _discounttype = 1;
        private decimal _discountamount = 0M;
        private int _type = 1;
        private int _stock = 0;
        /// <summary>
        /// 
        /// </summary>
        public int AccessoriesId
        {
            set { _accessoriesid = value; }
            get { return _accessoriesid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 配件组件名称 或 优惠套餐名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 最大购买量    暂未启用
        /// </summary>
        public int MaxQuantity
        {
            set { _maxquantity = value; }
            get { return _maxquantity; }
        }
        /// <summary>
        /// 最小购买量    暂未启用
        /// </summary>
        public int MinQuantity
        {
            set { _minquantity = value; }
            get { return _minquantity; }
        }
        /// <summary>
        /// 优惠类型   1：金额  2:折扣
        /// </summary>
        public int DiscountType
        {
            set { _discounttype = value; }
            get { return _discounttype; }
        }
        /// <summary>
        /// 优惠价         类型为  优惠组合（套装）时启用该字段
        /// </summary>
        public decimal DiscountAmount
        {
            set { _discountamount = value; }
            get { return _discountamount; }
        }
        /// <summary>
        /// 类型： 1 配件 2优惠组合（套装）
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 库存 0不启用
        /// </summary>
        public int Stock
        {
            set { _stock = value; }
            get { return _stock; }
        }
        #endregion Model

    }
}

