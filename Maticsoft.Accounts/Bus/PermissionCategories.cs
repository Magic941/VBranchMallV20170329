using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// Ȩ�����
    /// </summary>
    public class PermissionCategories
    {
        private IData.IPermissionCategory dalpc = PubConstant.IsSQLServer ? (IPermissionCategory)new Data.PermissionCategory() : new MySqlData.PermissionCategory();

        /// <summary>
        /// ���캯��
        /// </summary>
        public PermissionCategories()
        {
        }

        /// <summary>
        /// ����Ȩ�����
        /// </summary>
        public int Create(string description)
        {
            int pcID = dalpc.Create(description);
            return pcID;
        }

        /// <summary>
        /// ��������Ƿ����Ȩ�޼�¼
        /// </summary>
        public bool ExistsPerm(int CategoryID)
        {
            return dalpc.ExistsPerm(CategoryID);
        }

        /// <summary>
        /// ɾ��Ȩ�����
        /// </summary>
        public bool Delete(int pID)
        {
            return dalpc.Delete(pID);
        }
    }
}
