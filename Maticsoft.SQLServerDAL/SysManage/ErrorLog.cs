using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

namespace Maticsoft.SQLServerDAL.SysManage
{
    public class ErrorLog : Maticsoft.IDAL.SysManage.IErrorLog
    {
        /// <summary>
        /// Add a record
        /// </summary>
        public int Add(Maticsoft.Model.SysManage.ErrorLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_ErrorLog(");
            strSql.Append("OPTime,Url,Loginfo,StackTrace)");
            strSql.Append(" values (");
            strSql.Append("@OPTime,@Url,@Loginfo,@StackTrace)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@OPTime", SqlDbType.DateTime),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Loginfo", SqlDbType.NVarChar),
					new SqlParameter("@StackTrace", SqlDbType.NVarChar)};
            parameters[0].Value = DateTime.Now.ToString();
            parameters[1].Value = model.Url;
            parameters[2].Value = model.Loginfo;
            parameters[3].Value = model.StackTrace;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// Update a record
        /// </summary>
        public void Update(Maticsoft.Model.SysManage.ErrorLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SA_ErrorLog set ");
            strSql.Append("OPTime=@OPTime,");
            strSql.Append("Url=@Url,");
            strSql.Append("Loginfo=@Loginfo,");
            strSql.Append("StackTrace=@StackTrace");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@OPTime", SqlDbType.DateTime),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@Loginfo", SqlDbType.NVarChar),
					new SqlParameter("@StackTrace", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = DateTime.Now.ToString();
            parameters[2].Value = model.Url;
            parameters[3].Value = model.Loginfo;
            parameters[4].Value = model.StackTrace;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_ErrorLog ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// Delete record
        /// </summary>
        public void Delete(string IDList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_ErrorLog ");
            strSql.Append(" where ID in ("+IDList+") ");            
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除某一日期之前的数据
        /// </summary>
        /// <param name="dtDateBefore">日期</param>
        public void DeleteByDate(DateTime dtDateBefore)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SA_ErrorLog ");
            strSql.Append(" where OPTime <= @OPTime");
            SqlParameter[] parameters = {
					new SqlParameter("@OPTime", SqlDbType.DateTime)
				};
            parameters[0].Value = dtDateBefore;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Get an object entity
        /// </summary>
        public Maticsoft.Model.SysManage.ErrorLog GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,OPTime,Url,Loginfo,StackTrace from SA_ErrorLog ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OPTime"].ToString() != "")
                {
                    model.OPTime = DateTime.Parse(ds.Tables[0].Rows[0]["OPTime"].ToString());
                }
                model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                model.Loginfo = ds.Tables[0].Rows[0]["Loginfo"].ToString();
                model.StackTrace = ds.Tables[0].Rows[0]["StackTrace"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Query data list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,OPTime,Url,Loginfo,StackTrace ");
            strSql.Append(" FROM SA_ErrorLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// Query top lines of data 
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,OPTime,Url,Loginfo,StackTrace ");
            strSql.Append(" FROM SA_ErrorLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /*
        /// <summary>
        /// Paging data list
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
            parameters[0].Value = "SA_ErrorLog";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/


    }
}
