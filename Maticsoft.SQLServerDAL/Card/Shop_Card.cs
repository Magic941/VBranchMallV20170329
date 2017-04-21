using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;
using Maticsoft.IDAL;
using Maticsoft.DBUtility;

namespace Maticsoft.SQLServerDAL.Card
{
	/// <summary>
	/// 数据访问类:Shop_Card
	/// </summary>
	public partial class Shop_Card : IShop_Card
	{
		public Shop_Card()
		{ }
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("Id", "Shop_Card");
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Shop_Card");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop_Card model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_Card(");
			strSql.Append("Batch,CardNo,Password,SalesId,CardTypeId,Number,ActivateDate,ActivateAccID,ActivateAccNo,ActivateAccName,UnLockDate,IsActivate,IsLock,Value,InValue,OutStatus,CardStatus,CardOutNo,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,CardTypeNo)");
			strSql.Append(" values (");
			strSql.Append("@Batch,@CardNo,@Password,@SalesId,@CardTypeId,@Number,@ActivateDate,@ActivateAccID,@ActivateAccNo,@ActivateAccName,@UnLockDate,@IsActivate,@IsLock,@Value,@InValue,@OutStatus,@CardStatus,@CardOutNo,@CREATEDATE,@CREATER,@MODIFYDATE,@MODIFYER,@CardTypeNo)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Batch", SqlDbType.NChar,4),
					new SqlParameter("@CardNo", SqlDbType.VarChar,50),
					new SqlParameter("@Password", SqlDbType.VarChar,20),
					new SqlParameter("@SalesId", SqlDbType.Int,4),
					new SqlParameter("@CardTypeId", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.Int,4),
					new SqlParameter("@ActivateDate", SqlDbType.DateTime),
					new SqlParameter("@ActivateAccID", SqlDbType.VarChar,20),
					new SqlParameter("@ActivateAccNo", SqlDbType.VarChar,20),
					new SqlParameter("@ActivateAccName", SqlDbType.VarChar,30),
					new SqlParameter("@UnLockDate", SqlDbType.DateTime),
					new SqlParameter("@IsActivate", SqlDbType.Bit,1),
					new SqlParameter("@IsLock", SqlDbType.Bit,1),
					new SqlParameter("@Value", SqlDbType.Decimal,9),
					new SqlParameter("@InValue", SqlDbType.Decimal,9),
					new SqlParameter("@OutStatus", SqlDbType.Int,4),
					new SqlParameter("@CardStatus", SqlDbType.Int,4),
					new SqlParameter("@CardOutNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CREATEDATE", SqlDbType.DateTime),
					new SqlParameter("@CREATER", SqlDbType.NVarChar,20),
					new SqlParameter("@MODIFYDATE", SqlDbType.DateTime),
					new SqlParameter("@MODIFYER", SqlDbType.NVarChar,20),
					new SqlParameter("@CardTypeNo", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.Batch;
			parameters[1].Value = model.CardNo;
			parameters[2].Value = model.Password;
			parameters[3].Value = model.SalesId;
			parameters[4].Value = model.CardTypeId;
			parameters[5].Value = model.Number;
			parameters[6].Value = model.ActivateDate;
			parameters[7].Value = model.ActivateAccID;
			parameters[8].Value = model.ActivateAccNo;
			parameters[9].Value = model.ActivateAccName;
			parameters[10].Value = model.UnLockDate;
			parameters[11].Value = model.IsActivate;
			parameters[12].Value = model.IsLock;
			parameters[13].Value = model.Value;
			parameters[14].Value = model.InValue;
			parameters[15].Value = model.OutStatus;
			parameters[16].Value = model.CardStatus;
			parameters[17].Value = model.CardOutNo;
			parameters[18].Value = model.CREATEDATE;
			parameters[19].Value = model.CREATER;
			parameters[20].Value = model.MODIFYDATE;
			parameters[21].Value = model.MODIFYER;
			parameters[22].Value = model.CardTypeNo;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop_Card model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_Card set ");
            strSql.Append("Batch=@Batch,");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("Password=@Password,");
            strSql.Append("SalesId=@SalesId,");
            strSql.Append("CardTypeId=@CardTypeId,");
            strSql.Append("Number=@Number,");
            strSql.Append("ActivateDate=@ActivateDate,");
            strSql.Append("ActivateAccID=@ActivateAccID,");
            strSql.Append("ActivateAccNo=@ActivateAccNo,");
            strSql.Append("ActivateAccName=@ActivateAccName,");
            strSql.Append("UnLockDate=@UnLockDate,");
            strSql.Append("IsActivate=@IsActivate,");
            strSql.Append("IsLock=@IsLock,");
            strSql.Append("Value=@Value,");
            strSql.Append("InValue=@InValue,");
            strSql.Append("OutStatus=@OutStatus,");
            strSql.Append("CardStatus=@CardStatus,");
            strSql.Append("CardOutNo=@CardOutNo,");
            strSql.Append("CREATEDATE=@CREATEDATE,");
            strSql.Append("CREATER=@CREATER,");
            strSql.Append("MODIFYDATE=@MODIFYDATE,");
            strSql.Append("MODIFYER=@MODIFYER,");
            strSql.Append("CardTypeNo=@CardTypeNo");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Batch", SqlDbType.NChar,4),
					new SqlParameter("@CardNo", SqlDbType.VarChar,50),
					new SqlParameter("@Password", SqlDbType.VarChar,20),
					new SqlParameter("@SalesId", SqlDbType.Int,4),
					new SqlParameter("@CardTypeId", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.Int,4),
					new SqlParameter("@ActivateDate", SqlDbType.DateTime),
					new SqlParameter("@ActivateAccID", SqlDbType.VarChar,20),
					new SqlParameter("@ActivateAccNo", SqlDbType.VarChar,20),
					new SqlParameter("@ActivateAccName", SqlDbType.VarChar,30),
					new SqlParameter("@UnLockDate", SqlDbType.DateTime),
					new SqlParameter("@IsActivate", SqlDbType.Bit,1),
					new SqlParameter("@IsLock", SqlDbType.Bit,1),
					new SqlParameter("@Value", SqlDbType.Decimal,9),
					new SqlParameter("@InValue", SqlDbType.Decimal,9),
					new SqlParameter("@OutStatus", SqlDbType.Int,4),
					new SqlParameter("@CardStatus", SqlDbType.Int,4),
					new SqlParameter("@CardOutNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CREATEDATE", SqlDbType.DateTime),
					new SqlParameter("@CREATER", SqlDbType.NVarChar,20),
					new SqlParameter("@MODIFYDATE", SqlDbType.DateTime),
					new SqlParameter("@MODIFYER", SqlDbType.NVarChar,20),
					new SqlParameter("@CardTypeNo", SqlDbType.NVarChar,20),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.Batch;
            parameters[1].Value = model.CardNo;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.SalesId;
            parameters[4].Value = model.CardTypeId;
            parameters[5].Value = model.Number;
            parameters[6].Value = model.ActivateDate;
            parameters[7].Value = model.ActivateAccID;
            parameters[8].Value = model.ActivateAccNo;
            parameters[9].Value = model.ActivateAccName;
            parameters[10].Value = model.UnLockDate;
            parameters[11].Value = model.IsActivate;
            parameters[12].Value = model.IsLock;
            parameters[13].Value = model.Value;
            parameters[14].Value = model.InValue;
            parameters[15].Value = model.OutStatus;
            parameters[16].Value = model.CardStatus;
            parameters[17].Value = model.CardOutNo;
            parameters[18].Value = model.CREATEDATE;
            parameters[19].Value = model.CREATER;
            parameters[20].Value = model.MODIFYDATE;
            parameters[21].Value = model.MODIFYER;
            parameters[22].Value = model.CardTypeNo;
            parameters[23].Value = model.Id;

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
		public bool Delete(int Id)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Shop_Card ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
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
			strSql.Append("delete from Shop_Card ");
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
		/// 根据id得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop_Card GetModel(int Id)
		{

			StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Batch,CardNo,Password,SalesId,CardTypeId,Number,ActivateDate,ActivateAccID,ActivateAccNo,ActivateAccName,UnLockDate,IsActivate,IsLock,Value,InValue,OutStatus,CardStatus,CardOutNo,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,CardTypeNo  from Shop_Card ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			Maticsoft.Model.Shop_Card model = new Maticsoft.Model.Shop_Card();
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
        public Maticsoft.Model.Shop_Card DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop_Card model = new Maticsoft.Model.Shop_Card();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["Batch"] != null)
                {
                    model.Batch = row["Batch"].ToString();
                }
                if (row["CardNo"] != null)
                {
                    model.CardNo = row["CardNo"].ToString();
                }
                if (row["Password"] != null)
                {
                    model.Password = row["Password"].ToString();
                }
                if (row["SalesId"] != null && row["SalesId"].ToString() != "")
                {
                    model.SalesId = int.Parse(row["SalesId"].ToString());
                }
                if (row["CardTypeId"] != null && row["CardTypeId"].ToString() != "")
                {
                    model.CardTypeId = int.Parse(row["CardTypeId"].ToString());
                }
                if (row["Number"] != null && row["Number"].ToString() != "")
                {
                    model.Number = int.Parse(row["Number"].ToString());
                }
                if (row["ActivateDate"] != null && row["ActivateDate"].ToString() != "")
                {
                    model.ActivateDate = DateTime.Parse(row["ActivateDate"].ToString());
                }
                if (row["ActivateAccID"] != null)
                {
                    model.ActivateAccID = row["ActivateAccID"].ToString();
                }
                if (row["ActivateAccNo"] != null)
                {
                    model.ActivateAccNo = row["ActivateAccNo"].ToString();
                }
                if (row["ActivateAccName"] != null)
                {
                    model.ActivateAccName = row["ActivateAccName"].ToString();
                }
                if (row["UnLockDate"] != null && row["UnLockDate"].ToString() != "")
                {
                    model.UnLockDate = DateTime.Parse(row["UnLockDate"].ToString());
                }
                if (row["IsActivate"] != null && row["IsActivate"].ToString() != "")
                {
                    if ((row["IsActivate"].ToString() == "1") || (row["IsActivate"].ToString().ToLower() == "true"))
                    {
                        model.IsActivate = true;
                    }
                    else
                    {
                        model.IsActivate = false;
                    }
                }
                if (row["IsLock"] != null && row["IsLock"].ToString() != "")
                {
                    if ((row["IsLock"].ToString() == "1") || (row["IsLock"].ToString().ToLower() == "true"))
                    {
                        model.IsLock = true;
                    }
                    else
                    {
                        model.IsLock = false;
                    }
                }
                if (row["Value"] != null && row["Value"].ToString() != "")
                {
                    model.Value = decimal.Parse(row["Value"].ToString());
                }
                if (row["InValue"] != null && row["InValue"].ToString() != "")
                {
                    model.InValue = decimal.Parse(row["InValue"].ToString());
                }
                if (row["OutStatus"] != null && row["OutStatus"].ToString() != "")
                {
                    model.OutStatus = int.Parse(row["OutStatus"].ToString());
                }
                if (row["CardStatus"] != null && row["CardStatus"].ToString() != "")
                {
                    model.CardStatus = int.Parse(row["CardStatus"].ToString());
                }
                if (row["CardOutNo"] != null)
                {
                    model.CardOutNo = row["CardOutNo"].ToString();
                }
                if (row["CREATEDATE"] != null && row["CREATEDATE"].ToString() != "")
                {
                    model.CREATEDATE = DateTime.Parse(row["CREATEDATE"].ToString());
                }
                if (row["CREATER"] != null)
                {
                    model.CREATER = row["CREATER"].ToString();
                }
                if (row["MODIFYDATE"] != null && row["MODIFYDATE"].ToString() != "")
                {
                    model.MODIFYDATE = DateTime.Parse(row["MODIFYDATE"].ToString());
                }
                if (row["MODIFYER"] != null)
                {
                    model.MODIFYER = row["MODIFYER"].ToString();
                }
                if (row["CardTypeNo"] != null)
                {
                    model.CardTypeNo = row["CardTypeNo"].ToString();
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
            strSql.Append("select Id,Batch,CardNo,Password,SalesId,CardTypeId,Number,ActivateDate,ActivateAccID,ActivateAccNo,ActivateAccName,UnLockDate,IsActivate,IsLock,Value,InValue,OutStatus,CardStatus,CardOutNo,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,CardTypeNo  ");
			strSql.Append(" FROM Shop_Card ");
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
            strSql.Append(" Id,Batch,CardNo,Password,SalesId,CardTypeId,Number,ActivateDate,ActivateAccID,ActivateAccNo,ActivateAccName,UnLockDate,IsActivate,IsLock,Value,InValue,OutStatus,CardStatus,CardOutNo,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,CardTypeNo  ");
			strSql.Append(" FROM Shop_Card ");
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
			strSql.Append("select count(1) FROM Shop_Card ");
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
			strSql.Append(")AS Row, T.*  from Shop_Card T ");
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
			parameters[0].Value = "Shop_Card";
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
        /// 根据卡号获取卡信息的集合
        /// </summary>
        /// <param name="CardNoList"></param>
        /// <returns></returns>
		 public DataSet GetCardListByID(string CardNoList)
		{
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Batch,CardNo,Password,SalesId,CardTypeId,Number,ActivateDate,ActivateAccID,ActivateAccNo,ActivateAccName,UnLockDate,IsActivate,IsLock,Value,InValue,OutStatus,CardStatus,CardOutNo,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,CardTypeNo  from Shop_Card ");
			if (!string.IsNullOrEmpty(CardNoList))
			{
				strSql.Append(" where Id in (" + CardNoList + ")  ");
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		 /// <summary>
		 /// 根据卡号得到一个对象实体
		 /// </summary>
		 public Maticsoft.Model.Shop_Card GetModel(string CardNo)
		 {

			 StringBuilder strSql = new StringBuilder();
             strSql.Append("select  top 1 Id,Batch,CardNo,Password,SalesId,CardTypeId,Number,ActivateDate,ActivateAccID,ActivateAccNo,ActivateAccName,UnLockDate,IsActivate,IsLock,Value,InValue,OutStatus,CardStatus,CardOutNo,CREATEDATE,CREATER,MODIFYDATE,MODIFYER,CardTypeNo  from Shop_Card ");
			 strSql.Append(" where CardNo=@CardNo");
			 SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.VarChar,50)
			};
			 parameters[0].Value = CardNo;

			 Maticsoft.Model.Shop_Card model = new Maticsoft.Model.Shop_Card();
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

		#endregion  ExtensionMethod
	}
}


