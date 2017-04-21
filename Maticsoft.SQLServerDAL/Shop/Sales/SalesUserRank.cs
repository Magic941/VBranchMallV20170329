/**
* SalesUserRank.cs
*
* 功 能： N/A
* 类 名： SalesUserRank
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 18:55:02   N/A    初版
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
using Maticsoft.IDAL.Shop.Sales;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Sales
{
	/// <summary>
	/// 数据访问类:SalesUserRank
	/// </summary>
	public partial class SalesUserRank:ISalesUserRank
	{
		public SalesUserRank()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("RuleId", "Shop_SalesUserRank"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int RuleId,int RankId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_SalesUserRank");
			strSql.Append(" where RuleId=@RuleId and RankId=@RankId ");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@RankId", SqlDbType.Int,4)			};
			parameters[0].Value = RuleId;
			parameters[1].Value = RankId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Shop.Sales.SalesUserRank model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_SalesUserRank(");
			strSql.Append("RuleId,RankId,Remark)");
			strSql.Append(" values (");
			strSql.Append("@RuleId,@RankId,@Remark)");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@RankId", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.RuleId;
			parameters[1].Value = model.RankId;
			parameters[2].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.Shop.Sales.SalesUserRank model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_SalesUserRank set ");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where RuleId=@RuleId and RankId=@RankId ");
			SqlParameter[] parameters = {
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@RankId", SqlDbType.Int,4)};
			parameters[0].Value = model.Remark;
			parameters[1].Value = model.RuleId;
			parameters[2].Value = model.RankId;

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
		public bool Delete(int RuleId,int RankId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SalesUserRank ");
			strSql.Append(" where RuleId=@RuleId and RankId=@RankId ");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@RankId", SqlDbType.Int,4)			};
			parameters[0].Value = RuleId;
			parameters[1].Value = RankId;

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
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Sales.SalesUserRank GetModel(int RuleId,int RankId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 RuleId,RankId,Remark from Shop_SalesUserRank ");
			strSql.Append(" where RuleId=@RuleId and RankId=@RankId ");
			SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4),
					new SqlParameter("@RankId", SqlDbType.Int,4)			};
			parameters[0].Value = RuleId;
			parameters[1].Value = RankId;

			Maticsoft.Model.Shop.Sales.SalesUserRank model=new Maticsoft.Model.Shop.Sales.SalesUserRank();
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
		public Maticsoft.Model.Shop.Sales.SalesUserRank DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Sales.SalesUserRank model=new Maticsoft.Model.Shop.Sales.SalesUserRank();
			if (row != null)
			{
				if(row["RuleId"]!=null && row["RuleId"].ToString()!="")
				{
					model.RuleId=int.Parse(row["RuleId"].ToString());
				}
				if(row["RankId"]!=null && row["RankId"].ToString()!="")
				{
					model.RankId=int.Parse(row["RankId"].ToString());
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
			strSql.Append("select RuleId,RankId,Remark ");
			strSql.Append(" FROM Shop_SalesUserRank ");
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
			strSql.Append(" RuleId,RankId,Remark ");
			strSql.Append(" FROM Shop_SalesUserRank ");
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
			strSql.Append("select count(1) FROM Shop_SalesUserRank ");
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
				strSql.Append("order by T.RankId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_SalesUserRank T ");
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
			parameters[0].Value = "Shop_SalesUserRank";
			parameters[1].Value = "RankId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

	    public bool DeleteByRuleId(int ruleId)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_SalesUserRank ");
            strSql.Append(" where RuleId=@RuleId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RuleId", SqlDbType.Int,4)			};
            parameters[0].Value = ruleId;

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

