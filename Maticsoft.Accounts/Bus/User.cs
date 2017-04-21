using System;
using System.Data;
using System.Configuration;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class User
    {
        private IData.IUser dataUser = PubConstant.IsSQLServer ? (IUser)new Data.User() : new MySqlData.User();

        #region 属性
        private int userID;
        private string userName;
        private string nickName;
        private string trueName;
        private string sex;
        private string phone;
        private string email;
        private int employeeID;
        private string departmentID = "-1";
        private bool activity;
        private string userType;
        private byte[] password;
        private int style;
        private int _user_icreator;
        private DateTime _user_datecreate;
        private DateTime _user_datevalid;
        private DateTime _user_dateexpire;
        private int _user_iapprover;
        private DateTime _user_dateapprove;
        private int _user_iapprovestate;
        private string _user_clang;

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public byte[] Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get
            {
                return nickName;
            }
            set
            {
                nickName = value;
            }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName
        {
            get
            {
                return trueName;
            }
            set
            {
                trueName = value;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                sex = value;
            }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// （员工）编号
        /// </summary>
        public int EmployeeID
        {
            get
            {
                return employeeID;
            }
            set
            {
                employeeID = value;
            }
        }

        /// <summary>
        /// 用户所在单位或部门
        /// </summary>
        public string DepartmentID
        {
            get
            {
                return departmentID;
            }
            set
            {
                departmentID = value;
            }
        }

        /// <summary>
        /// 用户状态
        /// </summary>
        public bool Activity
        {
            get
            {
                return activity;
            }
            set
            {
                activity = value;
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType
        {
            get
            {
                return userType;
            }
            set
            {
                userType = value;
            }
        }

        /// <summary>
        /// 风格
        /// </summary>
        public int Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
            }
        }

        /// <summary>
        /// 创建者
        /// </summary>
        public int User_iCreator
        {
            set { _user_icreator = value; }
            get { return _user_icreator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime User_dateCreate
        {
            set { _user_datecreate = value; }
            get { return _user_datecreate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime User_dateValid
        {
            set { _user_datevalid = value; }
            get { return _user_datevalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime User_dateExpire
        {
            set { _user_dateexpire = value; }
            get { return _user_dateexpire; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int User_iApprover
        {
            set { _user_iapprover = value; }
            get { return _user_iapprover; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime User_dateApprove
        {
            set { _user_dateapprove = value; }
            get { return _user_dateapprove; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int User_iApproveState
        {
            set { _user_iapprovestate = value; }
            get { return _user_iapprovestate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_cLang
        {
            set { _user_clang = value; }
            get { return _user_clang; }
        }
        #endregion

        #region 构造用户信息
        public User()
        {
        }

        /// <summary>
        /// 加载用户数据
        /// </summary>
        private void LoadFromDR(DataRow userRow)
        {
            if (userRow != null)
            {
                UserID = (int)userRow["UserID"];
                userName = userRow["UserName"].ToString();
                if ((!Object.Equals(userRow["NickName"], null)) && (!Object.Equals(userRow["NickName"], System.DBNull.Value)))
                {
                    nickName = userRow["NickName"].ToString();
                }
                trueName = userRow["TrueName"].ToString();
                activity = (bool)userRow["Activity"];
                userType = userRow["UserType"].ToString();
                password = (byte[])userRow["Password"];

                if ((!Object.Equals(userRow["Sex"], null)) && (!Object.Equals(userRow["Sex"], System.DBNull.Value)))
                {
                    sex = userRow["Sex"].ToString();
                }
                if ((!Object.Equals(userRow["Phone"], null)) && (!Object.Equals(userRow["Phone"], System.DBNull.Value)))
                {
                    phone = userRow["Phone"].ToString();
                }
                if ((!Object.Equals(userRow["Email"], null)) && (!Object.Equals(userRow["Email"], System.DBNull.Value)))
                {
                    email = userRow["Email"].ToString();
                }
                if ((!Object.Equals(userRow["EmployeeID"], null)) && (!Object.Equals(userRow["EmployeeID"], System.DBNull.Value)))
                {
                    employeeID = Convert.ToInt32(userRow["EmployeeID"]);
                }
                if ((!Object.Equals(userRow["DepartmentID"], null)) && (!Object.Equals(userRow["DepartmentID"], System.DBNull.Value)))
                {
                    departmentID = userRow["DepartmentID"].ToString();
                }
                if ((!Object.Equals(userRow["Style"], null)) && (!Object.Equals(userRow["Style"], System.DBNull.Value)))
                {
                    style = Convert.ToInt32(userRow["Style"]);
                }
                if ((!Object.Equals(userRow["User_iCreator"], null)) && (!Object.Equals(userRow["User_iCreator"], System.DBNull.Value)))
                {
                    _user_icreator = Convert.ToInt32(userRow["User_iCreator"]);
                }
                if ((!Object.Equals(userRow["User_dateCreate"], null)) && (!Object.Equals(userRow["User_dateCreate"], System.DBNull.Value)))
                {
                    _user_datecreate = Convert.ToDateTime(userRow["User_dateCreate"]);
                }
                if ((!Object.Equals(userRow["User_dateValid"], null)) && (!Object.Equals(userRow["User_dateValid"], System.DBNull.Value)))
                {
                    _user_datevalid = Convert.ToDateTime(userRow["User_dateValid"]);
                }
                if ((!Object.Equals(userRow["User_dateExpire"], null)) && (!Object.Equals(userRow["User_dateExpire"], System.DBNull.Value)))
                {
                    _user_dateexpire = Convert.ToDateTime(userRow["User_dateExpire"]);
                }
                if ((!Object.Equals(userRow["User_iApprover"], null)) && (!Object.Equals(userRow["User_iApprover"], System.DBNull.Value)))
                {
                    _user_iapprover = Convert.ToInt32(userRow["User_iApprover"]);
                }
                if ((!Object.Equals(userRow["User_dateApprove"], null)) && (!Object.Equals(userRow["User_dateApprove"], System.DBNull.Value)))
                {
                    _user_dateapprove = Convert.ToDateTime(userRow["User_dateApprove"]);
                }
                if ((!Object.Equals(userRow["User_iApproveState"], null)) && (!Object.Equals(userRow["User_iApproveState"], System.DBNull.Value)))
                {
                    _user_iapprovestate = Convert.ToInt32(userRow["User_iApproveState"]);
                }
                _user_clang = userRow["User_cLang"].ToString();
            }
        }

        /// <summary>
        /// 根据用户ID构造
        /// </summary> 
        public User(int existingUserID)
        {
            userID = existingUserID;
            DataRow userRow = dataUser.Retrieve(userID);
            LoadFromDR(userRow);
        }
        /// <summary>
        /// 根据用户名构造
        /// </summary>        
        public User(string UserName)
        {
            DataRow userRow = dataUser.Retrieve(UserName);
            LoadFromDR(userRow);
        }
        /// <summary>
        /// 根据AccountsPrincipal构造
        /// </summary>        
        public User(AccountsPrincipal existingPrincipal)
        {
            userID = ((SiteIdentity)existingPrincipal.Identity).UserID;
            DataRow userRow = dataUser.Retrieve(userID);
            LoadFromDR(userRow);
        }
        #endregion

        #region  从缓存中获取用户的真实姓名
        /// <summary>
        /// 从缓存中获取用户的真实姓名
        /// </summary>
        public string GetTrueNameByCache(int userID)
        {
            string cacheKey = "TrueName-" + userID;
            object objModel = Maticsoft.Accounts.DataCache.GetCache(cacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = new User(userID).TrueName;
                    if (objModel == null) return "";
                    int cacheTime = Maticsoft.Accounts.ConfigHelper.GetConfigInt("CacheTime");
                    Maticsoft.Accounts.DataCache.SetCache(cacheKey, objModel,
                        DateTime.Now.AddMinutes(cacheTime > 0 ? cacheTime : ConfigHelper.DEFAULT_CACHETIME
                        ), TimeSpan.Zero);
                }
                catch
                {
                    return "";
                }
            }
            return objModel.ToString();
        }
        #endregion

        #region  从缓存中获取用户名
        /// <summary>
        /// 从缓存中获取用户名
        /// </summary>
        public string GetUserNameByCache(int userID)
        {
            string cacheKey = "UserName-" + userID;
            object objModel = Maticsoft.Accounts.DataCache.GetCache(cacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = new User(userID).UserName;
                    if (objModel == null) return "";
                    int cacheTime = Maticsoft.Accounts.ConfigHelper.GetConfigInt("CacheTime");
                    Maticsoft.Accounts.DataCache.SetCache(cacheKey, objModel,
                        DateTime.Now.AddMinutes(cacheTime > 0 ? cacheTime : ConfigHelper.DEFAULT_CACHETIME
                        ), TimeSpan.Zero);
                }
                catch
                {
                    return "";
                }
            }
            return objModel.ToString();
        }
        #endregion

        #region 是否存在
        /// <summary>
        /// 用户名是否已经存在
        /// </summary>
        [Obsolete]
        public bool HasUser(string userName)
        {
            return dataUser.HasUser(userName);
        }
        /// <summary>
        /// 用户名是否已经存在
        /// </summary>
        public bool HasUserByUserName(string userName)
        {
            return dataUser.HasUserByUserName(userName);
        }
        /// <summary>
        /// 邮箱是否已经存在
        /// </summary>
        public bool HasUserByEmail(string email)
        {
            return dataUser.HasUserByEmail(email);
        }
        /// <summary>
        /// 昵称是否已经存在
        /// </summary>
        public bool HasUserByNickName(string nickName)
        {
            return dataUser.HasUserByNickName(nickName);
        }
        /// <summary>
        /// 手机是否已经存在,查手机列，并且要结合验证
        /// </summary>
        public bool HasUserByPhone(string phone)
        {
            return dataUser.HasUserByPhone(phone);
        }
        /// <summary>
        /// 手机是否已经存在
        /// </summary>
        public bool HasUserByPhone(string phone, string userType)
        {
            return dataUser.HasUserByPhone(phone, userType);
        }
        #endregion

        #region 增加用户
        /// <summary>
        /// 创建用户
        /// </summary>
        public int Create()
        {
            userID = dataUser.Create(
                userName,
                password,
                nickName,
                trueName,
                sex,
                phone,
                email,
                employeeID,
                departmentID,
                activity,
                userType,
                style,
                User_iCreator,
                User_dateValid,
                User_cLang
                );

            return userID;
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        public int Create4CreateDate()
        {
            userID = dataUser.Create(
                userName,
                password,
                nickName,
                trueName,
                sex,
                phone,
                email,
                employeeID,
                departmentID,
                activity,
                userType,
                style,
                User_iCreator,
                User_dateCreate,
                User_dateValid,
                User_cLang
                );
            return userID;
        }
        #endregion

        #region 修改用户
        /// <summary>
        /// 更新用户信息
        /// </summary>
        public bool Update()
        {
            return dataUser.Update(
                userID,
                userName,
                password,
                nickName,
                trueName,
                sex,
                phone,
                email,
                employeeID,
                departmentID,
                activity,
                userType,
                style);
        }

        /// <summary>
        /// 设置用户密码
        /// </summary>
        public bool SetPassword(string UserName, string password)
        {
            byte[] cryptPassword = AccountsPrincipal.EncryptPassword(password);
            return dataUser.SetPassword(UserName, cryptPassword);
        }
        /// <summary>
        /// 设置部门和员工编号
        /// </summary>
        public bool UpdateEmployee(int UserID, int employeeID, string departmentID)
        {
            return dataUser.Update(userID, employeeID, departmentID);
        }
        /// <summary>
        /// 设置审核
        /// </summary>
        public bool UpdateApprover(int UserID, int User_iApprover, int User_iApproveState)
        {
            return dataUser.Update(UserID, User_iApprover, User_iApproveState);
        }
        /// <summary>
        /// 设置用户状态
        /// </summary>
        public bool UpdateActivity(int userId, bool activity)
        {
            return dataUser.UpdateActivity(userId, activity);
        }

        #endregion

        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        public bool Delete()
        {
            return dataUser.Delete(userID);
        }
        #endregion

        #region 查询用户
        public User GetUserByName(string UserName)
        {
          var x =  dataUser.Retrieve(UserName);
          LoadFromDR(x);
          return this;
        }

        /// <summary>
        /// 根据NickName获取用户对象
        /// </summary>
        public User GetUserByNickName(string NickName)
        {
            DataRow userRow = dataUser.RetrieveByNickName(NickName);
            LoadFromDR(userRow);
            return this;
        }
        /// <summary>
        /// 根据关键字查询用户
        /// </summary>
        public DataSet GetUserList(string key)
        {
            return dataUser.GetUserList(key);
        }
        /// <summary>
        /// 根据部门和关键字查询用户信息
        /// </summary>
        public DataSet GetUsersByDepart(string DepartmentID, string Key)
        {
            return dataUser.GetUsersByDepart(DepartmentID, Key);
        }
        /// <summary>
        /// 根据用户类型和关键字查询用户信息
        /// </summary>
        public DataSet GetUsersByType(string usertype, string key)
        {
            return dataUser.GetUsersByType(usertype, key);
        }
        /// <summary>
        /// 根据用户类型，部门，关键字查询用户
        /// </summary>
        public DataSet GetUserList(string UserType, string DepartmentID, string Key)
        {
            return dataUser.GetUserList(UserType, DepartmentID, Key);
        }
        /// <summary>
        /// 获取某角色下的所有用户
        /// </summary>
        public DataSet GetUsersByRole(int RoleID)
        {
            return dataUser.GetUsersByRole(RoleID);
        }
        /// <summary>
        /// 根据员工编号，获取员工编号的用户信息
        /// </summary>        
        /// <param name="EmployeeID">员工编号</param>        
        /// <returns></returns>
        public DataSet GetUsersByEmp(int EmployeeID)
        {
            return dataUser.GetUsersByEmp(EmployeeID);
        }

        #endregion

        #region 增加/移除 所属角色
        /// <summary>
        /// 为用户增加角色
        /// </summary>
        public bool AddToRole(int roleId)
        {
            return dataUser.AddRole(userID, roleId);
        }
        /// <summary>
        /// 从用户移除角色
        /// </summary>
        public bool RemoveRole(int roleId)
        {
            return dataUser.RemoveRole(userID, roleId);
        }
        /// <summary>
        /// 从用户移除角色
        /// </summary>
        public bool RemoveRole(int userID, int roleId)
        {
            return dataUser.RemoveRole(userID, roleId);
        }

        #endregion


        #region  管理员为用户分配（他有权）可以分配的角色

        /// <summary>
        /// 要分配是否存在该记录
        /// </summary>
        public bool AssignRoleExists(int UserID, int RoleID)
        {
            return dataUser.AssignRoleExists(UserID, RoleID);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddAssignRole(int UserID, int RoleID)
        {
            if (!AssignRoleExists(UserID, RoleID))
            {
                dataUser.AddAssignRole(UserID, RoleID);
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteAssignRole(int UserID, int RoleID)
        {
            dataUser.DeleteAssignRole(UserID, RoleID);
        }
        /// <summary>
        /// 获取用户的分配的角色列表
        /// </summary>
        public DataSet GetAssignRolesByUser(int UserID)
        {
            return dataUser.GetAssignRolesByUser(UserID);
        }

        /// <summary>
        /// 获取用户的未分配的角色列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataSet GetNoAssignRolesByUser(int UserID)
        {
            return dataUser.GetNoAssignRolesByUser(UserID);
        }
        #endregion


    }
}
