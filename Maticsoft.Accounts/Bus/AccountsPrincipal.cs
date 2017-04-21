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
    /// 用户对象的安全上下文信息
    /// </summary>
    public class AccountsPrincipal : System.Security.Principal.IPrincipal
    {
        private IData.IUser dataUser = PubConstant.IsSQLServer ? (IUser)new Data.User() : new MySqlData.User();

        #region 属性
        protected System.Security.Principal.IIdentity identity;
        protected DataSet permissionLists;
        protected List<string> permissionsDesc = new List<string>();
        protected List<int> permissionListid = new List<int>();
        protected Dictionary<int, string> rolesKeyValue;

        /// <summary>
        /// 当前用户的所有角色
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
        /// 当前用户拥有的权限列表数据集
        /// </summary>
        public DataSet PermissionLists
        {
            get
            {
                return permissionLists;
            }
        }

        /// <summary>
        /// 当前用户拥有的权限名称列表
        /// </summary>
        public List<string> PermissionsDesc
        {
            get
            {
                return permissionsDesc;
            }
        }
        /// <summary>
        /// 当前用户拥有的权限ID列表
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
        /// 当前用户的标识对象
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
        /// 根据用户编号构造
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

                    //增加用户 的特别权限

                }
            }
            //permissionList = dataUser.GetEffectivePermissionList(userID);
            //permissionListid=dataUser.GetEffectivePermissionListID(userID);
            rolesKeyValue = dataUser.GetUserRoles4KeyValues(userID);
        }
        /// <summary>
        /// 根据用户名构造
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
        /// 当前用户是否属于指定名称的角色
        /// </summary>
        public bool IsInRole(string role)
        {
            return rolesKeyValue != null && rolesKeyValue.ContainsValue(role);
        }
        /// <summary>
        /// 当前用户是否拥有指定的角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        public bool HasRole(int roleId)
        {
            return rolesKeyValue != null && rolesKeyValue.ContainsKey(roleId);
        }
        /// <summary>
        /// 当前用户是否拥有指定名称的权限
        /// </summary>
        public bool HasPermission(string permission)
        {
            return permissionsDesc.Contains(permission);
        }
        /// <summary>
        /// 当前用户是否拥有指定的权限
        /// </summary>
        public bool HasPermissionID(int permissionid)
        {
            return permissionListid.Contains(permissionid);
        }
        /// <summary>
        /// 验证登录信息
        /// 用户名登录
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
        /// 验证登录信息
        /// 邮箱登录
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
        /// 密码加密
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
