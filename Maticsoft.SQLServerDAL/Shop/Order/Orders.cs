using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
    /// <summary>
    /// 数据访问类:Orders
    /// </summary>
    public partial class Orders : IOrders
    {
        public Orders()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long OrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_Orders");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt)
			};
            parameters[0].Value = OrderId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Order.OrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_Orders(");
            strSql.Append("OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews,CardSysId,PaymentNumber,CardNo,HasReturn )");
            strSql.Append(" values (");
            strSql.Append("@OrderCode,@ParentOrderId,@CreatedDate,@UpdatedDate,@BuyerID,@BuyerName,@BuyerEmail,@BuyerCellPhone,@RegionId,@ShipRegion,@ShipAddress,@ShipZipCode,@ShipName,@ShipTelPhone,@ShipCellPhone,@ShipEmail,@ShippingModeId,@ShippingModeName,@RealShippingModeId,@RealShippingModeName,@ShipperId,@ShipperName,@ShipperAddress,@ShipperCellPhone,@Freight,@FreightAdjusted,@FreightActual,@Weight,@ShippingStatus,@ShipOrderNumber,@ExpressCompanyName,@ExpressCompanyAbb,@PaymentTypeId,@PaymentTypeName,@PaymentGateway,@PaymentStatus,@RefundStatus,@PayCurrencyCode,@PayCurrencyName,@PaymentFee,@PaymentFeeAdjusted,@GatewayOrderId,@OrderTotal,@OrderPoint,@OrderCostPrice,@OrderProfit,@OrderOtherCost,@OrderOptionPrice,@DiscountName,@DiscountAmount,@DiscountAdjusted,@DiscountValue,@DiscountValueType,@CouponCode,@CouponName,@CouponAmount,@CouponValue,@CouponValueType,@ActivityName,@ActivityFreeAmount,@ActivityStatus,@GroupBuyId,@GroupBuyPrice,@GroupBuyStatus,@Amount,@OrderType,@OrderStatus,@SellerID,@SellerName,@SellerEmail,@SellerCellPhone,@CommentStatus,@SupplierId,@SupplierName,@ReferID,@ReferURL,@OrderIP,@Remark,@ProductTotal,@HasChildren,@IsReviews,@CardSysId,@PaymentNumber,@CardNo,@HasReturn)");
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
                    new SqlParameter("@PaymentNumber",SqlDbType.NVarChar,100),
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@HasReturn", SqlDbType.Bit)
                                        };
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
            parameters[82].Value = model.Paymentnumber;
            if (model.OrderType == 2)
            {
                model.CardNo = "";
            }
            parameters[83].Value = model.CardNo;
            parameters[83].Value = model.HasReturn;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Order.OrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Orders set ");
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
            strSql.Append("CommentStatus=@CommentStatus,");
            strSql.Append("SupplierId=@SupplierId,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("ReferID=@ReferID,");
            strSql.Append("ReferURL=@ReferURL,");
            strSql.Append("OrderIP=@OrderIP,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ProductTotal=@ProductTotal,");
            strSql.Append("HasChildren=@HasChildren,");
            strSql.Append("IsReviews=@IsReviews,");
            strSql.Append("CardSysId=@CardSysId,");
            strSql.Append("PaymentNumber=@PaymentNumber,");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("HasReturn=@HasReturn");
            strSql.Append(" where OrderId=@OrderId");
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
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
                    new SqlParameter("@CardSysId",SqlDbType.Int,4),
                    new SqlParameter("@PaymentNumber",SqlDbType.NVarChar,100),
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@HasReturn", SqlDbType.Bit)
                                        };
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
            parameters[81].Value = model.OrderId;
            parameters[82].Value = model.CardSysId;
            parameters[83].Value = model.Paymentnumber;
            if (model.OrderType == 2)
            {
                model.CardNo = "";
            }
            parameters[84].Value = model.CardNo;
            parameters[85].Value = model.HasReturn;

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
        /// 更新一条数据(liyongqin)
        /// </summary>
        public bool UpdateGetaway(Maticsoft.Model.Shop.Order.OrderInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Orders set ");
            strSql.Append("PaymentTypeId=@PaymentTypeId,");
            strSql.Append("PaymentTypeName=@PaymentTypeName,");
            strSql.Append("PaymentGateway=@PaymentGateway ");
            strSql.Append(" where OrderId=@OrderId");

            SqlParameter[] parameters = {
                                 new SqlParameter("@PaymentTypeId", SqlDbType.Int,4),
                                 new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
                                 new SqlParameter("@PaymentGateway", SqlDbType.NVarChar,50),
                                 new SqlParameter("OrderId",SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = model.PaymentTypeId;
            parameters[1].Value = model.PaymentTypeName;
            parameters[2].Value = model.PaymentGateway;
            parameters[3].Value = model.OrderId;
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
            strSql.Append("delete from Shop_Orders ");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt)
			};
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
            strSql.Append("delete from Shop_Orders ");
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
        public Maticsoft.Model.Shop.Order.OrderInfo GetModel(long OrderId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews,CardSysId,PaymentNumber,CardNo,HasReturn from Shop_Orders ");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt)
			};
            parameters[0].Value = OrderId;

            Maticsoft.Model.Shop.Order.OrderInfo model = new Maticsoft.Model.Shop.Order.OrderInfo();
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
        //public Maticsoft.Model.Shop.Order.OrderInfo GetOrderInfoModel(long OrderId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select  top 1 * from Shop_Orders ");
        //    strSql.Append(" where ParentOrderId=@ParentOrderId");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ParentOrderId", SqlDbType.BigInt)
        //    };
        //    parameters[0].Value = OrderId;

        //    Maticsoft.Model.Shop.Order.OrderInfo model = new Maticsoft.Model.Shop.Order.OrderInfo();
        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return DataRowToModel(ds.Tables[0].Rows[0]);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Order.OrderInfo DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderInfo model = new Maticsoft.Model.Shop.Order.OrderInfo();
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
                if (row["CommentStatus"] != null && row["CommentStatus"].ToString() != "")
                {
                    model.CommentStatus = int.Parse(row["CommentStatus"].ToString());
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
                if (row["ProductTotal"] != null && row["ProductTotal"].ToString() != "")
                {
                    model.ProductTotal = decimal.Parse(row["ProductTotal"].ToString());
                }
                if (row["HasChildren"] != null && row["HasChildren"].ToString() != "")
                {
                    if ((row["HasChildren"].ToString() == "1") || (row["HasChildren"].ToString().ToLower() == "true"))
                    {
                        model.HasChildren = true;
                    }
                    else
                    {
                        model.HasChildren = false;
                    }
                }
                if (row["IsReviews"] != null && row["IsReviews"].ToString() != "")
                {
                    if ((row["IsReviews"].ToString() == "1") || (row["IsReviews"].ToString().ToLower() == "true"))
                    {
                        model.IsReviews = true;
                    }
                    else
                    {
                        model.IsReviews = false;
                    }
                }
                if (row.Table.Columns.Contains("ActionDate"))
                {
                    if (row["ActionDate"].ToString() != "")
                    {
                        model.ActionDate = DateTime.Parse(row["ActionDate"].ToString());
                    }

                }
                if (!string.IsNullOrWhiteSpace(row["CardSysId"].ToString()))
                {
                    model.CardSysId = int.Parse(row["CardSysId"].ToString());
                }
                model.CardNo = row["CardNo"].ToString();
                model.Paymentnumber = row["PaymentNumber"].ToString();
                model.CardNo = "";
                if (!string.IsNullOrWhiteSpace(row["CardNo"].ToString()))
                {
                    model.CardNo = row["CardNo"].ToString();
                }

                if (row.Table.Columns.Contains("HasReturn"))
                {
                    bool hasReturn = false;
                    bool.TryParse(row["HasReturn"].ToString(), out hasReturn);
                    model.HasReturn = hasReturn;
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
            strSql.Append(@"select OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,
            ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,
            ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,
            ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,
            PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,
            DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,
            ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,
            SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews,CardSysId,PaymentNumber,CardNo,HasReturn ");
            strSql.Append(" FROM Shop_Orders ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.ActionDate,TT.* FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.OrderId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_Orders T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT ");
            strSql.Append(@"LEFT JOIN (SELECT * FROM dbo.Shop_OrderAction WHERE OrderId IN (
SELECT OrderId from Shop_Orders WHERE  BuyerID=562272 AND OrderType=1 
) AND ActionCode=104) A ON TT.OrderCode = A.OrderCode");
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
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Shop_Orders";
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
            strSql.Append("  A.ActionDate,T.OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews,CardSysId,PaymentNumber,CardNo,HasReturn ");
            strSql.Append(" FROM Shop_Orders T ");
            strSql.Append(" LEFT JOIN (SELECT distinct OrderId,ActionDate FROM dbo.Shop_OrderAction WHERE OrderId IN (SELECT OrderId FROM shop_orders T");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ) AND ActionCode=104 ) A ON T.OrderId=A.OrderId ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据,并获取支付日期
        /// </summary>
        public DataSet GetList2(int Top, string strWhere, string filedOrder)
        {
            string temp = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append("(SELECT TOP 1 ActionDate FROM dbo.Shop_OrderAction WHERE OrderId=t.OrderId  and ActionCode=102),");
            strSql.Append("(SELECT TOP 1 ActionDate FROM dbo.Shop_OrderAction WHERE OrderId=t.OrderId  and ActionCode=104)PayDate,");
            strSql.Append("(SELECT TOP 1 OrderCode FROM dbo.Shop_Orders WHERE OrderId=t.ParentOrderId) ParentOrderCode,");
            strSql.Append(" T.OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,");
            strSql.Append("RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,");
            strSql.Append("ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,");
            strSql.Append("Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,");
            strSql.Append("PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,");
            strSql.Append("PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,");
            strSql.Append("DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,");
            strSql.Append("CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,");
            strSql.Append("OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,");
            strSql.Append("Remark,ProductTotal,HasChildren,IsReviews,CardSysId,PaymentNumber,CardNo,ExpressCompanyName,HasReturn ");
            strSql.Append(" FROM Shop_Orders T ");
           
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
            strSql.Append("select count(1) FROM Shop_Orders T ");
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

        //退货操作
        public bool ReturnStatus(long orderId)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //返回库存

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete SNS_Photo ");
            //strSql.Append(" where PhotoId=@PhotoId");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@PhotoId", SqlDbType.Int,4)
            //};
            //parameters[0].Value = PhotoID;
            //CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            //sqllist.Add(cmd);

            //更新订单状态

            //StringBuilder strSql2 = new StringBuilder();
            //strSql2.Append("delete SNS_Comments ");
            //strSql2.Append(" where type=1 and TargetID=@PhotoId");
            //SqlParameter[] parameters2 = {
            //        new SqlParameter("@PhotoId", SqlDbType.Int,4)
            //};
            //parameters2[0].Value = PhotoID;
            //cmd = new CommandInfo(strSql2.ToString(), parameters2);
            //sqllist.Add(cmd);


            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(long orderId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Orders set ");
            strSql.Append("OrderStatus=@OrderStatus,");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderStatus", SqlDbType.SmallInt,2),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)};
            parameters[0].Value = status;
            parameters[1].Value = orderId;

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

        public bool UpdateShipped(Maticsoft.Model.Shop.Order.OrderInfo model)
        {

            List<CommandInfo> sqllist = new List<CommandInfo>();
            #region 更新动作

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Orders set ");
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
            strSql.Append("Remark=@Remark,");
            strSql.Append("CommentStatus=@CommentStatus,");
            strSql.Append("HasReturn = @HasReturn ");
            strSql.Append(" where OrderId=@OrderId");
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
                    new SqlParameter("@CommentStatus", SqlDbType.SmallInt,2),
                    new SqlParameter("@HasReturn", SqlDbType.Bit),
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
            parameters[77].Value = model.CommentStatus;
            parameters[78].Value = model.HasReturn;
            parameters[79].Value = model.OrderId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql6 = new StringBuilder();
            strSql6.Append("UPDATE Shop_OrderItems SET ShipmentQuantity=Quantity WHERE OrderId =@OrderId ");
            SqlParameter[] parameters6 = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)};
            parameters6[0].Value = model.OrderId;
            cmd = new CommandInfo(strSql6.ToString(), parameters6);
            sqllist.Add(cmd);

            #endregion 更新动作

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 根据条件获取对应的订单状态的数量
        /// </summary>
        /// <param name="userid">下单人 ID</param>
        /// <param name="PaymentStatus">支付状态</param>
        /// <param name="OrderStatusCancel">订单的取消状态</param>
        /// <returns></returns>
        public int GetPaymentStatusCounts(int userid, int PaymentStatus, int OrderStatusCancel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT COUNT (*) FROM  Shop_Orders WHERE BuyerID=@BuyerID AND PaymentStatus=@PaymentStatus AND OrderStatus!=@OrderStatus");
            SqlParameter[] parameters =
                {
                    new  SqlParameter("@BuyerID",userid),
                    new SqlParameter("@PaymentStatus",PaymentStatus),
                    new SqlParameter("@OrderStatus",OrderStatusCancel)
                };
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

        /// <summary>
        /// 更新订单备注
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateOrderRemark(long orderId, string Remark, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Orders set ");
            strSql.Append("Remark=@Remark ");
            strSql.Append(" where OrderId=@OrderId ");
            if (!String.IsNullOrWhiteSpace(strWhere))
            {
                strSql.Append(strWhere);
            }
            SqlParameter[] parameters = {
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000),
					new SqlParameter("@OrderId", SqlDbType.BigInt,8)};
            parameters[0].Value = Remark;
            parameters[1].Value = orderId;

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
        /// 获取指定父订单号和订单详细中商品编号的订单信息
        /// </summary>
        /// <param name="OrderID">父订单ID</param>
        /// <param name="ProductID">订单详情商品ID</param>
        /// <returns></returns>
        public DataTable GetChildOrder(Int64 OrderID, Int64 ProductID)
        {
            string str_Sql = "select * from Shop_Orders where OrderId=(select OrderId from Shop_OrderItems where OrderId in(select OrderId from Shop_Orders where ParentOrderId=" + OrderID + ") and ProductId=" + ProductID + ")";
            return DbHelperSQL.Query(str_Sql).Tables[0];
        }

        /// <summary>
        /// 获取指定订单的商品种类是否超过一种，超过返回True，反之，返回False
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <returns></returns>
        public bool GetProductTypeCount(Int64 OrderID)
        {
            string str_Sql = " select count(*) from Shop_OrderItems where OrderId = " + OrderID + " Group By ProductId ";
            DataTable dt = DbHelperSQL.Query(str_Sql).Tables[0];
            if (dt.Rows.Count > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Maticsoft.Model.Shop.Order.OrderInfo GetOrderInfo(string ordercode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from Shop_Orders ");
            strSql.Append(" where OrderCode=@OrderCode");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = ordercode;

            Maticsoft.Model.Shop.Order.OrderInfo model = new Maticsoft.Model.Shop.Order.OrderInfo();
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
        #region  获取过期主订单 前100个...每次处理100条数据
        public DataSet GetOrderCancelList(DateTime CancelDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 100 OrderId,OrderCode,BuyerID,CreatedDate,PaymentStatus FROM dbo.Shop_Orders WHERE OrderType=1 AND PaymentStatus=0 and OrderStatus<>-1 AND DATEDIFF(s,CreatedDate,@CancelDate)>=0 order by CreatedDate asc ");

            SqlParameter[] parameters = {
					new SqlParameter("@CancelDate", SqlDbType.DateTime)
			};
            parameters[0].Value = CancelDate;
            return DbHelperSQL.Query(strSql.ToString(), parameters);

        }
        #endregion

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataSet GetOrderInfo(long OrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OrderId,OrderCode,ParentOrderId,CreatedDate,UpdatedDate,BuyerID,BuyerName,BuyerEmail,BuyerCellPhone,RegionId,ShipRegion,ShipAddress,ShipZipCode,ShipName,ShipTelPhone,ShipCellPhone,ShipEmail,ShippingModeId,ShippingModeName,RealShippingModeId,RealShippingModeName,ShipperId,ShipperName,ShipperAddress,ShipperCellPhone,Freight,FreightAdjusted,FreightActual,Weight,ShippingStatus,ShipOrderNumber,ExpressCompanyName,ExpressCompanyAbb,PaymentTypeId,PaymentTypeName,PaymentGateway,PaymentStatus,RefundStatus,PayCurrencyCode,PayCurrencyName,PaymentFee,PaymentFeeAdjusted,GatewayOrderId,OrderTotal,OrderPoint,OrderCostPrice,OrderProfit,OrderOtherCost,OrderOptionPrice,DiscountName,DiscountAmount,DiscountAdjusted,DiscountValue,DiscountValueType,CouponCode,CouponName,CouponAmount,CouponValue,CouponValueType,ActivityName,ActivityFreeAmount,ActivityStatus,GroupBuyId,GroupBuyPrice,GroupBuyStatus,Amount,OrderType,OrderStatus,SellerID,SellerName,SellerEmail,SellerCellPhone,CommentStatus,SupplierId,SupplierName,ReferID,ReferURL,OrderIP,Remark,ProductTotal,HasChildren,IsReviews,CardSysId,PaymentNumber,CardNo,HasReturn from Shop_Orders ");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt)
			};
            parameters[0].Value = OrderId;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// HasReturn=1 表示退货   更新订单是否已经退货
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool OrderReturnUpdate(long orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Orders set ");
            strSql.Append("HasReturn=1,RefundStatus = 0 ");
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = { new SqlParameter("@OrderId", SqlDbType.BigInt, 8) };
            parameters[0].Value = orderId;
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

        #endregion  ExtensionMethod
    }
}

