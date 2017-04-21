/**
* OrderManage.cs
*
* 功 能： [N/A]
* 类 名： OrderManage
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/20 18:29:57  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Order;
using System.Data;
using Maticsoft.Model.Shop.Order;
using System;
using System.Collections.Generic;
using Maticsoft.BLL.Shop.PromoteSales;
using System.Threading;

namespace Maticsoft.BLL.Shop.Order
{
    public static class OrderManage
    {
        private static readonly BLL.Shop.Order.Orders orderManage = new BLL.Shop.Order.Orders();
        private static readonly IOrderService service = DAShopOrder.CreateOrderService();
        private static Maticsoft.BLL.Shop.PromoteSales.GroupBuy groupBuyBll = new GroupBuy();

        #region 创建订单
        public static long CreateOrder(Model.Shop.Order.OrderInfo orderInfo)
        {
            if (orderInfo.GroupBuyId > 0)
            {
                /*随机休5秒*/
                var random = new Random();
                var r = random.Next(4000);
                Thread.Sleep(r);
                Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = groupBuyBll.GetModelByGroupID((int)orderInfo.GroupBuyId);
                var groupProductItem = orderInfo.OrderItems.Find(c => { return c.ProductId == buyModel.ProductId; });
                if (groupProductItem != null && buyModel.PromotionType == 1)
                {

                    if (groupBuyBll.GetGroupBuyLimit(orderInfo.ShipName, orderInfo.ShipCellPhone))
                    {
                        //休息5秒再向前走
                        Thread.Sleep(2000);
                    }
                }
            }
            Action action1 = delegate()
            {
                //throw new Exception("抢购活动超过限购数量！");

                if (orderInfo.GroupBuyId > 0)
                {
                    //收货人和电话一致，认为是同一个帐号，一次只能下一单少于1元的单

                    //检查是否在事务中完限购数量检查,  从数据库中取
                    Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = groupBuyBll.GetModelByGroupID((int)orderInfo.GroupBuyId);
                    var groupProductItem = orderInfo.OrderItems.Find(c => { return c.ProductId == buyModel.ProductId; });
                    if (groupProductItem != null && buyModel.PromotionType == 1)
                    {                       
                        if (buyModel.MaxCount < buyModel.BuyCount + groupProductItem.Quantity)
                        {
                            throw new Exception("抢购活动超过限购数量,您下单动作慢了！");
                        }
                        groupBuyBll.UpdateBuyCount((int)orderInfo.GroupBuyId, groupProductItem.Quantity);
                        if (orderInfo.OrderCode.IndexOf("WX") >= 0)
                            Maticsoft.Common.ErrorLogTxt.GetInstance("团购并发日志").Write("订单已执行了并发控制" + orderInfo.OrderCode);
                    }
                }
            };
            Action action2 = delegate()
            {
                //if (orderInfo.GroupBuyId > 0 )
                //{
                //    Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = groupBuyBll.GetModelByCache((int)orderInfo.GroupBuyId);
                //    var groupProductItem = orderInfo.OrderItems.Find(c => { return c.ProductId == buyModel.ProductId; });

                //    if (groupProductItem != null)
                //    {
                //        groupBuyBll.UpdateBuyCount((int)orderInfo.GroupBuyId, groupProductItem.Quantity);
                //    }
                //}

            };

            return service.CreateOrder(orderInfo, action1, action2);
        }
        #endregion

        #region 取消订单
        public static bool CancelOrder(Model.Shop.Order.OrderInfo orderInfo, Accounts.Bus.User currentUser)
        {
            //普通用户非法取消订单
            if (currentUser.UserType == "UU" &&
                (orderInfo.OrderStatus != 0 || currentUser.UserID != orderInfo.BuyerID))
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog();
                model.Loginfo = string.Format("入侵拦截:[非法取消订单][Maticsoft.BLL.Shop.Order.OrderManage.CancelOrder] IP:[{0}]", System.Web.HttpContext.Current.Request.UserHostAddress);
                model.StackTrace = string.Empty;
                model.Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
                return false;
            }
            return service.CancelOrder(orderInfo, currentUser);
        }

        public static bool SystemCancelOrder(Model.Shop.Order.OrderInfo orderInfo, Accounts.Bus.User currentUser)
        {
            return service.CancelOrder(orderInfo, currentUser);
        }
        #endregion

        #region 支付订单

        public static bool PayForOrder(Model.Shop.Order.OrderInfo orderInfo, string notifyid, Accounts.Bus.User currentUser = null)
        {
            //订单项检测 - 补全数据
            if (orderInfo.OrderItems == null || orderInfo.OrderItems.Count < 1)
                orderInfo = orderManage.GetModelInfoByCache(orderInfo.OrderId);

            //子订单检测 - 补全数据
            if (orderInfo.HasChildren && orderInfo.SubOrders.Count < 1)
                orderInfo.SubOrders = orderManage.GetModelList(" ParentOrderId=" + orderInfo.OrderId);

            return service.PayForOrder(orderInfo, notifyid, currentUser);
        }

        #endregion

        #region 完成订单

        public static bool CompleteOrder(OrderInfo orderInfo, Accounts.Bus.User currentUser)
        {
            //普通用户/商家 非法完成订单
            if (
                (currentUser.UserType == "UU" &&
                 (orderInfo.OrderStatus != 1 || currentUser.UserID != orderInfo.BuyerID)) ||
                (currentUser.UserType == "SP" &&
                 (orderInfo.OrderStatus != 1 || currentUser.UserID != orderInfo.SellerID))
                )
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog();
                model.Loginfo =
                    string.Format("入侵拦截:[非法完成订单][Maticsoft.BLL.Shop.Order.OrderManage.CompleteOrder] IP:[{0}]",
                        System.Web.HttpContext.Current.Request.UserHostAddress);
                model.StackTrace = string.Empty;
                model.Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
                return false;
            }
            return service.CompleteOrder(orderInfo, currentUser);
        }

        public static bool SystemCompleteOrder(OrderInfo orderInfo, Accounts.Bus.User currentUser)
        {
            return service.CompleteOrder(orderInfo, currentUser);
        }

        #endregion

        #region 订单统计
        public static DataSet Stat4OrderStatus(int orderStatus)
        {
            return service.Stat4OrderStatus(orderStatus);
        }
        public static DataSet Stat4OrderStatus(int orderStatus, DateTime startDate, DateTime endDate, int? supplierId = null)
        {
            return service.Stat4OrderStatus(orderStatus, startDate, endDate, supplierId);
        }

        public static DataSet StatSales(Model.Shop.Order.StatisticMode mode, System.DateTime startDate, System.DateTime endDate, int? supplierId = null)
        {
            return service.StatSales(mode, startDate, endDate, supplierId);
        }
        #endregion

        #region 商品销量排行统计
        public static DataSet ProductSales(Model.Shop.Order.StatisticMode mode, System.DateTime startDate, System.DateTime endDate, int supplierId = 0)
        {
            return service.ProductSales(mode, startDate, endDate, supplierId);
        }

        public static DataSet ProductSaleInfo(StatisticMode mode, DateTime startDate, DateTime endDate)
        {
            return service.ProductSaleInfo(mode, startDate, endDate);
        }
        public static List<Maticsoft.ViewModel.Order.OrderInfoExPage> GetListByPageEx(StatisticMode mode, DateTime startDate, DateTime endDate, string modes, int startIndex, int endIndex, int supplierId = 0)
        {
            DataSet ds = GetListByPage(mode, startDate, endDate, modes, startIndex, endIndex, supplierId);
            return DataTableToList(ds.Tables[0]);
        }
        public static List<Maticsoft.ViewModel.Order.OrderInfoExPage> DataTableToList(DataTable dt)
        {
            List<Maticsoft.ViewModel.Order.OrderInfoExPage> modelList = new List<Maticsoft.ViewModel.Order.OrderInfoExPage>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.ViewModel.Order.OrderInfoExPage model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = Maticsoft.SQLServerDAL.Shop.Order.OrderService.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;

        }
        public static DataSet GetListByPage(StatisticMode mode, DateTime startDate, DateTime endDate, string modes, int startIndex, int endIndex, int supplierId = 0)
        {
            return service.GetListByPage(mode, startDate, endDate, modes, startIndex, endIndex, supplierId);
        }
        public static int GetRecordCount(StatisticMode mode, DateTime startDate, DateTime endDate, string modes, int startIndex, int endIndex, int supplierId = 0)
        {
            return service.GetRecordCount(mode, startDate, endDate, modes, startIndex, endIndex, supplierId);
        }
        #endregion

        #region 店铺排行
        public static DataSet ShopSale(StatisticMode mode, DateTime startDate, DateTime endDate)
        {
            return service.ShopSale(mode, startDate, endDate);
        }
        public static DataSet ShopSaleInfo(StatisticMode mode, DateTime startDate, DateTime endDate)
        {
            return service.ShopSaleInfo(mode, startDate, endDate);
        }
        #endregion
    }
}
