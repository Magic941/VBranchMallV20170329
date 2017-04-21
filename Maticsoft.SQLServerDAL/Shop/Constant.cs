/**
* Constant.cs
*
* 功 能： N/A
* 类 名： Constant
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/7 12:27:59   N/A    初版
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
using Maticsoft.IDAL.Shop;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop
{
	/// <summary>
	/// 数据访问类:Constant
	/// </summary>
	public partial class Constant:IConstant
	{
		public Constant()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Shop.Constant model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_Constant(");
			strSql.Append("Type,DataDate,MaxValue,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Type,@DataDate,@MaxValue,@Remark)");
			SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@DataDate", SqlDbType.DateTime),
					new SqlParameter("@MaxValue", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300)};
			parameters[0].Value = model.Type;
			parameters[1].Value = model.DataDate;
			parameters[2].Value = model.MaxValue;
			parameters[3].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.Shop.Constant model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_Constant set ");
			strSql.Append("Type=@Type,");
			strSql.Append("DataDate=@DataDate,");
			strSql.Append("MaxValue=@MaxValue,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@DataDate", SqlDbType.DateTime),
					new SqlParameter("@MaxValue", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300)};
			parameters[0].Value = model.Type;
			parameters[1].Value = model.DataDate;
			parameters[2].Value = model.MaxValue;
			parameters[3].Value = model.Remark;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_Constant ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

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
		public Maticsoft.Model.Shop.Constant GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Type,DataDate,MaxValue,Remark from Shop_Constant ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			Maticsoft.Model.Shop.Constant model=new Maticsoft.Model.Shop.Constant();
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
		public Maticsoft.Model.Shop.Constant DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Constant model=new Maticsoft.Model.Shop.Constant();
			if (row != null)
			{
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["DataDate"]!=null && row["DataDate"].ToString()!="")
				{
					model.DataDate=DateTime.Parse(row["DataDate"].ToString());
				}
				if(row["MaxValue"]!=null && row["MaxValue"].ToString()!="")
				{
					model.MaxValue=int.Parse(row["MaxValue"].ToString());
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
			strSql.Append("select Type,DataDate,MaxValue,Remark ");
			strSql.Append(" FROM Shop_Constant ");
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
			strSql.Append(" Type,DataDate,MaxValue,Remark ");
			strSql.Append(" FROM Shop_Constant ");
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
			strSql.Append("select count(1) FROM Shop_Constant ");
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
				strSql.Append("order by T.OrderId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_Constant T ");
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
			parameters[0].Value = "Shop_Constant";
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

