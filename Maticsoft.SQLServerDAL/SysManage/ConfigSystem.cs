using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

namespace Maticsoft.SQLServerDAL.SysManage
{
    /// <summary>
    /// Config system
    /// </summary>
    public class ConfigSystem : Maticsoft.IDAL.SysManage.IConfigSystem
    {

        #region  Method

        /// <summary>
        /// Whether there is Exists
        /// </summary>
        public bool Exists(string Keyname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SA_Config_System");
            strSql.Append(" where Keyname=@Keyname ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Keyname", SqlDbType.NVarChar)};
            parameters[0].Value = Keyname;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// Add a record
        /// </summary>
        public int Add(string Keyname, string Value, string Description)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_Config_System(");
            strSql.Append("Keyname,Value,Description)");
            strSql.Append(" values (");
            strSql.Append("@Keyname,@Value,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Keyname", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.NVarChar,-1),
                    new SqlParameter("@Description", SqlDbType.NVarChar,200)};
            parameters[0].Value = Keyname;
            parameters[1].Value = Value;
            parameters[2].Value = Description;

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
        /// Add a record
        /// </summary>
        public int Add(string Keyname, string Value, string Description, Maticsoft.Model.SysManage.ApplicationKeyType KeyType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_Config_System(");
            strSql.Append("Keyname,Value,Description,KeyType)");
            strSql.Append(" values (");
            strSql.Append("@Keyname,@Value,@Description,@KeyType)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Keyname", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.NVarChar,-1),
                    new SqlParameter("@Description", SqlDbType.NVarChar,200),
                    new SqlParameter("@KeyType", SqlDbType.Int)};
            parameters[0].Value = Keyname;
            parameters[1].Value = Value;
            parameters[2].Value = Description;
            parameters[3].Value = (int)(KeyType);

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

        public void Update(int ID, string Keyname, string Value, string Description)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SA_Config_System set ");
            strSql.Append("Keyname=@Keyname,");
            strSql.Append("Value=@Value,");
            strSql.Append("Description=@Description");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@Keyname", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value",SqlDbType.NVarChar,-1),
                    new SqlParameter("@Description", SqlDbType.NVarChar,200)};
            parameters[0].Value = ID;
            parameters[1].Value = Keyname;
            parameters[2].Value = Value;
            parameters[3].Value = Description;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        public bool Update(string Keyname, string Value, string Description)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SA_Config_System set ");
            strSql.Append("Value=@Value,");
            strSql.Append("Description=@Description");
            strSql.Append(" where Keyname=@Keyname ");
            SqlParameter[] parameters = {					
                    new SqlParameter("@Keyname", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.NVarChar,-1),
                    new SqlParameter("@Description", SqlDbType.NVarChar,200)};
            parameters[0].Value = Keyname;
            parameters[1].Value = Value;
            parameters[2].Value = Description;

            return (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        public bool Update(string Keyname, string Value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SA_Config_System SET ");
            strSql.Append("Value=@Value");
            strSql.Append(" WHERE Keyname=@Keyname");
            SqlParameter[] parameters = {					
                    new SqlParameter("@Keyname", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.NVarChar,-1),
                    new SqlParameter("@KeyType", SqlDbType.Int)};
            parameters[0].Value = Keyname;
            parameters[1].Value = Value;

            return (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        public bool Update(string Keyname, string Value, Maticsoft.Model.SysManage.ApplicationKeyType KeyType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SA_Config_System SET ");
            strSql.Append("Value=@Value");
            strSql.Append(" WHERE Keyname=@Keyname AND KeyType=@KeyType");
            SqlParameter[] parameters = {					
                    new SqlParameter("@Keyname", SqlDbType.NVarChar,50),
                    new SqlParameter("@Value", SqlDbType.NVarChar,-1),
                    new SqlParameter("@KeyType", SqlDbType.Int)};
            parameters[0].Value = Keyname;
            parameters[1].Value = Value;
            parameters[2].Value = (int)(KeyType);

            return (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_Config_System ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Get an object entity
        /// </summary>
        public string GetValue(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Value from SA_Config_System ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// Get an object entity
        /// </summary>
        public string GetValue(string Keyname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  Value from SA_Config_System ");
            strSql.Append(" where Keyname=@Keyname ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Keyname", SqlDbType.NVarChar)};
            parameters[0].Value = Keyname;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }


        /// <summary>
        /// Query data list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Keyname,Value,Description ");
            strSql.Append(" FROM SA_Config_System ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        #endregion  Method

        public void UpdateConnectionString(string connectionString)
        {
            DbHelperSQL.connectionString = connectionString;
        }
    }
}
