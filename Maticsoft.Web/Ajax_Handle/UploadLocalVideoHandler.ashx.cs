/**
* UploadLocalVideoHandler.cs
*
* 功 能： 上传视频功能
* 类 名： UploadLocalVideoHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/24 12:15:32  蒋海滨    初版
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
    /// UploadLocalVideoHandler 的摘要说明
    /// </summary>
    public class UploadLocalVideoHandler : IHttpHandler
    {
        string ffmpegTools = BLL.SysManage.ConfigSystem.GetValueByCache("FFmpeg");
        string uploadFolder = BLL.SysManage.ConfigSystem.GetValueByCache("UploadFolder");

        private static readonly string[] AllowFileExt = ".flv|.mp4".Split('|');

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
                    //视频格式fileExt
                    string fileExt = Path.GetExtension(path).ToLower();

                    if (!AllowFileExt.Contains(fileExt))
                    {
                        LogHelp.AddInvadeLog("Ajax_Handle-UploadLocalVideoHandler", context.Request);
                        context.Response.Write("0");
                        return;
                    }

                    //文件名称
                    string fileName = Guid.NewGuid() + fileExt;

                    string filePath = uploadPath + fileName;

                    file.SaveAs(filePath);

                    str.AppendFormat(fileName);

                    CutOutLocalvideoImages(fileName);

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

        #region 截取本地上传的视频图片
        /// <summary>
        /// 截取本地上传的视频图片
        /// </summary>
        /// <param name="filename">视频文件名称</param>
        /// <returns></returns>
        public string CutOutLocalvideoImages(string filename)
        {
            try
            {
                string path = uploadFolder + filename;

                //ffmpeg.exe的位置
                string ffmpeg = HttpContext.Current.Server.MapPath(ffmpegTools);

                if ((!System.IO.File.Exists(ffmpeg)) || (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(path))))
                {
                    return "";
                }

                string fileExt = Path.GetExtension(path);

                //获得图片相对路径/最后存储到数据库的路径
                string image = System.IO.Path.ChangeExtension(path, ".jpg");

                //保存图片绝对路径
                string imageUrl = HttpContext.Current.Server.MapPath(path + ".jpg");

                //截图的尺寸大小,配置在Web.Config中,如:<add key="CatchFlvImgSize" value="128x96" /> 

                string catchImageSize = "448*336";

                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//System.Diagnostics.ProcessWindowStyle.Normal

                //此处组合成ffmpeg.exe文件需要的参数即可,此处命令在ffmpeg 0.4.9调试通过
                startInfo.Arguments = " -i " + HttpContext.Current.Server.MapPath(path) + " -y -f image2 -t 20 -s " + catchImageSize + " " + imageUrl;

                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    return "";
                }

                System.Threading.Thread.Sleep(4000);

                ///注意:图片截取成功后,数据由内存缓存写到磁盘需要时间较长,大概在3,4秒甚至更长;
                if (System.IO.File.Exists(imageUrl))
                {
                    return filename + ".jpg";
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}