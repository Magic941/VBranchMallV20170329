/**
* ProductSources.cs
*
* 功 能： N/A
* 类 名： ProductSources
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:50   N/A    初版
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
	/// 数据访问类:ProductSources
	/// </summary>
	public partial class ProductSources:IProductSources
	{
		public ProductSources()
		{}
		#region  BasicMethod

		
		/// <summary>
		/// 是否存在
		/// </summary>
        public bool Exists(string WebSiteName, string WebSiteUrl)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_ProductSources");
            strSql.Append(" where WebSiteName=@WebSiteName or WebSiteUrl=@WebSiteUrl");
			SqlParameter[] parameters = {
					new SqlParameter("@WebSiteName", SqlDbType.NVarChar,100),
					new SqlParameter("@WebSiteUrl", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = WebSiteName;
            parameters[1].Value = WebSiteUrl;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.ProductSources model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_ProductSources(");
			strSql.Append("WebSiteName,WebSiteUrl,WebSiteLogo,CategoryTags,PriceTags,ImagesTag,Status)");
			strSql.Append(" values (");
			strSql.Append("@WebSiteName,@WebSiteUrl,@WebSiteLogo,@CategoryTags,@PriceTags,@ImagesTag,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@WebSiteName", SqlDbType.NVarChar,100),
					new SqlParameter("@WebSiteUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@WebSiteLogo", SqlDbType.NVarChar,200),
					new SqlParameter("@CategoryTags", SqlDbType.NVarChar),
					new SqlParameter("@PriceTags", SqlDbType.NVarChar),
					new SqlParameter("@ImagesTag", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.WebSiteName;
			parameters[1].Value = model.WebSiteUrl;
			parameters[2].Value = model.WebSiteLogo;
			parameters[3].Value = model.CategoryTags;
			parameters[4].Value = model.PriceTags;
			parameters[5].Value = model.ImagesTag;
			parameters[6].Value = model.Status;

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
		public bool Update(Maticsoft.Model.SNS.ProductSources model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_ProductSources set ");
			strSql.Append("WebSiteName=@WebSiteName,");
			strSql.Append("WebSiteUrl=@WebSiteUrl,");
			strSql.Append("WebSiteLogo=@WebSiteLogo,");
			strSql.Append("CategoryTags=@CategoryTags,");
			strSql.Append("PriceTags=@PriceTags,");
			strSql.Append("ImagesTag=@ImagesTag,");
			strSql.Append("Status=@Status");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@WebSiteName", SqlDbType.NVarChar,100),
					new SqlParameter("@WebSiteUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@WebSiteLogo", SqlDbType.NVarChar,200),
					new SqlParameter("@CategoryTags", SqlDbType.NVarChar),
					new SqlParameter("@PriceTags", SqlDbType.NVarChar),
					new SqlParameter("@ImagesTag", SqlDbType.NVarChar),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.WebSiteName;
			parameters[1].Value = model.WebSiteUrl;
			parameters[2].Value = model.WebSiteLogo;
			parameters[3].Value = model.CategoryTags;
			parameters[4].Value = model.PriceTags;
			parameters[5].Value = model.ImagesTag;
			parameters[6].Value = model.Status;
			parameters[7].Value = model.ID;

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
		public bool Delete(int ID)
		{			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_ProductSources ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_ProductSources ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public Maticsoft.Model.SNS.ProductSources GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,WebSiteName,WebSiteUrl,WebSiteLogo,CategoryTags,PriceTags,ImagesTag,Status from SNS_ProductSources ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.SNS.ProductSources model=new Maticsoft.Model.SNS.ProductSources();
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
		public Maticsoft.Model.SNS.ProductSources DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.ProductSources model=new Maticsoft.Model.SNS.ProductSources();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["WebSiteName"]!=null)
				{
					model.WebSiteName=row["WebSiteName"].ToString();
				}
				if(row["WebSiteUrl"]!=null)
				{
					model.WebSiteUrl=row["WebSiteUrl"].ToString();
				}
				if(row["WebSiteLogo"]!=null)
				{
					model.WebSiteLogo=row["WebSiteLogo"].ToString();
				}
				if(row["CategoryTags"]!=null)
				{
					model.CategoryTags=row["CategoryTags"].ToString();
				}
				if(row["PriceTags"]!=null)
				{
					model.PriceTags=row["PriceTags"].ToString();
				}
				if(row["ImagesTag"]!=null)
				{
					model.ImagesTag=row["ImagesTag"].ToString();
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
			strSql.Append("select ID,WebSiteName,WebSiteUrl,WebSiteLogo,CategoryTags,PriceTags,ImagesTag,Status ");
			strSql.Append(" FROM SNS_ProductSources ");
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
			strSql.Append(" ID,WebSiteName,WebSiteUrl,WebSiteLogo,CategoryTags,PriceTags,ImagesTag,Status ");
			strSql.Append(" FROM SNS_ProductSources ");
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
			strSql.Append("select count(1) FROM SNS_ProductSources ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_ProductSources T ");
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
			parameters[0].Value = "SNS_ProductSources";
			parameters[1].Value = "ID";
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

