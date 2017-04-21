using System;
using System.Data;

using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.CMS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.CMS;

namespace Maticsoft.BLL.CMS
{
	/// <summary>
	/// ��Ŀ����
	/// </summary>
	public partial class ClassType
	{        
        private readonly IClassType dal = DataAccess<IClassType>.Create("CMS.ClassType");
		
		#region  Method

        #region �õ����ID
        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        } 
        #endregion

        #region �Ƿ���ڸü�¼
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int ClassTypeID)
        {
            return dal.Exists(ClassTypeID);
        } 
        #endregion

        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Add(Maticsoft.Model.CMS.ClassType model)
        {
            return dal.Add(model);
        } 
        #endregion

        #region ����һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Maticsoft.Model.CMS.ClassType model)
        {
            return dal.Update(model);
        } 
        #endregion

        #region ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int ClassTypeID)
        {

            return dal.Delete(ClassTypeID);
        } 
        #endregion

        #region ����ɾ������
        /// <summary>
        /// ����ɾ������
        /// </summary>
        public bool DeleteList(string ClassTypeIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ClassTypeIDlist,0) );
        } 
        #endregion

        #region �õ�һ������ʵ��
        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Maticsoft.Model.CMS.ClassType GetModel(int ClassTypeID)
        {
            return dal.GetModel(ClassTypeID);
        } 
        #endregion

        #region �ӻ����еõ�һ������ʵ��
        /// <summary>
        /// �ӻ����еõ�һ������ʵ��
        /// </summary>
        public Maticsoft.Model.CMS.ClassType GetModelByCache(int ClassTypeID)
        {
            string CacheKey = "ClassTypeModel-" + ClassTypeID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ClassTypeID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.CMS.ClassType)objModel;
        } 
        #endregion

        #region ��������б�

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        } 
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        } 
        #endregion

        #region ���ǰ��������
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        } 
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Maticsoft.Model.CMS.ClassType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            return dal.DataTableToList(ds.Tables[0]);
        } 
        #endregion

       

        #region ��ҳ��ȡ�����б�
        /// <summary>
        /// ��ҳ��ȡ�����б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //} 
        #endregion

		#endregion  Method
	}
}

