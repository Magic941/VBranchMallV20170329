using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Maticsoft.BLL.SysManage
{
    /// <summary>
    /// 文件管理器
    /// </summary>
    public  class FileManager
    {
        /// <summary>
        /// 移动图片到指定的ftp地址,返回相对路径地址
        /// </summary>
        /// <param name="ImageUrl">需要移动的图片 网站相对路径</param>
        /// <param name="savePath">存储地址 相对目录路径 目录</param>     
        public static string MoveImageForFTP(string ImageUrl, string savePath)
        {
            var ftpURI = BLL.SysManage.ConfigSystem.GetValueByCache("FtpURI");
            var ftpName = BLL.SysManage.ConfigSystem.GetValueByCache("FtpName");
            var ftpPWD = BLL.SysManage.ConfigSystem.GetValueByCache("FtpPWD");

            var ftphelper = Intercept.NewInstance<Maticsoft.BLL.SysManage.FTPHelper>(new VirtualMethodInterceptor(), new[] { new ErrorLogBehavior() }, new[] { ftpURI, ftpName, ftpPWD });
            try
            {               
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    if (!ftphelper.Exists(Path.Combine(ftpURI, savePath)))
                        ftphelper.MakeDir(savePath);                 

                    string imgname = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string originalUrl = "";
                  
                    //指定的文件存在，则上传到ftp
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    {
                        //图片保存路径
                        originalUrl = String.Format(savePath +"/"+ imgname, "");
                        Image image = Image.FromFile(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, "")));
                        using (MemoryStream stream = new MemoryStream())
                        {
                            image.Save(stream, ImageFormat.Png); //把图片保存到流中。
                            ftphelper.Upload(stream, originalUrl);
                        }
                    }
                    //
                    return originalUrl;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }

    }
}
