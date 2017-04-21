using System.Data;
using System.Text;
using Maticsoft.Accounts.IData;
using MySql.Data.MySqlClient;

namespace Maticsoft.Accounts.MySqlData
{
    /// <summary>
    ///权限类别
    /// </summary>
    public class PermissionCategory : IPermissionCategory
    {
        public PermissionCategory()
        { }

        /// <summary>
        /// 创建权限类别
        /// </summary>        
        public int Create(string description)
        {
            int rowsAffected;
            MySqlParameter[] parameters = 
                {
                    new MySqlParameter("?_Description", MySqlDbType.VarChar, 50)
                };
            parameters[0].Value = description;
            //INSERT INTO Accounts_PermissionCategories(Description) VALUES(_Description);SELECT LAST_INSERT_ID();
            return DbHelperMySQL.RunProcedure("sp_Accounts_CreatePermissionCategory", parameters, out rowsAffected);
        }

        /// <summary>
        /// 该类别下是否存在权限记录
        /// </summary>
        public bool ExistsPerm(int CategoryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_Permissions");
            strSql.Append(" where CategoryID=?CategoryID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("?CategoryID", MySqlDbType.Int32,4)
            };
            parameters[0].Value = CategoryID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除权限类别
        /// </summary>        
        public bool Delete(int CategoryID)
        {
            int rowsAffected;
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_CategoryID", MySqlDbType.Int32, 4)
                };
            parameters[0].Value = CategoryID;
            DbHelperMySQL.RunProcedure("sp_Accounts_DeletePermissionCategory", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// 获取权限类别信息
        /// </summary>        
        public DataRow Retrieve(int categoryId)
        {
            MySqlParameter[] parameters = 
                {
                    new MySqlParameter("?_CategoryID", MySqlDbType.Int32, 4)
                };
            parameters[0].Value = categoryId;

            using (DataSet categories = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategoryDetails", parameters, "Categories"))
            {
                return categories.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// 获取指定类别下的权限列表
        /// </summary>        
        public DataSet GetPermissionsInCategory(int categoryId)
        {
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("?_CategoryID", MySqlDbType.Int32, 4)
                };
            parameters[0].Value = categoryId;
            using (DataSet permissions = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionsInCategory",
                       parameters, "Categories"))
            {
                return permissions;
            }
        }

        /// <summary>
        /// 获取权限类别的列表
        /// </summary>        
        public DataSet GetCategoryList()
        {
            using (DataSet categories = DbHelperMySQL.RunProcedure("sp_Accounts_GetPermissionCategories",
                       new IDataParameter[] { },
                       "Categories"))
            {
                return categories;
            }
        }
    }
}
