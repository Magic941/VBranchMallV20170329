/**
* ExpressTemplates.cs
*
* 功 能： N/A
* 类 名： ExpressTemplates
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/18 19:00:30   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.DBUtility;

//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Sales
{
    /// <summary>
    /// 数据访问类:ExpressTemplates
    /// </summary>
    public partial class ExpressTemplate : IDAL.Shop.Sales.IExpressTemplate
    {
        public ExpressTemplate()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ExpressId", "Shop_ExpressTemplates");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ExpressId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ExpressTemplates");
            strSql.Append(" where ExpressId=@ExpressId");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpressId", SqlDbType.Int,4)
			};
            parameters[0].Value = ExpressId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Shop.Sales.ExpressTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ExpressTemplates(");
            strSql.Append("ExpressName,XmlFile,IsUse)");
            strSql.Append(" values (");
            strSql.Append("@ExpressName,@XmlFile,@IsUse)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpressName", SqlDbType.NVarChar,100),
					new SqlParameter("@XmlFile", SqlDbType.NVarChar,200),
					new SqlParameter("@IsUse", SqlDbType.Bit,1)};
            parameters[0].Value = model.ExpressName;
            parameters[1].Value = model.XmlFile;
            parameters[2].Value = model.IsUse;

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
        public bool Update(Model.Shop.Sales.ExpressTemplate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ExpressTemplates set ");
            strSql.Append("ExpressName=@ExpressName,");
            strSql.Append("XmlFile=@XmlFile,");
            strSql.Append("IsUse=@IsUse");
            strSql.Append(" where ExpressId=@ExpressId");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpressName", SqlDbType.NVarChar,100),
					new SqlParameter("@XmlFile", SqlDbType.NVarChar,200),
					new SqlParameter("@IsUse", SqlDbType.Bit,1),
					new SqlParameter("@ExpressId", SqlDbType.Int,4)};
            parameters[0].Value = model.ExpressName;
            parameters[1].Value = model.XmlFile;
            parameters[2].Value = model.IsUse;
            parameters[3].Value = model.ExpressId;

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
        public bool Delete(int ExpressId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ExpressTemplates ");
            strSql.Append(" where ExpressId=@ExpressId");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpressId", SqlDbType.Int,4)
			};
            parameters[0].Value = ExpressId;

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
        public bool DeleteList(string ExpressIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ExpressTemplates ");
            strSql.Append(" where ExpressId in (" + ExpressIdlist + ")  ");
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
        public Model.Shop.Sales.ExpressTemplate GetModel(int ExpressId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ExpressId,ExpressName,XmlFile,IsUse from Shop_ExpressTemplates ");
            strSql.Append(" where ExpressId=@ExpressId");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpressId", SqlDbType.Int,4)
			};
            parameters[0].Value = ExpressId;

            Model.Shop.Sales.ExpressTemplate model = new Model.Shop.Sales.ExpressTemplate();
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
        public Model.Shop.Sales.ExpressTemplate DataRowToModel(DataRow row)
        {
            Model.Shop.Sales.ExpressTemplate model = new Model.Shop.Sales.ExpressTemplate();
            if (row != null)
            {
                if (row["ExpressId"] != null && row["ExpressId"].ToString() != "")
                {
                    model.ExpressId = int.Parse(row["ExpressId"].ToString());
                }
                if (row["ExpressName"] != null)
                {
                    model.ExpressName = row["ExpressName"].ToString();
                }
                if (row["XmlFile"] != null)
                {
                    model.XmlFile = row["XmlFile"].ToString();
                }
                if (row["IsUse"] != null && row["IsUse"].ToString() != "")
                {
                    if ((row["IsUse"].ToString() == "1") || (row["IsUse"].ToString().ToLower() == "true"))
                    {
                        model.IsUse = true;
                    }
                    else
                    {
                        model.IsUse = false;
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
            strSql.Append("select ExpressId,ExpressName,XmlFile,IsUse ");
            strSql.Append(" FROM Shop_ExpressTemplates ");
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
            strSql.Append(" ExpressId,ExpressName,XmlFile,IsUse ");
            strSql.Append(" FROM Shop_ExpressTemplates ");
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
            strSql.Append("select count(1) FROM Shop_ExpressTemplates ");
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
                strSql.Append("order by T.ExpressId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ExpressTemplates T ");
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
            parameters[0].Value = "Shop_ExpressTemplates";
            parameters[1].Value = "ExpressId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        public bool UpdateExpressName(int expressId, string expressName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Shop_ExpressTemplates SET ExpressName = @ExpressName WHERE ExpressId = @ExpressId");
            SqlParameter[] parameters = {
					new SqlParameter("@ExpressName", SqlDbType.NVarChar,200),
					new SqlParameter("@ExpressId", SqlDbType.Int)};
            parameters[0].Value = expressName;
            parameters[1].Value = expressId;

            return (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0);
        }
        #endregion  ExtensionMethod

    }
}

