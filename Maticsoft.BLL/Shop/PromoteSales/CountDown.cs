/**
* CountDown.cs
*
* 功 能： N/A
* 类 名： CountDown
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/11 18:45:37   N/A    初版
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
using Maticsoft.Model.Shop.PromoteSales;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.PromoteSales;
namespace Maticsoft.BLL.Shop.PromoteSales
{
	/// <summary>
	/// CountDown
	/// </summary>
	public partial class CountDown
	{
        private readonly ICountDown dal = DAShopProSales.CreateCountDown();
		public CountDown()
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
		public bool Exists(int CountDownId)
		{
			return dal.Exists(CountDownId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.PromoteSales.CountDown model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.PromoteSales.CountDown model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CountDownId)
		{
			
			return dal.Delete(CountDownId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string CountDownIdlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(CountDownIdlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.PromoteSales.CountDown GetModel(int CountDownId)
		{
			
			return dal.GetModel(CountDownId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.PromoteSales.CountDown GetModelByCache(int CountDownId)
		{
			
			string CacheKey = "CountDownModel-" + CountDownId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(CountDownId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.PromoteSales.CountDown)objModel;
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
		public List<Maticsoft.Model.Shop.PromoteSales.CountDown> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.PromoteSales.CountDown> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.PromoteSales.CountDown> modelList = new List<Maticsoft.Model.Shop.PromoteSales.CountDown>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.PromoteSales.CountDown model;
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

		#endregion  BasicMethod
		#region  ExtensionMethod
        public int MaxSequence()
        {
            return dal.MaxSequence();
        }
        /// <summary>
        /// 是否在活动期间内
        /// </summary>
        /// <param name="countId"></param>
        /// <returns></returns>
	    public bool IsActivity(int countId)
	    {
	       Maticsoft.Model.Shop.PromoteSales.CountDown downModel= GetModelByCache(countId);
	        if (downModel == null)
	            return false;
	        return downModel.EndDate >= DateTime.Now;
	    }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool IsExists(long  ProductId)
        {
            return dal.IsExists(ProductId);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool UpdateStatus(string ids,int status)
        {
            return dal.UpdateStatus(ids, status);
        }

	    #endregion  ExtensionMethod
	}
}

