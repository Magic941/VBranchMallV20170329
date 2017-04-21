using System;
using System.Data;
using System.Configuration;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// �û�
    /// </summary>
    [Serializable]
    public class User
    {
        private IData.IUser dataUser = PubConstant.IsSQLServer ? (IUser)new Data.User() : new MySqlData.User();

        #region ����
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
        /// �û����
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
        /// �û���
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
        /// ����
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
        /// �ǳ�
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
        /// ��ʵ����
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
        /// �Ա�
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
        /// ��ϵ�绰
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
        /// ����
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
        /// ��Ա�������
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
        /// �û����ڵ�λ����
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
        /// �û�״̬
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
        /// �û�����
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
        /// ���
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
        /// ������
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

        #region �����û���Ϣ
        public User()
        {
        }

        /// <summary>
        /// �����û�����
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
        /// �����û�ID����
        /// </summary> 
        public User(int existingUserID)
        {
            userID = existingUserID;
            DataRow userRow = dataUser.Retrieve(userID);
            LoadFromDR(userRow);
        }
        /// <summary>
        /// �����û�������
        /// </summary>        
        public User(string UserName)
        {
            DataRow userRow = dataUser.Retrieve(UserName);
            LoadFromDR(userRow);
        }
        /// <summary>
        /// ����AccountsPrincipal����
        /// </summary>        
        public User(AccountsPrincipal existingPrincipal)
        {
            userID = ((SiteIdentity)existingPrincipal.Identity).UserID;
            DataRow userRow = dataUser.Retrieve(userID);
            LoadFromDR(userRow);
        }
        #endregion

        #region  �ӻ����л�ȡ�û�����ʵ����
        /// <summary>
        /// �ӻ����л�ȡ�û�����ʵ����
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

        #region  �ӻ����л�ȡ�û���
        /// <summary>
        /// �ӻ����л�ȡ�û���
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

        #region �Ƿ����
        /// <summary>
        /// �û����Ƿ��Ѿ�����
        /// </summary>
        [Obsolete]
        public bool HasUser(string userName)
        {
            return dataUser.HasUser(userName);
        }
        /// <summary>
        /// �û����Ƿ��Ѿ�����
        /// </summary>
        public bool HasUserByUserName(string userName)
        {
            return dataUser.HasUserByUserName(userName);
        }
        /// <summary>
        /// �����Ƿ��Ѿ�����
        /// </summary>
        public bool HasUserByEmail(string email)
        {
            return dataUser.HasUserByEmail(email);
        }
        /// <summary>
        /// �ǳ��Ƿ��Ѿ�����
        /// </summary>
        public bool HasUserByNickName(string nickName)
        {
            return dataUser.HasUserByNickName(nickName);
        }
        /// <summary>
        /// �ֻ��Ƿ��Ѿ�����,���ֻ��У�����Ҫ�����֤
        /// </summary>
        public bool HasUserByPhone(string phone)
        {
            return dataUser.HasUserByPhone(phone);
        }
        /// <summary>
        /// �ֻ��Ƿ��Ѿ�����
        /// </summary>
        public bool HasUserByPhone(string phone, string userType)
        {
            return dataUser.HasUserByPhone(phone, userType);
        }
        #endregion

        #region �����û�
        /// <summary>
        /// �����û�
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
        /// �����û�
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

        #region �޸��û�
        /// <summary>
        /// �����û���Ϣ
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
        /// �����û�����
        /// </summary>
        public bool SetPassword(string UserName, string password)
        {
            byte[] cryptPassword = AccountsPrincipal.EncryptPassword(password);
            return dataUser.SetPassword(UserName, cryptPassword);
        }
        /// <summary>
        /// ���ò��ź�Ա�����
        /// </summary>
        public bool UpdateEmployee(int UserID, int employeeID, string departmentID)
        {
            return dataUser.Update(userID, employeeID, departmentID);
        }
        /// <summary>
        /// �������
        /// </summary>
        public bool UpdateApprover(int UserID, int User_iApprover, int User_iApproveState)
        {
            return dataUser.Update(UserID, User_iApprover, User_iApproveState);
        }
        /// <summary>
        /// �����û�״̬
        /// </summary>
        public bool UpdateActivity(int userId, bool activity)
        {
            return dataUser.UpdateActivity(userId, activity);
        }

        #endregion

        #region ɾ���û�
        /// <summary>
        /// ɾ���û�
        /// </summary>
        public bool Delete()
        {
            return dataUser.Delete(userID);
        }
        #endregion

        #region ��ѯ�û�
        public User GetUserByName(string UserName)
        {
          var x =  dataUser.Retrieve(UserName);
          LoadFromDR(x);
          return this;
        }

        /// <summary>
        /// ����NickName��ȡ�û�����
        /// </summary>
        public User GetUserByNickName(string NickName)
        {
            DataRow userRow = dataUser.RetrieveByNickName(NickName);
            LoadFromDR(userRow);
            return this;
        }
        /// <summary>
        /// ���ݹؼ��ֲ�ѯ�û�
        /// </summary>
        public DataSet GetUserList(string key)
        {
            return dataUser.GetUserList(key);
        }
        /// <summary>
        /// ���ݲ��ź͹ؼ��ֲ�ѯ�û���Ϣ
        /// </summary>
        public DataSet GetUsersByDepart(string DepartmentID, string Key)
        {
            return dataUser.GetUsersByDepart(DepartmentID, Key);
        }
        /// <summary>
        /// �����û����ͺ͹ؼ��ֲ�ѯ�û���Ϣ
        /// </summary>
        public DataSet GetUsersByType(string usertype, string key)
        {
            return dataUser.GetUsersByType(usertype, key);
        }
        /// <summary>
        /// �����û����ͣ����ţ��ؼ��ֲ�ѯ�û�
        /// </summary>
        public DataSet GetUserList(string UserType, string DepartmentID, string Key)
        {
            return dataUser.GetUserList(UserType, DepartmentID, Key);
        }
        /// <summary>
        /// ��ȡĳ��ɫ�µ������û�
        /// </summary>
        public DataSet GetUsersByRole(int RoleID)
        {
            return dataUser.GetUsersByRole(RoleID);
        }
        /// <summary>
        /// ����Ա����ţ���ȡԱ����ŵ��û���Ϣ
        /// </summary>        
        /// <param name="EmployeeID">Ա�����</param>        
        /// <returns></returns>
        public DataSet GetUsersByEmp(int EmployeeID)
        {
            return dataUser.GetUsersByEmp(EmployeeID);
        }

        #endregion

        #region ����/�Ƴ� ������ɫ
        /// <summary>
        /// Ϊ�û����ӽ�ɫ
        /// </summary>
        public bool AddToRole(int roleId)
        {
            return dataUser.AddRole(userID, roleId);
        }
        /// <summary>
        /// ���û��Ƴ���ɫ
        /// </summary>
        public bool RemoveRole(int roleId)
        {
            return dataUser.RemoveRole(userID, roleId);
        }
        /// <summary>
        /// ���û��Ƴ���ɫ
        /// </summary>
        public bool RemoveRole(int userID, int roleId)
        {
            return dataUser.RemoveRole(userID, roleId);
        }

        #endregion


        #region  ����ԱΪ�û����䣨����Ȩ�����Է���Ľ�ɫ

        /// <summary>
        /// Ҫ�����Ƿ���ڸü�¼
        /// </summary>
        public bool AssignRoleExists(int UserID, int RoleID)
        {
            return dataUser.AssignRoleExists(UserID, RoleID);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void AddAssignRole(int UserID, int RoleID)
        {
            if (!AssignRoleExists(UserID, RoleID))
            {
                dataUser.AddAssignRole(UserID, RoleID);
            }
        }
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void DeleteAssignRole(int UserID, int RoleID)
        {
            dataUser.DeleteAssignRole(UserID, RoleID);
        }
        /// <summary>
        /// ��ȡ�û��ķ���Ľ�ɫ�б�
        /// </summary>
        public DataSet GetAssignRolesByUser(int UserID)
        {
            return dataUser.GetAssignRolesByUser(UserID);
        }

        /// <summary>
        /// ��ȡ�û���δ����Ľ�ɫ�б�
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
