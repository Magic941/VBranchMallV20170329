﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Package;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Package
{
	/// <summary>
	/// 数据访问类:PackageCategory
	/// </summary>
	public partial class PackageCategory:IPackageCategory
	{
		public PackageCategory()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CategoryId", "Shop_PackageCategory"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CategoryId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_PackageCategory");
			strSql.Append(" where CategoryId=@CategoryId");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Package.PackageCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_PackageCategory(");
			strSql.Append("Name,CreatedDate,Status,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Name,@CreatedDate,@Status,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.CreatedDate;
			parameters[2].Value = model.Status;
			parameters[3].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.Shop.Package.PackageCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_PackageCategory set ");
			strSql.Append("Name=@Name,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Status=@Status,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where CategoryId=@CategoryId");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@CategoryId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.CreatedDate;
			parameters[2].Value = model.Status;
			parameters[3].Value = model.Remark;
			parameters[4].Value = model.CategoryId;

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
		public bool Delete(int CategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_PackageCategory ");
			strSql.Append(" where CategoryId=@CategoryId");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryId;

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
		public bool DeleteList(string CategoryIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_PackageCategory ");
			strSql.Append(" where CategoryId in ("+CategoryIdlist + ")  ");
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
		public Maticsoft.Model.Shop.Package.PackageCategory GetModel(int CategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CategoryId,Name,CreatedDate,Status,Remark from Shop_PackageCategory ");
			strSql.Append(" where CategoryId=@CategoryId");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryId;

			Maticsoft.Model.Shop.Package.PackageCategory model=new Maticsoft.Model.Shop.Package.PackageCategory();
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
		public Maticsoft.Model.Shop.Package.PackageCategory DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Package.PackageCategory model=new Maticsoft.Model.Shop.Package.PackageCategory();
			if (row != null)
			{
				if(row["CategoryId"]!=null && row["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(row["CategoryId"].ToString());
				}
				if(row["Name"]!=null && row["Name"].ToString()!="")
				{
					model.Name=row["Name"].ToString();
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["Remark"]!=null && row["Remark"].ToString()!="")
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
			strSql.Append("select CategoryId,Name,CreatedDate,Status,Remark ");
			strSql.Append(" FROM Shop_PackageCategory ");
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
			strSql.Append(" CategoryId,Name,CreatedDate,Status,Remark ");
			strSql.Append(" FROM Shop_PackageCategory ");
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
			strSql.Append("select count(1) FROM Shop_PackageCategory ");
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
				strSql.Append("order by T.CategoryId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_PackageCategory T ");
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
			parameters[0].Value = "Shop_PackageCategory";
			parameters[1].Value = "CategoryId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

