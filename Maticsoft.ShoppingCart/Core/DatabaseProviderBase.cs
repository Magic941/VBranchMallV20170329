/**
* DatabaseProvider.cs
*
* 功 能： DB购物车抽象类
* 类 名： DatabaseProvider
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
using Maticsoft.ShoppingCart.Model;

namespace Maticsoft.ShoppingCart.Core
{
    /// <summary>
    /// 购物车抽象类
    /// </summary>
    public abstract class DatabaseProviderBase : DatabaseProviderBase<CartInfo<CartItemInfo>, CartItemInfo>
    {
        protected DatabaseProviderBase(IDataProvider<CartInfo<CartItemInfo>, CartItemInfo> provider)
            : base(provider)
        {
        }
    }

    /// <summary>
    /// DB购物车抽象类
    /// </summary>
    public abstract class DatabaseProviderBase<TCartInfo, TCartItemInfo> : ICartProvider<TCartInfo, TCartItemInfo>
        where TCartInfo : CartInfo<TCartItemInfo>, new()
        where TCartItemInfo : CartItemInfo, new()
    {
        private readonly IDataProvider<TCartInfo, TCartItemInfo> _provider;

        protected DatabaseProviderBase(IDataProvider<TCartInfo, TCartItemInfo> provider)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            _provider = provider;
        }


        public virtual void AddItem(TCartItemInfo itemInfo)
        {
            if (itemInfo.Quantity <= 0)
            {
                itemInfo.Quantity = 1;
            }
            _provider.AddItem(itemInfo);
        }

        //public abstract string CheckQuantity(TCartInfo cartInfo);

        public virtual void ClearShoppingCart()
        {
            _provider.ClearShoppingCart();
        }

        public virtual TCartInfo GetShoppingCart()
        {
            return _provider.GetShoppingCart();
        }

        public virtual void RemoveItem(int itemId)
        {
            _provider.RemoveItem(itemId);
        }

        public virtual void UpdateItemQuantity(int itemId, int quantity)
        {
            _provider.UpdateItemQuantity(itemId, quantity);
        }

        #region TODO
        public abstract TCartInfo GetShoppingCart4Selected();
        public abstract void SaveShoppingCart(TCartInfo cartInfo);
        #endregion
    }
}

