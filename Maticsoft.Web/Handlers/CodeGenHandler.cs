using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Json;
using System.Text;
using Maticsoft.Common;
using System.IO;
namespace Maticsoft.Web.Handlers
{
    public class CodeGenHandler : IHttpHandler
    {
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_KEY_DATA = "DATA";

        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_ERROR = "ERROR";

        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;
        BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
        ProductInfo productBll=new ProductInfo();
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //安全起见, 所有SNS相关Ajax请求为POST模式
            string action = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            try
            {
                switch (action)
                {
                    case "HttpToGen":
                        HttpToGen(context);
                        break;
                    case "GenerateCode":
                        GenerateImage(context);
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

        protected void HttpToGen(HttpContext context)
        {
            //TODO: 清除系统缓存 临时处理 To:涂朝辉 BEN ADD 2012-12-22
            JsonObject json = new JsonObject();
            StringBuilder strWhere = new StringBuilder();
            //strWhere.AppendFormat(" Status=1");
            string startDate = context.Request.Form["From"];
            string endDate = context.Request.Form["To"];

            int taskType = Common.Globals.SafeInt(context.Request.Form["TaskType"], 0);
            List<int> list = new List<int>();
            //商品图片任务
            if (taskType == (int)Model.SysManage.EnumHelper.TaskQueueType.ShopProductCode)
            {
                if (!String.IsNullOrWhiteSpace(startDate) && Common.PageValidate.IsDateTime(startDate))
                {
                    if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                    {
                        strWhere.Append("and");
                    }
                    strWhere.AppendFormat("  AddedDate >'" + startDate + "' ");
                }
                if (!String.IsNullOrWhiteSpace(endDate) && Common.PageValidate.IsDateTime(endDate))
                {
                    if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                    {
                        strWhere.Append("and");
                    }
                    strWhere.AppendFormat(" AddedDate <'" + endDate + "' ");
                }
                list = productBll.GetListToReGen(strWhere.ToString());
            }

            #region 循环静态化
            //静态化之前先清除表任务
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopProductCode);

            TaskList = new List<Model.SysManage.TaskQueue>();
            if (list != null && list.Count > 0)
            {
                Maticsoft.Model.SysManage.TaskQueue taskModel = null;
                int i = 1;
                foreach (int item in list)
                {
                    //去重，不要重复的添加任务
                    if (!TaskList.Select(c => c.TaskId).Contains(item))
                    {
                        taskModel = new Model.SysManage.TaskQueue();
                        taskModel.ID = i;
                        taskModel.TaskId = item;
                        taskModel.Status = 0;
                        taskModel.Type = taskType;
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
        /// 生成缩略图
        /// </summary>
        /// <param name="context"></param>
        protected void GenerateImage(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int TaskId = Globals.SafeInt(context.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue item = TaskList.FirstOrDefault(c => c.ID == TaskId);
            int taskType = Common.Globals.SafeInt(context.Request.Form["TaskType"], 0);
            if (item != null)
            {
                if(taskType == (int) Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopProductCode)
                {
                    ReGenProductCode(item.TaskId);
                }
                item.RunDate = DateTime.Now;
                item.Status = 1;
                taskBll.Update(item);

            }
            context.Response.Write(json.ToString());
        }
        /// <summary>
        /// 继续任务
        /// </summary>
        protected void ContinueTask(HttpContext context)
        {
            int taskType = Common.Globals.SafeInt(context.Request.Form["TaskType"], 0);
            TaskList = taskBll.GetContinueTask(taskType);
            JsonObject json = new JsonObject();
            json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);

            Maticsoft.Model.SysManage.TaskQueue item = TaskList.Count == 0 ? null : TaskList.First();
          
            if (item == null)
            {
                taskBll.DeleteTask(taskType);
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
            int type = Globals.SafeInt(context.Request.Form["TaskType"], 0);
            taskBll.DeleteTask(type);//删除SNS图片生成任务列表
        }
 

        #region 生成商品二维码
        protected void ReGenProductCode(int productId)
        {
                    try
                    {          
                        string area = BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
                        string basepath = "/";
                        if (area.ToLower() != AreaRoute.MShop.ToString().ToLower())
                        {
                            basepath = "/MShop/";
                        }
                        string _uploadFolder = string.Format("/{0}/Shop/QR/Product/", MvcApplication.UploadFolder);
                        string filename = string.Format("{0}.png", productId);
                        string mapPath = HttpContext.Current.Server.MapPath(_uploadFolder);
                        string mapPathQRImgUrl = mapPath + filename;

                        string baseURL = string.Format("/tools/qr/gen.aspx?margin={0}&size={1}&level={2}&format={3}&content={4}", 0, 180, "30%", "png", "{0}");
                        string websiteUrl = "http://" + Globals.DomainFullName + basepath + "p/d/" + productId;
                        websiteUrl = "http://" + Globals.DomainFullName + string.Format(baseURL, Common.Globals.UrlEncode(websiteUrl));
                        if (!Directory.Exists(mapPath))
                        {
                            Directory.CreateDirectory(mapPath);
                        }
                        using (var webClient = new System.Net.WebClient())
                        {
                            webClient.DownloadFile(websiteUrl, mapPathQRImgUrl);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelp.AddErrorLog(string.Format("商品：{0}重新生成二维码时发生异常:{1}", productId, ex.StackTrace), "", "重新生成二维码时发生异常");
                    }
         
            }
        #endregion
    }
}