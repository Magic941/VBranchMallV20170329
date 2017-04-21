/**
* SupplierConfig.cs
*
* 功 能： N/A
* 类 名： SupplierConfig
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:48   Ben    初版
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
	/// 供应商(店铺)配置
	/// </summary>
	public partial class SupplierConfig
	{
        private readonly static ISupplierConfig dal = DAShopSupplier.CreateSupplierConfig();
		public SupplierConfig()
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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Shop.Supplier.SupplierConfig GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Shop.Supplier.SupplierConfig GetModelByCache(int ID)
		{
			
			string CacheKey = "SupplierConfigModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Shop.Supplier.SupplierConfig)objModel;
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
		public List<Maticsoft.Model.Shop.Supplier.SupplierConfig> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Shop.Supplier.SupplierConfig> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Shop.Supplier.SupplierConfig> modelList = new List<Maticsoft.Model.Shop.Supplier.SupplierConfig>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Shop.Supplier.SupplierConfig model;
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

	   
	    /// <summary>
	    /// 根据供应商id和参数名称获取参数值 
	    /// </summary>
        /// <param name="suppId">供应商id</param>
	    /// <param name="keyName">参数名称</param>
	    /// <returns></returns>
        public static string GetValue(int suppId, string keyName)
	    {
            return dal.GetValue(suppId, keyName);
	    }

        /// <summary>
        /// 根据供应商id和参数名称获取参数值 ，从缓存中
        /// </summary>
        public  static  string GetValueByCache(int suppId, string keyName)
        {
            string CacheKey = "SupplierConfigModel-suppId" + suppId + "-KeyName" + keyName;
            object objValue = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objValue == null)
            {
                try
                {
                    objValue =  GetValue(suppId, keyName);
                    if (objValue != null)
                    {
                        int ValueCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objValue, DateTime.Now.AddMinutes(ValueCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return objValue.ToString();
        }
        /// <summary>
        ///   Get an object entity for INT，From cache
        /// </summary>
        /// <param name="suppId"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static int GetIntValueByCache(int suppId, string keyName)
        {
            return Globals.SafeInt(GetValueByCache(suppId, keyName), -1);
        }
        public Maticsoft.Model.Shop.Supplier.SupplierConfig GetModel(string strWhere)
        {
            return dal.GetModel(strWhere);
        }

	    public bool Exists(string key, int sipId)
	    {
	        return dal.Exists(key, sipId);
	    }
        /// <summary>
        /// 编辑 商家Config 参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Modify(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
        {
            if (Exists(model.KeyName, model.SupplierId))
            {
              return    UpdateEx(model);
            }
            else
            {
                return Add(model)>0;
            }
        }
        /// <summary>
        /// 编辑 商家Config 参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Modify(int supplierId, string keyname, string value,int type, string description = "")
        {
            Maticsoft.Model.Shop.Supplier.SupplierConfig model= new Model.Shop.Supplier.SupplierConfig();
            model.KeyName = keyname;
            model.KeyType = type;
            model.SupplierId = supplierId;
            model.Value = value;
            model.Description = description;
            if (Exists(keyname, supplierId))
            {
              return    UpdateEx(model);
            }
            else
            {
                return Add(model)>0;
            }
        }
       
       public bool UpdateEx(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
        {
            return dal.UpdateEx(model);
        }
 
	    #endregion  ExtensionMethod
	}
}

