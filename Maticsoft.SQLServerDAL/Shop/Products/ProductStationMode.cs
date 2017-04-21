/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductStationModes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:28
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
    /// 数据访问类:ProductStationMode
    /// </summary>
    public partial class ProductStationMode : IProductStationMode
    {
        public ProductStationMode()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("StationId", "Shop_ProductStationModes");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int StationId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_ProductStationModes");
            strSql.Append(" WHERE StationId=@StationId");
            SqlParameter[] parameters = {
					new SqlParameter("@StationId", SqlDbType.Int,4)
			};
            parameters[0].Value = StationId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_ProductStationModes(");
            strSql.Append("ProductId,DisplaySequence,Type,Floor,Sort,GoodTypeID)");
            strSql.Append(" VALUES (");
            strSql.Append("@ProductId,@DisplaySequence,@Type,@Floor,@Sort,@GoodTypeID)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.SmallInt,2),
                    new SqlParameter("@Floor", SqlDbType.SmallInt,2),
                    new SqlParameter("@Sort", SqlDbType.SmallInt,2),
                    new SqlParameter("@GoodTypeID",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ProductId;
            parameters[1].Value = model.DisplaySequence;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.Floor;
            parameters[4].Value = model.Sort;
            parameters[5].Value = model.GoodTypeID;

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
        public bool Update(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_ProductStationModes SET ");
            strSql.Append("ProductId=@ProductId,");
            strSql.Append("DisplaySequence=@DisplaySequence,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("Type=@Type");
            strSql.Append(" WHERE StationId=@StationId");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.BigInt,8),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.SmallInt,2),
					new SqlParameter("@StationId", SqlDbType.Int,4),
                    new SqlParameter("@Sort",SqlDbType.Int,4),
                    new SqlParameter("@GoodTypeID",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ProductId;
            parameters[1].Value = model.DisplaySequence;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.Sort;
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
        /// 更新一条数据
        /// </summary>
        public bool Update2(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_ProductStationModes SET ");
            strSql.Append("Sort=@Sort");
            strSql.Append(" WHERE StationId=@StationId and ProductId=@ProductId");
            SqlParameter[] parameters = {
                    new SqlParameter("@Sort",SqlDbType.Int,4),
                    new SqlParameter("@StationId",SqlDbType.Int,4),
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = model.Sort;
            parameters[1].Value = model.StationId;
            parameters[2].Value = model.ProductId;
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
            strSql.Append("DELETE FROM Shop_ProductStationModes ");
            strSql.Append(" WHERE StationId=@StationId");
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
            strSql.Append("DELETE FROM Shop_ProductStationModes ");
            strSql.Append(" WHERE StationId in (" + StationIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Products.ProductStationMode GetModel(int StationId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 StationId,ProductId,DisplaySequence,Type,GoodTypeID FROM Shop_ProductStationModes ");
            strSql.Append(" WHERE StationId=@StationId");
            SqlParameter[] parameters = {
					new SqlParameter("@StationId", SqlDbType.Int,4)
			};
            parameters[0].Value = StationId;

            Maticsoft.Model.Shop.Products.ProductStationMode model = new Maticsoft.Model.Shop.Products.ProductStationMode();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["StationId"] != null && ds.Tables[0].Rows[0]["StationId"].ToString() != "")
                {
                    model.StationId = int.Parse(ds.Tables[0].Rows[0]["StationId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductId"] != null && ds.Tables[0].Rows[0]["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DisplaySequence"] != null && ds.Tables[0].Rows[0]["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(ds.Tables[0].Rows[0]["DisplaySequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GoodTypeID"] != null && ds.Tables[0].Rows[0]["GoodTypeID"].ToString() != "")
                {
                    model.GoodTypeID = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据商品点好查询表内是否存在该条数据(李永琴)
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.ProductStationMode GetProductStationModel(int ProductId, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 StationId,ProductId,DisplaySequence,Type,GoodTypeID FROM Shop_ProductStationModes ");
            strSql.Append(" WHERE ProductId=@ProductId and type=@type");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.Int,4),
                    new SqlParameter("@type",SqlDbType.Int,4)
			};
            parameters[0].Value = ProductId;
            parameters[1].Value = type;

            Maticsoft.Model.Shop.Products.ProductStationMode model = new Maticsoft.Model.Shop.Products.ProductStationMode();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["StationId"] != null && ds.Tables[0].Rows[0]["StationId"].ToString() != "")
                {
                    model.StationId = int.Parse(ds.Tables[0].Rows[0]["StationId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProductId"] != null && ds.Tables[0].Rows[0]["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(ds.Tables[0].Rows[0]["ProductId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DisplaySequence"] != null && ds.Tables[0].Rows[0]["DisplaySequence"].ToString() != "")
                {
                    model.DisplaySequence = int.Parse(ds.Tables[0].Rows[0]["DisplaySequence"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GoodTypeID"] != null && ds.Tables[0].Rows[0]["GoodTypeID"].ToString() != "")
                {
                    model.GoodTypeID = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
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
            strSql.Append("SELECT * ");
            strSql.Append(" FROM Shop_ProductStationModes ");
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
            strSql.Append(" StationId,ProductId,DisplaySequence,Type ");
            strSql.Append(" FROM Shop_ProductStationModes ");
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
            strSql.Append("SELECT COUNT(1) FROM Shop_ProductStationModes ");
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
                strSql.Append("ORDER BY T.StationId desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_ProductStationModes T ");
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
            parameters[0].Value = "Shop_ProductStationModes";
            parameters[1].Value = "StationId";
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
        /// 根据type获得数据列表
        /// </summary>
        public DataSet GetListByType(string strType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select P.* From Shop_ProductStationModes S, Shop_Products P ");
            strSql.Append(" WHERE S.ProductId = P.ProductId ");
            if (!string.IsNullOrWhiteSpace(strType.Trim()))
            {
                strSql.Append(" and S.Type =" + strType);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int productId, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_ProductStationModes");
            strSql.Append(" WHERE ProductId=@ProductId and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)
			};
            parameters[0].Value = productId;
            parameters[1].Value = type;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int productId, int type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_ProductStationModes ");
            strSql.Append(" WHERE ProductId=@ProductId and Type=@Type ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4)
			};
            parameters[0].Value = productId;
            parameters[1].Value = type;

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
        public bool DeleteByType(int type, int categoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_ProductStationModes ");
            strSql.Append(" WHERE Type=@Type ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS ( SELECT DISTINCT *   FROM   Shop_ProductCategories ");
                strSql.AppendFormat(
                    " WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0} ) AND ProductId = Shop_ProductStationModes.ProductId ) ",
                    categoryId);
            }
            SqlParameter[] parameters = {
					new SqlParameter("@Type", SqlDbType.Int,4)
			};
            parameters[0].Value = type;

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
        public DataSet GetStationMode(int modeType, int categoryId, string pName, int supplierId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT PSM.ProductId,sort,PSM.StationId ");
            strSql.Append("FROM Shop_ProductStationModes PSM ");
            strSql.Append(" WHERE Type=@Type ");
            if (categoryId > 0)
            {
                strSql.Append(" AND EXISTS ( SELECT DISTINCT *   FROM   Shop_ProductCategories ");
                strSql.AppendFormat(
                    " WHERE  ( CategoryPath LIKE '{0}|%' OR CategoryId = {0} ) AND ProductId = PSM.ProductId ) ",
                    categoryId);
            }
            strSql.Append(" AND EXISTS ( SELECT *  FROM      Shop_Products WHERE  SaleStatus = 1   ");
            if (!String.IsNullOrWhiteSpace(pName))
            {

                strSql.AppendFormat(
                    "  AND ProductName LIKE '%{0}%'  ", pName);
            }
            if (supplierId > 0)
            {
                strSql.AppendFormat(
                                "  AND SupplierId = {0}  ", supplierId);
            }
            strSql.Append(" AND ProductId = PSM.ProductId )  ");
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Type", SqlDbType.Int, 4)
                };
            parameters[0].Value = modeType;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion
    }
}

