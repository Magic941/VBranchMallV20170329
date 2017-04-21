/**
* UploadImageHandler.cs
*
* 功 能： 上传图片Handler基类
* 类 名： UploadImageHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/4 17:06:00  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Maticsoft.Common;
using System.Linq;

namespace Maticsoft.Web.Ajax_Handle
{
    public abstract class UploadImageHandlerBase : IHttpHandler, IReadOnlySessionState
    {
        protected MakeThumbnailMode makeThumbnailMode = MakeThumbnailMode.Auto;
        private static readonly string[] AllowFileExt = ".jpg|.gif|.png|.bmp".Split('|');

        #region 子类实现
        /// <summary>
        /// 获取缩略图尺寸
        /// </summary>
        protected abstract Size GetThumbImageSize();
        /// <summary>
        /// 获取常规缩略图尺寸
        /// </summary>
        protected abstract Size GetNormalImageSize();
        /// <summary>
        /// 保存文件后的操作(Response输出状态) - 子类实现
        /// </summary>
        protected abstract void ProcessSub(HttpContext context, string fileName);

        /// <summary>
        /// 临时保存原文件并生成缩略图
        /// </summary>
        protected virtual void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            //保存临时原图
            file.SaveAs(uploadPath + fileName);
            //生成临时缩略图
            MakeThumbnail(uploadPath, fileName, GetThumbImageSize(), GetNormalImageSize());
        }
        /// <summary>
        /// 生成文件名 GUID
        /// </summary>
        protected virtual string GenerateFileName(HttpPostedFile file)
        {
            return Guid.NewGuid() + Path.GetExtension(file.FileName);
        }
        /// <summary>
        /// 获取文件上传路径
        /// </summary>
        protected virtual string GetUploadPath(HttpContext context)
        {
            return HttpContext.Current.Server.MapPath(@context.Request.Params["folder"]) + "\\";
        }
        #endregion

        #region IHttpHandler 成员

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HttpPostedFile file = context.Request.Files["Filedata"];

            if (file == null) return;
            if (file.FileName.Length < 1) return;

            string uploadPath = GetUploadPath(context);
            if (!Directory.Exists(uploadPath))
            {
                //不存在则自动创建文件夹
                Directory.CreateDirectory(uploadPath);
            }

            if (!AllowFileExt.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                LogHelp.AddInvadeLog("Ajax_Handle-UploadImageHandlerBase", context.Request);
                context.Response.Clear();
                return;
            }

            //文件重命名
            string fileName = GenerateFileName(file);
            //保存文件
            SaveAs(uploadPath, fileName, file);

            //调用子类实现
            ProcessSub(context, fileName);
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        protected virtual void MakeThumbnail(string uploadPath, string fileName, Size thumbImageSize, Size normalImageSize)
        {
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T_" + fileName, thumbImageSize.Width, thumbImageSize.Height, makeThumbnailMode);
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "N_" + fileName, normalImageSize.Width, normalImageSize.Height, makeThumbnailMode);
        }
        #endregion
    }
}