/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductConsultationsType.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/08/24 17:43:18
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
    /// 数据访问类:ProductConsultationsType
    /// </summary>
    public partial class ProductConsultsType : IProductConsultsType
    {
        public ProductConsultsType()
        { }

        #region Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("TypeId", "Shop_ProductConsultationsType");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM Shop_ProductConsultationsType");
            strSql.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
            parameters[0].Value = TypeId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.ProductConsultsType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Shop_ProductConsultationsType(");
            strSql.Append("TypeName,CreatedDate,IsActive)");
            strSql.Append(" VALUES (");
            strSql.Append("@TypeName,@CreatedDate,@IsActive)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsActive", SqlDbType.Bit,1)};
            parameters[0].Value = model.TypeName;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.IsActive;

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
        public bool Update(Maticsoft.Model.Shop.Products.ProductConsultsType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_ProductConsultationsType SET ");
            strSql.Append("TypeName=@TypeName,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("IsActive=@IsActive");
            strSql.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IsActive", SqlDbType.Bit,1),
					new SqlParameter("@TypeId", SqlDbType.Int,4)};
            parameters[0].Value = model.TypeName;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.IsActive;
            parameters[3].Value = model.TypeId;

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
        public bool Delete(int TypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_ProductConsultationsType ");
            strSql.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
            parameters[0].Value = TypeId;

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
        public bool DeleteList(string TypeIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM Shop_ProductConsultationsType ");
            strSql.Append(" WHERE TypeId in (" + TypeIdlist + ")  ");
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
        public Maticsoft.Model.Shop.Products.ProductConsultsType GetModel(int TypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 TypeId,TypeName,CreatedDate,IsActive FROM Shop_ProductConsultationsType ");
            strSql.Append(" WHERE TypeId=@TypeId");
            SqlParameter[] parameters = {
					new SqlParameter("@TypeId", SqlDbType.Int,4)
			};
            parameters[0].Value = TypeId;

            Maticsoft.Model.Shop.Products.ProductConsultsType model = new Maticsoft.Model.Shop.Products.ProductConsultsType();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["TypeId"] != null && ds.Tables[0].Rows[0]["TypeId"].ToString() != "")
                {
                    model.TypeId = int.Parse(ds.Tables[0].Rows[0]["TypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TypeName"] != null && ds.Tables[0].Rows[0]["TypeName"].ToString() != "")
                {
                    model.TypeName = ds.Tables[0].Rows[0]["TypeName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatedDate"] != null && ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsActive"] != null && ds.Tables[0].Rows[0]["IsActive"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsActive"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsActive"].ToString().ToLower() == "true"))
                    {
                        model.IsActive = true;
                    }
                    else
                    {
                        model.IsActive = false;
                    }
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
            strSql.Append("SELECT TypeId,TypeName,CreatedDate,IsActive ");
            strSql.Append(" FROM Shop_ProductConsultationsType ");
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
            strSql.Append(" TypeId,TypeName,CreatedDate,IsActive ");
            strSql.Append(" FROM Shop_ProductConsultationsType ");
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
            strSql.Append("SELECT COUNT(1) FROM Shop_ProductConsultationsType ");
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
                strSql.Append("ORDER BY T.TypeId desc");
            }
            strSql.Append(")AS Row, T.*  FROM Shop_ProductConsultationsType T ");
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
            parameters[0].Value = "Shop_ProductConsultationsType";
            parameters[1].Value = "TypeId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion Method
    }
}