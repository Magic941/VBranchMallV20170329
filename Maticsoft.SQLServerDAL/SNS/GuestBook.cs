/**
* GuestBook.cs
*
* 功 能： N/A
* 类 名： GuestBook
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:45   N/A    初版
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
	/// 数据访问类:GuestBook
	/// </summary>
	public partial class GuestBook:IGuestBook
	{
		public GuestBook()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.GuestBook model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_GuestBook(");
			strSql.Append("CreateUserID,CreateNickName,ToUserID,ToNickName,ParentID,Description,UserIP,Privacy,CreatedDate,Email,Path,Depth)");
			strSql.Append(" values (");
			strSql.Append("@CreateUserID,@CreateNickName,@ToUserID,@ToNickName,@ParentID,@Description,@UserIP,@Privacy,@CreatedDate,@Email,@Path,@Depth)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@CreateNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ToUserID", SqlDbType.Int,4),
					new SqlParameter("@ToNickName", SqlDbType.NVarChar,100),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@Path", SqlDbType.NVarChar,100),
					new SqlParameter("@Depth", SqlDbType.Int,4)};
			parameters[0].Value = model.CreateUserID;
			parameters[1].Value = model.CreateNickName;
			parameters[2].Value = model.ToUserID;
			parameters[3].Value = model.ToNickName;
			parameters[4].Value = model.ParentID;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.UserIP;
			parameters[7].Value = model.Privacy;
			parameters[8].Value = model.CreatedDate;
			parameters[9].Value = model.Email;
			parameters[10].Value = model.Path;
			parameters[11].Value = model.Depth;

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
		public bool Update(Maticsoft.Model.SNS.GuestBook model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_GuestBook set ");
			strSql.Append("CreateUserID=@CreateUserID,");
			strSql.Append("CreateNickName=@CreateNickName,");
			strSql.Append("ToUserID=@ToUserID,");
			strSql.Append("ToNickName=@ToNickName,");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("Description=@Description,");
			strSql.Append("UserIP=@UserIP,");
			strSql.Append("Privacy=@Privacy,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("Email=@Email,");
			strSql.Append("Path=@Path,");
			strSql.Append("Depth=@Depth");
			strSql.Append(" where GuestBookID=@GuestBookID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateUserID", SqlDbType.Int,4),
					new SqlParameter("@CreateNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ToUserID", SqlDbType.Int,4),
					new SqlParameter("@ToNickName", SqlDbType.NVarChar,100),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@UserIP", SqlDbType.NVarChar,15),
					new SqlParameter("@Privacy", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@Email", SqlDbType.NVarChar,100),
					new SqlParameter("@Path", SqlDbType.NVarChar,100),
					new SqlParameter("@Depth", SqlDbType.Int,4),
					new SqlParameter("@GuestBookID", SqlDbType.Int,4)};
			parameters[0].Value = model.CreateUserID;
			parameters[1].Value = model.CreateNickName;
			parameters[2].Value = model.ToUserID;
			parameters[3].Value = model.ToNickName;
			parameters[4].Value = model.ParentID;
			parameters[5].Value = model.Description;
			parameters[6].Value = model.UserIP;
			parameters[7].Value = model.Privacy;
			parameters[8].Value = model.CreatedDate;
			parameters[9].Value = model.Email;
			parameters[10].Value = model.Path;
			parameters[11].Value = model.Depth;
			parameters[12].Value = model.GuestBookID;

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
		public bool Delete(int GuestBookID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_GuestBook ");
			strSql.Append(" where GuestBookID=@GuestBookID");
			SqlParameter[] parameters = {
					new SqlParameter("@GuestBookID", SqlDbType.Int,4)
			};
			parameters[0].Value = GuestBookID;

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
		public bool DeleteList(string GuestBookIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_GuestBook ");
			strSql.Append(" where GuestBookID in ("+GuestBookIDlist + ")  ");
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
		public Maticsoft.Model.SNS.GuestBook GetModel(int GuestBookID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 GuestBookID,CreateUserID,CreateNickName,ToUserID,ToNickName,ParentID,Description,UserIP,Privacy,CreatedDate,Email,Path,Depth from SNS_GuestBook ");
			strSql.Append(" where GuestBookID=@GuestBookID");
			SqlParameter[] parameters = {
					new SqlParameter("@GuestBookID", SqlDbType.Int,4)
			};
			parameters[0].Value = GuestBookID;

			Maticsoft.Model.SNS.GuestBook model=new Maticsoft.Model.SNS.GuestBook();
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
		public Maticsoft.Model.SNS.GuestBook DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.GuestBook model=new Maticsoft.Model.SNS.GuestBook();
			if (row != null)
			{
				if(row["GuestBookID"]!=null && row["GuestBookID"].ToString()!="")
				{
					model.GuestBookID=int.Parse(row["GuestBookID"].ToString());
				}
				if(row["CreateUserID"]!=null && row["CreateUserID"].ToString()!="")
				{
					model.CreateUserID=int.Parse(row["CreateUserID"].ToString());
				}
				if(row["CreateNickName"]!=null)
				{
					model.CreateNickName=row["CreateNickName"].ToString();
				}
				if(row["ToUserID"]!=null && row["ToUserID"].ToString()!="")
				{
					model.ToUserID=int.Parse(row["ToUserID"].ToString());
				}
				if(row["ToNickName"]!=null)
				{
					model.ToNickName=row["ToNickName"].ToString();
				}
				if(row["ParentID"]!=null && row["ParentID"].ToString()!="")
				{
					model.ParentID=int.Parse(row["ParentID"].ToString());
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["UserIP"]!=null)
				{
					model.UserIP=row["UserIP"].ToString();
				}
				if(row["Privacy"]!=null && row["Privacy"].ToString()!="")
				{
					model.Privacy=int.Parse(row["Privacy"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["Email"]!=null)
				{
					model.Email=row["Email"].ToString();
				}
				if(row["Path"]!=null)
				{
					model.Path=row["Path"].ToString();
				}
				if(row["Depth"]!=null && row["Depth"].ToString()!="")
				{
					model.Depth=int.Parse(row["Depth"].ToString());
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
			strSql.Append("select GuestBookID,CreateUserID,CreateNickName,ToUserID,ToNickName,ParentID,Description,UserIP,Privacy,CreatedDate,Email,Path,Depth ");
			strSql.Append(" FROM SNS_GuestBook ");
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
			strSql.Append(" GuestBookID,CreateUserID,CreateNickName,ToUserID,ToNickName,ParentID,Description,UserIP,Privacy,CreatedDate,Email,Path,Depth ");
			strSql.Append(" FROM SNS_GuestBook ");
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
			strSql.Append("select count(1) FROM SNS_GuestBook ");
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
				strSql.Append("order by T.GuestBookID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_GuestBook T ");
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
			parameters[0].Value = "SNS_GuestBook";
			parameters[1].Value = "GuestBookID";
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

