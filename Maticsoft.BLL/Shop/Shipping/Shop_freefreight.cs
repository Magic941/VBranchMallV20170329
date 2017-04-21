/**  版本信息模板在安装目录下，可自行修改。
* Shop_freefreight.cs
*
* 功 能： N/A
* 类 名： Shop_freefreight
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/8/13 11:01:41   N/A    初版
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
using Maticsoft.Model;
using Maticsoft.DALFactory;
using Maticsoft.IDAL;
using Maticsoft.IDAL.Shop.Shipping;
namespace Maticsoft.BLL.Shop.Shipping
{
	/// <summary>
	/// Shop_freefreight
	/// </summary>
	public partial class Shop_freefreight
	{
        private readonly IShop_freefreight dal = DAShopShipping.CreateShop_freefreight();
		public Shop_freefreight()
		{}
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
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

        public bool ExistsRegion(int regionId)
        {
            return dal.ExistsRegion(regionId);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.Shipping.Shop_freefreight model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Shipping.Shop_freefreight model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool Delete(string strWhere)
		{

            return dal.Delete(strWhere);
		}

        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Shipping.Shop_freefreight GetModel(int regionid)
		{

            return dal.GetModel(regionid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Shipping.Shop_freefreight GetModelByCache(int id)
		{
			
			string CacheKey = "Shop_freefreightModel-" + id;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Shipping.Shop_freefreight)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Shipping.Shop_freefreight> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

        public List<Maticsoft.Model.Shop.Shipping.Shop_freefreight> GetModelList()
        {
            DataSet ds = dal.GetList();
            return DataTableToList(ds.Tables[0]);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Shipping.Shop_freefreight> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Shipping.Shop_freefreight> modelList = new List<Maticsoft.Model.Shop.Shipping.Shop_freefreight>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Shipping.Shop_freefreight model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}
        /// <summary>
        /// 计算免邮
        /// </summary>
        /// <param name="regionid">区域id</param>
        /// <param name="totalprice">订单总价（非支付）</param>
        /// <param name="cartInfo">购物车</param>
        /// <returns></returns>
        public decimal CalFreeShipping(int regionid, decimal totalprice, Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo,Model.Shop.Shipping.ShippingType shipType,Model.Shop.Shipping.ShippingRegionGroups regionGroups)
        {
            Maticsoft.Model.Shop.Shipping.Shop_freefreight freeFreght = null;
            //判断是否有单品或单品集合存在免邮




            //判断当前订单配送区域是否支持免邮
            if (this.ExistsRegion(regionid))
            {
                //若存在，判断订单金额是否满足免邮值
                freeFreght = dal.GetModel(regionid);
                if (freeFreght==null)
                {
                    return cartInfo.CalcFreight(shipType, regionGroups);
                }
                if (totalprice >= freeFreght.totalmoney)
                {
                    return 0M;
                }
                else
                {
                    return cartInfo.CalcFreight(shipType, regionGroups);
                }
            }
            else
            { 
                //若不存在，判断是否设置全场免邮
                if (this.ExistsRegion(0))
                {
                    //若存在，判断订单金额是否满足免邮值
                    freeFreght = dal.GetModel(0);

                    if (freeFreght == null)
                    {
                        return cartInfo.CalcFreight(shipType, regionGroups);
                    }
                    if (totalprice >= freeFreght.totalmoney)
                    {
                        return 0M;
                    }
                    else
                    {
                        return cartInfo.CalcFreight(shipType, regionGroups);
                    }
                }
                else 
                { 
                    //若不存在,则根据规则计算运费
                    return cartInfo.CalcFreight(shipType, regionGroups);
                }
            }

        }
        
		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

