/**
* OrdersHistory.cs
*
* 功 能： N/A
* 类 名： OrdersHistory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/7 12:05:24   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
	/// <summary>
	/// 数据访问类:OrdersHistory
	/// </summary>
	public partial class OrdersHistory:IOrdersHistory
	{
		public OrdersHistory()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long OrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_OrdersHistory");
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)			};
            parameters[0].Value = OrderId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Order.OrdersHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_OrdersHistory(");
            strSql.Append("OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@OrderCode,@ParentOrderId,@CreatedDate,@UpdatedDate,@BuyerID,@BuyerName,@BuyerEmail,@BuyerCellPhone,@RegionId,@ShipRegion,@ShipAddress,@ShipZipCode,@ShipName,@ShipTelPhone,@ShipCellPhone,@ShipEmail,@ShippingModeId,@ShippingModeName,@RealShippingModeId,@RealShippingModeName,@ShipperId,@ShipperName,@ShipperAddress,@ShipperCellPhone,@Freight,@FreightAdjusted,@FreightActual,@Weight,@ShippingStatus,@ShipOrderNumber,@ExpressCompanyName,@ExpressCompanyAbb,@PaymentTypeId,@PaymentTypeName,@PaymentGateway,@PaymentStatus,@RefundStatus,@PayCurrencyCode,@PayCurrencyName,@PaymentFee,@PaymentFeeAdjusted,@GatewayOrderId,@OrderTotal,@OrderPoint,@OrderCostPrice,@OrderProfit,@OrderOtherCost,@OrderOptionPrice,@DiscountName,@DiscountAmount,@DiscountAdjusted,@DiscountValue,@DiscountValueType,@CouponCode,@CouponName,@CouponAmount,@CouponValue,@CouponValueType,@ActivityName,@ActivityFreeAmount,@ActivityStatus,@GroupBuyId,@GroupBuyPrice,@GroupBuyStatus,@Amount,@OrderType,@OrderStatus,@SellerID,@SellerName,@SellerEmail,@SellerCellPhone,@SupplierId,@SupplierName,@ReferID,@ReferURL,@OrderIP,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
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
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,100),
					new SqlParameter("@ReferID", SqlDbType.NVarChar,50),
					new SqlParameter("@ReferURL", SqlDbType.NVarChar,200),
					new SqlParameter("@OrderIP", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.OrderCode;
            parameters[2].Value = model.ParentOrderId;
            parameters[3].Value = model.CreatedDate;
            parameters[4].Value = model.UpdatedDate;
            parameters[5].Value = model.BuyerID;
            parameters[6].Value = model.BuyerName;
            parameters[7].Value = model.BuyerEmail;
            parameters[8].Value = model.BuyerCellPhone;
            parameters[9].Value = model.RegionId;
            parameters[10].Value = model.ShipRegion;
            parameters[11].Value = model.ShipAddress;
            parameters[12].Value = model.ShipZipCode;
            parameters[13].Value = model.ShipName;
            parameters[14].Value = model.ShipTelPhone;
            parameters[15].Value = model.ShipCellPhone;
            parameters[16].Value = model.ShipEmail;
            parameters[17].Value = model.ShippingModeId;
            parameters[18].Value = model.ShippingModeName;
            parameters[19].Value = model.RealShippingModeId;
            parameters[20].Value = model.RealShippingModeName;
            parameters[21].Value = model.ShipperId;
            parameters[22].Value = model.ShipperName;
            parameters[23].Value = model.ShipperAddress;
            parameters[24].Value = model.ShipperCellPhone;
            parameters[25].Value = model.Freight;
            parameters[26].Value = model.FreightAdjusted;
            parameters[27].Value = model.FreightActual;
            parameters[28].Value = model.Weight;
            parameters[29].Value = model.ShippingStatus;
            parameters[30].Value = model.ShipOrderNumber;
            parameters[31].Value = model.ExpressCompanyName;
            parameters[32].Value = model.ExpressCompanyAbb;
            parameters[33].Value = model.PaymentTypeId;
            parameters[34].Value = model.PaymentTypeName;
            parameters[35].Value = model.PaymentGateway;
            parameters[36].Value = model.PaymentStatus;
            parameters[37].Value = model.RefundStatus;
            parameters[38].Value = model.PayCurrencyCode;
            parameters[39].Value = model.PayCurrencyName;
            parameters[40].Value = model.PaymentFee;
            parameters[41].Value = model.PaymentFeeAdjusted;
            parameters[42].Value = model.GatewayOrderId;
            parameters[43].Value = model.OrderTotal;
            parameters[44].Value = model.OrderPoint;
            parameters[45].Value = model.OrderCostPrice;
            parameters[46].Value = model.OrderProfit;
            parameters[47].Value = model.OrderOtherCost;
            parameters[48].Value = model.OrderOptionPrice;
            parameters[49].Value = model.DiscountName;
            parameters[50].Value = model.DiscountAmount;
            parameters[51].Value = model.DiscountAdjusted;
            parameters[52].Value = model.DiscountValue;
            parameters[53].Value = model.DiscountValueType;
            parameters[54].Value = model.CouponCode;
            parameters[55].Value = model.CouponName;
            parameters[56].Value = model.CouponAmount;
            parameters[57].Value = model.CouponValue;
            parameters[58].Value = model.CouponValueType;
            parameters[59].Value = model.ActivityName;
            parameters[60].Value = model.ActivityFreeAmount;
            parameters[61].Value = model.ActivityStatus;
            parameters[62].Value = model.GroupBuyId;
            parameters[63].Value = model.GroupBuyPrice;
            parameters[64].Value = model.GroupBuyStatus;
            parameters[65].Value = model.Amount;
            parameters[66].Value = model.OrderType;
            parameters[67].Value = model.OrderStatus;
            parameters[68].Value = model.SellerID;
            parameters[69].Value = model.SellerName;
            parameters[70].Value = model.SellerEmail;
            parameters[71].Value = model.SellerCellPhone;
            parameters[72].Value = model.SupplierId;
            parameters[73].Value = model.SupplierName;
            parameters[74].Value = model.ReferID;
            parameters[75].Value = model.ReferURL;
            parameters[76].Value = model.OrderIP;
            parameters[77].Value = model.Remark;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Order.OrdersHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrdersHistory set ");
            strSql.Append("OrderCode=@OrderCode,");
            strSql.Append("ParentOrderId=@ParentOrderId,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("UpdatedDate=@UpdatedDate,");
            strSql.Append("BuyerID=@BuyerID,");
            strSql.Append("BuyerName=@BuyerName,");
            strSql.Append("BuyerEmail=@BuyerEmail,");
            strSql.Append("BuyerCellPhone=@BuyerCellPhone,");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("ShipRegion=@ShipRegion,");
            strSql.Append("ShipAddress=@ShipAddress,");
            strSql.Append("ShipZipCode=@ShipZipCode,");
            strSql.Append("ShipName=@ShipName,");
            strSql.Append("ShipTelPhone=@ShipTelPhone,");
            strSql.Append("ShipCellPhone=@ShipCellPhone,");
            strSql.Append("ShipEmail=@ShipEmail,");
            strSql.Append("ShippingModeId=@ShippingModeId,");
            strSql.Append("ShippingModeName=@ShippingModeName,");
            strSql.Append("RealShippingModeId=@RealShippingModeId,");
            strSql.Append("RealShippingModeName=@RealShippingModeName,");
            strSql.Append("ShipperId=@ShipperId,");
            strSql.Append("ShipperName=@ShipperName,");
            strSql.Append("ShipperAddress=@ShipperAddress,");
            strSql.Append("ShipperCellPhone=@ShipperCellPhone,");
            strSql.Append("Freight=@Freight,");
            strSql.Append("FreightAdjusted=@FreightAdjusted,");
            strSql.Append("FreightActual=@FreightActual,");
            strSql.Append("Weight=@Weight,");
            strSql.Append("ShippingStatus=@ShippingStatus,");
            strSql.Append("ShipOrderNumber=@ShipOrderNumber,");
            strSql.Append("ExpressCompanyName=@ExpressCompanyName,");
            strSql.Append("ExpressCompanyAbb=@ExpressCompanyAbb,");
            strSql.Append("PaymentTypeId=@PaymentTypeId,");
            strSql.Append("PaymentTypeName=@PaymentTypeName,");
            strSql.Append("PaymentGateway=@PaymentGateway,");
            strSql.Append("PaymentStatus=@PaymentStatus,");
            strSql.Append("RefundStatus=@RefundStatus,");
            strSql.Append("PayCurrencyCode=@PayCurrencyCode,");
            strSql.Append("PayCurrencyName=@PayCurrencyName,");
            strSql.Append("PaymentFee=@PaymentFee,");
            strSql.Append("PaymentFeeAdjusted=@PaymentFeeAdjusted,");
            strSql.Append("GatewayOrderId=@GatewayOrderId,");
            strSql.Append("OrderTotal=@OrderTotal,");
            strSql.Append("OrderPoint=@OrderPoint,");
            strSql.Append("OrderCostPrice=@OrderCostPrice,");
            strSql.Append("OrderProfit=@OrderProfit,");
            strSql.Append("OrderOtherCost=@OrderOtherCost,");
            strSql.Append("OrderOptionPrice=@OrderOptionPrice,");
            strSql.Append("DiscountName=@DiscountName,");
            strSql.Append("DiscountAmount=@DiscountAmount,");
            strSql.Append("DiscountAdjusted=@DiscountAdjusted,");
            strSql.Append("DiscountValue=@DiscountValue,");
            strSql.Append("DiscountValueType=@DiscountValueType,");
            strSql.Append("CouponCode=@CouponCode,");
            strSql.Append("CouponName=@CouponName,");
            strSql.Append("CouponAmount=@CouponAmount,");
            strSql.Append("CouponValue=@CouponValue,");
            strSql.Append("CouponValueType=@CouponValueType,");
            strSql.Append("ActivityName=@ActivityName,");
            strSql.Append("ActivityFreeAmount=@ActivityFreeAmount,");
            strSql.Append("ActivityStatus=@ActivityStatus,");
            strSql.Append("GroupBuyId=@GroupBuyId,");
            strSql.Append("GroupBuyPrice=@GroupBuyPrice,");
            strSql.Append("GroupBuyStatus=@GroupBuyStatus,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("OrderType=@OrderType,");
            strSql.Append("OrderStatus=@OrderStatus,");
            strSql.Append("SellerID=@SellerID,");
            strSql.Append("SellerName=@SellerName,");
            strSql.Append("SellerEmail=@SellerEmail,");
            strSql.Append("SellerCellPhone=@SellerCellPhone,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("ReferID=@ReferID,");
            strSql.Append("ReferURL=@ReferURL,");
            strSql.Append("OrderIP=@OrderIP,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where OrderId=@OrderId ");
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
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,100),
					new SqlParameter("@ReferID", SqlDbType.NVarChar,50),
					new SqlParameter("@ReferURL", SqlDbType.NVarChar,200),
					new SqlParameter("@OrderIP", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)};
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
            parameters[71].Value = model.SupplierId;
            parameters[72].Value = model.SupplierName;
            parameters[73].Value = model.ReferID;
            parameters[74].Value = model.ReferURL;
            parameters[75].Value = model.OrderIP;
            parameters[76].Value = model.Remark;
            parameters[77].Value = model.OrderId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrdersHistory ");
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)			};
            parameters[0].Value = OrderId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string OrderIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrdersHistory ");
            strSql.Append(" where OrderId in (" + OrderIdlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrdersHistory GetModel(long OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark from Shop_OrdersHistory ");
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)			};
            parameters[0].Value = OrderId;

            Maticsoft.Model.Shop.Order.OrdersHistory model = new Maticsoft.Model.Shop.Order.OrdersHistory();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrdersHistory DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrdersHistory model = new Maticsoft.Model.Shop.Order.OrdersHistory();
            if (row != null)
            {
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    model.OrderCode = row["OrderCode"].ToString();
                }
                if (row["ParentOrderId"] != null && row["ParentOrderId"].ToString() != "")
                {
                    model.ParentOrderId = long.Parse(row["ParentOrderId"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["UpdatedDate"] != null && row["UpdatedDate"].ToString() != "")
                {
                    model.UpdatedDate = DateTime.Parse(row["UpdatedDate"].ToString());
                }
                if (row["BuyerID"] != null && row["BuyerID"].ToString() != "")
                {
                    model.BuyerID = int.Parse(row["BuyerID"].ToString());
                }
                if (row["BuyerName"] != null)
                {
                    model.BuyerName = row["BuyerName"].ToString();
                }
                if (row["BuyerEmail"] != null)
                {
                    model.BuyerEmail = row["BuyerEmail"].ToString();
                }
                if (row["BuyerCellPhone"] != null)
                {
                    model.BuyerCellPhone = row["BuyerCellPhone"].ToString();
                }
                if (row["RegionId"] != null && row["RegionId"].ToString() != "")
                {
                    model.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if (row["ShipRegion"] != null)
                {
                    model.ShipRegion = row["ShipRegion"].ToString();
                }
                if (row["ShipAddress"] != null)
                {
                    model.ShipAddress = row["ShipAddress"].ToString();
                }
                if (row["ShipZipCode"] != null)
                {
                    model.ShipZipCode = row["ShipZipCode"].ToString();
                }
                if (row["ShipName"] != null)
                {
                    model.ShipName = row["ShipName"].ToString();
                }
                if (row["ShipTelPhone"] != null)
                {
                    model.ShipTelPhone = row["ShipTelPhone"].ToString();
                }
                if (row["ShipCellPhone"] != null)
                {
                    model.ShipCellPhone = row["ShipCellPhone"].ToString();
                }
                if (row["ShipEmail"] != null)
                {
                    model.ShipEmail = row["ShipEmail"].ToString();
                }
                if (row["ShippingModeId"] != null && row["ShippingModeId"].ToString() != "")
                {
                    model.ShippingModeId = int.Parse(row["ShippingModeId"].ToString());
                }
                if (row["ShippingModeName"] != null)
                {
                    model.ShippingModeName = row["ShippingModeName"].ToString();
                }
                if (row["RealShippingModeId"] != null && row["RealShippingModeId"].ToString() != "")
                {
                    model.RealShippingModeId = int.Parse(row["RealShippingModeId"].ToString());
                }
                if (row["RealShippingModeName"] != null)
                {
                    model.RealShippingModeName = row["RealShippingModeName"].ToString();
                }
                if (row["ShipperId"] != null && row["ShipperId"].ToString() != "")
                {
                    model.ShipperId = int.Parse(row["ShipperId"].ToString());
                }
                if (row["ShipperName"] != null)
                {
                    model.ShipperName = row["ShipperName"].ToString();
                }
                if (row["ShipperAddress"] != null)
                {
                    model.ShipperAddress = row["ShipperAddress"].ToString();
                }
                if (row["ShipperCellPhone"] != null)
                {
                    model.ShipperCellPhone = row["ShipperCellPhone"].ToString();
                }
                if (row["Freight"] != null && row["Freight"].ToString() != "")
                {
                    model.Freight = decimal.Parse(row["Freight"].ToString());
                }
                if (row["FreightAdjusted"] != null && row["FreightAdjusted"].ToString() != "")
                {
                    model.FreightAdjusted = decimal.Parse(row["FreightAdjusted"].ToString());
                }
                if (row["FreightActual"] != null && row["FreightActual"].ToString() != "")
                {
                    model.FreightActual = decimal.Parse(row["FreightActual"].ToString());
                }
                if (row["Weight"] != null && row["Weight"].ToString() != "")
                {
                    model.Weight = int.Parse(row["Weight"].ToString());
                }
                if (row["ShippingStatus"] != null && row["ShippingStatus"].ToString() != "")
                {
                    model.ShippingStatus = int.Parse(row["ShippingStatus"].ToString());
                }
                if (row["ShipOrderNumber"] != null)
                {
                    model.ShipOrderNumber = row["ShipOrderNumber"].ToString();
                }
                if (row["ExpressCompanyName"] != null)
                {
                    model.ExpressCompanyName = row["ExpressCompanyName"].ToString();
                }
                if (row["ExpressCompanyAbb"] != null)
                {
                    model.ExpressCompanyAbb = row["ExpressCompanyAbb"].ToString();
                }
                if (row["PaymentTypeId"] != null && row["PaymentTypeId"].ToString() != "")
                {
                    model.PaymentTypeId = int.Parse(row["PaymentTypeId"].ToString());
                }
                if (row["PaymentTypeName"] != null)
                {
                    model.PaymentTypeName = row["PaymentTypeName"].ToString();
                }
                if (row["PaymentGateway"] != null)
                {
                    model.PaymentGateway = row["PaymentGateway"].ToString();
                }
                if (row["PaymentStatus"] != null && row["PaymentStatus"].ToString() != "")
                {
                    model.PaymentStatus = int.Parse(row["PaymentStatus"].ToString());
                }
                if (row["RefundStatus"] != null && row["RefundStatus"].ToString() != "")
                {
                    model.RefundStatus = int.Parse(row["RefundStatus"].ToString());
                }
                if (row["PayCurrencyCode"] != null)
                {
                    model.PayCurrencyCode = row["PayCurrencyCode"].ToString();
                }
                if (row["PayCurrencyName"] != null)
                {
                    model.PayCurrencyName = row["PayCurrencyName"].ToString();
                }
                if (row["PaymentFee"] != null && row["PaymentFee"].ToString() != "")
                {
                    model.PaymentFee = decimal.Parse(row["PaymentFee"].ToString());
                }
                if (row["PaymentFeeAdjusted"] != null && row["PaymentFeeAdjusted"].ToString() != "")
                {
                    model.PaymentFeeAdjusted = decimal.Parse(row["PaymentFeeAdjusted"].ToString());
                }
                if (row["GatewayOrderId"] != null)
                {
                    model.GatewayOrderId = row["GatewayOrderId"].ToString();
                }
                if (row["OrderTotal"] != null && row["OrderTotal"].ToString() != "")
                {
                    model.OrderTotal = decimal.Parse(row["OrderTotal"].ToString());
                }
                if (row["OrderPoint"] != null && row["OrderPoint"].ToString() != "")
                {
                    model.OrderPoint = int.Parse(row["OrderPoint"].ToString());
                }
                if (row["OrderCostPrice"] != null && row["OrderCostPrice"].ToString() != "")
                {
                    model.OrderCostPrice = decimal.Parse(row["OrderCostPrice"].ToString());
                }
                if (row["OrderProfit"] != null && row["OrderProfit"].ToString() != "")
                {
                    model.OrderProfit = decimal.Parse(row["OrderProfit"].ToString());
                }
                if (row["OrderOtherCost"] != null && row["OrderOtherCost"].ToString() != "")
                {
                    model.OrderOtherCost = decimal.Parse(row["OrderOtherCost"].ToString());
                }
                if (row["OrderOptionPrice"] != null && row["OrderOptionPrice"].ToString() != "")
                {
                    model.OrderOptionPrice = decimal.Parse(row["OrderOptionPrice"].ToString());
                }
                if (row["DiscountName"] != null)
                {
                    model.DiscountName = row["DiscountName"].ToString();
                }
                if (row["DiscountAmount"] != null && row["DiscountAmount"].ToString() != "")
                {
                    model.DiscountAmount = decimal.Parse(row["DiscountAmount"].ToString());
                }
                if (row["DiscountAdjusted"] != null && row["DiscountAdjusted"].ToString() != "")
                {
                    model.DiscountAdjusted = decimal.Parse(row["DiscountAdjusted"].ToString());
                }
                if (row["DiscountValue"] != null && row["DiscountValue"].ToString() != "")
                {
                    model.DiscountValue = decimal.Parse(row["DiscountValue"].ToString());
                }
                if (row["DiscountValueType"] != null && row["DiscountValueType"].ToString() != "")
                {
                    model.DiscountValueType = int.Parse(row["DiscountValueType"].ToString());
                }
                if (row["CouponCode"] != null)
                {
                    model.CouponCode = row["CouponCode"].ToString();
                }
                if (row["CouponName"] != null)
                {
                    model.CouponName = row["CouponName"].ToString();
                }
                if (row["CouponAmount"] != null && row["CouponAmount"].ToString() != "")
                {
                    model.CouponAmount = decimal.Parse(row["CouponAmount"].ToString());
                }
                if (row["CouponValue"] != null && row["CouponValue"].ToString() != "")
                {
                    model.CouponValue = decimal.Parse(row["CouponValue"].ToString());
                }
                if (row["CouponValueType"] != null && row["CouponValueType"].ToString() != "")
                {
                    model.CouponValueType = int.Parse(row["CouponValueType"].ToString());
                }
                if (row["ActivityName"] != null)
                {
                    model.ActivityName = row["ActivityName"].ToString();
                }
                if (row["ActivityFreeAmount"] != null && row["ActivityFreeAmount"].ToString() != "")
                {
                    model.ActivityFreeAmount = decimal.Parse(row["ActivityFreeAmount"].ToString());
                }
                if (row["ActivityStatus"] != null && row["ActivityStatus"].ToString() != "")
                {
                    model.ActivityStatus = int.Parse(row["ActivityStatus"].ToString());
                }
                if (row["GroupBuyId"] != null && row["GroupBuyId"].ToString() != "")
                {
                    model.GroupBuyId = int.Parse(row["GroupBuyId"].ToString());
                }
                if (row["GroupBuyPrice"] != null && row["GroupBuyPrice"].ToString() != "")
                {
                    model.GroupBuyPrice = decimal.Parse(row["GroupBuyPrice"].ToString());
                }
                if (row["GroupBuyStatus"] != null && row["GroupBuyStatus"].ToString() != "")
                {
                    model.GroupBuyStatus = int.Parse(row["GroupBuyStatus"].ToString());
                }
                if (row["Amount"] != null && row["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if (row["OrderType"] != null && row["OrderType"].ToString() != "")
                {
                    model.OrderType = int.Parse(row["OrderType"].ToString());
                }
                if (row["OrderStatus"] != null && row["OrderStatus"].ToString() != "")
                {
                    model.OrderStatus = int.Parse(row["OrderStatus"].ToString());
                }
                if (row["SellerID"] != null && row["SellerID"].ToString() != "")
                {
                    model.SellerID = int.Parse(row["SellerID"].ToString());
                }
                if (row["SellerName"] != null)
                {
                    model.SellerName = row["SellerName"].ToString();
                }
                if (row["SellerEmail"] != null)
                {
                    model.SellerEmail = row["SellerEmail"].ToString();
                }
                if (row["SellerCellPhone"] != null)
                {
                    model.SellerCellPhone = row["SellerCellPhone"].ToString();
                }
                if (row["SupplierId"] != null && row["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["SupplierName"] != null)
                {
                    model.SupplierName = row["SupplierName"].ToString();
                }
                if (row["ReferID"] != null)
                {
                    model.ReferID = row["ReferID"].ToString();
                }
                if (row["ReferURL"] != null)
                {
                    model.ReferURL = row["ReferURL"].ToString();
                }
                if (row["OrderIP"] != null)
                {
                    model.OrderIP = row["OrderIP"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark ");
            strSql.Append(" FROM Shop_OrdersHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark ");
            strSql.Append(" FROM Shop_OrdersHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Shop_OrdersHistory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.OrderId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_OrdersHistory T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NVarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.NVarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.NVarChar,1000),
                    };
            parameters[0].Value = "Shop_OrdersHistory";
            parameters[1].Value = "OrderId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

