/**
* UploadMultipleHandler.cs
*
* 功 能： [N/A]
* 类 名： UploadMultipleHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/7 14:43:23  Rock    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers
{
    public class UploadMultipleFileHandler : HandlerBase, IReadOnlySessionState
    {
        #region 属性
        internal string ResponseContentType = "text/plain";
        internal static readonly string[] AllowFileExt = ".rar|.zip|.doc|.docx|.xls|.swf|.xlsx|.jpg|.jpeg|.gif|.png|.bmp".Split('|');
        #endregion

        /// <summary>
        /// 获取文件数据
        /// </summary>
        protected virtual HttpFileCollection GetHttpPostedFile(HttpContext context)
        {
            return context.Request.Files.Count == 0 || context.Request.Files[0].ContentLength < 1 ?
                null : context.Request.Files;
        }

        #region IHttpHandler 成员

        public override void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = ResponseContentType;

                //获取上传文件集合
                HttpFileCollection files = GetHttpPostedFile(context);
                if (files == null) throw new FileNotFoundException("UpdateFile Not Found! HttpPostedFile Is NULL!");

                //获取临时目录
                string uploadPath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/"; ;
                string uploadPathMP = context.Server.MapPath(uploadPath);
                if (!Directory.Exists(uploadPathMP))
                {
                    //不存在则自动创建文件夹
                    Directory.CreateDirectory(uploadPathMP);
                }
                string fileNames = string.Empty;

                string ext;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile postedFile = files[i];

                    ext = Path.GetExtension(postedFile.FileName).ToLower();
                    if (!AllowFileExt.Contains(ext))
                    {
                        LogHelp.AddInvadeLog("Handlers-UploadMultipleFileHandler", context.Request);
                        return;
                    }

                    //文件重命名
                    string reName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + ext;
                    postedFile.SaveAs(uploadPathMP + reName);
                    string thumName = "T300X400_" + reName;
                    //临时保存文件
                    ImageTools.MakeThumbnail(uploadPathMP + reName, uploadPathMP + thumName, 300, 400, MakeThumbnailMode.HW);
                    fileNames += "|" + thumName;
                }
                //json方式输出成功信息 和 保存路径 , 文件名
                JsonObject json = new JsonObject();
                json.Put("success", true);  //成功
                json.Put("path", uploadPath + "{0}");   //临时保存路径
                json.Put("names", fileNames.TrimStart('|'));   //文件名 | 分割
                context.Response.Write(json.ToString());    //输出json数据
            }
            catch (Exception ex)
            {
                Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
                model.Loginfo = ex.Message;
                model.StackTrace = ex.ToString();
                model.Url = context.Request.Url.AbsoluteUri;
                BLL.SysManage.ErrorLog.Add(model);
                throw;
            }
        }

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}