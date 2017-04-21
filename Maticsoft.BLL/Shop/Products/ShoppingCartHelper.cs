/**
* ShoppingCartHelper.cs
*
* 功 能： [N/A]
* 类 名： ShoppingCartHelper
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/17 15:18:50  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using Maticsoft.Model.Shop.Products;
using Maticsoft.ShoppingCart.Core;

namespace Maticsoft.BLL.Shop.Products
{
    public class ShoppingCartHelper
    {
        private readonly ICartProvider<ShoppingCartInfo, ShoppingCartItem> _cartProvider;

        public ShoppingCartHelper(int userId)
        {
            //使用Cookie购物车
            _cartProvider = new CookieProvider<ShoppingCartInfo, ShoppingCartItem>(userId);
        }


        #region ICartProvider<ShoppingCartInfo,ShoppingCartItem> 成员

        public static void LoadShoppingCart(int userId)
        {
            CookieProvider<ShoppingCartInfo, ShoppingCartItem>.LoadShoppingCart(userId);
        }

        public void AddItem(ShoppingCartItem itemInfo)
        {

            _cartProvider.AddItem(itemInfo);
        }

        public void ClearShoppingCart()
        {
            _cartProvider.ClearShoppingCart();
        }

        public ShoppingCartInfo GetShoppingCart()
        {
            return _cartProvider.GetShoppingCart();
        }

        public ShoppingCartInfo GetShoppingCart4Selected()
        {
            return _cartProvider.GetShoppingCart4Selected();
        }

        public void RemoveItem(int itemId)
        {
            _cartProvider.RemoveItem(itemId);
        }

        public void UpdateItemQuantity(int itemId, int quantity)
        {
            _cartProvider.UpdateItemQuantity(itemId, quantity);
        }


        public void SaveShoppingCart(ShoppingCartInfo cartInfo)
        {
            _cartProvider.SaveShoppingCart(cartInfo);
        }
        #endregion
    }
}
