/**
* UserShipCategories.cs
*
* 功 能： N/A
* 类 名： UserShipCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:07   N/A    初版
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
	/// 数据访问类:UserShipCategories
	/// </summary>
	public partial class UserShipCategories:IUserShipCategories
	{
		public UserShipCategories()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CategoryID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SNS_UserShipCategories");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.UserShipCategories model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_UserShipCategories(");
			strSql.Append("CreatedUserID,CategoryName,Description,Sequence,LastUpdatedDate,CreatedDate,Privacy)");
			strSql.Append(" values (");
			strSql.Append("@CreatedUserID,@CategoryName,@Description,@Sequence,@LastUpdatedDate,@CreatedDate,@Privacy)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Privacy", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.CreatedUserID;
			parameters[1].Value = model.CategoryName;
			parameters[2].Value = model.Description;
			parameters[3].Value = model.Sequence;
			parameters[4].Value = model.LastUpdatedDate;
			parameters[5].Value = model.CreatedDate;
			parameters[6].Value = model.Privacy;

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
		public bool Update(Maticsoft.Model.SNS.UserShipCategories model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_UserShipCategories set ");
			strSql.Append("CreatedUserID=@CreatedUserID,");
			strSql.Append("CategoryName=@CategoryName,");
			strSql.Append("Description=@Description,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("LastUpdatedDate=@LastUpdatedDate,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Privacy=@Privacy");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
					new SqlParameter("@CategoryID", SqlDbType.Int,4)};
			parameters[0].Value = model.CreatedUserID;
			parameters[1].Value = model.CategoryName;
			parameters[2].Value = model.Description;
			parameters[3].Value = model.Sequence;
			parameters[4].Value = model.LastUpdatedDate;
			parameters[5].Value = model.CreatedDate;
			parameters[6].Value = model.Privacy;
			parameters[7].Value = model.CategoryID;

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
		public bool Delete(int CategoryID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_UserShipCategories ");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

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
		public bool DeleteList(string CategoryIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_UserShipCategories ");
			strSql.Append(" where CategoryID in ("+CategoryIDlist + ")  ");
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
		public Maticsoft.Model.SNS.UserShipCategories GetModel(int CategoryID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CategoryID,CreatedUserID,CategoryName,Description,Sequence,LastUpdatedDate,CreatedDate,Privacy from SNS_UserShipCategories ");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryID", SqlDbType.Int,4)
			};
			parameters[0].Value = CategoryID;

			Maticsoft.Model.SNS.UserShipCategories model=new Maticsoft.Model.SNS.UserShipCategories();
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
		public Maticsoft.Model.SNS.UserShipCategories DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.UserShipCategories model=new Maticsoft.Model.SNS.UserShipCategories();
			if (row != null)
			{
				if(row["CategoryID"]!=null && row["CategoryID"].ToString()!="")
				{
					model.CategoryID=int.Parse(row["CategoryID"].ToString());
				}
				if(row["CreatedUserID"]!=null && row["CreatedUserID"].ToString()!="")
				{
					model.CreatedUserID=int.Parse(row["CreatedUserID"].ToString());
				}
				if(row["CategoryName"]!=null)
				{
					model.CategoryName=row["CategoryName"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["Sequence"]!=null && row["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(row["Sequence"].ToString());
				}
				if(row["LastUpdatedDate"]!=null && row["LastUpdatedDate"].ToString()!="")
				{
					model.LastUpdatedDate=DateTime.Parse(row["LastUpdatedDate"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["Privacy"]!=null && row["Privacy"].ToString()!="")
				{
					model.Privacy=int.Parse(row["Privacy"].ToString());
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
			strSql.Append("select CategoryID,CreatedUserID,CategoryName,Description,Sequence,LastUpdatedDate,CreatedDate,Privacy ");
			strSql.Append(" FROM SNS_UserShipCategories ");
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
			strSql.Append(" CategoryID,CreatedUserID,CategoryName,Description,Sequence,LastUpdatedDate,CreatedDate,Privacy ");
			strSql.Append(" FROM SNS_UserShipCategories ");
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
			strSql.Append("select count(1) FROM SNS_UserShipCategories ");
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
				strSql.Append("order by T.CategoryID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_UserShipCategories T ");
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
			parameters[0].Value = "SNS_UserShipCategories";
			parameters[1].Value = "CategoryID";
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

