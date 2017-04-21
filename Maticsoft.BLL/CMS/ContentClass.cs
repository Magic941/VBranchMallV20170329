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
    /// 栏目
    /// </summary>
    public partial class ContentClass
    {
        private readonly IContentClass dal = DataAccess<IContentClass>.Create("CMS.ContentClass");

        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ClassID)
        {
            return dal.Exists(ClassID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.CMS.ContentClass model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.CMS.ContentClass model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ClassID)
        {

            return dal.Delete(ClassID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ClassIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ClassIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.ContentClass GetModel(int ClassID)
        {

            return dal.GetModel(ClassID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
      
        /// <summary>
        /// 获得数据列表
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region MethodEx

        #region 批量审核
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }
        #endregion

        #region  获取树集合
        /// <summary>
        /// 获取树集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetTreeList(string strWhere)
        {
            return dal.GetTreeList(strWhere);
        }
        #endregion

        #region 删除分类信息
        /// <summary>
        /// 删除分类信息
        /// </summary>
        public bool DeleteCategory(int categoryId)
        {
            return dal.DeleteCategory(categoryId);
        }
        #endregion

        #region 对类别进行排序
        /// <summary>
        /// 对类别进行排序
        /// </summary>
        /// <param name="ContentClassId"></param>
        /// <param name="zIndex"></param>
        /// <returns></returns>
        public int SwapCategorySequence(int ContentClassId, Maticsoft.Common.Video.SwapSequenceIndex zIndex)
        {
            return dal.SwapCategorySequence(ContentClassId, zIndex);
        }
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListByView(string strWhere)
        {
            return dal.GetListByView(strWhere);
        }
        #endregion

        #region 获得前几行数据
        /// <summary>
        /// 获得前几行数据
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
        /// 扩展属性Model
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
        /// 扩展属性Model
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
        /// 根据path获取UrlPath(自定义的URL，字段没值，会默认返回栏目ID)
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
        /// 返回拼音URL（栏目名称的拼音）
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
            int rule = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("CMS_Static_ClassRule"); //获取商品静态的根目录
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
        /// 获得栏目的名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetClassnameById(int id)
        {
            Maticsoft.Model.CMS.ContentClass model = GetModelByCache(id);
            if (model != null)        
                return model.ClassName;
            return "此栏目已不存在";
        }

        /// <summary>
        /// 获得根栏目的名称
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
                    if (model.ParentId.HasValue)  //因为parentId是可空类型的
                    {
                        int classid = Convert.ToInt32(model.ParentId);
                        model = GetModel(classid);
                    }
                    return model.ClassName;
                }
            }
            return "此栏目已不存在";
        }
        /// <summary>
        /// 获得根栏目的名称
        /// </summary>
        /// <param name="id">classid</param>
        /// <returns></returns>
        public string GetAClassnameById(int id,out int Aclassid)
        {
            Aclassid = -1;
            Maticsoft.Model.CMS.ContentClass model = GetModelByCache(id);
            if (model == null)
                return "此栏目已不存在";
            if (model.ParentId == 0)
            {
                Aclassid = model.ClassID;
                return model.ClassName;
            }
            else
            {
                if (model.ParentId.HasValue)  //因为parentId是可空类型的
                {
                    int classid = Convert.ToInt32(model.ParentId);
                    model = GetModel(classid);
                }
                Aclassid = model.ClassID;
                return model.ClassName;
            }
        }

        /// <summary>
        /// 获得根栏目的Id
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
                    if (model.ParentId.HasValue)  //因为parentId是可空类型的
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
        /// 获得数据列表     
        /// </summary>
        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(int classid,out Model.CMS.ContentClass classmodel)
        {
            int AClassId = GetClassIdById(classid);//根栏目ID
            classmodel = GetModelByCache(AClassId);//根栏目Model
            List<Maticsoft.Model.CMS.ContentClass> list = GetModelList(string.Format(" ParentId={0}", AClassId));//子栏目列表
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
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.ContentClass> GetModelList(int Top, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(Top, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
      /// <summary>
        /// 获得数据列表 根据根栏目判断是否存在子栏目 如果存在子栏目则返回子栏目内容，否则返回根栏目内容
      /// </summary>
      /// <param name="Top">前几条</param>
      /// <param name="classid">栏目类别</param>
      /// <returns></returns>
      public List<Model.CMS.ContentClass> GetModelList(int Top,int? classid,out string classname)
      {
            classname = "此栏目不存在";
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

