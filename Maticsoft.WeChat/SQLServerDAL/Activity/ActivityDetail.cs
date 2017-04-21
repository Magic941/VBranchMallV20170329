/**  版本信息模板在安装目录下，可自行修改。
* ActivityDetail.cs
*
* 功 能： N/A
* 类 名： ActivityDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:16   N/A    初版
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
using Maticsoft.WeChat.IDAL.Activity;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.WeChat.SQLServerDAL.Activity
{
	/// <summary>
	/// 数据访问类:ActivityDetail
	/// </summary>
	public partial class ActivityDetail:IActivityDetail
	{
		public ActivityDetail()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long DetailId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WeChat_ActivityDetail");
			strSql.Append(" where DetailId=@DetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.BigInt)
			};
			parameters[0].Value = DetailId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(Maticsoft.WeChat.Model.Activity.ActivityDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into WeChat_ActivityDetail(");
			strSql.Append("UserName,ActivityId,ActivityName,CreateDate,Remark)");
			strSql.Append(" values (");
			strSql.Append("@UserName,@ActivityId,@ActivityName,@CreateDate,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@ActivityId", SqlDbType.Int,4),
					new SqlParameter("@ActivityName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.ActivityId;
			parameters[2].Value = model.ActivityName;
			parameters[3].Value = model.CreateDate;
			parameters[4].Value = model.Remark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt64(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Activity.ActivityDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update WeChat_ActivityDetail set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("ActivityId=@ActivityId,");
			strSql.Append("ActivityName=@ActivityName,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where DetailId=@DetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar,200),
					new SqlParameter("@ActivityId", SqlDbType.Int,4),
					new SqlParameter("@ActivityName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,-1),
					new SqlParameter("@DetailId", SqlDbType.BigInt,8)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.ActivityId;
			parameters[2].Value = model.ActivityName;
			parameters[3].Value = model.CreateDate;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.DetailId;

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
		public bool Delete(long DetailId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WeChat_ActivityDetail ");
			strSql.Append(" where DetailId=@DetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.BigInt)
			};
			parameters[0].Value = DetailId;

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
		public bool DeleteList(string DetailIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WeChat_ActivityDetail ");
			strSql.Append(" where DetailId in ("+DetailIdlist + ")  ");
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
		public Maticsoft.WeChat.Model.Activity.ActivityDetail GetModel(long DetailId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 DetailId,UserName,ActivityId,ActivityName,CreateDate,Remark from WeChat_ActivityDetail ");
			strSql.Append(" where DetailId=@DetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.BigInt)
			};
			parameters[0].Value = DetailId;

			Maticsoft.WeChat.Model.Activity.ActivityDetail model=new Maticsoft.WeChat.Model.Activity.ActivityDetail();
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
		public Maticsoft.WeChat.Model.Activity.ActivityDetail DataRowToModel(DataRow row)
		{
			Maticsoft.WeChat.Model.Activity.ActivityDetail model=new Maticsoft.WeChat.Model.Activity.ActivityDetail();
			if (row != null)
			{
				if(row["DetailId"]!=null && row["DetailId"].ToString()!="")
				{
					model.DetailId=long.Parse(row["DetailId"].ToString());
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["ActivityId"]!=null && row["ActivityId"].ToString()!="")
				{
					model.ActivityId=int.Parse(row["ActivityId"].ToString());
				}
				if(row["ActivityName"]!=null)
				{
					model.ActivityName=row["ActivityName"].ToString();
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
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
			strSql.Append("select DetailId,UserName,ActivityId,ActivityName,CreateDate,Remark ");
			strSql.Append(" FROM WeChat_ActivityDetail ");
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
			strSql.Append(" DetailId,UserName,ActivityId,ActivityName,CreateDate,Remark ");
			strSql.Append(" FROM WeChat_ActivityDetail ");
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
			strSql.Append("select count(1) FROM WeChat_ActivityDetail ");
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
				strSql.Append("order by T.DetailId desc");
			}
			strSql.Append(")AS Row, T.*  from WeChat_ActivityDetail T ");
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
			parameters[0].Value = "WeChat_ActivityDetail";
			parameters[1].Value = "DetailId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        public int GetEachCount(int activityId, string userName)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("SELECT Count(1)  FROM WeChat_ActivityDetail WHERE ActivityId=@ActivityId and UserName=@UserName and  datediff( day, CreateDate, GETDATE())<1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = activityId;
            parameters[1].Value = userName;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
        }


        public int GetDayTotal(int activityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Count(1)  FROM WeChat_ActivityDetail WHERE ActivityId=@ActivityId   and  datediff( day, CreateDate, GETDATE())<1 ");
            SqlParameter[] parameters = {
					new SqlParameter("@ActivityId", SqlDbType.Int,4)
                     
			};
            parameters[0].Value = activityId;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        
		#endregion  ExtensionMethod
	}
}

