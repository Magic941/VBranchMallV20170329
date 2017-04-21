/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：CategoryInfo.cs
// 文件功能描述：
//
// 创建标识： [Ben]  2012/06/11 20:36:23
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using System.Linq;
using Maticsoft.IDAL.Shop.Products;

using ServiceStack.RedisCache;

namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// CategoryInfo
    /// </summary>
    public partial class CategoryInfo
    {
        private readonly ICategoryInfo dal = DAShopProducts.CreateCategoryInfo();
        //ServiceStack.RedisCache.Products.CategoryInfo categoryInfoCache = new ServiceStack.RedisCache.Products.CategoryInfo();

        public CategoryInfo()
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
        public int Add(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.CategoryInfo model)
        {
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "CategoryInfoModel-" + model.CategoryId.ToString();
                if (RedisBase.Item_Get<Maticsoft.Model.Shop.Products.CategoryInfo>(CacheKey) != null)
                {
                    RedisBase.Item_Set<Maticsoft.Model.Shop.Products.CategoryInfo>(CacheKey, model);
                }
            }

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
        public Maticsoft.Model.Shop.Products.CategoryInfo GetModel(int CategoryId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo category = null;
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "CategoryInfoModel-" + CategoryId;
                category = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.CategoryInfo>(CacheKey);
            }

            if (category == null)
            {
                category = dal.GetModel(CategoryId);
            }

            return category;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.CategoryInfo GetModelByCache(int CategoryId)
        {
            object objModel = null;
            string CacheKey = "CategoryInfoModel-" + CategoryId;

            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo category = new Maticsoft.Model.Shop.Products.CategoryInfo();
                category = RedisBase.Item_Get<Maticsoft.Model.Shop.Products.CategoryInfo>(CacheKey);
                objModel = category;
            }

            if (Maticsoft.BLL.DataCacheType.CacheType == 0)
            {
                objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            }

            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(CategoryId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        if (Maticsoft.BLL.DataCacheType.CacheType == 1)
                        {
                            RedisBase.Item_Set<Maticsoft.Model.Shop.Products.CategoryInfo>(CacheKey, (Maticsoft.Model.Shop.Products.CategoryInfo)objModel);
                        }
                        if (Maticsoft.BLL.DataCacheType.CacheType == 0)
                        {
                            Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                        }
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.CategoryInfo)objModel;
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
        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.CategoryInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> modelList = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo model;
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
        /// 商品推荐
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.CategoryInfo> NameTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> modelList = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.CategoryInfo();

                    if (dt.Rows[n]["Name"] != null && dt.Rows[n]["Name"].ToString() != "")
                    {
                        model.Name = dt.Rows[n]["Name"].ToString();
                    }
                    modelList.Add(model);
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

        #region NewMethod

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetCategorysByDepth(int depth)
        {
            //ADD Cache
            return GetModelList("Depth = " + depth);
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetCategorysByParentId(int parentCategoryId, int Top = -1)
        {
            //ADD Cache
            DataSet ds = GetList(Top, "ParentCategoryId = " + parentCategoryId, " DisplaySequence");
            return DataTableToList(ds.Tables[0]);
        }

        public string GetFullNameByCache(int categoryId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo category = this.GetModelByCache(categoryId);
            if (category == null) return null;
#if false
            string name = category.Name;
            while ((category != null) && category.ParentCategoryId.HasValue)
            {
                category = this.GetModelByCache(category.ParentCategoryId.Value);
                if (category != null)
                {
                    name = category.Name + " &raquo; " + name;
                }
            }
            return name;
#else
            string[] path = category.Path.Split(
                                new char[] { '|' },
                                StringSplitOptions.RemoveEmptyEntries);
            int count = path.Length;
            string[] categoryName = new string[count];
            for (int i = 0; i < count; i++)
            {
                category = this.GetModelByCache(Globals.SafeInt(path[i], 0));
                if (category == null) continue; // 忽略错误
                categoryName[i] = category.Name;
            }
            return string.Join(" &raquo; ", categoryName);
#endif
        }

        public DataSet GetCategorysByDepthDs(int depth)
        {
            //ADD Cache
            return GetModelDs("Depth = " + depth);
        }

        public DataSet GetCategorysByParentIdDs(int parentCategoryId)
        {
            //ADD Cache
            return GetModelDs("ParentCategoryId = " + parentCategoryId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetModelDs(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return ds;
        }

        #endregion NewMethod

        public bool CreateCategory(Model.Shop.Products.CategoryInfo model)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo parentModel = GetModel(model.ParentCategoryId);
            if (parentModel != null)
            {
                model.Depth = parentModel.Depth + 1;
            }
            else
            {
                model.Depth = 1;
            }
            model.DisplaySequence = GetMaxSeqByCid(model.ParentCategoryId) + 1;

            model.Path = "";
            model.CategoryId = dal.Add(model);
            if (model.CategoryId > 0)
            {
                //更新父分类 是否含有子集
                if (parentModel != null)
                {
                    UpdateHasChild(parentModel.CategoryId);
                    model.Path = parentModel.Path + "|" + model.CategoryId;
                }
                else
                {
                    model.Path = model.CategoryId.ToString();
                }
                return dal.UpdatePath(model);
            }
            return false;
        }

        /// <summary>
        /// 获取排序后的分类列表
        /// </summary>
        public DataSet GetCateList(string strWhere, bool IsOrder)
        {
            return dal.GetList(strWhere, IsOrder);
        }

        /// <summary>
        /// 对分类信息进行排序
        /// </summary>
        public bool SwapCategorySequence(int CategoryId, Model.Shop.Products.SwapSequenceIndex zIndex)
        {
            return dal.SwapCategorySequence(CategoryId, zIndex);
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        public DataSet DeleteCategory(int categoryId, out int Result)
        {
            try
            {
                Maticsoft.Model.Shop.Products.CategoryInfo infoModel = GetModel(categoryId);
                DataSet ds = dal.DeleteCategory(categoryId, out Result);
                if (infoModel != null && infoModel.ParentCategoryId > 0)
                {
                    int count = GetRecordCount("ParentCategoryId =" + infoModel.ParentCategoryId);
                    if (count == 0)
                    {
                        UpdateHasChild(infoModel.ParentCategoryId, 0);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        /// <summary>
        /// 更新分类信息
        /// </summary>
        public bool UpdateCategory(Model.Shop.Products.CategoryInfo model)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo parentModel = GetModel(model.ParentCategoryId);
            string path = model.Path;
            if (parentModel != null)
            {
                model.Depth = parentModel.Depth + 1;
                model.Path = parentModel.Path + "|" + model.CategoryId;
            }
            else
            {
                model.Depth =  1;
                model.Path =  model.CategoryId.ToString();
            }
            if (Update(model))
            {
                //更新父分类 是否含有子集
                if (parentModel != null)
                {
                    UpdateHasChild(parentModel.CategoryId);
                }
                // 需要循环更新该类别下的子分类
                List<Maticsoft.Model.Shop.Products.CategoryInfo> ChildList =
                    GetModelList(" Path Like '" + path + "|%'").OrderBy(c => c.Depth).ToList();
                List<Maticsoft.Model.Shop.Products.CategoryInfo> CateList=new List<Model.Shop.Products.CategoryInfo>();
                CateList.Add(model);
                CateList.AddRange(ChildList);
                foreach (var item in ChildList)
                {
                    Maticsoft.Model.Shop.Products.CategoryInfo parentItemInfo =
                        CateList.FirstOrDefault(c => c.CategoryId == item.ParentCategoryId);
                    if (parentItemInfo != null)
                    {
                        item.Depth = parentItemInfo.Depth + 1;
                        item.Path = parentItemInfo.Path + "|" + item.CategoryId;
                    }
                    else
                    {
                        item.Depth = 1;
                        item.Path = item.CategoryId.ToString();
                    }
                    UpdateDepthAndPath(item.CategoryId, item.Depth, item.Path);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断分类下是否存在商品
        /// </summary>
        public bool IsExistedProduce(int category)
        {
            return dal.IsExistedProduce(category);
        }

        /// <summary>
        /// 转移商品
        /// </summary>
        public bool DisplaceCategory(int FromCategoryId, int ToCategoryId)
        {
            return dal.DisplaceCategory(FromCategoryId, ToCategoryId);
        }
        /// <summary>
        /// 根据path获取NamePath
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetNamePathByPath(string path)
        {
            return dal.GetNamePathByPath(path);
        }
        /// <summary>
        /// 扩展属性Model
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.CategoryInfo GetModelEx(int CategoryId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo model = GetModel(CategoryId);
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
        public Maticsoft.Model.Shop.Products.CategoryInfo GetModelExCache(int CategoryId)
        {
            string CacheKey = "GetModelExCache-" + CategoryId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModelEx(CategoryId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.CategoryInfo)objModel;
        }

        /// <summary>
        /// 产品大分类列表信息
        /// </summary>
        /// <returns></returns>
        public List<Model.Shop.Products.CategoryInfo> MainCategoryList(int? parentId)
        {
            string strWhere = string.Format(" ParentCategoryId={0} ORDER BY DisplaySequence ASC",
                                            parentId.HasValue ? parentId.Value : 0);
            DataSet ds = dal.GetList(strWhere);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        public List<Model.Shop.Products.CategoryInfo> GetCategoryListByPath(string path)
        {
            string CacheKey = "GetCategoryListByPath-" + path;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    DataSet ds = dal.GetCategoryListByPath(path);
                    if (ds != null)
                    {
                        objModel = DataTableToList(ds.Tables[0]);
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                            TimeSpan.Zero);
                    }
                }
                catch
                {

                }
            }
            return (List<Model.Shop.Products.CategoryInfo>)objModel;
        }


        //获取产品分类名称 （多个分类以“，”分隔返回）
        public string GetNameByPid(long productId)
        {
            DataSet ds = dal.GetNameByPid(productId);
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = NameTableToList(ds.Tables[0]);
            if (categoryInfos != null && categoryInfos.Count > 0)
            {
                return String.Join(",", categoryInfos.Select(c => c.Name));
            }
            return "";
            //Maticsoft.BLL.Shop.Products.ProductCategories cateBll=new ProductCategories();
            //List<Maticsoft.Model.Shop.Products.ProductCategories> cateProductList=cateBll.GetModelList(" ProductId="+productId)
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<Maticsoft.Model.Shop.Products.CategoryInfo> GetAllCateList()
        {
            string CacheKey = "GetAllCateList-CateList";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.BLL.Shop.Products.CategoryInfo categoryBll = new CategoryInfo();
                    DataSet ds = categoryBll.GetList(-1, "", " DisplaySequence");
                    objModel = categoryBll.DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.Shop.Products.CategoryInfo>)objModel;
        }

        public int GetMaxSeqByCid(int parentId)
        {
            return dal.GetMaxSeqByCid(parentId);
        }

        public int GetDepthByCid(int parentId)
        {
            return dal.GetDepthByCid(parentId);
        }

        public bool UpdateSeqByCid(int Seq,int Cid)
        {
            return dal.UpdateSeqByCid(Seq,Cid);
        }

        public bool UpdateDepthAndPath(int Cid, int Depth, string Path)
        {
            return dal.UpdateDepthAndPath(Cid, Depth, Path);
        }
        //添加编辑分类时 更新分类的HasChildren 字段
        public bool UpdateHasChild(int cid,int hasChild=1)
        {
            return dal.UpdateHasChild(cid, hasChild);
        }

        /// <summary>
        /// 同级下是否存在同名
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExisted(int parentId, string name,int categoryId=0)
        {
            return dal.IsExisted(parentId, name, categoryId);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Shop.Products.CategoryInfo> GetModelList()
        {
            DataSet ds = dal.GetList(-1, "  Depth = 1 ", " DisplaySequence ");
            return DataTableToList(ds.Tables[0]);
        }


        public string GetClassUrl(int cateId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo model = GetModel(cateId);
            if (model == null)
            {
                return "";
            }
            int rule = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("Shop_Static_TypeRule");
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
            return model.SeoUrl.Replace("|", "/");
        }

        /// <summary>
        /// 返回拼音URL
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetPingYinUrl(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                return "";
            }
            var path_array = path.Split(',');
            string Url = "";
            int i = 0;
            foreach (var item in path_array)
            {
                Maticsoft.Model.Shop.Products.CategoryInfo model = GetModelByCache(Common.Globals.SafeInt(item,0));
                if (model == null)
                {
                    return "";
                }
                if (i == 0)
                {
                    Url = Maticsoft.Common.PinyinHelper.GetPinyin(model.Name).ToLower();
                }
                else
                {
                    Url = Url + "/" + (Maticsoft.Common.PinyinHelper.GetPinyin(model.Name).ToLower());
                }
            }
            return Url;
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
                Maticsoft.Model.Shop.Products.CategoryInfo model = GetModelByCache(Common.Globals.SafeInt(item, 0));
                if (model == null)
                    return "";
                if (i == 0)
                {
                    Url = String.IsNullOrWhiteSpace(model.SeoUrl) ? model.CategoryId.ToString() : model.SeoUrl;
                }
                else
                {
                    Url = Url + "/" + (String.IsNullOrWhiteSpace(model.SeoUrl) ? model.CategoryId.ToString() : model.SeoUrl);
                }
            }
            return Url;
        }

        public List<Maticsoft.Model.Shop.Products.CategoryInfo> GetGroupList()
        {
            DataSet ds = dal.GetGroupCate();
            return DataTableToList(ds.Tables[0]);
        }
      

    }
}