/**
* OrderService.cs
*
* 功 能： Shop模块-订单相关 多表事务操作类
* 类 名： OrderService
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/22 10:46:33  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using Maticsoft.Common;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.Model.Shop.Order;
using System.Data;
using System.Collections.Generic;
using System;
using Maticsoft.Model.Shop.PaymentNumber;
using System.IO;

namespace Maticsoft.SQLServerDAL.Shop.Order
{
    /// <summary>
    /// Shop模块-订单相关 多表事务操作类
    /// </summary>
    public class OrderService : IOrderService
    {
        #region 创建订单

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <returns>主订单Id</returns>
        public long CreateOrder(OrderInfo orderInfo,Action checkCallBack1,Action transactionCallBack2)
        {
            using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    object result;
                    try
                    {
                        if (checkCallBack1 != null)
                        {
                            //直接抛出异常
                            checkCallBack1();
                        }
                      
                        //DONE: 1.新增订单
                        result = DbHelperSQL.GetSingle4Trans(GenerateOrderInfo(orderInfo), transaction);

                        //加载订单主键
                        orderInfo.OrderId = Globals.SafeLong(result.ToString(), -1);

                        //DONE: 2.新增订单项目
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateOrderItems(orderInfo), transaction);

                        //DONE: 3.新增订单创建记录
                        DbHelperSQL.ExecuteSqlTran4Indentity(GenerateOrderAction(orderInfo), transaction);

                        //DONE: 4.减少商品SKU库存
                        DbHelperSQL.ExecuteSqlTran4Indentity(CutSKUStock(orderInfo), transaction);


                        //TODO: 5.增加Shop用户扩展表的订单数 Count+1

                        //DONE: 6.新增已拆单的子订单数据
                        if (orderInfo.SubOrders != null &&
                            orderInfo.SubOrders.Count > 0)
                        {
                            foreach (OrderInfo subOrder in orderInfo.SubOrders)
                            {
                                //加载主订单Id
                                subOrder.ParentOrderId = orderInfo.OrderId;
                                CreateSubOrder(subOrder, transaction);
                            }
                            //TODO: 7.或增加 主订单日志 拆单记录
                        }
                        //事务内更新语句，如团购的抢购数量
                        if (transactionCallBack2!=null)
                        {

                            transactionCallBack2.Invoke();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return orderInfo.OrderId;
        }

        #region 创建子订单(拆单)
        /// <summary>
        /// 创建子订单(拆单)
        /// </summary>
        /// <param name="subInfo">子订单信息</param>
        /// <param name="transaction">主订单事务</param>
        /// <returns>子订单Id</returns>
        public long CreateSubOrder(OrderInfo subInfo, SqlTransaction transaction)
        {
            object result;
            subInfo.CardNo = "";
            //DONE: 1.新增订单
            result = DbHelperSQL.GetSingle4Trans(GenerateOrderInfo(subInfo), transaction);

            //加载子订单主键
            subInfo.OrderId = Globals.SafeLong(result.ToString(), -1);

            //DONE: 2.新增订单项目
            DbHelperSQL.ExecuteSqlTran4Indentity(GenerateOrderItems(subInfo), transaction);

            //DONE: 3.新增订单拆单记录
            DbHelperSQL.ExecuteSqlTran4Indentity(GenerateOrderAction(subInfo), transaction);

            return subInfo.OrderId;
        }
        #endregion

        #region UpdateProductStock

        private List<CommandInfo> CutSKUStock(OrderInfo orderInfo)
        {
            List<CommandInfo> listComand = new List<CommandInfo>();
            foreach (Model.Shop.Order.OrderItems item in orderInfo.OrderItems)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update Shop_SKUs  set Stock=Stock-@Stock");
                strSql.Append(" where SKU=@SKU");
                SqlParameter[] parameters =
                        {
                            new SqlParameter("@SKU", SqlDbType.NVarChar, 50),
                            new SqlParameter("@Stock", SqlDbType.Int, 4)
                        };
                parameters[0].Value = item.SKU;
                parameters[1].Value = item.Quantity;
                listComand.Add(new CommandInfo(strSql.ToString(), parameters));
            }
            return listComand;
        }

        #endregion

        #region GenerateOrderAction

        private List<CommandInfo> GenerateOrderAction(OrderInfo orderInfo)
        {
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();
            strSql.Append("insert into Shop_OrderAction(");
            strSql.Append("OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@OrderCode,@UserId,@Username,@ActionCode,@ActionDate,@Remark)");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Username", SqlDbType.NVarChar, 200),
                    new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ActionDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                };
            parameters[0].Value = orderInfo.OrderId;
            parameters[1].Value = orderInfo.OrderCode;
            parameters[4].Value = ((int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.CustomersCreateOrder); //orderInfo.ActionCode;
            parameters[5].Value = DateTime.Now;
            //拆单日志处理
            //if (orderInfo.ParentOrderId == -1)
            //{
            parameters[2].Value = orderInfo.BuyerID;
            parameters[3].Value = "客户";

            parameters[6].Value = "创建订单";
            //}
            //else
            //{
            //    parameters[2].Value = -1;
            //    parameters[3].Value = "系统";
            //    parameters[6].Value = "系统自动拆单";
            //}
            return new List<CommandInfo>
                {
                    new CommandInfo(strSql.ToString(), parameters,
                                    EffentNextType.ExcuteEffectRows)
                };
        }

        #endregion

        #region GenerateOrderItems

        private List<CommandInfo> GenerateOrderItems(OrderInfo orderInfo)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            foreach (Model.Shop.Order.OrderItems model in orderInfo.OrderItems)
            {
                System.Text.StringBuilder strSql = new System.Text.StringBuilder();
                strSql.Append("insert into Shop_OrderItems(");
                strSql.Append(
                    "OrderId,OrderCode,ProductId,ProductCode,SKU,Name,ThumbnailsUrl,Description,Quantity,ShipmentQuantity,CostPrice,SellPrice,AdjustedPrice,Attribute,Remark,Weight,Deduct,Points,ProductLineId,SupplierId,SupplierName,ActiveID,ActiveType,CostPrice2)");
                strSql.Append(" values (");
                strSql.Append(
                    "@OrderId,@OrderCode,@ProductId,@ProductCode,@SKU,@Name,@ThumbnailsUrl,@Description,@Quantity,@ShipmentQuantity,@CostPrice,@SellPrice,@AdjustedPrice,@Attribute,@Remark,@Weight,@Deduct,@Points,@ProductLineId,@SupplierId,@SupplierName,@ActiveID,@ActiveType,@CostPrice2)");
                strSql.Append(";select @@IDENTITY");

                #region SqlParameter
                SqlParameter[] parameters =
                    {
                        new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                        new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50),
                        new SqlParameter("@ProductId", SqlDbType.BigInt, 8),
                        new SqlParameter("@ProductCode", SqlDbType.NVarChar, 50),
                        new SqlParameter("@SKU", SqlDbType.NVarChar, 200),
                        new SqlParameter("@Name", SqlDbType.NVarChar, 200),
                        new SqlParameter("@ThumbnailsUrl", SqlDbType.NVarChar, 300),
                        new SqlParameter("@Description", SqlDbType.NVarChar, 500),
                        new SqlParameter("@Quantity", SqlDbType.Int, 4),
                        new SqlParameter("@ShipmentQuantity", SqlDbType.Int, 4),
                        new SqlParameter("@CostPrice", SqlDbType.Money, 8),
                        new SqlParameter("@SellPrice", SqlDbType.Money, 8),
                        new SqlParameter("@AdjustedPrice", SqlDbType.Money, 8),
                        new SqlParameter("@Attribute", SqlDbType.Text),
                        new SqlParameter("@Remark", SqlDbType.Text),
                        new SqlParameter("@Weight", SqlDbType.Int, 4),
                        new SqlParameter("@Deduct", SqlDbType.Money, 8),
                        new SqlParameter("@Points", SqlDbType.Int, 4),
                        new SqlParameter("@ProductLineId", SqlDbType.Int, 4),
                        new SqlParameter("@SupplierId", SqlDbType.Int, 4),
                        new SqlParameter("@SupplierName", SqlDbType.NVarChar, 100),
                        new SqlParameter("@ActiveID", SqlDbType.Int, 4),
                        new SqlParameter("@ActiveType", SqlDbType.Int, 4),
                        new SqlParameter("@CostPrice2", SqlDbType.Money, 8)
                    };
                parameters[0].Value = orderInfo.OrderId;
                parameters[1].Value = orderInfo.OrderCode;
                parameters[2].Value = model.ProductId;
                parameters[3].Value = model.ProductCode;
                parameters[4].Value = model.SKU;
                parameters[5].Value = model.Name;
                parameters[6].Value = model.ThumbnailsUrl;
                parameters[7].Value = model.Description;
                parameters[8].Value = model.Quantity;
                parameters[9].Value = model.ShipmentQuantity;
                parameters[10].Value = model.CostPrice;
                parameters[11].Value = model.SellPrice;
                parameters[12].Value = model.AdjustedPrice;
                parameters[13].Value = model.Attribute;
                parameters[14].Value = model.Remark;
                parameters[15].Value = model.Weight;
                parameters[16].Value = model.Deduct;
                parameters[17].Value = model.Points;
                parameters[18].Value = model.ProductLineId;
                parameters[19].Value = model.SupplierId;
                parameters[20].Value = model.SupplierName;
                parameters[21].Value = model.ActiveID;
                parameters[22].Value = model.ActiveType;
                parameters[23].Value = model.CostPrice2;
                #endregion

                list.Add(new CommandInfo(strSql.ToString(),
                                         parameters, EffentNextType.ExcuteEffectRows));
            }
            return list;
        }

        #endregion

        #region GenerateOrderInfo

        //增加子订单关联卡系统编号 2014/7/12
        private CommandInfo GenerateOrderInfo(OrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_Orders(");
            strSql.Append("OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews,CardSysId,CardNo,OrderOptType,OrderCostPrice2)");
            strSql.Append(" values (");
            strSql.Append("@OrderCode,@ParentOrderId,@CreatedDate,@UpdatedDate,@BuyerID,@BuyerName,@BuyerEmail,@BuyerCellPhone,@RegionId,@ShipRegion,@ShipAddress,@ShipZipCode,@ShipName,@ShipTelPhone,@ShipCellPhone,@ShipEmail,@ShippingModeId,@ShippingModeName,@RealShippingModeId,@RealShippingModeName,@ShipperId,@ShipperName,@ShipperAddress,@ShipperCellPhone,@Freight,@FreightAdjusted,@FreightActual,@Weight,@ShippingStatus,@ShipOrderNumber,@ExpressCompanyName,@ExpressCompanyAbb,@PaymentTypeId,@PaymentTypeName,@PaymentGateway,@PaymentStatus,@RefundStatus,@PayCurrencyCode,@PayCurrencyName,@PaymentFee,@PaymentFeeAdjusted,@GatewayOrderId,@OrderTotal,@OrderPoint,@OrderCostPrice,@OrderProfit,@OrderOtherCost,@OrderOptionPrice,@DiscountName,@DiscountAmount,@DiscountAdjusted,@DiscountValue,@DiscountValueType,@CouponCode,@CouponName,@CouponAmount,@CouponValue,@CouponValueType,@ActivityName,@ActivityFreeAmount,@ActivityStatus,@GroupBuyId,@GroupBuyPrice,@GroupBuyStatus,@Amount,@OrderType,@OrderStatus,@SellerID,@SellerName,@SellerEmail,@SellerCellPhone,@CommentStatus,@SupplierId,@SupplierName,@ReferID,@ReferURL,@OrderIP,@Remark,@ProductTotal,@HasChildren,@IsReviews,@CardSysId,@CardNo,@OrderOptType,@OrderCostPrice2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@ParentOrderId", SqlDbType.BigInt,8),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
                    new SqlParameter("@BuyerID", SqlDbType.Int,4),
                    new SqlParameter("@BuyerName", SqlDbType.NVarChar,100),
                    new SqlParameter("@BuyerEmail", SqlDbType.NVarChar,100),
                    new SqlParameter("@BuyerCellPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@RegionId", SqlDbType.Int,4),
                    new SqlParameter("@ShipRegion", SqlDbType.NVarChar,300),
                    new SqlParameter("@ShipAddress", SqlDbType.NVarChar,300),
                    new SqlParameter("@ShipZipCode", SqlDbType.NVarChar,20),
                    new SqlParameter("@ShipName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipTelPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipCellPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@ShipEmail", SqlDbType.NVarChar,100),
                    new SqlParameter("@ShippingModeId", SqlDbType.Int,4),
                    new SqlParameter("@ShippingModeName", SqlDbType.NVarChar,100),
                    new SqlParameter("@RealShippingModeId", SqlDbType.Int,4),
                    new SqlParameter("@RealShippingModeName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ShipperId", SqlDbType.Int,4),
                    new SqlParameter("@ShipperName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ShipperAddress", SqlDbType.NVarChar,300),
                    new SqlParameter("@ShipperCellPhone", SqlDbType.NVarChar,20),
                    new SqlParameter("@Freight", SqlDbType.Money,8),
                    new SqlParameter("@FreightAdjusted", SqlDbType.Money,8),
                    new SqlParameter("@FreightActual", SqlDbType.Money,8),
                    new SqlParameter("@Weight", SqlDbType.Int,4),
                    new SqlParameter("@ShippingStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@ShipOrderNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar,500),
                    new SqlParameter("@ExpressCompanyAbb", SqlDbType.NVarChar,500),
                    new SqlParameter("@PaymentTypeId", SqlDbType.Int,4),
                    new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,100),
                    new SqlParameter("@PaymentGateway", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@RefundStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@PayCurrencyCode", SqlDbType.NVarChar,20),
                    new SqlParameter("@PayCurrencyName", SqlDbType.NVarChar,20),
                    new SqlParameter("@PaymentFee", SqlDbType.Money,8),
                    new SqlParameter("@PaymentFeeAdjusted", SqlDbType.Money,8),
                    new SqlParameter("@GatewayOrderId", SqlDbType.NVarChar,100),
                    new SqlParameter("@OrderTotal", SqlDbType.Money,8),
                    new SqlParameter("@OrderPoint", SqlDbType.Int,4),
                    new SqlParameter("@OrderCostPrice", SqlDbType.Money,8),
                    new SqlParameter("@OrderProfit", SqlDbType.Money,8),
                    new SqlParameter("@OrderOtherCost", SqlDbType.Money,8),
                    new SqlParameter("@OrderOptionPrice", SqlDbType.Money,8),
                    new SqlParameter("@DiscountName", SqlDbType.NVarChar,200),
                    new SqlParameter("@DiscountAmount", SqlDbType.Money,8),
                    new SqlParameter("@DiscountAdjusted", SqlDbType.Money,8),
                    new SqlParameter("@DiscountValue", SqlDbType.Money,8),
                    new SqlParameter("@DiscountValueType", SqlDbType.SmallInt,2),
                    new SqlParameter("@CouponCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@CouponName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CouponAmount", SqlDbType.Money,8),
                    new SqlParameter("@CouponValue", SqlDbType.Money,8),
                    new SqlParameter("@CouponValueType", SqlDbType.SmallInt,2),
                    new SqlParameter("@ActivityName", SqlDbType.NVarChar,200),
                    new SqlParameter("@ActivityFreeAmount", SqlDbType.Money,8),
                    new SqlParameter("@ActivityStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@GroupBuyId", SqlDbType.Int,4),
                    new SqlParameter("@GroupBuyPrice", SqlDbType.Money,8),
                    new SqlParameter("@GroupBuyStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@Amount", SqlDbType.Money,8),
                    new SqlParameter("@OrderType", SqlDbType.SmallInt,2),
                    new SqlParameter("@OrderStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@SellerID", SqlDbType.Int,4),
                    new SqlParameter("@SellerName", SqlDbType.NVarChar,100),
                    new SqlParameter("@SellerEmail", SqlDbType.NVarChar,100),
                    new SqlParameter("@SellerCellPhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@CommentStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@SupplierId", SqlDbType.Int,4),
                    new SqlParameter("@SupplierName", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReferID", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReferURL", SqlDbType.NVarChar,200),
                    new SqlParameter("@OrderIP", SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,2000),
                    new SqlParameter("@ProductTotal", SqlDbType.Money,8),
                    new SqlParameter("@HasChildren", SqlDbType.Bit,1),
                    new SqlParameter("@IsReviews", SqlDbType.Bit,1),
                    new SqlParameter("@CardSysId",SqlDbType.Int,4),
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@OrderOptType",SqlDbType.Int,4),
                    new SqlParameter("@OrderCostPrice2", SqlDbType.Money,8)};
            parameters[0].Value = model.OrderCode;
            parameters[1].Value = model.ParentOrderId;
            parameters[2].Value = model.CreatedDate;
            parameters[3].Value = model.UpdatedDate;
            parameters[4].Value = model.BuyerID;
            parameters[5].Value = model.BuyerName;
            parameters[6].Value = model.BuyerEmail;
            parameters[7].Value = model.BuyerCellPhone;
            parameters[8].Value = model.RegionId;
            parameters[9].Value = model.ShipRegion;
            parameters[10].Value = model.ShipAddress;
            parameters[11].Value = model.ShipZipCode;
            parameters[12].Value = model.ShipName;
            parameters[13].Value = model.ShipTelPhone;
            parameters[14].Value = model.ShipCellPhone;
            parameters[15].Value = model.ShipEmail;
            parameters[16].Value = model.ShippingModeId;
            parameters[17].Value = model.ShippingModeName;
            parameters[18].Value = model.RealShippingModeId;
            parameters[19].Value = model.RealShippingModeName;
            parameters[20].Value = model.ShipperId;
            parameters[21].Value = model.ShipperName;
            parameters[22].Value = model.ShipperAddress;
            parameters[23].Value = model.ShipperCellPhone;
            parameters[24].Value = model.Freight;
            parameters[25].Value = model.FreightAdjusted;
            parameters[26].Value = model.FreightActual;
            parameters[27].Value = model.Weight;
            parameters[28].Value = model.ShippingStatus;
            parameters[29].Value = model.ShipOrderNumber;
            parameters[30].Value = model.ExpressCompanyName;
            parameters[31].Value = model.ExpressCompanyAbb;
            parameters[32].Value = model.PaymentTypeId;
            parameters[33].Value = model.PaymentTypeName;
            parameters[34].Value = model.PaymentGateway;
            parameters[35].Value = model.PaymentStatus;
            parameters[36].Value = model.RefundStatus;
            parameters[37].Value = model.PayCurrencyCode;
            parameters[38].Value = model.PayCurrencyName;
            parameters[39].Value = model.PaymentFee;
            parameters[40].Value = model.PaymentFeeAdjusted;
            parameters[41].Value = model.GatewayOrderId;
            parameters[42].Value = model.OrderTotal;
            parameters[43].Value = model.OrderPoint;
            parameters[44].Value = model.OrderCostPrice;
            parameters[45].Value = model.OrderProfit;
            parameters[46].Value = model.OrderOtherCost;
            parameters[47].Value = model.OrderOptionPrice;
            parameters[48].Value = model.DiscountName;
            parameters[49].Value = model.DiscountAmount;
            parameters[50].Value = model.DiscountAdjusted;
            parameters[51].Value = model.DiscountValue;
            parameters[52].Value = model.DiscountValueType;
            parameters[53].Value = model.CouponCode;
            parameters[54].Value = model.CouponName;
            parameters[55].Value = model.CouponAmount;
            parameters[56].Value = model.CouponValue;
            parameters[57].Value = model.CouponValueType;
            parameters[58].Value = model.ActivityName;
            parameters[59].Value = model.ActivityFreeAmount;
            parameters[60].Value = model.ActivityStatus;
            parameters[61].Value = model.GroupBuyId;
            parameters[62].Value = model.GroupBuyPrice;
            parameters[63].Value = model.GroupBuyStatus;
            parameters[64].Value = model.Amount;
            parameters[65].Value = model.OrderType;
            parameters[66].Value = model.OrderStatus;
            parameters[67].Value = model.SellerID;
            parameters[68].Value = model.SellerName;
            parameters[69].Value = model.SellerEmail;
            parameters[70].Value = model.SellerCellPhone;
            parameters[71].Value = model.CommentStatus;
            parameters[72].Value = model.SupplierId;
            parameters[73].Value = model.SupplierName;
            parameters[74].Value = model.ReferID;
            parameters[75].Value = model.ReferURL;
            parameters[76].Value = model.OrderIP;
            parameters[77].Value = model.Remark;
            parameters[78].Value = model.ProductTotal;
            parameters[79].Value = model.HasChildren;
            parameters[80].Value = model.IsReviews;
            parameters[81].Value = model.CardSysId;
            parameters[82].Value = model.CardNo;
            parameters[83].Value = model.OrderOptType;
            parameters[84].Value = model.OrderCostPrice2;
            return new CommandInfo(strSql.ToString(), parameters);
        }

        #endregion

        #endregion

        #region 取消订单

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool CancelOrder(OrderInfo orderInfo, Accounts.Bus.User currentUser = null)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            //返回SKU库存
            if (orderInfo.OrderItems != null && orderInfo.OrderItems.Count > 0)
            {
                foreach (Model.Shop.Order.OrderItems item in orderInfo.OrderItems)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update Shop_SKUs  set Stock=Stock+@Stock");
                    strSql.Append(" where SKU=@SKU");
                    SqlParameter[] parameters =
                        {
                            new SqlParameter("@SKU", SqlDbType.NVarChar, 50),
                            new SqlParameter("@Stock", SqlDbType.Int, 4)
                        };
                    parameters[0].Value = item.SKU;
                    parameters[1].Value = item.Quantity;
                    sqllist.Add(new CommandInfo(strSql.ToString(), parameters));
                }
            }

            //返回团购库存
            //if (orderInfo.GroupBuyId > 0 && orderInfo.GroupBuyStatus == 1)
            if(orderInfo.GroupBuyStatus == 1)
            {
                foreach (Model.Shop.Order.OrderItems item in orderInfo.OrderItems)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update Shop_GroupBuy set ");
                    strSql.Append("BuyCount=BuyCount-@BuyCount");
                    strSql.Append(" where productid =@prdouctid and BuyCount>=@BuyCount");
                    SqlParameter[] parameters = {
                    new SqlParameter("@BuyCount", SqlDbType.Int,4),
                        new SqlParameter("@prdouctid", SqlDbType.Int,4)
                                        };

                    parameters[0].Value = item.Quantity;
                    parameters[1].Value = item.ProductId;
                    sqllist.Add(new CommandInfo(strSql.ToString(), parameters));
                }
            }

            //返回商品销售数
            //if (orderInfo.OrderStatus == 已支付)
            //{

            //}

            //更新订单状态
            //DONE: 更新子订单的状态为 已取消
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("UPDATE  Shop_Orders SET OrderStatus=-1, UpdatedDate=@UpdatedDate");
            strSql2.Append(" where OrderId=@OrderId OR ParentOrderId=@OrderId");
            SqlParameter[] parameters2 =
                {
                    new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                    new SqlParameter("@UpdatedDate", SqlDbType.DateTime)
                };
            parameters2[0].Value = orderInfo.OrderId;
            parameters2[1].Value = DateTime.Now;
            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2, EffentNextType.ExcuteEffectRows);
            sqllist.Add(cmd);

            
            //添加操作记录
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into Shop_OrderAction(");
            strSql3.Append("OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark)");
            strSql3.Append(" values (");
            strSql3.Append("@OrderId,@OrderCode,@UserId,@Username,@ActionCode,@ActionDate,@Remark)");
            SqlParameter[] parameters3 =
                {
                    new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Username", SqlDbType.NVarChar, 200),
                    new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ActionDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                };
            parameters3[0].Value = orderInfo.OrderId;
            parameters3[1].Value = orderInfo.OrderCode;
            parameters3[2].Value = currentUser != null ? currentUser.UserID : orderInfo.BuyerID;
            parameters3[3].Value = currentUser != null ? currentUser.UserName : orderInfo.BuyerName;
            parameters3[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemCancel; //orderInfo.ActionCode;
            parameters3[5].Value = DateTime.Now;

            //DONE: 要区分 user/admin 取消
            if (currentUser != null)  //TODO: 如果出现另外一种可以操作订单的角色 那么此处就要多加一层判断
            {
                switch (currentUser.UserType)
                {
                    case "AA":
                        parameters3[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemCancel;
                        break;
                    case "SP":
                        parameters3[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SellerCancel;
                        break;
                    case "UU":
                        parameters3[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.CustomersCancel;
                        break;
                    case "AG":
                        parameters3[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.AgentCancel;
                        break;
                }

            }
            parameters3[6].Value = "取消订单";
            cmd = new CommandInfo(strSql3.ToString(), parameters3, EffentNextType.ExcuteEffectRows);
            sqllist.Add(cmd);

            //如果该订单有使用优惠券，那么取消订单需要返还该优惠券。&& orderInfo.CouponCode!=""
            if (orderInfo.BuyerID > 0 && orderInfo.CouponCode != "")
            {
                StringBuilder strSql4 = new StringBuilder();
                strSql4.Append("UPDATE Shop_CouponInfo SET Status=1 ");
                strSql4.Append(" WHERE CouponCode = @CouponCode");
                SqlParameter[] parameters ={
                        new SqlParameter("@CouponCode", SqlDbType.NVarChar,200)
                                           };
                parameters[0].Value = orderInfo.CouponCode;
                sqllist.Add(new CommandInfo(strSql4.ToString(), parameters));

            }

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            return rowsAffected > 0;
        }

        #endregion

        #region 支付订单

        /// <summary>
        /// 支付订单
        /// </summary>
        public bool PayForOrder(OrderInfo orderInfo,string notifyid, Accounts.Bus.User currentUser = null)
        {
            List<CommandInfo> listCommand = new List<CommandInfo>();
            DateTime updatedDate = DateTime.Now;

            #region 1.更新订单状态为 进行中 - 已支付

            //DONE: 1.更新订单状态为 进行中 - 已支付
            //DONE: 更新子订单的状态为 已支付
            StringBuilder sqlOrders = new StringBuilder();
            sqlOrders.Append("UPDATE  Shop_Orders SET OrderStatus=1, PaymentStatus=2, UpdatedDate=@UpdatedDate,PaymentNumber=@PaymentNumber");
            sqlOrders.Append(" WHERE OrderId=@OrderId OR ParentOrderId=@OrderId");
            SqlParameter[] paramOrders =
                {
                    new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                    new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
                    new SqlParameter("@PaymentNumber", SqlDbType.NVarChar,100)
                };
            paramOrders[0].Value = orderInfo.OrderId;
            paramOrders[1].Value = updatedDate;
            paramOrders[2].Value = notifyid;
            listCommand.Add(new CommandInfo(sqlOrders.ToString(), paramOrders, EffentNextType.ExcuteEffectRows));
            ErrorLogTxt.GetInstance("sql日志").Write("更新订单状态为 进行中 - 已支付:" + sqlOrders.ToString() + "参数：@OrderId=" + orderInfo.OrderId + ",@UpdatedDate=" + updatedDate + ",@PaymentNumber="+notifyid);
            #endregion

            #region 2.新增订单操作记录

            //DONE: 2.新增订单操作记录
            StringBuilder sqlOrderAction = new StringBuilder();
            sqlOrderAction.Append("insert into Shop_OrderAction(");
            sqlOrderAction.Append("OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark)");
            sqlOrderAction.Append(" values (");
            sqlOrderAction.Append("@OrderId,@OrderCode,@UserId,@Username,@ActionCode,@ActionDate,@Remark)");
            SqlParameter[] paramOrderAction =
                {
                    new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Username", SqlDbType.NVarChar, 200),
                    new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ActionDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                };
            paramOrderAction[0].Value = orderInfo.OrderId;
            paramOrderAction[1].Value = orderInfo.OrderCode;
            paramOrderAction[2].Value = currentUser != null ? currentUser.UserID : orderInfo.BuyerID;
            paramOrderAction[3].Value = "系统";
            paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemPay;
            //DONE: 要区分 user/admin 支付
            if (currentUser != null)
            {
                switch (currentUser.UserType)
                {
                    case "AA":
                        paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemPay;
                        break;
                    case "UU":
                        paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.CustomersPay;
                        break;
                    default:
                        paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemPay;
                        break;
                }

            }
            paramOrderAction[5].Value = updatedDate;
            paramOrderAction[6].Value = "支付订单"; //TODO: 需要记录实际操作人
            listCommand.Add(new CommandInfo(sqlOrderAction.ToString(), paramOrderAction, EffentNextType.ExcuteEffectRows));
            ErrorLogTxt.GetInstance("sql日志").Write(@"新增订单操作记录:" + sqlOrderAction.ToString() + "参数：@OrderId="+orderInfo.OrderId+
                ",@OrderCode=" + orderInfo.OrderCode + ",@UserId=" + paramOrderAction[2].Value + ",@Username=" + paramOrderAction[3].Value + ",@ActionCode="+
                paramOrderAction[4].Value + ",@ActionDate" + paramOrderAction[5].Value + ",@Remark=" + paramOrderAction[4].Value);
            #endregion

            //DONE: 3.增加用户扩展表 积分 禁止执行 *)此功能移动到[完成订单]时

            //DONE: 4.新增积分记录 禁止执行 *)此功能移动到[完成订单]时

            #region 5.增加商品销售数
            //DONE: 5.增加商品销售数
            if (orderInfo.OrderItems != null && orderInfo.OrderItems.Count > 0)
            {
                foreach (Model.Shop.Order.OrderItems item in orderInfo.OrderItems)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update Shop_Products SET SaleCounts=SaleCounts+@Stock");
                    strSql.Append(" where ProductId=@ProductId");
                    SqlParameter[] parameters =
                        {
                            new SqlParameter("@ProductId", SqlDbType.BigInt),
                            new SqlParameter("@Stock", SqlDbType.Int, 4)
                        };
                    parameters[0].Value = item.ProductId;
                    parameters[1].Value = item.Quantity;
                    listCommand.Add(new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows));
                    ErrorLogTxt.GetInstance("sql日志").Write("增加商品销售数:" + strSql.ToString() + "参数：@ProductId=" + parameters[0].Value + ",@Stock" + parameters[1].Value);
                }
            }
            #endregion

            #region 子单操作
            if (orderInfo.HasChildren && orderInfo.SubOrders.Count > 0)
            {
                foreach (OrderInfo subOrder in orderInfo.SubOrders)
                {
                    #region 子单日志
                    paramOrderAction = new SqlParameter[]
                    {
                        new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                        new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50),
                        new SqlParameter("@UserId", SqlDbType.Int, 4),
                        new SqlParameter("@Username", SqlDbType.NVarChar, 200),
                        new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100),
                        new SqlParameter("@ActionDate", SqlDbType.DateTime),
                        new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                    };
                    paramOrderAction[0].Value = subOrder.OrderId;
                    paramOrderAction[1].Value = subOrder.OrderCode;
                    paramOrderAction[2].Value = currentUser != null ? currentUser.UserID : orderInfo.BuyerID;
                    paramOrderAction[3].Value = "系统";
                    paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemPay;
                    //DONE: 要区分 user/admin 支付
                    if (currentUser != null)
                    {
                        switch (currentUser.UserType)
                        {
                            case "AA":
                                paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemPay;
                                break;
                            case "UU":
                                paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.CustomersPay;
                                break;
                            default:
                                paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemPay;
                                break;
                        }

                    }
                    paramOrderAction[5].Value = updatedDate;
                    paramOrderAction[6].Value = "支付订单"; //TODO: 需要记录实际操作人
                    listCommand.Add(new CommandInfo(sqlOrderAction.ToString(), paramOrderAction,
                        EffentNextType.ExcuteEffectRows));
                    #endregion

                    #region 子订单卖家操作
                    PayToSupplier(listCommand, updatedDate, subOrder);
                    #endregion
                }
            }
            #endregion
            else
            {
                #region 主订单卖家操作
                PayToSupplier(listCommand, updatedDate, orderInfo);
                #endregion
            }

            #region
            //ErrorLogTxt.GetInstance("sql日志").Write("sql执行结果:" + DbHelperSQL.ExecuteSqlTran(listCommand));
            //return DbHelperSQL.ExecuteSqlTran(listCommand) > 0;
            #endregion

            int ret = DbHelperSQL.ExecuteSqlTran(listCommand);
            ErrorLogTxt.GetInstance("sql日志").Write("sql执行结果:" + ret);
            return ret > 0;
            
        }

        /// <summary>
        /// 返回一条insert语句
        /// </summary>
        /// <returns></returns>
        public string InsertStr()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("insert into Shop_PaymentNumber(OrderId,OrderCode,ParentOrderId,SwiftNumber) values(");
            stb.Append("{0},'{1}',{2},{3})");
            return stb.ToString();
        }

        #region 卖家操作
        private static void PayToSupplier(List<CommandInfo> listCommand, DateTime updatedDate, OrderInfo orderInfo)
        {
            StringBuilder sqlOrders;
            SqlParameter[] paramOrders;

            if (orderInfo.SellerID.HasValue &&
                orderInfo.SellerID.Value > 0 &&
                orderInfo.OrderCostPrice.HasValue &&
                orderInfo.OrderCostPrice.Value > 0)
            {
                //#region 增加卖家余额
                //Edit by wangzhongyu 20140804 暂时不在商家钱包中结算
                //sqlOrders = new StringBuilder();
                //sqlOrders.Append("UPDATE  Accounts_UsersExp SET Balance=Balance+@Balance");
                //sqlOrders.Append(" WHERE UserID=@UserID");
                //paramOrders = new SqlParameter[]
                //        {
                //            new SqlParameter("@Balance", SqlDbType.Money, 8),
                //            new SqlParameter("@UserID", SqlDbType.Int, 4)
                //        };
                //paramOrders[0].Value = orderInfo.OrderCostPrice.Value;
                //paramOrders[1].Value = orderInfo.SellerID.Value;
                //listCommand.Add(new CommandInfo(sqlOrders.ToString(), paramOrders,
                //    EffentNextType.ExcuteEffectRows));
                //ErrorLogTxt.GetInstance("sql日志").Write("增加卖家余额:" + sqlOrders.ToString() + "参数：@Balance=" + paramOrders[0].Value + ",@UserID=" + paramOrders[1].Value);
                //#endregion

                #region 增加商家余额 暂未使用

                /**
                         * 商家余额保存到  Accounts_UsersExp 表, 仅商家所有者可以提现.
                         * 不保存到 Shop_Suppliers 表 原因:
                         * 1. 交易记录 UserId 共通模块 无商家Id
                         * 2. 提现流程 UserId 共通模块 无商家Id
                         * 3. Shop v1.9.5 基础上执行最小改动
                         */

                #endregion

                //#region 卖家交易(收入)记录 Edit by wangzhongyu 20140804 暂时不在商家钱包中结算

                //sqlOrders = new StringBuilder();
                //sqlOrders.Append("insert into Pay_BalanceDetails(");
                //sqlOrders.Append("UserId,TradeDate,TradeType,Income,Balance,Remark)");
                //sqlOrders.Append(" values (");
                ////TODO:Sql2005语法兼容性Check BEN ADD 20131202
                //sqlOrders.Append(
                //    "@UserId,@TradeDate,@TradeType,@Income,(SELECT Balance FROM Accounts_UsersExp WITH (NOLOCK) WHERE UserID = @UserId),@Remark)");
                //paramOrders = new SqlParameter[]
                //        {
                //            new SqlParameter("@UserId", SqlDbType.Int, 4),
                //            new SqlParameter("@TradeDate", SqlDbType.DateTime),
                //            new SqlParameter("@TradeType", SqlDbType.Int, 4),
                //            new SqlParameter("@Income", SqlDbType.Money, 8),
                //            new SqlParameter("@Remark", SqlDbType.NVarChar, 2000)
                //        };
                //paramOrders[0].Value = orderInfo.SellerID.Value;
                //paramOrders[1].Value = updatedDate;
                //paramOrders[2].Value = 1;
                //paramOrders[3].Value = orderInfo.OrderCostPrice.Value; //收入
                //paramOrders[4].Value = string.Format("交易收入 订单号[{0}]", orderInfo.OrderCode); //备注
                //listCommand.Add(new CommandInfo(sqlOrders.ToString(), paramOrders,
                //    EffentNextType.ExcuteEffectRows));
                //ErrorLogTxt.GetInstance("sql日志").Write("卖家交易(收入)记录:" + sqlOrders.ToString() + "参数：@UserId=" + paramOrders[0].Value + ",@TradeDate=" + paramOrders[1].Value + ",@Income=" + paramOrders[3].Value + ",@Remark=" + paramOrders[4].Value);
                //#endregion
            }
        }
        #endregion

        #endregion
        
        #region 完成订单

        public bool CompleteOrder(OrderInfo orderInfo, Accounts.Bus.User currentUser = null)
        {

            List<CommandInfo> listCommand = new List<CommandInfo>();
            DateTime updatedDate = DateTime.Now;

            #region 1.更新订单状态为 进行中 - 已支付

            //DONE: 1.更新订单状态为  已完成 - 已支付 - 已确认收货
            //DONE: 更新子订单的状态为 已完成 - 已支付 - 已确认收货
            StringBuilder sqlOrders = new StringBuilder();
            sqlOrders.Append("UPDATE  Shop_Orders SET OrderStatus=2,PaymentStatus=2,ShippingStatus=3, UpdatedDate=@UpdatedDate");
            sqlOrders.Append(" WHERE OrderId=@OrderId OR ParentOrderId=@OrderId");
            SqlParameter[] paramOrders =
                {
                    new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                    new SqlParameter("@UpdatedDate", SqlDbType.DateTime)
                };
            paramOrders[0].Value = orderInfo.OrderId;
            paramOrders[1].Value = updatedDate;
            listCommand.Add(new CommandInfo(sqlOrders.ToString(), paramOrders, EffentNextType.ExcuteEffectRows));

            #endregion

            #region 2.新增订单操作记录

            //DONE: 2.新增订单操作记录
            StringBuilder sqlOrderAction = new StringBuilder();
            sqlOrderAction.Append("insert into Shop_OrderAction(");
            sqlOrderAction.Append("OrderId,OrderCode,UserId,Username,ActionCode,ActionDate,Remark)");
            sqlOrderAction.Append(" values (");
            sqlOrderAction.Append("@OrderId,@OrderCode,@UserId,@Username,@ActionCode,@ActionDate,@Remark)");
            SqlParameter[] paramOrderAction =
                {
                    new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Username", SqlDbType.NVarChar, 200),
                    new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ActionDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                };
            paramOrderAction[0].Value = orderInfo.OrderId;
            paramOrderAction[1].Value = orderInfo.OrderCode;
            paramOrderAction[2].Value = currentUser != null ? currentUser.UserID : orderInfo.BuyerID;
            paramOrderAction[3].Value = currentUser != null ? currentUser.NickName : "系统";
            paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemComplete;
            //DONE: 要区分 user/admin 支付
            if (currentUser != null)
            {
                switch (currentUser.UserType)
                {
                    case "AA":
                        paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemComplete;
                        break;
                    case "AG":
                        paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.AgentComplete;
                        break;
                    case "SP":
                        paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SellerComplete;
                        break;
                    case "UU":
                        paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.CustomersComplete;
                        break;
                    default:
                        paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemComplete;
                        break;
                }
            }
            paramOrderAction[5].Value = updatedDate;
            paramOrderAction[6].Value = "完成订单"; //TODO: 需要记录实际操作人
            listCommand.Add(new CommandInfo(sqlOrderAction.ToString(), paramOrderAction, EffentNextType.ExcuteEffectRows));

            #endregion

            #region 增加积分
            if (orderInfo.OrderPoint > 0)
            {
                #region 3.增加用户扩展表 积分
                //DONE: 3.增加用户扩展表 积分
                //TODO: 如果要[实现购买某件商品获得与商品价格一定比率的积分，比率可以通过配置表配置]功能
                //TODO: 请将[3][4]提取到BLL层执行, 并作积分比率计算
                StringBuilder sqlOrderPoint = new StringBuilder();
                sqlOrderPoint.Append("update Accounts_UsersExp SET ");
                sqlOrderPoint.Append(" Points=Points+@Points ");
                sqlOrderPoint.Append(" WHERE UserID=@UserID ");
                SqlParameter[] paramOrderPoint =
            {
                new SqlParameter("@Points", SqlDbType.Int, 4),
                new SqlParameter("@UserID", SqlDbType.Int, 4)
            };
                paramOrderPoint[0].Value = orderInfo.OrderPoint;
                paramOrderPoint[1].Value = orderInfo.BuyerID;
                listCommand.Add(new CommandInfo(sqlOrderPoint.ToString(), paramOrderPoint));
                #endregion

                #region 4.新增积分记录
                //DONE: 4.新增积分记录

                StringBuilder sqlPointDetail = new StringBuilder();
                sqlPointDetail.Append("insert into Accounts_PointsDetail(");
                sqlPointDetail.Append("RuleID,UserID,Score,ExtData,CurrentPoints,Description,CreatedDate,Type)");
                sqlPointDetail.Append(" values (");
                sqlPointDetail.Append("@RuleID,@UserID,@Score,@ExtData,0,@Description,@CreatedDate,@Type)");
                SqlParameter[] paramPointDetail =
            {
                new SqlParameter("@RuleID", SqlDbType.Int, 4),
                new SqlParameter("@UserID", SqlDbType.Int, 4),
                new SqlParameter("@Score", SqlDbType.Int, 4),
                new SqlParameter("@ExtData", SqlDbType.NVarChar),
                new SqlParameter("@Description", SqlDbType.NVarChar),
                new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                new SqlParameter("@Type", SqlDbType.Int, 4)
            };
            paramPointDetail[0].Value = (int)Maticsoft.Model.Members.Enum.PointRule.Order;
            paramPointDetail[1].Value = orderInfo.BuyerID;
            paramPointDetail[2].Value = orderInfo.OrderPoint;
            paramPointDetail[3].Value = string.Empty;
            paramPointDetail[4].Value = string.Format("[订单完成] 订单号:{0}", orderInfo.OrderId);
            paramPointDetail[5].Value = updatedDate;
            paramPointDetail[6].Value = 0;
            listCommand.Add(new CommandInfo(sqlPointDetail.ToString(), paramPointDetail));

                #endregion
            } 
            #endregion

            #region 5.增加商品销售数 未启用 已在支付流程调用 禁止二次调用
            //DONE: 5.增加商品销售数
            //if (orderInfo.OrderItems != null && orderInfo.OrderItems.Count > 0)
            //{
            //    foreach (Model.Shop.Order.OrderItems item in orderInfo.OrderItems)
            //    {
            //        StringBuilder strSql = new StringBuilder();
            //        strSql.Append("update Shop_Products SET SaleCounts=SaleCounts+@Stock");
            //        strSql.Append(" where ProductId=@ProductId");
            //        SqlParameter[] parameters =
            //            {
            //                new SqlParameter("@ProductId", SqlDbType.BigInt),
            //                new SqlParameter("@Stock", SqlDbType.Int, 4)
            //            };
            //        parameters[0].Value = item.ProductId;
            //        parameters[1].Value = item.Quantity;
            //        listCommand.Add(new CommandInfo(strSql.ToString(), parameters));
            //    }
            //}
            #endregion

            #region 子单操作
            if (orderInfo.HasChildren && orderInfo.SubOrders.Count > 0)
            {
                foreach (OrderInfo subOrder in orderInfo.SubOrders)
                {
                    #region 子单日志
                    paramOrderAction = new SqlParameter[]
                    {
                        new SqlParameter("@OrderId", SqlDbType.BigInt, 8),
                        new SqlParameter("@OrderCode", SqlDbType.NVarChar, 50),
                        new SqlParameter("@UserId", SqlDbType.Int, 4),
                        new SqlParameter("@Username", SqlDbType.NVarChar, 200),
                        new SqlParameter("@ActionCode", SqlDbType.NVarChar, 100),
                        new SqlParameter("@ActionDate", SqlDbType.DateTime),
                        new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                    };
                    paramOrderAction[0].Value = subOrder.OrderId;
                    paramOrderAction[1].Value = subOrder.OrderCode;
                    paramOrderAction[2].Value = currentUser != null ? currentUser.UserID : orderInfo.BuyerID;
                    paramOrderAction[3].Value = currentUser != null ? currentUser.NickName : "系统";
                    paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemComplete;
                    //DONE: 要区分 user/admin 支付
                    if (currentUser != null)
                    {
                        switch (currentUser.UserType)
                        {
                            case "AA":
                                paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemComplete;
                                break;
                            case "AG":
                                paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.AgentComplete;
                                break;
                            case "SP":
                                paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SellerComplete;
                                break;
                            case "UU":
                                paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.CustomersComplete;
                                break;
                            default:
                                paramOrderAction[4].Value = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemComplete;
                                break;
                        }
                    }
                    paramOrderAction[5].Value = updatedDate;
                    paramOrderAction[6].Value = "完成订单"; //TODO: 需要记录实际操作人
                    listCommand.Add(new CommandInfo(sqlOrderAction.ToString(), paramOrderAction,
                        EffentNextType.ExcuteEffectRows));
                    #endregion

                    #region 子订单卖家操作 未启用 已在支付流程调用 禁止二次调用
                    //PayToSupplier(listCommand, updatedDate, subOrder);
                    #endregion
                }
            }
            #endregion
            else
            {
                #region 主订单卖家操作 未启用 已在支付流程调用 禁止二次调用
                //PayToSupplier(listCommand, updatedDate, orderInfo);
                #endregion
            }

            return DbHelperSQL.ExecuteSqlTran(listCommand) > 0;
        }
        #endregion

        #region 订单统计
        public DataSet Stat4OrderStatus(int orderStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
                @"
SELECT COUNT(*) ToalQuantity
, SUM(O.Amount) ToalPrice
FROM    Shop_Orders O
WHERE   O.OrderStatus = {0} ", orderStatus);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet Stat4OrderStatus(int orderStatus, DateTime startDate, DateTime endDate, int? supplierId = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select B.ToalPrice,B.ToalQuantity
                                from (select COUNT(*) ToalQuantity,SUM(O.Amount) ToalPrice
                                from Shop_Orders O
                                where O.OrderStatus ={0} and O.CreatedDate BETWEEN '{1}' AND '{2}' 
                                ", orderStatus, startDate, endDate);
            if (supplierId.HasValue && supplierId.Value > 0)
            {
                strSql.AppendFormat("  AND O.SupplierId = {0}", supplierId);
            }
            strSql.AppendFormat(@") B ");
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet StatSales(StatisticMode mode, DateTime startDate, DateTime endDate, int? supplierId = null)
        {
            int subLength = 8;
            string method;
            switch (mode)
            {
                case StatisticMode.Year:
                    subLength = 4;
                    method = "GET_GeneratedYear";
                    break;
                case StatisticMode.Month:
                    subLength = 6;
                    method = "GET_GeneratedMonth";
                    break;
                case StatisticMode.Day:
                    subLength = 8;
                    method = "GET_GeneratedDay";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
            @"
--销量/业绩走势图
SELECT  A.GeneratedDate AS GeneratedDate
      , CASE WHEN B.ToalQuantity IS NULL THEN 0
             ELSE B.ToalQuantity
        END AS ToalQuantity
      , CASE WHEN B.ToalPrice IS NULL THEN 0.00
             ELSE B.ToalPrice
        END AS ToalPrice
FROM    ( SELECT    *
          FROM      {0}(@StartDate, @EndDate)
        ) A
        LEFT JOIN ( SELECT  CONVERT(varchar({1}) , O.CreatedDate, 112 ) GeneratedDate
                          , SUM(I.Quantity) ToalQuantity
                          , SUM(I.SellPrice) ToalPrice
                    FROM    Shop_OrderItems I
                          , Shop_Orders O
                    WHERE   I.OrderId = O.OrderId ", method, subLength);
            if (supplierId.HasValue && supplierId.Value > 0)
            {
                strSql.AppendFormat("  AND O.SupplierId = {0}", supplierId);
            }
            strSql.AppendFormat(@" 
                            AND O.OrderStatus = 2
                            AND O.OrderType = 1
                            AND O.CreatedDate BETWEEN @StartDate AND @EndDate 
                    GROUP BY CONVERT(varchar({0}) , O.CreatedDate, 112 )
                  ) B 
ON CONVERT(varchar({0}) , A.GeneratedDate, 112 ) = CONVERT(varchar({0}) , B.GeneratedDate, 112 ) 
", subLength);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion

        #region 商品销量排行统计
        public DataSet ProductSales(StatisticMode mode, DateTime startDate, DateTime endDate,int supplierId)
        {
            int subLength = 8;
            string method;
            switch (mode)
            {
                case StatisticMode.Year:
                    subLength = 4;
                    method = "GET_GeneratedYear";
                    break;
                case StatisticMode.Month:
                    subLength = 6;
                    method = "GET_GeneratedMonth";
                    break;
                case StatisticMode.Day:
                    subLength = 8;
                    method = "GET_GeneratedDay";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
            @"
--商品销量排行统计走势图
SELECT  A.GeneratedDate AS GeneratedDate
      , CASE WHEN B.Product IS NULL THEN 0
             ELSE B.Product
        END AS Product
      , CASE WHEN B.ToalQuantity IS NULL THEN 0
             ELSE B.ToalQuantity
        END AS ToalQuantity
FROM    ( SELECT    *
          FROM      {0}(@StartDate, @EndDate)
        ) A
        LEFT JOIN ( SELECT  CONVERT(varchar({1}) , O.CreatedDate, 112 ) GeneratedDate
                          , SUM(I.ProductId) Product
                          , SUM(I.Quantity) ToalQuantity
                    FROM    Shop_OrderItems I
                          , Shop_Orders O
                    WHERE   I.OrderId = O.OrderId ", method, subLength);
          
                if (supplierId > 0)
                {
                    strSql.AppendFormat(" and I.SupplierId={0}  ", supplierId);
                }
               
            strSql.AppendFormat(@" 
                            AND O.CreatedDate BETWEEN @StartDate AND @EndDate 
                    GROUP BY CONVERT(varchar({0}) , O.CreatedDate, 112 )
                  ) B 
ON CONVERT(varchar({0}) , A.GeneratedDate, 112 ) = CONVERT(varchar({0}) , B.GeneratedDate, 112 ) 
 order by ToalQuantity desc
", subLength);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        #endregion

        #region 每种商品销量统计
        public DataSet ProductSaleInfo(StatisticMode mode, DateTime startDate, DateTime endDate)
        {
            int subLength = 8;
            string method;
            switch (mode)
            {
                case StatisticMode.Year:
                    subLength = 4;
                    method = "GET_GeneratedYear";
                    break;
                case StatisticMode.Month:
                    subLength = 6;
                    method = "GET_GeneratedMonth";
                    break;
                case StatisticMode.Day:
                    subLength = 8;
                    method = "GET_GeneratedDay";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
            @"
--商品销量排行统计列表
SELECT  CASE WHEN B.ProductID IS NULL THEN 0
             ELSE B.ProductID
        END AS Product
      , CASE WHEN B.ToalQuantity IS NULL THEN 0
             ELSE B.ToalQuantity
        END AS ToalQuantity
        ,P.ProductName
FROM    ( SELECT            I.ProductId ProductID
                          , SUM(I.Quantity) ToalQuantity
                    FROM    Shop_OrderItems I
                          , Shop_Orders O
                    WHERE   I.OrderId = O.OrderId AND O.ordertype=1 ", method, subLength);

            strSql.AppendFormat(@" 
                            AND O.CreatedDate BETWEEN @StartDate AND @EndDate 
                    GROUP BY I.ProductId
                  ) B 
 INNER JOIN Shop_Products P on P.ProductId = B.ProductID order by ToalQuantity desc
", subLength);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion

        #region 店铺排行统计
        public DataSet ShopSale(StatisticMode mode, DateTime startDate, DateTime endDate)
        {
            int subLength = 8;
            string method;
            switch (mode)
            {
                case StatisticMode.Year:
                    subLength = 4;
                    method = "GET_GeneratedYear";
                    break;
                case StatisticMode.Month:
                    subLength = 6;
                    method = "GET_GeneratedMonth";
                    break;
                case StatisticMode.Day:
                    subLength = 8;
                    method = "GET_GeneratedDay";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
           @"
--店铺排行走势图
SELECT  A.GeneratedDate AS GeneratedDate
       ,B.SupplierID
       ,CASE WHEN Amount IS NULL THEN 0
             ELSE Amount
        END AS Amount
       ,P.Name
       ,P.ShopName,P.Slogan
FROM    ( SELECT    *
          FROM      {0}(@StartDate, @EndDate)
        ) A
        LEFT JOIN ( SELECT  CONVERT(varchar({1}) , U.CreatedDate, 112 ) GeneratedDate
                          ,sum(SupplierId) SupplierId
                          ,sum(Amount) Amount
                    FROM    Shop_Orders U
                    ", method, subLength);
            strSql.AppendFormat(@" 
                            where U.CreatedDate BETWEEN @StartDate AND @EndDate 
                            and SupplierId!=-1
                            GROUP BY CONVERT(varchar({0}) , U.CreatedDate, 112 )
                  ) B 
ON CONVERT(varchar({0}) , A.GeneratedDate, 112 ) = CONVERT(varchar({0}) , B.GeneratedDate, 112 ) 
left join Shop_Suppliers P on P.SupplierId = B.SupplierId ", subLength);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion

        #region 每种店铺统计
        public DataSet ShopSaleInfo(StatisticMode mode, DateTime startDate, DateTime endDate)
        {
            int subLength = 8;
            string method;
            switch (mode)
            {
                case StatisticMode.Year:
                    subLength = 4;
                    method = "GET_GeneratedYear";
                    break;
                case StatisticMode.Month:
                    subLength = 6;
                    method = "GET_GeneratedMonth";
                    break;
                case StatisticMode.Day:
                    subLength = 8;
                    method = "GET_GeneratedDay";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
           @"
--店铺排行走势图
SELECT B.SupplierID
       ,Amount as Amount
       ,P.Name
       ,P.ShopName 

 FROM  (SELECT  SupplierId ,sum(Amount) Amount
                    FROM    Shop_Orders U
                    ", method, subLength);
            strSql.AppendFormat(@" 
                            where U.CreatedDate BETWEEN @StartDate AND @EndDate 
                            and SupplierId!=-1
                            GROUP BY SupplierId
                  ) B 
left join Shop_Suppliers P on P.SupplierId = B.SupplierId order by Amount desc", subLength);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion

        #region 商品销量排行统计

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetRecordCount(StatisticMode mode, DateTime startDate, DateTime endDate, string modes, int startIndex, int endIndex,int supplierId)
        {
            int subLength = 8;
            string method;
            switch (mode)
            {
                case StatisticMode.Year:
                    subLength = 4;
                    method = "GET_GeneratedYear";
                    break;
                case StatisticMode.Month:
                    subLength = 6;
                    method = "GET_GeneratedMonth";
                    break;
                case StatisticMode.Day:
                    subLength = 8;
                    method = "GET_GeneratedDay";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
            @"
--商品销量排行统计列表
select COUNT(*) from (
select * from (select ROW_NUMBER() over (order by T.ToalQuantity desc,T.GeneratedDate desc) as num,* from (
SELECT  A.GeneratedDate AS GeneratedDate
      , CASE WHEN B.ProductID IS NULL THEN 0
             ELSE B.ProductID
        END AS Product
      , CASE WHEN B.ToalQuantity IS NULL THEN 0
             ELSE B.ToalQuantity
        END AS ToalQuantity
        ,P.ProductName
FROM    ( SELECT    *
          FROM      {0}(@StartDate, @EndDate)
        ) A
        INNER JOIN ( SELECT  CONVERT(varchar({1}) , O.CreatedDate, 112 ) GeneratedDate
                          , I.ProductId ProductID
                          , SUM(I.Quantity) ToalQuantity
                    FROM    Shop_OrderItems I
                          , Shop_Orders O
                    WHERE   I.OrderId = O.OrderId  ", method, subLength);
            if (supplierId > 0)
            {
                strSql.AppendFormat(" and I.SupplierId={0}  ",supplierId);
            }
            strSql.AppendFormat(@" 
                            AND O.CreatedDate BETWEEN @StartDate AND @EndDate 
                    GROUP BY CONVERT(varchar({0}) , O.CreatedDate, 112 ),I.ProductId
                  ) B 
ON CONVERT(varchar({0}) , A.GeneratedDate, 112 ) = CONVERT(varchar({0}) , B.GeneratedDate, 112 ) 
 INNER JOIN Shop_Products P on P.ProductId = B.ProductID  ) as T )as M)as N 
", subLength);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }

        }

        public static Maticsoft.ViewModel.Order.OrderInfoExPage DataRowToModel(DataRow row)
        {
            Maticsoft.ViewModel.Order.OrderInfoExPage model = new Maticsoft.ViewModel.Order.OrderInfoExPage();
            if (row != null)
            {
                if (row["GeneratedDate"] != null && row["GeneratedDate"].ToString() != "")
                {
                    model.GeneratedDate = DateTime.Parse(row["GeneratedDate"].ToString());
                }
                if (row["Product"] != null && row["Product"].ToString() != "")
                {
                    model.Product = int.Parse(row["Product"].ToString());
                }
                if (row["ToalQuantity"] != null && row["ToalQuantity"].ToString() != "")
                {
                    model.ToalQuantity = int.Parse(row["ToalQuantity"].ToString());
                }
                if (row["ProductName"] != null && row["ProductName"].ToString() != "")
                {
                    model.ProductName = row["ProductName"].ToString();
                }
            }
            return model;
        }

        public DataSet GetListByPage(StatisticMode mode, DateTime startDate, DateTime endDate, string modes, int startIndex, int endIndex,int supplierId)
        {
            int subLength = 8;
            string method;
            switch (mode)
            {
                case StatisticMode.Year:
                    subLength = 4;
                    method = "GET_GeneratedYear";
                    break;
                case StatisticMode.Month:
                    subLength = 6;
                    method = "GET_GeneratedMonth";
                    break;
                case StatisticMode.Day:
                    subLength = 8;
                    method = "GET_GeneratedDay";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(
            @"
--商品销量排行统计列表

select * from (select ROW_NUMBER() over (order by T.ToalQuantity desc,T.GeneratedDate desc ) as num,* from (
SELECT  A.GeneratedDate AS GeneratedDate
      , CASE WHEN B.ProductID IS NULL THEN 0
             ELSE B.ProductID
        END AS Product
      , CASE WHEN B.ToalQuantity IS NULL THEN 0
             ELSE B.ToalQuantity
        END AS ToalQuantity
        ,P.ProductName
FROM    ( SELECT    *
          FROM      {0}(@StartDate, @EndDate)
        ) A
        INNER JOIN ( SELECT  CONVERT(varchar({1}) , O.CreatedDate, 112 ) GeneratedDate
                          , I.ProductId ProductID
                          , SUM(I.Quantity) ToalQuantity
                    FROM    Shop_OrderItems I
                          , Shop_Orders O
                    WHERE   I.OrderId = O.OrderId ", method, subLength );
            if (supplierId > 0)
            {
                strSql.AppendFormat(" and I.SupplierId={0}  ", supplierId);
            }
            strSql.AppendFormat(@" 
                            AND O.CreatedDate BETWEEN @StartDate AND @EndDate 
                    GROUP BY CONVERT(varchar({0}) , O.CreatedDate, 112 ),I.ProductId
                  ) B 
ON CONVERT(varchar({0}) , A.GeneratedDate, 112 ) = CONVERT(varchar({0}) , B.GeneratedDate, 112 ) 
 INNER JOIN Shop_Products P on P.ProductId = B.ProductID  ) as T )as M where M.num between {1} and {2} 
", subLength, startIndex, endIndex);
            SqlParameter[] parameters =
            {
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime)
            };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion

        
    }
}