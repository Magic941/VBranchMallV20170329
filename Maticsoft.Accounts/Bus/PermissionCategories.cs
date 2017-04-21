using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// 权限类别。
    /// </summary>
    public class PermissionCategories
    {
        private IData.IPermissionCategory dalpc = PubConstant.IsSQLServer ? (IPermissionCategory)new Data.PermissionCategory() : new MySqlData.PermissionCategory();

        /// <summary>
        /// 构造函数
        /// </summary>
        public PermissionCategories()
        {
        }

        /// <summary>
        /// 创建权限类别
        /// </summary>
        public int Create(string description)
        {
            int pcID = dalpc.Create(description);
            return pcID;
        }

        /// <summary>
        /// 该类别下是否存在权限记录
        /// </summary>
        public bool ExistsPerm(int CategoryID)
        {
            return dalpc.ExistsPerm(CategoryID);
        }

        /// <summary>
        /// 删除权限类别
        /// </summary>
        public bool Delete(int pID)
        {
            return dalpc.Delete(pID);
        }
    }
}
