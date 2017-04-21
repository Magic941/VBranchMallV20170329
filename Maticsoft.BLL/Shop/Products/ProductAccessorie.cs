/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductAccessories.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:24
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
    /// ProductAccessories
    /// </summary>
    public partial class ProductAccessorie
    {
        private readonly IProductAccessorie dal = DAShopProducts.CreateProductAccessorie();
        public ProductAccessorie()
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
        public bool Exists(long ProductId, int AccessoriesId)
        {
            return dal.Exists(ProductId, AccessoriesId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.ProductAccessorie model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductAccessorie model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AccessoriesId)
        {

            return dal.Delete(AccessoriesId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ProductId, int AccessoriesId)
        {

            return dal.Delete(ProductId, AccessoriesId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string AccessoriesIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(AccessoriesIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductAccessorie GetModel(int AccessoriesId)
        {

            return dal.GetModel(AccessoriesId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductAccessorie GetModelByCache(int AccessoriesId)
        {
            string CacheKey = "ProductAccessoriesModel-" + AccessoriesId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AccessoriesId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductAccessorie)objModel;
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
        public List<Maticsoft.Model.Shop.Products.ProductAccessorie> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductAccessorie> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductAccessorie> modelList = new List<Maticsoft.Model.Shop.Products.ProductAccessorie>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductAccessorie model;
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


        #region  ExtensionMethod
        /// <summary>
        /// 根据商品id获取该商品的组合列表
        /// </summary>
        /// <param name="productId">商品id</param>
        /// <param name="type">组合类型  1 配件 2优惠组合（套装）</param>
        /// <returns></returns>
        public List<Model.Shop.Products.ProductAccessorie> GetModelList(long productId, int type)
        {
            DataSet ds = dal.GetList(string.Format(" ProductId={0} and type={1} ", productId, type));
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="accessoriesId">组合id</param>
        /// <param name="productid">商品id</param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.ProductAccessorie GetModel(int accessoriesId,int productid)
        {
            return dal.GetModel(accessoriesId, productid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="accessoriesId">组合id</param>
        /// <param name="productid">商品id</param>
        /// <returns></returns>
        public Model.Shop.Products.ProductAccessorie GetModelByCache(int accessoriesId,long  productid)
        {
            string CacheKey = string.Format("ProductAccessoriesModel-AccessoriesId{0}_productid{1}", accessoriesId, productid);
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(accessoriesId,productid);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductAccessorie)objModel;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.Shop.Products.ProductAccessorie model,string sku)
        {
            return dal.Add(model,sku);
        }

        /// <summary>
        /// 删除一条数据同时删除该组合下的sku
        /// </summary>
        public bool DeleteEx(int AccessoriesId)
        {
            return dal.DeleteEx(AccessoriesId);
        }

        /// <summary>
        /// 批量删除数据 同时删除该组合下的sku
        /// </summary>
        public bool DeleteListEx(string AccessoriesIdlist)
        {
            return dal.DeleteListEx(AccessoriesIdlist);
        }

        #endregion


       
    }
}

