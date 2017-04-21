/**
* ShippingType.cs
*
* 功 能： N/A
* 类 名： ShippingType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 10:24:45   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Shipping;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Shipping;
namespace Maticsoft.BLL.Shop.Shipping
{
    /// <summary>
    /// ShippingType
    /// </summary>
    public partial class ShippingType
    {
        private readonly IShippingType dal = DAShopShipping.CreateShippingType();
        public ShippingType()
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
        public bool Exists(int ModeId)
        {
            return dal.Exists(ModeId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.Shipping.ShippingType model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ModeId)
        {

            return dal.Delete(ModeId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ModeIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ModeIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Shipping.ShippingType GetModel(int ModeId)
        {

            return dal.GetModel(ModeId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Shipping.ShippingType GetModelByCache(int ModeId)
        {

            string CacheKey = "ShippingTypeModel-" + ModeId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ModeId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Shipping.ShippingType)objModel;
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
        public List<Maticsoft.Model.Shop.Shipping.ShippingType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Shipping.ShippingType> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Shipping.ShippingType> modelList = new List<Maticsoft.Model.Shop.Shipping.ShippingType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Shipping.ShippingType model;
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
        /// 根据支付方式获取对应物流
        /// </summary>
        public List<Maticsoft.Model.Shop.Shipping.ShippingType> GetListByPay(int paymentModeId)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.AppendFormat(@"EXISTS ( SELECT ShippingModeId
                 FROM   Shop_ShippingPayment
                 WHERE  ShippingModeId = Shop_ShippingType.ModeId
                        AND PaymentModeId = {0} )", paymentModeId);
            return GetModelList(sql.ToString());
        }

        /// <summary>
        /// 获取运费
        /// </summary>
        public decimal GetFreight(Maticsoft.Model.Shop.Shipping.ShippingType typeModel, int weight)
        {
            if (weight <= typeModel.Weight)
            {
                return typeModel.Price;
            }
            else
            {
                if (!typeModel.AddWeight.HasValue || typeModel.AddWeight.Value <= 0 || !typeModel.AddPrice.HasValue ||
                    typeModel.AddPrice.Value < 0)
                {
                    return typeModel.Price;
                }
                int addWeight = weight - typeModel.Weight;
                int addStep = addWeight % typeModel.AddWeight == 0 ? addWeight / typeModel.AddWeight.Value : (addWeight / typeModel.AddWeight.Value) + 1;
                return typeModel.Price + addStep * typeModel.AddPrice.Value;
            }
        }
        #endregion  ExtensionMethod
    }
}

