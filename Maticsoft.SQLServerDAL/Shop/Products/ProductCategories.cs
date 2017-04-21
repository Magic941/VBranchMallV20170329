/**
* ProductCategories.cs
*
* 功 能： N/A
* 类 名： ProductCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年12月14日 11:37:55  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;//Please add references
using Maticsoft.IDAL.Shop.Products;

namespace Maticsoft.SQLServerDAL.Shop.Products
{
    /// <summary>
    /// 数据访问类:ProductCategories
    /// </summary>
    public partial class ProductCategories : IProductCategories
    {
        public ProductCategories()
        { }

        #region Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.ProductCategories model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_ProductCategories(");
            strSql.Append("CategoryId,ProductId,CategoryPath)");
            strSql.Append(" VALUES (");
            strSql.Append("@CategoryId,@ProductId,@CategoryPath)");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@CategoryPath", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.ProductId;
            parameters[2].Value = model.CategoryPath;

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
        public bool Delete(long produtId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_ProductCategories ");
            strSql.Append(" WHERE ProductId=@ProductId");
            SqlParameter[] param ={
                            new SqlParameter("@ProductId",SqlDbType.BigInt)
                            };
            param[0].Value = produtId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), param);
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
        public Maticsoft.Model.Shop.Products.ProductCategories GetModel(long produtId)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 CategoryId,ProductId,CategoryPath FROM Shop_ProductCategories ");
            strSql.Append(" WHERE ProductId=@ProductId");
            SqlParameter[] param ={
                            new SqlParameter("@ProductId",SqlDbType.BigInt)
                            };
            param[0].Value = produtId;

            Model.Shop.Products.ProductCategories model = new Model.Shop.Products.ProductCategories();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), param);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CategoryId"] != null && ds.Tables[0].Rows[0]["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(ds.Tables[0].Rows[0]["CategoryId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductId"] != null && ds.Tables[0].Rows[0]["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CategoryPath"] != null && ds.Tables[0].Rows[0]["CategoryPath"].ToString() != "")
                {
                    model.CategoryPath =ds.Tables[0].Rows[0]["CategoryPath"].ToString();
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
            strSql.Append("SELECT CategoryId,ProductId,CategoryPath ");
            strSql.Append(" FROM Shop_ProductCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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
            strSql.Append(" CategoryId,ProductId,CategoryPath ");
            strSql.Append(" FROM Shop_ProductCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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
            strSql.Append("SELECT COUNT(1) FROM Shop_ProductCategories ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
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
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("ORDER BY T." + orderby);
            }
            else
            {
                strSql.Append("ORDER BY T.TagID desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_ProductCategories T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion Method

         
    }
}