using System;
using System.Data;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// 权限管理。
    /// </summary>
    [Serializable]
    public class Permissions
    {
        private IData.IPermission dalPermission = PubConstant.IsSQLServer
                                                       ? (IPermission)new Data.Permission()
                                                       : new MySqlData.Permission();


        private int _permissionID;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int PermissionID
        {
            get
            {
                return _permissionID;
            }
            set
            {
                _permissionID = value;
            }
        }


        private string _description;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private int _categoryID;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public Permissions()
        {
        }

        /// <summary>
        /// 创建一个权限
        /// </summary>
        /// <param name="pcID">类别ID</param>
        /// <param name="description">权限描述</param>
        /// <returns></returns>
        public int Create(int pcID, string description)
        {
            int pID = dalPermission.Create(pcID, description);
            return pID;
        }
        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="pcID">权限ID</param>
        /// <param name="description">权限描述</param>
        /// <returns></returns>
        public bool Update(int pcID, string description)
        {
            return dalPermission.Update(pcID, description);
        }

        public void UpdateCategory(string PermissionIDlist, int CategoryID)
        {
            dalPermission.UpdateCategory(PermissionIDlist, CategoryID);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public bool Delete(int pID)
        {
            return dalPermission.Delete(pID);
        }

        /// <summary>
        /// 得到权限名称
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public string GetPermissionName(int permissionId)
        {
            DataSet permissions = dalPermission.Retrieve(permissionId);
            if (permissions.Tables[0].Rows.Count == 0)
            {
                throw new Exception("找不到权限 （" + permissionId + "）");
            }
            else
            {
                return permissions.Tables[0].Rows[0]["Description"].ToString();
            }
        }

        /// <summary>
        /// 得到权限信息
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public void GetPermissionDetails(int pID)
        {
            DataSet permissions = dalPermission.Retrieve(pID);
            if (permissions.Tables[0].Rows.Count > 0)
            {
                if (permissions.Tables[0].Rows[0]["PermissionID"] != null)
                {
                    _permissionID = Convert.ToInt32(permissions.Tables[0].Rows[0]["PermissionID"]);
                }
                _description = permissions.Tables[0].Rows[0]["Description"].ToString();
                if (permissions.Tables[0].Rows[0]["CategoryID"] != null)
                {
                    _categoryID = Convert.ToInt32(permissions.Tables[0].Rows[0]["CategoryID"]);
                }
            }
        }
    }
}
