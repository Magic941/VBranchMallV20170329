/**
* RequestMsg.cs
*
* 功 能： N/A
* 类 名： RequestMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:17   N/A    初版
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
using System.Xml;
using Maticsoft.WeChat.IDAL.Core;
using System.Text;
namespace Maticsoft.WeChat.BLL.Core
{
    /// <summary>
    /// RequestMsg
    /// </summary>
    public partial class RequestMsg
    {
        private readonly IRequestMsg dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IRequestMsg)new WeChat.SQLServerDAL.Core.RequestMsg() : null;
        public RequestMsg()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long UserMsgId)
        {
            return dal.Exists(UserMsgId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.WeChat.Model.Core.RequestMsg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.RequestMsg model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long UserMsgId)
        {

            return dal.Delete(UserMsgId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string UserMsgIdlist)
        {
            return dal.DeleteList(UserMsgIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.RequestMsg GetModel(long UserMsgId)
        {

            return dal.GetModel(UserMsgId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.RequestMsg GetModelByCache(long UserMsgId)
        {

            string CacheKey = "RequestMsgModel-" + UserMsgId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(UserMsgId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.RequestMsg)objModel;
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
        public List<Maticsoft.WeChat.Model.Core.RequestMsg> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.RequestMsg> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.RequestMsg> modelList = new List<Maticsoft.WeChat.Model.Core.RequestMsg>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.RequestMsg model;
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

        #endregion  BasicMethod

        #region  ExtensionMethod
        /// <summary>
        /// 获取微信信息。转化为Model
        /// </summary>
        /// <param name="postStr"></param>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.RequestMsg GetRequestMsg(string postStr)
        {
            Maticsoft.WeChat.Model.Core.RequestMsg requestModel = new Maticsoft.WeChat.Model.Core.RequestMsg();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(postStr);
                XmlElement rootElement = doc.DocumentElement;
                if (rootElement == null)
                {
                    return requestModel;
                }
                XmlNode msgType = rootElement.SelectSingleNode("MsgType");
                requestModel.OpenId = rootElement.SelectSingleNode("ToUserName").InnerText;
                requestModel.UserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                requestModel.CreateTime = Utils.UnixTimeToTime(rootElement.SelectSingleNode("CreateTime").InnerText);
                requestModel.MsgType = msgType.InnerText;
                switch (requestModel.MsgType)
                {
                    case "text":
                        requestModel.Description = rootElement.SelectSingleNode("Content").InnerText;
                        break;
                    case "location":
                        requestModel.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                        requestModel.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                        requestModel.Scale = Common.Globals.SafeInt(rootElement.SelectSingleNode("Scale").InnerText, 0);
                        requestModel.Description = rootElement.SelectSingleNode("Label").InnerText;
                        break;
                    case "image":
                        requestModel.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                        break;
                    case "link":
                        requestModel.Title = rootElement.SelectSingleNode("Title").InnerText;
                        requestModel.Description = rootElement.SelectSingleNode("Description").InnerText;
                        requestModel.Url = rootElement.SelectSingleNode("Url").InnerText;
                        break;
                    case "event":
                        requestModel.Event = rootElement.SelectSingleNode("Event").InnerText;
                        if (requestModel.Event == "LOCATION")
                        {
                            requestModel.Location_X = rootElement.SelectSingleNode("Latitude").InnerText;
                            requestModel.Location_Y = rootElement.SelectSingleNode("Longitude").InnerText;
                            requestModel.Precision =Common.Globals.SafeDecimal(rootElement.SelectSingleNode("Precision").InnerText,0);
                        }
                        else
                        {
                            requestModel.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                        }
                        break;
                    case "voice":
                        requestModel.Description = rootElement.SelectSingleNode("MediaId").InnerText;
                        if (rootElement.SelectSingleNode("Recognition") != null)
                        {
                            requestModel.Description = rootElement.SelectSingleNode("Recognition").InnerText;
                        }
                        break;
                    default:
                        requestModel.Description = rootElement.SelectSingleNode("Content").InnerText;
                        break;
                }
                return requestModel;
            }
            catch (Exception ex )
            {
                throw ex;
            }
         
          
        }
        /// <summary>
        /// 添加消息数据
        /// </summary>
        /// <param name="postStr"></param>
        /// <returns></returns>
        /// 
        public static bool AddMsg(Maticsoft.WeChat.Model.Core.RequestMsg msgModel)
        {
            Maticsoft.WeChat.BLL.Core.RequestMsg requestMsgBll = new RequestMsg();
            try
            {
                if (msgModel.MsgType == "event")
                {
                    Maticsoft.WeChat.Model.Core.User userModel = new Model.Core.User();
                    Maticsoft.WeChat.BLL.Core.User userBll = new User();
                    switch (msgModel.Event)
                    {
                        //关注事件
                        case "subscribe":
                            userModel.OpenId = msgModel.OpenId;
                            userModel.UserName = msgModel.UserName;
                            userModel.CreateTime = DateTime.Now;
                            userModel.Status = 1;
                            msgModel.Description = "新用户添加关注";
                            userBll.AddUser(userModel);
                            break;
                        //取消关注事件
                        case "unsubscribe":
                            msgModel.Description = "用户取消关注";
                            userBll.CancelUser(msgModel.OpenId, msgModel.UserName);
                            break;
                        //自定义点击事件
                        case "CLICK":
                            Maticsoft.WeChat.Model.Core.Action actionModel = Core.Action.GetActionByKey(msgModel.EventKey);
                            msgModel.Description = actionModel == null ? "未知事件" : actionModel.Name;
                            break;
                            //地理位置消息
                        case "LOCATION":
                            msgModel.Description = "获取用户位置信息";
                            //添加地理位置信息
                            Maticsoft.WeChat.Model.Core.Location cationModel = new Model.Core.Location();
                            Maticsoft.WeChat.BLL.Core.Location locationBll = new BLL.Core.Location();
                            cationModel.CreateTime = DateTime.Now;
                            cationModel.Latitude =Common.Globals.SafeDecimal(msgModel.Location_X,0);
                            cationModel.Longitude =Common.Globals.SafeDecimal( msgModel.Location_Y,0);
                            cationModel.Precision = msgModel.Precision;
                            cationModel.OpenId = msgModel.OpenId;
                            cationModel.UserName = msgModel.UserName;
                            locationBll.Add(cationModel);
                            break;
                           
                        default:
                            break;
                    }
                }
                long result= requestMsgBll.Add(msgModel) ;
                if (result > 0)
                {
                      Maticsoft.WeChat.BLL.Core.User userBll = new User();
                      userBll.UpdateMsgTime(msgModel.OpenId, msgModel.UserName, DateTime.Now);
                      return true;
                }
                return false;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }


        public List<Maticsoft.WeChat.Model.Core.RequestMsg> GetMsgList(string openId, string startdate, string enddate, bool isShowEvent,string keyword, int startIndex, int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            if (!String.IsNullOrWhiteSpace(startdate) && Common.PageValidate.IsDateTime(startdate))
            {
                strWhere.AppendFormat(" and CreateTime >='" + startdate + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(enddate) && Common.PageValidate.IsDateTime(enddate))
            {
                strWhere.AppendFormat(" and CreateTime <='" + enddate + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" and  Description like '%{0}%' ", keyword);
            }
            if (!isShowEvent)
            {
                strWhere.Append(" and MsgType <> 'event' ");
            }
            DataSet ds = GetListByPage(strWhere.ToString(), " CreateTime desc ", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        public int GetCount(string openId, string startdate, string enddate, bool isShowEvent,string keyword)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            if (!String.IsNullOrWhiteSpace(startdate) && Common.PageValidate.IsDateTime(startdate))
            {
                strWhere.AppendFormat(" and CreateTime >='" + startdate + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(enddate) && Common.PageValidate.IsDateTime(enddate))
            {
                strWhere.AppendFormat(" and CreateTime <='" + enddate + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" and  Description like '%{0}%' ", keyword);
            }
            if (!isShowEvent)
            {
                strWhere.Append(" and MsgType <> 'event' ");
            }
            return GetRecordCount(strWhere.ToString());
        }
        #endregion  ExtensionMethod
    }
}

