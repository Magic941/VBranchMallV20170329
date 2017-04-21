using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// ��ǰ�û��ı�ʶ����
    /// </summary>
    [Serializable]
    public class SiteIdentity : System.Security.Principal.IIdentity
    {
        private IData.IUser dataUser = PubConstant.IsSQLServer ? (IUser)new Data.User() : new MySqlData.User();

        #region  �û�����
        private int userID;
        private string userName;
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

        #region IIdentity interface requirments:

        /// <summary>
        /// ��ǰ�û�������
        /// </summary>
        public string Name
        {
            get
            {
                return userName;
            }
        }

        /// <summary>
        /// ��ȡ��ʹ�õ������֤�����͡�
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return "Custom Authentication";
            }
            set
            {
                // do nothing
            }
        }
        /// <summary>
        /// �Ƿ���֤���û�
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
        #endregion


        /// <summary>
        /// �����û�����
        /// </summary>
        private void LoadFromDR(DataRow userRow)
        {
            if (userRow != null)
            {
                UserID = (int)userRow["UserID"];
                userName = userRow["UserName"].ToString();
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

        #region ���췽��
        /// <summary>
        /// �����û�������
        /// </summary>
        public SiteIdentity(string currentUserName)
        {
            DataRow userRow = dataUser.Retrieve(currentUserName);
            if (userRow != null)
            {
                LoadFromDR(userRow);
            }

        }
        /// <summary>
        /// �����û�ID����
        /// </summary>
        public SiteIdentity(int currentUserID)
        {
            DataRow userRow = dataUser.Retrieve(currentUserID);
            if (userRow != null)
            {
                LoadFromDR(userRow);
            }
        }
        #endregion

        /// <summary>
        /// ��鵱ǰ�û���������
        /// </summary>
        public int TestPassword(string password)
        {
            // At some point, we may have a more complex way of encrypting or storing the passwords
            // so by supplying this procedure, we can simply replace its contents to move password
            // comparison to the database (as we've done below) or somewhere else (e.g. another
            // web service, etc).
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes = encoding.GetBytes(password);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] cryptPassword = sha1.ComputeHash(hashBytes);
            return dataUser.TestPassword(userID, cryptPassword);
        }

    }
}
