using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Web;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// �û�����İ�ȫ��������Ϣ
    /// </summary>
    public class AccountsPrincipal : System.Security.Principal.IPrincipal
    {
        private IData.IUser dataUser = PubConstant.IsSQLServer ? (IUser)new Data.User() : new MySqlData.User();

        #region ����
        protected System.Security.Principal.IIdentity identity;
        protected DataSet permissionLists;
        protected List<string> permissionsDesc = new List<string>();
        protected List<int> permissionListid = new List<int>();
        protected Dictionary<int, string> rolesKeyValue;

        /// <summary>
        /// ��ǰ�û������н�ɫ
        /// </summary>
        public ArrayList Roles
        {
            get
            {
                return rolesKeyValue == null ?
                    null : new ArrayList(rolesKeyValue.Values);
            }
        }
        /// <summary>
        /// ��ǰ�û�ӵ�е�Ȩ���б����ݼ�
        /// </summary>
        public DataSet PermissionLists
        {
            get
            {
                return permissionLists;
            }
        }

        /// <summary>
        /// ��ǰ�û�ӵ�е�Ȩ�������б�
        /// </summary>
        public List<string> PermissionsDesc
        {
            get
            {
                return permissionsDesc;
            }
        }
        /// <summary>
        /// ��ǰ�û�ӵ�е�Ȩ��ID�б�
        /// </summary>
        public List<int> PermissionsID
        {
            get
            {
                return permissionListid;
            }
        }


        // IPrincipal Interface Requirements:
        /// <summary>
        /// ��ǰ�û��ı�ʶ����
        /// </summary>
        public System.Security.Principal.IIdentity Identity
        {
            get
            {
                return identity;
            }
            set
            {
                identity = value;
            }
        }
        #endregion

        /// <summary>
        /// �����û���Ź���
        /// </summary>
        public AccountsPrincipal(int userID)
        {
            identity = new SiteIdentity(userID);
            permissionLists = dataUser.GetEffectivePermissionLists(userID);
            if (permissionLists.Tables.Count > 0)
            {
                foreach (DataRow dr in permissionLists.Tables[0].Rows)
                {
                    permissionListid.Add(Convert.ToInt32(dr["PermissionID"]));
                    permissionsDesc.Add(dr["Description"].ToString());

                    //�����û� ���ر�Ȩ��

                }
            }
            //permissionList = dataUser.GetEffectivePermissionList(userID);
            //permissionListid=dataUser.GetEffectivePermissionListID(userID);
            rolesKeyValue = dataUser.GetUserRoles4KeyValues(userID);
        }
        /// <summary>
        /// �����û�������
        /// </summary>
        public AccountsPrincipal(string userName)
        {
            SiteIdentity _identity;
            identity = _identity = new SiteIdentity(userName);

            permissionLists = dataUser.GetEffectivePermissionLists(_identity.UserID);
            if (permissionLists.Tables.Count > 0)
            {
                foreach (DataRow dr in permissionLists.Tables[0].Rows)
                {
                    permissionListid.Add(Convert.ToInt32(dr["PermissionID"]));
                    permissionsDesc.Add(dr["Description"].ToString());
                }
            }
            //permissionList = dataUser.GetEffectivePermissionList( ((SiteIdentity)identity).UserID );
            //permissionListid=dataUser.GetEffectivePermissionListID(((SiteIdentity)identity).UserID);
            rolesKeyValue = dataUser.GetUserRoles4KeyValues(_identity.UserID);
        }


        /// <summary>
        /// ��ǰ�û��Ƿ�����ָ�����ƵĽ�ɫ
        /// </summary>
        public bool IsInRole(string role)
        {
            return rolesKeyValue != null && rolesKeyValue.ContainsValue(role);
        }
        /// <summary>
        /// ��ǰ�û��Ƿ�ӵ��ָ���Ľ�ɫ
        /// </summary>
        /// <param name="roleId">��ɫID</param>
        public bool HasRole(int roleId)
        {
            return rolesKeyValue != null && rolesKeyValue.ContainsKey(roleId);
        }
        /// <summary>
        /// ��ǰ�û��Ƿ�ӵ��ָ�����Ƶ�Ȩ��
        /// </summary>
        public bool HasPermission(string permission)
        {
            return permissionsDesc.Contains(permission);
        }
        /// <summary>
        /// ��ǰ�û��Ƿ�ӵ��ָ����Ȩ��
        /// </summary>
        public bool HasPermissionID(int permissionid)
        {
            return permissionListid.Contains(permissionid);
        }
        /// <summary>
        /// ��֤��¼��Ϣ
        /// �û�����¼
        /// </summary>
        public static AccountsPrincipal ValidateLogin(string userName, string password)
        {
            int newID;
            byte[] cryptPassword = EncryptPassword(password);
            IData.IUser dataUser = PubConstant.IsSQLServer ? (IUser)new Data.User() : new MySqlData.User();
            if ((newID = dataUser.ValidateLogin(userName, cryptPassword)) > 0)
                return new AccountsPrincipal(newID);
            else
                return null;
        }
        /// <summary>
        /// ��֤��¼��Ϣ
        /// �����¼
        /// </summary>
        public static AccountsPrincipal ValidateLogin4Email(string email, string password)
        {
            int newID;
            byte[] cryptPassword = EncryptPassword(password);
            IData.IUser dataUser = PubConstant.IsSQLServer ? (IUser)new Data.User() : new MySqlData.User();
            if ((newID = dataUser.ValidateLogin4Email(email, cryptPassword)) > 0)
                return new AccountsPrincipal(newID);
            else
                return null;
        }
        /// <summary>
        /// �������
        /// </summary>
        public static byte[] EncryptPassword(string password)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes = encoding.GetBytes(password);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] cryptPassword = sha1.ComputeHash(hashBytes);
            return cryptPassword;
        }
    }
}
