/**
* VisiteLogs.cs
*
* 功 能： N/A
* 类 名： VisiteLogs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:09   N/A    初版
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
	/// 数据访问类:VisiteLogs
	/// </summary>
	public partial class VisiteLogs:IVisiteLogs
	{
		public VisiteLogs()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.SNS.VisiteLogs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SNS_VisiteLogs(");
			strSql.Append("FromUserID,FromNickName,ToUserID,ToNickName,VisitTimes,LastVisitTime)");
			strSql.Append(" values (");
			strSql.Append("@FromUserID,@FromNickName,@ToUserID,@ToNickName,@VisitTimes,@LastVisitTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@FromUserID", SqlDbType.Int,4),
					new SqlParameter("@FromNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ToUserID", SqlDbType.Int,4),
					new SqlParameter("@ToNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@VisitTimes", SqlDbType.Int,4),
					new SqlParameter("@LastVisitTime", SqlDbType.DateTime)};
			parameters[0].Value = model.FromUserID;
			parameters[1].Value = model.FromNickName;
			parameters[2].Value = model.ToUserID;
			parameters[3].Value = model.ToNickName;
			parameters[4].Value = model.VisitTimes;
			parameters[5].Value = model.LastVisitTime;

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
		public bool Update(Maticsoft.Model.SNS.VisiteLogs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SNS_VisiteLogs set ");
			strSql.Append("FromUserID=@FromUserID,");
			strSql.Append("FromNickName=@FromNickName,");
			strSql.Append("ToUserID=@ToUserID,");
			strSql.Append("ToNickName=@ToNickName,");
			strSql.Append("VisitTimes=@VisitTimes,");
			strSql.Append("LastVisitTime=@LastVisitTime");
			strSql.Append(" where VisitID=@VisitID");
			SqlParameter[] parameters = {
					new SqlParameter("@FromUserID", SqlDbType.Int,4),
					new SqlParameter("@FromNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@ToUserID", SqlDbType.Int,4),
					new SqlParameter("@ToNickName", SqlDbType.NVarChar,50),
					new SqlParameter("@VisitTimes", SqlDbType.Int,4),
					new SqlParameter("@LastVisitTime", SqlDbType.DateTime),
					new SqlParameter("@VisitID", SqlDbType.Int,4)};
			parameters[0].Value = model.FromUserID;
			parameters[1].Value = model.FromNickName;
			parameters[2].Value = model.ToUserID;
			parameters[3].Value = model.ToNickName;
			parameters[4].Value = model.VisitTimes;
			parameters[5].Value = model.LastVisitTime;
			parameters[6].Value = model.VisitID;

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
		public bool Delete(int VisitID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_VisiteLogs ");
			strSql.Append(" where VisitID=@VisitID");
			SqlParameter[] parameters = {
					new SqlParameter("@VisitID", SqlDbType.Int,4)
			};
			parameters[0].Value = VisitID;

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
		public bool DeleteList(string VisitIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SNS_VisiteLogs ");
			strSql.Append(" where VisitID in ("+VisitIDlist + ")  ");
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
		public Maticsoft.Model.SNS.VisiteLogs GetModel(int VisitID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 VisitID,FromUserID,FromNickName,ToUserID,ToNickName,VisitTimes,LastVisitTime from SNS_VisiteLogs ");
			strSql.Append(" where VisitID=@VisitID");
			SqlParameter[] parameters = {
					new SqlParameter("@VisitID", SqlDbType.Int,4)
			};
			parameters[0].Value = VisitID;

			Maticsoft.Model.SNS.VisiteLogs model=new Maticsoft.Model.SNS.VisiteLogs();
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
		public Maticsoft.Model.SNS.VisiteLogs DataRowToModel(DataRow row)
		{
			Maticsoft.Model.SNS.VisiteLogs model=new Maticsoft.Model.SNS.VisiteLogs();
			if (row != null)
			{
				if(row["VisitID"]!=null && row["VisitID"].ToString()!="")
				{
					model.VisitID=int.Parse(row["VisitID"].ToString());
				}
				if(row["FromUserID"]!=null && row["FromUserID"].ToString()!="")
				{
					model.FromUserID=int.Parse(row["FromUserID"].ToString());
				}
				if(row["FromNickName"]!=null)
				{
					model.FromNickName=row["FromNickName"].ToString();
				}
				if(row["ToUserID"]!=null && row["ToUserID"].ToString()!="")
				{
					model.ToUserID=int.Parse(row["ToUserID"].ToString());
				}
				if(row["ToNickName"]!=null)
				{
					model.ToNickName=row["ToNickName"].ToString();
				}
				if(row["VisitTimes"]!=null && row["VisitTimes"].ToString()!="")
				{
					model.VisitTimes=int.Parse(row["VisitTimes"].ToString());
				}
				if(row["LastVisitTime"]!=null && row["LastVisitTime"].ToString()!="")
				{
					model.LastVisitTime=DateTime.Parse(row["LastVisitTime"].ToString());
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
			strSql.Append("select VisitID,FromUserID,FromNickName,ToUserID,ToNickName,VisitTimes,LastVisitTime ");
			strSql.Append(" FROM SNS_VisiteLogs ");
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
			strSql.Append(" VisitID,FromUserID,FromNickName,ToUserID,ToNickName,VisitTimes,LastVisitTime ");
			strSql.Append(" FROM SNS_VisiteLogs ");
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
			strSql.Append("select count(1) FROM SNS_VisiteLogs ");
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
				strSql.Append("order by T.VisitID desc");
			}
			strSql.Append(")AS Row, T.*  from SNS_VisiteLogs T ");
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
			parameters[0].Value = "SNS_VisiteLogs";
			parameters[1].Value = "VisitID";
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

