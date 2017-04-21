/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：AccessoriesValues.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:21
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
    /// AccessoriesValue
    /// </summary>
    public partial class AccessoriesValue
    {
        private readonly IAccessoriesValue dal = DAShopProducts.CreateAccessoriesValue();
        public AccessoriesValue()
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
        public bool Exists(int AccessoriesId, int AccessoriesValueId)
        {
            return dal.Exists(AccessoriesId, AccessoriesValueId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.AccessoriesValue model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.AccessoriesValue model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AccessoriesValueId)
        {

            return dal.Delete(AccessoriesValueId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AccessoriesId, int AccessoriesValueId)
        {

            return dal.Delete(AccessoriesId, AccessoriesValueId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string AccessoriesValueIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(AccessoriesValueIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.AccessoriesValue GetModel(int AccessoriesValueId)
        {

            return dal.GetModel(AccessoriesValueId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.AccessoriesValue GetModelByCache(int AccessoriesValueId)
        {

            string CacheKey = "AccessoriesValuesModel-" + AccessoriesValueId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AccessoriesValueId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.AccessoriesValue)objModel;
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
        public List<Maticsoft.Model.Shop.Products.AccessoriesValue> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.AccessoriesValue> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.AccessoriesValue> modelList = new List<Maticsoft.Model.Shop.Products.AccessoriesValue>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.AccessoriesValue model;
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
        /// 根据商品组合id获取该组中的商品
        /// </summary>
        public List<Model.Shop.Products.AccessoriesValue> GetListByAccessoriesId(int accessoriesId)
        {
            DataSet ds = dal.GetList(string.Format(" AccessoriesId = {0} ", accessoriesId));
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
        /// 是否存在该记录
        /// </summary>
        /// <param name="AccessoriesId">组合id</param>
        /// <param name="SKU">SKU</param>
        /// <returns></returns>
        public bool Exists(int AccessoriesId, string SKU)
        {
            return dal.Exists(AccessoriesId, SKU);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        ///<param name="AccessoriesId">组合id</param>
        /// <param name="SKU">SKU</param>
        /// <returns></returns>
        public bool Delete(int AccessoriesId, string SKU)
        {
            return dal.Delete(AccessoriesId, SKU);
        }

        #endregion
    }
}

