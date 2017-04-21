/**
* CouponClass.cs
*
* 功 能： N/A
* 类 名： CouponClass
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:20:56   N/A    初版
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
using Maticsoft.IDAL.Shop.Coupon;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Coupon
{
	/// <summary>
	/// 数据访问类:CouponClass
	/// </summary>
	public partial class CouponClass:ICouponClass
	{
		public CouponClass()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ClassId", "Shop_CouponClass"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ClassId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_CouponClass");
			strSql.Append(" where ClassId=@ClassId");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.Int,4)
			};
			parameters[0].Value = ClassId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Shop.Coupon.CouponClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_CouponClass(");
			strSql.Append("Name,Sequence,Status)");
			strSql.Append(" values (");
			strSql.Append("@Name,@Sequence,@Status)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Sequence;
			parameters[2].Value = model.Status;

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
		public bool Update(Maticsoft.Model.Shop.Coupon.CouponClass model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_CouponClass set ");
			strSql.Append("Name=@Name,");
			strSql.Append("Sequence=@Sequence,");
			strSql.Append("Status=@Status");
			strSql.Append(" where ClassId=@ClassId");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ClassId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Sequence;
			parameters[2].Value = model.Status;
			parameters[3].Value = model.ClassId;

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
		public bool Delete(int ClassId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_CouponClass ");
			strSql.Append(" where ClassId=@ClassId");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.Int,4)
			};
			parameters[0].Value = ClassId;

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
		public bool DeleteList(string ClassIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_CouponClass ");
			strSql.Append(" where ClassId in ("+ClassIdlist + ")  ");
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
		public Maticsoft.Model.Shop.Coupon.CouponClass GetModel(int ClassId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ClassId,Name,Sequence,Status from Shop_CouponClass ");
			strSql.Append(" where ClassId=@ClassId");
			SqlParameter[] parameters = {
					new SqlParameter("@ClassId", SqlDbType.Int,4)
			};
			parameters[0].Value = ClassId;

			Maticsoft.Model.Shop.Coupon.CouponClass model=new Maticsoft.Model.Shop.Coupon.CouponClass();
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
		public Maticsoft.Model.Shop.Coupon.CouponClass DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Coupon.CouponClass model=new Maticsoft.Model.Shop.Coupon.CouponClass();
			if (row != null)
			{
				if(row["ClassId"]!=null && row["ClassId"].ToString()!="")
				{
					model.ClassId=int.Parse(row["ClassId"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["Sequence"]!=null && row["Sequence"].ToString()!="")
				{
					model.Sequence=int.Parse(row["Sequence"].ToString());
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
			strSql.Append("select ClassId,Name,Sequence,Status ");
			strSql.Append(" FROM Shop_CouponClass ");
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
			strSql.Append(" ClassId,Name,Sequence,Status ");
			strSql.Append(" FROM Shop_CouponClass ");
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
			strSql.Append("select count(1) FROM Shop_CouponClass ");
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
				strSql.Append("order by T.ClassId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_CouponClass T ");
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
			parameters[0].Value = "Shop_CouponClass";
			parameters[1].Value = "ClassId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

	    public int GetSequence()
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT MAX(Sequence) FROM Shop_CouponClass ");
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

        public bool UpdateSeqByCid(int Cid, int seq)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponClass set ");
            strSql.Append("Sequence=@Sequence");
            strSql.Append(" where ClassId=@ClassId");
            SqlParameter[] parameters = {
					new SqlParameter("@Sequence", SqlDbType.Int,4),
					new SqlParameter("@ClassId", SqlDbType.Int,4)};
            parameters[0].Value = seq;
            parameters[1].Value = Cid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
	    }

        public bool UpdateStatus(int Cid, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_CouponClass set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where ClassId=@ClassId");
            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ClassId", SqlDbType.Int,4)};
            parameters[0].Value = status;
            parameters[1].Value = Cid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
     
	    #endregion  ExtensionMethod
	}
}

