/**
* CartItemType.cs
*
* 功 能： 购物车项目类型
* 类 名： CartItemType
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2013/05/09  研发部    姚远   初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/

namespace Maticsoft.ShoppingCart.Model
{
    /// <summary>
    /// 购物车项目类型
    /// </summary>
    public enum CartItemType
    {
        None = -1,
        /// <summary>
        /// 商品
        /// </summary>
        Product = 1,
        /// <summary>
        /// 礼品
        /// </summary>
        Gift = 2,
        /// <summary>
        /// 打包
        /// </summary>
        Package = 3,
        /// <summary>
        /// 计时抢购
        /// </summary>
        Countdown = 4
    }
}

