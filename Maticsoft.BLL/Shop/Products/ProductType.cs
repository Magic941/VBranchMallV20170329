/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductTypes.cs
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
using System.Collections;
namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// ProductType
    /// </summary>
    public partial class ProductType
    {
        private readonly IProductType dal = DAShopProducts.CreateProductType();
        public ProductType()
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
        public bool Exists(int TypeId)
        {
            return dal.Exists(TypeId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.ProductType model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TypeId)
        {

            return dal.Delete(TypeId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string TypeIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(TypeIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductType GetModel(int TypeId)
        {

            return dal.GetModel(TypeId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductType GetModelByCache(int TypeId)
        {

            string CacheKey = "ProductTypesModel-" + TypeId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(TypeId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductType)objModel;
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
        public List<Maticsoft.Model.Shop.Products.ProductType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductType> modelList = new List<Maticsoft.Model.Shop.Products.ProductType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductType();
                    if (dt.Rows[n]["TypeId"] != null && dt.Rows[n]["TypeId"].ToString() != "")
                    {
                        model.TypeId = int.Parse(dt.Rows[n]["TypeId"].ToString());
                    }
                    if (dt.Rows[n]["TypeName"] != null && dt.Rows[n]["TypeName"].ToString() != "")
                    {
                        model.TypeName = dt.Rows[n]["TypeName"].ToString();
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
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method

        public List<Maticsoft.Model.Shop.Products.ProductType> GetProductTypes()
        {
            return dal.GetProductTypes();
        }

        public bool ProductTypeManage(Model.Shop.Products.ProductType model, Model.Shop.Products.DataProviderAction Action, out int Typeid)
        {
            return dal.ProductTypeManage(model, Action, out Typeid);
        }

        public bool DeleteManage(int? TypeId, long? AttributeId, long? ValueId)
        {
            return dal.DeleteManage(TypeId, AttributeId, ValueId);
        }


        public bool SwapSeqManage(int? TypeId, long? AttributeId, long? ValueId, Model.Shop.Products.SwapSequenceIndex zIndex, bool UsageMode)
        {
            return dal.SwapSeqManage(TypeId, AttributeId, ValueId, zIndex, UsageMode);
        }
    }
}

