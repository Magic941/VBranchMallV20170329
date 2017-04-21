using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.SysManage
{
    public class MultiLanguage : Maticsoft.IDAL.SysManage.IMultiLanguage
    {

        #region  SA_MultiLang_det

        /// <summary>
        /// Whether there is Exists
        /// </summary>
        public bool Exists(string MultiLang_cField,int MultiLang_iPKValue,string MultiLang_cLang)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SA_MultiLang_det");
            strSql.Append(" where MultiLang_cField=@MultiLang_cField and MultiLang_iPKValue=@MultiLang_iPKValue and  MultiLang_cLang=@MultiLang_cLang");
            SqlParameter[] parameters = {
					new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar,50),
					new SqlParameter("@MultiLang_iPKValue", SqlDbType.Int),
					new SqlParameter("@MultiLang_cLang", SqlDbType.NVarChar,10)};
            parameters[0].Value = MultiLang_cField;
            parameters[1].Value = MultiLang_iPKValue;
            parameters[2].Value = MultiLang_cLang;
            
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// Add a record
        /// </summary>
        public int Add(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang, string MultiLang_cValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_MultiLang_det(");
            strSql.Append("MultiLang_cField,MultiLang_iPKValue,MultiLang_cLang,MultiLang_cValue)");
            strSql.Append(" values (");
            strSql.Append("@MultiLang_cField,@MultiLang_iPKValue,@MultiLang_cLang,@MultiLang_cValue)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar,50),
					new SqlParameter("@MultiLang_iPKValue", SqlDbType.Int),
					new SqlParameter("@MultiLang_cLang", SqlDbType.NVarChar,10),
					new SqlParameter("@MultiLang_cValue", SqlDbType.NVarChar,100)};
            parameters[0].Value = MultiLang_cField;
            parameters[1].Value = MultiLang_iPKValue;
            parameters[2].Value = MultiLang_cLang;
            parameters[3].Value = MultiLang_cValue;

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
        /// Update MultiLang_cValue
        /// </summary>
        public void Update(int MultiLang_iID, string MultiLang_cValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SA_MultiLang_det set ");            
            strSql.Append("MultiLang_cValue=@MultiLang_cValue");
            strSql.Append(" where MultiLang_iID=@MultiLang_iID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MultiLang_iID", SqlDbType.Int,4),					
					new SqlParameter("@MultiLang_cValue", SqlDbType.NVarChar,100)};
            parameters[0].Value = MultiLang_iID;      
            parameters[1].Value = MultiLang_cValue;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int MultiLang_iID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SA_MultiLang_det ");
            strSql.Append(" where MultiLang_iID=@MultiLang_iID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MultiLang_iID", SqlDbType.Int,4)};
            parameters[0].Value = MultiLang_iID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// Get an object entity
        /// </summary>
        public string GetModel(int MultiLang_iID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from SA_MultiLang_det ");
            strSql.Append(" where MultiLang_iID=@MultiLang_iID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MultiLang_iID", SqlDbType.Int,4)};
            parameters[0].Value = MultiLang_iID;            
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {                
                return  ds.Tables[0].Rows[0]["MultiLang_cValue"].ToString();                
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get an object entity
        /// </summary>
        public string GetModel(string MultiLang_cField, int MultiLang_iPKValue, string MultiLang_cLang)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from SA_MultiLang_det ");
            strSql.Append(" where MultiLang_cField=@MultiLang_cField and MultiLang_iPKValue=@MultiLang_iPKValue and  MultiLang_cLang=@MultiLang_cLang");

            SqlParameter[] parameters = {
					new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar,50),
					new SqlParameter("@MultiLang_iPKValue", SqlDbType.Int),
					new SqlParameter("@MultiLang_cLang", SqlDbType.NVarChar,10)};
            parameters[0].Value = MultiLang_cField;
            parameters[1].Value = MultiLang_iPKValue;
            parameters[2].Value = MultiLang_cLang;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["MultiLang_cValue"].ToString();
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
            strSql.Append("select * ");
            strSql.Append(" FROM SA_MultiLang_det ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 查询指定字段的，指定语言的 所有值数据
        /// </summary>
        public DataSet GetValueListByLang(string MultiLang_cField, string MultiLang_cLang)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MultiLang_iID,MultiLang_iPKValue,MultiLang_cValue ");
            strSql.Append(" FROM SA_MultiLang_det ");
            strSql.Append(" where MultiLang_cField=@MultiLang_cField and MultiLang_cLang=@MultiLang_cLang ");

            SqlParameter[] parameters = {
					new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar,50),
					new SqlParameter("@MultiLang_cLang", SqlDbType.NVarChar,10)
					};
            parameters[0].Value = MultiLang_cField;
            parameters[1].Value = MultiLang_cLang;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 查询指定字段，指定值的所有 语言数据
        /// </summary>
        public DataSet GetLangListByValue(string MultiLang_cField, int MultiLang_iPKValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MultiLang_iID,MultiLang_cLang,MultiLang_cValue ");
            strSql.Append(" FROM SA_MultiLang_det ");
            strSql.Append(" where MultiLang_cField=@MultiLang_cField and MultiLang_iPKValue=@MultiLang_iPKValue ");

            SqlParameter[] parameters = {
					new SqlParameter("@MultiLang_cField", SqlDbType.NVarChar,50),
					new SqlParameter("@MultiLang_iPKValue", SqlDbType.Int)
					};
            parameters[0].Value = MultiLang_cField;
            parameters[1].Value = MultiLang_iPKValue;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
            strSql.Append(" * ");
            strSql.Append(" FROM SA_MultiLang_det ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

       
        #endregion  Method


        #region  SA_Language_mstr

        public DataSet GetLanguageList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SA_Language_mstr ");            
            return DbHelperSQL.Query(strSql.ToString());
        }

        public string GetLanguageName(string Language_cCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Language_cName ");
            strSql.Append(" FROM SA_Language_mstr ");
            strSql.Append(" where Language_cCode= @Language_cCode");
            SqlParameter[] parameters = {
					new SqlParameter("@Language_cCode", SqlDbType.NVarChar,10)					
					};
            parameters[0].Value = Language_cCode;
            
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Language_cName"].ToString();
            }
            else
            {
                return null;
            }
        }

        public string GetDefaultLangCode()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Language_cCode ");
            strSql.Append(" FROM SA_Language_mstr ");
            strSql.Append(" where Language_bDefault= 1");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Language_cCode"].ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion



    }
}
