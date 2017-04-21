using System.Data;
using System.Data.SqlClient;
using System.Text;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Data
{
    /// <summary>
    /// Ȩ�޹���
    /// </summary>
    public class Permission : IPermission
    {

        #region
        /// <summary>
        /// ����һ��Ȩ��
        /// </summary>
        public int Create(int categoryID, string description)
        {
            int rowsAffected;
            SqlParameter[] parameters = 
                {
                    new SqlParameter("@CategoryID", SqlDbType.Int, 8),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50)
                };
            parameters[0].Value = categoryID;
            parameters[1].Value = description;

            return DbHelperSQL.RunProcedure("sp_Accounts_CreatePermission", parameters, out rowsAffected);
        }

        /// <summary>
        /// ����Ȩ����Ϣ
        /// </summary>
        public bool Update(int PermissionID, string description)
        {
            int rowsAffected;
            SqlParameter[] parameters = 
                {
                    new SqlParameter("@PermissionID", SqlDbType.Int, 8),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50)
                };
            parameters[0].Value = PermissionID;
            parameters[1].Value = description;

            DbHelperSQL.RunProcedure("sp_Accounts_UpdatePermission", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        public void UpdateCategory(string PermissionIDlist, int CategoryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Permissions set ");
            strSql.AppendFormat(" CategoryID={0}", CategoryID);
            strSql.AppendFormat(" where PermissionID in({0})", PermissionIDlist);

            DbHelperSQL.ExecuteSql(strSql.ToString());

        }


        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>        
        public bool Delete(int id)
        {
            int rowsAffected;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@PermissionID", SqlDbType.Int, 4)
                };
            parameters[0].Value = id;
            DbHelperSQL.RunProcedure("sp_Accounts_DeletePermission", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// ����Ȩ��ID��ȡȨ����Ϣ
        /// </summary>
        public DataSet Retrieve(int permissionId)
        {
            SqlParameter[] parameters = { new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            parameters[0].Value = permissionId;
            return DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionDetails", parameters, "Permissions");

        }

        /// <summary>
        /// ��ȡȨ���б�
        /// </summary>        
        public DataSet GetPermissionList()
        {
            SqlParameter[] parameters = { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            using (DataSet permissions = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionList", parameters, permissions, "Permissions");
                DataRelation permissionCategories = new DataRelation("PermissionCategories",
                    permissions.Tables["Categories"].Columns["CategoryID"],
                    permissions.Tables["Permissions"].Columns["CategoryID"], true);
                permissions.Relations.Add(permissionCategories);
                DataColumn[] categoryKeys = new DataColumn[1];
                categoryKeys[0] = permissions.Tables["Categories"].Columns["CategoryID"];
                DataColumn[] permissionKeys = new DataColumn[1];
                permissionKeys[0] = permissions.Tables["Permissions"].Columns["PermissionID"];
                permissions.Tables["Categories"].PrimaryKey = categoryKeys;
                permissions.Tables["Permissions"].PrimaryKey = permissionKeys;
                return permissions;
            }
        }

        #endregion



        /// <summary>
        /// ��ȡָ����ɫ��Ȩ���б�
        /// </summary>        
        public DataSet GetPermissionList(int roleId)
        {
            SqlParameter[] parameters = { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet permissions = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionList", parameters, permissions, "Permissions");
                DataRelation permissionCategories = new DataRelation("PermissionCategories",
                    permissions.Tables["Categories"].Columns["CategoryID"],
                    permissions.Tables["Permissions"].Columns["CategoryID"], true);
                permissions.Relations.Add(permissionCategories);
                DataColumn[] categoryKeys = new DataColumn[1];
                categoryKeys[0] = permissions.Tables["Categories"].Columns["CategoryID"];
                DataColumn[] permissionKeys = new DataColumn[1];
                permissionKeys[0] = permissions.Tables["Permissions"].Columns["PermissionID"];
                permissions.Tables["Categories"].PrimaryKey = categoryKeys;
                permissions.Tables["Permissions"].PrimaryKey = permissionKeys;
                return permissions;
            }
        }

        /// <summary>
        /// ��ȡָ����ɫû�е�Ȩ���б�
        /// </summary>        
        public DataSet GetNoPermissionList(int roleId)
        {
            SqlParameter[] parameters = { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet permissions = DbHelperSQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DbHelperSQL.RunProcedure("sp_Accounts_GetNoPermissionList", parameters, permissions, "Permissions");
                DataRelation permissionCategories = new DataRelation("PermissionCategories",
                    permissions.Tables["Categories"].Columns["CategoryID"],
                    permissions.Tables["Permissions"].Columns["CategoryID"], true);
                permissions.Relations.Add(permissionCategories);
                DataColumn[] categoryKeys = new DataColumn[1];
                categoryKeys[0] = permissions.Tables["Categories"].Columns["CategoryID"];
                DataColumn[] permissionKeys = new DataColumn[1];
                permissionKeys[0] = permissions.Tables["Permissions"].Columns["PermissionID"];
                permissions.Tables["Categories"].PrimaryKey = categoryKeys;
                permissions.Tables["Permissions"].PrimaryKey = permissionKeys;
                return permissions;
            }
        }




    }
}
