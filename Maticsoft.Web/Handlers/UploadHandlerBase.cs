/**
* UploadHandlerBase.cs
*
* 功 能： 上传Handler基类
* 类 名： UploadHandlerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/16 18:45:00  Ben     初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.SessionState;
using Maticsoft.Model.SysManage;
using Microsoft.Practices.Unity.InterceptionExtension;
using Maticsoft.BLL.SysManage;

namespace Maticsoft.Web.Handlers
{
    public abstract class UploadHandlerBase : HandlerBase, IReadOnlySessionState
    {
        #region 属性

        internal string ResponseContentType = "text/plain";

        /// <summary>
        /// Ajax上传, 临时目录
        /// </summary>
        internal string UploadTempFolder = string.Format("/{0}/Temp/{1}/",
            MvcApplication.UploadFolder,
            DateTime.Now.ToString("yyyyMMdd"));

        internal bool IsLocalSave;
        internal ApplicationKeyType ApplicationKeyType;

        protected virtual string[] AllowFileExt
        {
            get
            {
                return ".rar|.zip|.doc|.docx|.xls|.swf|.xlsx|.jpg|.jpeg|.gif|.png|.bmp".Split('|');
            }
        }

        #endregion
        public UploadHandlerBase(bool isLocalSave = true, ApplicationKeyType applicationKeyType = ApplicationKeyType.None)
        {
            IsLocalSave = isLocalSave;
            ApplicationKeyType = applicationKeyType;
        }
        #region 子类实现
        /// <summary>
        /// 保存文件后的操作(Response输出状态) - 子类实现
        /// </summary>
        /// <param name="uploadPath">上传路径</param>
        /// <param name="fileName">文件名称</param>
        protected abstract void ProcessSub(HttpContext context, string uploadPath, string fileName);

        /// <summary>
        /// 临时保存原文件
        /// </summary>
        protected abstract void SaveAs(string uploadPath, string fileName, HttpPostedFile file);

        /// <summary>
        /// 生成文件名 时间戳
        /// </summary>
        /// <remarks>
        /// 精确度时间值的千万分之几秒
        /// 尽管可以显示时间值的秒部分的千万分之几秒，但是该值可能并没有意义。 日期和时间值的精度取决于系统时钟的分辨率。 在 Windows NT 3.5 和更高版本以及 Windows Vista 操作系统上，时钟的分辨率大约为 10-15 毫秒。
        /// </remarks>
        protected virtual string GenerateFileName(HttpPostedFile file)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + Path.GetExtension(file.FileName);
        }

        /// <summary>
        /// 获取文件上传路径
        /// </summary>
        protected virtual string GetUploadPath(HttpContext context)
        {
            //return HttpContext.Current.Server.MapPath(@context.Request.Params["Folder"]) + "\\";
            //文件上传路径不再由Ajax请求设置 改为内部变量设置
            return HttpContext.Current.Server.MapPath(UploadTempFolder);
        }

        /// <summary>
        /// 获取文件数据
        /// </summary>
        protected virtual HttpPostedFile GetHttpPostedFile(HttpContext context)
        {
            return context.Request.Files.Count == 0 ? null : context.Request.Files[0];
        }

        #endregion

        #region IHttpHandler 成员

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = ResponseContentType;

            HttpPostedFile file = GetHttpPostedFile(context);
            if (file == null) throw new FileNotFoundException("UpdateFile Not Found! HttpPostedFile Is NULL!");
            if (file.FileName.Length < 1) return;

            if (!AllowFileExt.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                LogHelp.AddInvadeLog("Handlers-UploadHandlerBase", context.Request);
                return;
            }

            //文件重命名
            string fileName = GenerateFileName(file);

            try
            {
                if (IsLocalSave)
                {
                    //var ftphelper = Intercept.NewInstance<Maticsoft.BLL.SysManage.FTPHelper>(new VirtualMethodInterceptor(), new[] { new ErrorLogBehavior() });
                    string uploadPath = GetUploadPath(context);
                    //if (!ftphelper.Exists(uploadPath))
                    //{
                    //    //不存在则自动创建文件夹
                    //    ftphelper.MakeDir(uploadPath);
                    //}
                    if (!Directory.Exists(uploadPath))
                    {
                        //不存在则自动创建文件夹
                        Directory.CreateDirectory(uploadPath);
                    }

                    //保存文件
                    SaveAs(uploadPath, fileName, file);
                }
                else
                {
                    int filelength = file.ContentLength;
                    byte[] buffer = new byte[filelength];
                    file.InputStream.Read(buffer, 0, filelength);
                    string ImageUrl = "";
                    if (Maticsoft.BLL.SysManage.UpYunManager.UploadExecute(buffer, fileName, ApplicationKeyType, out ImageUrl))
                    {
                        fileName = ImageUrl;
                    }
                }
                //调用子类实现
                ProcessSub(context, UploadTempFolder, fileName);
            }
            catch (Exception ex)
            {
                Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
                model.Loginfo = ex.Message;
                model.StackTrace = ex.ToString();
                model.Url = context.Request.Url.AbsoluteUri;
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
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