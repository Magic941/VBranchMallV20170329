/**
* Categories.cs
*
* 功 能： N/A
* 类 名： Categories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:40   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using Maticsoft.TaoBao;
using Maticsoft.TaoBao.Request;
using Maticsoft.TaoBao.Response;

namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 专辑类型
    /// </summary>
    public partial class Categories
    {
        private readonly ICategories dal = DASNS.CreateCategories();

        public Categories()
        { }

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
        public bool Exists(int CategoryId)
        {
            return dal.Exists(CategoryId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Categories model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.Categories model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CategoryId)
        {

            return dal.Delete(CategoryId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string CategoryIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(CategoryIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Categories GetModel(int CategoryId)
        {

            return dal.GetModel(CategoryId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.Categories GetModelByCache(int CategoryId)
        {

            string CacheKey = "CategoriesModel-" + CategoryId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(CategoryId);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.Categories)objModel;
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
        public List<Maticsoft.Model.SNS.Categories> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Categories> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Categories> modelList = new List<Maticsoft.Model.SNS.Categories>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Categories model;
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

        #region ExtensionMethod

  
        ///// <summary>
        ///// 递归调用插入类别表当中的数据
        ///// </summary>
        ///// <param name="CategoryId"></param>
        ///// <returns></returns>
        // public bool AddCategoryByLoop(int CategoryId)
        // {
        // }

        public List<Maticsoft.Model.SNS.Categories> GetChildrenListById(int Cid)
        {
            return GetModelList("Depth=3 AND Path LIKE '" + Cid + "|%' ");
        }

        /// <summary>
        /// 得到最顶级的栏目名称
        /// </summary>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Categories> GetMenuByCategory(int Top=-1)
        {
            string CacheKey = "GetMenuByCategory-" + Top;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    DataSet ds = GetList(Top, " ParentID=0 and IsMenu=1 and  Type=0", " Sequence");
                    objModel = DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SNS.Categories>)objModel;
        
        }

        #region 获取商品分类（For逛宝贝和商品类别页面）含缓存

        /// <summary>
        /// 获取一个分类下面的子分类和子分类的分类
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public Maticsoft.ViewModel.SNS.ProductCategory GetCateListByParentId(int ParentID)
        {
            Maticsoft.ViewModel.SNS.ProductCategory ParentList = new ViewModel.SNS.ProductCategory();
            if (ParentID == 0)
            {
                return ParentList;
            }
            Maticsoft.Model.SNS.Categories ParentModel = GetModel(ParentID);
            ParentList.CurrentCateName = ParentModel == null ? "暂无" : ParentModel.Name;
            ParentList.CurrentCid = ParentID;
            List<Maticsoft.Model.SNS.Categories> list = GetModelList("ParentID=" + ParentID + "");
            foreach (Maticsoft.Model.SNS.Categories items in list)
            {
                Maticsoft.ViewModel.SNS.SonCategory SonList = new ViewModel.SNS.SonCategory();
                SonList.ParentModel = items;
                SonList.Grandson = GetModelList("ParentID=" + items.CategoryId + "");
                ParentList.SonList.Add(SonList);
            }
            return ParentList;
        }

        /// <summary>
        /// 缓存获取一个分类下面的子分类和子分类的分类
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public Maticsoft.ViewModel.SNS.ProductCategory GetCacheCateListByParentId(int ParentID)
        {
            string CacheKey = "CacheCateList-" + ParentID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetCateListByParentId(ParentID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.ViewModel.SNS.ProductCategory)objModel;
        }

        #endregion 获取商品分类（For逛宝贝和商品类别页面）含缓存

        #region 获取商品分类（For首页）含缓存

        /// <summary>
        /// For首页获取一个分类下面的子分类和子分类的分类
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public Maticsoft.ViewModel.SNS.ProductCategory GetCateListByParentIdEx(int ParentID)
        {
            Maticsoft.ViewModel.SNS.ProductCategory ParentList = new ViewModel.SNS.ProductCategory();
            if (ParentID == 0)
            {
                return ParentList;
            }
            Maticsoft.Model.SNS.Categories ParentModel = GetModel(ParentID);
            ParentList.CurrentCateName = (ParentModel == null ? "暂无" : ParentModel.Name);
            ParentList.CurrentCid = ParentID;
            List<Maticsoft.Model.SNS.Categories> list = GetModelList("ParentID=" + ParentID + "");
            foreach (Maticsoft.Model.SNS.Categories items in list)
            {
                Maticsoft.ViewModel.SNS.SonCategory SonList = new ViewModel.SNS.SonCategory();
                SonList.ParentModel = items;
                SonList.Grandson = DataTableToList(GetListByPage("ParentID=" + items.CategoryId + "", "", 1, 5).Tables[0]);
                ParentList.SonList.Add(SonList);
            }
            return ParentList;
        }

        /// <summary>
        /// For首页缓存获取一个分类下面的子分类和子分类的分类
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public Maticsoft.ViewModel.SNS.ProductCategory GetCacheCateListByParentIdEx(int ParentID)
        {
            string CacheKey = "CacheCateListEx-" + ParentID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetCateListByParentIdEx(ParentID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.ViewModel.SNS.ProductCategory)objModel;
        }

        #endregion 获取商品分类（For首页）含缓存

        #region 顶级分类的下面子分类

        public List<Maticsoft.ViewModel.SNS.ProductCategory> GetChildByMenu()
        {
            List<Maticsoft.Model.SNS.Categories> TopList = GetMenuByCategory();
            List<Maticsoft.ViewModel.SNS.ProductCategory> ResultList = new List<ViewModel.SNS.ProductCategory>();
            foreach (Maticsoft.Model.SNS.Categories item in TopList)
            {
                Maticsoft.ViewModel.SNS.ProductCategory ParentList = new ViewModel.SNS.ProductCategory();
                ParentList.ParentModel = item;
                List<Maticsoft.Model.SNS.Categories> list = GetChildrenListById(item.CategoryId);
                ParentList.ChildList = list.Take(10).ToList();
                ResultList.Add(ParentList);
            }
            return ResultList;
        }

        #endregion 顶级分类的下面子分类

        /// <summary>
        /// 根据类别的id获取其最顶级的父级的名称（后期要家缓存）
        /// </summary>
        /// <param name="Cid"></param>
        public string GetTopNameByCid(int Cid)
        {
            Maticsoft.Model.SNS.Categories Cmodel = new Model.SNS.Categories();
            Cmodel = GetModel(Cid);
            if (Cmodel.ParentID == 0 || Cmodel.Depth == 1)
            {
                return Cmodel == null ? "暂无分类" : Cmodel.Name;
            }
            else
            {
                string[] ids = Cmodel.Path.Split('|');
                if (ids.Length > 0)
                {
                    Cmodel = GetModel(Common.Globals.SafeInt(ids[0], 0));
                    return Cmodel == null ? "暂无分类" : Cmodel.Name;
                }
                return "暂无分类";
            }
        }

        /// <summary>
        /// 根据类别的id获取其最顶级的父级的id（后期要家缓存）
        /// </summary>
        /// <param name="Cid"></param>
        public int GetTopCidByChildCid(int Cid)
        {
            Maticsoft.Model.SNS.Categories Cmodel = new Model.SNS.Categories();
            Cmodel = GetModel(Cid);
            if (Cmodel == null || Cmodel.ParentID == 0 || Cmodel.Depth == 1)
            {
                return Cid;
            }
            else
            {
                string[] ids = Cmodel.Path.Split('|');
                if (ids.Length > 0)
                {
                    return Common.Globals.SafeInt(ids[0], 0);
                }
                return 0;
            }
        }

        /// <summary>
        /// 根据类别的名称获取其最顶级的父级的名称（后期要家缓存）
        /// </summary>
        /// <param name="Cid"></param>
        public string GetTopNameByCid(string Name)
        {
            Maticsoft.Model.SNS.Categories Cmodel = new Model.SNS.Categories();
            Cmodel = GetModel(Name);
            if (Cmodel.ParentID == 0 || Cmodel.Depth == 1)
            {
                return Cmodel == null ? "暂无分类" : Cmodel.Name;
            }
            else
            {
                string[] ids = Cmodel.Path.Split('|');
                if (ids.Length > 0)
                {
                    Cmodel = GetModel(Common.Globals.SafeInt(ids[0], 0));
                    return Cmodel == null ? "暂无分类" : Cmodel.Name;
                }
                return "暂无分类";
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Categories GetModel(string Name)
        {
            List<Maticsoft.Model.SNS.Categories> list = GetModelList("Name='" + Name + "'");
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        #endregion ExtensionMethod

        #region 扩展方法

        /// <summary>
        /// 判断分类下是否存在礼品
        /// </summary>
        public bool IsExistedCate(int categoryid)
        {
            Maticsoft.BLL.SNS.Categories SNSCateBll = new BLL.SNS.Categories();
            int count = SNSCateBll.GetRecordCount("CategoryID=" + categoryid);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Maticsoft.Model.SNS.Categories> GetCategorysByDepth(int depth, int type)
        {
            //ADD Cache
            return GetModelList("Depth = " + depth + " and type=" + type);
        }

        public DataSet GetCategorysByParentId(int parentCategoryId)
        {
            //ADD Cache
            return GetList("ParentID = " + parentCategoryId);
        }

        public List<Maticsoft.Model.SNS.Categories> GetListByParentId(int parentCategoryId)
        {
            //ADD Cache
            return GetModelList("ParentID = " + parentCategoryId);
        }

        /// <summary>
        /// 添加分类（更新树形结构）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddCategory(Maticsoft.Model.SNS.Categories model)
        {
            return dal.AddCategory(model);
        }

        /// <summary>
        /// 更新分类(更新树形结构)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCategory(Maticsoft.Model.SNS.Categories model)
        {
            return dal.UpdateCategory(model);
        }

        /// <summary>
        /// 根据条件获取分类列表（是否排序）
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="IsOrder"></param>
        /// <returns></returns>
        public DataSet GetCategoryList(string strWhere)
        {
            return dal.GetCategoryList(strWhere);
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        public bool DeleteCategory(int categoryId)
        {
            return dal.DeleteCategory(categoryId);
        }

        public List<Maticsoft.Model.SNS.Categories> GetPhotoMenuCategoryList()
        {
            //ADD Cache
            return GetModelList("Type = 1 and  IsMenu=1 ");
        }

        ///// <summary>
        ///// 对分类信息进行排序
        ///// </summary>
        //public bool SwapSequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex)
        //{
        //    return dal.SwapSequence(CategoryId, zIndex);
        //}

        public bool AddCategories(Model.SNS.Categories model)
        {
            return dal.AddCategories(model);
        }
                /// <summary>
        /// 对分类进行排序
        /// </summary>
        public bool SwapCategorySequence(int CategoryId, Model.SNS.EnumHelper.SwapSequenceIndex zIndex)
        {
            return dal.SwapCategorySequence(CategoryId, zIndex);
        }


        /// <summary>
        /// 根据Id获取静态化URl
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetUrlByIdCache(int  Id)
        {
            string CacheKey = "GetUrlByPathCache-" + Id;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetUrlById(Id);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return objModel.ToString();
        }

        public string GetUrlById(int Id)
        {
            string pathUrl = "other";
            Maticsoft.Model.SNS.Categories CateModel =  GetModel(Id);
            if (CateModel != null)
            {
                var arry_path = CateModel.Path.Split('|');
                int i = 0;
                foreach (var item in arry_path)
                {
                    Maticsoft.Model.SNS.Categories model = GetModelByCache(Common.Globals.SafeInt(item, 0));
                    if (model != null)
                    {
                        if (i == 0)
                        {
                            pathUrl = Maticsoft.Common.PinyinHelper.GetPinyin(model.Name).ToLower();
                        }
                        else
                        {
                            pathUrl = pathUrl + "/" + Maticsoft.Common.PinyinHelper.GetPinyin(model.Name).ToLower() ;
                        }
                    }
                    i++;
                }
            }
            return pathUrl;
        }

        public List<Maticsoft.Model.SNS.Categories> GetCategoryList(int parentId,int type)
        {
            return GetModelList(string.Format("  ParentID ={0} and type={1} ORDER BY Sequence ASC", parentId, type));
        }


        public List<Maticsoft.Model.SNS.Categories> GetChildList(int parentId, int top,int type)
        {
            string CacheKey = "GetChildList-" + parentId + top + type;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    DataSet ds = dal.GetList(top, string.Format(" ParentID ={0} and type={1}  ", parentId, type), "Sequence ASC");
                    objModel = DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SNS.Categories>)objModel;
           

        }

        /// <summary>
        /// 缓存获取所有的分类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Categories> GetAllCateByCache(int type)
        {
            string CacheKey = "GetAllCateByCache-" + type;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModelList(string.Format(" Status=1 and type={0} ORDER BY Sequence ASC", type));
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SNS.Categories>)objModel;
        }

        public static List<Maticsoft.Model.SNS.Categories> GetAllList(int type)
        {
            string CacheKey = "GetAllList-" + type;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.BLL.SNS.Categories cateBll=new Categories();
                    objModel = cateBll.GetAllCateByCache(type);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SNS.Categories>)objModel;
        }

        #endregion 扩展方法
    }
}