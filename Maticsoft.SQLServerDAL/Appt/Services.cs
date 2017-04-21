/**  版本信息模板在安装目录下，可自行修改。
* Services.cs
*
* 功 能： N/A
* 类 名： Services
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/2 17:36:20   N/A    初版
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
using Maticsoft.IDAL.Appt;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Appt
{
	/// <summary>
	/// 数据访问类:Services
	/// </summary>
	public partial class Services:IServices
	{
		public Services()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ServiceId", "Appt_Services"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ServiceId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Appt_Services");
			strSql.Append(" where ServiceId=@ServiceId");
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceId", SqlDbType.Int,4)
			};
			parameters[0].Value = ServiceId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Appt.Services model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Appt_Services(");
			strSql.Append("Name,StartDate,EndDate,IsPay,ServiceType,RuleType,MaxCount,Summary,Description,ImageUrl,ThumbnailUrl,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Name,@StartDate,@EndDate,@IsPay,@ServiceType,@RuleType,@MaxCount,@Summary,@Description,@ImageUrl,@ThumbnailUrl,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@IsPay", SqlDbType.Bit,1),
					new SqlParameter("@ServiceType", SqlDbType.SmallInt,2),
					new SqlParameter("@RuleType", SqlDbType.SmallInt,2),
					new SqlParameter("@MaxCount", SqlDbType.SmallInt,2),
					new SqlParameter("@Summary", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@ThumbnailUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.StartDate;
			parameters[2].Value = model.EndDate;
			parameters[3].Value = model.IsPay;
			parameters[4].Value = model.ServiceType;
			parameters[5].Value = model.RuleType;
			parameters[6].Value = model.MaxCount;
			parameters[7].Value = model.Summary;
			parameters[8].Value = model.Description;
			parameters[9].Value = model.ImageUrl;
			parameters[10].Value = model.ThumbnailUrl;
			parameters[11].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.Appt.Services model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Appt_Services set ");
			strSql.Append("Name=@Name,");
			strSql.Append("StartDate=@StartDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("IsPay=@IsPay,");
			strSql.Append("ServiceType=@ServiceType,");
			strSql.Append("RuleType=@RuleType,");
			strSql.Append("MaxCount=@MaxCount,");
			strSql.Append("Summary=@Summary,");
			strSql.Append("Description=@Description,");
			strSql.Append("ImageUrl=@ImageUrl,");
			strSql.Append("ThumbnailUrl=@ThumbnailUrl,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ServiceId=@ServiceId");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@IsPay", SqlDbType.Bit,1),
					new SqlParameter("@ServiceType", SqlDbType.SmallInt,2),
					new SqlParameter("@RuleType", SqlDbType.SmallInt,2),
					new SqlParameter("@MaxCount", SqlDbType.SmallInt,2),
					new SqlParameter("@Summary", SqlDbType.NVarChar,200),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@ThumbnailUrl", SqlDbType.NVarChar,255),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@ServiceId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.StartDate;
			parameters[2].Value = model.EndDate;
			parameters[3].Value = model.IsPay;
			parameters[4].Value = model.ServiceType;
			parameters[5].Value = model.RuleType;
			parameters[6].Value = model.MaxCount;
			parameters[7].Value = model.Summary;
			parameters[8].Value = model.Description;
			parameters[9].Value = model.ImageUrl;
			parameters[10].Value = model.ThumbnailUrl;
			parameters[11].Value = model.Remark;
			parameters[12].Value = model.ServiceId;

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
		public bool Delete(int ServiceId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Appt_Services ");
			strSql.Append(" where ServiceId=@ServiceId");
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceId", SqlDbType.Int,4)
			};
			parameters[0].Value = ServiceId;

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
		public bool DeleteList(string ServiceIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Appt_Services ");
			strSql.Append(" where ServiceId in ("+ServiceIdlist + ")  ");
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
		public Maticsoft.Model.Appt.Services GetModel(int ServiceId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ServiceId,Name,StartDate,EndDate,IsPay,ServiceType,RuleType,MaxCount,Summary,Description,ImageUrl,ThumbnailUrl,Remark from Appt_Services ");
			strSql.Append(" where ServiceId=@ServiceId");
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceId", SqlDbType.Int,4)
			};
			parameters[0].Value = ServiceId;

			Maticsoft.Model.Appt.Services model=new Maticsoft.Model.Appt.Services();
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
		public Maticsoft.Model.Appt.Services DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Appt.Services model=new Maticsoft.Model.Appt.Services();
			if (row != null)
			{
				if(row["ServiceId"]!=null && row["ServiceId"].ToString()!="")
				{
					model.ServiceId=int.Parse(row["ServiceId"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["StartDate"]!=null && row["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(row["StartDate"].ToString());
				}
				if(row["EndDate"]!=null && row["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(row["EndDate"].ToString());
				}
				if(row["IsPay"]!=null && row["IsPay"].ToString()!="")
				{
					if((row["IsPay"].ToString()=="1")||(row["IsPay"].ToString().ToLower()=="true"))
					{
						model.IsPay=true;
					}
					else
					{
						model.IsPay=false;
					}
				}
				if(row["ServiceType"]!=null && row["ServiceType"].ToString()!="")
				{
					model.ServiceType=int.Parse(row["ServiceType"].ToString());
				}
				if(row["RuleType"]!=null && row["RuleType"].ToString()!="")
				{
					model.RuleType=int.Parse(row["RuleType"].ToString());
				}
				if(row["MaxCount"]!=null && row["MaxCount"].ToString()!="")
				{
					model.MaxCount=int.Parse(row["MaxCount"].ToString());
				}
				if(row["Summary"]!=null)
				{
					model.Summary=row["Summary"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["ImageUrl"]!=null)
				{
					model.ImageUrl=row["ImageUrl"].ToString();
				}
				if(row["ThumbnailUrl"]!=null)
				{
					model.ThumbnailUrl=row["ThumbnailUrl"].ToString();
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
			strSql.Append("select ServiceId,Name,StartDate,EndDate,IsPay,ServiceType,RuleType,MaxCount,Summary,Description,ImageUrl,ThumbnailUrl,Remark ");
			strSql.Append(" FROM Appt_Services ");
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
			strSql.Append(" ServiceId,Name,StartDate,EndDate,IsPay,ServiceType,RuleType,MaxCount,Summary,Description,ImageUrl,ThumbnailUrl,Remark ");
			strSql.Append(" FROM Appt_Services ");
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
			strSql.Append("select count(1) FROM Appt_Services ");
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
				strSql.Append("order by T.ServiceId desc");
			}
			strSql.Append(")AS Row, T.*  from Appt_Services T ");
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
			parameters[0].Value = "Appt_Services";
			parameters[1].Value = "ServiceId";
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

