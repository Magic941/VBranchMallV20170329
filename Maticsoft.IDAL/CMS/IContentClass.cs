using System;
using System.Data;
using System.Collections.Generic;
namespace Maticsoft.IDAL.CMS
{
	/// <summary>
	/// �ӿڲ�ContentClass
	/// </summary>
	public interface IContentClass
    {
        #region  ��Ա����
        /// <summary>
        /// �õ����ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        bool Exists(int ClassID);
        /// <summary>
        /// ����һ������
        /// </summary>
        int Add(Maticsoft.Model.CMS.ContentClass model);
        /// <summary>
        /// ����һ������
        /// </summary>
        bool Update(Maticsoft.Model.CMS.ContentClass model);
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        bool Delete(int ClassID);
        bool DeleteList(string ClassIDlist);
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        Maticsoft.Model.CMS.ContentClass GetModel(int ClassID);
        Maticsoft.Model.CMS.ContentClass DataRowToModel(DataRow row);
        /// <summary>
        /// ��������б�
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// ���ݷ�ҳ��������б�
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  ��Ա����

        #region MethodEx

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strWhere); 
        #endregion

        #region ��ȡ������
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetTreeList(string strWhere); 
        #endregion

        #region ɾ��������Ϣ
        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        bool DeleteCategory(int categoryId); 
        #endregion

        #region ������������
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="VideoClassId">���ID</param>
        /// <param name="zIndex">����ʽ</param>
        /// <returns></returns>
        int SwapCategorySequence(int ContentClassId, Maticsoft.Common.Video.SwapSequenceIndex zIndex);
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        DataSet GetListByView(string strWhere);
        #endregion

        #region ���ǰ��������
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        DataSet GetListByView(int Top, string strWhere, string filedOrder);
        #endregion
        bool AddExt(Maticsoft.Model.CMS.ContentClass model);

        string GetNamePathByPath(string path);
        #endregion
    } 
}
