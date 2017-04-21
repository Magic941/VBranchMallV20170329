/**
* KeyValue.cs
*
* 功 能： N/A
* 类 名： KeyValue
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:26   N/A    初版
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
using Maticsoft.WeChat.Model.Core;
using Maticsoft.WeChat.IDAL.Core;
namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// KeyValue
	/// </summary>
	public partial class KeyValue
	{
        private readonly IKeyValue dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IKeyValue)new Maticsoft.WeChat.SQLServerDAL.Core.KeyValue() : null;//暂时预留
		public KeyValue()
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
		public bool Exists(int ValueId)
		{
			return dal.Exists(ValueId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.WeChat.Model.Core.KeyValue model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Core.KeyValue model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ValueId)
		{
			
			return dal.Delete(ValueId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ValueIdlist )
		{
			return dal.DeleteList(ValueIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Core.KeyValue GetModel(int ValueId)
		{
			
			return dal.GetModel(ValueId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Core.KeyValue GetModelByCache(int ValueId)
		{
			
			string CacheKey = "KeyValueModel-" + ValueId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ValueId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Core.KeyValue)objModel;
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
		public List<Maticsoft.WeChat.Model.Core.KeyValue> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Core.KeyValue> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Core.KeyValue> modelList = new List<Maticsoft.WeChat.Model.Core.KeyValue>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Core.KeyValue model;
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
        public bool UpdateType(int valueId, int type)
        {
            return dal.UpdateType(valueId, type);
        }

	    public Maticsoft.WeChat.Model.Core.KeyValue GetValue(string value,string openId)
	    {
            string CacheKey = "GetValue11111-" + value;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    //获取所有的关键字
                    List<Maticsoft.WeChat.Model.Core.KeyValue> AllValueList = GetValueList(openId);
                    //先进行全匹配
                    foreach (var keyValue in AllValueList)
                    {
                        if (keyValue.Value.ToLower() == value.ToLower())
                        {
                            objModel = keyValue;
                            break;
                        }
                    }
                    //全匹配没有匹配到，进行非全匹配
                    if (objModel == null)
                    {
                        foreach (var keyValue in AllValueList)
                        {
                            //模糊匹配表示 客户的语句中包含 匹配关键字内容
                            if (value.ToLower().Contains(keyValue.Value.ToLower()))
                            {
                                objModel = keyValue;
                                break;
                            }
                        }
                    }
                    if (objModel != null)
                    {
                        int ModelCache =30;
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.KeyValue)objModel;

	    }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string  value,string openId)
        {
            return dal.Exists(value,openId);
        }
        /// <summary>
        /// 获取所有的回复列表
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public List<Maticsoft.WeChat.Model.Core.KeyValue> GetValueList(string openId)
        {
            DataSet ds = dal.GetValueList(openId);
            return DataTableToList(ds.Tables[0]);
        }
	    #endregion  ExtensionMethod
	}
}

