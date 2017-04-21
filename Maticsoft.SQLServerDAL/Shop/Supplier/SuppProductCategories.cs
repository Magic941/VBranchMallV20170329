/**
* SuppProductCategories.cs
*
* 功 能： N/A
* 类 名： SuppProductCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:52   Ben    初版
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
	/// 数据访问类:SuppProductCategories
	/// </summary>
	public partial class SuppProductCategories:ISuppProductCategories
	{
		public SuppProductCategories()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CategoryId", "Shop_SuppProductCategories"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CategoryId,long ProductId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Shop_SuppProductCategories");
			strSql.Append(" where CategoryId=@CategoryId and ProductId=@ProductId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
			parameters[0].Value = CategoryId;
			parameters[1].Value = ProductId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Shop.Supplier.SuppProductCategories model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Shop_SuppProductCategories(");
			strSql.Append("CategoryId,ProductId,CategoryPath)");
			strSql.Append(" values (");
			strSql.Append("@CategoryId,@ProductId,@CategoryPath)");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@CategoryPath", SqlDbType.NVarChar,4000)};
			parameters[0].Value = model.CategoryId;
			parameters[1].Value = model.ProductId;
			parameters[2].Value = model.CategoryPath;

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
		public bool Update(Maticsoft.Model.Shop.Supplier.SuppProductCategories model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Shop_SuppProductCategories set ");
			strSql.Append("CategoryPath=@CategoryPath");
			strSql.Append(" where CategoryId=@CategoryId and ProductId=@ProductId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryPath", SqlDbType.NVarChar,4000),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)};
			parameters[0].Value = model.CategoryPath;
			parameters[1].Value = model.CategoryId;
			parameters[2].Value = model.ProductId;

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
		public bool Delete(int CategoryId,long ProductId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Shop_SuppProductCategories ");
			strSql.Append(" where CategoryId=@CategoryId and ProductId=@ProductId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
			parameters[0].Value = CategoryId;
			parameters[1].Value = ProductId;

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
		public Maticsoft.Model.Shop.Supplier.SuppProductCategories GetModel(int CategoryId,long ProductId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CategoryId,ProductId,CategoryPath from Shop_SuppProductCategories ");
			strSql.Append(" where CategoryId=@CategoryId and ProductId=@ProductId ");
			SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
			parameters[0].Value = CategoryId;
			parameters[1].Value = ProductId;

			Maticsoft.Model.Shop.Supplier.SuppProductCategories model=new Maticsoft.Model.Shop.Supplier.SuppProductCategories();
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
		public Maticsoft.Model.Shop.Supplier.SuppProductCategories DataRowToModel(DataRow row)
		{
			Maticsoft.Model.Shop.Supplier.SuppProductCategories model=new Maticsoft.Model.Shop.Supplier.SuppProductCategories();
			if (row != null)
			{
				if(row["CategoryId"]!=null && row["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(row["CategoryId"].ToString());
				}
				if(row["ProductId"]!=null && row["ProductId"].ToString()!="")
				{
					model.ProductId=long.Parse(row["ProductId"].ToString());
				}
				if(row["CategoryPath"]!=null)
				{
					model.CategoryPath=row["CategoryPath"].ToString();
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
			strSql.Append("select CategoryId,ProductId,CategoryPath ");
			strSql.Append(" FROM Shop_SuppProductCategories ");
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
			strSql.Append(" CategoryId,ProductId,CategoryPath ");
			strSql.Append(" FROM Shop_SuppProductCategories ");
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
			strSql.Append("select count(1) FROM Shop_SuppProductCategories ");
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
				strSql.Append("order by T.ProductId desc");
			}
			strSql.Append(")AS Row, T.*  from Shop_SuppProductCategories T ");
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
			parameters[0].Value = "Shop_SuppProductCategories";
			parameters[1].Value = "ProductId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Supplier.SuppProductCategories GetModel(long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CategoryId,ProductId,CategoryPath from Shop_SuppProductCategories ");
            strSql.Append(" where  ProductId=@ProductId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
            parameters[0].Value = ProductId;

            Maticsoft.Model.Shop.Supplier.SuppProductCategories model = new Maticsoft.Model.Shop.Supplier.SuppProductCategories();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_SuppProductCategories ");
            strSql.Append(" where ProductId=@ProductId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
            parameters[0].Value = ProductId;

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

