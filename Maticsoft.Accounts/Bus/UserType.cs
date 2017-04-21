using System;
using System.Data;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// �û�����
    /// </summary>
    public class UserType
    {
        private IUserType dal;

        public UserType()
        {
            dal = PubConstant.IsSQLServer ? (IUserType)new Data.UserType() : new MySqlData.UserType();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string UserType, string Description)
        {
            return dal.Exists(UserType, Description);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(string UserType, string Description)
        {
            dal.Add(UserType, Description);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(string UserType, string Description)
        {
            dal.Update(UserType, Description);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string UserType)
        {
            dal.Delete(UserType);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public string GetDescription(string UserType)
        {
            return dal.GetDescription(UserType);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
        /// </summary>
        public string GetDescriptionByCache(string UserType)
        {
            string CacheKey = "Accounts_UserTypeModel-" + UserType;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetDescription(UserType);
                    if (objModel != null)
                    {
                        int CacheTime = ConfigHelper.GetConfigInt("CacheTime");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return objModel.ToString();
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
    }
}
