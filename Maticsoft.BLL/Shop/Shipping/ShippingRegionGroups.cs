/**
* ShippingRegionGroups.cs
*
* 功 能： N/A
* 类 名： ShippingRegionGroups
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/8 18:17:33   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.DALFactory;

namespace Maticsoft.BLL.Shop.Shipping
{
    /// <summary>
    /// ShippingRegionGroups
    /// </summary>
    public partial class ShippingRegionGroups
    {
        private readonly IDAL.Shop.Shipping.IShippingRegionGroups dal = DAShopShipping.CreateShippingRegionGroups();
        public ShippingRegionGroups()
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
        public bool Exists(int GroupId)
        {
            return dal.Exists(GroupId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Shop.Shipping.ShippingRegionGroups model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Shop.Shipping.ShippingRegionGroups model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GroupId)
        {

            return dal.Delete(GroupId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string GroupIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(GroupIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Shop.Shipping.ShippingRegionGroups GetModel(int GroupId)
        {

            return dal.GetModel(GroupId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.Shop.Shipping.ShippingRegionGroups GetModelByCache(int GroupId)
        {

            string CacheKey = "ShippingRegionGroupsModel-" + GroupId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GroupId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.Shop.Shipping.ShippingRegionGroups)objModel;
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
        public List<Model.Shop.Shipping.ShippingRegionGroups> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Shop.Shipping.ShippingRegionGroups> DataTableToList(DataTable dt)
        {
            List<Model.Shop.Shipping.ShippingRegionGroups> modelList = new List<Model.Shop.Shipping.ShippingRegionGroups>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Shop.Shipping.ShippingRegionGroups model;
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
        /// 清空配送地区价格
        /// </summary>
        public bool ClearShippingRegionGroups(int modelId)
        {
            return dal.ClearShippingRegionGroups(modelId);
        }

        /// <summary>
        /// 保存配送地区价格
        /// </summary>
        public bool SaveShippingRegionGroups(Model.Shop.Shipping.ShippingType shippingType,
            List<Model.Shop.Shipping.ShippingRegionGroups> list)
        {
            ClearShippingRegionGroups(shippingType.ModeId);
            dal.SaveShippingRegionGroups(list);
            return false;
        }

        /// <summary>
        /// 获取配送地区价格
        /// </summary>
        public List<Model.Shop.Shipping.ShippingRegionGroups> GetShippingRegionGroups(int modeId)
        {
            DataSet ds = dal.GetShippingRegionGroups(modeId);
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 根据顶级RegionId 获取配送地区价格
        /// </summary>
        public Model.Shop.Shipping.ShippingRegionGroups GetShippingRegion(int modeId, int topRegionId)
        {
            return dal.GetShippingRegion(modeId, topRegionId);
        }
        #endregion  ExtensionMethod
    }
}

