/**
* ShippingRegionGroups.cs
*
* 功 能： N/A
* 类 名： ShippingRegionGroups
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/8 18:17:32   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;

namespace Maticsoft.Model.Shop.Shipping
{
    /// <summary>
    /// ShippingRegionGroups:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ShippingRegionGroups
    {
        public ShippingRegionGroups()
        { }
        #region Model
        private int _groupid;
        private int _modeid;
        private decimal _price;
        private decimal? _addprice;
        /// <summary>
        /// 
        /// </summary>
        public int GroupId
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModeId
        {
            set { _modeid = value; }
            get { return _modeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AddPrice
        {
            set { _addprice = value; }
            get { return _addprice; }
        }
        #endregion Model

        public string[] RegionIds { get; set; }
    }
}

