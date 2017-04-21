using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Maticsoft.Web.Handlers.CMS
{
    public class UploadVideoHandler : UploadFileHandlerBase
    {

        string ffmpegTools = BLL.SysManage.ConfigSystem.GetValueByCache("FFmpeg");
        /// <summary>
        /// 保存文件后的操作(Response输出状态) - 子类实现
        /// </summary>
        /// <param name="uploadPath">上传路径</param>
        /// <param name="fileName">文件名称</param>
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            CutOutLocalvideoImages(fileName, uploadPath);
            context.Response.Write(uploadPath + fileName + "|" + uploadPath + fileName + ".jpg");
        }



        #region 截取本地上传的视频图片
        /// <summary>
        /// 截取本地上传的视频图片
        /// </summary>
        /// <param name="filename">视频文件名称</param>
        /// <returns></returns>
        public string CutOutLocalvideoImages(string filename, string uploadPath)
        {
            try
            {
                string path = uploadPath + filename;

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