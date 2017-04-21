/**
* IOrderService.cs
*
* 功 能： Shop模块-订单相关 含多表事务操作接口
* 类 名： IOrderService
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/05/20 18:35:05  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/


using Maticsoft.Model.Shop.Order;
using System;
namespace Maticsoft.IDAL.Shop.Order
{
    /// <summary>
    /// Shop模块-订单相关 含多表事务操作接口
    /// </summary>
    public interface IOrderService
    {
        long CreateOrder(Model.Shop.Order.OrderInfo orderInfo, Action checkCallBack1, Action transactionCallBack2);

        bool CancelOrder(Model.Shop.Order.OrderInfo orderInfo, Accounts.Bus.User currentUser = null);

        bool PayForOrder(Model.Shop.Order.OrderInfo orderInfo,string notifyid="", Accounts.Bus.User currentUser = null);
        System.Data.DataSet Stat4OrderStatus(int orderStatus);
        System.Data.DataSet StatSales(Model.Shop.Order.StatisticMode mode, System.DateTime startDate, System.DateTime endDate, int? supplierId = null);
        System.Data.DataSet ProductSales(Model.Shop.Order.StatisticMode mode, System.DateTime startDate, System.DateTime endDate, int supplierId);
        System.Data.DataSet ProductSaleInfo(StatisticMode mode, DateTime startDate, DateTime endDate);
        System.Data.DataSet ShopSale(StatisticMode mode, DateTime startDate, DateTime endDate);
        System.Data.DataSet ShopSaleInfo(StatisticMode mode, DateTime startDate, DateTime endDate);
        int GetRecordCount(StatisticMode mode, DateTime startDate, DateTime endDate, string modes, int startIndex, int endIndex,int supplierId);
        System.Data.DataSet GetListByPage( StatisticMode mode, DateTime startDate, DateTime endDate,string modes, int startIndex, int endIndex,int supplierId);
        System.Data.DataSet Stat4OrderStatus(int orderStatus, DateTime startDate, DateTime endDate, int? supplierId = null);
        bool CompleteOrder(OrderInfo orderInfo, Accounts.Bus.User currentUser = null);
    }
}
