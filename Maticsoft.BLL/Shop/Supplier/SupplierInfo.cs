/**
* Suppliers.cs
*
* 功 能： N/A
* 类 名： Suppliers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:50   Ben    初版
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
using ServiceStack.RedisCache;

namespace Maticsoft.BLL.Shop.Supplier
{
    /// <summary>
    /// 供应商
    /// </summary>
    public partial class SupplierInfo
    {
        private readonly ISupplierInfo dal = DAShopSupplier.CreateSupplierInfo();
        public SupplierInfo()
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
        public bool Exists(int SupplierId)
        {
            return dal.Exists(SupplierId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int SupplierId)
        {

            return dal.Delete(SupplierId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string SupplierIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(SupplierIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModel(int SupplierId)
        {
            return dal.GetModel(SupplierId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModelByCache(int SupplierId)
        {
            object objModel = null;
            string CacheKey = "SuppliersModel-" + SupplierId;
            if (Maticsoft.BLL.DataCacheType.CacheType == 1)
            {
                Maticsoft.Model.Shop.Supplier.SupplierInfo productImg = new Maticsoft.Model.Shop.Supplier.SupplierInfo();
                productImg = RedisBase.Item_Get<Maticsoft.Model.Shop.Supplier.SupplierInfo>(CacheKey);
                objModel = productImg;
            }

            if (Maticsoft.BLL.DataCacheType.CacheType == 0)
            {
                objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            }

            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SupplierId);
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                    if (Maticsoft.BLL.DataCacheType.CacheType == 1)
                    {
                        RedisBase.Item_Set<Maticsoft.Model.Shop.Supplier.SupplierInfo>(CacheKey, (Maticsoft.Model.Shop.Supplier.SupplierInfo)objModel);
                    }
                    if (Maticsoft.BLL.DataCacheType.CacheType == 0)
                    {
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Supplier.SupplierInfo)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public int GetCountByAgent(int agentId)
        {
            string str = string.Format(" AgentId={0}", agentId.ToString());
            DataSet ds = dal.GetList(str);
            return ds.Tables[0].Rows.Count;
        }

       public int GetCountByWhere(string strWhere)
       {
           DataSet ds = dal.GetList(strWhere);
           return ds.Tables[0].Rows.Count;
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
        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Supplier.SupplierInfo> modelList = new List<Maticsoft.Model.Shop.Supplier.SupplierInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Supplier.SupplierInfo model;
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
        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> GetListByPageEx(string strWhere, string orderby,
                                                                                int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
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
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModelByUserId(int userId)
        {
            return dal.GetModelByUserId(userId);
        }
        /// <summary>
        /// 供应商名称是否已存在
        /// </summary>
        public bool Exists(string Name)
        {
            return dal.Exists(Name);
        }
        /// <summary>
        /// 店铺名称是否已存在
        /// </summary>
        public bool ExistsShopName(string Name)
        {
            return dal.ExistsShopName(Name);
        }

        /// <summary>
        /// 店铺名称是否已存在
        /// </summary>
        public bool ExistsShopName(string Name, int SupplierID)
        {
            return dal.ExistsShopName(Name,SupplierID);
        }

        /// <summary>
        /// 供应商名称是否已存在
        /// </summary>
        public bool Exists(string Name, int id)
        {
            return dal.Exists(Name, id);
        }
        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }

        public DataSet GetEnteName(string name, int iCount)
        {
            string strWhere = "Name like '" + name + "%' AND Status=1 ";
            return dal.GetList(iCount, strWhere, "Name");
        }

        public List<Model.Shop.Supplier.SupplierInfo> GetModelBySupplierName(string name)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrWhiteSpace(name))
            {
                strWhere = "Name = '" + name + "'";
            }
            return GetModelList(strWhere);
        }

        public DataSet GetStatisticsSupply(int supplierId)
        {
            return dal.GetStatisticsSupply(supplierId);
        }

        public DataSet GetStatisticsSales(int supplierId, int year)
        {
            return dal.GetStatisticsSales(supplierId, year);
        }

        /// <summary>
        /// 关闭店铺 
        /// </summary>
        public bool CloseShop(int SupplierId)
        {
            return dal.CloseShop(SupplierId);
        }

        /// <summary>
        /// 根据店铺名称得到店铺model
        /// </summary>
        /// <param name="ShopName"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Supplier.SupplierInfo GetModelByShopName(string ShopName)
        { 
            if (!string.IsNullOrWhiteSpace(ShopName))
            {
                return dal.GetModelByShopName(ShopName);
            }
            return null;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>   
        public bool IsExist(int agentId, int? supplierId)
        {
            if (supplierId.HasValue)
            {
                Model.Shop.Supplier.SupplierInfo model = GetModelByCache(supplierId.Value);
                if (model != null && model.AgentId == agentId)
                {
                    return true;
                }
            }
            return false;
        }
       /// <summary>
        /// 根据代理商id获得商家数据列表 
       /// </summary>
        /// <param name="agentId">代理商id</param>
       /// <returns></returns>
        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> GetModelListByAgentId(int agentId)
        {
            DataSet ds = dal.GetList(string.Format(" AgentId = {0} and  Status=1 and StoreStatus=1 ", agentId));
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <param name="Rec"></param>
        /// <returns></returns>
        public bool SetRec(int SupplierId, int Rec)
        {
            return dal.SetRec(SupplierId, Rec);
        }
        /// <summary>
        ///获取推荐的店铺
        /// </summary>
        /// <param name="Top">前几条</param>
        /// <param name="rec">0 未推荐;1 已推荐</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> GetList(int Top,int rec)
        {
            DataSet ds = dal.GetList(Top, string.Format(" Status=1 and StoreStatus=1  and  Recomend = {0} ", rec), " SupplierId desc ");
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 删除一条数据 事物删除该商家的所以关联数据
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        public bool DeleteEx(int SupplierId)
        {
            return dal.DeleteEx(SupplierId);
        }

        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> GetSupplierByPosition(double latitudeLow,
                                                                              double longitudeLow,
                                                                              double latitudeHigh,
                                                                              double longitudeHigh, double range)
        {
            DataSet ds = dal.GetSupplierByPosition(latitudeLow, longitudeLow, latitudeHigh, longitudeHigh, range);
            return DataTableToList(ds.Tables[0]);
        }


       public bool Update(Model.Shop.Supplier.SupplierInfo supplierInfo, int SupplierId, List<int> idList)
       {
           return dal.Update(supplierInfo, SupplierId, idList);
       }


       public bool Add(int supplierId, List<int> idList)
       {
           if (null == idList || idList.Count < 1) return false;
           return dal.Add(supplierId, idList);
       }
        #endregion  ExtensionMethod

       public static string SupperLogo(int id, string size)
       {
           var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
           string pathName = picServerUrl + string.Format("/Upload/Supplier/Logo/{0}_{1}.png", id, size);
           return pathName;
       }

       public static string SupperLogo(string imgUrl)
       {
           var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
           string pathName = picServerUrl + imgUrl;
           return pathName;
       }
    }
}

