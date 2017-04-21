/**
* SuppProductStatModes.cs
*
* 功 能： N/A
* 类 名： SuppProductStatModes
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/27 18:11:59   Ben    初版
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
    /// 数据访问类:SuppProductStatModes
    /// </summary>
    public partial class SuppProductStatModes : ISuppProductStatModes
    {
        public SuppProductStatModes()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("StationId", "Shop_SuppProductStatModes");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int StationId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_SuppProductStatModes");
            strSql.Append(" where StationId=@StationId");
            SqlParameter[] parameters = {
                    new SqlParameter("@StationId", SqlDbType.Int,4)
            };
            parameters[0].Value = StationId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Supplier.SuppProductStatModes model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_SuppProductStatModes(");
            strSql.Append("ProductId,DisplaySequence,Type,SupplierId)");
            strSql.Append(" values (");
            strSql.Append("@ProductId,@DisplaySequence,@Type,@SupplierId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.SmallInt,2),
                    new SqlParameter("@SupplierId", SqlDbType.Int,4)};
            parameters[0].Value = model.ProductId;
            parameters[1].Value = model.DisplaySequence;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.SupplierId;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Supplier.SuppProductStatModes model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_SuppProductStatModes set ");
            strSql.Append("ProductId=@ProductId,");
            strSql.Append("DisplaySequence=@DisplaySequence,");
            strSql.Append("Type=@Type,");
            strSql.Append("SupplierId=@SupplierId");
            strSql.Append(" where StationId=@StationId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.SmallInt,2),
                    new SqlParameter("@SupplierId", SqlDbType.Int,4),
                    new SqlParameter("@StationId", SqlDbType.Int,4)};
            parameters[0].Value = model.ProductId;
            parameters[1].Value = model.DisplaySequence;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.SupplierId;
            parameters[4].Value = model.StationId;

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
        public bool Delete(int StationId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_SuppProductStatModes ");
            strSql.Append(" where StationId=@StationId");
            SqlParameter[] parameters = {
                    new SqlParameter("@StationId", SqlDbType.Int,4)
            };
            parameters[0].Value = StationId;

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
        public bool DeleteList(string StationIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_SuppProductStatModes ");
            strSql.Append(" where StationId in (" + StationIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Supplier.SuppProductStatModes GetModel(int StationId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 StationId,ProductId,DisplaySequence,Type,SupplierId from Shop_SuppProductStatModes ");
            strSql.Append(" where StationId=@StationId");
            SqlParameter[] parameters = {
                    new SqlParameter("@StationId", SqlDbType.Int,4)
            };
            parameters[0].Value = StationId;

            Maticsoft.Model.Shop.Supplier.SuppProductStatModes model = new Maticsoft.Model.Shop.Supplier.SuppProductStatModes();
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Supplier.SuppProductStatModes DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SuppProductStatModes model = new Maticsoft.Model.Shop.Supplier.SuppProductStatModes();
            if (row != null)
            {
                if (row["StationId"] != null && row["StationId"].ToString() != "")
                {
                    model.StationId = int.Parse(row["StationId"].ToString());
                }
                if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["DisplaySequence"] != null && row["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["SupplierId"] != null && row["SupplierId"].ToString() != "")
                {
                    model.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select StationId,ProductId,DisplaySequence,Type,SupplierId ");
            strSql.Append(" FROM Shop_SuppProductStatModes ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" StationId,ProductId,DisplaySequence,Type,SupplierId ");
            strSql.Append(" FROM Shop_SuppProductStatModes ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Shop_SuppProductStatModes ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.StationId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_SuppProductStatModes T ");
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
            parameters[0].Value = "Shop_SuppProductStatModes";
            parameters[1].Value = "StationId";
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
        /// 根据type获得数据列表
        /// </summary>
        public DataSet GetListByType(int supplierId, string strType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select P.* From  Shop_SuppProductStatModes S, Shop_Products P ");
            strSql.AppendFormat(" WHERE S.SupplierId={0} AND S.ProductId = P.ProductId ", supplierId);
            if (!string.IsNullOrWhiteSpace(strType))
            {
                strSql.Append(" and S.Type =" + strType);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int supplierId, long productId, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM  Shop_SuppProductStatModes");
            strSql.Append(" WHERE SupplierId=@SupplierId AND ProductId=@ProductId and Type=@Type ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters[0].Value = productId;
            parameters[1].Value = type;
            parameters[2].Value = supplierId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int supplierId, long productId, int type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM  Shop_SuppProductStatModes ");
            strSql.Append(" WHERE SupplierId=@SupplierId AND ProductId=@ProductId and Type=@Type ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters[0].Value = productId;
            parameters[1].Value = type;
            parameters[2].Value = supplierId;

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
        /// 清空type下所有商品
        /// </summary>
        public bool DeleteByType(int supplierId, int type, int categoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM  Shop_SuppProductStatModes ");
            strSql.Append(" WHERE SupplierId=@SupplierId AND Type=@Type ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS ( SELECT DISTINCT *   FROM   Shop_SuppProductCategories ");
                strSql.AppendFormat(
                    " WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0} ) AND ProductId =  Shop_SuppProductStatModes.ProductId AND SupplierId = @SupplierId) ",
                    categoryId);
            }
            SqlParameter[] parameters = {
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@SupplierId", SqlDbType.Int,4)
            };
            parameters[0].Value = type;
            parameters[1].Value = supplierId;

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
        /// 推荐商品中已经添加到热卖、最新、特价中去的商品信息
        /// </summary>
        public DataSet GetStationMode(int supplierId, int modeType, int categoryId, string pName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT PSM.ProductId ");
            strSql.Append("FROM  Shop_SuppProductStatModes PSM ");
            strSql.Append(" WHERE PSM.SupplierId = @SupplierId AND Type=@Type ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS ( SELECT DISTINCT *   FROM   Shop_SuppProductCategories ");
                strSql.AppendFormat(
                    " WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0} ) AND ProductId = PSM.ProductId AND SupplierId = @SupplierId) ",
                    categoryId);
            }
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.Append(" AND EXISTS ( SELECT *  FROM      Shop_Products WHERE  SaleStatus = 1   ");
                strSql.AppendFormat(
                    "  AND ProductName LIKE '%{0}%'  AND ProductId = PSM.ProductId AND SupplierId = @SupplierId) ", pName);
            }
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Type", SqlDbType.Int, 4),
                    new SqlParameter("@SupplierId", SqlDbType.Int,4)
                };
            parameters[0].Value = modeType;
            parameters[1].Value = supplierId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        public int GetProductNoRecCount(int supplierId, int categoryId, string pName, int modeType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Shop_Products ");

            strSql.Append(" WHERE  SaleStatus = 1 AND SupplierId = @SupplierId");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_SuppProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = Shop_Products.ProductId AND SupplierId = @SupplierId) ",
                    categoryId);
            }
            strSql.Append(" AND NOT EXISTS ( SELECT *  FROM   Shop_SuppProductStatModes ");
            strSql.AppendFormat("   WHERE  Type = {0} AND ProductId = Shop_Products.ProductId AND SupplierId = @SupplierId) ", modeType);
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            SqlParameter[] parameters =
                {
                    new SqlParameter("@SupplierId", SqlDbType.Int,4)
                };
            parameters[0].Value = supplierId;
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
        public DataSet GetProductNoRecList(int supplierId, int categoryId, string pName, int modeType, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            strSql.Append("order by T.ProductId desc");
            strSql.Append(")AS Row, T.*  from Shop_Products T ");

            strSql.Append(" WHERE  SaleStatus = 1  AND SupplierId = @SupplierId");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS (  SELECT DISTINCT  *  FROM   Shop_SuppProductCategories  ");
                strSql.AppendFormat(
                    "  WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0}  )  AND ProductId = T.ProductId AND SupplierId = @SupplierId) ",
                    categoryId);
            }
            strSql.Append(" AND NOT EXISTS ( SELECT *  FROM   Shop_SuppProductStatModes ");
            strSql.AppendFormat("   WHERE  Type = {0} AND ProductId = T.ProductId AND SupplierId = @SupplierId) ", modeType);
            if (!String.IsNullOrWhiteSpace(pName))
            {
                strSql.AppendFormat(" AND ProductName LIKE '%{0}%' ", pName);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            SqlParameter[] parameters =
                {
                    new SqlParameter("@SupplierId", SqlDbType.Int,4)
                };
            parameters[0].Value = supplierId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

     
        #endregion  ExtensionMethod
    }
}

