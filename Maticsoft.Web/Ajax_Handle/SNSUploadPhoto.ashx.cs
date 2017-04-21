using System;
using System.Web;
using System.IO;
using System.Drawing;
using Maticsoft.BLL.SysManage;
using Maticsoft.Common;
using System.Web.SessionState;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Ajax_Handle
{
    /// <summary>
    /// SNSUploadPhoto 的摘要说明
    /// </summary>
    public class SNSUploadPhoto : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile postedFile = context.Request.Files[0];
            string savePath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            int filelength = postedFile.ContentLength;
            string fileSizeString =Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSPhotoSizes");  //1M
            int fileSize = 10240000;
            if (string.IsNullOrEmpty(fileSizeString))
            {
                fileSize = Common.Globals.SafeInt(fileSizeString, 10240000);
            }
            string fileName = "-1"; //返回的上传后的文件名
            if (filelength <= fileSize)
            {
                byte[] buffer = new byte[filelength];
                postedFile.InputStream.Read(buffer, 0, filelength);
                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(".") + 1);
                //以下分两种情况，一种是存到本地，一种是存储到又拍云
                string StoreWay = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay");
                if (StoreWay != "1")
                {
                    fileName = UploadImage(buffer, savePath, ext, context);
                }
                else
                {
                    string ImageUrl = "";
                    string ThumbImageUrl = "";
                    string BigImageUrl = "";
                    string FileName = CreateIDCode() + "." + ext;
                    if (Maticsoft.BLL.SysManage.UpYunManager.UploadExecute(buffer, FileName,ApplicationKeyType.SNS, out ImageUrl))
                    {
                        fileName = ImageUrl + "|" + ThumbImageUrl + "|" + BigImageUrl;
                    }
                }
            }
            context.Response.Write(fileName);
        }

        public static string UploadImage(byte[] imgBuffer, string uploadpath, string ext, HttpContext context)
        {
            System.IO.MemoryStream m = new MemoryStream(imgBuffer);
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(uploadpath)))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadpath));
            string imgname = CreateIDCode() + "." + ext;
            if (context.Request["UserID"] != null)
            {
                imgname = "B" + context.Request["UserID"] + "." + ext;
            }
            string _path = HttpContext.Current.Server.MapPath(uploadpath) + imgname;
            Image img = Image.FromStream(m);
            if (ext == "jpg")
            {
                img.Save(_path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (ext == "png")
            {
                img.Save(_path, System.Drawing.Imaging.ImageFormat.Png);
            }
            if (ext == "gif")
            {
                img.Save(_path, System.Drawing.Imaging.ImageFormat.Gif);
            }
            if (ext != null && ext.ToLower() == "jpeg")
            {
                img.Save(_path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            m.Close();
            string SthumbImage="";
            string BthumbImage="";
            if (context.Request["Type"] != null)
            {
                ///生成小图
                 SthumbImage = "S_" + imgname;
                string SthumbImagePath = HttpContext.Current.Server.MapPath(uploadpath + SthumbImage);
                  
                string SStrSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_SmallPhotoSize");  // 111;   111xx1;   11x;   x
                int SWindthInt = 200;
                int SHeightInt=200;

                if (!string.IsNullOrEmpty(SStrSize))
                {
                    string[] Size = SStrSize.Split('X');
                    if (Size.Length == 1)
                    {
                        int DefaultInt = Common.Globals.SafeInt(SStrSize, 200);
                        SWindthInt = DefaultInt;
                        SHeightInt = DefaultInt;
                    }
                    else if (Size.Length == 2)
                    {
                        SWindthInt = Common.Globals.SafeInt(Size[0], 200);
                        SHeightInt = Common.Globals.SafeInt(Size[1], 200);
                    }
                }
                

                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), SthumbImagePath, SWindthInt, SHeightInt, MakeThumbnailMode.W);
                ///生成大图
                 BthumbImage = "B_" + imgname;
                 string BthumbImagePath = HttpContext.Current.Server.MapPath(uploadpath + BthumbImage);
                 string BStrSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_BigPhotoSize");
                 int BWindthInt = 800;
                 int BHeightInt = 800;

                 if (!string.IsNullOrEmpty(BStrSize))
                 {
                     string[] Size = BStrSize.Split('X');
                     if (Size.Length == 1)
                     {
                         int DefaultInt = Common.Globals.SafeInt(BStrSize, 800);
                         BWindthInt = DefaultInt;
                         BHeightInt = DefaultInt;
                     }
                     else if (Size.Length == 2)
                     {
                         BWindthInt = Common.Globals.SafeInt(Size[0], 800);
                         BHeightInt = Common.Globals.SafeInt(Size[1], 800);
                     }
                 }
                 ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), BthumbImagePath,BWindthInt, BHeightInt, MakeThumbnailMode.W);
            }
            if (context.Request["UserID"] != null)
            {
                string thumbImage = context.Request["UserID"] + "." + ext;
                string thumbImagePath = HttpContext.Current.Server.MapPath(uploadpath + thumbImage);
                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), thumbImagePath, 100, 100, MakeThumbnailMode.Auto);
                context.Session["Gravatar"] = context.Request["UserID"] + "." + ext;
                return uploadpath + imgname + "?" + DateTime.Now.ToString();
            }
            return uploadpath + imgname + "|" + uploadpath + SthumbImage + "|" + uploadpath + BthumbImage;
        }

        public static string CreateIDCode()
        {
            DateTime Time1 = DateTime.Now.ToUniversalTime();
            DateTime Time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = Time1 - Time2;   //span就是两个日期之间的差额   
            string t = span.TotalMilliseconds.ToString("0");
            return t;
        }
        public bool IsReusable
        {
            get { return false; }
        }
    }


}