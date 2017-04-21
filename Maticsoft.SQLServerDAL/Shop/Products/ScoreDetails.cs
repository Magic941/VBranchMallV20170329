/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ScoreDetails.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/08/27 14:50:44
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

namespace Maticsoft.SQLServerDAL.Admin.Shop.Products
{
    /// <summary>/
    /// 数据访问类:ScoreDetails
    /// </summary>
    public partial class ScoreDetails : IScoreDetails
    {
        public ScoreDetails()
        { }

        #region Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ScoreId", "Shop_ScoreDetails");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ScoreId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_ScoreDetails");
            strSql.Append(" WHERE ScoreId=@ScoreId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ScoreId", SqlDbType.Int,4)			};
            parameters[0].Value = ScoreId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.ScoreDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_ScoreDetails(");
            strSql.Append("ScoreId,ReviewId,UserId,ProductId,Score,CreatedDate)");
            strSql.Append(" VALUES (");
            strSql.Append("@ScoreId,@ReviewId,@UserId,@ProductId,@Score,@CreatedDate)");
            SqlParameter[] parameters = {
					new SqlParameter("@ScoreId", SqlDbType.Int,4),
					new SqlParameter("@ReviewId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@Score", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ScoreId;
            parameters[1].Value = model.ReviewId;
            parameters[2].Value = model.UserId;
            parameters[3].Value = model.ProductId;
            parameters[4].Value = model.Score;
            parameters[5].Value = model.CreatedDate;

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
        public bool Update(Maticsoft.Model.Shop.Products.ScoreDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_ScoreDetails SET ");
            strSql.Append("ReviewId=@ReviewId,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("ProductId=@ProductId,");
            strSql.Append("Score=@Score,");
            strSql.Append("CreatedDate=@CreatedDate");
            strSql.Append(" WHERE ScoreId=@ScoreId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReviewId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@Score", SqlDbType.Int,4),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ScoreId", SqlDbType.Int,4)};
            parameters[0].Value = model.ReviewId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.ProductId;
            parameters[3].Value = model.Score;
            parameters[4].Value = model.CreatedDate;
            parameters[5].Value = model.ScoreId;

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
        public bool Delete(int ScoreId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_ScoreDetails ");
            strSql.Append(" WHERE ScoreId=@ScoreId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ScoreId", SqlDbType.Int,4)			};
            parameters[0].Value = ScoreId;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string ScoreIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_ScoreDetails ");
            strSql.Append(" WHERE ScoreId in (" + ScoreIdlist + ")  ");
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ScoreDetails GetModel(int ScoreId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 ScoreId,ReviewId,UserId,ProductId,Score,CreatedDate FROM Shop_ScoreDetails ");
            strSql.Append(" WHERE ScoreId=@ScoreId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ScoreId", SqlDbType.Int,4)			};
            parameters[0].Value = ScoreId;

            Maticsoft.Model.Shop.Products.ScoreDetails model = new Maticsoft.Model.Shop.Products.ScoreDetails();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ScoreId"] != null && ds.Tables[0].Rows[0]["ScoreId"].ToString() != "")
                {
                    model.ScoreId = int.Parse(ds.Tables[0].Rows[0]["ScoreId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReviewId"] != null && ds.Tables[0].Rows[0]["ReviewId"].ToString() != "")
                {
                    model.ReviewId = int.Parse(ds.Tables[0].Rows[0]["ReviewId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"] != null && ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductId"] != null && ds.Tables[0].Rows[0]["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Score"] != null && ds.Tables[0].Rows[0]["Score"].ToString() != "")
                {
                    model.Score = int.Parse(ds.Tables[0].Rows[0]["Score"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
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
            strSql.Append("SELECT ScoreId,ReviewId,UserId,ProductId,Score,CreatedDate ");
            strSql.Append(" FROM Shop_ScoreDetails ");
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
            strSql.Append(" ScoreId,ReviewId,UserId,ProductId,Score,CreatedDate ");
            strSql.Append(" FROM Shop_ScoreDetails ");
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
            strSql.Append("SELECT COUNT(1) FROM Shop_ScoreDetails ");
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
                strSql.Append("ORDER BY T.ScoreId desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_ScoreDetails T ");
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
            parameters[0].Value = "Shop_ScoreDetails";
            parameters[1].Value = "ScoreId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion Method

        public int GetScore(int? ReviewId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Score FROM Shop_ScoreDetails ");
            strSql.AppendFormat("WHERE ReviewId={0} ", ReviewId.Value);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取商品的评分等级和总评论数
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.ProductId,ScoreCount,Score,Score/SCoreCount AS AVE FROM( ");
            strSql.Append("SELECT COUNT(*)AS ScoreCount,ProductId ");
            strSql.Append("FROM Shop_ScoreDetails ");
            strSql.Append("GROUP BY ProductId)A  ");
            strSql.Append("LEFT JOIN (SELECT SUM(Score) AS Score,ProductId ");
            strSql.Append("FROM Shop_ScoreDetails ");
            strSql.Append("GROUP BY ProductId)B ON A.ProductId = B.ProductId ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取商品评分的详细信息 差评  中评 好评 各占比例
        /// </summary>
        public DataSet GetScoreDetailInfo(long productId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.ProductId,Poor,Rating,HeightScore FROM ( ");
            strSql.Append("SELECT (COUNT( Score)+0.0)/(SELECT COUNT(*) FROM  Shop_ScoreDetails ");
            strSql.Append("WHERE ProductId=@ProductId)*100 AS Poor,ProductId FROM Shop_ScoreDetails ");
            strSql.Append("WHERE Score<3  AND ProductId=@ProductId GROUP BY ProductId)A ");
            strSql.Append("LEFT JOIN ( ");
            strSql.Append("SELECT (COUNT( Score)+0.0)/(SELECT COUNT(*) FROM  Shop_ScoreDetails ");
            strSql.Append("WHERE ProductId=@ProductId)*100 AS Rating,ProductId FROM Shop_ScoreDetails ");
            strSql.Append("WHERE Score=3  AND ProductId=@ProductId  GROUP BY ProductId)B ON A.ProductId = B.ProductId ");
            strSql.Append("LEFT JOIN ( ");
            strSql.Append("SELECT (COUNT( Score)+0.0)/(SELECT COUNT(*) FROM  Shop_ScoreDetails ");
            strSql.Append("WHERE ProductId=@ProductId)*100 AS HeightScore,ProductId FROM Shop_ScoreDetails ");
            strSql.Append("WHERE Score>3  AND ProductId=@ProductId GROUP BY ProductId)C  ON A.ProductId = C.ProductId ");
            SqlParameter[] parameter = {
                                       new SqlParameter("@ProductId",SqlDbType.BigInt)
                                       };
            parameter[0].Value = productId;
            return DbHelperSQL.Query(strSql.ToString(), parameter);
        }
    }
}