/**  版本信息模板在安装目录下，可自行修改。
* Config.cs
*
* 功 能： N/A
* 类 名： Config
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/21 18:25:39   N/A    初版
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
using Maticsoft.WeChat.Model.Core;
using Maticsoft.WeChat.IDAL.Core;
using System.Web;
using System.Linq;
namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// Config
	/// </summary>
	public partial class Config
	{
        private readonly IConfig dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IConfig)new WeChat.SQLServerDAL.Core.Config() : null;//暂时预留
		public Config()
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
		public int  Add(Maticsoft.WeChat.Model.Core.Config model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Core.Config model)
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
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Core.Config GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Core.Config GetModelByCache(int ID)
		{
			
			string CacheKey = "ConfigModel-" + ID;
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
			return (Maticsoft.WeChat.Model.Core.Config)objModel;
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
		public List<Maticsoft.WeChat.Model.Core.Config> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Core.Config> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Core.Config> modelList = new List<Maticsoft.WeChat.Model.Core.Config>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Core.Config model;
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

        public bool Exists(string key, int TargetId, string UserType)
        {
            return dal.Exists(key, TargetId, UserType);
        }

        public bool Exists(string key, string value)
        {
            return dal.Exists(key, value);
        }


        public bool UpdateEx(Maticsoft.WeChat.Model.Core.Config model)
        {
            return dal.UpdateEx(model);
        }

        public static  bool Modify(Maticsoft.WeChat.Model.Core.Config model,bool isRepeat)
        {
            Maticsoft.WeChat.BLL.Core.Config bll = new Config();
            if (!isRepeat && bll.Exists(model.KeyName, model.Value))
            {
                //先删除
                bll.Delete(model.KeyName, model.Value);
                return bll.Add(model)>0;
            }
            else if (bll.Exists(model.KeyName, model.TargetId, model.UserType))
            {
                return bll.UpdateEx(model);
            }
            else
            {
                return bll.Add(model) > 0;
            }
        }

        public static bool Modify(string key ,string value,int targetid,string userType,string desc,bool isRepeat=true)
        {
            Maticsoft.WeChat.BLL.Core.Config bll = new Config();
            Maticsoft.WeChat.Model.Core.Config model = new Model.Core.Config();
            model.Value = value;
            model.UserType = userType;
            model.KeyName = key;
            model.TargetId = targetid;
            model.Description = desc;
            return Modify(model, isRepeat);
        }

        public static List<Maticsoft.WeChat.Model.Core.Config> GetConfigs()
        {
            string CacheKey = "WeChatALLConfig";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.WeChat.BLL.Core.Config bll = new Config();
                    objModel =bll.GetModelList("");
                    if (objModel != null)
                    {
                        int CacheTime = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.WeChat.Model.Core.Config>)objModel;
        }

        public static void  ClearCache()
        {
            HttpContext.Current.Cache.Remove("WeChatALLConfig");
        }

        public static string GetValueByCache(string key, int TargetId, string UserType)
        {
           List<Maticsoft.WeChat.Model.Core.Config> AllConfigs= GetConfigs();
           var config = AllConfigs.FirstOrDefault(c => c.KeyName == key && c.TargetId == TargetId && c.UserType == UserType);
           return config == null ? "" : config.Value;
        }

        public static bool isSendEmail(string openId, string key)
        {
            List<Maticsoft.WeChat.Model.Core.Config> ALLConfig=GetConfigs();
            Maticsoft.WeChat.Model.Core.Config configModel = ALLConfig.FirstOrDefault(c => c.Value == openId);
            if (configModel == null)
                return false;
            Maticsoft.WeChat.Model.Core.Config configModel2 = ALLConfig.FirstOrDefault(c => c.KeyName == key && 
                c.TargetId == configModel.TargetId && c.UserType == configModel.UserType);
            return configModel2 == null ? false : Common.Globals.SafeBool(configModel2.Value,false);
        }


        public static string GetValueByCache(string key, string openId)
        {
            List<Maticsoft.WeChat.Model.Core.Config> ALLConfig = GetConfigs();
            Maticsoft.WeChat.Model.Core.Config configModel = ALLConfig.FirstOrDefault(c => c.Value == openId);
            if (configModel == null)
                return "";
            Maticsoft.WeChat.Model.Core.Config configModel2 = ALLConfig.FirstOrDefault(c => c.KeyName == key &&
                c.TargetId == configModel.TargetId && c.UserType == configModel.UserType);
            return configModel2 == null ? "" : configModel2.Value;
        }
        /// <summary>
        /// 获取对应的角色ID，商家或者 代理商
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static int GetTargetId(string openId)
        {
            List<Maticsoft.WeChat.Model.Core.Config> ALLConfig = GetConfigs();
            Maticsoft.WeChat.Model.Core.Config configModel = ALLConfig.FirstOrDefault(c => c.Value == openId && c.KeyName == "WeChat_OpenId");
            return configModel == null ? 0 : configModel.TargetId;
        }

        public bool Delete(string key, string value)
        {
            return dal.Delete(key, value);
        }

        public static List<Maticsoft.WeChat.Model.Core.Config> GetOpenIds()
        {
          List<Maticsoft.WeChat.Model.Core.Config>  AllConfig=  GetConfigs();
          return AllConfig.Where(c => c.KeyName == "WeChat_OpenId").ToList();
        }
		#endregion  ExtensionMethod
	}
}

