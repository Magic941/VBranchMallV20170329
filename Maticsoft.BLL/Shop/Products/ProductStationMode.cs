/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductStationModes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:28
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
using System.Text;
namespace Maticsoft.BLL.Shop.Products
{
    /// <summary>
    /// ProductStationMode
    /// </summary>
    public partial class ProductStationMode
    {
        private readonly IProductStationMode dal = DAShopProducts.CreateProductStationMode();
        public ProductStationMode()
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
        public bool Exists(int StationId)
        {
            return dal.Exists(StationId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update2(Maticsoft.Model.Shop.Products.ProductStationMode model)
        {
            return dal.Update2(model);
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
        public Maticsoft.Model.Shop.Products.ProductStationMode GetModel(int StationId)
        {

            return dal.GetModel(StationId);
        }
        /// <summary>
        /// 根据商品点好查询表内是否存在该条数据(李永琴)
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.ProductStationMode GetProductStationModel(int ProductId,int type)
        {
            return dal.GetProductStationModel(ProductId, type);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Products.ProductStationMode GetModelByCache(int StationId)
        {

            string CacheKey = "ProductStationModesModel-" + StationId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(StationId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Products.ProductStationMode)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductStationMode> GetList2(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
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
        public List<Maticsoft.Model.Shop.Products.ProductStationMode> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Products.ProductStationMode> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Products.ProductStationMode> modelList = new List<Maticsoft.Model.Shop.Products.ProductStationMode>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Products.ProductStationMode model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Shop.Products.ProductStationMode();
                    if (dt.Rows[n]["StationId"] != null && dt.Rows[n]["StationId"].ToString() != "")
                    {
                        model.StationId = int.Parse(dt.Rows[n]["StationId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["DisplaySequence"] != null && dt.Rows[n]["DisplaySequence"].ToString() != "")
                    {
                        model.DisplaySequence = int.Parse(dt.Rows[n]["DisplaySequence"].ToString());
                    }
                    if (dt.Rows[n]["Type"] != null && dt.Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(dt.Rows[n]["Type"].ToString());
                    }
                    if (dt.Columns.Contains("GoodTypeID"))
                    {
                        if (dt.Rows[n]["GoodTypeID"] != null && dt.Rows[n]["GoodTypeID"].ToString() != "")
                        {
                            model.GoodTypeID = int.Parse(dt.Rows[n]["GoodTypeID"].ToString());
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
        /// 根据type获得数据列表
        /// </summary>
        public DataSet GetListByType(string strType)
        {
            return dal.GetListByType(strType);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int productId, int type)
        {
            return dal.Exists(productId, type);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int productId, int type)
        {
            return dal.Delete(productId, type);
        }

        /// <summary>
        /// 清空type下所有商品
        /// </summary>
        public bool DeleteByType(int type,int categoryId)
        {
            return dal.DeleteByType(type, categoryId);
        }

        public DataSet GetStationMode(int modeType, int  categoryId,string pName,int supplierId)
        {
            return dal.GetStationMode(modeType, categoryId, pName, supplierId);
        }

     
        #endregion
    }
}

