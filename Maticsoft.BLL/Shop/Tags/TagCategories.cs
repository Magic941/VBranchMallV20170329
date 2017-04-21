/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：TagCategories.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/12/14 10:05:39
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
using Maticsoft.IDAL.Shop.Tags;
using System.Text;

namespace Maticsoft.BLL.Shop.Tags
{
    /// <summary>
    /// TagCategories
    /// </summary>
    public partial class TagCategories
    {
        private readonly ITagCategories dal = DAShopProducts.CreateTagCategories();

        public TagCategories()
        { }

        #region Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Tags.TagCategories model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Tags.TagCategories model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            return dal.Delete(ID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Tags.TagCategories GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Tags.TagCategories GetModelByCache(int ID)
        {
            string CacheKey = "TagCategoriesModel-" + ID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Tags.TagCategories)objModel;
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
        public List<Maticsoft.Model.Shop.Tags.TagCategories> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Tags.TagCategories> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Tags.TagCategories> modelList = new List<Maticsoft.Model.Shop.Tags.TagCategories>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Tags.TagCategories model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Tags.TagCategories();
                    if (dt.Rows[n]["ID"] != null && dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["CategoryName"] != null && dt.Rows[n]["CategoryName"].ToString() != "")
                    {
                        model.CategoryName = dt.Rows[n]["CategoryName"].ToString();
                    }
                    if (dt.Rows[n]["ParentCategoryId"] != null && dt.Rows[n]["ParentCategoryId"].ToString() != "")
                    {
                        model.ParentCategoryId = int.Parse(dt.Rows[n]["ParentCategoryId"].ToString());
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["Depth"] != null && dt.Rows[n]["Depth"].ToString() != "")
                    {
                        model.Depth = int.Parse(dt.Rows[n]["Depth"].ToString());
                    }
                    if (dt.Rows[n]["Path"] != null && dt.Rows[n]["Path"].ToString() != "")
                    {
                        model.Path = dt.Rows[n]["Path"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Title"] != null && dt.Rows[n]["Meta_Title"].ToString() != "")
                    {
                        model.Meta_Title = dt.Rows[n]["Meta_Title"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Description"] != null && dt.Rows[n]["Meta_Description"].ToString() != "")
                    {
                        model.Meta_Description = dt.Rows[n]["Meta_Description"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Keywords"] != null && dt.Rows[n]["Meta_Keywords"].ToString() != "")
                    {
                        model.Meta_Keywords = dt.Rows[n]["Meta_Keywords"].ToString();
                    }
                    if (dt.Rows[n]["HasChildren"] != null && dt.Rows[n]["HasChildren"].ToString() != "")
                    {
                        if ((dt.Rows[n]["HasChildren"].ToString() == "1") || (dt.Rows[n]["HasChildren"].ToString().ToLower() == "true"))
                        {
                            model.HasChildren = true;
                        }
                        else
                        {
                            model.HasChildren = false;
                        }
                    }
                    if (dt.Rows[n]["Status"] != null && dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["Remark"] != null && dt.Rows[n]["Remark"].ToString() != "")
                    {
                        model.Remark = dt.Rows[n]["Remark"].ToString();
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
        #endregion Method

        #region ExtensionMethod

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSearchList(string Keywords, int Cid = -1)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Keywords))
            {
                sb.Append(" CategoryName like '" + Keywords + "'");
            }
            if (Cid >= 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" and ");
                }
                sb.Append("  Depth>=0");
            }

            return dal.GetList(0, sb.ToString(), "");
        }

        public bool CreateCategory(Model.Shop.Tags.TagCategories model)
        {
            return dal.CreateCategory(model);
        }

        /// <summary>
        /// 对分类信息进行排序
        /// </summary>
        public bool TagCategoriesSequence(int ID, Model.Shop.Tags.SequenceIndex Index)
        {
            return dal.TagCategoriesSequence(ID, Index);
        }

        /// <summary>
        /// 删除分类信息
        /// </summary>
        public DataSet DeleteTagCategories(int ID, out int Result)
        {
            return dal.DeleteTagCategories(ID, out Result);
        }

        public List<Maticsoft.Model.Shop.Tags.TagCategories> GetCategorysByParentId(int parentCategoryId)
        {
            //ADD Cache
            return GetModelList("ParentCategoryId = " + parentCategoryId);
        }

        public string GetFullNameByCache(int categoryId)
        {
            Maticsoft.Model.Shop.Tags.TagCategories category = this.GetModelByCache(categoryId);
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
                categoryName[i] = category.CategoryName;
            }
            return string.Join(" &raquo; ", categoryName);
#endif
        }
        #endregion
    }
}