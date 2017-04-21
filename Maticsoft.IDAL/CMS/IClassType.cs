using System;
using System.Data;

using System.Collections.Generic;

namespace Maticsoft.IDAL.CMS
{
	/// <summary>
	/// �ӿڲ�ClassType
	/// </summary>
	public interface IClassType
	{
		#region  ��Ա����
		/// <summary>
		/// �õ����ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		bool Exists(int ClassTypeID);
		/// <summary>
		/// ����һ������
		/// </summary>
        bool Add(Maticsoft.Model.CMS.ClassType model);
		/// <summary>
		/// ����һ������
		/// </summary>
		bool Update(Maticsoft.Model.CMS.ClassType model);
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		bool Delete(int ClassTypeID);
        /// <summary>
        /// ����ɾ������
        /// </summary>
		bool DeleteList(string ClassTypeIDlist );
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		Maticsoft.Model.CMS.ClassType GetModel(int ClassTypeID);
		/// <summary>
		/// ��������б�
		/// </summary>
		DataSet GetList(string strWhere);
          /// <summary>
        /// ��������б�
        /// </summary>
        List<Maticsoft.Model.CMS.ClassType> DataTableToList(DataTable dt);
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		/// <summary>
		/// ���ݷ�ҳ��������б�
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  ��Ա����
	} 
}
