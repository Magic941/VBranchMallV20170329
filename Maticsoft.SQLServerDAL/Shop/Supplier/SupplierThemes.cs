/**
* SupplierThemes.cs
*
* 功 能： N/A
* 类 名： SupplierThemes
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:51   Ben    初版
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
using Maticsoft.IDAL.Shop.Supplier;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
	/// <summary>
	/// 数据访问类:SupplierThemes
	/// </summary>
	public partial class SupplierThemes:ISupplierThemes
	{
		public SupplierThemes()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ThemeId", "Shop_SupplierThemes"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ThemeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_SupplierThemes");
			strSql.Append(" where ThemeId=@ThemeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ThemeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ThemeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Supplier.SupplierThemes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_SupplierThemes(");
			strSql.Append("Name,Description,ImageUrl,Author,WebSite,Language,CreatedDate,UpdatedDate,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Name,@Description,@ImageUrl,@Author,@WebSite,@Language,@CreatedDate,@UpdatedDate,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,100),
					new SqlParameter("@Author", SqlDbType.NVarChar,100),
					new SqlParameter("@WebSite", SqlDbType.NVarChar,200),
					new SqlParameter("@Language", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.ImageUrl;
			parameters[3].Value = model.Author;
			parameters[4].Value = model.WebSite;
			parameters[5].Value = model.Language;
			parameters[6].Value = model.CreatedDate;
			parameters[7].Value = model.UpdatedDate;
			parameters[8].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.Shop.Supplier.SupplierThemes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_SupplierThemes set ");
			strSql.Append("Name=@Name,");
			strSql.Append("Description=@Description,");
			strSql.Append("ImageUrl=@ImageUrl,");
			strSql.Append("Author=@Author,");
			strSql.Append("WebSite=@WebSite,");
			strSql.Append("Language=@Language,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("UpdatedDate=@UpdatedDate,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ThemeId=@ThemeId");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Description", SqlDbType.NVarChar,200),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,100),
					new SqlParameter("@Author", SqlDbType.NVarChar,100),
					new SqlParameter("@WebSite", SqlDbType.NVarChar,200),
					new SqlParameter("@Language", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@ThemeId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Description;
			parameters[2].Value = model.ImageUrl;
			parameters[3].Value = model.Author;
			parameters[4].Value = model.WebSite;
			parameters[5].Value = model.Language;
			parameters[6].Value = model.CreatedDate;
			parameters[7].Value = model.UpdatedDate;
			parameters[8].Value = model.Remark;
			parameters[9].Value = model.ThemeId;

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
		public bool Delete(int ThemeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SupplierThemes ");
			strSql.Append(" where ThemeId=@ThemeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ThemeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ThemeId;

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
		public bool DeleteList(string ThemeIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SupplierThemes ");
			strSql.Append(" where ThemeId in ("+ThemeIdlist + ")  ");
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
		public Maticsoft.Model.Shop.Supplier.SupplierThemes GetModel(int ThemeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ThemeId,Name,Description,ImageUrl,Author,WebSite,Language,CreatedDate,UpdatedDate,Remark from Shop_SupplierThemes ");
			strSql.Append(" where ThemeId=@ThemeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ThemeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ThemeId;

			Maticsoft.Model.Shop.Supplier.SupplierThemes model=new Maticsoft.Model.Shop.Supplier.SupplierThemes();
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
		public Maticsoft.Model.Shop.Supplier.SupplierThemes DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Supplier.SupplierThemes model=new Maticsoft.Model.Shop.Supplier.SupplierThemes();
			if (row != null)
			{
				if(row["ThemeId"]!=null && row["ThemeId"].ToString()!="")
				{
					model.ThemeId=int.Parse(row["ThemeId"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["ImageUrl"]!=null)
				{
					model.ImageUrl=row["ImageUrl"].ToString();
				}
				if(row["Author"]!=null)
				{
					model.Author=row["Author"].ToString();
				}
				if(row["WebSite"]!=null)
				{
					model.WebSite=row["WebSite"].ToString();
				}
				if(row["Language"]!=null)
				{
					model.Language=row["Language"].ToString();
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["UpdatedDate"]!=null && row["UpdatedDate"].ToString()!="")
				{
					model.UpdatedDate=DateTime.Parse(row["UpdatedDate"].ToString());
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
			strSql.Append("select ThemeId,Name,Description,ImageUrl,Author,WebSite,Language,CreatedDate,UpdatedDate,Remark ");
			strSql.Append(" FROM Shop_SupplierThemes ");
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
			strSql.Append(" ThemeId,Name,Description,ImageUrl,Author,WebSite,Language,CreatedDate,UpdatedDate,Remark ");
			strSql.Append(" FROM Shop_SupplierThemes ");
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
			strSql.Append("select count(1) FROM Shop_SupplierThemes ");
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
				strSql.Append("order by T.ThemeId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_SupplierThemes T ");
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
			parameters[0].Value = "Shop_SupplierThemes";
			parameters[1].Value = "ThemeId";
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

