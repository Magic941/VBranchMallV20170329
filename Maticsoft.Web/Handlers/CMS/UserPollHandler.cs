/**
* UserPollHandler.cs
*
* 功 能： [N/A]
* 类 名： UserPollHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/23 17:34:31  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Web;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.CMS
{
    public class UserPollHandler
    {
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_KEY_DATA = "DATA";

        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_ERROR = "ERROR";

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //安全起见, 所有产品相关Ajax请求为POST模式
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                switch (action)
                {
                    case "userPoll":
                        UserPoll(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                json.Put(POLL_KEY_STATUS, POLL_STATUS_ERROR);
                json.Put(POLL_KEY_DATA, ex);
                context.Response.Write(json.ToString());
            }
        }

        private string UserPoll(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int userId = Convert.ToInt32(context.Request.Form["UID"]);
            string selectOption = context.Request.Form["Option"];
            string FID = context.Request.Form["FID"];

            if (context.Request.Cookies["vote" + FID] != null)
            {
                HttpCookie httpCookie = context.Request.Cookies["vote" + FID];
                if (httpCookie.Values["voteid"].ToString() != "" || httpCookie.Values["voteid"].ToString() != null)
                {
                    json.Put(POLL_KEY_STATUS, POLL_STATUS_FAILED);
                }
            }
            BLL.Poll.UserPoll userPoll = new BLL.Poll.UserPoll();

            string[] topicInfo = selectOption.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Maticsoft.Model.Poll.UserPoll modelup = null;
            for (int i = 0; i < topicInfo.Length; i++)
            {
                string[] optoins = topicInfo[i].Split(new char['_'], StringSplitOptions.RemoveEmptyEntries);
                modelup.CreatTime = DateTime.Now;
                modelup.TopicID = int.Parse(optoins[0]);
                modelup.UserID = userId;
                modelup.UserIP =context.Request.UserHostAddress;
                modelup.OptionID = int.Parse(optoins[1]);
                userPoll.Add(modelup);
            }
            json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);
            return json.ToString();
        }
    }
}