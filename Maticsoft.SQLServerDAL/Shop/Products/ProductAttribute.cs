/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductAttributes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:25
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
	/// 数据访问类:ProductAttribute
	/// </summary>
	public partial class ProductAttribute:IProductAttribute
	{
		public ProductAttribute()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ValueId", "Shop_ProductAttributes"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ProductId,long AttributeId,int ValueId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM Shop_ProductAttributes");
			strSql.Append(" WHERE ProductId=@ProductId and AttributeId=@AttributeId and ValueId=@ValueId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
					new SqlParameter("@ValueId", SqlDbType.Int,4)			};
			parameters[0].Value = ProductId;
			parameters[1].Value = AttributeId;
			parameters[2].Value = ValueId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.Shop.Products.ProductAttribute model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("INSERT INTO Shop_ProductAttributes(");
			strSql.Append("ProductId,AttributeId,ValueId)");
			strSql.Append(" VALUES (");
			strSql.Append("@ProductId,@AttributeId,@ValueId)");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
					new SqlParameter("@ValueId", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductId;
			parameters[1].Value = model.AttributeId;
			parameters[2].Value = model.ValueId;

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
		public bool Update(Maticsoft.Model.Shop.Products.ProductAttribute model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("UPDATE Shop_ProductAttributes SET ");
			strSql.Append("ProductId=@ProductId,");
			strSql.Append("AttributeId=@AttributeId,");
			strSql.Append("ValueId=@ValueId");
			strSql.Append(" WHERE ProductId=@ProductId and AttributeId=@AttributeId and ValueId=@ValueId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
					new SqlParameter("@ValueId", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductId;
			parameters[1].Value = model.AttributeId;
			parameters[2].Value = model.ValueId;

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
		public bool Delete(long ProductId,long AttributeId,int ValueId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM Shop_ProductAttributes ");
			strSql.Append(" WHERE ProductId=@ProductId and AttributeId=@AttributeId and ValueId=@ValueId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
					new SqlParameter("@ValueId", SqlDbType.Int,4)			};
			parameters[0].Value = ProductId;
			parameters[1].Value = AttributeId;
			parameters[2].Value = ValueId;

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
		public Maticsoft.Model.Shop.Products.ProductAttribute GetModel(long ProductId,long AttributeId,int ValueId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT  TOP 1 ProductId,AttributeId,ValueId FROM Shop_ProductAttributes ");
			strSql.Append(" WHERE ProductId=@ProductId and AttributeId=@AttributeId and ValueId=@ValueId ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@AttributeId", SqlDbType.BigInt,8),
					new SqlParameter("@ValueId", SqlDbType.Int,4)			};
			parameters[0].Value = ProductId;
			parameters[1].Value = AttributeId;
			parameters[2].Value = ValueId;

			Maticsoft.Model.Shop.Products.ProductAttribute model=new Maticsoft.Model.Shop.Products.ProductAttribute();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ProductId"]!=null && ds.Tables[0].Rows[0]["ProductId"].ToString()!="")
				{
					model.ProductId=long.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AttributeId"]!=null && ds.Tables[0].Rows[0]["AttributeId"].ToString()!="")
				{
					model.AttributeId=long.Parse(ds.Tables[0].Rows[0]["AttributeId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ValueId"]!=null && ds.Tables[0].Rows[0]["ValueId"].ToString()!="")
				{
					model.ValueId=int.Parse(ds.Tables[0].Rows[0]["ValueId"].ToString());
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
			strSql.Append("SELECT ProductId,AttributeId,ValueId ");
			strSql.Append(" FROM Shop_ProductAttributes ");
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
			strSql.Append(" ProductId,AttributeId,ValueId ");
			strSql.Append(" FROM Shop_ProductAttributes ");
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
			strSql.Append("SELECT COUNT(1) FROM Shop_ProductAttributes ");
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
				strSql.Append("ORDER BY T.ValueId desc");
			}
			strSql.Append(")AS Row, T.*  FROM Shop_ProductAttributes T ");
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
			parameters[0].Value = "Shop_ProductAttributes";
			parameters[1].Value = "ValueId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method


        public bool Exists(long? ProductId, long? AttributeId, long? ValueId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_ProductAttributes");
            strSql.Append(" WHERE 1=1 ");
            if (ProductId.HasValue)
            {
                strSql.Append(" AND ProductId=" + ProductId.Value);
            }
            if (AttributeId.HasValue)
            {
                strSql.Append(" AND AttributeId=" + AttributeId.Value);
            }
            if (ValueId.HasValue)
            {
                strSql.Append(" AND ValueId=" + ValueId.Value);
            }
            return DbHelperSQL.Exists(strSql.ToString());
        }
	}
}

