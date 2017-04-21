using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json;
using System.Text;
using Maticsoft.Common;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Maticsoft.Web.Handlers.CMS
{
    public class PhotoReGenHandler : IHttpHandler
    {
        public const string POLL_KEY_STATUS = "STATUS";
        public const string POLL_KEY_DATA = "DATA";

        public const string POLL_STATUS_SUCCESS = "SUCCESS";
        public const string POLL_STATUS_FAILED = "FAILED";
        public const string POLL_STATUS_ERROR = "ERROR";

        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;
        Maticsoft.BLL.CMS.Photo photoBll = new BLL.CMS.Photo();
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //安全起见, 所有CMS相关Ajax请求为POST模式
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
                    case "GenerateImage":
                        GenerateImage(context);
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

        protected void HttpToGen(HttpContext context)
        {
            JsonObject json = new JsonObject();
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" State=1");
            if (!String.IsNullOrWhiteSpace(context.Request.Form["From"]) && Common.PageValidate.IsDateTime(context.Request.Form["From"]))
            {
                strWhere.AppendFormat(" and  CreatedDate >'" + context.Request.Form["From"] + "' ");
            }
            if (!String.IsNullOrWhiteSpace(context.Request.Form["To"]) && Common.PageValidate.IsDateTime(context.Request.Form["To"]))
            {
                strWhere.AppendFormat(" and CreatedDate <'" + context.Request.Form["To"] + "' ");
            }
            List<int> list = photoBll.GetListToReGen(strWhere.ToString());
            #region 循环静态化
            //静态化之前先清除表任务
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.CMSPhotoReGen);

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
                        taskModel.Type = (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.CMSPhotoReGen;
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
        /// 生成HTML页面
        /// </summary>
        /// <param name="context"></param>
        protected void GenerateImage(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int TaskId = Globals.SafeInt(context.Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue item = TaskList.FirstOrDefault(c => c.ID == TaskId);
            if (item != null)
            {
                ReGenPhoto(item.TaskId);
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
            TaskList = taskBll.GetContinueTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.CMSPhotoReGen);
            JsonObject json = new JsonObject();
            json.Put(POLL_KEY_STATUS, POLL_STATUS_SUCCESS);

            Maticsoft.Model.SysManage.TaskQueue item = TaskList.Count == 0 ? null : TaskList.First();
            if (item == null)
            {
                taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.CMSPhotoReGen);
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
        protected void DeleteTask()
        {
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.CMSPhotoReGen);//删除SNS图片生成任务列表
        }

        #region 生成缩略图

        protected void ReGenPhoto(int phottoId)
        {
            Maticsoft.Model.CMS.Photo photoModel = photoBll.GetModelByCache(phottoId);
            if (photoModel != null)
            {
                //是否是旧数据
                bool IsOldData = false;
                //原始图片地址
                string origialStr = photoModel.ImageUrl;
                //判断原始文件是否存在
                if (!File.Exists(HttpContext.Current.Server.MapPath(origialStr)) || origialStr.StartsWith("http://"))
                {
                    return;
                }
                FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(origialStr));
                //重新生成缩略图
                MakeThumbnailMode ThumbnailMode = MakeThumbnailMode.W;
                //原始文件名
                string fileName = origialStr.Substring(origialStr.LastIndexOf('/') + 1, origialStr.Length - origialStr.LastIndexOf('/') - 1);

                if (!photoModel.ThumbImageUrl.Contains("{0}"))
                {
                    IsOldData = true;
                    string dir = "/Upload/CMS/Images/PhotosThumbs/" + DateTime.Now.ToString("yyyyMMdd");
                    if (Directory.Exists(HttpContext.Current.Server.MapPath(dir)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(dir));
                    }
                    photoModel.ThumbImageUrl = dir + "/{0}" + fileName;
                }

                //#region 对存储位置不发生改变
                //string thumbImageUrl =photoModel.ThumbImageUrl; //fileInfo.Directory + "/T_" + fileName;
                //string normalImageUrl = photoModel.NormalImageUrl; //fileInfo.Directory + "/N_" + fileName;
                //#endregion
                List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList =
             Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, MvcApplication.ThemeName);
                try
                {
                    //重新生成缩略图
                    if (thumSizeList != null && thumSizeList.Count > 0)
                    {
                        foreach (var thumSize in thumSizeList)
                        {
                            ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(origialStr), HttpContext.Current.Server.MapPath(String.Format(photoModel.ThumbImageUrl, thumSize.ThumName)),
                                thumSize.ThumWidth, thumSize.ThumHeight, ThumbnailMode, ImageFormat.Jpeg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelp.AddErrorLog(string.Format("CMS：{0}重新生成缩略图时发生异常:{1}", origialStr, ex.StackTrace), "", "重新生成缩略图时发生异常");
                }
                try
                {
                    #region 存储位置不发生改变，就不需要更新数据库
                    //更新到数据库
                    if (IsOldData)
                    {
                        photoBll.Update(photoModel);
                    }
                    //photoModel.ThumbImageUrl = GetAbsolutePath(thumbImageUrl);
                    //photoModel.NormalImageUrl = GetAbsolutePath(normalImageUrl);
                    //photoBll.Update(photoModel);
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelp.AddErrorLog(string.Format("CMS：{0}重新生成缩略图更新到数据库时发生异常:{1}", origialStr, ex.StackTrace), "", "重新生成缩略图时发生异常");
                }

            }
        }

        public static string GetAbsolutePath(string path)
        {
            return "/" + path.Replace(HttpContext.Current.Request.PhysicalApplicationPath, "").Replace("\\", "/");
        }
        #endregion
    }
}