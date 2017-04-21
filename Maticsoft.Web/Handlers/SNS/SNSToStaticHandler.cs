using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model.SysManage;
using Maticsoft.Json;
using System.Collections;
using Maticsoft.Common;
using System.Text;
using Maticsoft.Web.Components.Setting.SNS;

namespace Maticsoft.Web.Handlers.SNS
{
    public class SNSToStaticHandler : IHttpHandler
    {
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_KEY_DATA = "DATA";

        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_ERROR = "ERROR";

        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;
        Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
        Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
        BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);
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
                        DeleteTask(context);
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
            int type = Globals.SafeInt(context.Request.Form["Type"], -1);
            if (type == (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSProduct)//商品静态化
            {
                ProductToStatic(context);
            }
            if (type == (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSPhoto)//图片静态化
            {
                PhotoToStatic(context);
            }

        }
        /// <summary>
        /// 静态化产品
        /// </summary>
        /// <param name="context"></param>
        protected void ProductToStatic(HttpContext context)
        {
            JsonObject json = new JsonObject();
            StringBuilder strWhere = new StringBuilder();
            //strWhere.AppendFormat(" Status=1");
            if (!String.IsNullOrWhiteSpace(context.Request.Form["From"]) && Common.PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append("and");
                }
                strWhere.AppendFormat("   CreatedDate >'" + context.Request.Form["From"] + "' ");
            }
            if (!String.IsNullOrWhiteSpace(context.Request.Form["To"]) && Common.PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append("and");
                }
                DateTime time = Common.Globals.SafeDateTime(context.Request.Form["To"], DateTime.Now).AddDays(1);
                strWhere.AppendFormat(" CreatedDate <'" + time.ToString("yyyy-MM-dd") + "' ");
            }
            List<int> list = productBll.GetListToStatic(strWhere.ToString());
            #region 循环静态化
            //静态化之前先清除表任务
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSProduct);

            TaskList = new List<Model.SysManage.TaskQueue>();
            if (list != null && list.Count > 0)
            {
                Maticsoft.Model.SysManage.TaskQueue taskModel = null;
                int i = 1;
                foreach (int productId in list)
                {
                    //去重，不要重复的添加任务
                    if (!TaskList.Select(c => c.TaskId).Contains(productId))
                    {
                        taskModel = new Model.SysManage.TaskQueue();
                        taskModel.ID = i;
                        taskModel.TaskId = productId;
                        taskModel.Status = 0;
                        taskModel.Type = (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSProduct;
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
            }
            #endregion
            json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);
            json.Put(POLL_KEY_DATA, TaskList.Count);
            context.Response.Write(json.ToString());
        }
        /// <summary>
        /// 静态化图片
        /// </summary>
        /// <param name="context"></param>
        protected void PhotoToStatic(HttpContext context)
        {
            JsonObject json = new JsonObject();
            StringBuilder strWhere = new StringBuilder();
            //strWhere.AppendFormat(" Status=1");
            if (!String.IsNullOrWhiteSpace(context.Request.Form["From"]) && Common.PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append("and");
                }
                strWhere.AppendFormat("   CreatedDate >'" + context.Request.Form["From"] + "' ");
            }
            if (!String.IsNullOrWhiteSpace(context.Request.Form["To"]) && Common.PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append("and");
                }
                DateTime time= Common.Globals.SafeDateTime(context.Request.Form["To"], DateTime.Now).AddDays(1);
                strWhere.AppendFormat(" CreatedDate <'" + time.ToString("yyyy-MM-dd") + "' ");
            }
            List<int> list = photoBll.GetListToReGen(strWhere.ToString());
            #region 循环静态化
            //静态化之前先清除表任务
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSPhoto);
            TaskList = new List<Model.SysManage.TaskQueue>();
            if (list != null && list.Count > 0)
            {
                Maticsoft.Model.SysManage.TaskQueue taskModel = null;
                int i = 1;
                foreach (int ID in list)
                {
                    taskModel = new Model.SysManage.TaskQueue();
                    taskModel.ID = i;
                    taskModel.TaskId = ID;
                    taskModel.Status = 0;
                    taskModel.Type = (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSPhoto;
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
            json.Put(POLL_KEY_DATA, TaskList.Count);
            context.Response.Write(json.ToString());
        }
        /// <summary>
        /// 生成HTML页面
        /// </summary>
        /// <param name="context"></param>
        protected void GenerateHtml(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int TaskId = Globals.SafeInt(context.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue item = TaskList.FirstOrDefault(c => c.ID == TaskId);
            int type = Globals.SafeInt(context.Request.Form["Type"], -1);
            string requestUrl = "";//静态化请求地址
            //静态化商品页面

            if (type == (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSProduct && item != null)
            {
                string saveUrls = PageSetting.GetProductUrl(item.TaskId);
                if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    requestUrl = "/Product/Detail/" + item.TaskId;
                }
                else
                {
                    requestUrl = "/SNS/Product/Detail/" + item.TaskId;
                }
                if (!String.IsNullOrWhiteSpace(requestUrl) && !String.IsNullOrWhiteSpace(saveUrls))
                {
                    if (Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(requestUrl, saveUrls))
                    {
                        item.RunDate = DateTime.Now;
                        item.Status = 1;
                        taskBll.Update(item);
                        //更新对应数据库
                        productBll.UpdateStaticUrl(item.TaskId, saveUrls);
                        json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);
                    }
                    else
                    {
                        json.Put(POLL_KEY_STATUS, POLL_STATUS_FAILED);
                    }
                }
            }
            //静态化图片页面
            if (type == (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSPhoto && item != null)
            {
                string saveUrls = PageSetting.GetPhotoUrl(item.TaskId);
                if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    requestUrl = "/Photo/Detail/" + item.TaskId;
                }
                else
                {
                    requestUrl = "/SNS/Photo/Detail/" + item.TaskId;
                }
                if (!String.IsNullOrWhiteSpace(requestUrl) && !String.IsNullOrWhiteSpace(saveUrls))
                {
                    if (Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(requestUrl, saveUrls))
                    {
                        item.RunDate = DateTime.Now;
                        item.Status = 1;
                        taskBll.Update(item);
                        photoBll.UpdateStaticUrl(item.TaskId, saveUrls);
                        json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);
                    }
                    else
                    {
                        json.Put(POLL_KEY_STATUS, POLL_STATUS_FAILED);
                    }
                }
            }
            context.Response.Write(json.ToString());
        }
        /// <summary>
        /// 继续任务
        /// </summary>
        protected void ContinueTask(HttpContext context)
        {
            int type = Globals.SafeInt(context.Request.Form["Type"], -1);
            TaskList = taskBll.GetContinueTask(type);
            JsonObject json = new JsonObject();
            json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);

            Maticsoft.Model.SysManage.TaskQueue item = TaskList.Count == 0 ? null : TaskList.First();
            if (item == null)
            {
                taskBll.DeleteTask(type);
                json.Put(POLL_KEY_DATA, 0);
                context.Response.Write(json.ToString());
            }
            else
            {

                json.Put(POLL_KEY_DATA, item.ID);
                context.Response.Write(json.ToString());
            }
        }

        //删除任务（不删除未完成任务）
        protected void DeleteTask(HttpContext context)
        {
            //删除任务之前会静态化首页
            string requestUrl = "";
            if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
            {
                requestUrl = "/Home/Index?RequestType=1";
            }
            else
            {
                requestUrl = "/SNS/Home/Index?RequestType=1";
            }
            Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(requestUrl, "/index.html");
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSProduct);//删除商品任务列表
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.SNSPhoto);//删除图片任务列表
        }


    }
}