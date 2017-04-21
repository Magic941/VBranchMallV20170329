/**
* SuppProductStatModes.cs
*
* 功 能： N/A
* 类 名： SuppProductStatModes
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/27 18:11:59   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Supplier;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Supplier;
namespace Maticsoft.BLL.Shop.Supplier
{
    /// <summary>
    /// SuppProductStatModes
    /// </summary>
    public partial class SuppProductStatModes
    {
        private readonly ISuppProductStatModes dal = DAShopSupplier.CreateSuppProductStatModes();
        public SuppProductStatModes()
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
        public bool Exists(int StationId)
        {
            return dal.Exists(StationId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Supplier.SuppProductStatModes model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Supplier.SuppProductStatModes model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int StationId)
        {

            return dal.Delete(StationId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string StationIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(StationIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Supplier.SuppProductStatModes GetModel(int StationId)
        {

            return dal.GetModel(StationId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Supplier.SuppProductStatModes GetModelByCache(int StationId)
        {

            string CacheKey = "SuppProductStatModesModel-" + StationId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(StationId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Supplier.SuppProductStatModes)objModel;
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
        public List<Maticsoft.Model.Shop.Supplier.SuppProductStatModes> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Supplier.SuppProductStatModes> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Supplier.SuppProductStatModes> modelList = new List<Maticsoft.Model.Shop.Supplier.SuppProductStatModes>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Supplier.SuppProductStatModes model;
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
        /// 根据type获得数据列表
        /// </summary>
        public DataSet GetListByType(int supplierId, string strType)
        {
            return dal.GetListByType(supplierId, strType);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int supplierId, long productId, int type)
        {
            return dal.Exists(supplierId, productId, type);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int supplierId, long productId, int type)
        {
            return dal.Delete(supplierId, productId, type);
        }

        /// <summary>
        /// 清空type下所有商品
        /// </summary>
        public bool DeleteByType(int supplierId, int type, int categoryId)
        {
            return dal.DeleteByType(supplierId, type, categoryId);
        }

        public DataSet GetStationMode(int supplierId, int modeType, int categoryId, string pName)
        {
            return dal.GetStationMode(supplierId, modeType, categoryId, pName);
        }
        
        /// <summary>
        /// 商品推荐信息列表
        /// </summary>
        public List<Model.Shop.Products.ProductInfo> GetProductNoRecList(int supplierId, int categoryId, string pName, int modeType, int startIndex, int endIndex)
        {
            BLL.Shop.Products.ProductInfo productManage = new Products.ProductInfo();
            DataSet ds = dal.GetProductNoRecList(supplierId,categoryId, pName, modeType, startIndex, endIndex);
            if (Common.DataSetTools.DataSetIsNull(ds)) return null;
            return productManage.DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 商品推荐信息Count
        /// </summary>
        public int GetProductNoRecCount(int supplierId, int categoryId, string pName, int modeType)
        {
            return dal.GetProductNoRecCount(supplierId,categoryId, pName, modeType);
        }
        #endregion  ExtensionMethod
    }
}

