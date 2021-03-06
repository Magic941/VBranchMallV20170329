﻿/**
* AgentConfig.cs
*
* 功 能： N/A
* 类 名： AgentConfig
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/28 18:17:10   Ben    初版
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
using Maticsoft.Model.Ms.Agent;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Ms.Agent;
namespace Maticsoft.BLL.Ms.Agent
{
	/// <summary>
	/// 代理商(店铺)配置
	/// </summary>
	public partial class AgentConfig
	{
        private static readonly IAgentConfig dal = DAMsAgent.CreateAgentConfig();
		public AgentConfig()
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
		public int  Add(Maticsoft.Model.Ms.Agent.AgentConfig model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Ms.Agent.AgentConfig model)
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
		public Maticsoft.Model.Ms.Agent.AgentConfig GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Ms.Agent.AgentConfig GetModelByCache(int ID)
		{
			
			string CacheKey = "AgentConfigModel-" + ID;
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
			return (Maticsoft.Model.Ms.Agent.AgentConfig)objModel;
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
		public List<Maticsoft.Model.Ms.Agent.AgentConfig> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Ms.Agent.AgentConfig> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Ms.Agent.AgentConfig> modelList = new List<Maticsoft.Model.Ms.Agent.AgentConfig>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Ms.Agent.AgentConfig model;
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

        public static string GetValue(int agentId, string keyName)
        {
            return dal.GetValue(agentId, keyName);
        }

        /// <summary>
        /// 根据供应商id和参数名称获取参数值 ，从缓存中
        /// </summary>
        public static string GetValueByCache(int suppId, string keyName)
        {
            string CacheKey = "SupplierConfigModel-suppId" + suppId + "-KeyName" + keyName;
            object objValue = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objValue == null)
            {
                try
                {
                    objValue = GetValue(suppId, keyName);
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
        public Model.Ms.Agent.AgentConfig GetModel(string strWhere)
        {
            return dal.GetModel(strWhere);
        }
		#endregion  ExtensionMethod
	}
}

