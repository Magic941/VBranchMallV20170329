/**  版本信息模板在安装目录下，可自行修改。
* SceneDetail.cs
*
* 功 能： N/A
* 类 名： SceneDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/2/20 12:32:25   N/A    初版
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
using System.Text;
namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// SceneDetail
	/// </summary>
	public partial class SceneDetail
	{
        private readonly ISceneDetail dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (ISceneDetail)new WeChat.SQLServerDAL.Core.SceneDetail() : null;
		public SceneDetail()
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
		public bool Exists(int DetailId)
		{
			return dal.Exists(DetailId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.WeChat.Model.Core.SceneDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Core.SceneDetail model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int DetailId)
		{
			
			return dal.Delete(DetailId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string DetailIdlist )
		{
			return dal.DeleteList(DetailIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Core.SceneDetail GetModel(int DetailId)
		{
			
			return dal.GetModel(DetailId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Core.SceneDetail GetModelByCache(int DetailId)
		{
			
			string CacheKey = "SceneDetailModel-" + DetailId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DetailId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Core.SceneDetail)objModel;
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
		public List<Maticsoft.WeChat.Model.Core.SceneDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Core.SceneDetail> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Core.SceneDetail> modelList = new List<Maticsoft.WeChat.Model.Core.SceneDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Core.SceneDetail model;
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
        public int GetCount(int sceneId)
        {
            return GetRecordCount(" SceneId=" + sceneId);
        }

         public static int AddDetail(int SceneId, Maticsoft.WeChat.Model.Core.User user  )
        {
            Maticsoft.WeChat.BLL.Core.SceneDetail bll = new SceneDetail();
            Maticsoft.WeChat.Model.Core.SceneDetail model = new Model.Core.SceneDetail();
            model.CreateTime = DateTime.Now;
             model.City=user.City;
             model.Country=user.Country;
             model.Language=user.Language;
             model.OpenId=user.OpenId;
             model.UserName=user.UserName;
             model.Province=user.Province;
             model.SceneId=SceneId;
             model.Sex=user.Sex;
             model.NickName = user.NickName;
             return bll.Add(model);
        }
        /// <summary>
        /// 获取天数的Count
        /// </summary>
        /// <param name="sceneId"></param>
        /// <param name="Fdate"></param>
        /// <param name="Tdate"></param>
        /// <returns></returns>
         public int GetDayCount(int sceneId,string Fdate,string Tdate)
         {
             StringBuilder strWhere = new StringBuilder();
             strWhere.AppendFormat(" SceneId={0}  ", sceneId);
             if ( !String.IsNullOrWhiteSpace(Fdate))
             {
                 strWhere.AppendFormat(" and CreateTime >='{0}' ", Fdate);
             }
             if (!String.IsNullOrWhiteSpace(Tdate))
             {
                 strWhere.AppendFormat(" and CreateTime<='{0}' ", Tdate);
             }
             return GetRecordCount(strWhere.ToString());
         }

		#endregion  ExtensionMethod
	}
}

