/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：RelatedProducts.cs
// 文件功能描述：
//
// 创建标识： [Ben]  2012/06/11 20:36:31
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop.Products;

namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// 数据访问类:RelatedProduct
    /// </summary>
    public partial class RelatedProduct : IRelatedProduct
    {
        public RelatedProduct()
        { }

        #region Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("RelatedId", "Shop_RelatedProducts");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RelatedId, long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_RelatedProducts");
            strSql.Append(" WHERE RelatedId=@RelatedId and ProductId=@ProductId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RelatedId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
            parameters[0].Value = RelatedId;
            parameters[1].Value = ProductId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.RelatedProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_RelatedProducts(");
            strSql.Append("RelatedId,ProductId)");
            strSql.Append(" VALUES (");
            strSql.Append("@RelatedId,@ProductId)");
            SqlParameter[] parameters = {
					new SqlParameter("@RelatedId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.RelatedId;
            parameters[1].Value = model.ProductId;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.RelatedProduct model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_RelatedProducts SET ");
            strSql.Append("RelatedId=@RelatedId,");
            strSql.Append("ProductId=@ProductId");
            strSql.Append(" WHERE RelatedId=@RelatedId and ProductId=@ProductId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RelatedId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)};
            parameters[0].Value = model.RelatedId;
            parameters[1].Value = model.ProductId;

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
        public bool Delete(int RelatedId, long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_RelatedProducts ");
            strSql.Append(" WHERE RelatedId=@RelatedId and ProductId=@ProductId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RelatedId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
            parameters[0].Value = RelatedId;
            parameters[1].Value = ProductId;

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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.RelatedProduct GetModel(int RelatedId, long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 RelatedId,ProductId FROM Shop_RelatedProducts ");
            strSql.Append(" WHERE RelatedId=@RelatedId and ProductId=@ProductId ");
            SqlParameter[] parameters = {
					new SqlParameter("@RelatedId", SqlDbType.BigInt,8),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8)			};
            parameters[0].Value = RelatedId;
            parameters[1].Value = ProductId;

            Maticsoft.Model.Shop.Products.RelatedProduct model = new Maticsoft.Model.Shop.Products.RelatedProduct();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RelatedId"] != null && ds.Tables[0].Rows[0]["RelatedId"].ToString() != "")
                {
                    model.RelatedId = int.Parse(ds.Tables[0].Rows[0]["RelatedId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductId"] != null && ds.Tables[0].Rows[0]["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT RelatedId,ProductId ");
            strSql.Append(" FROM Shop_RelatedProducts ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            if (Top > 0)
            {
                strSql.Append(" TOP " + Top.ToString());
            }
            strSql.Append(" RelatedId,ProductId ");
            strSql.Append(" FROM Shop_RelatedProducts ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_RelatedProducts ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                strSql.Append("ORDER BY T." + orderby);
            }
            else
            {
                strSql.Append("ORDER BY T.ProductId desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_RelatedProducts T ");
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
            parameters[0].Value = "Shop_RelatedProducts";
            parameters[1].Value = "ProductId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion Method

        public DataSet IsDoubleRelated(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.*,CASE  WHEN B.RelatedId IS NULL THEN 0 ELSE 1 END AS IsRelated   FROM( ");
            strSql.Append("SELECT * FROM Shop_RelatedProducts P ");
            strSql.AppendFormat("WHERE P.ProductId={0})A ", productId);
            strSql.Append("LEFT JOIN (SELECT * FROM Shop_RelatedProducts RP ");
            strSql.AppendFormat("WHERE RP.RelatedId={0})B ON  A.RelatedId = B.ProductId ", productId);

            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}