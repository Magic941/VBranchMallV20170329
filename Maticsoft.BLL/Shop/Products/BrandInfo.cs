/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Brands.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/12 10:02:41
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// BrandInfo
    /// </summary>
    public partial class BrandInfo
    {
        private readonly IBrandInfo dal = DAShopProducts.CreateBrandInfo();
        public BrandInfo()
        { }
        #region  Method

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
        public bool Exists(int BrandId)
        {
            return dal.Exists(BrandId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.BrandInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.BrandInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int BrandId)
        {

            return dal.Delete(BrandId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string BrandIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(BrandIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.BrandInfo GetModel(int BrandId)
        {

            return dal.GetModel(BrandId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.BrandInfo GetModelByCache(int BrandId)
        {

            string CacheKey = "BrandsModel-" + BrandId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(BrandId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.BrandInfo)objModel;
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
        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.BrandInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> modelList = new List<Maticsoft.Model.Shop.Products.BrandInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.BrandInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.BrandInfo();
                    if (dt.Rows[n]["BrandId"] != null && dt.Rows[n]["BrandId"].ToString() != "")
                    {
                        model.BrandId = int.Parse(dt.Rows[n]["BrandId"].ToString());
                    }
                    if (dt.Rows[n]["BrandName"] != null && dt.Rows[n]["BrandName"].ToString() != "")
                    {
                        model.BrandName = dt.Rows[n]["BrandName"].ToString();
                    }
                    if (dt.Rows[n]["BrandSpell"] != null && dt.Rows[n]["BrandSpell"].ToString() != "")
                    {
                        model.BrandSpell = dt.Rows[n]["BrandSpell"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Description"] != null && dt.Rows[n]["Meta_Description"].ToString() != "")
                    {
                        model.Meta_Description = dt.Rows[n]["Meta_Description"].ToString();
                    }
                    if (dt.Rows[n]["Meta_Keywords"] != null && dt.Rows[n]["Meta_Keywords"].ToString() != "")
                    {
                        model.Meta_Keywords = dt.Rows[n]["Meta_Keywords"].ToString();
                    }
                    if (dt.Rows[n]["Logo"] != null && dt.Rows[n]["Logo"].ToString() != "")
                    {
                        model.Logo = dt.Rows[n]["Logo"].ToString();
                    }
                    if (dt.Rows[n]["CompanyUrl"] != null && dt.Rows[n]["CompanyUrl"].ToString() != "")
                    {
                        model.CompanyUrl = dt.Rows[n]["CompanyUrl"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["Theme"] != null && dt.Rows[n]["Theme"].ToString() != "")
                    {
                        model.Theme = dt.Rows[n]["Theme"].ToString();
                    }
                    if (dt.Columns.Contains("BrandsThumbs"))
                    {
                        if (dt.Rows[n]["BrandsThumbs"] != null && dt.Rows[n]["BrandsThumbs"].ToString() != "")
                        {
                            model.BrandsThumbs = dt.Rows[n]["BrandsThumbs"].ToString();
                        }
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

        #endregion  Method

        #region NewMethod
        /// <summary>
        /// 得到最大顺序
        /// </summary>
        public int GetMaxDisplaySequence()
        {
            return dal.GetMaxDisplaySequence();
        }
        public bool CreateBrandsAndTypes(Model.Shop.Products.BrandInfo model, Model.Shop.Products.DataProviderAction action)
        {
            return dal.CreateBrandsAndTypes(model, action);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetModelListByProductTypeId(int ProductTypeId,int top=-1)
        {
            DataSet ds = dal.GetListByProductTypeId(ProductTypeId, top);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetListByProductTypeId(out int rowCount, out int pageCount, int ProductTypeId, int PageIndex, int PageSize, int action)
        {
            DataSet ds = dal.GetListByProductTypeId(out rowCount, out pageCount, ProductTypeId, PageIndex, PageSize, action);
            return DataTableToList(ds.Tables[0]);
        }

        public Model.Shop.Products.BrandInfo GetRelatedProduct(int brandsId)
        {
            return dal.GetRelatedProduct(brandsId);
        }

        public Model.Shop.Products.BrandInfo GetRelatedProduct(int? brandsId, int? ProductTypeId)
        {
            return dal.GetRelatedProduct(brandsId, ProductTypeId);
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetBrands()
        {
            DataSet ds = dal.GetList("");
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetBrandsModelListByCateId(int? cateId)
        {
            DataSet ds = dal.GetBrandsListByCateId(cateId);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetBrandList(string strWhere,int Top=-1)
        {
            DataSet ds = dal.GetList(Top, strWhere, " DisplaySequence");
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 根据分类获取品牌（是否包括子类）
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.BrandInfo> GetBrandsByCateId(int cateId,bool IsChild=false,int Top=-1)
        {
            DataSet ds = dal.GetBrandsByCateId(cateId, IsChild, Top);
            return DataTableToList(ds.Tables[0]);
        }
        #endregion
    }
}

