using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Web.Components.Setting.CMS;

namespace Maticsoft.Web.Handlers.CMS
{
    public class HttpToStaticHandler : IHttpHandler
    {
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_KEY_DATA = "DATA";

        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_ERROR = "ERROR";

        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;
        Maticsoft.BLL.CMS.Content bll = new BLL.CMS.Content();
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
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
                    case "HttpToStatic":
                        HttpToStatic(context);
                        break;
                    case "GenerateHtml":
                        GenerateHtml(context);
                        break;
                    case "DeleteTask":
                        DeleteTask();
                        break;
                    case "ContinueTask":
                        ContinueTask(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);
                json.Put(POLL_KEY_DATA, ex);
                context.Response.Write(json.ToString());
            }
        }

        protected void HttpToStatic(HttpContext context)
        {
            IDictionaryEnumerator de = context.Cache.GetEnumerator();
            ArrayList listCache = new ArrayList();
            while (de.MoveNext())
            {
                listCache.Add(de.Key.ToString());
            }
            foreach (string key in listCache)
            {
                context.Cache.Remove(key);
            }
            JsonObject json = new JsonObject();
            StringBuilder strWhere = new StringBuilder();
            int classId = Globals.SafeInt(context.Request.Form["Cid"], 0);
            strWhere.AppendFormat(" State=0");
            if (classId > 0)
            {
                strWhere.AppendFormat(" and ClassID =" + classId);
            }
            if (!String.IsNullOrWhiteSpace(context.Request.Form["From"]) && Common.PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                strWhere.AppendFormat(" and  CreatedDate >'" + context.Request.Form["From"] + "' ");
            }
            if (!String.IsNullOrWhiteSpace(context.Request.Form["To"]) && Common.PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                strWhere.AppendFormat(" and CreatedDate <'" + context.Request.Form["To"] + "' ");
            }
            List<Maticsoft.Model.CMS.Content> list = bll.GetModelList(strWhere.ToString());
            #region 循环静态化
            //静态化之前先清除表任务
            taskBll.DeleteArticle();

            TaskList = new List<Model.SysManage.TaskQueue>();
            if (list != null && list.Count > 0)
            {
                Maticsoft.Model.SysManage.TaskQueue taskModel = null;
                int i = 1;
                foreach (Maticsoft.Model.CMS.Content item in list)
                {
                    taskModel = new Model.SysManage.TaskQueue();
                    taskModel.ID = i;
                    taskModel.TaskId = item.ContentID;
                    taskModel.Status = 0;
                    taskModel.Type = (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.Article;
                    if (taskBll.Add(taskModel))
                    {
                        TaskList.Add(taskModel);
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #endregion
            json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);
            json.Put(POLL_KEY_DATA, list.Count);
            context.Response.Write(json.ToString());
        }
        /// <summary>
        /// 生成HTML页面
        /// </summary>
        /// <param name="context"></param>
        protected void GenerateHtml(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string area = BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
            int TaskId = Globals.SafeInt(context.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue item = TaskList.FirstOrDefault(c => c.ID == TaskId);
            if (item != null)
            {
                string requestUrl = "";//静态化请求地址
                string saveUrl = PageSetting.GetCMSUrl(item.TaskId);
                if (area == "CMS")
                {
                    requestUrl = "/Article/Details/" + item.TaskId;
                }
                else
                {
                    requestUrl = "/CMS/Article/Details/" + item.TaskId;
                }
                if (!String.IsNullOrWhiteSpace(requestUrl) && !String.IsNullOrWhiteSpace(saveUrl))
                {
                    if (Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(requestUrl, saveUrl))
                    {
                        item.RunDate = DateTime.Now;
                        item.Status = 1;
                        taskBll.Update(item);
                        json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);
                    }
                    else
                    {
                        json.Put(POLL_KEY_STATUS, POLL_STATUS_FAILED);
                    }
                }
                context.Response.Write(json.ToString());
            }
        }
        /// <summary>
        /// 继续任务
        /// </summary>
        protected void ContinueTask(HttpContext context)
        {
            TaskList = taskBll.GetContinueTask( (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.Article);
            JsonObject json = new JsonObject();
            json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);
            Maticsoft.Model.SysManage.TaskQueue item = TaskList.First();
            json.Put(POLL_KEY_DATA, item.ID);
            context.Response.Write(json.ToString());
        }

        //删除任务（不删除未完成任务）
        protected void DeleteTask()
        {
            taskBll.DeleteArticle();
        }


    }
}