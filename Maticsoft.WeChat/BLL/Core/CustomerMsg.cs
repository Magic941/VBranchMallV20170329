/**  版本信息模板在安装目录下，可自行修改。
* CustomerMsg.cs
*
* 功 能： N/A
* 类 名： CustomerMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/21 20:52:18   N/A    初版
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
using System.IO;
using System.Net;
using System.Text;
using Maticsoft.Json.Conversion;
using Maticsoft.Json;
namespace Maticsoft.WeChat.BLL.Core
{
    /// <summary>
    /// CustomerMsg
    /// </summary>
    public partial class CustomerMsg
    {
        private readonly ICustomerMsg dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (ICustomerMsg)new WeChat.SQLServerDAL.Core.CustomerMsg() : null;//暂时预留
        public CustomerMsg()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long MsgId)
        {
            return dal.Exists(MsgId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(Maticsoft.WeChat.Model.Core.CustomerMsg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.CustomerMsg model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long MsgId)
        {
            return dal.Delete(MsgId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string MsgIdlist)
        {
            return dal.DeleteList(MsgIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.CustomerMsg GetModel(long MsgId)
        {

            return dal.GetModel(MsgId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.CustomerMsg GetModelByCache(long MsgId)
        {

            string CacheKey = "CustomerMsgModel-" + MsgId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(MsgId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.CustomerMsg)objModel;
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
        public List<Maticsoft.WeChat.Model.Core.CustomerMsg> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.CustomerMsg> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.CustomerMsg> modelList = new List<Maticsoft.WeChat.Model.Core.CustomerMsg>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.CustomerMsg model;
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

        #region 客服消息发送
        public static string GetJsonMsg(Maticsoft.WeChat.Model.Core.CustomerMsg msgModel, string UserName)
        {
            JsonObject json = new JsonObject();
            json.Accumulate("touser", UserName);
            switch (msgModel.MsgType)
            {
                case "text":
                    json.Accumulate("msgtype", msgModel.MsgType);
                    JsonObject jsontext = new JsonObject();
                    jsontext.Accumulate("content", msgModel.Description);
                    json.Accumulate("text", jsontext);
                    break;
                case "news":
                    json.Accumulate("msgtype", msgModel.MsgType);
                    JsonObject news = new JsonObject();
                    JsonArray newsArry = new JsonArray();
                    JsonObject itemObj = null;
                    int i = 0;
                    string PicUrl = "";
                    foreach (var item in msgModel.MsgItems)
                    {
                        
                        if (i == 0)
                        {
                            PicUrl = String.IsNullOrWhiteSpace(item.PicUrl) ? "" :
                                "http://" + Common.Globals.DomainFullName + String.Format(item.PicUrl, "N_");
                        }
                        else
                        {
                            PicUrl = String.IsNullOrWhiteSpace(item.PicUrl) ? "" :
                                                                        "http://" + Common.Globals.DomainFullName + String.Format(item.PicUrl, "T_");
                        }
                        itemObj = new JsonObject();
                        itemObj.Accumulate("title", item.Title);
                        itemObj.Accumulate("description", item.Description);
                        itemObj.Accumulate("url", Maticsoft.WeChat.BLL.Core.Utils.GetWCUrl(msgModel.OpenId, UserName, item.Url));
                        itemObj.Accumulate("picurl", PicUrl);
                        newsArry.Add(itemObj);
                        i++;
                    }
                    news.Accumulate("articles", newsArry);
                    json.Accumulate("news", news);
                    break;
                case "video":
                      json.Accumulate("msgtype", msgModel.MsgType);
                      JsonObject video = new JsonObject();
                      video.Accumulate("media_id", msgModel.MediaId);
                      video.Accumulate("title", msgModel.Title);
                      video.Accumulate("description", msgModel.Description);
                      json.Accumulate("video", video);
                    break;
                case "voice":
                      json.Accumulate("msgtype", msgModel.MsgType);
                      JsonObject voice = new JsonObject();
                      voice.Accumulate("media_id", msgModel.MediaId);
                      json.Accumulate("voice", voice);
                    break;
                default:
                    json.Accumulate("msgtype", "text");
                    JsonObject jsontext1 = new JsonObject();
                    jsontext1.Accumulate("content", msgModel.Description);
                    json.Accumulate("text", jsontext1);
                    break;
            }
            return json.ToString();
        }

        public static void SendCustomMsg(Maticsoft.WeChat.Model.Core.CustomerMsg msgModel, string access_token, List<String> UserList)
        {
            if (UserList.Count == 0)
            {
                return;
            }
            Maticsoft.WeChat.BLL.Core.CustomerMsg msgBll = new CustomerMsg();
            Maticsoft.WeChat.BLL.Core.CustUserMsg usermsgBll = new CustUserMsg();
            Maticsoft.WeChat.Model.Core.CustUserMsg usermsgModel = null;
            long msgId = msgBll.AddEx(msgModel);
            StreamReader reader = null;
            Stream newStream = null;
            string posturl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + access_token;

            try
            {
                //循环发送 
                foreach (var user in UserList)
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(posturl);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Method = "POST";
                    string postMsg = GetJsonMsg(msgModel, user);
                    byte[] postdata = Encoding.GetEncoding("UTF-8").GetBytes(postMsg);
                    request.ContentLength = postdata.Length;
                    newStream = request.GetRequestStream();
                    newStream.Write(postdata, 0, postdata.Length);
                    HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                    reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    string content = reader.ReadToEnd();//得到结果
                    Maticsoft.Json.JsonObject jsonObject = JsonConvert.Import<JsonObject>(content);
                    int code = Common.Globals.SafeInt(jsonObject["errcode"].ToString(), 0);
                    //如果发送成功  则写入数据库
                    if (code == 0)
                    {
                        usermsgModel = new Model.Core.CustUserMsg();
                        usermsgModel.MsgId = msgId;
                        usermsgModel.UserName = user;
                        usermsgBll.Add(usermsgModel);
                    }
                    newStream.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
        }


        #endregion

        #region  高级接口消息推送
        /// <summary>
        /// 高级接口消息推送 Json 数据
        /// </summary>
        /// <param name="msgModel"></param>
        /// <returns></returns>
        private static string GetAdvJsonMsg(List<Maticsoft.WeChat.Model.Core.MsgItem> itemList)
        {
            JsonObject json = new JsonObject();
         
                    JsonArray newsArry = new JsonArray();
                    JsonObject itemObj = null;
                    int i = 0;
                    foreach (var item in itemList)
                    {
                        //itemObj = new JsonObject();
                        //itemObj.Accumulate("thumb_media_id", item.Title);
                        //itemObj.Accumulate("author", item.Author);
                        //itemObj.Accumulate("title", item.Title);
                        //itemObj.Accumulate("content", item.Description);
                        //itemObj.Accumulate("content_source_url",  "http://" + Common.Globals.DomainFullName + item.Url);
                        //itemObj.Accumulate("digest", item.Digest);
                        newsArry.Add(itemObj);
                        i++;
                    }
                    json.Accumulate("articles", newsArry);
               
            return json.ToString();
        }

        public static string GetAdvMedia(Maticsoft.WeChat.Model.Core.CustomerMsg msgModel)
        {
            return "";
        }

        #endregion 

        public List<Maticsoft.WeChat.Model.Core.CustomerMsg> GetMsgList(string openId, string startdate, string enddate, string keyword = "")
        {
            StringBuilder strWhere = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(openId))
            {
                strWhere.AppendFormat(" OpenId='{0}'", openId);
            }

            if (!String.IsNullOrWhiteSpace(startdate) && Common.PageValidate.IsDateTime(startdate))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime >='" + startdate + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(enddate) && Common.PageValidate.IsDateTime(enddate))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime <='" + enddate + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  Description like '%{0}%' ", keyword);
            }
            DataSet ds = GetList(-1, strWhere.ToString(), " CreateTime desc ");
            return DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.WeChat.Model.Core.CustomerMsg> GetMsgList(string openId, string startdate, string enddate, string keyword, int startIndex, int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(openId))
            {
                strWhere.AppendFormat(" OpenId='{0}'", openId);
            }

            if (!String.IsNullOrWhiteSpace(startdate) && Common.PageValidate.IsDateTime(startdate))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime >='" + startdate + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(enddate) && Common.PageValidate.IsDateTime(enddate))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime <='" + enddate + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  Description like '%{0}%' ", keyword);
            }

            DataSet ds = GetListByPage(strWhere.ToString(), " CreateTime desc ", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        public int GetCount(string openId, string startdate, string enddate, string keyword)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(openId))
            {
                strWhere.AppendFormat(" OpenId='{0}'", openId);
            }
            if (!String.IsNullOrWhiteSpace(startdate) && Common.PageValidate.IsDateTime(startdate))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime >='" + startdate + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(enddate) && Common.PageValidate.IsDateTime(enddate))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime <='" + enddate + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  Description like '%{0}%' ", keyword);
            }
            return GetRecordCount(strWhere.ToString());
        }

        public long AddEx(Maticsoft.WeChat.Model.Core.CustomerMsg msgModel)
        {
            long msgId = Add(msgModel);

            if (msgModel.MsgType == "news")
            {
                Maticsoft.WeChat.BLL.Core.PostMsgItem postItemBll = new PostMsgItem();
                Maticsoft.WeChat.Model.Core.PostMsgItem model = null;
                foreach (var item in msgModel.MsgItems)
                {
                    model = new Model.Core.PostMsgItem();
                    model.ItemId = item.ItemId;
                    model.PostMsgId = Common.Globals.SafeInt(msgId, 0);
                    model.Type = 1;
                    postItemBll.Add(model);
                }
            }
            return msgId;
        }

        public bool DeleteListEx(string ids) 
        {
            return dal.DeleteListEx(ids);                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        }
        #endregion  ExtensionMethod
    }
}

