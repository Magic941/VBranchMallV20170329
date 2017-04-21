/**  版本信息模板在安装目录下，可自行修改。
* Shop_freefreight.cs
*
* 功 能： N/A
* 类 名： Shop_freefreight
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/10/22 16:32:56   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.shop.Freight;//Please add references

namespace Maticsoft.SQLServerDAL.shop.Freight
{
    /// <summary>
    /// 数据访问类:Shop_freefreight
    /// </summary>
    /// <summary>
    /// 数据访问类:Shop_ProductsFreight
    /// </summary>
    public partial class Shop_ProductsFreight : IShop_ProductsFreight
    {
        public Shop_ProductsFreight()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ProductCode, string SKU)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ProductsFreight");
            strSql.Append(" where ProductCode=@ProductCode and SKU=@SKU ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SKU", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ProductCode;
            parameters[1].Value = SKU;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.shop.Freight.Shop_ProductsFreight model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ProductsFreight(");
            strSql.Append("ProductCode,SKU,Freight,ModeId,Eidtor,AddTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@ProductCode,@SKU,@Freight,@ModeId,@Eidtor,@AddTime,@UpdateTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SKU", SqlDbType.NVarChar,50),
					new SqlParameter("@Freight", SqlDbType.Float,8),
					new SqlParameter("@ModeId", SqlDbType.Int,4),
					new SqlParameter("@Eidtor", SqlDbType.NVarChar,100),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.ProductCode;
            parameters[1].Value = model.SKU;
            parameters[2].Value = model.Freight;
            parameters[3].Value = model.ModeId;
            parameters[4].Value = model.Eidtor;
            parameters[5].Value = model.AddTime;
            parameters[6].Value = model.UpdateTime;

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
        public bool Update(Maticsoft.Model.shop.Freight.Shop_ProductsFreight model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ProductsFreight set ");
            strSql.Append("Freight=@Freight,");
            strSql.Append("ModeId=@ModeId,");
            strSql.Append("Eidtor=@Eidtor,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ProductCode=@ProductCode and SKU=@SKU ");
            SqlParameter[] parameters = {
					new SqlParameter("@Freight", SqlDbType.Float,8),
					new SqlParameter("@ModeId", SqlDbType.Int,4),
					new SqlParameter("@Eidtor", SqlDbType.NVarChar,100),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SKU", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Freight;
            parameters[1].Value = model.ModeId;
            parameters[2].Value = model.Eidtor;
            parameters[3].Value = model.AddTime;
            parameters[4].Value = model.UpdateTime;
            parameters[5].Value = model.ProductCode;
            parameters[6].Value = model.SKU;

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
        public bool Delete(string ProductCode, string SKU)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ProductsFreight ");
            strSql.Append(" where ProductCode=@ProductCode and SKU=@SKU ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SKU", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ProductCode;
            parameters[1].Value = SKU;

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
        public Maticsoft.Model.shop.Freight.Shop_ProductsFreight GetModel(string ProductCode, string SKU)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProductCode,SKU,Freight,ModeId,Eidtor,AddTime,UpdateTime from Shop_ProductsFreight ");
            strSql.Append(" where ProductCode=@ProductCode and SKU=@SKU ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@SKU", SqlDbType.NVarChar,50)			};
            parameters[0].Value = ProductCode;
            parameters[1].Value = SKU;

            Maticsoft.Model.shop.Freight.Shop_ProductsFreight model = new Maticsoft.Model.shop.Freight.Shop_ProductsFreight();
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
        public Maticsoft.Model.shop.Freight.Shop_ProductsFreight DataRowToModel(DataRow row)
        {
            Maticsoft.Model.shop.Freight.Shop_ProductsFreight model = new Maticsoft.Model.shop.Freight.Shop_ProductsFreight();
            if (row != null)
            {
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["SKU"] != null)
                {
                    model.SKU = row["SKU"].ToString();
                }
                if (row["Freight"] != null && row["Freight"].ToString() != "")
                {
                    model.Freight = decimal.Parse(row["Freight"].ToString());
                }
                if (row["ModeId"] != null && row["ModeId"].ToString() != "")
                {
                    model.ModeId = int.Parse(row["ModeId"].ToString());
                }
                if (row["Eidtor"] != null)
                {
                    model.Eidtor = row["Eidtor"].ToString();
                }
                if (row["AddTime"] != null && row["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(row["AddTime"].ToString());
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row.Table.Columns.Contains("ProductID"))
                {
                    if (row["ProductID"] != null && row["ProductID"].ToString() != "")
                    {
                        model.ProductID = long.Parse(row["ProductID"].ToString());
                    }
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
            strSql.Append("select ProductCode,SKU,Freight,ModeId,Eidtor,AddTime,UpdateTime ");
            strSql.Append(" FROM Shop_ProductsFreight ");
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
            strSql.Append(" ProductCode,SKU,Freight,ModeId,Eidtor,AddTime,UpdateTime ");
            strSql.Append(" FROM Shop_ProductsFreight ");
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
            strSql.Append("select count(1) FROM Shop_ProductsFreight ");
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
                strSql.Append("order by T.SKU desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ProductsFreight T ");
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
            parameters[0].Value = "Shop_ProductsFreight";
            parameters[1].Value = "SKU";
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
        public Maticsoft.Model.shop.Freight.Shop_ProductsFreight GetModel(long productID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1 a.ProductId, b.ProductCode ,b.SKU ,b.Freight ,b.ModeId ,b.Eidtor ,b.AddTime ,b.UpdateTime FROM dbo.Shop_Products  a ");
            strSql.Append(" INNER JOIN dbo.Shop_ProductsFreight b ON a.ProductCode =b.ProductCode ");
            strSql.Append(" WHERE a.ProductId =@productID");
            SqlParameter[] parameters = {
					new SqlParameter("@productID", SqlDbType.BigInt)	
                                        };
            parameters[0].Value = productID;

            Maticsoft.Model.shop.Freight.Shop_ProductsFreight model = new Maticsoft.Model.shop.Freight.Shop_ProductsFreight();
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


        
        #endregion  ExtensionMethod
    }
}

