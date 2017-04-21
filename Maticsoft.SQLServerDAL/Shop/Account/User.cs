using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.SQLServerDAL.Shop.Account
{
    public class User
    {
        /// <summary>
        ///根据手机号获取用户对象
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Maticsoft.Accounts.Bus.User GetPhoneUser(string phone,string password ="")
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 * from Accounts_Users ");
                strSql.Append(" where Phone=@Phone ");
                strSql.Append(" AND IsPhoneVerify=1");
                SqlParameter[] parameters = {
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
                    new SqlParameter("@Password", SqlDbType.Binary,20)			};
                parameters[0].Value = phone;
                parameters[1].Value = AccountsPrincipal.EncryptPassword(password);

                if (!string.IsNullOrEmpty(password))
                {
                    strSql.Append(" and Password=@Password ");
                }
                

                Maticsoft.Accounts.Bus.User user = new Accounts.Bus.User();
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
            catch(Exception)
            {
                return null;
            }
        }

        

        /// <summary>
        /// 将用户数据转化成用户对象
        /// </summary>
        /// <param name="RowUser"></param>
        /// <returns></returns>
        public Maticsoft.Accounts.Bus.User DataRowToModel(DataRow RowUser)
        {
            try
            {
                Maticsoft.Accounts.Bus.User user = new Accounts.Bus.User();
                user.UserName = RowUser["UserName"].ToString();
                user.Password = System.Text.Encoding.Default.GetBytes(RowUser["Password"].ToString());
                user.NickName = RowUser["NickName"].ToString();
                user.TrueName = RowUser["TrueName"].ToString();
                user.Sex = RowUser["Sex"].ToString();
                user.Phone = RowUser["Phone"].ToString();
                user.Email = RowUser["Email"].ToString();
                user.EmployeeID = RowUser["EmployeeID"]==null?0:string.IsNullOrWhiteSpace(RowUser["EmployeeID"].ToString())?0:Convert.ToInt32(RowUser["EmployeeID"]);
                user.DepartmentID = RowUser["DepartmentID"].ToString();
                user.Activity = Convert.ToBoolean(RowUser["Activity"]);
                user.UserType = RowUser["UserType"].ToString();
                user.UserID = Convert.ToInt32(RowUser["UserID"]);
                user.Style = RowUser["Style"] == null ? 0 : string.IsNullOrWhiteSpace(RowUser["Style"].ToString()) ? 0 : Convert.ToInt32(RowUser["Style"]);
                return user;
            }
            catch (Exception)
            { return null; }
        }

        //IsPhoneVerify 0为false 1为true
        public bool SetPhoneMark(string username,string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Users Set ");
            strSql.Append("IsPhoneVerify= 1 ,phone=@phone");
            strSql.Append(" WHere username=@username ");
            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.NVarChar,200),
                    new SqlParameter("@phone",SqlDbType.NVarChar,100)
                                        };
            parameters[0].Value = username;
            parameters[1].Value = phone;
            var x = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

            if (x > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>true是这个手机号已经被验证过</returns>
        public bool CheckPhoneVerify(string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_Users ");
            strSql.Append(" where Phone=@Phone ");
            strSql.Append(" AND IsPhoneVerify=1");
            SqlParameter[] parameters = {
					new SqlParameter("@Phone", SqlDbType.NVarChar,200)
                                        };
            parameters[0].Value = phone;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>IsPhoneVerify 0为false 1为true</returns>
        public bool GetPhoneMarkByUserName(string username)
        {
            var u = new Accounts.Data.User();
            var data = u.Retrieve(username);

            if ((!Object.Equals(data["IsPhoneVerify"], null)) && (!Object.Equals(data["IsPhoneVerify"], System.DBNull.Value)))
            {
                var result = Convert.ToBoolean(data["IsPhoneVerify"]);
                return result;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>IsPhoneVerify 0为false 1为true</returns>
        public bool GetPhoneMarkByID(int userid)
        {
            var u = new Accounts.Data.User();
            var data = u.Retrieve(userid);
            if (data != null)
            {
                if ((!Object.Equals(data["IsPhoneVerify"], null)) && (!Object.Equals(data["IsPhoneVerify"], System.DBNull.Value)))
                {
                    var result = Convert.ToBoolean(data["IsPhoneVerify"]);
                    return result;
                }
            }
            return false;
        }


        public bool CheckPhoneExits(string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID from Accounts_Users");
            strSql.Append(" where phone=@phone");
            strSql.Append(" AND IsPhoneVerify=1");
            SqlParameter[] parameters = {
					new SqlParameter("@phone", SqlDbType.NVarChar,20)
			};
            parameters[0].Value = phone;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        public bool UpdateUserName(string oldusername, string newusername)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_Users SET ");
            strSql.Append("username=@newusername ");
            strSql.Append(" Where username=@oldusername");
            SqlParameter[] parameters = {
					new SqlParameter("@newusername", SqlDbType.NVarChar,40),
                    new SqlParameter("@oldusername",SqlDbType.NVarChar,40)
										 };
            parameters[0].Value = newusername;
            parameters[1].Value = oldusername;

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


        public bool UpdateUserName4Accounts(string oldusername,string newusername)
        {

            List<CommandInfo> list = new List<CommandInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_Users SET ");
            strSql.Append("username=@newusername ");
            strSql.Append(" Where username=@oldusername");
            SqlParameter[] parameters = {
					new SqlParameter("@newusername", SqlDbType.NVarChar,40),
                    new SqlParameter("@oldusername",SqlDbType.NVarChar,40)
										 };
            parameters[0].Value = newusername;
            parameters[1].Value = oldusername;

            list.Add(new CommandInfo(strSql.ToString(), parameters, EffentNextType.ExcuteEffectRows));

            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE Shop_CardUserInfo SET ");
            sql.Append("UserId=@newusername ");
            sql.Append(" Where UserId=@oldusername");
            SqlParameter[] param = {
					new SqlParameter("@newusername", SqlDbType.NVarChar,40),
                    new SqlParameter("@oldusername",SqlDbType.NVarChar,40)
										 };
            param[0].Value = newusername;
            param[1].Value = oldusername;
            list.Add(new CommandInfo(sql.ToString(), param, EffentNextType.ExcuteEffectRows));

             using (SqlConnection connection = DbHelperSQL.GetConnection)
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {

                        DbHelperSQL.ExecuteSqlTran4Indentity(list,transaction);


                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
