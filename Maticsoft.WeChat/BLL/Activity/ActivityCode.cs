/**  版本信息模板在安装目录下，可自行修改。
* ActivityCode.cs
*
* 功 能： N/A
* 类 名： ActivityCode
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
	/// ActivityCode
	/// </summary>
	public partial class ActivityCode
	{
        private readonly IActivityCode dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IActivityCode)new Maticsoft.WeChat.SQLServerDAL.Activity.ActivityCode() : null;//暂时预留
		public ActivityCode()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ActivityCode)
		{
			return dal.Exists(ActivityCode);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.WeChat.Model.Activity.ActivityCode model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Activity.ActivityCode model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ActivityCode)
		{
			
			return dal.Delete(ActivityCode);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ActivityCodelist )
		{
			return dal.DeleteList(ActivityCodelist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Activity.ActivityCode GetModel(string ActivityCode)
		{
			
			return dal.GetModel(ActivityCode);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Activity.ActivityCode GetModelByCache(string ActivityCode)
		{
			
			string CacheKey = "ActivityCodeModel-" + ActivityCode;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ActivityCode);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Activity.ActivityCode)objModel;
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
		public List<Maticsoft.WeChat.Model.Activity.ActivityCode> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Activity.ActivityCode> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Activity.ActivityCode> modelList = new List<Maticsoft.WeChat.Model.Activity.ActivityCode>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Activity.ActivityCode model;
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
        //获取奖品
        public Maticsoft.WeChat.Model.Activity.ActivityCode GetAwardCode(int activityId,string userName)
        {
            //获取活动详细信息
            Maticsoft.WeChat.BLL.Activity.ActivityInfo infoBll=new ActivityInfo();
            Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = infoBll.GetModelByCache(activityId);
            if (infoModel == null)
            {
                return null;
            }
            if (HasChance(activityId, userName))
            {
                //开始写入记录表
                Maticsoft.WeChat.BLL.Activity.ActivityDetail detailBll = new ActivityDetail();
                Maticsoft.WeChat.Model.Activity.ActivityDetail detailModel = new Model.Activity.ActivityDetail();
                detailModel.ActivityId = activityId;
                detailModel.ActivityName = infoModel.Name;
                detailModel.CreateDate = DateTime.Now;
                detailModel.UserName = userName;
                detailBll.Add(detailModel);
                //根据几率获取
                Random rnd = new Random();
                int randValue=rnd.Next(100);
                //在这个几率内 就获取奖品
                if (randValue <= infoModel.Probability)
                {
                    return GetRandCode(activityId, userName);
                }
            }
            return null;
        }
        /// <summary>
        /// 审查 是否有机会参与
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>

        public bool HasChance(int activityId, string userName)
        {
            Maticsoft.WeChat.BLL.Activity.ActivityInfo infoBll = new ActivityInfo();
            Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = infoBll.GetModelByCache(activityId);
            if (infoModel == null)
            {
                return false;
            }
            Maticsoft.WeChat.BLL.Activity.ActivityDetail detailBll=new ActivityDetail();
            //判断活动今天总次数
            if(infoModel.DayTotal>0)
            {
                int dayTotal=detailBll.GetDayTotal(activityId);
                if(dayTotal>=infoModel.DayTotal)
                    return false;
            }
            if(infoModel.LimitType==0)
            {
                int userTotal=detailBll.GetUserTotal(activityId,userName);
                if(userTotal>=infoModel.UserTotal)
                    return false;
            }
            if(infoModel.LimitType==1)
            {
                int dayCount=detailBll.GetEachCount(activityId,userName);
                if(dayCount>=infoModel.EachCount)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 随机获取奖品券
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Maticsoft.WeChat.Model.Activity.ActivityCode GetRandCode(int activityId,string userName,bool AutoBind=false)
        {
            Maticsoft.WeChat.Model.Activity.ActivityCode codeModel = dal.GetRandCode(activityId);
            if (codeModel != null && AutoBind)
            {
                UpdateUser(codeModel.CodeName, userName,"");
            }
            return codeModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeName"></param>
        /// <param name="userName"></param>
        /// <param name="status"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool UpdateUser(string codeName,string userId, string userName,int status=1, string phone="",string remark="")
        {
            return dal.UpdateUser(codeName, userId, userName, status, phone, remark);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateStatusList(string ids, int status)
        {
            return dal.UpdateStatusList(ids, status);
        }
        /// <summary>
        /// 获取用户的SN码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Maticsoft.WeChat.Model.Activity.ActivityCode> GetUserCodes(string username, int top)
        {
            DataSet ds = GetList(top, " UserId='" + username + "'", " GenerateDate desc");
            return DataTableToList(ds.Tables[0]);
        }
		#endregion  ExtensionMethod
	}
}

