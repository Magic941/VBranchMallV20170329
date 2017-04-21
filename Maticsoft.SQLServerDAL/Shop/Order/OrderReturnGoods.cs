using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Order;
using Maticsoft.DBUtility;
using Maticsoft.Model.Shop.Order;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Order
{
    /// <summary>
    /// 数据访问类:OrderReturnGoods
    /// </summary>
    public partial class OrderReturnGoods : IOrderReturnGoods
    {
        public OrderReturnGoods()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_OrderReturnGoods");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        //public long Add(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("insert into Shop_OrderReturnGoods(");
        //    strSql.Append("OrderId, UserId, ReturnAmounts, ReturnTime, ReturnReason, ReturnDescription, ReturnAddress, ReturnPostCode, ReturnTelphone, ReturnContacts, Attachment, ApproveStatus, ApprovePeason, ApproveTime, ApproveRemark, AccountStatus, AccountTime, AccountPeason, IsDeleted, Information, ExpressNO, AmountActual, ReturnRemark, ReturnOrderCode, OrderCode, SupplierId, SupplierName, CouponCode, CouponName, CouponAmount, CouponValueType, CouponValue, ReturnGoodsType, ReturnCoupon, ActualSalesTotal, AmountAdjusted, Amount, ServiceType, ReturnType, PickRegionId, PickRegion, PickAddress, PickZipCode, PickName, PickTelPhone, PickCellPhone, PickEmail, ReturnTrueName, ReturnBankName, ReturnCard, ReturnCardType, ContactName, ContactPhone, Status, RefundStatus, LogisticStatus, CustomerReview, RefuseReason)");
        //    strSql.Append(" values (");
        //    strSql.Append("@OrderId, @UserId, @ReturnAmounts, @ReturnTime, @ReturnReason, @ReturnDescription, @ReturnAddress, @ReturnPostCode, @ReturnTelphone, @ReturnContacts, @Attachment, @ApproveStatus, @ApprovePeason, @ApproveTime, @ApproveRemark, @AccountStatus, @AccountTime, @AccountPeason, @IsDeleted, @Information, @ExpressNO, @AmountActual,@ReturnRemark, @ReturnOrderCode, @OrderCode, @SupplierId, @SupplierName, @CouponCode, @CouponName, @CouponAmount, @CouponValueType, @CouponValue, @ReturnGoodsType, @ReturnCoupon, @ActualSalesTotal, @AmountAdjusted, @Amount, @ServiceType, @ReturnType, @PickRegionId, @PickRegion, @PickAddress, @PickZipCode, @PickName, @PickTelPhone, @PickCellPhone, @PickEmail,@ReturnTrueName, @ReturnBankName, @ReturnCard, @ReturnCardType, @ContactName, @ContactPhone, @Status, @RefundStatus, @LogisticStatus, @CustomerReview, @RefuseReason)");
        //    strSql.Append(";select @@IDENTITY");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@OrderId", SqlDbType.BigInt,8),
        //            new SqlParameter("@UserId", SqlDbType.BigInt,8),
        //            new SqlParameter("@ReturnAmounts", SqlDbType.Money,8),
        //            new SqlParameter("@ReturnTime", SqlDbType.DateTime),
        //            new SqlParameter("@ReturnReason", SqlDbType.Text),
        //            new SqlParameter("@ReturnDescription", SqlDbType.Text),
        //            new SqlParameter("@ReturnAddress", SqlDbType.VarChar,500),
        //            new SqlParameter("@ReturnPostCode", SqlDbType.VarChar,20),
        //            new SqlParameter("@ReturnTelphone", SqlDbType.VarChar,50),
        //            new SqlParameter("@ReturnContacts", SqlDbType.VarChar,50),
        //            new SqlParameter("@Attachment", SqlDbType.Text),
        //            new SqlParameter("@ApproveStatus", SqlDbType.SmallInt,2),
        //            new SqlParameter("@ApprovePeason", SqlDbType.VarChar,50),
        //            new SqlParameter("@ApproveTime", SqlDbType.DateTime),
        //            new SqlParameter("@ApproveRemark", SqlDbType.Text),
        //            new SqlParameter("@AccountStatus", SqlDbType.SmallInt,2),
        //            new SqlParameter("@AccountTime", SqlDbType.DateTime),
        //            new SqlParameter("@AccountPeason", SqlDbType.VarChar,50),
        //            new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
        //            new SqlParameter("@ReturnOrderCode",SqlDbType.NVarChar,50),
        //            new SqlParameter("@ReturnGoodsType",SqlDbType.Int,8)
        //                                };
        //    parameters[0].Value = model.OrderId;
        //    parameters[1].Value = model.UserId;
        //    parameters[2].Value = model.ReturnAmounts;
        //    parameters[3].Value = model.ReturnTime;
        //    parameters[4].Value = model.ReturnReason;
        //    parameters[5].Value = model.ReturnDescription;
        //    parameters[6].Value = model.ReturnAddress;
        //    parameters[7].Value = model.ReturnPostCode;
        //    parameters[8].Value = model.ReturnTelphone;
        //    parameters[9].Value = model.ReturnContacts;
        //    parameters[10].Value = model.Attachment;
        //    parameters[11].Value = model.ApproveStatus;
        //    parameters[12].Value = model.ApprovePeason;
        //    parameters[13].Value = model.ApproveTime;
        //    parameters[14].Value = model.ApproveRemark;
        //    parameters[15].Value = model.AccountStatus;
        //    parameters[16].Value = model.AccountTime;
        //    parameters[17].Value = model.AccountPeason;
        //    parameters[18].Value = model.IsDeleted;
        //    parameters[19].Value = model.ReturnOrderCode;
        //    parameters[20].Value = model.ReturnGoodsType;

        //    object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
        //    if (obj == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return Convert.ToInt64(obj);
        //    }
        //}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_OrderReturnGoods(");
            strSql.Append("OrderId,UserId,ReturnAmounts,ReturnTime,ReturnReason,ReturnDescription,ReturnAddress,ReturnPostCode,ReturnTelphone,ReturnContacts,Attachment,ApproveStatus,ApprovePeason,ApproveTime,ApproveRemark,AccountStatus,AccountTime,AccountPeason,IsDeleted,Information,ExpressNO,AmountActual,ReturnRemark,ReturnOrderCode,OrderCode,SupplierId,SupplierName,CouponCode,CouponName,CouponAmount,CouponValueType,CouponValue,ReturnGoodsType,ReturnCoupon,ActualSalesTotal,AmountAdjusted,Amount,ServiceType,ReturnType,PickRegionId,PickRegion,PickAddress,PickZipCode,PickName,PickTelPhone,PickCellPhone,PickEmail,ReturnTrueName,ReturnBankName,ReturnCard,ReturnCardType,ContactName,ContactPhone,Status,RefundStatus,LogisticStatus,CustomerReview,RefuseReason,Refuseremark,Repairremark,Adjustableremark)");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@UserId,@ReturnAmounts,@ReturnTime,@ReturnReason,@ReturnDescription,@ReturnAddress,@ReturnPostCode,@ReturnTelphone,@ReturnContacts,@Attachment,@ApproveStatus,@ApprovePeason,@ApproveTime,@ApproveRemark,@AccountStatus,@AccountTime,@AccountPeason,@IsDeleted,@Information,@ExpressNO,@AmountActual,@ReturnRemark,@ReturnOrderCode,@OrderCode,@SupplierId,@SupplierName,@CouponCode,@CouponName,@CouponAmount,@CouponValueType,@CouponValue,@ReturnGoodsType,@ReturnCoupon,@ActualSalesTotal,@AmountAdjusted,@Amount,@ServiceType,@ReturnType,@PickRegionId,@PickRegion,@PickAddress,@PickZipCode,@PickName,@PickTelPhone,@PickCellPhone,@PickEmail,@ReturnTrueName,@ReturnBankName,@ReturnCard,@ReturnCardType,@ContactName,@ContactPhone,@Status,@RefundStatus,@LogisticStatus,@CustomerReview,@RefuseReason,@Refuseremark,@Repairremark,@Adjustableremark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@UserId", SqlDbType.BigInt,8),
					new SqlParameter("@ReturnAmounts", SqlDbType.Money,8),
					new SqlParameter("@ReturnTime", SqlDbType.DateTime),
					new SqlParameter("@ReturnReason", SqlDbType.NVarChar,2000),
					new SqlParameter("@ReturnDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@ReturnAddress", SqlDbType.VarChar,500),
					new SqlParameter("@ReturnPostCode", SqlDbType.VarChar,20),
					new SqlParameter("@ReturnTelphone", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnContacts", SqlDbType.VarChar,50),
					new SqlParameter("@Attachment", SqlDbType.Text),
					new SqlParameter("@ApproveStatus", SqlDbType.SmallInt,2),
					new SqlParameter("@ApprovePeason", SqlDbType.VarChar,50),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
					new SqlParameter("@ApproveRemark", SqlDbType.Text),
					new SqlParameter("@AccountStatus", SqlDbType.SmallInt,2),
					new SqlParameter("@AccountTime", SqlDbType.DateTime),
					new SqlParameter("@AccountPeason", SqlDbType.VarChar,50),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@Information", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpressNO", SqlDbType.NVarChar,50),
					new SqlParameter("@AmountActual", SqlDbType.Money,8),
					new SqlParameter("@ReturnRemark", SqlDbType.NVarChar,500),
					new SqlParameter("@ReturnOrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,150),
					new SqlParameter("@CouponCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CouponName", SqlDbType.NVarChar,150),
					new SqlParameter("@CouponAmount", SqlDbType.Money,8),
					new SqlParameter("@CouponValueType", SqlDbType.Int,4),
					new SqlParameter("@CouponValue", SqlDbType.Money,8),
					new SqlParameter("@ReturnGoodsType", SqlDbType.SmallInt,2),
					new SqlParameter("@ReturnCoupon", SqlDbType.SmallInt,2),
					new SqlParameter("@ActualSalesTotal", SqlDbType.Money,8),
					new SqlParameter("@AmountAdjusted", SqlDbType.Money,8),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@ServiceType", SqlDbType.SmallInt,2),
					new SqlParameter("@ReturnType", SqlDbType.SmallInt,2),
					new SqlParameter("@PickRegionId", SqlDbType.Int,4),
					new SqlParameter("@PickRegion", SqlDbType.NVarChar,250),
					new SqlParameter("@PickAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@PickZipCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PickName", SqlDbType.NVarChar,20),
					new SqlParameter("@PickTelPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@PickCellPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@PickEmail", SqlDbType.NVarChar,100),
					new SqlParameter("@ReturnTrueName", SqlDbType.NVarChar,20),
					new SqlParameter("@ReturnBankName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnCard", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnCardType", SqlDbType.SmallInt,2),
					new SqlParameter("@ContactName", SqlDbType.NVarChar,80),
					new SqlParameter("@ContactPhone", SqlDbType.NVarChar,20),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@RefundStatus", SqlDbType.SmallInt,2),
					new SqlParameter("@LogisticStatus", SqlDbType.SmallInt,2),
					new SqlParameter("@CustomerReview", SqlDbType.SmallInt,2),
					new SqlParameter("@RefuseReason", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Refuseremark",SqlDbType.NVarChar,200),
                    new SqlParameter("@Repairremark",SqlDbType.NVarChar,200),
                    new SqlParameter("@Adjustableremark",SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ReturnAmounts;
            parameters[3].Value = model.ReturnTime;
            parameters[4].Value = model.ReturnReason;
            parameters[5].Value = model.ReturnDescription;
            parameters[6].Value = model.ReturnAddress;
            parameters[7].Value = model.ReturnPostCode;
            parameters[8].Value = model.ReturnTelphone;
            parameters[9].Value = model.ReturnContacts;
            parameters[10].Value = model.Attachment;
            parameters[11].Value = model.ApproveStatus;
            parameters[12].Value = model.ApprovePeason;
            parameters[13].Value = model.ApproveTime;
            parameters[14].Value = model.ApproveRemark;
            parameters[15].Value = model.AccountStatus;
            parameters[16].Value = model.AccountTime;
            parameters[17].Value = model.AccountPeason;
            parameters[18].Value = model.IsDeleted;
            parameters[19].Value = model.Information;
            parameters[20].Value = model.ExpressNO;
            parameters[21].Value = model.AmountActual;
            parameters[22].Value = model.ReturnRemark;
            parameters[23].Value = model.ReturnOrderCode;
            parameters[24].Value = model.OrderCode;
            parameters[25].Value = model.SupplierId;
            parameters[26].Value = model.SupplierName;
            parameters[27].Value = model.CouponCode;
            parameters[28].Value = model.CouponName;
            parameters[29].Value = model.CouponAmount;
            parameters[30].Value = model.CouponValueType;
            parameters[31].Value = model.CouponValue;
            parameters[32].Value = model.ReturnGoodsType;
            parameters[33].Value = model.ReturnCoupon;
            parameters[34].Value = model.ActualSalesTotal;
            parameters[35].Value = model.AmountAdjusted;
            parameters[36].Value = model.Amount;
            parameters[37].Value = model.ServiceType;
            parameters[38].Value = model.ReturnType;
            parameters[39].Value = model.PickRegionId;
            parameters[40].Value = model.PickRegion;
            parameters[41].Value = model.PickAddress;
            parameters[42].Value = model.PickZipCode;
            parameters[43].Value = model.PickName;
            parameters[44].Value = model.PickTelPhone;
            parameters[45].Value = model.PickCellPhone;
            parameters[46].Value = model.PickEmail;
            parameters[47].Value = model.ReturnTrueName;
            parameters[48].Value = model.ReturnBankName;
            parameters[49].Value = model.ReturnCard;
            parameters[50].Value = model.ReturnCardType;
            parameters[51].Value = model.ContactName;
            parameters[52].Value = model.ContactPhone;
            parameters[53].Value = model.Status;
            parameters[54].Value = model.RefundStatus;
            parameters[55].Value = model.LogisticStatus;
            parameters[56].Value = model.CustomerReview;
            parameters[57].Value = model.RefuseReason;
            parameters[58].Value = model.Refuseremark;
            parameters[59].Value = model.Repairremark;
            parameters[60].Value = model.Adjustableremark;

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
        public bool Update(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoods set ");
            strSql.Append("OrderId=@OrderId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("ReturnAmounts=@ReturnAmounts,");
            strSql.Append("ReturnTime=@ReturnTime,");
            strSql.Append("ReturnReason=@ReturnReason,");
            strSql.Append("ReturnDescription=@ReturnDescription,");
            strSql.Append("ReturnAddress=@ReturnAddress,");
            strSql.Append("ReturnPostCode=@ReturnPostCode,");
            strSql.Append("ReturnTelphone=@ReturnTelphone,");
            strSql.Append("ReturnContacts=@ReturnContacts,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("ApproveStatus=@ApproveStatus,");
            strSql.Append("ApprovePeason=@ApprovePeason,");
            strSql.Append("ApproveTime=@ApproveTime,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("AccountStatus=@AccountStatus,");
            strSql.Append("AccountTime=@AccountTime,");
            strSql.Append("AccountPeason=@AccountPeason,");
            strSql.Append("IsDeleted=@IsDeleted");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@UserId", SqlDbType.BigInt,8),
					new SqlParameter("@ReturnAmounts", SqlDbType.Money,8),
					new SqlParameter("@ReturnTime", SqlDbType.DateTime),
					new SqlParameter("@ReturnReason", SqlDbType.Text),
					new SqlParameter("@ReturnDescription", SqlDbType.Text),
					new SqlParameter("@ReturnAddress", SqlDbType.VarChar,500),
					new SqlParameter("@ReturnPostCode", SqlDbType.VarChar,20),
					new SqlParameter("@ReturnTelphone", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnContacts", SqlDbType.VarChar,50),
					new SqlParameter("@Attachment", SqlDbType.Text),
					new SqlParameter("@ApproveStatus", SqlDbType.SmallInt,2),
					new SqlParameter("@ApprovePeason", SqlDbType.VarChar,50),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
					new SqlParameter("@ApproveRemark", SqlDbType.Text),
					new SqlParameter("@AccountStatus", SqlDbType.SmallInt,2),
					new SqlParameter("@AccountTime", SqlDbType.DateTime),
					new SqlParameter("@AccountPeason", SqlDbType.VarChar,50),
					new SqlParameter("@IsDeleted", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
            parameters[0].Value = model.OrderId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ReturnAmounts;
            parameters[3].Value = model.ReturnTime;
            parameters[4].Value = model.ReturnReason;
            parameters[5].Value = model.ReturnDescription;
            parameters[6].Value = model.ReturnAddress;
            parameters[7].Value = model.ReturnPostCode;
            parameters[8].Value = model.ReturnTelphone;
            parameters[9].Value = model.ReturnContacts;
            parameters[10].Value = model.Attachment;
            parameters[11].Value = model.ApproveStatus;
            parameters[12].Value = model.ApprovePeason;
            parameters[13].Value = model.ApproveTime;
            parameters[14].Value = model.ApproveRemark;
            parameters[15].Value = model.AccountStatus;
            parameters[16].Value = model.AccountTime;
            parameters[17].Value = model.AccountPeason;
            parameters[18].Value = model.IsDeleted;
            parameters[19].Value = model.Id;

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
        /// 审核通过改变退货状态 返回卖家收货信息(oft)
        /// </summary>
        public bool Detail(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoods set ");
            strSql.Append("ReturnRemark=@ReturnRemark,");
            strSql.Append("ReturnAddress=@ReturnAddress,");
            strSql.Append("Status=@Status,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("ApprovePeason=@ApprovePeason,");
            strSql.Append("ApproveTime=@ApproveTime,");
            strSql.Append("LogisticStatus=@LogisticStatus,");
            strSql.Append("PickName=@PickName,");
            strSql.Append("PickRegionId=@PickRegionId,");
            strSql.Append("PickZipCode=@PickZipCode,");
            strSql.Append("PickCellPhone=@PickCellPhone,");
            strSql.Append("ReturnTelphone=@ReturnTelphone,");
            strSql.Append("PickEmail=@PickEmail,");
            strSql.Append("AmountActual=@AmountActual");
            //strSql.Append("ReturnGoodsType=@ReturnGoodsType");
            strSql.Append(" where Id=@Id");

            SqlParameter[] parameters = {
					new SqlParameter("@ReturnRemark", SqlDbType.VarChar,50),
					new SqlParameter("@ReturnAddress", SqlDbType.VarChar,50),
					new SqlParameter("@Status", SqlDbType.SmallInt,8),
					new SqlParameter("@ApproveRemark", SqlDbType.VarChar,50),
					new SqlParameter("@ApprovePeason", SqlDbType.VarChar,50),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
                    new SqlParameter("@LogisticStatus", SqlDbType.SmallInt,8),
                    new SqlParameter("@PickName", SqlDbType.VarChar,50),
                    new SqlParameter("@PickRegionId", SqlDbType.SmallInt,8),
                    new SqlParameter("@PickZipCode", SqlDbType.VarChar,50),
                    new SqlParameter("@PickCellPhone", SqlDbType.VarChar,50),
                    new SqlParameter("@ReturnTelphone", SqlDbType.VarChar,50),
                    new SqlParameter("@PickEmail", SqlDbType.VarChar,50),
                    new SqlParameter("@AmountActual",SqlDbType.Decimal), 
                    //new SqlParameter("@ReturnGoodsType",SqlDbType.Int), 
					new SqlParameter("@Id", SqlDbType.BigInt,8)
                    };
            parameters[0].Value = model.ReturnRemark;
            parameters[1].Value = model.ReturnAddress != null ? model.ReturnAddress :"";
            parameters[2].Value = model.Status;
            parameters[3].Value = model.ApproveRemark;
            parameters[4].Value = model.ApprovePeason;
            parameters[5].Value = model.ApproveTime;
            parameters[6].Value = model.LogisticStatus.HasValue ? model.LogisticStatus.Value : 0;
            parameters[7].Value = model.PickName;
            parameters[8].Value = model.PickRegionId;
            parameters[9].Value = model.PickZipCode;
            parameters[10].Value = model.PickCellPhone;
            parameters[11].Value = model.ReturnTelphone;
            parameters[12].Value = model.PickEmail;
            parameters[13].Value = model.AmountActual;
            //parameters[14].Value = model.ReturnGoodsType;
            parameters[14].Value = model.Id;

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
        /// 审核不通过改变退货状态
        /// </summary>
        public bool Detail_Update(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoods set ");
            strSql.Append("ReturnRemark=@ReturnRemark,");
            strSql.Append("RefuseReason=@RefuseReason,");
            strSql.Append("Status=@Status,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("ApprovePeason=@ApprovePeason,");
            strSql.Append("ApproveTime=@ApproveTime");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@ReturnRemark", SqlDbType.VarChar,50),
					new SqlParameter("@RefuseReason", SqlDbType.VarChar,50),
					new SqlParameter("@Status", SqlDbType.SmallInt,8),
					new SqlParameter("@ApproveRemark", SqlDbType.VarChar,50),
					new SqlParameter("@ApprovePeason", SqlDbType.VarChar,50),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
            parameters[0].Value = model.ReturnRemark;
            parameters[1].Value = model.RefuseReason;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.ApproveRemark;
            parameters[4].Value = model.ApprovePeason;
            parameters[5].Value = model.ApproveTime;
            parameters[6].Value = model.Id;

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

        //退款
        public bool UpdatePrice(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoods set ");
            strSql.Append("RefundStatus=@RefundStatus,");
            strSql.Append("AccountPeason=@AccountPeason,");
            strSql.Append("AccountTime=@AccountTime,");
            strSql.Append("AmountActual=@AmountActual,");
            strSql.Append("ApproveRemark=@ApproveRemark,");
            strSql.Append("Status=@Status ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@RefundStatus", SqlDbType.SmallInt,50),
					new SqlParameter("@AccountPeason", SqlDbType.VarChar,50),
					new SqlParameter("@AccountTime", SqlDbType.DateTime),
					new SqlParameter("@AmountActual", SqlDbType.Money),
                    new SqlParameter("ApproveRemark",SqlDbType.VarChar,50),
                    new SqlParameter("Status",SqlDbType.Int,4),
                    new SqlParameter("@Id",SqlDbType.SmallInt,8)
                                        };
            parameters[0].Value = model.RefundStatus;
            parameters[1].Value = model.AccountPeason;
            parameters[2].Value = model.AccountTime;
            parameters[3].Value = model.AmountActual;
            parameters[4].Value = model.ApproveRemark;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.Id;

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

        //审核通过修改物流信息和快递单号  更改状态未已发货
        public bool UpdateInformation(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoods set ");
            strSql.Append("Information=@Information,");
            strSql.Append("ExpressNO=@ExpressNO,");
            strSql.Append("Status=@Status,");
            strSql.Append("LogisticStatus=@LogisticStatus,");
            strSql.Append("RefundStatus=@RefundStatus");
            strSql.Append("");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                        new SqlParameter("@Information",SqlDbType.VarChar,500),
                        new SqlParameter("@ExpressNO",SqlDbType.VarChar,500),
                        new SqlParameter("@Status",SqlDbType.BigInt,8),
                        new SqlParameter("@LogisticStatus",SqlDbType.BigInt,8),
                        new SqlParameter("@RefundStatus",SqlDbType.BigInt,8),
                        new SqlParameter("@Id",SqlDbType.Int,8)
                                         };
            parameters[0].Value = model.Information;
            parameters[1].Value = model.ExpressNO;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.LogisticStatus;
            parameters[4].Value = model.RefundStatus;
            parameters[5].Value = model.Id;

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
        public bool Delete(long Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderReturnGoods ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
			};
            parameters[0].Value = Id;

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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_OrderReturnGoods ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
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
        public Maticsoft.Model.Shop.Order.OrderReturnGoods GetModel(long Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id, OrderId, UserId, ReturnAmounts, ReturnTime, ReturnReason, ReturnDescription, ReturnAddress, ReturnPostCode, ReturnTelphone, ReturnContacts, Attachment, ApproveStatus, ApprovePeason, ApproveTime, ApproveRemark, AccountStatus, AccountTime, AccountPeason, IsDeleted, Information, ExpressNO, AmountActual, ReturnRemark, ReturnOrderCode, OrderCode, SupplierId, SupplierName, CouponCode, CouponName, CouponAmount, CouponValueType, CouponValue, ReturnGoodsType, ReturnCoupon, ActualSalesTotal, AmountAdjusted, Amount, ServiceType, ReturnType, PickRegionId, PickRegion, PickAddress, PickZipCode, PickName, PickTelPhone, PickCellPhone, PickEmail, ReturnTrueName, ReturnBankName, ReturnCard, ReturnCardType, ContactName, ContactPhone, Status, RefundStatus, LogisticStatus, CustomerReview, RefuseReason,Refuseremark,Repairremark,Adjustableremark from Shop_OrderReturnGoods ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt)
			};
            parameters[0].Value = Id;

            Maticsoft.Model.Shop.Order.OrderReturnGoods model = new Maticsoft.Model.Shop.Order.OrderReturnGoods();
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
        public Maticsoft.Model.Shop.Order.OrderReturnGoods DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderReturnGoods model = new Maticsoft.Model.Shop.Order.OrderReturnGoods();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = long.Parse(row["Id"].ToString());
                }
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = long.Parse(row["OrderId"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = long.Parse(row["UserId"].ToString());
                }
                if (row["ReturnAmounts"] != null && row["ReturnAmounts"].ToString() != "")
                {
                    model.ReturnAmounts = decimal.Parse(row["ReturnAmounts"].ToString());
                }
                if (row["ReturnTime"] != null && row["ReturnTime"].ToString() != "")
                {
                    model.ReturnTime = DateTime.Parse(row["ReturnTime"].ToString());
                }
                if (row["ReturnReason"] != null)
                {
                    model.ReturnReason = row["ReturnReason"].ToString();
                }
                if (row["ReturnDescription"] != null)
                {
                    model.ReturnDescription = row["ReturnDescription"].ToString();
                }
                if (row["ReturnAddress"] != null)
                {
                    model.ReturnAddress = row["ReturnAddress"].ToString();
                }
                if (row["ReturnPostCode"] != null)
                {
                    model.ReturnPostCode = row["ReturnPostCode"].ToString();
                }
                if (row["ReturnTelphone"] != null)
                {
                    model.ReturnTelphone = row["ReturnTelphone"].ToString();
                }
                if (row["ReturnContacts"] != null)
                {
                    model.ReturnContacts = row["ReturnContacts"].ToString();
                }
                if (row["Attachment"] != null)
                {
                    model.Attachment = row["Attachment"].ToString();
                }
                if (row["ApproveStatus"] != null && row["ApproveStatus"].ToString() != "")
                {
                    model.ApproveStatus = int.Parse(row["ApproveStatus"].ToString());
                }
                if (row["ApprovePeason"] != null)
                {
                    model.ApprovePeason = row["ApprovePeason"].ToString();
                }
                if (row["ApproveTime"] != null && row["ApproveTime"].ToString() != "")
                {
                    model.ApproveTime = DateTime.Parse(row["ApproveTime"].ToString());
                }
                if (row["ApproveRemark"] != null)
                {
                    model.ApproveRemark = row["ApproveRemark"].ToString();
                }
                if (row["AccountStatus"] != null && row["AccountStatus"].ToString() != "")
                {
                    model.AccountStatus = int.Parse(row["AccountStatus"].ToString());
                }
                if (row["AccountTime"] != null && row["AccountTime"].ToString() != "")
                {
                    model.AccountTime = DateTime.Parse(row["AccountTime"].ToString());
                }
                if (row["AccountPeason"] != null)
                {
                    model.AccountPeason = row["AccountPeason"].ToString();
                }
                if (row["IsDeleted"] != null && row["IsDeleted"].ToString() != "")
                {
                    if ((row["IsDeleted"].ToString() == "1") || (row["IsDeleted"].ToString().ToLower() == "true"))
                    {
                        model.IsDeleted = true;
                    }
                    else
                    {
                        model.IsDeleted = false;
                    }
                }
                if (row["Information"] != null)
                {
                    model.Information = row["Information"].ToString();
                }
                if (row["ExpressNO"] != null)
                {
                    model.ExpressNO = row["ExpressNO"].ToString();
                }
                if (row["AmountActual"] != null && row["AmountActual"].ToString() != "")
                {
                    model.AmountActual = decimal.Parse(row["AmountActual"].ToString());
                }
                if (row["ReturnRemark"] != null)
                {
                    model.ReturnRemark = row["ReturnRemark"].ToString();
                }
                if (row["ReturnOrderCode"] != null)
                {
                    model.ReturnOrderCode = row["ReturnOrderCode"].ToString();
                }
                if (row["OrderCode"] != null)
                {
                    model.OrderCode = row["OrderCode"].ToString();
                }
                if (row["SupplierId"] != null && row["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
                if (row["SupplierName"] != null)
                {
                    model.SupplierName = row["SupplierName"].ToString();
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
                if (row["CouponValueType"] != null && row["CouponValueType"].ToString() != "")
                {
                    model.CouponValueType = int.Parse(row["CouponValueType"].ToString());
                }
                if (row["CouponValue"] != null && row["CouponValue"].ToString() != "")
                {
                    model.CouponValue = decimal.Parse(row["CouponValue"].ToString());
                }
                if (row.Table.Columns.Contains("ReturnGoodsType"))
                {
                    int returnGoodsType = 0;
                    int.TryParse(row["ReturnGoodsType"].ToString(), out returnGoodsType);
                    model.ReturnGoodsType = returnGoodsType;
                }
                if (row["ReturnCoupon"] != null && row["ReturnCoupon"].ToString() != "")
                {
                    model.ReturnCoupon = int.Parse(row["ReturnCoupon"].ToString());
                }
                if (row["ActualSalesTotal"] != null && row["ActualSalesTotal"].ToString() != "")
                {
                    model.ActualSalesTotal = decimal.Parse(row["ActualSalesTotal"].ToString());
                }
                if (row["AmountAdjusted"] != null && row["AmountAdjusted"].ToString() != "")
                {
                    model.AmountAdjusted = decimal.Parse(row["AmountAdjusted"].ToString());
                }
                if (row["Amount"] != null && row["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if (row["ServiceType"] != null && row["ServiceType"].ToString() != "")
                {
                    model.ServiceType = int.Parse(row["ServiceType"].ToString());
                }
                if (row["ReturnType"] != null && row["ReturnType"].ToString() != "")
                {
                    model.ReturnType = int.Parse(row["ReturnType"].ToString());
                }
                if (row["PickRegionId"] != null && row["PickRegionId"].ToString() != "")
                {
                    model.PickRegionId = int.Parse(row["PickRegionId"].ToString());
                }
                if (row["PickRegion"] != null)
                {
                    model.PickRegion = row["PickRegion"].ToString();
                }
                if (row["PickAddress"] != null)
                {
                    model.PickAddress = row["PickAddress"].ToString();
                }
                if (row["PickZipCode"] != null)
                {
                    model.PickZipCode = row["PickZipCode"].ToString();
                }
                if (row["PickName"] != null)
                {
                    model.PickName = row["PickName"].ToString();
                }
                if (row["PickTelPhone"] != null)
                {
                    model.PickTelPhone = row["PickTelPhone"].ToString();
                }
                if (row["PickCellPhone"] != null)
                {
                    model.PickCellPhone = row["PickCellPhone"].ToString();
                }
                if (row["PickEmail"] != null)
                {
                    model.PickEmail = row["PickEmail"].ToString();
                }
                if (row["ReturnTrueName"] != null)
                {
                    model.ReturnTrueName = row["ReturnTrueName"].ToString();
                }
                if (row["ReturnBankName"] != null)
                {
                    model.ReturnBankName = row["ReturnBankName"].ToString();
                }
                if (row["ReturnCard"] != null)
                {
                    model.ReturnCard = row["ReturnCard"].ToString();
                }
                if (row["ReturnCardType"] != null && row["ReturnCardType"].ToString() != "")
                {
                    model.ReturnCardType = int.Parse(row["ReturnCardType"].ToString());
                }
                if (row["ContactName"] != null)
                {
                    model.ContactName = row["ContactName"].ToString();
                }
                if (row["ContactPhone"] != null)
                {
                    model.ContactPhone = row["ContactPhone"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["RefundStatus"] != null && row["RefundStatus"].ToString() != "")
                {
                    model.RefundStatus = int.Parse(row["RefundStatus"].ToString());
                }
                if (row["LogisticStatus"] != null && row["LogisticStatus"].ToString() != "")
                {
                    model.LogisticStatus = int.Parse(row["LogisticStatus"].ToString());
                }
                if (row["CustomerReview"] != null && row["CustomerReview"].ToString() != "")
                {
                    model.CustomerReview = int.Parse(row["CustomerReview"].ToString());
                }
                if (row["RefuseReason"] != null)
                {
                    model.RefuseReason = row["RefuseReason"].ToString();
                }
                if (row["Refuseremark"] != null && row["Refuseremark"].ToString() != "")
                {
                    model.Refuseremark = row["Refuseremark"].ToString();
                }
                if (row["Repairremark"] != null && row["Repairremark"].ToString() != "")
                {
                    model.Repairremark = row["Repairremark"].ToString();
                }
                if (row["Adjustableremark"] != null && row["Adjustableremark"].ToString() != "")
                {
                    model.Adjustableremark = row["Adjustableremark"].ToString();
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
            strSql.Append("select Id,OrderId,UserId,ReturnAmounts,ReturnTime,ReturnReason,ReturnDescription,ReturnAddress,ReturnPostCode,ReturnTelphone,ReturnContacts,Attachment,ApproveStatus,ApprovePeason,ApproveTime,ApproveRemark,AccountStatus,AccountTime,AccountPeason,IsDeleted,Information,ExpressNO,AmountActual,ReturnRemark,ReturnOrderCode,OrderCode,SupplierId,SupplierName,CouponCode,CouponName,CouponAmount,CouponValueType,CouponValue,ReturnGoodsType,ReturnCoupon,ActualSalesTotal,AmountAdjusted,Amount,ServiceType,ReturnType,PickRegionId,PickRegion,PickAddress,PickZipCode,PickName,PickTelPhone,PickCellPhone,PickEmail,ReturnTrueName,ReturnBankName,ReturnCard,ReturnCardType,ContactName,ContactPhone,Status,RefundStatus,LogisticStatus,CustomerReview,RefuseReason,Refuseremark,Repairremark,Adjustableremark ");
            strSql.Append(" FROM Shop_OrderReturnGoods ");
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
            strSql.Append(" Id,OrderId,UserId,ReturnAmounts,ReturnTime,ReturnReason,ReturnDescription,ReturnAddress,ReturnPostCode,ReturnTelphone,ReturnContacts,Attachment,ApproveStatus,ApprovePeason,ApproveTime,ApproveRemark,AccountStatus,AccountTime,AccountPeason,IsDeleted,Information,ExpressNO,AmountActual,ReturnRemark,ReturnOrderCode,OrderCode,SupplierId,SupplierName,CouponCode,CouponName,CouponAmount,CouponValueType,CouponValue,ReturnGoodsType,ReturnCoupon,ActualSalesTotal,AmountAdjusted,Amount,ServiceType,ReturnType,PickRegionId,PickRegion,PickAddress,PickZipCode,PickName,PickTelPhone,PickCellPhone,PickEmail,ReturnTrueName,ReturnBankName,ReturnCard,ReturnCardType,ContactName,ContactPhone,Status,RefundStatus,LogisticStatus,CustomerReview,RefuseReason,Refuseremark ,Repairremark,Adjustableremark");
            strSql.Append(" FROM Shop_OrderReturnGoods ");
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
            strSql.Append("select count(1) FROM Shop_OrderReturnGoods ");
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
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_OrderReturnGoods T ");
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
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Shop_OrderReturnGoods";
            parameters[1].Value = "Id";
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
        /// 审核操作
        /// </summary>
        /// <param name="returnApproveStatus"></param>
        public int ApproveReturnOrder(int Status, long returnId, string peason, string remark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoods set ");
            strSql.Append("Status=@Status,");
            strSql.Append("ApprovePeason=@ApprovePeason,");
            strSql.Append("ApproveTime=@ApproveTime,");
            strSql.Append("ApproveRemark=@ApproveRemark ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {					 
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@ApprovePeason", SqlDbType.VarChar,50),
					new SqlParameter("@ApproveTime", SqlDbType.DateTime),
					new SqlParameter("@ApproveRemark", SqlDbType.Text),	 
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
            parameters[0].Value = Status;
            parameters[1].Value = peason;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = remark;
            parameters[4].Value = returnId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 退款操作
        /// </summary>
        public int ReturnAccount(int status, long returnId, string accountPeason, string remark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoods set ");
            strSql.Append("RefundStatus=@RefundStatus,");
            strSql.Append("AccountTime=@AccountTime,");
            strSql.Append("AccountPeason=@AccountPeason,");
            strSql.Append("ApproveRemark=@ApproveRemark ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {					 
					new SqlParameter("@RefundStatus", SqlDbType.SmallInt,2),
					new SqlParameter("@AccountTime", SqlDbType.DateTime),
					new SqlParameter("@AccountPeason", SqlDbType.VarChar,50),
                    new SqlParameter("@ApproveRemark", SqlDbType.Text),	 
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
            parameters[0].Value = status;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = accountPeason;
            parameters[3].Value = remark;
            parameters[4].Value = returnId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }
        #endregion  ExtensionMethod

        /// <summary>
        /// 拒收退货 返回拒收理由 ，修改退货状态为 拒绝收货
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool RefuseReturnGoods(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_OrderReturnGoods set ");
            strSql.Append("Refuseremark= @Refuseremark,");
            strSql.Append("LogisticStatus =@LogisticStatus ");
            strSql.Append(" Where Id = @Id");
            SqlParameter[] parameters = {
                       new SqlParameter("@Refuseremark", SqlDbType.NVarChar,200),
                       new SqlParameter("@LogisticStatus",SqlDbType.SmallInt,2),
                       new SqlParameter("@Id",SqlDbType.Int,8)
                                        };
            parameters[0].Value = model.Refuseremark;
            parameters[1].Value = model.LogisticStatus;
            parameters[2].Value = model.Id;

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
        ///维修
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool OrderReturnRepair(Maticsoft.Model.Shop.Order.OrderReturnGoods model)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("update Shop_OrderReturnGoods set ");
            strsql.Append("LogisticStatus=@LogisticStatus,");
            strsql.Append("Repairremark=@Repairremark,");
            strsql.Append("ApproveTime=@ApproveTime");
            strsql.Append(" where Id = @Id");
            SqlParameter[] parement ={
                        new SqlParameter("@LogisticStatus",SqlDbType.Int,8),
                        new SqlParameter("@Repairremark",SqlDbType.NVarChar,200),
                        new SqlParameter("@ApproveTime",SqlDbType.DateTime),
                        new SqlParameter("@Id",SqlDbType.Int,8)
                                    };
            parement[0].Value = model.LogisticStatus;
            parement[1].Value = model.Repairremark;
            parement[2].Value = DateTime.Now;
            parement[3].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strsql.ToString(), parement);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

