using System;
using System.Data;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// Ȩ�޹�����
    /// </summary>
    public class AccountsTool
    {
        /// <summary>
        /// ��ȡ����Ȩ�����
        /// </summary>
        public static DataSet GetAllCategories()
        {
            IData.IPermissionCategory dataPermissionCategory = PubConstant.IsSQLServer ? (IPermissionCategory)new Data.PermissionCategory() : new MySqlData.PermissionCategory();
            return dataPermissionCategory.GetCategoryList();
        }
        /// <summary>
        /// ��ȡĳ��������Ȩ��
        /// </summary>
        public static DataSet GetPermissionsByCategory(int categoryID)
        {
            IData.IPermissionCategory dataPermission = PubConstant.IsSQLServer ? (IPermissionCategory)new Data.PermissionCategory() : new MySqlData.PermissionCategory();
            return dataPermission.GetPermissionsInCategory(categoryID);
        }

        /// <summary>
        /// ��ȡ����Ȩ��
        /// </summary>
        public static DataSet GetAllPermissions()
        {
            IData.IPermission dataPermission = PubConstant.IsSQLServer ? (IPermission)new Data.Permission() : new MySqlData.Permission();
            return dataPermission.GetPermissionList();
        }

        /// <summary>
        /// ��ȡ���н�ɫ
        /// </summary>
        public static DataSet GetRoleList()
        {
            IData.IRole dataRole = PubConstant.IsSQLServer ? (IRole)new Data.Role() : new MySqlData.Role();
            return dataRole.GetRoleList();
        }

        /// <summary>
        /// ��ȡ���ֽ�ɫ
        /// </summary>
        public static DataSet GetRoleList(string idlist)
        {
            IData.IRole dataRole = PubConstant.IsSQLServer ? (IRole)new Data.Role() : new MySqlData.Role();
            return dataRole.GetRoleList(idlist);
        }

    }
}
