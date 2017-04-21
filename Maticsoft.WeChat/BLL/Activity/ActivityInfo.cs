/**  版本信息模板在安装目录下，可自行修改。
* ActivityInfo.cs
*
* 功 能： N/A
* 类 名： ActivityInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:16   N/A    初版
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
using Maticsoft.WeChat.Model.Activity;
using Maticsoft.WeChat.IDAL.Activity;
namespace Maticsoft.WeChat.BLL.Activity
{
	/// <summary>
	/// ActivityInfo
	/// </summary>
	public partial class ActivityInfo
	{
        private readonly IActivityInfo dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IActivityInfo)new Maticsoft.WeChat.SQLServerDAL.Activity.ActivityInfo() : null;//暂时预留
		public ActivityInfo()
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
		public bool Exists(int ActivityId)
		{
			return dal.Exists(ActivityId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.WeChat.Model.Activity.ActivityInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Activity.ActivityInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ActivityId)
		{
			
			return dal.Delete(ActivityId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ActivityIdlist )
		{
			return dal.DeleteList(ActivityIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Activity.ActivityInfo GetModel(int ActivityId)
		{
			
			return dal.GetModel(ActivityId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Activity.ActivityInfo GetModelByCache(int ActivityId)
		{
			
			string CacheKey = "ActivityInfoModel-" + ActivityId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ActivityId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Activity.ActivityInfo)objModel;
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
		public List<Maticsoft.WeChat.Model.Activity.ActivityInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Activity.ActivityInfo> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Activity.ActivityInfo> modelList = new List<Maticsoft.WeChat.Model.Activity.ActivityInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Activity.ActivityInfo model;
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

        public bool StartActivity(int activityId)
        {
          Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel= GetModel(activityId);
          if (infoModel !=null)
            {
                //获取活动礼品信息
                Maticsoft.WeChat.BLL.Activity.ActivityAward awardBll = new ActivityAward();
                List<Maticsoft.WeChat.Model.Activity.ActivityAward> AwardList = awardBll.GetAwardList(activityId);
                Random rnd = new Random();
                List<string> codeList = new List<string>();
                Maticsoft.WeChat.Model.Activity.ActivityCode codeModel = null;
              Maticsoft.WeChat.BLL.Activity.ActivityCode codeBll=new ActivityCode();
                foreach (var item in AwardList)
                {
                    int maxValue = 10;
                    for (int j = 1; j < 8; j++)
                    {
                        maxValue = maxValue * 10;
                    }
                    for (int i = 0; i < item.Count; i++)
                    {
                        codeModel = new Model.Activity.ActivityCode();
                        int rand = rnd.Next(maxValue / 10 + 1, maxValue - 1);
                        codeModel.CodeName = infoModel.PreName + DateTime.Now.ToString("MMdd") +
                                               rand.ToString();
                        while (codeList.Contains(codeModel.CodeName))
                        {
                            rand = rnd.Next(maxValue / 10 + 1, maxValue - 1);
                            codeModel.CodeName = infoModel.PreName + DateTime.Now.ToString("MMdd") +
                                              rand.ToString();
                        }
                        codeList.Add(codeModel.CodeName);
                        codeModel.ActivityName = infoModel.Name;
                        codeModel.GenerateDate = DateTime.Now;
                        codeModel.ActivityId = infoModel.ActivityId;
                        codeModel.AwardId = item.AwardId;
                        codeModel.AwardName = item.AwardName;
                        codeModel.EndDate = infoModel.EndDate;
                        codeModel.StartDate = infoModel.StartDate;
                        codeModel.IsPwd = infoModel.IsPwd;
                        codeModel.UserName = "";
                        codeModel.Phone = "";
                        codeModel.ActivityPwd = "";
                        codeModel.Status = 0;
                        codeModel.UserId = "";
                        codeBll.Add(codeModel);
                    }
                }
              //更新活动状态
                UpdateStatus(activityId, 1);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatus(int activityId, int status)
        {
            return dal.UpdateStatus(activityId, status);
        }

        /// <summary>
        /// 获取活动
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Maticsoft.WeChat.Model.Activity.ActivityInfo GetActivity(int activityId, int type)
        {
            return dal.GetActivity(activityId, type);
        }
		#endregion  ExtensionMethod
	}
}

