using System;
using System.Data;
using System.Text;
using Maticsoft.Accounts.IData;
using MySql.Data.MySqlClient;

namespace Maticsoft.Accounts.MySqlData
{
    /// <summary>
    /// �û����͹���
    /// </summary>
    [Serializable]
    public class UserType : IUserType
    {
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string UserType, string Description)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_UserType");
            strSql.Append(" where UserType=?UserType ");
            strSql.Append(" and Description=?Description ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("?UserType", MySqlDbType.VarChar,2),
            new MySqlParameter("?Description", MySqlDbType.VarChar,50)};
            parameters[0].Value = UserType;
            parameters[1].Value = Description;
            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(string UserType, string Description)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_UserType(");
            strSql.Append("UserType,Description)");
            strSql.Append(" values (");
            strSql.Append("?UserType,?Description)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("?UserType", MySqlDbType.VarChar,2),
                    new MySqlParameter("?Description", MySqlDbType.VarChar,100)};
            parameters[0].Value = UserType;
            parameters[1].Value = Description;

            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(string UserType, string Description)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_UserType set ");
            strSql.Append("Description=?Description");
            strSql.Append(" where UserType=?UserType ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("?UserType", MySqlDbType.VarChar,2),
                    new MySqlParameter("?Description", MySqlDbType.VarChar,100)};
            parameters[0].Value = UserType;
            parameters[1].Value = Description;
            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string UserType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Accounts_UserType ");
            strSql.Append(" where UserType=?UserType ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("?UserType", MySqlDbType.VarChar,2)};
            parameters[0].Value = UserType;

            DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// �õ���������
        /// </summary>
        public string GetDescription(string UserType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Description from Accounts_UserType ");
            strSql.Append(" where UserType=?UserType LIMIT 1");
            MySqlParameter[] parameters = {
                    new MySqlParameter("?UserType", MySqlDbType.VarChar,2)};
            parameters[0].Value = UserType;
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Description"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserType,Description ");
            strSql.Append(" FROM Accounts_UserType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
    }
}
