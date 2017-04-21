using System.Data;
using System.Text;
using Maticsoft.Accounts.IData;
using MySql.Data.MySqlClient;

namespace Maticsoft.Accounts.MySqlData
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
            MySqlParameter[] parameters = 
                {
                    new MySqlParameter("?_CategoryID", MySqlDbType.Int32, 8),
                    new MySqlParameter("?_Description", MySqlDbType.VarChar, 50)
                };
            parameters[0].Value = categoryID;
            parameters[1].Value = description;
            //INSERT INTO Accounts_Permissions(CategoryID,Description) VALUES(_CategoryID,_Description);SELECT LAST_INSERT_ID();
            return DbHelperMySQL.RunProcedure("sp_Accounts_CreatePermission", parameters, out rowsAffected);
        }

        /// <summary>
        /// ����Ȩ����Ϣ
        /// </summary>
        public bool Update(int PermissionID, string description)
        {
            int rowsAffected;
            MySqlParameter[] parameters = 
                {
                    new MySqlParameter("?_PermissionID", MySqlDbType.Int32, 8),
                    new MySqlParameter("?_Description", MySqlDbType.VarChar, 50)
                };
            parameters[0].Value = PermissionID;
            parameters[1].Value = description;

            DbHelperMySQL.RunProcedure("sp_Accounts_UpdatePermission", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        public void UpdateCategory(string PermissionIDlist, int CategoryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Permissions set ");
            strSql.AppendFormat(" CategoryID={0}", CategoryID);
            strSql.AppendFormat(" where PermissionID in({0})", PermissionIDlist);

            DbHelperMySQL.ExecuteSql(strSql.ToString());

        }


        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>        
        public bool Delete(int id)
        {
            int rowsAffected;
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_PermissionID", MySqlDbType.Int32, 4)
                };
            parameters[0].Value = id;
            DbHelperMySQL.RunProcedure("sp_Accounts_DeletePermission", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// ����Ȩ��ID��ȡȨ����Ϣ
        /// </summary>
        public DataSet Retrieve(int permissionId)
        {
            MySqlParameter[] parameters = { new MySqlParameter("?_PermissionID", MySqlDbType.Int32, 4) };
            parameters[0].Value = permissionId;
            return DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionDetails", parameters, "Permissions");

        }

        /// <summary>
        /// ��ȡȨ���б�
        /// </summary>        
        public DataSet GetPermissionList()
        {
            MySqlParameter[] parameters = { new MySqlParameter("?_RoleID", MySqlDbType.VarChar, 4) };
            using (DataSet permissions = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionList", parameters, permissions, "Permissions");
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
            MySqlParameter[] parameters = { new MySqlParameter("?_RoleID", MySqlDbType.VarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet permissions = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionList", parameters, permissions, "Permissions");
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
            MySqlParameter[] parameters = { new MySqlParameter("?_RoleID", MySqlDbType.VarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet permissions = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DbHelperMySQL.RunProcedure("sp_Accounts_GetNoPermissionList", parameters, permissions, "Permissions");
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
