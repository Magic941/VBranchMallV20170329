/**
* UploadPictureHandler.cs
*
* 功 能： 上传视频专辑图片
* 类 名： UploadPictureHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/28 10:56:45  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Text;

namespace Maticsoft.Web.Ajax_Handle
{
    /// <summary>
    /// UploadPictureHandler 的摘要说明
    /// </summary>
    public class UploadPictureHandler : IHttpHandler
    {
        private static readonly string[] AllowFileExt = ".jpg|.jpeg|.png|.gif|.bmp".Split('|');

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";

            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                StringBuilder str = new StringBuilder();
                string path = file.FileName;
                if (path.Length > 0)
                {
                    string fileExt = Path.GetExtension(path).ToLower();
                    if (!AllowFileExt.Contains(fileExt))
                    {
                        LogHelp.AddInvadeLog("Ajax_Handle-UploadPictureHandler", context.Request);
                        context.Response.Write("0");
                        return;
                    }

                    string fileName = Guid.NewGuid() + fileExt;

                    string filePath = uploadPath + fileName;

                    file.SaveAs(filePath);

                    str.AppendFormat(fileName);

                }

                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write("1|" + str);
            }
            else
            {
                context.Response.Write("0");
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}