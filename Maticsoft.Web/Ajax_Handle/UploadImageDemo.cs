/**
* DemoUploadImagecs.cs
*
* 功 能： 移动设备上传图片Demo
* 类 名： DemoUploadImagecs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/26 17:26:47  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Maticsoft.Common;

namespace Maticsoft.Web.AjaxHandle
{
    public class UploadImageDemo : IHttpHandler
    {
        private static readonly string[] AllowFileExt = ".jpg|.png|.jpeg|.gif".Split('|');

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile postedFile = context.Request.Files[0];
            string savePath = "/UploadFolder/";
            int filelength = postedFile.ContentLength;
            int fileSize = 5120000;
            string fileName = "-1"; //返回的上传后的文件名
            if (filelength <= fileSize)
            {
                string fileExt = Path.GetExtension(postedFile.FileName).ToLower();
                if (!AllowFileExt.Contains(fileExt))
                {
                    LogHelp.AddInvadeLog("Ajax_Handle-UploadImageDemo", context.Request);
                    context.Response.Clear();
                    return;
                }
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                string imgname = Guid.NewGuid() + fileExt;

                string _path = HttpContext.Current.Server.MapPath(savePath) + imgname;
                postedFile.SaveAs(_path);
                fileName = savePath + imgname;
            }
            context.Response.Write(fileName);
        }


        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion
    }
}