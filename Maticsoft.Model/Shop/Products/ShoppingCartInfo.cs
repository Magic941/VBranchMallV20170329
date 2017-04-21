/**
* ShoppingCartInfo.cs
*
* 功 能： 购物车对象
* 类 名： ShoppingCartInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/17            Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Linq;
using System.Collections.Generic;

namespace Maticsoft.Model.Shop.Products
{
    /// <summary>
    /// 购物车对象
    /// </summary>
    [Serializable]
    public class ShoppingCartInfo : ShoppingCart.Model.CartInfo<ShoppingCartItem>
    {
        #region 总价

        /// <summary>
        /// 购物车项目总销售价 (非支付)
        /// </summary>
        public decimal TotalSellPriceNew
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return 0;

                decimal totalPrice = 1;
                Items.ForEach(info => totalPrice += info.SellPrice * info.Quantity);
                return totalPrice;
            }
        }

        public decimal TotalSellPrice { get; set; }
        public decimal TotalAdjustedPrice { get; set; }


        /// <summary>
        /// 购物车项目调整后总价 (非支付)
        /// </summary>
        /// <remarks>优惠后总价</remarks>
        public decimal TotalAdjustedPriceNew
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return 0;

                decimal totalPrice = 0;
                Items.ForEach(info => totalPrice += info.AdjustedPrice * info.Quantity);
                return totalPrice;
            }
        }

        /// <summary>
        /// 购物车项目总成本价
        /// </summary>
        public decimal TotalCostPrice
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return 0;

                decimal totalPrice = 0;
                Items.ForEach(info => totalPrice += info.CostPrice * info.Quantity);
                return totalPrice;
            }
        }

        /// <summary>
        /// 购物车项目总成本价
        /// </summary>
        public decimal TotalCostPrice2
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return 0;

                decimal totalPrice = 0;
                Items.ForEach(info => totalPrice += info.CostPrice2 * info.Quantity);
                return totalPrice;
            }
        }

        #endregion

        #region 总重
        /// <summary>
        /// 购物车项目总重量
        /// </summary>
        private int _totalWeight;

        public int TotalWeight
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return 0;
                var totalWeight = 0;
                Items.ForEach(info => totalWeight += info.Weight * info.Quantity);
                return totalWeight;
            }
            set { _totalWeight = value; }
        }
        #endregion

        #region 总积分
        /// <summary>
        /// 购物车项目总积分
        /// </summary>
        public int TotalPoints
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return 0;

                int totalPoints = 0;
                Items.ForEach(info => totalPoints += info.Points * info.Quantity);
                return totalPoints;
            }
        }
        #endregion

        public List<Maticsoft.Model.CustomModel.SupplierPrice> _SupplierPriceList = new List<CustomModel.SupplierPrice>();
        public List<Maticsoft.Model.CustomModel.SupplierPrice> SupplierPriceList
        {
            get { return _SupplierPriceList; }
            set { _SupplierPriceList = value; }
        }

        #region 计算运费
        /// <summary>
        /// 根据收货地区计算运费
        /// </summary>
        /// <param name="shippingType">配送对象</param>
        /// <param name="shippingRegion">地区价格对象</param>
        /// <returns>运费合计</returns>
        public decimal CalcFreight(Model.Shop.Shipping.ShippingType shippingType,
            Shipping.ShippingRegionGroups shippingRegion)
        {
            
            if (shippingType == null) return 0;

          //  int totalWeight = _totalWeight;
            int totalWeight = TotalWeight;
            //总重 = 0 或 参数非法 不计算运费
            if (totalWeight == 0)
                return 0;

            //无加价/增重 返回配送默认价格
            if (!shippingType.AddPrice.HasValue ||
                !shippingType.AddWeight.HasValue)
                return shippingType.Price;

            decimal price;
            decimal? addPrice;

            if (shippingRegion != null)
            {
                //使用地区价格
                price = shippingRegion.Price;
                addPrice = shippingRegion.AddPrice;
            }
            else
            {
                //使用配送默认价格
                price = shippingType.Price;
                addPrice = shippingType.AddPrice;
            }

            //无加价 返回配送地区/默认价格
            if (!addPrice.HasValue) return price;

            //未超过首重 不计运费
            if (totalWeight <= shippingType.Weight)
                return price;

            //计算增重
            int scale = 1;
            if (totalWeight > shippingType.Weight && shippingType.AddWeight.Value > 0)
            {
                int excessWeight = totalWeight - shippingType.Weight;
                if ((excessWeight % shippingType.AddWeight) == 0)
                {
                    scale = (totalWeight - shippingType.Weight) / shippingType.AddWeight.Value;
                }
                else
                {
                    scale = ((totalWeight - shippingType.Weight) / shippingType.AddWeight.Value) + 1;
                }
            }

            //计算最终运费
            return totalWeight > shippingType.Weight
                ? (scale * addPrice.Value) + price
                : price;
        }
        #endregion
    }
}

