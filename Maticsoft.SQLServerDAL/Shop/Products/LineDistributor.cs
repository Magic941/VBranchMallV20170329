/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：LineDistributors.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:24
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.Products;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Shop.Products
{
	/// <summary>
	/// 数据访问类:LineDistributor
	/// </summary>
	public partial class LineDistributor:ILineDistributor
	{
		public LineDistributor()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LineId", "Shop_LineDistributors"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LineId,int DistributorId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM Shop_LineDistributors");
			strSql.Append(" WHERE LineId=@LineId and DistributorId=@DistributorId ");
			SqlParameter[] parameters = {
					new SqlParameter("@LineId", SqlDbType.Int,4),
					new SqlParameter("@DistributorId", SqlDbType.Int,4)			};
			parameters[0].Value = LineId;
			parameters[1].Value = DistributorId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Shop.Products.LineDistributor model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("INSERT INTO Shop_LineDistributors(");
			strSql.Append("LineId,DistributorId)");
			strSql.Append(" VALUES (");
			strSql.Append("@LineId,@DistributorId)");
			SqlParameter[] parameters = {
					new SqlParameter("@LineId", SqlDbType.Int,4),
					new SqlParameter("@DistributorId", SqlDbType.Int,4)};
			parameters[0].Value = model.LineId;
			parameters[1].Value = model.DistributorId;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Products.LineDistributor model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("UPDATE Shop_LineDistributors SET ");
			strSql.Append("LineId=@LineId,");
			strSql.Append("DistributorId=@DistributorId");
			strSql.Append(" WHERE LineId=@LineId and DistributorId=@DistributorId ");
			SqlParameter[] parameters = {
					new SqlParameter("@LineId", SqlDbType.Int,4),
					new SqlParameter("@DistributorId", SqlDbType.Int,4)};
			parameters[0].Value = model.LineId;
			parameters[1].Value = model.DistributorId;

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
		public bool Delete(int LineId,int DistributorId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM Shop_LineDistributors ");
			strSql.Append(" WHERE LineId=@LineId and DistributorId=@DistributorId ");
			SqlParameter[] parameters = {
					new SqlParameter("@LineId", SqlDbType.Int,4),
					new SqlParameter("@DistributorId", SqlDbType.Int,4)			};
			parameters[0].Value = LineId;
			parameters[1].Value = DistributorId;

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
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Products.LineDistributor GetModel(int LineId,int DistributorId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT  TOP 1 LineId,DistributorId FROM Shop_LineDistributors ");
			strSql.Append(" WHERE LineId=@LineId and DistributorId=@DistributorId ");
			SqlParameter[] parameters = {
					new SqlParameter("@LineId", SqlDbType.Int,4),
					new SqlParameter("@DistributorId", SqlDbType.Int,4)			};
			parameters[0].Value = LineId;
			parameters[1].Value = DistributorId;

			Maticsoft.Model.Shop.Products.LineDistributor model=new Maticsoft.Model.Shop.Products.LineDistributor();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["LineId"]!=null && ds.Tables[0].Rows[0]["LineId"].ToString()!="")
				{
					model.LineId=int.Parse(ds.Tables[0].Rows[0]["LineId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DistributorId"]!=null && ds.Tables[0].Rows[0]["DistributorId"].ToString()!="")
				{
					model.DistributorId=int.Parse(ds.Tables[0].Rows[0]["DistributorId"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT LineId,DistributorId ");
			strSql.Append(" FROM Shop_LineDistributors ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT ");
			if(Top>0)
			{
				strSql.Append(" TOP "+Top.ToString());
			}
			strSql.Append(" LineId,DistributorId ");
			strSql.Append(" FROM Shop_LineDistributors ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
			}
			strSql.Append(" ORDER BY " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM Shop_LineDistributors ");
			if(!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE "+strWhere);
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
			if (!string.IsNullOrWhiteSpace(orderby.Trim()))
			{
				strSql.Append("ORDER BY T." + orderby );
			}
			else
			{
				strSql.Append("ORDER BY T.DistributorId desc");
			}
			strSql.Append(")AS Row, T.*  FROM Shop_LineDistributors T ");
			if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
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
			parameters[0].Value = "Shop_LineDistributors";
			parameters[1].Value = "DistributorId";
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

