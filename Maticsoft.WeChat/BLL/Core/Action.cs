/**
* Action.cs
*
* 功 能： N/A
* 类 名： Action
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:10   N/A    初版
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
	/// Action
	/// </summary>
	public partial class Action
	{
        private readonly IAction dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IAction)new Maticsoft.WeChat.SQLServerDAL.Core.Action() : null;//暂时预留
		public Action()
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
		public bool Exists(int ActionId)
		{
			return dal.Exists(ActionId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.WeChat.Model.Core.Action model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Core.Action model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ActionId)
		{
			
			return dal.Delete(ActionId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ActionIdlist )
		{
			return dal.DeleteList(ActionIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Core.Action GetModel(int ActionId)
		{
			
			return dal.GetModel(ActionId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Core.Action GetModelByCache(int ActionId)
		{
			
			string CacheKey = "ActionModel-" + ActionId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ActionId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Core.Action)objModel;
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
		public List<Maticsoft.WeChat.Model.Core.Action> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Core.Action> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Core.Action> modelList = new List<Maticsoft.WeChat.Model.Core.Action>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Core.Action model;
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
        /// 获取所有的指令
        /// </summary>
        /// <returns></returns>
	    public static List<Maticsoft.WeChat.Model.Core.Action> GetAllAction()
	    {
            string CacheKey = "GetAllAction-" ;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.WeChat.BLL.Core.Action actionBll = new Action();
                    objModel = actionBll.GetModelList("");
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.WeChat.Model.Core.Action>)objModel;
           
	    }

        public bool DeleteListEx(string ActionIdlist)
        {
            return dal.DeleteListEx(ActionIdlist);
        }

	    public static int GetActionId(string Key)
	    {
	        if (String.IsNullOrWhiteSpace(Key))
	        {
	            return 0;
	        }
            var key_arry = Key.Split('_');
            return key_arry.Length <= 1?0:Common.Globals.SafeInt(key_arry[1], 0);
	    }
        /// <summary>
        /// 根据事件Key 获取 Action
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
	    public static Maticsoft.WeChat.Model.Core.Action GetActionByKey(string key)
	    {
	        int actionId = GetActionId(key);
            Maticsoft.WeChat.BLL.Core.Action actionBll = new Action();

	       return actionBll.GetModel(actionId);
	    }

	    #endregion  ExtensionMethod
	}
}

