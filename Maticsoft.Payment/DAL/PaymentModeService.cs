/**
* PaymentModeService.cs
*
* 功 能： 支付模块DB数据交互类
* 类 名： PaymentModeService
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Maticsoft.Payment.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using GL.Payment;

namespace Maticsoft.Payment.DAL
{
    internal class PaymentModeService
    {
        private Database database = DatabaseFactory.CreateDatabase();

        #region Payment
        public PaymentModeActionStatus CreateUpdateDeletePaymentMode(PaymentModeInfo paymentMode, DataProviderAction action)
        {
            if (paymentMode == null)
            {
                return PaymentModeActionStatus.UnknowError;
            }
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("sp_Pay_PaymentMode_CreateUpdateDelete");
            this.database.AddInParameter(storedProcCommand, "Action", DbType.Int32, (int)action);
            this.database.AddOutParameter(storedProcCommand, "Status", DbType.Int32, 4);
            if (action == DataProviderAction.Create)
            {
                this.database.AddOutParameter(storedProcCommand, "ModeId", DbType.Int32, 4);
            }
            else
            {
                this.database.AddInParameter(storedProcCommand, "ModeId", DbType.Int32, paymentMode.ModeId);
            }
            if (action != DataProviderAction.Delete)
            {
                this.database.AddInParameter(storedProcCommand, "MerchantCode", DbType.String, paymentMode.MerchantCode);
                this.database.AddInParameter(storedProcCommand, "EmailAddress", DbType.String, paymentMode.EmailAddress);
                this.database.AddInParameter(storedProcCommand, "SecretKey", DbType.String, paymentMode.SecretKey);
                this.database.AddInParameter(storedProcCommand, "SecondKey", DbType.String, paymentMode.SecondKey);
                this.database.AddInParameter(storedProcCommand, "Password", DbType.String, paymentMode.Password);
                this.database.AddInParameter(storedProcCommand, "Partner", DbType.String, paymentMode.Partner);
                this.database.AddInParameter(storedProcCommand, "Name", DbType.String, paymentMode.Name);
                this.database.AddInParameter(storedProcCommand, "Description", DbType.String, paymentMode.Description);
                this.database.AddInParameter(storedProcCommand, "AllowRecharge", DbType.Boolean, paymentMode.AllowRecharge);
                this.database.AddInParameter(storedProcCommand, "Gateway", DbType.String, paymentMode.Gateway);
                this.database.AddInParameter(storedProcCommand, "DrivePath", DbType.String, paymentMode.DrivePath);
                if (paymentMode.DisplaySequence > 0)
                {
                    this.database.AddInParameter(storedProcCommand, "DisplaySequence", DbType.Int32, paymentMode.DisplaySequence);
                }
                this.database.AddInParameter(storedProcCommand, "Charge", DbType.Currency, paymentMode.Charge);
                this.database.AddInParameter(storedProcCommand, "IsPercent", DbType.Boolean, paymentMode.IsPercent);
            }
            PaymentModeActionStatus unknowError = PaymentModeActionStatus.UnknowError;
            if ((action != DataProviderAction.Delete) && (paymentMode.SupportedCurrencys.Count > 0))
            {
                using (DbConnection connection = this.database.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        this.database.ExecuteNonQuery(storedProcCommand, transaction);
                        unknowError = (PaymentModeActionStatus)((int)this.database.GetParameterValue(storedProcCommand, "Status"));
                        int num = (action == DataProviderAction.Create) ? ((int)this.database.GetParameterValue(storedProcCommand, "ModeId")) : paymentMode.ModeId;
                        if (unknowError == PaymentModeActionStatus.Success)
                        {
                            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("  ");
                            this.database.AddInParameter(sqlStringCommand, "ModeId", DbType.Int32, num);
                            StringBuilder builder = new StringBuilder();
                            if (action == DataProviderAction.Update)
                            {
                                builder.Append("DELETE  From  Pay_PaymentCurrencys Where ModeId=@ModeId");
                            }
                            builder.Append(" DECLARE @intErrorCode INT;SET @intErrorCode = 0;");
                            int num2 = 0;
                            foreach (string str in paymentMode.SupportedCurrencys)
                            {
                                builder.Append("INSERT INTO Pay_PaymentCurrencys(ModeId,Code) Values(").Append("@ModeId").Append(",@Code").Append(num2).Append(");SET @intErrorCode = @intErrorCode + @@ERROR;");
                                this.database.AddInParameter(sqlStringCommand, "Code" + num2, DbType.String, str);
                                num2++;
                            }
                            sqlStringCommand.CommandText = builder.Append("SELECT @intErrorCode;").ToString();
                            if (((int)this.database.ExecuteScalar(sqlStringCommand, transaction)) == 0)
                            {
                                transaction.Commit();
                            }
                            else
                            {
                                transaction.Rollback();
                                unknowError = PaymentModeActionStatus.UnknowError;
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        unknowError = PaymentModeActionStatus.UnknowError;
                    }
                    connection.Close();
                    return unknowError;
                }
            }
            this.database.ExecuteNonQuery(storedProcCommand);
            return (PaymentModeActionStatus)((int)this.database.GetParameterValue(storedProcCommand, "Status"));
        }

        public void SortPaymentMode(int modeId, SortAction action)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("sp_Pay_PaymentMode_Sequence");
            this.database.AddInParameter(storedProcCommand, "ModeId", DbType.Int32, modeId);
            this.database.AddInParameter(storedProcCommand, "Sort", DbType.Int32, (int)action);
            this.database.ExecuteNonQuery(storedProcCommand);
        }

        public PaymentModeInfo GetPaymentMode(int modeId)
        {
            PaymentModeInfo info = new PaymentModeInfo();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Pay_PaymentTypes WHERE ModeId = @ModeId;SELECT Code FROM Pay_PaymentCurrencys WHERE ModeId = @ModeId");
            this.database.AddInParameter(sqlStringCommand, "ModeId", DbType.Int32, modeId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = this.PopupPayment(reader);
                }
                if (!reader.NextResult())
                {
                    return info;
                }
                while (reader.Read())
                {
                    info.SupportedCurrencys.Add(reader.GetString(0));
                }
            }
            return info;
        }

        public PaymentModeInfo GetPaymentMode(string gateway)
        {
            PaymentModeInfo info = null;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT top 1 * FROM Pay_PaymentTypes WHERE Gateway = @Gateway;SELECT Code FROM Pay_PaymentCurrencys WHERE ModeId = (SELECT top 1 ModeId FROM Pay_PaymentTypes WHERE Gateway = @Gateway)");
            this.database.AddInParameter(sqlStringCommand, "Gateway", DbType.String, gateway);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = this.PopupPayment(reader);
                }
                if (!reader.NextResult())
                {
                    return info;
                }
                while (reader.Read())
                {
                    info.SupportedCurrencys.Add(reader.GetString(0));
                }
            }
            return info;
        }

        public List<PaymentModeInfo> GetPaymentModes(int type)
        {
            List<PaymentModeInfo> list = new List<PaymentModeInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Pay_PaymentTypes WHERE Gateway <> 'adminaction'  ");
            if (type > 0)
            {
                strSql.AppendFormat(" And DrivePath like '%|{0}|%'", type);
            }
            strSql.Append("  Order by DisplaySequence");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    list.Add(this.PopupPayment(reader));
                }
            }
            return list;
        }

        #region PopupPayment
        public PaymentModeInfo PopupPayment(IDataRecord reader)
        {
            if (reader == null)
            {
                return null;
            }
            PaymentModeInfo info = new PaymentModeInfo
            {
                ModeId = (int)reader["ModeId"],
                Name = (string)reader["Name"],
                MerchantCode = (string)reader["MerchantCode"],
                DisplaySequence = (int)reader["DisplaySequence"],
                Charge = (decimal)reader["Charge"],
                IsPercent = (bool)reader["IsPercent"],
                AllowRecharge = (bool)reader["AllowRecharge"]
            };
            if (reader["EmailAddress"] != DBNull.Value)
            {
                info.EmailAddress = (string)reader["EmailAddress"];
            }
            if (reader["SecretKey"] != DBNull.Value)
            {
                info.SecretKey = (string)reader["SecretKey"];
            }
            if (reader["SecondKey"] != DBNull.Value)
            {
                info.SecondKey = (string)reader["SecondKey"];
            }
            if (reader["Password"] != DBNull.Value)
            {
                info.Password = (string)reader["Password"];
            }
            if (reader["Partner"] != DBNull.Value)
            {
                info.Partner = (string)reader["Partner"];
            }
            if (reader["Description"] != DBNull.Value)
            {
                info.Description = (string)reader["Description"];
            }
            if (reader["Gateway"] != DBNull.Value)
            {
                info.Gateway = (string)reader["Gateway"];
            }
            if (reader["Logo"] != DBNull.Value)
            {
                info.Logo = reader["Logo"].ToString();
            }

            if (reader["DrivePath"] != DBNull.Value)
            {
                info.DrivePath = reader["DrivePath"].ToString();
            }
            
            return info;
        }
        #endregion
        #endregion

        #region AccountSummary
        public AccountSummaryInfo GetAccountSummary(int userId)
        {
            AccountSummaryInfo info = null;
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("sp_Pay_AccountSummary_Get");
            this.database.AddInParameter(storedProcCommand, "UserId", DbType.Int32, userId);
            using (IDataReader reader = this.database.ExecuteReader(storedProcCommand))
            {
                while (reader.Read())
                {
                    info = this.PopupAccountSummary(reader);
                }
            }
            return info;
        }
        #region PopupAccountSummary
        public AccountSummaryInfo PopupAccountSummary(IDataRecord reader)
        {
            if (reader == null)
            {
                return null;
            }
            AccountSummaryInfo info = new AccountSummaryInfo();
            if (reader["AccountAmount"] != DBNull.Value)
            {
                info.AccountAmount = (decimal)reader["AccountAmount"];
            }
            if (reader["FreezeBalance"] != DBNull.Value)
            {
                info.FreezeBalance = (decimal)reader["FreezeBalance"];
            }
            info.UseableBalance = info.AccountAmount - info.FreezeBalance;
            return info;
        }
        #endregion
        #endregion

        #region Recharge
        public RechargeRequestInfo GetRechargeRequest(long rechargeId)
        {
            RechargeRequestInfo info = null;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Pay_RechargeRequest WHERE RechargeId = @RechargeId");
            this.database.AddInParameter(sqlStringCommand, "RechargeId", DbType.Int64, rechargeId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    info = this.PopulateRechargeRequest(reader);
                }
            }
            return info;
        }

        public long AddRechargeBlance(RechargeRequestInfo rechargeRequest)
        {
            if (rechargeRequest != null)
            {
                DbCommand storedProcCommand = this.database.GetStoredProcCommand("sp_cf_rechargeRequest_Create");
                this.database.AddOutParameter(storedProcCommand, "Status", DbType.Int32, 4);
                this.database.AddOutParameter(storedProcCommand, "RechargeId", DbType.Int64, 10);
                this.database.AddInParameter(storedProcCommand, "TradeDate", DbType.DateTime, rechargeRequest.TradeDate);
                this.database.AddInParameter(storedProcCommand, "RechargeBlance", DbType.Currency, rechargeRequest.RechargeBlance);
                this.database.AddInParameter(storedProcCommand, "UserId", DbType.Int32, rechargeRequest.UserId);
                this.database.AddInParameter(storedProcCommand, "PaymentGateway", DbType.String, rechargeRequest.PaymentGateway);
                this.database.ExecuteNonQuery(storedProcCommand);
                if (((int)this.database.GetParameterValue(storedProcCommand, "Status")) == 0)
                {
                    return (long)this.database.GetParameterValue(storedProcCommand, "RechargeId");
                }
            }
            return 0L;
        }

        public int RemoveRechargeRequest(long rechargeId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Pay_RechargeRequest WHERE rechargeId = @rechargeId");
            this.database.AddInParameter(sqlStringCommand, "rechargeId", DbType.Int64, rechargeId);
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool AddBalanceDetail(BalanceDetailInfo balanceDetails)
        {
            if (balanceDetails == null)
            {
                return false;
            }
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("sp_Pay_BalanceDetails_Create");
            this.database.AddOutParameter(storedProcCommand, "Status", DbType.Int32, 4);
            this.database.AddInParameter(storedProcCommand, "UserId", DbType.Int32, balanceDetails.UserId);
            this.database.AddInParameter(storedProcCommand, "TradeDate", DbType.DateTime, balanceDetails.TradeDate);
            this.database.AddInParameter(storedProcCommand, "TradeType", DbType.Int32, (int)balanceDetails.TradeType);
            this.database.AddInParameter(storedProcCommand, "Income", DbType.Currency, balanceDetails.Income);
            this.database.AddInParameter(storedProcCommand, "Balance", DbType.Currency, balanceDetails.Balance);
            this.database.AddInParameter(storedProcCommand, "Remark", DbType.String, balanceDetails.Remark);
            this.database.AddInParameter(storedProcCommand, "rechargeId", DbType.Int64, balanceDetails.JournalNumber);
            this.database.ExecuteNonQuery(storedProcCommand);
            return (((int)this.database.GetParameterValue(storedProcCommand, "Status")) == 0);
        }

        /// <summary>
        /// 保存银联商务返回的订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool SaveChinaUMSOrder(ChinaUMSOrder order)
        {
            var cmd = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Insert Into Shop_Order_ChinaUMS(");
            strSql.Append("[TransCode],");
            strSql.Append("[OrderDate],");
            strSql.Append("[MerOrderId],");
            strSql.Append("[TransType],");
            strSql.Append("[TransAmt],");
            strSql.Append("[MerId],");
            strSql.Append("[MerTermId],");
            strSql.Append("[NotifyUrl],");
            strSql.Append("[Reserve],");
            strSql.Append("[OrderDesc],");
            strSql.Append("[EffectiveTime],");
            strSql.Append("[RespCode],");
            strSql.Append("[IsSuccess],");
            strSql.Append("[Msg],");
            strSql.Append("[ChrCode],");
            strSql.Append("[TransId],");
            strSql.Append("[OrderId]");
            strSql.Append(")values(");
            strSql.Append("@TransCode,");
            strSql.Append("@OrderDate,");
            strSql.Append("@MerOrderId,");
            strSql.Append("@TransType,");
            strSql.Append("@TransAmt,");
            strSql.Append("@MerId,");
            strSql.Append("@MerTermId,");
            strSql.Append("@NotifyUrl,");
            strSql.Append("@Reserve,");
            strSql.Append("@OrderDesc,");
            strSql.Append("@EffectiveTime,");
            strSql.Append("@RespCode,");
            strSql.Append("@IsSuccess,");
            strSql.Append("@Msg,");
            strSql.Append("@ChrCode,");
            strSql.Append("@TransId,");
            strSql.Append("@OrderId");
            strSql.Append(")");

            cmd.CommandText = strSql.ToString();
            cmd.Parameters.Add(new SqlParameter("@TransCode", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@OrderDate", SqlDbType.DateTime));
            cmd.Parameters.Add(new SqlParameter("@MerOrderId", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@TransType", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@TransAmt", SqlDbType.Decimal));
            cmd.Parameters.Add(new SqlParameter("@MerId", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@MerTermId", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@NotifyUrl", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@Reserve", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@OrderDesc", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@EffectiveTime", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@RespCode", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@IsSuccess", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@Msg", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@ChrCode", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@TransId", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.NVarChar));

            cmd.Parameters[0].Value = order.TransCode;
            cmd.Parameters[1].Value = order.OrderDate;
            cmd.Parameters[2].Value = order.MerOrderId;
            cmd.Parameters[3].Value = order.TransType;
            cmd.Parameters[4].Value = order.TransAmt;
            cmd.Parameters[5].Value = order.MerId;
            cmd.Parameters[6].Value = order.MerTermId;
            cmd.Parameters[7].Value = order.NotifyUrl;
            cmd.Parameters[8].Value = order.Reserve;
            cmd.Parameters[9].Value = order.OrderDesc;
            cmd.Parameters[10].Value = order.EffectiveTime;
            cmd.Parameters[11].Value = order.RespCode;
            cmd.Parameters[12].Value = order.IsSuccess;
            cmd.Parameters[13].Value = order.Msg;
            cmd.Parameters[14].Value = order.ChrCode;
            cmd.Parameters[15].Value = order.TransId;
            cmd.Parameters[16].Value = order.OrderId;
            var result = this.database.ExecuteNonQuery(cmd);
            if (result >= 1) 
                return true;
            else 
                return false;

        }

        //public bool UpdateOrderPaymentStatus(int orderId, int paymentStatus)
        //{
        //    var cmd = new SqlCommand();
        //    cmd.CommandText = "UPDATE Shop_Order_ChinaUMS SET PaymentStatus = @PaymentStatus WHERE OrderId = @OrderId";
        //    cmd.Parameters.Add(new SqlParameter("@PaymentStatus", SqlDbType.SmallInt));
        //    cmd.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.SmallInt));
        //    cmd.Parameters[0].Value = paymentStatus;
        //    cmd.Parameters[1].Value = orderId;
        //    return cmd.ExecuteNonQuery() > 0;
        //}

        #region PopupRecharge
        public RechargeRequestInfo PopulateRechargeRequest(IDataRecord reader)
        {
            if (reader == null)
            {
                return null;
            }
            return new RechargeRequestInfo
                       {
                           RechargeId = (long)reader["RechargeId"],
                           TradeDate = (DateTime)reader["TradeDate"],
                           UserId = (int)reader["UserId"],
                           PaymentTypeId = (int)reader["PaymentTypeId"],
                           PaymentGateway = (string)reader["PaymentGateway"],
                           RechargeBlance = (decimal)reader["RechargeBlance"]
                       };
        }
        #endregion
        #endregion

        #region Currency
        public CurrencyInfo GetCurrency(string code)
        {
            CurrencyInfo info = null;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Pay_Currencys WHERE LOWER(Code) = LOWER(@Code)");
            this.database.AddInParameter(sqlStringCommand, "Code", DbType.String, code);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = this.PopulateCurrencyFromDataReader(reader);
                }
            }
            return info;
        }
        public IList<CurrencyInfo> GetCurrencys()
        {
            IList<CurrencyInfo> list = new List<CurrencyInfo>();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Pay_Currencys");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    list.Add(this.PopulateCurrencyFromDataReader(reader));
                }
            }
            return list;
        }
        public int DeleteCurrency(IList<string> code)
        {
            int num = 0;
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Pay_Currencys WHERE Code = @Code");
            this.database.AddInParameter(sqlStringCommand, "Code", DbType.String);
            foreach (string str in code)
            {
                this.database.SetParameterValue(sqlStringCommand, "Code", str);
                this.database.ExecuteNonQuery(sqlStringCommand);
                num++;
            }
            return num;
        }
        #region PopupCurrency
        public CurrencyInfo PopulateCurrencyFromDataReader(IDataRecord reader)
        {
            if (reader == null) return null;

            return new CurrencyInfo
                       {
                           Code = (string)reader["Code"],
                           Name = (string)reader["Name"],
                           Symbol = (string)reader["Symbol"],
                           ExchangeRate = (decimal)reader["ExchangeRate"]
                       };
        }
        #endregion
        #endregion

        #region 微信支付

        /// <summary>
        /// 保存微信预支付订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool SaveWeiXinPrepayOrder(Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage.UnifiedPrePayMessage order)
        {
            #region
            //var cmd = new SqlCommand();
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("Insert Into Shop_Order_WeiXinPrepay (");
            //strSql.Append("[OutTradeNo],");
            //strSql.Append("[ReturnCode],");
            //strSql.Append("[ReturnMsg],");
            //strSql.Append("[ResultCode],");
            //strSql.Append("[AppId],");
            //strSql.Append("[MchId],");
            //strSql.Append("[NonceStr],");
            //strSql.Append("[Sign],");
            //strSql.Append("[ErrCode],");
            //strSql.Append("[ErrCodeDes],");
            //strSql.Append("[TradeType],");
            //strSql.Append("[PrepayId],");
            //strSql.Append("[CodeUrl]");
            //strSql.Append(") Values (");
            //strSql.Append("@OutTradeNo,");
            //strSql.Append("@ReturnCode,");
            //strSql.Append("@ReturnMsg,");
            //strSql.Append("@ResultCode,");
            //strSql.Append("@AppId,");
            //strSql.Append("@MchId,");
            //strSql.Append("@NonceStr,");
            //strSql.Append("@Sign,");
            //strSql.Append("@ErrCode,");
            //strSql.Append("@ErrCodeDes,");
            //strSql.Append("@TradeType,");
            //strSql.Append("@PrepayId,");
            //strSql.Append("@CodeUrl");
            //strSql.Append(")");

            //cmd.CommandText = strSql.ToString();
            //cmd.Parameters.Add(new SqlParameter("@OutTradeNo", SqlDbType.NVarChar)); // length:32
            //cmd.Parameters.Add(new SqlParameter("@ReturnCode", SqlDbType.NVarChar)); // length:16
            //cmd.Parameters.Add(new SqlParameter("@ReturnMsg", SqlDbType.NVarChar)); // length:128
            //cmd.Parameters.Add(new SqlParameter("@ResultCode", SqlDbType.NVarChar)); // length:16
            //cmd.Parameters.Add(new SqlParameter("@AppId", SqlDbType.NVarChar)); // length:32
            //cmd.Parameters.Add(new SqlParameter("@MchId", SqlDbType.NVarChar)); // length:32
            //cmd.Parameters.Add(new SqlParameter("@NonceStr", SqlDbType.NVarChar)); // length:32
            //cmd.Parameters.Add(new SqlParameter("@Sign", SqlDbType.NVarChar)); // length:32
            //cmd.Parameters.Add(new SqlParameter("@ErrCode", SqlDbType.NVarChar)); // length:32
            //cmd.Parameters.Add(new SqlParameter("@ErrCodeDes", SqlDbType.NVarChar)); // length:128
            //cmd.Parameters.Add(new SqlParameter("@TradeType", SqlDbType.NVarChar)); // length:16
            //cmd.Parameters.Add(new SqlParameter("@PrepayId", SqlDbType.NVarChar)); // length:64
            //cmd.Parameters.Add(new SqlParameter("@CodeUrl", SqlDbType.NVarChar)); // length:64

            //cmd.Parameters[0].Value = order.Out_Trade_No;
            //cmd.Parameters[1].Value = order.Return_Code ?? "";
            //cmd.Parameters[2].Value = order.Return_Msg ?? "";
            //cmd.Parameters[3].Value = order.Result_Code ?? "";
            //cmd.Parameters[4].Value = order.AppId ?? "";
            //cmd.Parameters[5].Value = order.Mch_Id ?? "";
            //cmd.Parameters[6].Value = order.Nonce_Str ?? "";
            //cmd.Parameters[7].Value = order.Sign ?? "";
            //cmd.Parameters[8].Value = order.Err_Code ?? "";
            //cmd.Parameters[9].Value = order.Err_Code_Des ?? "";
            //cmd.Parameters[10].Value = order.Trade_Type ?? "";
            //cmd.Parameters[11].Value = order.Prepay_Id ?? "";
            //cmd.Parameters[12].Value = order.Code_Url ?? "";
            //int result = result = this.database.ExecuteNonQuery(cmd);
            //if (result >= 1)
            //    return true;
            //else
            //    return false;
            #endregion

            // 如果不存在,则新增
            // 如果已经存在,则更新

            using (SqlCommand cmd = new SqlCommand())
            {
                StringBuilder strSql = new StringBuilder();
                if (!WeiXinPrepayOrderExists(order.Out_Trade_No))
                {
                    // 新增
                    strSql.Append("Insert Into Shop_Order_WeiXinPrepay (");
                    strSql.Append("[OutTradeNo],");
                    strSql.Append("[ReturnCode],");
                    strSql.Append("[ReturnMsg],");
                    strSql.Append("[ResultCode],");
                    strSql.Append("[AppId],");
                    strSql.Append("[MchId],");
                    strSql.Append("[NonceStr],");
                    strSql.Append("[Sign],");
                    strSql.Append("[ErrCode],");
                    strSql.Append("[ErrCodeDes],");
                    strSql.Append("[TradeType],");
                    strSql.Append("[PrepayId],");
                    strSql.Append("[CodeUrl]");
                    strSql.Append(") Values (");
                    strSql.Append("@OutTradeNo,");
                    strSql.Append("@ReturnCode,");
                    strSql.Append("@ReturnMsg,");
                    strSql.Append("@ResultCode,");
                    strSql.Append("@AppId,");
                    strSql.Append("@MchId,");
                    strSql.Append("@NonceStr,");
                    strSql.Append("@Sign,");
                    strSql.Append("@ErrCode,");
                    strSql.Append("@ErrCodeDes,");
                    strSql.Append("@TradeType,");
                    strSql.Append("@PrepayId,");
                    strSql.Append("@CodeUrl");
                    strSql.Append(")");

                    cmd.CommandText = strSql.ToString();
                    cmd.Parameters.Add(new SqlParameter("@OutTradeNo", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@ReturnCode", SqlDbType.NVarChar)); // length:16
                    cmd.Parameters.Add(new SqlParameter("@ReturnMsg", SqlDbType.NVarChar)); // length:128
                    cmd.Parameters.Add(new SqlParameter("@ResultCode", SqlDbType.NVarChar)); // length:16
                    cmd.Parameters.Add(new SqlParameter("@AppId", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@MchId", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@NonceStr", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@Sign", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@ErrCode", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@ErrCodeDes", SqlDbType.NVarChar)); // length:128
                    cmd.Parameters.Add(new SqlParameter("@TradeType", SqlDbType.NVarChar)); // length:16
                    cmd.Parameters.Add(new SqlParameter("@PrepayId", SqlDbType.NVarChar)); // length:64
                    cmd.Parameters.Add(new SqlParameter("@CodeUrl", SqlDbType.NVarChar)); // length:64

                    cmd.Parameters[0].Value = order.Out_Trade_No;
                    cmd.Parameters[1].Value = order.Return_Code ?? "";
                    cmd.Parameters[2].Value = order.Return_Msg ?? "";
                    cmd.Parameters[3].Value = order.Result_Code ?? "";
                    cmd.Parameters[4].Value = order.AppId ?? "";
                    cmd.Parameters[5].Value = order.Mch_Id ?? "";
                    cmd.Parameters[6].Value = order.Nonce_Str ?? "";
                    cmd.Parameters[7].Value = order.Sign ?? "";
                    cmd.Parameters[8].Value = order.Err_Code ?? "";
                    cmd.Parameters[9].Value = order.Err_Code_Des ?? "";
                    cmd.Parameters[10].Value = order.Trade_Type ?? "";
                    cmd.Parameters[11].Value = order.Prepay_Id ?? "";
                    cmd.Parameters[12].Value = order.Code_Url ?? "";
                    int result = result = this.database.ExecuteNonQuery(cmd);
                    if (result >= 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    // 更新
                    strSql.Append("Update Shop_Order_WeiXinPrepay Set ");
                    strSql.Append("[ReturnCode] = @ReturnCode,");
                    strSql.Append("[ReturnMsg] = @ReturnMsg,");
                    strSql.Append("[ResultCode] = @ResultCode,");
                    strSql.Append("[AppId] = @AppId,");
                    strSql.Append("[MchId] = @MchId,");
                    strSql.Append("[NonceStr] = @NonceStr,");
                    strSql.Append("[Sign] = @Sign,");
                    strSql.Append("[ErrCode] = @ErrCode,");
                    strSql.Append("[ErrCodeDes] = @ErrCodeDes,");
                    strSql.Append("[TradeType] = @TradeType,");
                    strSql.Append("[PrepayId] = @PrepayId,");
                    strSql.Append("[CodeUrl] = @CodeUrl ");
                    strSql.Append("Where OutTradeNo = @OutTradeNo");

                    cmd.CommandText = strSql.ToString();
                    cmd.Parameters.Add(new SqlParameter("@ReturnCode", SqlDbType.NVarChar)); // length:16
                    cmd.Parameters.Add(new SqlParameter("@ReturnMsg", SqlDbType.NVarChar)); // length:128
                    cmd.Parameters.Add(new SqlParameter("@ResultCode", SqlDbType.NVarChar)); // length:16
                    cmd.Parameters.Add(new SqlParameter("@AppId", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@MchId", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@NonceStr", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@Sign", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@ErrCode", SqlDbType.NVarChar)); // length:32
                    cmd.Parameters.Add(new SqlParameter("@ErrCodeDes", SqlDbType.NVarChar)); // length:128
                    cmd.Parameters.Add(new SqlParameter("@TradeType", SqlDbType.NVarChar)); // length:16
                    cmd.Parameters.Add(new SqlParameter("@PrepayId", SqlDbType.NVarChar)); // length:64
                    cmd.Parameters.Add(new SqlParameter("@CodeUrl", SqlDbType.NVarChar)); // length:64
                    cmd.Parameters.Add(new SqlParameter("@OutTradeNo", SqlDbType.NVarChar)); // length:32

                    cmd.Parameters[0].Value = order.Return_Code ?? "";
                    cmd.Parameters[1].Value = order.Return_Msg ?? "";
                    cmd.Parameters[2].Value = order.Result_Code ?? "";
                    cmd.Parameters[3].Value = order.AppId ?? "";
                    cmd.Parameters[4].Value = order.Mch_Id ?? "";
                    cmd.Parameters[5].Value = order.Nonce_Str ?? "";
                    cmd.Parameters[6].Value = order.Sign ?? "";
                    cmd.Parameters[7].Value = order.Err_Code ?? "";
                    cmd.Parameters[8].Value = order.Err_Code_Des ?? "";
                    cmd.Parameters[9].Value = order.Trade_Type ?? "";
                    cmd.Parameters[10].Value = order.Prepay_Id ?? "";
                    cmd.Parameters[11].Value = order.Code_Url ?? "";
                    cmd.Parameters[12].Value = order.Out_Trade_No;
                    int result = result = this.database.ExecuteNonQuery(cmd);
                    if (result >= 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// 查询微信预支付订单
        /// </summary>
        /// <param name="mchOrderId"></param>
        /// <returns></returns>
        public Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage.UnifiedPrePayMessage GetWeiXinPrepayOrder(string outTradeNo)
        {
            if (string.IsNullOrWhiteSpace(outTradeNo)) return null;
            Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage.UnifiedPrePayMessage model = new PaymentInterface.WeChat.Models.UnifiedMessage.UnifiedPrePayMessage();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM Shop_Order_WeiXinPrepay ");
            sb.Append("WHERE OutTradeNo = @OutTradeNo");
            using (SqlCommand command = new SqlCommand(sb.ToString()))
            {
                command.Parameters.Add(new SqlParameter("@OutTradeNo", outTradeNo));
                using (IDataReader reader = this.database.ExecuteReader(command))
                {
                    if (null != reader)
                    {
                        while (reader.Read())
                        {
                            if (reader["OutTradeNo"] != DBNull.Value)
                            {
                                model.Out_Trade_No = (string)reader["OutTradeNo"];
                            }
                            if (reader["ReturnCode"] != DBNull.Value)
                            {
                                model.Return_Code = (string)reader["ReturnCode"];
                            }
                            if (reader["ReturnMsg"] != DBNull.Value)
                            {
                                model.Return_Msg = (string)reader["ReturnMsg"];
                            }
                            if (reader["ResultCode"] != DBNull.Value)
                            {
                                model.Result_Code = (string)reader["ResultCode"];
                            }
                            if (reader["AppId"] != DBNull.Value)
                            {
                                model.AppId = (string)reader["AppId"];
                            }
                            if (reader["MchId"] != DBNull.Value)
                            {
                                model.Mch_Id = (string)reader["MchId"];
                            }
                            if (reader["NonceStr"] != DBNull.Value)
                            {
                                model.Nonce_Str = (string)reader["NonceStr"];
                            }
                            if (reader["Sign"] != DBNull.Value)
                            {
                                model.Sign = (string)reader["Sign"];
                            }
                            if (reader["ErrCode"] != DBNull.Value)
                            {
                                model.Err_Code = (string)reader["ErrCode"];
                            }
                            if (reader["ErrCodeDes"] != DBNull.Value)
                            {
                                model.Err_Code_Des = (string)reader["ErrCodeDes"];
                            }
                            if (reader["TradeType"] != DBNull.Value)
                            {
                                model.Trade_Type = (string)reader["TradeType"];
                            }
                            if (reader["PrepayId"] != DBNull.Value)
                            {
                                model.Prepay_Id = (string)reader["PrepayId"];
                            }
                            if (reader["CodeUrl"] != DBNull.Value)
                            {
                                model.Code_Url = (string)reader["CodeUrl"];
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// 检验预支付订单是否已经存在
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public bool WeiXinPrepayOrderExists(string outTradeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(1) FROM Shop_Order_WeiXinPrepay ");
            strSql.Append("WHERE OutTradeNo=@OutTradeNo");
            SqlCommand cmd = new SqlCommand(strSql.ToString());
            cmd.Parameters.Add(new SqlParameter("@OutTradeNo",SqlDbType.NVarChar));
            cmd.Parameters[0].Value = outTradeNo;
            return (int)this.database.ExecuteScalar(cmd) > 0;
        }

        #endregion
    }
}