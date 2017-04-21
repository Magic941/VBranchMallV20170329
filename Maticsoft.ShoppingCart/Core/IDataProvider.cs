/**
* IDataProvider.cs
*
* 功 能： IDataProvider
* 类 名： IDataProvider
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

using System.Data;
using Maticsoft.ShoppingCart.Model;

namespace Maticsoft.ShoppingCart.Core
{
    /// <summary>
    /// DB购物车数据处理接口
    /// </summary>
    public interface IDataProvider<TCartInfo, TCartItemInfo> : ICartProvider<TCartInfo, TCartItemInfo>
        where TCartInfo : CartInfo<TCartItemInfo>, new()
        where TCartItemInfo : CartItemInfo, new()
    {
        // bool AddCouponUseRecord(int userId, string couponCode, DateTime useDate, DbTransaction dbTran);
        // bool AddOrderGifts(string orderId, IList<OrderGiftInfo> orderGifts, DbTransaction dbTran);
        // bool AddOrderLineItems(string orderId, IList<LineItemInfo> lineItemInfos, DbTransaction dbTran);
        // bool AddOrderOptions(string orderId, IList<OrderOptionInfo> orderOptions, DbTransaction dbTran);
        // bool CreatOrder(OrderInfo orderInfo, DbTransaction dbTran);
        DataTable GetProductAndSku(int productId);
        DataTable GetProductAndSku(int productId, string skuOptions);
        DataTable GetProductListBySkus(string skus);

        //void LoadCartGift(ICartInfo cartInfo, Dictionary<int, int> giftItemIdList, Dictionary<int, int> giftItemQuantityList, string giftIdString);
        //void LoadCartProduct(TCartInfo cartInfo, Dictionary<object, int> productItemIdList,
        //                     Dictionary<object, int> productItemQuantityList,
        //                     Dictionary<object, string>
        //                         productItemAttributeFillList, string productSKUString);
        void LoadCartProduct(TCartInfo cartInfo);
    }
}

