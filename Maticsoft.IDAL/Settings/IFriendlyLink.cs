using System;
using System.Data;
using System.Collections.Generic;
namespace Maticsoft.IDAL.Settings
{
	/// <summary>
	/// �ӿڲ�FLinks
	/// </summary>
    public interface IFriendlyLink
	{
		#region  ��Ա����
		/// <summary>
		/// �õ����ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int ID);
		/// <summary>
		/// ����һ������
		/// </summary>
		int Add(Maticsoft.Model.Settings.FriendlyLink model);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Update(Maticsoft.Model.Settings.FriendlyLink model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Maticsoft.Model.Settings.FriendlyLink GetModel(int ID);
		/// <summary>
		/// ��������б�
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
         /// <summary>
        /// ��������б�
        /// </summary>
        Maticsoft.Model.Settings.FriendlyLink DataRowToModel(DataRow row);
		/// <summary>
		/// ���ݷ�ҳ��������б�
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
        /// <summary>
        /// <summary>
        /// �����������״̬
        /// </summary>
        /// <param name="IDlsit"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist,string strWhere);
		#endregion  ��Ա����
	} 
}
