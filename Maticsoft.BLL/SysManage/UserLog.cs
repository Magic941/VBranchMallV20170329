using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SysManage;

namespace Maticsoft.BLL.SysManage
{
    
    /// <summary>
    /// �û���־
    /// </summary>
    public class UserLog
    {
        private static IUserLog dal = DASysManage.CreateUserLog();
               
        #region 
        
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="model">Ҫ���ӵ���־ʵ�����</param>
        public static void LogUserAdd(Maticsoft.Model.SysManage.UserLog model)
        {
            dal.LogUserAdd(model);
        }
        /// <summary>
        /// ��ȡ������־����
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// ���ݲ�ѯ������ȡ��־�б�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns>���ص����ݼ�</returns>
        public static DataSet GetAllList()
        {
            return dal.GetList("");
        }

        /// <summary>
        /// ɾ��һ����־��¼
        /// </summary>
        /// <param name="iID">Ҫɾ������־���</param>
        public static void Delete(int iID)
        {
            dal.LogUserDelete(iID);
        }
        public static void Delete(string IdList)
        {
            dal.LogUserDelete(IdList);
        }
        /// <summary>
        /// ɾ��ĳһ����֮ǰ������
        /// </summary>
        /// <param name="dtDateBefore">����</param>
        public static void Delete(DateTime dtDateBefore)
        {
            dal.LogUserDelete(dtDateBefore);
        }
                
        #endregion

    
    }
    
}
