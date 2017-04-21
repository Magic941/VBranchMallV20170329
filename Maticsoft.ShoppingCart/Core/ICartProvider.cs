/**
* ICartProvider.cs
*
* 功 能： 购物车处理接口
* 类 名： ICartProvider
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

using Maticsoft.ShoppingCart.Model;

namespace Maticsoft.ShoppingCart.Core
{
    /// <summary>
    /// 购物车处理接口
    /// </summary>
    public interface ICartProvider<TCartInfo, TCartItemInfo>
        where TCartInfo : CartInfo<TCartItemInfo>, new()
        where TCartItemInfo : CartItemInfo, new()
    {
        /// <summary>
        /// 清空购物车
        /// </summary>
        void ClearShoppingCart();

        /// <summary>
        /// 获取购物车
        /// </summary>
        TCartInfo GetShoppingCart();

        /// <summary>
        /// 获取已选内容的购物车
        /// </summary>
        /// <remarks>提交订单页面专用</remarks>
        TCartInfo GetShoppingCart4Selected();

        /// <summary>
        /// 添加新项
        /// </summary>
        void AddItem(TCartItemInfo itemInfo);

        ///// <summary>
        ///// 检查购物车数量是否合法
        ///// </summary>
        //string CheckQuantity(TCartInfo cartInfo);

        /// <summary>
        /// 删除指定项
        /// </summary>
        void RemoveItem(int itemId);

        /// <summary>
        /// 更新指定项的数量
        /// </summary>
        void UpdateItemQuantity(int itemId, int quantity);

        /// <summary>
        /// 保存购物车到Cookie
        /// </summary>
        void SaveShoppingCart(TCartInfo cartInfo);
    }
}

