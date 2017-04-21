/**
* StarType.cs
*
* 功 能： N/A
* 类 名： StarType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:57   N/A    初版
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
using Maticsoft.IDAL.SNS;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.SNS
{
	/// <summary>
	/// 数据访问类:StarType
	/// </summary>
	public partial class StarType:IStarType
	{
		public StarType()
		{}
		#region  BasicMethod

		
		/// <summary>
		/// 是否存在该名称
		/// </summary>
        public bool Exists(string TypeName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_StarType");
            strSql.Append(" where TypeName=@TypeName");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,100)
			};
            parameters[0].Value = TypeName;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.StarType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_StarType(");
			strSql.Append("TypeName,CheckRule,Remark,Status)");
			strSql.Append(" values (");
			strSql.Append("@TypeName,@CheckRule,@Remark,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,100),
					new SqlParameter("@CheckRule", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.TypeName;
			parameters[1].Value = model.CheckRule;
			parameters[2].Value = model.Remark;
			parameters[3].Value = model.Status;

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
		public bool Update(Maticsoft.Model.SNS.StarType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_StarType set ");
			strSql.Append("TypeName=@TypeName,");
			strSql.Append("CheckRule=@CheckRule,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("Status=@Status");
			strSql.Append(" where TypeID=@TypeID");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,100),
					new SqlParameter("@CheckRule", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,200),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4)};
			parameters[0].Value = model.TypeName;
			parameters[1].Value = model.CheckRule;
			parameters[2].Value = model.Remark;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.TypeID;

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
		public bool Delete(int TypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_StarType ");
			strSql.Append(" where TypeID=@TypeID");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.Int,4)
			};
			parameters[0].Value = TypeID;

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
		public bool DeleteList(string TypeIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_StarType ");
			strSql.Append(" where TypeID in ("+TypeIDlist + ")  ");
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
		public Maticsoft.Model.SNS.StarType GetModel(int TypeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 TypeID,TypeName,CheckRule,Remark,Status from SNS_StarType ");
			strSql.Append(" where TypeID=@TypeID");
			SqlParameter[] parameters = {
					new SqlParameter("@TypeID", SqlDbType.Int,4)
			};
			parameters[0].Value = TypeID;

			Maticsoft.Model.SNS.StarType model=new Maticsoft.Model.SNS.StarType();
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
		public Maticsoft.Model.SNS.StarType DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.StarType model=new Maticsoft.Model.SNS.StarType();
			if (row != null)
			{
				if(row["TypeID"]!=null && row["TypeID"].ToString()!="")
				{
					model.TypeID=int.Parse(row["TypeID"].ToString());
				}
				if(row["TypeName"]!=null)
				{
					model.TypeName=row["TypeName"].ToString();
				}
				if(row["CheckRule"]!=null)
				{
					model.CheckRule=row["CheckRule"].ToString();
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
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
			strSql.Append("select TypeID,TypeName,CheckRule,Remark,Status ");
			strSql.Append(" FROM SNS_StarType ");
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
			strSql.Append(" TypeID,TypeName,CheckRule,Remark,Status ");
			strSql.Append(" FROM SNS_StarType ");
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
			strSql.Append("select count(1) FROM SNS_StarType ");
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
				strSql.Append("order by T.TypeID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_StarType T ");
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
			parameters[0].Value = "SNS_StarType";
			parameters[1].Value = "TypeID";
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

