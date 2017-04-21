/**
* SysMsg.cs
*
* 功 能： N/A
* 类 名： SysMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/2 14:55:58   N/A    初版
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
using System.Text.RegularExpressions;
namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// SysMsg
	/// </summary>
	public partial class SysMsg
	{
        private readonly ISysMsg dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (ISysMsg)new WeChat.SQLServerDAL.Core.SysMsg() : null;
		public SysMsg()
		{}
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
        public bool Exists(int SysMsgId)
        {
            return dal.Exists(SysMsgId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.SysMsg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.SysMsg model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int SysMsgId)
        {

            return dal.Delete(SysMsgId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string SysMsgIdlist)
        {
            return dal.DeleteList(SysMsgIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.SysMsg GetModel(int SysMsgId)
        {

            return dal.GetModel(SysMsgId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.SysMsg GetModelByCache(int SysMsgId)
        {

            string CacheKey = "SysMsgModel-" + SysMsgId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SysMsgId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.SysMsg)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.SysMsg> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.SysMsg> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.SysMsg> modelList = new List<Maticsoft.WeChat.Model.Core.SysMsg>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.SysMsg model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}


	    #region  ExtensionMethod
        /// <summary>
        /// 获取被关注回复消息
        /// </summary>
        /// <returns></returns>
	    public Maticsoft.WeChat.Model.Core.PostMsg GetSubscribeMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg)
        {
            string CacheKey = "GetSubscribeMsg-" + msg.OpenId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.WeChat.Model.Core.PostMsg postMsgModel = new Model.Core.PostMsg();
                    List<Maticsoft.WeChat.Model.Core.SysMsg> sysMsgList = GetSubscribeMsg(msg.OpenId);
                    if (sysMsgList != null && sysMsgList.Count>0)
                    {
                        postMsgModel.MsgType = sysMsgList[0].MsgType;
                        postMsgModel.CreateTime = DateTime.Now;
                        if (sysMsgList[0].MsgType == "text")
                        {
                            string commandStr = Maticsoft.WeChat.BLL.Core.Command.GetCommandStr(msg.OpenId);
                            postMsgModel.Description = Maticsoft.WeChat.BLL.Core.Utils.GetDescUrl(msg, sysMsgList[0].Description) + "\n\n" + commandStr;
                        }
                        //处理图文信息
                        if (sysMsgList[0].MsgType == "news")
                        {
                            postMsgModel.ArticleCount = sysMsgList.Count;
                            Maticsoft.WeChat.Model.Core.MsgItem item = null;
                            for (int i=0;i<sysMsgList.Count;i++)
                            {
                                item = new Model.Core.MsgItem();
                                item.Title = sysMsgList[i].Title;
                                item.Description = sysMsgList[i].Description;
                                if (i == 0)
                                {
                                    item.PicUrl = String.IsNullOrWhiteSpace(sysMsgList[i].PicUrl) ? "" :
                                        "http://" + Common.Globals.DomainFullName + String.Format(sysMsgList[i].PicUrl, "N_");
                                }
                                else
                                {
                                    item.PicUrl = String.IsNullOrWhiteSpace(sysMsgList[i].PicUrl) ? "" :
                                                                          "http://" + Common.Globals.DomainFullName + String.Format(sysMsgList[i].PicUrl, "T_");
                                }
                                item.Url = Maticsoft.WeChat.BLL.Core.Utils.GetWCUrl(msg, sysMsgList[i].Url);
                                postMsgModel.MsgItems.Add(item);
                            }
                        }
                    }
                    objModel = postMsgModel;
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.PostMsg)objModel;
          
        }

        /// <summary>
        /// 获取自动回复消息
        /// </summary>
        /// <returns></returns>
        public Maticsoft.WeChat.Model.Core.PostMsg GetAutoReplyMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg)
        {
            string CacheKey = "GetAutoReplyMsg-" + msg.OpenId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.WeChat.Model.Core.PostMsg postMsgModel = new Model.Core.PostMsg();
                    List<Maticsoft.WeChat.Model.Core.SysMsg> sysMsgList = GetAutoReplyMsg(msg.OpenId);
                    if (sysMsgList != null && sysMsgList.Count > 0)
                    {
                        postMsgModel.MsgType = sysMsgList[0].MsgType;
                        postMsgModel.CreateTime = DateTime.Now;
                        if (sysMsgList[0].MsgType == "text")
                        {
                            string commandStr = Maticsoft.WeChat.BLL.Core.Command.GetCommandStr(msg.OpenId);
                            postMsgModel.Description = Maticsoft.WeChat.BLL.Core.Utils.GetDescUrl(msg, sysMsgList[0].Description) + "\n" + commandStr;
                        }
                        //处理图文信息
                        if (sysMsgList[0].MsgType == "news")
                        {
                            postMsgModel.ArticleCount = sysMsgList.Count;
                            Maticsoft.WeChat.Model.Core.MsgItem item = null;
                            for (int i = 0; i < sysMsgList.Count; i++)
                            {
                                item = new Model.Core.MsgItem();
                                item.Title = sysMsgList[i].Title;
                                item.Description = sysMsgList[i].Description;
                                if (i == 0)
                                {
                                    item.PicUrl = String.IsNullOrWhiteSpace(sysMsgList[i].PicUrl) ? "" :
                                        "http://" + Common.Globals.DomainFullName + String.Format(sysMsgList[i].PicUrl, "N_");
                                }
                                else
                                {
                                    item.PicUrl = String.IsNullOrWhiteSpace(sysMsgList[i].PicUrl) ? "" :
                                                                          "http://" + Common.Globals.DomainFullName + String.Format(sysMsgList[i].PicUrl, "T_");
                                }
                                item.Url =Maticsoft.WeChat.BLL.Core.Utils.GetWCUrl(msg, sysMsgList[i].Url);
                                postMsgModel.MsgItems.Add(item);
                            }
                        }
                    }
                    objModel = postMsgModel;
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.PostMsg)objModel;
          
        }
        /// <summary>
        /// 获取被关注回复消息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public List<Maticsoft.WeChat.Model.Core.SysMsg> GetSubscribeMsg(string openId)
        {
            return GetSysMsgs(1, openId);
        }
        /// <summary>
        /// 获取自动回复消息
        /// </summary>
        /// <returns></returns>
        public List<Maticsoft.WeChat.Model.Core.SysMsg> GetAutoReplyMsg(string openId)
        {
            return GetSysMsgs(2, openId);
        }

        public List<Maticsoft.WeChat.Model.Core.SysMsg> GetSysMsgs(int type, string openId)
        {
            DataSet ds = dal.GetSysMsgs(type, openId);
            return DataTableToList(ds.Tables[0]);
        }

        public int AddEx(Maticsoft.WeChat.Model.Core.SysMsg msgModel)
        {
            //删除所有的该类型数据
            DeleteEx(msgModel.MsgType, msgModel.SysType, msgModel.OpenId);
            return dal.Add(msgModel);
        }
        /// <summary>
        /// 删除系统消息
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool DeleteEx(string msgType, int type, string openId)
        {
            return dal.DeleteEx(msgType, type, openId);
        }

          public bool DeleteEx(int msgId, int type)
          {
              return dal.DeleteEx(msgId, type);
          }

	    #endregion  ExtensionMethod
	}
}

