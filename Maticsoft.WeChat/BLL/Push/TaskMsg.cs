/**  版本信息模板在安装目录下，可自行修改。
* TaskMsg.cs
*
* 功 能： N/A
* 类 名： TaskMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/7 17:58:09   N/A    初版
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
using Maticsoft.WeChat.Model.Push;
using Maticsoft.WeChat.IDAL.Push;
using Maticsoft.WeChat.BLL.Core;
using System.Linq;
namespace Maticsoft.WeChat.BLL.Push
{
	/// <summary>
	/// TaskMsg
	/// </summary>
	public partial class TaskMsg
	{
        private readonly ITaskMsg dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (ITaskMsg)new Maticsoft.WeChat.SQLServerDAL.Push.TaskMsg() : null;//暂时预留
		public TaskMsg()
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
		public bool Exists(int TaskId)
		{
			return dal.Exists(TaskId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.WeChat.Model.Push.TaskMsg model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Push.TaskMsg model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int TaskId)
		{
			
			return dal.Delete(TaskId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string TaskIdlist )
		{
			return dal.DeleteList(TaskIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Push.TaskMsg GetModel(int TaskId)
		{
			
			return dal.GetModel(TaskId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Push.TaskMsg GetModelByCache(int TaskId)
		{
			
			string CacheKey = "TaskMsgModel-" + TaskId;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(TaskId);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Push.TaskMsg)objModel;
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
		public List<Maticsoft.WeChat.Model.Push.TaskMsg> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Push.TaskMsg> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Push.TaskMsg> modelList = new List<Maticsoft.WeChat.Model.Push.TaskMsg>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Push.TaskMsg model;
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
        /// 添加任务消息
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        public int AddEx(Maticsoft.WeChat.Model.Push.TaskMsg msgModel)
        {
            int  msgId = Add(msgModel);

            if (msgModel.MsgType == "news")
            {
                Maticsoft.WeChat.BLL.Core.PostMsgItem postItemBll = new PostMsgItem();
                Maticsoft.WeChat.Model.Core.PostMsgItem model = null;
                foreach (var item in msgModel.MsgItems)
                {
                    model = new Model.Core.PostMsgItem();
                    model.ItemId = item.ItemId;
                    model.PostMsgId = Common.Globals.SafeInt(msgId, 0);
                    model.Type = 2;
                    postItemBll.Add(model);
                }
            }
            return msgId;
        }
        /// <summary>
        /// 获取推送消息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public  Maticsoft.WeChat.Model.Push.TaskMsg GetMsg(string openId, string userName)
        {
            Maticsoft.WeChat.Model.Core.User weUser = Maticsoft.WeChat.BLL.Core.User.GetUserInfo(openId, userName);
            //查找该用户的消息推送记录
            List<Maticsoft.WeChat.Model.Push.TaskLog> logs=Maticsoft.WeChat.BLL.Push.TaskLog.GetUserLogs(userName);
            var hasTasks = new List<int>();
            if (logs != null)
            {
                hasTasks = logs.Select(c => c.TaskId).ToList();
            }
            //查找该公众号的所有的任务消息
            List<Maticsoft.WeChat.Model.Push.TaskMsg> msgList = GetMsgList(openId);
            //过滤条件
           return  msgList.Where(c => !hasTasks.Contains(c.TaskId)).Where(c => (c.GroupId == 0) || (c.GroupId != 0 && c.GroupId == weUser.GroupId)).
                 Where(c => (String.IsNullOrWhiteSpace(c.UserName) || (c.UserName == userName))).FirstOrDefault();
        }

        public List<Maticsoft.WeChat.Model.Push.TaskMsg> GetMsgList(string openId)
        {
            DataSet ds=dal.GetMsgList(openId,DateTime.Now.ToString("yyyy-MM-dd"));
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获取任务推送消息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.PostMsg GetPushMsg(string openId, string userName)
        {
            Maticsoft.WeChat.BLL.Push.TaskMsg taskBll=new TaskMsg();
            Maticsoft.WeChat.BLL.Core.MsgItem itemBll=new MsgItem();
            Maticsoft.WeChat.Model.Core.PostMsg postMsg = new Model.Core.PostMsg();
            Maticsoft.WeChat.Model.Push.TaskMsg taskMsg = taskBll.GetMsg(openId, userName);
            if (taskMsg != null)
            {
                postMsg.CreateTime = DateTime.Now;
                postMsg.OpenId = openId;
                postMsg.UserName = userName;
                postMsg.MsgType = taskMsg.MsgType;
                postMsg.Description = taskMsg.Description;
                postMsg.MediaId = taskMsg.MediaId;
                if (postMsg.MsgType == "news")
                {
                   List<Maticsoft.WeChat.Model.Core.MsgItem> MsgItems = itemBll.GetItemList(taskMsg.TaskId, 2);

                   postMsg.ArticleCount = MsgItems.Count;
                    Maticsoft.WeChat.Model.Core.MsgItem item = null;
                    for (int i = 0; i < MsgItems.Count; i++)
                    {
                        item = new Model.Core.MsgItem();
                        item.Title = MsgItems[i].Title;
                        item.Description = MsgItems[i].Description;
                        if (i == 0)
                        {
                            item.PicUrl = String.IsNullOrWhiteSpace(MsgItems[i].PicUrl) ? "" :
                                "http://" + Common.Globals.DomainFullName + String.Format(MsgItems[i].PicUrl, "N_");
                        }
                        else
                        {
                            item.PicUrl = String.IsNullOrWhiteSpace(MsgItems[i].PicUrl) ? "" :
                                                                  "http://" + Common.Globals.DomainFullName + String.Format(MsgItems[i].PicUrl, "T_");
                        }
                        item.Url = Maticsoft.WeChat.BLL.Core.Utils.GetWCUrl(openId,userName, MsgItems[i].Url);
                        postMsg.MsgItems.Add(item);
                    }
                  
                }
                Maticsoft.WeChat.BLL.Push.TaskLog.Add(taskMsg.TaskId, userName);
            }
            return postMsg;
        }
        
		#endregion  ExtensionMethod
	}
}

