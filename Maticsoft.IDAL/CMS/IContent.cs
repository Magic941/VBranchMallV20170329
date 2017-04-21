using System;
using System.Data;
using System.Collections.Generic;
namespace Maticsoft.IDAL.CMS
{
    /// <summary>
    /// �ӿڲ�Content
    /// </summary>
    public interface IContent
    {
        #region  ��Ա����
        /// <summary>
        /// �õ����ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        bool Exists(int ContentID);
        /// <summary>
        /// ����һ������
        /// </summary>
        int Add(Maticsoft.Model.CMS.Content model);
        /// <summary>
        /// ����һ������
        /// </summary>
        bool Update(Maticsoft.Model.CMS.Content model);
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        bool Delete(int ContentID);
        bool DeleteList(string ContentIDlist);
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        Maticsoft.Model.CMS.Content GetModel(int ContentID);
        Maticsoft.Model.CMS.Content DataRowToModel(DataRow row);
        /// <summary>
        /// ��������б�
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        int GetRecordCount4Menu(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// ���ݷ�ҳ��������б�
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  ��Ա����
        /// <summary>
        /// ���ô洢���̻�ȡ��һ������һ��
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        DataSet GetNextAndUp(int classId,int contentId);


        #region MethodEx

        /// <summary>
        /// ����һ������
        /// </summary>
        int UpdatePV(int ContentID);
        /// <summary>
        /// ����һ������
        /// </summary>
        bool UpdateTotalSupport(int ContentID);

        /// <summary>
        /// ��������б�
        /// </summary>
        List<Maticsoft.Model.CMS.Content> DataTableToListEx(DataTable dt);

        #region �����������״̬
        /// �����������״̬
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strWhere);
        #endregion

        #region ����ClassID�ж��Ƿ���ڸü�¼
        /// <summary>
        /// ����ClassID�ж��Ƿ���ڸü�¼
        /// </summary>
        bool ExistsByClassID(int ClassID);
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        DataSet GetListByView(string strWhere);
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        DataSet GetListByView(int Top, string strWhere, string filedOrder);
        #endregion


        #region ����ĳ�ֶλ��ǰ��������
        ///<summary>
        ///����ĳ�ֶλ��ǰ��������
        /// </summary>
        DataSet GetListByItem(int Top, string strWhere, string filedOrder);
        #endregion


        #region �õ�һ������ʵ��
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        Maticsoft.Model.CMS.Content GetModelEx(int ContentID);
        #endregion

        #region �õ���һ��ContentID
        /// <summary>
        /// �õ���һ��ContentID
        /// </summary>
        int GetPrevID(int ContentID, int ClassId);
        #endregion

        #region �õ���һ��ContentID
        /// <summary>
        /// �õ���һ��ContentID
        /// </summary>
        int GetNextID(int ContentID,int ClassId);
        #endregion
        bool ExistTitle(string Title);

        DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex);

     DataSet GetHotCom(int diffDate, int top);

        bool UpdateFav(int ContentID);


        bool SetRecList(string ids);
        bool SetHotList(string ids);
        bool SetColorList(string ids);
        bool SetTopList(string ids);

        bool SetRec(int id,bool rec);
        bool SetHot(int id, bool rec);
        bool SetColor(int id, bool rec);
        bool SetTop(int id, bool rec);

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        DataSet GetListEx(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// ��ȡ��¼����
        /// </summary>
        int GetRecordCountEx(string strWhere);

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        Maticsoft.Model.CMS.Content GetModelByClassID(int ClassID);



        DataSet GetWeChatList(int ClassID, string keyword, int Top);

        

        /// <summary>
        /// ��ȡ˳�����ֵ
        /// </summary>
        /// <returns></returns>
        int GetMaxSeq();

        #endregion MethodEx
    }
}
