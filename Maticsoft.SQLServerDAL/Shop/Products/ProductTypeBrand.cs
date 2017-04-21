/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductTypeBrands.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:30
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
	/// 数据访问类:ProductTypeBrand
	/// </summary>
	public partial class ProductTypeBrand:IProductTypeBrand
	{
		public ProductTypeBrand()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ProductTypeId", "Shop_ProductTypeBrands"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ProductTypeId,int BrandId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM Shop_ProductTypeBrands");
			strSql.Append(" WHERE ProductTypeId=@ProductTypeId and BrandId=@BrandId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductTypeId", SqlDbType.Int,4),
					new SqlParameter("@BrandId", SqlDbType.Int,4)			};
			parameters[0].Value = ProductTypeId;
			parameters[1].Value = BrandId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Shop.Products.ProductTypeBrand model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("INSERT INTO Shop_ProductTypeBrands(");
			strSql.Append("ProductTypeId,BrandId)");
			strSql.Append(" VALUES (");
			strSql.Append("@ProductTypeId,@BrandId)");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductTypeId", SqlDbType.Int,4),
					new SqlParameter("@BrandId", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductTypeId;
			parameters[1].Value = model.BrandId;

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
		public bool Update(Maticsoft.Model.Shop.Products.ProductTypeBrand model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("UPDATE Shop_ProductTypeBrands SET ");
			strSql.Append("ProductTypeId=@ProductTypeId");
			strSql.Append(" WHERE BrandId=@BrandId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductTypeId", SqlDbType.Int,4),
					new SqlParameter("@BrandId", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductTypeId;
			parameters[1].Value = model.BrandId;

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
        //public bool Delete(int ProductTypeId,int BrandId)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("DELETE FROM Shop_ProductTypeBrands ");
        //    strSql.Append(" WHERE ProductTypeId=@ProductTypeId and BrandId=@BrandId ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ProductTypeId", SqlDbType.Int,4),
        //            new SqlParameter("@BrandId", SqlDbType.Int,4)			};
        //    parameters[0].Value = ProductTypeId;
        //    parameters[1].Value = BrandId;

        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Products.ProductTypeBrand GetModel(int ProductTypeId,int BrandId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT  TOP 1 ProductTypeId,BrandId FROM Shop_ProductTypeBrands ");
			strSql.Append(" WHERE ProductTypeId=@ProductTypeId and BrandId=@BrandId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductTypeId", SqlDbType.Int,4),
					new SqlParameter("@BrandId", SqlDbType.Int,4)			};
			parameters[0].Value = ProductTypeId;
			parameters[1].Value = BrandId;

			Maticsoft.Model.Shop.Products.ProductTypeBrand model=new Maticsoft.Model.Shop.Products.ProductTypeBrand();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ProductTypeId"]!=null && ds.Tables[0].Rows[0]["ProductTypeId"].ToString()!="")
				{
					model.ProductTypeId=int.Parse(ds.Tables[0].Rows[0]["ProductTypeId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BrandId"]!=null && ds.Tables[0].Rows[0]["BrandId"].ToString()!="")
				{
					model.BrandId=int.Parse(ds.Tables[0].Rows[0]["BrandId"].ToString());
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
			strSql.Append("SELECT ProductTypeId,BrandId ");
			strSql.Append(" FROM Shop_ProductTypeBrands ");
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
			strSql.Append(" ProductTypeId,BrandId ");
			strSql.Append(" FROM Shop_ProductTypeBrands ");
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
			strSql.Append("SELECT COUNT(1) FROM Shop_ProductTypeBrands ");
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
				strSql.Append("ORDER BY T.BrandId desc");
			}
			strSql.Append(")AS Row, T.*  FROM Shop_ProductTypeBrands T ");
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
			parameters[0].Value = "Shop_ProductTypeBrands";
			parameters[1].Value = "BrandId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
        #region NewMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(int ProductTypeId, int BrandsId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_ProductTypeBrands(");
            strSql.Append("ProductTypeId,BrandId)");
            strSql.Append(" VALUES (");
            strSql.Append("@ProductTypeId,@BrandId)");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductTypeId", SqlDbType.Int,4),
					new SqlParameter("@BrandId", SqlDbType.Int,4)};
            parameters[0].Value = ProductTypeId;
            parameters[1].Value = BrandsId;

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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int? ProductTypeId, int? BrandId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_ProductTypeBrands ");
            if (ProductTypeId.HasValue)
            {
                strSql.AppendFormat(" WHERE ProductTypeId={0}", ProductTypeId.Value);
            }
            else if (BrandId.HasValue)
            {
                strSql.AppendFormat(" WHERE BrandId={0} ", BrandId.Value);
            }
            else
            {
                strSql.AppendFormat(" WHERE ProductTypeId={0} AND BrandId={1} ", ProductTypeId.Value, BrandId.Value);
            }

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        /// 是否存在该记录
        /// </summary>
        public bool ExistsBrands( int BrandId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_ProductTypeBrands");
            strSql.Append(" WHERE  BrandId=@BrandId ");
            SqlParameter[] parameters = {
					new SqlParameter("@BrandId", SqlDbType.Int,4)			};
            parameters[0].Value = BrandId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        #endregion
	}
}

