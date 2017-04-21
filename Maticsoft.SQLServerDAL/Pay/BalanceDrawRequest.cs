/**
* BalanceDrawRequest.cs
*
* 功 能： N/A
* 类 名： BalanceDrawRequest
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/16 18:31:09   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Pay;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace Maticsoft.SQLServerDAL.Pay
{
    /// <summary>
    /// 数据访问类:BalanceDrawRequest
    /// </summary>
    public partial class BalanceDrawRequest : IBalanceDrawRequest
    {
        public BalanceDrawRequest()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long JournalNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Pay_BalanceDrawRequest");
            strSql.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] parameters = {
					new SqlParameter("@JournalNumber", SqlDbType.BigInt)
			};
            parameters[0].Value = JournalNumber;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.Model.Pay.BalanceDrawRequest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Pay_BalanceDrawRequest(");
            strSql.Append("RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,RequestType,TargetId,Remark)");
            strSql.Append(" values (");
            strSql.Append("@RequestTime,@Amount,@UserID,@TrueName,@BankName,@BankCard,@CardTypeID,@RequestStatus,@RequestType,@TargetId,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RequestTime", SqlDbType.DateTime),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankName", SqlDbType.NVarChar,200),
					new SqlParameter("@BankCard", SqlDbType.NVarChar,50),
					new SqlParameter("@CardTypeID", SqlDbType.Int,4),
					new SqlParameter("@RequestStatus", SqlDbType.Int,4),
					new SqlParameter("@RequestType", SqlDbType.SmallInt,2),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000)};
            parameters[0].Value = model.RequestTime;
            parameters[1].Value = model.Amount;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.TrueName;
            parameters[4].Value = model.BankName;
            parameters[5].Value = model.BankCard;
            parameters[6].Value = model.CardTypeID;
            parameters[7].Value = model.RequestStatus;
            parameters[8].Value = model.RequestType;
            parameters[9].Value = model.TargetId;
            parameters[10].Value = model.Remark;

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
        public bool Update(Maticsoft.Model.Pay.BalanceDrawRequest model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pay_BalanceDrawRequest set ");
            strSql.Append("RequestTime=@RequestTime,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("UserID=@UserID,");
            strSql.Append("TrueName=@TrueName,");
            strSql.Append("BankName=@BankName,");
            strSql.Append("BankCard=@BankCard,");
            strSql.Append("CardTypeID=@CardTypeID,");
            strSql.Append("RequestStatus=@RequestStatus,");
            strSql.Append("RequestType=@RequestType,");
            strSql.Append("TargetId=@TargetId,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] parameters = {
					new SqlParameter("@RequestTime", SqlDbType.DateTime),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankName", SqlDbType.NVarChar,200),
					new SqlParameter("@BankCard", SqlDbType.NVarChar,50),
					new SqlParameter("@CardTypeID", SqlDbType.Int,4),
					new SqlParameter("@RequestStatus", SqlDbType.Int,4),
					new SqlParameter("@RequestType", SqlDbType.SmallInt,2),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000),
					new SqlParameter("@JournalNumber", SqlDbType.BigInt,8)};
            parameters[0].Value = model.RequestTime;
            parameters[1].Value = model.Amount;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.TrueName;
            parameters[4].Value = model.BankName;
            parameters[5].Value = model.BankCard;
            parameters[6].Value = model.CardTypeID;
            parameters[7].Value = model.RequestStatus;
            parameters[8].Value = model.RequestType;
            parameters[9].Value = model.TargetId;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.JournalNumber;

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
        public bool Delete(long JournalNumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Pay_BalanceDrawRequest ");
            strSql.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] parameters = {
					new SqlParameter("@JournalNumber", SqlDbType.BigInt)
			};
            parameters[0].Value = JournalNumber;

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
        public bool DeleteList(string JournalNumberlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Pay_BalanceDrawRequest ");
            strSql.Append(" where JournalNumber in (" + JournalNumberlist + ")  ");
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
        public Maticsoft.Model.Pay.BalanceDrawRequest GetModel(long JournalNumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 JournalNumber,RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,RequestType,TargetId,Remark from Pay_BalanceDrawRequest ");
            strSql.Append(" where JournalNumber=@JournalNumber");
            SqlParameter[] parameters = {
					new SqlParameter("@JournalNumber", SqlDbType.BigInt)
			};
            parameters[0].Value = JournalNumber;

            Maticsoft.Model.Pay.BalanceDrawRequest model = new Maticsoft.Model.Pay.BalanceDrawRequest();
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
        public Maticsoft.Model.Pay.BalanceDrawRequest DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Pay.BalanceDrawRequest model = new Maticsoft.Model.Pay.BalanceDrawRequest();
            if (row != null)
            {
                if (row["JournalNumber"] != null && row["JournalNumber"].ToString() != "")
                {
                    model.JournalNumber = long.Parse(row["JournalNumber"].ToString());
                }
                if (row["RequestTime"] != null && row["RequestTime"].ToString() != "")
                {
                    model.RequestTime = DateTime.Parse(row["RequestTime"].ToString());
                }
                if (row["Amount"] != null && row["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["TrueName"] != null)
                {
                    model.TrueName = row["TrueName"].ToString();
                }
                if (row["BankName"] != null)
                {
                    model.BankName = row["BankName"].ToString();
                }
                if (row["BankCard"] != null)
                {
                    model.BankCard = row["BankCard"].ToString();
                }
                if (row["CardTypeID"] != null && row["CardTypeID"].ToString() != "")
                {
                    model.CardTypeID = int.Parse(row["CardTypeID"].ToString());
                }
                if (row["RequestStatus"] != null && row["RequestStatus"].ToString() != "")
                {
                    model.RequestStatus = int.Parse(row["RequestStatus"].ToString());
                }
                if (row["RequestType"] != null && row["RequestType"].ToString() != "")
                {
                    model.RequestType = int.Parse(row["RequestType"].ToString());
                }
                if (row["TargetId"] != null && row["TargetId"].ToString() != "")
                {
                    model.TargetId = int.Parse(row["TargetId"].ToString());
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
            strSql.Append("select JournalNumber,RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,RequestType,TargetId,Remark ");
            strSql.Append(" FROM Pay_BalanceDrawRequest ");
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
            strSql.Append(" JournalNumber,RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,RequestType,TargetId,Remark ");
            strSql.Append(" FROM Pay_BalanceDrawRequest ");
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
            strSql.Append("select count(1) FROM Pay_BalanceDrawRequest ");
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
                strSql.Append("order by T.JournalNumber desc");
            }
            strSql.Append(")AS Row, T.*  from Pay_BalanceDrawRequest T ");
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
            parameters[0].Value = "Pay_BalanceDrawRequest";
            parameters[1].Value = "JournalNumber";
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
        /// 增加一条数据
        /// </summary>
        public bool AddEx(Model.Pay.BalanceDrawRequest model, decimal balance)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Pay_BalanceDrawRequest(");
            strSql.Append("RequestTime,Amount,UserID,TrueName,BankName,BankCard,CardTypeID,RequestStatus,RequestType,TargetId,Remark)");
            strSql.Append(" values (");
            strSql.Append("@RequestTime,@Amount,@UserID,@TrueName,@BankName,@BankCard,@CardTypeID,@RequestStatus,@RequestType,@TargetId,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RequestTime", SqlDbType.DateTime),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@TrueName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankName", SqlDbType.NVarChar,200),
					new SqlParameter("@BankCard", SqlDbType.NVarChar,50),
					new SqlParameter("@CardTypeID", SqlDbType.Int,4),
					new SqlParameter("@RequestStatus", SqlDbType.Int,4),
					new SqlParameter("@RequestType", SqlDbType.SmallInt,2),
					new SqlParameter("@TargetId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,2000)};
            parameters[0].Value = model.RequestTime;
            parameters[1].Value = model.Amount;
            parameters[2].Value = model.UserID;
            parameters[3].Value = model.TrueName;
            parameters[4].Value = model.BankName;
            parameters[5].Value = model.BankCard;
            parameters[6].Value = model.CardTypeID;
            parameters[7].Value = model.RequestStatus;
            parameters[8].Value = model.RequestType;
            parameters[9].Value = model.TargetId;
            parameters[10].Value = model.Remark;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //更新用户表中的余额
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(" update Accounts_UsersExp set ");
            strSql2.Append(" Balance=Balance- @Amount  where UserID=@UserId");
            SqlParameter[] parameters2 = {
					new SqlParameter("@Amount", SqlDbType.Money,8),
                       new SqlParameter("@UserId", SqlDbType.Int,4)                 };
            parameters2[0].Value = model.Amount;//余额
            parameters2[1].Value = model.UserID;// 
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into Pay_BalanceDetails(");
            strSql3.Append("UserId,TradeDate,TradeType,Expenses,Balance,Remark)");
            strSql3.Append(" values (");
            strSql3.Append("@UserId,@TradeDate,@TradeType,@Expenses,@Balance,@Remark)");
            strSql3.Append(";select @@IDENTITY");
            SqlParameter[] parameters3 = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@TradeDate", SqlDbType.DateTime),
					new SqlParameter("@TradeType", SqlDbType.Int,4),
					new SqlParameter("@Expenses", SqlDbType.Money,8),
					new SqlParameter("@Balance", SqlDbType.Money,8),
                     new SqlParameter("@Remark", SqlDbType.NVarChar,2000)
				 };
            parameters3[0].Value = model.UserID;
            parameters3[1].Value = DateTime.Now;
            parameters3[2].Value = 2;
            parameters3[3].Value = model.Amount;//支出
            parameters3[4].Value = balance - model.Amount; ;//余额
            parameters3[5].Value = model.RequestType == 1 ? "用户提现" : "商家结算"; //备注
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
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
        /// 获得提现金额  sum
        /// </summary>
        public decimal GetBalanceDraw(int userid, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM( Amount) FROM Pay_BalanceDrawRequest where UserID=@UserID and RequestStatus=@RequestStatus");
            SqlParameter[] parameters = {			 
					new SqlParameter("@UserID", SqlDbType.Int,4),		 
					new SqlParameter("@RequestStatus", SqlDbType.Int,4)};
            parameters[0].Value = userid;
            parameters[1].Value = Status;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }

        /// <summary>
        /// 获得数据列表 与users表内联
        /// </summary>
        public DataSet GetListUser(string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  baldraw.*,users.UserName  ");
            strSql.Append(" FROM  Pay_BalanceDrawRequest AS baldraw ");
            strSql.Append(" INNER JOIN  Accounts_Users AS users ");
            strSql.Append(" ON baldraw.UserID= users.UserID  ");
            strSql.Append(" where RequestType=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表 与商家表内联
        /// </summary>
        public DataSet GetListSupplier(string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT  baldraw.*,users.UserName,SS.Name,SS.Slogan  ");
            strSql.Append(" FROM  Pay_BalanceDrawRequest AS baldraw ");
            strSql.Append(" INNER JOIN  Accounts_Users AS users ");
            strSql.Append(" ON baldraw.UserID= users.UserID  ");
            strSql.Append(" INNER JOIN  Shop_Suppliers AS SS ");
            strSql.Append(" ON baldraw.TargetId= SS.SupplierId  ");
            strSql.Append(" where RequestType=2 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Status">状态</param>
        /// <param name="balance">用户账户余额</param>
        /// <returns></returns>
        public bool UpdateStatus(Model.Pay.BalanceDrawRequest model, int Status, decimal balance)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Update  Pay_BalanceDrawRequest  Set ");
            strSql.Append(" RequestStatus=@RequestStatus ");
            strSql.Append(" where JournalNumber = " + model.JournalNumber);
            SqlParameter[] parameters = { 
					new SqlParameter("@RequestStatus", SqlDbType.Int,4)};
            parameters[0].Value = Status;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            if (Status == 2)  //如果状态改为未审核就将提现的钱返还给用户 
            {
                //更新用户表中的余额
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append(" update Accounts_UsersExp set ");
                strSql2.Append(" Balance=Balance+ @Amount   where UserID=@UserId ");
                SqlParameter[] parameters2 = {
                    new SqlParameter("@Amount", SqlDbType.Money,8),
                      new SqlParameter("@UserId", SqlDbType.Int,4)  
                                        };
                parameters2[0].Value = model.Amount;//余额
                parameters2[1].Value = model.UserID;
                cmd = new CommandInfo(strSql2.ToString(), parameters2);
                sqllist.Add(cmd);

                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("insert into Pay_BalanceDetails(");
                strSql3.Append("UserId,TradeDate,TradeType,Income,Balance,Remark)");
                strSql3.Append(" values (");
                strSql3.Append("@UserId,@TradeDate,@TradeType,@Income,@Balance,@Remark)");
                strSql3.Append(";select @@IDENTITY");
                SqlParameter[] parameters3 = {
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@TradeDate", SqlDbType.DateTime),
                    new SqlParameter("@TradeType", SqlDbType.Int,4),
                    new SqlParameter("@Income", SqlDbType.Money,8),
                    new SqlParameter("@Balance", SqlDbType.Money,8),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,2000)
                 };
                parameters3[0].Value = model.UserID;
                parameters3[1].Value = DateTime.Now;
                parameters3[2].Value = 3;//
                parameters3[3].Value = model.Amount;//收入
                parameters3[4].Value = balance + model.Amount;//余额
                parameters3[5].Value = model.RequestType == 1 ? "提现申请失败" : "结算申请失败"; //备注

                cmd = new CommandInfo(strSql3.ToString(), parameters3);
                sqllist.Add(cmd);
            }
            int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///// <summary>
        ///// 批量修改状态
        ///// </summary>
        //public bool UpdateStatus(string JournalNumberlist,int Status)
        //{   
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append(" Update  Pay_BalanceDrawRequest  Set ");
        //    strSql.Append(" RequestStatus=@RequestStatus ");
        //    strSql.Append(" where JournalNumber in ("+JournalNumberlist + ")  ");
        //    SqlParameter[] parameters = { 
        //            new SqlParameter("@RequestStatus", SqlDbType.Int,4)};
        //    parameters[0].Value = Status;

        //    List<CommandInfo> sqllist = new List<CommandInfo>();
        //    CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
        //    sqllist.Add(cmd);

        //    if (Status == 2)  //如果状态改为未审核就将提现的钱返还给用户 
        //    {  
        //        //更新用户表中的余额
        //        StringBuilder strSql2 = new StringBuilder();
        //        strSql2.Append(" update Accounts_UsersExp set ");
        //        strSql2.Append(" Balance=Balance- @Amount");
        //        SqlParameter[] parameters2 = {
        //            new SqlParameter("@Amount", SqlDbType.Money,8)
        //                                };
        //        parameters2[0].Value = model.Amount;//余额
        //        cmd = new CommandInfo(strSql2.ToString(), parameters2);
        //        sqllist.Add(cmd);

        //        StringBuilder strSql3 = new StringBuilder();
        //        strSql3.Append("insert into Pay_BalanceDetails(");
        //        strSql3.Append("UserId,TradeDate,TradeType,Expenses,Balance,Remark)");
        //        strSql3.Append(" values (");
        //        strSql3.Append("@UserId,@TradeDate,@TradeType,@Expenses,@Balance,@Remark)");
        //        strSql3.Append(";select @@IDENTITY");
        //        SqlParameter[] parameters3 = {
        //            new SqlParameter("@UserId", SqlDbType.Int,4),
        //            new SqlParameter("@TradeDate", SqlDbType.DateTime),
        //            new SqlParameter("@TradeType", SqlDbType.Int,4),
        //            new SqlParameter("@Expenses", SqlDbType.Money,8),
        //            new SqlParameter("@Balance", SqlDbType.Money,8),
        //            new SqlParameter("@Remark", SqlDbType.NVarChar,2000)
        //         };
        //        parameters3[0].Value = model.UserID;
        //        parameters3[1].Value = DateTime.Now;
        //        parameters3[2].Value = 2;
        //        parameters3[3].Value = model.Amount;//支出
        //        parameters3[4].Value = balance - model.Amount; ;//余额
        //        parameters3[5].Value = "提现申请失败"; //备注

        //        cmd = new CommandInfo(strSql3.ToString(), parameters3);
        //        sqllist.Add(cmd);
        //        cmd = new CommandInfo(strSql2.ToString(), parameters2);
        //        sqllist.Add(cmd);
        //    }
        //    int rows = DbHelperSQL.ExecuteSqlTran(sqllist);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public int GetTotalcount(string startTime, string endTime)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select count(*) from Pay_BalanceDrawRequest where  ");
            stringBuilder.Append("convert(date,RequestTime)>='" + startTime + "'");
            stringBuilder.Append(" and convert(date,RequestTime)<='" + endTime + "'");
            object returnValue = DbHelperSQL.GetSingle(stringBuilder.ToString());
            if (returnValue == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(returnValue);
            }
        }

        public int GetTotalAmount(string startTime, string endTime)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select sum(Amount) from Pay_BalanceDrawRequest where  ");
            stringBuilder.Append("convert(date,RequestTime)>='" + startTime + "'");
            stringBuilder.Append(" and convert(date,RequestTime)<='" + endTime + "'");
            object returnValue = DbHelperSQL.GetSingle(stringBuilder.ToString());
            if (returnValue == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(returnValue);
            }
        }
        #endregion  ExtensionMethod
    }
}

