using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Data
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Serializable]
    public class User : IUser
    {
        public User()
        { }

        #region 增加用户

        /// <summary>
        /// 创建用户
        /// </summary>
        public int Create(string userName,
            byte[] password,
            string nickName,
            string trueName,
            string sex,
            string phone,
            string email,
            int employeeID,
            string departmentID,
            bool activity,
            string userType,
            int style,
            int User_iCreator,
            DateTime User_dateValid,
            string User_cLang
            )
        {
            if (userName == null) throw new ArgumentNullException("userName");
            if (password == null) throw new ArgumentNullException("password");

            int rowsAffected;
            SqlParameter[]
                parameters = {
                                 new SqlParameter("@UserName", SqlDbType.NVarChar, 100),
                                 new SqlParameter("@Password", SqlDbType.Binary, 20),
                                 new SqlParameter("@NickName", SqlDbType.NVarChar, 50),
                                 new SqlParameter("@TrueName", SqlDbType.NVarChar, 50),
                                 new SqlParameter("@Sex", SqlDbType.Char, 2),
                                 new SqlParameter("@Phone", SqlDbType.NVarChar, 20),
                                 new SqlParameter("@Email", SqlDbType.NVarChar, 100),
                                 new SqlParameter("@EmployeeID", SqlDbType.Int, 4),
                                 new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15),
                                 new SqlParameter("@Activity", SqlDbType.Bit, 1),
                                 new SqlParameter("@UserType", SqlDbType.Char, 2),
                                 new SqlParameter("@UserID", SqlDbType.Int, 4),
                                 new SqlParameter("@Style", SqlDbType.Int, 4),
                                 new SqlParameter("@User_iCreator", SqlDbType.Int, 4),
                                 new SqlParameter("@User_dateCreate", SqlDbType.DateTime),
                                 new SqlParameter("@User_dateValid", SqlDbType.DateTime),                                                                
                                 new SqlParameter("@User_cLang",  SqlDbType.NVarChar, 10)
                             };

            parameters[0].Value = userName;
            parameters[1].Value = password;
            parameters[2].Value = nickName;
            parameters[3].Value = trueName;
            parameters[4].Value = sex;
            parameters[5].Value = phone;
            parameters[6].Value = email;
            parameters[7].Value = employeeID;
            parameters[8].Value = departmentID;
            parameters[9].Value = activity ? 1 : 0;
            parameters[10].Value = userType;
            parameters[11].Direction = ParameterDirection.Output;
            parameters[12].Value = style;
            parameters[13].Value = User_iCreator;
            parameters[14].Value = DateTime.Now;
            parameters[15].Value = DateTime.Now;
            parameters[16].Value = User_cLang;

            try
            {
                DbHelperSQL.RunProcedure("sp_Accounts_CreateUser", parameters, out rowsAffected);
            }
            catch (SqlException e)
            {
                if (e.Number == 2601)
                {
                    return -100;
                }
            }

            return (int)parameters[11].Value;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public int Create(string userName,
            byte[] password,
            string nickName,
            string trueName,
            string sex,
            string phone,
            string email,
            int employeeID,
            string departmentID,
            bool activity,
            string userType,
            int style,
            int User_iCreator,
            DateTime User_dateCreate,
            DateTime User_dateValid,
            string User_cLang
            )
        {
            if (userName == null) throw new ArgumentNullException("userName");
            if (password == null) throw new ArgumentNullException("password");

            int rowsAffected;
            SqlParameter[]
                parameters = {
                                 new SqlParameter("@UserName", SqlDbType.NVarChar, 100),
                                 new SqlParameter("@Password", SqlDbType.Binary, 20),
                                 new SqlParameter("@NickName", SqlDbType.NVarChar, 50),
                                 new SqlParameter("@TrueName", SqlDbType.NVarChar, 50),
                                 new SqlParameter("@Sex", SqlDbType.Char, 2),
                                 new SqlParameter("@Phone", SqlDbType.NVarChar, 20),
                                 new SqlParameter("@Email", SqlDbType.NVarChar, 100),
                                 new SqlParameter("@EmployeeID", SqlDbType.Int, 4),
                                 new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15),
                                 new SqlParameter("@Activity", SqlDbType.Bit, 1),
                                 new SqlParameter("@UserType", SqlDbType.Char, 2),
                                 new SqlParameter("@UserID", SqlDbType.Int, 4),
                                 new SqlParameter("@Style", SqlDbType.Int, 4),
                                 new SqlParameter("@User_iCreator", SqlDbType.Int, 4),
                                 new SqlParameter("@User_dateCreate", SqlDbType.DateTime),
                                 new SqlParameter("@User_dateValid", SqlDbType.DateTime),                                                                
                                 new SqlParameter("@User_cLang",  SqlDbType.NVarChar, 10)
                             };

            parameters[0].Value = userName;
            parameters[1].Value = password;
            parameters[2].Value = nickName;
            parameters[3].Value = trueName;
            parameters[4].Value = sex;
            parameters[5].Value = phone;
            parameters[6].Value = email;
            parameters[7].Value = employeeID;
            parameters[8].Value = departmentID;
            parameters[9].Value = activity ? 1 : 0;
            parameters[10].Value = userType;
            parameters[11].Direction = ParameterDirection.Output;
            parameters[12].Value = style;
            parameters[13].Value = User_iCreator;
            parameters[14].Value = User_dateCreate == DateTime.MinValue ? DateTime.Now : User_dateCreate;
            parameters[15].Value = DateTime.Now;
            parameters[16].Value = User_cLang;

            try
            {
                DbHelperSQL.RunProcedure("sp_Accounts_CreateUser", parameters, out rowsAffected);
            }
            catch (SqlException e)
            {
                if (e.Number == 2601)
                {
                    return -100;
                }
            }

            return (int)parameters[11].Value;
        }
        #endregion

        #region 得到用户信息
        /// <summary>
        /// 根据UserID查询用户详细信息
        /// </summary>
        public DataRow Retrieve(int userID)
        {
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;

            using (DataSet users = DbHelperSQL.RunProcedure("sp_Accounts_GetUserDetails", parameters, "Users"))
            {
                if (users.Tables[0].Rows.Count > 0)
                {
                    return users.Tables[0].Rows[0];
                }
                else
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// 根据UserName查询用户详细信息
        /// </summary>
        public DataRow Retrieve(string userName)
        {
            SqlParameter[] parameters = { new SqlParameter("@UserName", SqlDbType.NVarChar, 100) };
            parameters[0].Value = userName;

            using (DataSet users = DbHelperSQL.RunProcedure("sp_Accounts_GetUserDetailsByUserName", parameters, "Users"))
            {
                if (users.Tables[0].Rows.Count == 0)
                {
                    throw new System.Security.Principal.IdentityNotMappedException("无此用户或用户已过期：" + userName);
                }
                else
                    return users.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// 根据NickName查询用户详细信息
        /// </summary>
        public DataRow RetrieveByNickName(string nickName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top(1) from Accounts_Users ");
            strSql.Append("where NickName = @NickName ");

            SqlParameter[] parameters = { new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            parameters[0].Value = nickName;
            DataSet users = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (users.Tables[0].Rows.Count > 0)
            {
                return users.Tables[0].Rows[0];
            }
            return null;
        }

        #endregion

        #region 是否存在
        /// <summary>
        /// 用户名是否已经存在
        /// </summary>
        [Obsolete]
        public bool HasUser(string userName)
        {
            SqlParameter[] parameters = { new SqlParameter("@UserName", SqlDbType.NVarChar, 100) };
            parameters[0].Value = userName;

            using (DataSet users = DbHelperSQL.RunProcedure("sp_Accounts_GetUserDetailsByUserName", parameters, "Users"))
            {
                if (users.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 用户名是否已经存在
        /// </summary>
        public bool HasUserByUserName(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users ");
            strSql.Append("where UserName = @UserName ");
            SqlParameter[]
                parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100)
                             };
            parameters[0].Value = userName;
            return (DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0].Rows.Count > 0);
        }
        /// <summary>
        /// 邮箱是否已经存在
        /// </summary>
        public bool HasUserByEmail(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users ");
            strSql.Append("where Email = @Email ");
            SqlParameter[]
                parameters = {
                    new SqlParameter("@Email", SqlDbType.NVarChar,100)
                             };
            parameters[0].Value = email;
            return (DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0].Rows.Count > 0);
        }
        /// <summary>
        /// 昵称是否已经存在
        /// </summary>
        public bool HasUserByNickName(string nickName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users ");
            strSql.Append("where NickName = @NickName ");
            SqlParameter[]
                parameters = {
                    new SqlParameter("@NickName", SqlDbType.NVarChar,50)
                             };
            parameters[0].Value = nickName;
            return (DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0].Rows.Count > 0);
        }
        /// <summary>
        /// 手机是否已经存在
        /// </summary>
        public bool HasUserByPhone(string phone)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users ");
            strSql.Append("where Phone = @Phone and IsPhoneVerify=1");
            SqlParameter[]
                parameters = {
                    new SqlParameter("@Phone", SqlDbType.NVarChar,20)
                             };
            parameters[0].Value = phone;
            return (DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0].Rows.Count > 0);
        }
        /// <summary>
        /// 手机是否已经存在
        /// </summary>
        public bool HasUserByPhone(string phone, string userType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users ");
            strSql.Append("where Phone = @Phone and UserType = @UserType and IsPhoneVerify=1");
            SqlParameter[]
                parameters = {
                    new SqlParameter("@Phone", SqlDbType.NVarChar,20),
                    new SqlParameter("@UserType", SqlDbType.Char,2)
                             };
            parameters[0].Value = phone;
            parameters[1].Value = userType;
            return (DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0].Rows.Count > 0);
        }
        #endregion

        #region 修改用户

        /// <summary>
        /// 更新用户状态
        /// </summary>
        public bool UpdateActivity(int userId, bool activity)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Accounts_Users ");
            strSql.Append("SET Activity=@Activity ");
            strSql.Append("WHERE UserID=@UserID");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Activity", SqlDbType.Bit,1)};
            parameters[0].Value = userId;
            parameters[1].Value = activity;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return (rows > 0);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        public bool Update(int userID,
            string userName,
            byte[] password,
            string nickName,
            string trueName,
            string sex,
            string phone,
            string email,
            int employeeID,
            string departmentID,
            bool activity,
            string userType,
            int style
            )
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                                            new SqlParameter("@UserName", SqlDbType.NVarChar, 100),
                                            new SqlParameter("@Password", SqlDbType.Binary, 20),
                                            new SqlParameter("@NickName", SqlDbType.NVarChar, 50),
                                            new SqlParameter("@TrueName", SqlDbType.NVarChar, 50),
                                            new SqlParameter("@Sex", SqlDbType.Char, 2),
                                            new SqlParameter("@Phone", SqlDbType.NVarChar, 20),
                                            new SqlParameter("@Email", SqlDbType.NVarChar, 100),
                                            new SqlParameter("@EmployeeID", SqlDbType.Int, 4),
                                            new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 15),
                                            new SqlParameter("@Activity", SqlDbType.Bit, 1),
                                            new SqlParameter("@UserType", SqlDbType.Char, 2),
                                            new SqlParameter("@UserID", SqlDbType.Int, 4),
                                            new SqlParameter("@Style", SqlDbType.Int,4)
                                            
                                        };

            parameters[0].Value = userName;
            parameters[1].Value = password;
            parameters[2].Value = nickName;
            parameters[3].Value = trueName;
            parameters[4].Value = sex;
            parameters[5].Value = phone;
            parameters[6].Value = email;
            parameters[7].Value = employeeID;
            parameters[8].Value = departmentID;
            parameters[9].Value = activity;
            parameters[10].Value = userType;
            parameters[11].Value = userID;
            parameters[12].Value = style;

            DbHelperSQL.RunProcedure("sp_Accounts_UpdateUser", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// 设置部门和员工编号
        /// </summary>
        public bool Update(int UserID, int EmployeeID, string DepartmentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Users set ");
            strSql.Append("EmployeeID=@EmployeeID,");
            strSql.Append("DepartmentID=@DepartmentID ");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@EmployeeID", SqlDbType.Int,4),
                    new SqlParameter("@DepartmentID", SqlDbType.NVarChar,15)
                };
            parameters[0].Value = UserID;
            parameters[1].Value = EmployeeID;
            parameters[2].Value = DepartmentID;
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
        /// 设置审核
        /// </summary>
        public bool Update(int UserID, int User_iApprover, int User_iApproveState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Users set ");
            strSql.Append("User_iApprover=@User_iApprover,");
            strSql.Append("User_dateApprove=@User_dateApprove,");
            strSql.Append("User_iApproveState=@User_iApproveState");
            strSql.Append(" where UserID=@UserID");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4),					
                    new SqlParameter("@User_iApprover", SqlDbType.Int,4),
                    new SqlParameter("@User_dateApprove", SqlDbType.DateTime),
                    new SqlParameter("@User_iApproveState", SqlDbType.Int,4)};
            parameters[0].Value = UserID;
            parameters[1].Value = User_iApprover;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = User_iApproveState;
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
        /// 设置用户密码
        /// </summary>
        public bool SetPassword(string UserName, byte[] encPassword)
        {
            int rowsAffected;
            SqlParameter[] parameters = 
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar),
                new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20)
            };

            parameters[0].Value = UserName;
            parameters[1].Value = encPassword;

            DbHelperSQL.RunProcedure("sp_Accounts_SetPassword", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }
        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        public bool Delete(int userID)
        {
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            int rowsAffected;

            parameters[0].Value = userID;

            DbHelperSQL.RunProcedure("sp_Accounts_DeleteUser", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }
        #endregion

        #region 验证登陆信息

        /// <summary>
        /// 验证用户登录信息
        /// 用户名登录
        /// </summary>
        public int ValidateLogin(string userName, byte[] encPassword)
        {
            int rowsAffected;
            SqlParameter[] parameters = 	  {
                new SqlParameter("@UserName", SqlDbType.NVarChar, 100),
                new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20)};

            parameters[0].Value = userName;
            parameters[1].Value = encPassword;

            return DbHelperSQL.RunProcedure("sp_Accounts_ValidateLogin", parameters, out rowsAffected);
        }

        /// <summary>
        /// 验证用户登录信息
        /// 邮箱登录
        /// </summary>
        public int ValidateLogin4Email(string email, byte[] encPassword)
        {
            int rowsAffected;
            SqlParameter[] parameters = 	  {
                new SqlParameter("@Email", SqlDbType.NVarChar, 50),
                new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20)};

            parameters[0].Value = email;
            parameters[1].Value = encPassword;

            return DbHelperSQL.RunProcedure("sp_Accounts_ValidateLogin", parameters, out rowsAffected);
        }

        /// <summary>
        /// 测试用户密码
        /// </summary>
        public int TestPassword(int userID, byte[] encPassword)
        {
            int rowsAffected;
            SqlParameter[] parameters = 
            {
                new SqlParameter("@UserID", SqlDbType.Int, 4),
                new SqlParameter("@EncryptedPassword", SqlDbType.Binary, 20)
            };

            parameters[0].Value = userID;
            parameters[1].Value = encPassword;

            return DbHelperSQL.RunProcedure("sp_Accounts_TestPassword", parameters, out rowsAffected);
        }


        #endregion

        #region 查询用户信息

        /// <summary>
        /// 根据关键字查询用户
        /// </summary>
        public DataSet GetUserList(string key)
        {
            SqlParameter[]
                parameters = {
                                 new SqlParameter("@key", SqlDbType.NVarChar, 50)
                             };

            parameters[0].Value = key;
            return DbHelperSQL.RunProcedure("sp_Accounts_GetUsers", parameters, "Users");
        }
        /// <summary>
        /// 根据用户类型和关键字查询用户信息
        /// </summary>
        public DataSet GetUsersByType(string UserType, string Key)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users ");
            strSql.Append("where ");
            if (UserType.Length > 0)
            {
                strSql.Append(" UserType = @UserType ");
            }
            if (Key.Length > 0)
            {
                if (UserType.Length > 0)
                {
                    strSql.Append(" and ");
                }
                strSql.Append(" (UserName like '%'+@Key+'%' or TrueName like '%'+@Key+'%')  ");
            }
            strSql.Append(" order by UserName ");
            SqlParameter[]
                parameters = {
                    new SqlParameter("@UserType", SqlDbType.NVarChar, 50),                    
                    new SqlParameter("@Key", SqlDbType.NVarChar, 50)
                             };
            parameters[0].Value = UserType;
            parameters[1].Value = Key;
            return DbHelperSQL.Query(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 根据部门和关键字查询用户信息
        /// </summary>
        public DataSet GetUsersByDepart(string DepartmentID, string Key)
        {
            SqlParameter[]
                parameters = {
                    new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 30),
                    new SqlParameter("@key", SqlDbType.NVarChar, 50)
                             };

            parameters[0].Value = DepartmentID;
            parameters[1].Value = Key;
            return DbHelperSQL.RunProcedure("sp_Accounts_GetUsersByDepart", parameters, "Users");
        }

        /// <summary>
        /// 根据用户类型，部门，关键字查询用户
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="DepartmentID"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public DataSet GetUserList(string UserType, string DepartmentID, string Key)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users ");
            strSql.Append("where (1=1)");
            strSql.Append(" and UserType in ( @UserType )");
            strSql.Append(" and DepartmentID= @DepartmentID ");
            strSql.Append(" and (UserName like '%'+@Key+'%' or TrueName like '%'+@Key+'%')  ");
            strSql.Append(" order by UserName ");
            SqlParameter[]
                parameters = {
                    new SqlParameter("@UserType", SqlDbType.NVarChar, 50),
                    new SqlParameter("@DepartmentID", SqlDbType.NVarChar, 30),
                    new SqlParameter("@Key", SqlDbType.NVarChar, 50)
                             };
            parameters[0].Value = UserType;
            parameters[1].Value = DepartmentID;
            parameters[2].Value = Key;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据员工编号，获取员工编号的用户信息
        /// </summary>        
        /// <param name="EmployeeID">员工编号</param>        
        /// <returns></returns>
        public DataSet GetUsersByEmp(int EmployeeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users ");
            strSql.Append("where EmployeeID= @EmployeeID ");
            SqlParameter[]
                parameters = {					
                    new SqlParameter("@EmployeeID", SqlDbType.Int,4)                    
                             };
            parameters[0].Value = EmployeeID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion

        #region 获取某角色下的所有用户
        /// <summary>
        /// 获取某角色下的所有用户
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public DataSet GetUsersByRole(int RoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Users where UserID in ");
            strSql.Append("(select UserID from Accounts_UserRoles ");
            strSql.Append(" where RoleID= @RoleID) ");
            //strSql.Append(" ORDER BY Description ASC ");
            SqlParameter[] parameters = {
                    new SqlParameter("@RoleID", SqlDbType.Int,4)                    
                };
            parameters[0].Value = RoleID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        #endregion

        #region 得到用户的角色信息
        /// <summary>
        /// 获取用户的角色信息
        /// </summary>
        [Obsolete]
        public ArrayList GetUserRoles(int userID)
        {
            ArrayList roles = new ArrayList();
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;

            SqlDataReader tmpReader = DbHelperSQL.RunProcedure("sp_Accounts_GetUserRoles", parameters);
            while (tmpReader.Read())
            {
                roles.Add(tmpReader.GetString(1));
            }
            tmpReader.Close();
            return roles;
        }
        /// <summary>
        /// 获取用户的角色信息
        /// </summary>
        public Dictionary<int, string> GetUserRoles4KeyValues(int userID)
        {
            Dictionary<int, string> roles = new Dictionary<int, string>();
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;

            SqlDataReader tmpReader = DbHelperSQL.RunProcedure("sp_Accounts_GetUserRoles", parameters);
            while (tmpReader.Read())
            {
                roles.Add(tmpReader.GetInt32(0), tmpReader.GetString(1));
            }
            tmpReader.Close();
            return roles;
        }
        #endregion

        #region 得到用户权限信息

        /// <summary>
        /// 获取用户有效的权限列表数据集
        /// </summary>
        public DataSet GetEffectivePermissionLists(int userID)
        {
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;
            DataSet ds = DbHelperSQL.RunProcedure("sp_Accounts_GetEffectivePermissionList", parameters, "PermissionList");
            return ds;
        }
        /// <summary>
        /// 获取用户有效的权限名称列表
        /// </summary>
        public ArrayList GetEffectivePermissionList(int userID)
        {
            ArrayList permissions = new ArrayList();
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;

            SqlDataReader tmpReader = DbHelperSQL.RunProcedure("sp_Accounts_GetEffectivePermissionList", parameters);
            while (tmpReader.Read())
            {
                permissions.Add(tmpReader.GetString(1));
            }
            tmpReader.Close();
            return permissions;
        }
        /// <summary>
        /// 获取用户有效的权限ID列表
        /// </summary>
        public ArrayList GetEffectivePermissionListID(int userID)
        {
            ArrayList permissionsid = new ArrayList();
            SqlParameter[] parameters = { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameters[0].Value = userID;

            SqlDataReader tmpReader = DbHelperSQL.RunProcedure("sp_Accounts_GetEffectivePermissionListID", parameters);
            while (tmpReader.Read())
            {
                permissionsid.Add(tmpReader.GetInt32(0));
            }
            tmpReader.Close();
            return permissionsid;
        }
        #endregion

        #region 增加/移除 所属角色
        /// <summary>
        /// 为用户增加角色
        /// </summary>
        public bool AddRole(int userId, int roleId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                                            new SqlParameter("@UserID", SqlDbType.Int, 4),
                                            new SqlParameter("@RoleID", SqlDbType.Int, 4)
                                        };
            parameters[0].Value = userId;
            parameters[1].Value = roleId;

            DbHelperSQL.RunProcedure("sp_Accounts_AddUserToRole", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// 从用户移除角色
        /// </summary>
        public bool RemoveRole(int userId, int roleId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                                            new SqlParameter("@UserID", SqlDbType.Int, 4),
                                            new SqlParameter("@RoleID", SqlDbType.Int,4 )
                                        };
            parameters[0].Value = userId;
            parameters[1].Value = roleId;

            DbHelperSQL.RunProcedure("sp_Accounts_RemoveUserFromRole", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }
        #endregion

        #region  普通管理员（有权）可以为别的用户分配的角色

        /// <summary>
        /// 要分配是否存在该记录
        /// </summary>
        public bool AssignRoleExists(int UserID, int RoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_UserAssignmentRoles");
            strSql.Append(" where UserID= @UserID and RoleID=@RoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4)
                };
            parameters[0].Value = UserID;
            parameters[1].Value = RoleID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条关联数据
        /// </summary>
        public void AddAssignRole(int UserID, int RoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Accounts_UserAssignmentRoles(");
            strSql.Append("UserID,RoleID)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@RoleID)");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;
            parameters[1].Value = RoleID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条关联数据
        /// </summary>
        public void DeleteAssignRole(int UserID, int RoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Accounts_UserAssignmentRoles ");
            strSql.Append(" where UserID= @UserID and RoleID=@RoleID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4)
                };
            parameters[0].Value = UserID;
            parameters[1].Value = RoleID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取用户分配的角色列表
        /// </summary>
        public DataSet GetAssignRolesByUser(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Roles where RoleID in ");
            strSql.Append("(select RoleID from Accounts_UserAssignmentRoles ");
            strSql.Append(" where UserID= @UserID) ");
            strSql.Append(" ORDER BY Description ASC ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4)                    
                };
            parameters[0].Value = UserID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取用户的未分配的角色列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataSet GetNoAssignRolesByUser(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Accounts_Roles where RoleID not in ");
            strSql.Append("(select RoleID from Accounts_UserAssignmentRoles ");
            strSql.Append(" where UserID= @UserID) ");
            strSql.Append(" ORDER BY Description ASC ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4)                    
                };
            parameters[0].Value = UserID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion
    }
}
