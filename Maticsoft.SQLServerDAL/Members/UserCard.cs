/**  版本信息模板在安装目录下，可自行修改。
* UserCard.cs
*
* 功 能： N/A
* 类 名： UserCard
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/17 19:10:22   N/A    初版
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
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace Maticsoft.SQLServerDAL.Members
{
	/// <summary>
	/// 数据访问类:UserCard
	/// </summary>
	public partial class UserCard:IUserCard
	{
		public UserCard()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string CardCode)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Accounts_UserCard");
			strSql.Append(" where CardCode=@CardCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@CardCode", SqlDbType.NVarChar,50)			};
			parameters[0].Value = CardCode;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Members.UserCard model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Accounts_UserCard(");
			strSql.Append("CardCode,CardPwd,CardValue,UserId,Status,Type,EndDate,CreatedDate,Remark)");
			strSql.Append(" values (");
			strSql.Append("@CardCode,@CardPwd,@CardValue,@UserId,@Status,@Type,@EndDate,@CreatedDate,@Remark)");
			SqlParameter[] parameters = {
					new SqlParameter("@CardCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CardPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@CardValue", SqlDbType.Decimal,9),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1)};
			parameters[0].Value = model.CardCode;
			parameters[1].Value = model.CardPwd;
			parameters[2].Value = model.CardValue;
			parameters[3].Value = model.UserId;
			parameters[4].Value = model.Status;
			parameters[5].Value = model.Type;
			parameters[6].Value = model.EndDate;
			parameters[7].Value = model.CreatedDate;
			parameters[8].Value = model.Remark;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.Members.UserCard model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Accounts_UserCard set ");
			strSql.Append("CardPwd=@CardPwd,");
			strSql.Append("CardValue=@CardValue,");
			strSql.Append("UserId=@UserId,");
			strSql.Append("Status=@Status,");
			strSql.Append("Type=@Type,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where CardCode=@CardCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@CardPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@CardValue", SqlDbType.Decimal,9),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@CardCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.CardPwd;
			parameters[1].Value = model.CardValue;
			parameters[2].Value = model.UserId;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.Type;
			parameters[5].Value = model.EndDate;
			parameters[6].Value = model.CreatedDate;
			parameters[7].Value = model.Remark;
			parameters[8].Value = model.CardCode;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(string CardCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Accounts_UserCard ");
			strSql.Append(" where CardCode=@CardCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@CardCode", SqlDbType.NVarChar,50)			};
			parameters[0].Value = CardCode;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string CardCodelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Accounts_UserCard ");
			strSql.Append(" where CardCode in ('"+CardCodelist + "')  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.Members.UserCard GetModel(string CardCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CardCode,CardPwd,CardValue,UserId,Status,Type,EndDate,CreatedDate,Remark from Accounts_UserCard ");
			strSql.Append(" where CardCode=@CardCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@CardCode", SqlDbType.NVarChar,50)			};
			parameters[0].Value = CardCode;

			Maticsoft.Model.Members.UserCard model=new Maticsoft.Model.Members.UserCard();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public Maticsoft.Model.Members.UserCard DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Members.UserCard model=new Maticsoft.Model.Members.UserCard();
			if (row != null)
			{
				if(row["CardCode"]!=null)
				{
					model.CardCode=row["CardCode"].ToString();
				}
				if(row["CardPwd"]!=null)
				{
					model.CardPwd=row["CardPwd"].ToString();
				}
				if(row["CardValue"]!=null && row["CardValue"].ToString()!="")
				{
					model.CardValue=decimal.Parse(row["CardValue"].ToString());
				}
				if(row["UserId"]!=null && row["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(row["UserId"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["EndDate"]!=null && row["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(row["EndDate"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CardCode,CardPwd,CardValue,UserId,Status,Type,EndDate,CreatedDate,Remark ");
			strSql.Append(" FROM Accounts_UserCard ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" CardCode,CardPwd,CardValue,UserId,Status,Type,EndDate,CreatedDate,Remark ");
			strSql.Append(" FROM Accounts_UserCard ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Accounts_UserCard ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.CardCode desc");
			}
			strSql.Append(")AS Row, T.*  from Accounts_UserCard T ");
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
			parameters[0].Value = "Accounts_UserCard";
			parameters[1].Value = "CardCode";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
       public bool AddCard(Maticsoft.Model.Members.UserCard model)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_UserCard(");
            strSql.Append("CardCode,CardPwd,CardValue,UserId,Status,Type,EndDate,CreatedDate,Remark)");
            strSql.Append(" values (");
            strSql.Append("@CardCode,@CardPwd,@CardValue,@UserId,@Status,@Type,@EndDate,@CreatedDate,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@CardCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CardPwd", SqlDbType.NVarChar,50),
					new SqlParameter("@CardValue", SqlDbType.Decimal,9),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.CardCode;
            parameters[1].Value = model.CardPwd;
            parameters[2].Value = model.CardValue;
            parameters[3].Value = model.UserId;
            parameters[4].Value = model.Status;
            parameters[5].Value = model.Type;
            parameters[6].Value = model.EndDate;
            parameters[7].Value = model.CreatedDate;
            parameters[8].Value = model.Remark;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("Update Accounts_UsersExp set  UserCardCode=@UserCardCode,UserCardType=@UserCardType");
            strSql2.Append(" where UserID=@UserID");
            SqlParameter[] parameters2 = {
					new SqlParameter("@UserCardCode", SqlDbType.NVarChar,50),
                    	new SqlParameter("@UserCardType", SqlDbType.SmallInt),
                        	new SqlParameter("@UserID", SqlDbType.Int)
                                         };
            parameters2[0].Value = model.CardCode;
            parameters2[1].Value = model.Type;
            parameters2[2].Value = model.UserId;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            

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

       public bool DeleteEx(string CardCode)
       {
           List<CommandInfo> sqllist = new List<CommandInfo>();
           StringBuilder strSql = new StringBuilder();
           strSql.Append("delete from Accounts_UserCard ");
           strSql.Append(" where CardCode=@CardCode ");
           SqlParameter[] parameters = {
					new SqlParameter("@CardCode", SqlDbType.NVarChar,50)			};
           parameters[0].Value = CardCode;
           CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
           sqllist.Add(cmd);

           StringBuilder strSql2 = new StringBuilder();
           strSql2.Append("Update Accounts_UsersExp set  UserCardCode='',UserCardType=-1");
           strSql2.Append(" where UserCardCode=@CardCode");
           SqlParameter[] parameters2 = {
				new SqlParameter("@CardCode", SqlDbType.NVarChar,50)			};
           parameters2[0].Value = CardCode;
           cmd = new CommandInfo(strSql2.ToString(), parameters2);
           sqllist.Add(cmd);

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
		#endregion  ExtensionMethod
	}
}

