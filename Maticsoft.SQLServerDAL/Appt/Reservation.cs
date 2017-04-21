/**  版本信息模板在安装目录下，可自行修改。
* Reservation.cs
*
* 功 能： N/A
* 类 名： Reservation
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/2 19:07:47   N/A    初版
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
	/// 数据访问类:Reservation
	/// </summary>
	public partial class Reservation:IReservation
	{
		public Reservation()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReservalId", "Appt_Reservation"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ReservalId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Appt_Reservation");
			strSql.Append(" where ReservalId=@ReservalId");
			SqlParameter[] parameters = {
					new SqlParameter("@ReservalId", SqlDbType.Int,4)
			};
			parameters[0].Value = ReservalId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Appt.Reservation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Appt_Reservation(");
			strSql.Append("Name,RegionId,ContactName,ContactPhone,ReservalDate,Content,Address,ContactEmail,Status,CreatedDate,CreatedUserId,SupplierId,ServiceId,OrderCode,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Name,@RegionId,@ContactName,@ContactPhone,@ReservalDate,@Content,@Address,@ContactEmail,@Status,@CreatedDate,@CreatedUserId,@SupplierId,@ServiceId,@OrderCode,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@ContactName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@ReservalDate", SqlDbType.DateTime),
					new SqlParameter("@Content", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@ContactEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ServiceId", SqlDbType.Int,4),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.RegionId;
			parameters[2].Value = model.ContactName;
			parameters[3].Value = model.ContactPhone;
			parameters[4].Value = model.ReservalDate;
			parameters[5].Value = model.Content;
			parameters[6].Value = model.Address;
			parameters[7].Value = model.ContactEmail;
			parameters[8].Value = model.Status;
			parameters[9].Value = model.CreatedDate;
			parameters[10].Value = model.CreatedUserId;
			parameters[11].Value = model.SupplierId;
			parameters[12].Value = model.ServiceId;
			parameters[13].Value = model.OrderCode;
			parameters[14].Value = model.Remark;

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
		public bool Update(Maticsoft.Model.Appt.Reservation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Appt_Reservation set ");
			strSql.Append("Name=@Name,");
			strSql.Append("RegionId=@RegionId,");
			strSql.Append("ContactName=@ContactName,");
			strSql.Append("ContactPhone=@ContactPhone,");
			strSql.Append("ReservalDate=@ReservalDate,");
			strSql.Append("Content=@Content,");
			strSql.Append("Address=@Address,");
			strSql.Append("ContactEmail=@ContactEmail,");
			strSql.Append("Status=@Status,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CreatedUserId=@CreatedUserId,");
			strSql.Append("SupplierId=@SupplierId,");
			strSql.Append("ServiceId=@ServiceId,");
			strSql.Append("OrderCode=@OrderCode,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ReservalId=@ReservalId");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionId", SqlDbType.Int,4),
					new SqlParameter("@ContactName", SqlDbType.NVarChar,50),
					new SqlParameter("@ContactPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@ReservalDate", SqlDbType.DateTime),
					new SqlParameter("@Content", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@ContactEmail", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@ServiceId", SqlDbType.Int,4),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@ReservalId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.RegionId;
			parameters[2].Value = model.ContactName;
			parameters[3].Value = model.ContactPhone;
			parameters[4].Value = model.ReservalDate;
			parameters[5].Value = model.Content;
			parameters[6].Value = model.Address;
			parameters[7].Value = model.ContactEmail;
			parameters[8].Value = model.Status;
			parameters[9].Value = model.CreatedDate;
			parameters[10].Value = model.CreatedUserId;
			parameters[11].Value = model.SupplierId;
			parameters[12].Value = model.ServiceId;
			parameters[13].Value = model.OrderCode;
			parameters[14].Value = model.Remark;
			parameters[15].Value = model.ReservalId;

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
		public bool Delete(int ReservalId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Appt_Reservation ");
			strSql.Append(" where ReservalId=@ReservalId");
			SqlParameter[] parameters = {
					new SqlParameter("@ReservalId", SqlDbType.Int,4)
			};
			parameters[0].Value = ReservalId;

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
		public bool DeleteList(string ReservalIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Appt_Reservation ");
			strSql.Append(" where ReservalId in ("+ReservalIdlist + ")  ");
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
		public Maticsoft.Model.Appt.Reservation GetModel(int ReservalId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ReservalId,Name,RegionId,ContactName,ContactPhone,ReservalDate,Content,Address,ContactEmail,Status,CreatedDate,CreatedUserId,SupplierId,ServiceId,OrderCode,Remark from Appt_Reservation ");
			strSql.Append(" where ReservalId=@ReservalId");
			SqlParameter[] parameters = {
					new SqlParameter("@ReservalId", SqlDbType.Int,4)
			};
			parameters[0].Value = ReservalId;

			Maticsoft.Model.Appt.Reservation model=new Maticsoft.Model.Appt.Reservation();
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
		public Maticsoft.Model.Appt.Reservation DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Appt.Reservation model=new Maticsoft.Model.Appt.Reservation();
			if (row != null)
			{
				if(row["ReservalId"]!=null && row["ReservalId"].ToString()!="")
				{
					model.ReservalId=int.Parse(row["ReservalId"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["RegionId"]!=null && row["RegionId"].ToString()!="")
				{
					model.RegionId=int.Parse(row["RegionId"].ToString());
				}
				if(row["ContactName"]!=null)
				{
					model.ContactName=row["ContactName"].ToString();
				}
				if(row["ContactPhone"]!=null)
				{
					model.ContactPhone=row["ContactPhone"].ToString();
				}
				if(row["ReservalDate"]!=null && row["ReservalDate"].ToString()!="")
				{
					model.ReservalDate=DateTime.Parse(row["ReservalDate"].ToString());
				}
				if(row["Content"]!=null)
				{
					model.Content=row["Content"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["ContactEmail"]!=null)
				{
					model.ContactEmail=row["ContactEmail"].ToString();
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["CreatedUserId"]!=null && row["CreatedUserId"].ToString()!="")
				{
					model.CreatedUserId=int.Parse(row["CreatedUserId"].ToString());
				}
				if(row["SupplierId"]!=null && row["SupplierId"].ToString()!="")
				{
					model.SupplierId=int.Parse(row["SupplierId"].ToString());
				}
				if(row["ServiceId"]!=null && row["ServiceId"].ToString()!="")
				{
					model.ServiceId=int.Parse(row["ServiceId"].ToString());
				}
				if(row["OrderCode"]!=null)
				{
					model.OrderCode=row["OrderCode"].ToString();
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
			strSql.Append("select ReservalId,Name,RegionId,ContactName,ContactPhone,ReservalDate,Content,Address,ContactEmail,Status,CreatedDate,CreatedUserId,SupplierId,ServiceId,OrderCode,Remark ");
			strSql.Append(" FROM Appt_Reservation ");
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
			strSql.Append(" ReservalId,Name,RegionId,ContactName,ContactPhone,ReservalDate,Content,Address,ContactEmail,Status,CreatedDate,CreatedUserId,SupplierId,ServiceId,OrderCode,Remark ");
			strSql.Append(" FROM Appt_Reservation ");
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
			strSql.Append("select count(1) FROM Appt_Reservation ");
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
				strSql.Append("order by T.ReservalId desc");
			}
			strSql.Append(")AS Row, T.*  from Appt_Reservation T ");
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
			parameters[0].Value = "Appt_Reservation";
			parameters[1].Value = "ReservalId";
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

