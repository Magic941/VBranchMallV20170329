using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Data
{
    /// <summary>
    /// ��ɫ����
    /// </summary>    
    public class Role : IRole
    {
        //public Role(string newConnectionString) 
        //{ }

        public Role()
        { }

        /// <summary>
        /// �Ƿ���ڸý�ɫ
        /// </summary>
        public bool RoleExists(string Description)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_Roles");
            strSql.Append(" where Description=@Description");
            SqlParameter[] parameters = {
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50)
            };
            parameters[0].Value = Description;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ���ӽ�ɫ
        /// </summary>       
        public int Create(string description)
        {
            int rowsAffected;
            SqlParameter[] parameters = 
                {
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50)
                };
            parameters[0].Value = description;
            return DbHelperSQL.RunProcedure("sp_Accounts_CreateRole", parameters, out rowsAffected);
        }

        /// <summary>
        /// ���½�ɫ��Ϣ
        /// </summary>
        public bool Update(int roleId, string description)
        {
            int rowsAffected;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleID", SqlDbType.Int, 4),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50)
                };
            parameters[0].Value = roleId;
            parameters[1].Value = description;
            DbHelperSQL.RunProcedure("sp_Accounts_UpdateRole", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }
        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        public bool Delete(int roleId)
        {
            int rowsAffected;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleID", SqlDbType.Int, 4)
                };
            parameters[0].Value = roleId;
            DbHelperSQL.RunProcedure("sp_Accounts_DeleteRole", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// ���ݽ�ɫID��ȡ��ɫ����Ϣ
        /// </summary>
        public DataRow Retrieve(int roleId)
        {
            SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleID", SqlDbType.Int, 4)
                };

            parameters[0].Value = roleId;
            using (DataSet roles = DbHelperSQL.RunProcedure("sp_Accounts_GetRoleDetails", parameters, "Roles"))
            {
                return roles.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// Ϊ��ɫ����Ȩ��
        /// </summary>
        public void AddPermission(int roleId, int permissionId)
        {
            int rowsAffected;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleID", SqlDbType.Int, 4),
                    new SqlParameter("@PermissionID", SqlDbType.Int, 4)
                };
            parameters[0].Value = roleId;
            parameters[1].Value = permissionId;
            DbHelperSQL.RunProcedure("sp_Accounts_AddPermissionToRole", parameters, out rowsAffected);
        }
        /// <summary>
        /// �ӽ�ɫ�Ƴ�Ȩ��
        /// </summary>
        public void RemovePermission(int roleId, int permissionId)
        {
            int rowsAffected;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleID", SqlDbType.Int, 4),
                    new SqlParameter("@PermissionID", SqlDbType.Int, 4)
                };
            parameters[0].Value = roleId;
            parameters[1].Value = permissionId;
            DbHelperSQL.RunProcedure("sp_Accounts_RemovePermissionFromRole", parameters, out rowsAffected);
        }
        /// <summary>
        /// ��ս�ɫ��Ȩ��
        /// </summary>
        public void ClearPermissions(int roleId)
        {
            int rowsAffected;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@RoleID", SqlDbType.Int, 4),					
            };
            parameters[0].Value = roleId;
            DbHelperSQL.RunProcedure("sp_Accounts_ClearPermissionsFromRole", parameters, out rowsAffected);
        }

        /// <summary>
        /// ��ȡ���н�ɫ���б�
        /// </summary>
        public DataSet GetRoleList()
        {
            using (DataSet roles = DbHelperSQL.RunProcedure("sp_Accounts_GetAllRoles", new IDataParameter[] { }, "Roles"))
            {
                return roles;
            }
        }

        /// <summary>
        /// ��������б�
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
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}
