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
    /// ��Ŀ
    /// </summary>
    public partial class ContentClass
    {
        private readonly IContentClass dal = DataAccess<IContentClass>.Create("CMS.ContentClass");

        #region  BasicMethod

        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int ClassID)
        {
            return dal.Exists(ClassID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Maticsoft.Model.CMS.ContentClass model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Maticsoft.Model.CMS.ContentClass model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int ClassID)
        {

            return dal.Delete(ClassID);
        }
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool DeleteList(string ClassIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ClassIDlist,0) );
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Maticsoft.Model.CMS.ContentClass GetModel(int ClassID)
        {

            return dal.GetModel(ClassID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ�����
        /// </summary>
        public Maticsoft.Model.CMS.ContentClass GetModelByCache(int ClassID)
        {

            string CacheKey = "ContentClassModel-" + ClassID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ClassID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.CMS.ContentClass)objModel;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
      
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Maticsoft.Model.CMS.ContentClass> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.ContentClass> modelList = new List<Maticsoft.Model.CMS.ContentClass>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.CMS.ContentClass model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��ҳ��ȡ�����б�
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// ��ҳ��ȡ�����б�
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// ��ҳ��ȡ�����б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region MethodEx

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }
        #endregion

        #region  ��ȡ������
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetTreeList(string strWhere)
        {
            return dal.GetTreeList(strWhere);
        }
        #endregion

        #region ɾ��������Ϣ
        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        public bool DeleteCategory(int categoryId)
        {
            return dal.DeleteCategory(categoryId);
        }
        #endregion

        #region ������������
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="ContentClassId"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        public int SwapCategorySequence(int ContentClassId, Maticsoft.Common.Video.SwapSequenceIndex zIndex)
        {
            return dal.SwapCategorySequence(ContentClassId, zIndex);
        }
        #endregion

        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetListByView(string strWhere)
        {
            return dal.GetListByView(strWhere);
        }
        #endregion

        #region ���ǰ��������
        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetListByView(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListByView(Top, strWhere, filedOrder);
        }
        #endregion

        public bool AddExt(Maticsoft.Model.CMS.ContentClass model)
        {
            return dal.AddExt(model);
        }

        public string GetNamePathByPath(string path)
        {
            return dal.GetNamePathByPath(path);
        }

        /// <summary>
        /// ��չ����Model
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public Maticsoft.Model.CMS.ContentClass GetModelEx(int classId)
        {
            Maticsoft.Model.CMS.ContentClass model = GetModel(classId);
            if (model != null)
            {
                model.NamePath = GetNamePathByPath(model.Path);
            }
            return model;
        }

        /// <summary>
        /// ��չ����Model
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public Maticsoft.Model.CMS.ContentClass GetModelExCache(int classId)
        {
            string CacheKey = "GetModelExCache-" + classId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModelEx(classId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.CMS.ContentClass)objModel;
        }



        /// <summary>
        /// ����path��ȡUrlPath(�Զ����URL���ֶ�ûֵ����Ĭ�Ϸ�����ĿID)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetCustomUrl(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                return "";
            var path_arry = path.Split(',');
            string Url = "";
            int i = 0;
            foreach (var item in path_arry)
            {
                Maticsoft.Model.CMS.ContentClass model = GetModelByCache(Common.Globals.SafeInt(item, 0));
                if (model == null)
                    return "";
                if (i == 0)
                {
                    Url = String.IsNullOrWhiteSpace(model.IndexChar) ? model.ClassID.ToString() : model.IndexChar;
                }
                else
                {
                    Url = Url + "/" + (String.IsNullOrWhiteSpace(model.IndexChar) ? model.ClassID.ToString() : model.IndexChar);
                }
            }
            return Url;
        }

        /// <summary>
        /// ����ƴ��URL����Ŀ���Ƶ�ƴ����
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetPingYinUrl(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                return "";
            var path_arry = path.Split(',');
            string Url = "";
            int i = 0;
            foreach (var item in path_arry)
            {
                Maticsoft.Model.CMS.ContentClass model = GetModelByCache(Common.Globals.SafeInt(item, 0));
                if (model == null)
                    return "";
                if (i == 0)
                {
                    Url = Maticsoft.Common.PinyinHelper.GetPinyin(model.ClassName).ToLower();
                }
                else
                {
                    Url = Url + "/" + (Maticsoft.Common.PinyinHelper.GetPinyin(model.ClassName).ToLower());
                }
            }
            return Url;
        }


        public string GetClassUrl(int classId)
        {
            Maticsoft.Model.CMS.ContentClass model = GetModelByCache(classId);
            if (model == null)
                return "";
            int rule = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("CMS_Static_ClassRule"); //��ȡ��Ʒ��̬�ĸ�Ŀ¼
            if (rule == 0)
            {
                return model.Path.Replace("|", "/");
            }
            if (rule == 1)
            {
                return GetPingYinUrl(model.Path);
            }
            if (rule == 2)
            {
                return GetCustomUrl(model.Path);
            }
            return model.Path.Replace("|", "/");
        }

        /// <summary>
        /// �����Ŀ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetClassnameById(int id)
        {
            Maticsoft.Model.CMS.ContentClass model = GetModelByCache(id);
            if (model != null)        
                return model.ClassName;
            return "����Ŀ�Ѳ�����";
        }

        /// <summary>
        /// ��ø���Ŀ������
        /// </summary>
        /// <param name="id">classid</param>
        /// <returns></returns>
        public  string GetAClassnameById(int id)
        {
            Maticsoft.Model.CMS.ContentClass model = GetModelByCache(id);
            if (model != null)
            {
                if (model.ParentId == 0)
                {
                    return model.ClassName;
                }
                else
                {
                    if (model.ParentId.HasValue)  //��ΪparentId�ǿɿ����͵�
                    {
                        int classid = Convert.ToInt32(model.ParentId);
                        model = GetModel(classid);
                    }
                    return model.ClassName;
                }
            }
            return "����Ŀ�Ѳ�����";
        }
        /// <summary>
        /// ��ø���Ŀ������
        /// </summary>
        /// <param name="id">classid</param>
        /// <returns></returns>
        public string GetAClassnameById(int id,out int Aclassid)
        {
            Aclassid = -1;
            Maticsoft.Model.CMS.ContentClass model = GetModelByCache(id);
            if (model == null)
                return "����Ŀ�Ѳ�����";
            if (model.ParentId == 0)
            {
                Aclassid = model.ClassID;
                return model.ClassName;
            }
            else
            {
                if (model.ParentId.HasValue)  //��ΪparentId�ǿɿ����͵�
                {
                    int classid = Convert.ToInt32(model.ParentId);
                    model = GetModel(classid);
                }
                Aclassid = model.ClassID;
                return model.ClassName;
            }
        }

        /// <summary>
        /// ��ø���Ŀ��Id
        /// </summary>
        /// <param name="id">classID</param>
        /// <returns></returns>
        public int GetClassIdById(int id)
        {
            Maticsoft.BLL.CMS.ContentClass bll = new BLL.CMS.ContentClass();
            Maticsoft.Model.CMS.ContentClass model = bll.GetModelByCache(id);
            if (model != null)
            {
                if (model.ParentId == 0)
                {
                    return model.ClassID;
                }
                else
                {
                    if (model.ParentId.HasValue)  //��ΪparentId�ǿɿ����͵�
                    {
                        int classid = Convert.ToInt32(model.ParentId);
                        model = bll.GetModel(classid);
                    }
                    return model.ClassID;
                }
            }
            return 0;
        }
        #endregion
        
  
        /// <summary>
        /// ��������б�     
        /// </summary>
        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(int classid,out Model.CMS.ContentClass classmodel)
        {
            int AClassId = GetClassIdById(classid);//����ĿID
            classmodel = GetModelByCache(AClassId);//����ĿModel
            List<Maticsoft.Model.CMS.ContentClass> list = GetModelList(string.Format(" ParentId={0}", AClassId));//����Ŀ�б�
            return list;
        }

        public static List<Maticsoft.Model.CMS.ContentClass> GetAllClass()
        {
            string CacheKey = "ContentClass-GetAllClass";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.BLL.CMS.ContentClass classBll=new ContentClass();
                    objModel = classBll.GetModelList("");
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.CMS.ContentClass>)objModel;
        }



        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
      /// <summary>
        /// ��������б� ���ݸ���Ŀ�ж��Ƿ��������Ŀ �����������Ŀ�򷵻�����Ŀ���ݣ����򷵻ظ���Ŀ����
      /// </summary>
      /// <param name="Top">ǰ����</param>
      /// <param name="classid">��Ŀ���</param>
      /// <returns></returns>
      public List<Model.CMS.ContentClass> GetModelList(int Top,int? classid,out string classname)
      {
            classname = "����Ŀ������";
            if (classid.HasValue)
            {
                BLL.CMS.ContentClass classContBll = new BLL.CMS.ContentClass();
                classname= classContBll.GetClassnameById(classid.Value);
                List<Model.CMS.ContentClass> clascontList= GetModelList(Top, string.Format("  State=0  and  ParentId in ({0})", classid.Value), " Sequence ");
                if (clascontList != null && clascontList.Count>0)
                {
                    return clascontList;
                }
                else
                {
                    return GetModelList(1, string.Format("  State=0  and  ClassID ={0}", classid.Value), " Sequence ");
                }
            }
            return null;
        }

    }
}

