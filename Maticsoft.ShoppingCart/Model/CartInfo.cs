/**
* ICartInfo.cs
*
* 功 能： 购物车对象接口
* 类 名： ICartInfo
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

using System.Collections.Generic;

namespace Maticsoft.ShoppingCart.Model
{
    /// <summary>
    /// 购物车对象接口
    /// </summary>
    public class CartInfo<T>
        where T : CartItemInfo
    {
        private System.Collections.Generic.List<T> _list = new List<T>();

        /// <summary>
        /// 项目集合对象
        /// </summary>
        public List<T> Items
        {
            get { return _list; }
            set { _list = value; }
        }
        /// <summary>
        /// 商品总数
        /// </summary>
        public int Quantity
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return 0;

                int num = 0;
                Items.ForEach(xx => num += xx.Quantity);
                return num;
            }
        }

       
        /// <summary>
        /// 项目 索引器
        /// </summary>
        public T this[int itemId]
        {
            get { return Items.Find(xx => xx.ItemId == itemId); }
        }

        /// <summary>
        /// SKU 索引器
        /// </summary>
        public T this[string target]
        {
            get { return Items.Find(xx => xx.SKU == target); }
        }
    }
}

