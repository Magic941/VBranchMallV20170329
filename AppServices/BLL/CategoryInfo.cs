using Maticsoft.Common;
using Maticsoft.IDAL.Shop.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppServices.BLL
{
    public class CategoryInfo
    {
        private readonly ICategoryInfo dal = Maticsoft.DALFactory.DAShopProducts.CreateCategoryInfo();
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<AppServices.Models.CategoryInfo> GetAllCateList()
        {
            string CacheKey = "GetAllCateList-CateList";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.BLL.Shop.Products.CategoryInfo categoryBll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
                    DataSet ds = categoryBll.GetList(-1, "", " DisplaySequence");
                    
                    objModel = DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<AppServices.Models.CategoryInfo>)objModel;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<AppServices.Models.CategoryInfo> DataTableToList(DataTable dt)
        {
            List<AppServices.Models.CategoryInfo> modelList = new List<AppServices.Models.CategoryInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                AppServices.Models.CategoryInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new AppServices.Models.CategoryInfo();


                    if (dt.Rows[n]["CategoryId"] != null && dt.Rows[n]["CategoryId"].ToString() != "")
                    {
                        model.CategoryId = int.Parse(dt.Rows[n]["CategoryId"].ToString());
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["Name"] != null)
                    {
                        model.Name = dt.Rows[n]["Name"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Title"] != null)
                    {
                        model.Meta_Title = dt.Rows[n]["Meta_Title"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Description"] != null)
                    {
                        model.Meta_Description = dt.Rows[n]["Meta_Description"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Keywords"] != null)
                    {
                        model.Meta_Keywords = dt.Rows[n]["Meta_Keywords"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null)
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["ParentCategoryId"] != null && dt.Rows[n]["ParentCategoryId"].ToString() != "")
                    {
                        model.ParentCategoryId = int.Parse(dt.Rows[n]["ParentCategoryId"].ToString());
                    }
                    if (dt.Rows[n]["Depth"] != null && dt.Rows[n]["Depth"].ToString() != "")
                    {
                        model.Depth = int.Parse(dt.Rows[n]["Depth"].ToString());
                    }
                    if (dt.Rows[n]["Path"] != null)
                    {
                        model.Path = dt.Rows[n]["Path"].ToString();
                    }
                    if (dt.Rows[n]["RewriteName"] != null)
                    {
                        model.RewriteName = dt.Rows[n]["RewriteName"].ToString();
                    }
                    if (dt.Rows[n]["SKUPrefix"] != null)
                    {
                        model.SKUPrefix = dt.Rows[n]["SKUPrefix"].ToString();
                    }
                    if (dt.Rows[n]["AssociatedProductType"] != null && dt.Rows[n]["AssociatedProductType"].ToString() != "")
                    {
                        model.AssociatedProductType = int.Parse(dt.Rows[n]["AssociatedProductType"].ToString());
                    }
                    if (dt.Rows[n]["ImageUrl"] != null)
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["Notes1"] != null)
                    {
                        model.Notes1 = dt.Rows[n]["Notes1"].ToString();
                    }
                    if (dt.Rows[n]["Notes2"] != null)
                    {
                        model.Notes2 = dt.Rows[n]["Notes2"].ToString();
                    }
                    if (dt.Rows[n]["Notes3"] != null)
                    {
                        model.Notes3 = dt.Rows[n]["Notes3"].ToString();
                    }
                    if (dt.Rows[n]["Notes4"] != null)
                    {
                        model.Notes4 = dt.Rows[n]["Notes4"].ToString();
                    }
                    if (dt.Rows[n]["Notes5"] != null)
                    {
                        model.Notes5 = dt.Rows[n]["Notes5"].ToString();
                    }
                    if (dt.Rows[n]["Theme"] != null)
                    {
                        model.Theme = dt.Rows[n]["Theme"].ToString();
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
                    if (dt.Rows[n]["SeoUrl"] != null)
                    {
                        model.SeoUrl = dt.Rows[n]["SeoUrl"].ToString();
                    }
                    if (dt.Rows[n]["SeoImageAlt"] != null)
                    {
                        model.SeoImageAlt = dt.Rows[n]["SeoImageAlt"].ToString();
                    }
                    if (dt.Rows[n]["SeoImageTitle"] != null)
                    {
                        model.SeoImageTitle = dt.Rows[n]["SeoImageTitle"].ToString();
                    }
                    model.ProductCount = 0;
                    if (dt.Rows[n]["ProductCount"] != null && dt.Rows[n]["ProductCount"].ToString() != "")
                    {
                        model.ProductCount = int.Parse(dt.Rows[n]["ProductCount"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AppServices.Models.CategoryInfo GetModel(int CategoryId)
        {
            AppServices.Models.CategoryInfo category = null;
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                string CacheKey = "CategoryInfoModel-" + CategoryId;
                category = ServiceStack.RedisCache.RedisBase.Item_Get<AppServices.Models.CategoryInfo>(CacheKey);
            }

            if (category == null)
            {
                //category = dal.GetModel(CategoryId);
            }

            return category;
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public AppServices.Models.CategoryInfo GetModelByCache(int CategoryId)
        {
            object objModel = null;
            string CacheKey = "CategoryInfoModel-" + CategoryId;

            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                AppServices.Models.CategoryInfo category = new AppServices.Models.CategoryInfo();
                category = ServiceStack.RedisCache.RedisBase.Item_Get<AppServices.Models.CategoryInfo>(CacheKey);
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
                        int ModelCache = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        if (Maticsoft.BLL.DataCacheType.CacheType == 1)
                        {
                            ServiceStack.RedisCache.RedisBase.Item_Set<AppServices.Models.CategoryInfo>(CacheKey, (AppServices.Models.CategoryInfo)objModel);
                        }
                        if (Maticsoft.BLL.DataCacheType.CacheType == 0)
                        {
                            Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                        }
                    }
                }
                catch { }
            }
            return (AppServices.Models.CategoryInfo)objModel;
        }
       
    }
}