/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductAttributes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:25
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
    /// ProductAttribute
    /// </summary>
    public partial class ProductAttribute
    {
        private readonly IProductAttribute dal = DAShopProducts.CreateProductAttribute();
        public ProductAttribute()
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
        public bool Exists(long ProductId, long AttributeId, int ValueId)
        {
            return dal.Exists(ProductId, AttributeId, ValueId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Products.ProductAttribute model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductAttribute model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ProductId, long AttributeId, int ValueId)
        {

            return dal.Delete(ProductId, AttributeId, ValueId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductAttribute GetModel(long ProductId, long AttributeId, int ValueId)
        {

            return dal.GetModel(ProductId, AttributeId, ValueId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductAttribute GetModelByCache(long ProductId, long AttributeId, int ValueId)
        {

            string CacheKey = "ProductAttributesModel-" + ProductId + AttributeId + ValueId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ProductId, AttributeId, ValueId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductAttribute)objModel;
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
        public List<Maticsoft.Model.Shop.Products.ProductAttribute> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductAttribute> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductAttribute> modelList = new List<Maticsoft.Model.Shop.Products.ProductAttribute>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductAttribute model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductAttribute();
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["AttributeId"] != null && dt.Rows[n]["AttributeId"].ToString() != "")
                    {
                        model.AttributeId = long.Parse(dt.Rows[n]["AttributeId"].ToString());
                    }
                    if (dt.Rows[n]["ValueId"] != null && dt.Rows[n]["ValueId"].ToString() != "")
                    {
                        model.ValueId = int.Parse(dt.Rows[n]["ValueId"].ToString());
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

        public bool Exists(long? ProductId, long? AttributeId, long? ValueId)
        {
            return dal.Exists(ProductId, AttributeId, ValueId);
        }
    }
}

