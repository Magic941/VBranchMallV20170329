/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：RelatedProducts.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:31
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
    /// RelatedProduct
    /// </summary>
    public partial class RelatedProduct
    {
        private readonly IRelatedProduct dal = DAShopProducts.CreateRelatedProduct();
        public RelatedProduct()
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
        public bool Exists(int RelatedId, long ProductId)
        {
            return dal.Exists(RelatedId, ProductId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.RelatedProduct model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.RelatedProduct model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int RelatedId, long ProductId)
        {

            return dal.Delete(RelatedId, ProductId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.RelatedProduct GetModel(int RelatedId, long ProductId)
        {

            return dal.GetModel(RelatedId, ProductId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.RelatedProduct GetModelByCache(int RelatedId, long ProductId)
        {

            string CacheKey = "RelatedProductsModel-" + RelatedId + ProductId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RelatedId, ProductId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.RelatedProduct)objModel;
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
        public List<Maticsoft.Model.Shop.Products.RelatedProduct> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.RelatedProduct> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.RelatedProduct> modelList = new List<Maticsoft.Model.Shop.Products.RelatedProduct>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.RelatedProduct model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.RelatedProduct();
                    if (dt.Rows[n]["RelatedId"] != null && dt.Rows[n]["RelatedId"].ToString() != "")
                    {
                        model.RelatedId = long.Parse(dt.Rows[n]["RelatedId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
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

        public List<Model.Shop.Products.RelatedProduct> GetModelList(long productId)
        {
            DataSet ds = dal.GetList(string.Format(" ProductId={0}", productId));
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

         public DataSet IsDoubleRelated(long productId)
         {
             return dal.IsDoubleRelated(productId);
         }
    }
}

