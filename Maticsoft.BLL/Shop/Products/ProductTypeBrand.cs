/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductTypeBrands.cs
// 文件功能描述：
//
// 创建标识： [Ben]  2012/06/11 20:36:30
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
using Maticsoft.IDAL.Shop.Products;

namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// ProductTypeBrand
    /// </summary>
    public partial class ProductTypeBrand
    {
        private readonly IProductTypeBrand dal = DAShopProducts.CreateProductTypeBrand();

        public ProductTypeBrand()
        { }

        #region Method

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
        public bool Exists(int ProductTypeId, int BrandId)
        {
            return dal.Exists(ProductTypeId, BrandId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.ProductTypeBrand model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductTypeBrand model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ProductTypeId, int BrandId)
        {
            return dal.Delete(ProductTypeId, BrandId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductTypeBrand GetModel(int ProductTypeId, int BrandId)
        {
            return dal.GetModel(ProductTypeId, BrandId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductTypeBrand GetModelByCache(int ProductTypeId, int BrandId)
        {
            string CacheKey = "ProductTypeBrandsModel-" + ProductTypeId + BrandId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ProductTypeId, BrandId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductTypeBrand)objModel;
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
        public List<Maticsoft.Model.Shop.Products.ProductTypeBrand> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductTypeBrand> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductTypeBrand> modelList = new List<Maticsoft.Model.Shop.Products.ProductTypeBrand>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductTypeBrand model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductTypeBrand();
                    if (dt.Rows[n]["ProductTypeId"] != null && dt.Rows[n]["ProductTypeId"].ToString() != "")
                    {
                        model.ProductTypeId = int.Parse(dt.Rows[n]["ProductTypeId"].ToString());
                    }
                    if (dt.Rows[n]["BrandId"] != null && dt.Rows[n]["BrandId"].ToString() != "")
                    {
                        model.BrandId = int.Parse(dt.Rows[n]["BrandId"].ToString());
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

        #endregion Method

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int? ProductTypeId, int? BrandId)
        {
            return dal.Delete(ProductTypeId, BrandId);
        }

        public bool ExistsBrands(int BrandId)
        {
            return dal.ExistsBrands(BrandId);
        }
    }
}