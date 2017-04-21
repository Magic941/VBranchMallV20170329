/**
* PostMsg.cs
*
* 功 能： N/A
* 类 名： PostMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:13   N/A    初版
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
using Maticsoft.WeChat.IDAL.Core;
using Maticsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using Maticsoft.Json.Conversion;
namespace Maticsoft.WeChat.BLL.Core
{
    /// <summary>
    /// PostMsg
    /// </summary>
    public partial class PostMsg
    {
        private readonly IPostMsg dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IPostMsg)new WeChat.SQLServerDAL.Core.PostMsg() : null;
        public PostMsg()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long PostMsgId)
        {
            return dal.Exists(PostMsgId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.WeChat.Model.Core.PostMsg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.PostMsg model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long PostMsgId)
        {

            return dal.Delete(PostMsgId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PostMsgIdlist)
        {
            return dal.DeleteList(PostMsgIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.PostMsg GetModel(long PostMsgId)
        {

            return dal.GetModel(PostMsgId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.PostMsg GetModelByCache(long PostMsgId)
        {

            string CacheKey = "PostMsgModel-" + PostMsgId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(PostMsgId);
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
        public List<Maticsoft.WeChat.Model.Core.PostMsg> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.PostMsg> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.PostMsg> modelList = new List<Maticsoft.WeChat.Model.Core.PostMsg>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.PostMsg model;
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
        //返回回复信息的XML形式
        public static string GetPostMsgXML(Maticsoft.WeChat.Model.Core.PostMsg msgModel)
        {
            string startXml = String.Format(" <xml> <ToUserName><![CDATA[{0}]]></ToUserName> <FromUserName><![CDATA[{1}]]></FromUserName> <CreateTime>{2}</CreateTime>",
                msgModel.UserName, msgModel.OpenId, Utils.ConvertDateTimeInt(msgModel.CreateTime));
            string bodyXml = "";
          
            switch (msgModel.MsgType)
            {
                case "text":
                    bodyXml = String.Format("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{0}]]></Content>",
                                            msgModel.Description.Replace("target=\"_self\"", "").Replace("target=\"_blank\"", ""));
                    break;
                case "music":
                    bodyXml = "<MsgType><![CDATA[music]]></MsgType><Music>" +
                              "<Title><![CDATA[{0}]]></Title>" +
                              "<Description><![CDATA[{1}]]></Description>" +
                              "<MusicUrl><![CDATA[{2}]]></MusicUrl>" +
                              "<HQMusicUrl><![CDATA[{3}]]></HQMusicUrl>" +
                              "</Music>";
                    bodyXml = String.Format(bodyXml, msgModel.Title, msgModel.Description, msgModel.MusicUrl, msgModel.HQMusicUrl);
                    break;
                case "news":
                    bodyXml = String.Format(" <MsgType><![CDATA[news]]></MsgType><ArticleCount>{0}</ArticleCount><Articles>", msgModel.ArticleCount);
                    string itemXml = "<item><Title><![CDATA[{0}]]></Title>" +
                               "<Description><![CDATA[{1}]]></Description>" +
                               "<PicUrl><![CDATA[{2}]]></PicUrl>" +
                               "<Url><![CDATA[{3}]]></Url></item>";
                    if (msgModel.MsgItems.Count > 0)
                    {
                        foreach (var item in msgModel.MsgItems)
                        {
                            bodyXml = bodyXml +
                                      String.Format(itemXml, item.Title, item.Description, item.PicUrl, item.Url);
                        }
                    }
                    bodyXml = bodyXml + " </Articles>";
                    break;
                case "voice":
                    bodyXml = String.Format("<MsgType><![CDATA[voice]]></MsgType><Voice> <MediaId><![CDATA[{0}]]></MediaId> </Voice>", msgModel.MediaId);
                    break;
                    //人工客服消息
                case "transfer_customer_service":
                    bodyXml = "<MsgType><![CDATA[transfer_customer_service]]></MsgType></xml>";
                    return startXml + bodyXml;
                    break;
                default:
                    bodyXml = String.Format("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{0}]]></Content>",
                                       msgModel.Description.Replace("target=\"_self\"", "").Replace("target=\"_blank\"", ""));
                    break;
            }
            string endXml = "<FuncFlag>0</FuncFlag></xml>";

            return startXml + bodyXml + endXml;
        }

        /// <summary>
        /// 根据值获取关键字回复消息
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.PostMsg GetPostMsg(string value, string openId)
        {
            string CacheKey = "GetPostMsg-" + value;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    //处理关键字回复消息
                    Maticsoft.WeChat.BLL.Core.PostMsg postMsgBll = new PostMsg();
                    Maticsoft.WeChat.BLL.Core.KeyValue valueBll = new KeyValue();
                    Maticsoft.WeChat.Model.Core.KeyValue valueModel= valueBll.GetValue(value,openId);
                    if (valueModel != null)
                    {
                        objModel = postMsgBll.GetRanMsg(valueModel.RuleId);
                    }
                    if (objModel != null)
                    {
                        int ModelCache = 30;
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache),
                                                            TimeSpan.Zero);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return (Maticsoft.WeChat.Model.Core.PostMsg)objModel;
        }
        /// <summary>
        /// 执行事件消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.PostMsg RunEvevtMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg)
        {
            Maticsoft.WeChat.BLL.Core.SysMsg sysMsgBll=new SysMsg();
            Maticsoft.WeChat.Model.Core.PostMsg postMsgModel = new Model.Core.PostMsg();
            switch (msg.Event)
            {
                    //关注消息
                case "subscribe":
                    postMsgModel=sysMsgBll.GetSubscribeMsg(msg);
                    break;
                    //取消关注消息
                case "unsubscribe":

                    break;
            }
            return postMsgModel;
        }
        /// <summary>
        /// 处理坐标位置信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.PostMsg RunLocationMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg)
        {
            Maticsoft.WeChat.Model.Core.PostMsg postMsgModel = new Model.Core.PostMsg();
            return postMsgModel;
        }
        /// <summary>
        /// 处理链接地址消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.PostMsg RunLinkMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg)
        {
            Maticsoft.WeChat.Model.Core.PostMsg postMsgModel = new Model.Core.PostMsg();
            return postMsgModel;
        }
        /// <summary>
        /// 处理图片消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.PostMsg RunImageMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg)
        {
            Maticsoft.WeChat.Model.Core.PostMsg postMsgModel = new Model.Core.PostMsg();
            return postMsgModel;
        }
        /// <summary>
        /// 获取自动回复消息
        /// </summary>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.PostMsg GetAutoMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg,bool IsTransfer=false)
        {
            Maticsoft.WeChat.BLL.Core.SysMsg sysMsgBll = new SysMsg();
            //添加未有效回复消息 (暂时排除事件消息)
            if (msg.MsgType != "event" && !IsTransfer)
            {
                Maticsoft.WeChat.BLL.Core.NoReplyMsg nomsgBll = new NoReplyMsg();
                nomsgBll.AddMsg(msg);
            }
            if (!IsTransfer)
            {
                return sysMsgBll.GetAutoReplyMsg(msg);
            }
            else
            {
                Maticsoft.WeChat.Model.Core.PostMsg postMsg = new Model.Core.PostMsg();
                postMsg.MsgType = "transfer_customer_service";
                return postMsg;
            }
        }
       

        
        /// <summary>
        /// 发送回复消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string  SendPostMsg(Maticsoft.WeChat.Model.Core.RequestMsg msg)
        {
            //根据接收信息 获取发送消息
            string xmlStr = "";
            Maticsoft.WeChat.Model.Core.PostMsg postMsg = new Model.Core.PostMsg();
            switch (msg.MsgType)
            {
                   // 文本
                case "text":
                  postMsg= GetPostMsg(msg.Description,msg.OpenId);
                    break;
                case "event":
                   postMsg= RunEvevtMsg(msg);
                    break;
                case "location":
                    postMsg = RunLocationMsg(msg);
                    break;
                case "link":
                    postMsg = RunLinkMsg(msg);
                    break;
                case "image":
                    postMsg = RunImageMsg(msg);
                    break;
                default:
                    postMsg = GetPostMsg(msg.Description, msg.OpenId);
                    break;
            }
            if (postMsg != null && !String.IsNullOrWhiteSpace(postMsg.MsgType))
            {
                postMsg.OpenId = msg.OpenId;
                postMsg.UserName = msg.UserName;
                xmlStr =  GetPostMsgXML(postMsg);
            }
            return xmlStr;
        }
        /// <summary>
        /// 根据关键字规则 随机获取当前的回复
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public Maticsoft.WeChat.Model.Core.PostMsg GetRanMsg(int ruleId)
        {
            return dal.GetRanMsg(ruleId);
        }


     
        #endregion  ExtensionMethod


    }
}

