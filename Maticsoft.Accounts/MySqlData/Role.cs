using System.Data;
using System.Text;
using Maticsoft.Accounts.IData;
using MySql.Data.MySqlClient;

namespace Maticsoft.Accounts.MySqlData
{
    /// <summary>
    /// 角色管理
    /// </summary>    
    public class Role : IRole
    {
        //public Role(string newConnectionString) 
        //{ }

        public Role()
        { }

        /// <summary>
        /// 是否存在该角色
        /// </summary>
        public bool RoleExists(string Description)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_Roles");
            strSql.Append(" where Description=?Description");
            MySqlParameter[] parameters = {
                    new MySqlParameter("?Description", MySqlDbType.VarChar, 50)
            };
            parameters[0].Value = Description;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加角色
        /// </summary>       
        public int Create(string description)
        {
            int rowsAffected;
            MySqlParameter[] parameters = 
                {
                    new MySqlParameter("?_Description", MySqlDbType.VarChar, 50)
                };
            parameters[0].Value = description;
            //INSERT INTO Accounts_Roles(Description) VALUES(_Description);SELECT LAST_INSERT_ID();
            return DbHelperMySQL.RunProcedure("sp_Accounts_CreateRole", parameters, out rowsAffected);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        public bool Update(int roleId, string description)
        {
            int rowsAffected;
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_RoleID", MySqlDbType.Int32, 4),
                    new MySqlParameter("?_Description", MySqlDbType.VarChar, 50)
                };
            parameters[0].Value = roleId;
            parameters[1].Value = description;
            DbHelperMySQL.RunProcedure("sp_Accounts_UpdateRole", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        public bool Delete(int roleId)
        {
            int rowsAffected;
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_RoleID", MySqlDbType.Int32, 4)
                };
            parameters[0].Value = roleId;
            DbHelperMySQL.RunProcedure("sp_Accounts_DeleteRole", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// 根据角色ID获取角色的信息
        /// </summary>
        public DataRow Retrieve(int roleId)
        {
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_RoleID", MySqlDbType.Int32, 4)
                };

            parameters[0].Value = roleId;
            using (DataSet roles = DbHelperMySQL.RunProcedure("sp_Accounts_GetRoleDetails", parameters, "Roles"))
            {
                return roles.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// 为角色增加权限
        /// </summary>
        public void AddPermission(int roleId, int permissionId)
        {
            int rowsAffected;
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_RoleID", MySqlDbType.Int32, 4),
                    new MySqlParameter("?_PermissionID", MySqlDbType.Int32, 4)
                };
            parameters[0].Value = roleId;
            parameters[1].Value = permissionId;
            DbHelperMySQL.RunProcedure("sp_Accounts_AddPermissionToRole", parameters, out rowsAffected);
        }
        /// <summary>
        /// 从角色移除权限
        /// </summary>
        public void RemovePermission(int roleId, int permissionId)
        {
            int rowsAffected;
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_RoleID", MySqlDbType.Int32, 4),
                    new MySqlParameter("?_PermissionID", MySqlDbType.Int32, 4)
                };
            parameters[0].Value = roleId;
            parameters[1].Value = permissionId;
            DbHelperMySQL.RunProcedure("sp_Accounts_RemovePermissionFromRole", parameters, out rowsAffected);
        }
        /// <summary>
        /// 清空角色的权限
        /// </summary>
        public void ClearPermissions(int roleId)
        {
            int rowsAffected;
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_RoleID", MySqlDbType.Int32, 4),					
            };
            parameters[0].Value = roleId;
            DbHelperMySQL.RunProcedure("sp_Accounts_ClearPermissionsFromRole", parameters, out rowsAffected);
        }

        /// <summary>
        /// 获取所有角色的列表
        /// </summary>
        public DataSet GetRoleList()
        {
            using (DataSet roles = DbHelperMySQL.RunProcedure("sp_Accounts_GetAllRoles", new IDataParameter[] { }, "Roles"))
            {
                return roles;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetRoleList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RoleID,Description ");
            strSql.Append(" FROM Accounts_Roles ");
            if (idlist.Trim() != "")
            {
                strSql.Append(" where RoleID in (" + idlist + ")");
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

    }
}
