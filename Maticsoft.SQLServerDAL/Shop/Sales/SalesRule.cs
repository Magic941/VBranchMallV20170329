/**
* SalesRule.cs
*
* 功 能： N/A
* 类 名： SalesRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 18:54:56   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Sales;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Sales
{
	/// <summary>
	/// 数据访问类:SalesRule
	/// </summary>
	public partial class SalesRule:ISalesRule
	{
		public SalesRule()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("RuleId", "Shop_SalesRule"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int RuleId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_SalesRule");
			strSql.Append(" where RuleId=@RuleId");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)
			};
			parameters[0].Value = RuleId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Sales.SalesRule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_SalesRule(");
			strSql.Append("RuleName,RuleMode,RuleUnit,Status,CreatedDate,CreatedUserID)");
			strSql.Append(" values (");
			strSql.Append("@RuleName,@RuleMode,@RuleUnit,@Status,@CreatedDate,@CreatedUserID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleName", SqlDbType.NVarChar,200),
					new SqlParameter("@RuleMode", SqlDbType.Int,4),
					new SqlParameter("@RuleUnit", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4)};
			parameters[0].Value = model.RuleName;
			parameters[1].Value = model.RuleMode;
			parameters[2].Value = model.RuleUnit;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.CreatedUserID;

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
		public bool Update(Maticsoft.Model.Shop.Sales.SalesRule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_SalesRule set ");
			strSql.Append("RuleName=@RuleName,");
			strSql.Append("RuleMode=@RuleMode,");
			strSql.Append("RuleUnit=@RuleUnit,");
			strSql.Append("Status=@Status,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CreatedUserID=@CreatedUserID");
			strSql.Append(" where RuleId=@RuleId");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleName", SqlDbType.NVarChar,200),
					new SqlParameter("@RuleMode", SqlDbType.Int,4),
					new SqlParameter("@RuleUnit", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@RuleId", SqlDbType.Int,4)};
			parameters[0].Value = model.RuleName;
			parameters[1].Value = model.RuleMode;
			parameters[2].Value = model.RuleUnit;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.CreatedDate;
			parameters[5].Value = model.CreatedUserID;
			parameters[6].Value = model.RuleId;

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
		public bool Delete(int RuleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SalesRule ");
			strSql.Append(" where RuleId=@RuleId");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)
			};
			parameters[0].Value = RuleId;

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
		public bool DeleteList(string RuleIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SalesRule ");
			strSql.Append(" where RuleId in ("+RuleIdlist + ")  ");
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
		public Maticsoft.Model.Shop.Sales.SalesRule GetModel(int RuleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 RuleId,RuleName,RuleMode,RuleUnit,Status,CreatedDate,CreatedUserID from Shop_SalesRule ");
			strSql.Append(" where RuleId=@RuleId");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)
			};
			parameters[0].Value = RuleId;

			Maticsoft.Model.Shop.Sales.SalesRule model=new Maticsoft.Model.Shop.Sales.SalesRule();
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
		public Maticsoft.Model.Shop.Sales.SalesRule DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Sales.SalesRule model=new Maticsoft.Model.Shop.Sales.SalesRule();
			if (row != null)
			{
				if(row["RuleId"]!=null && row["RuleId"].ToString()!="")
				{
					model.RuleId=int.Parse(row["RuleId"].ToString());
				}
				if(row["RuleName"]!=null)
				{
					model.RuleName=row["RuleName"].ToString();
				}
				if(row["RuleMode"]!=null && row["RuleMode"].ToString()!="")
				{
					model.RuleMode=int.Parse(row["RuleMode"].ToString());
				}
				if(row["RuleUnit"]!=null && row["RuleUnit"].ToString()!="")
				{
					model.RuleUnit=int.Parse(row["RuleUnit"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["CreatedUserID"]!=null && row["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(row["CreatedUserID"].ToString());
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
			strSql.Append("select RuleId,RuleName,RuleMode,RuleUnit,Status,CreatedDate,CreatedUserID ");
			strSql.Append(" FROM Shop_SalesRule ");
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
			strSql.Append(" RuleId,RuleName,RuleMode,RuleUnit,Status,CreatedDate,CreatedUserID ");
			strSql.Append(" FROM Shop_SalesRule ");
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
			strSql.Append("select count(1) FROM Shop_SalesRule ");
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
				strSql.Append("order by T.RuleId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_SalesRule T ");
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
			parameters[0].Value = "Shop_SalesRule";
			parameters[1].Value = "RuleId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

	    public bool DeleteEx(int RuleId)
	    {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            //删除规则表数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Shop_SalesRule ");
            strSql.Append(" where RuleId=@RuleId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)};
            parameters[0].Value = RuleId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //删除规则项表数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete Shop_SalesItem ");
            strSql2.Append(" where RuleId=@RuleId ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)};
            parameters2[0].Value = RuleId;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除用户等级中间表数据
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete Shop_SalesUserRank ");
            strSql3.Append(" where  RuleId=@RuleId ");
            SqlParameter[] parameters3 = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)};
            parameters3[0].Value = RuleId;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //删除商品规则中间表数据
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete Shop_SalesRuleProduct ");
            strSql4.Append(" where  RuleId=@RuleId ");
            SqlParameter[] parameters4 = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)};
            parameters4[0].Value = RuleId;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
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


        public bool DeleteListEx(string RuleIdlist)
	    {
            List<CommandInfo> sqllist = new List<CommandInfo>();

            //删除规则表数据
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_SalesRule ");
            strSql.Append(" where RuleId in (" + RuleIdlist + ") ");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleIdt", SqlDbType.Int,4)};
            parameters[0].Value = 1;
            CommandInfo cmd = new CommandInfo(strSql.ToString(),parameters);
            sqllist.Add(cmd);

            //删除规则项表数据
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete Shop_SalesItem ");
            strSql2.Append(" where RuleId in (" + RuleIdlist + ") ");
            cmd = new CommandInfo(strSql2.ToString(), parameters);
            sqllist.Add(cmd);

            //删除用户等级中间表数据
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete Shop_SalesUserRank ");
            strSql3.Append(" where RuleId in (" + RuleIdlist + ") ");
            cmd = new CommandInfo(strSql3.ToString(), parameters);
            sqllist.Add(cmd);

            //删除商品规则中间表数据
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete Shop_SalesRuleProduct ");
            strSql4.Append(" where RuleId in (" + RuleIdlist + ") ");
            cmd = new CommandInfo(strSql4.ToString(), parameters);
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


        public bool UpdateStatus(int RuleId, int Status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_SalesRule set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where RuleId=@RuleId");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@RuleId", SqlDbType.Int,4)};
            parameters[0].Value = Status;
            parameters[1].Value = RuleId;

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

