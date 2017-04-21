using System;
using System.Data;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// 权限工具类
    /// </summary>
    public class AccountsTool
    {
        /// <summary>
        /// 获取所有权限类别
        /// </summary>
        public static DataSet GetAllCategories()
        {
            IData.IPermissionCategory dataPermissionCategory = PubConstant.IsSQLServer ? (IPermissionCategory)new Data.PermissionCategory() : new MySqlData.PermissionCategory();
            return dataPermissionCategory.GetCategoryList();
        }
        /// <summary>
        /// 获取某类别的所有权限
        /// </summary>
        public static DataSet GetPermissionsByCategory(int categoryID)
        {
            IData.IPermissionCategory dataPermission = PubConstant.IsSQLServer ? (IPermissionCategory)new Data.PermissionCategory() : new MySqlData.PermissionCategory();
            return dataPermission.GetPermissionsInCategory(categoryID);
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        public static DataSet GetAllPermissions()
        {
            IData.IPermission dataPermission = PubConstant.IsSQLServer ? (IPermission)new Data.Permission() : new MySqlData.Permission();
            return dataPermission.GetPermissionList();
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        public static DataSet GetRoleList()
        {
            IData.IRole dataRole = PubConstant.IsSQLServer ? (IRole)new Data.Role() : new MySqlData.Role();
            return dataRole.GetRoleList();
        }

        /// <summary>
        /// 获取部分角色
        /// </summary>
        public static DataSet GetRoleList(string idlist)
        {
            IData.IRole dataRole = PubConstant.IsSQLServer ? (IRole)new Data.Role() : new MySqlData.Role();
            return dataRole.GetRoleList(idlist);
        }

    }
}
