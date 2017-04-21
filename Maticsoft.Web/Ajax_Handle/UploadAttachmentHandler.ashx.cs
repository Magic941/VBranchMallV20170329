/**
* UploadAttachmentHandler.cs
*
* 功 能： 上传附件功能
* 类 名： UploadAttachmentHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/24 21:06:08  蒋海滨    初版
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
using Maticsoft.Common.Video;

namespace Maticsoft.Web.Ajax_Handle
{
    /// <summary>
    /// UploadAttachmentHandler 的摘要说明
    /// </summary>
    public class UploadAttachmentHandler : IHttpHandler
    {
        private static readonly string[] AllowFileExt = ".rar|.zip|.doc|.docx|.xls|.swf|.xlsx|.jpg|.jpeg|.gif|.png|.bmp".Split('|');

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
                string path=file.FileName;
                if (path.Length > 0)
                {
                    string fileExt = Path.GetExtension(path).ToLower();
                    if (!AllowFileExt.Contains(fileExt))
                    {
                        LogHelp.AddInvadeLog("Ajax_Handle-UploadLocalVideoHandler", context.Request);
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