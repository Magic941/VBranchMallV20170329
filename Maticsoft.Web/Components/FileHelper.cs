using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Xml;
using Maticsoft.Model.Ms;
using Maticsoft.ZipLib;
using Maticsoft.Common;
using Maticsoft.Model.Settings;
using Maticsoft.Model.SysManage;
using Maticsoft.ZipLib.Zip;
using EnumHelper = Maticsoft.Model.Ms.EnumHelper;
using Microsoft.Practices.Unity.InterceptionExtension;
using Maticsoft.BLL.SysManage;

namespace Maticsoft.Web.Components
{
    public class FileHelper
    {
        private static BLL.SysManage.WebSiteSet WebSiteSetShop = new BLL.SysManage.WebSiteSet(ApplicationKeyType.Shop);

        private static Size GetThumbImageSize()
        {
            int thumbImgWidth = Globals.SafeInt(WebSiteSetShop.Shop_ThumbImageWidth, 0);
            int thumbImgHeight = Globals.SafeInt(WebSiteSetShop.Shop_ThumbImageHeight, 0);
            return Maticsoft.Common.StringPlus.SplitToSize(
                BLL.SysManage.ConfigSystem.GetValueByCache(SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                '|', thumbImgWidth == 0 ? SettingConstant.ProductThumbSize.Width : thumbImgWidth, thumbImgHeight == 0 ? SettingConstant.ProductThumbSize.Height : thumbImgHeight);
        }

        private static Size GetNormalImageSize()
        {
            int normalImgWidth = Globals.SafeInt(WebSiteSetShop.Shop_NormalImageWidth, 0);
            int normalImgHeight = Globals.SafeInt(WebSiteSetShop.Shop_NormalImageHeight, 0);
            return Maticsoft.Common.StringPlus.SplitToSize(
                BLL.SysManage.ConfigSystem.GetValueByCache(SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                '|', normalImgWidth == 0 ? SettingConstant.ProductNormalSize.Width : normalImgWidth, normalImgHeight == 0 ? SettingConstant.ProductNormalSize.Height : normalImgHeight);
        }

        /// <summary>
        /// 图片的地址
        /// </summary>
        /// <param name="imageurl"></param>
        /// <returns></returns>
        public static string GetImageUrl(string imageurl)
        {
            var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");


            return picServerUrl + "/" + imageurl;
        }


        /// <summary>
        /// 获取缩略图路径
        /// </summary>
        /// <param name="imageurl"></param>
        /// <param name="thumbName"></param>
        /// <returns></returns>
        public static string GeThumbImage(string imageurl, string thumbName)
        {
            if (string.IsNullOrWhiteSpace(imageurl) || string.IsNullOrWhiteSpace(thumbName))
                return string.Empty;

            //先排除淘宝图片
            if (imageurl.Contains("taobaocdn.com"))
            {
                return imageurl;
            }
            //云存储图片
            if (imageurl.StartsWith("http://"))
            {
                return imageurl + Maticsoft.BLL.Ms.ThumbnailSize.GetCloudName(thumbName);
            }
            //string thumbUrl = String.Format(imageurl, "T_");

            ////ftp图片看是否有该图片
            //var ftpURI = BLL.SysManage.ConfigSystem.GetValueByCache("FtpURI");
            //var ftpName = BLL.SysManage.ConfigSystem.GetValueByCache("FtpName");
            //var ftpPWD = BLL.SysManage.ConfigSystem.GetValueByCache("FtpPWD");
            //var ftphelper = Intercept.NewInstance<Maticsoft.BLL.SysManage.FTPHelper>(new VirtualMethodInterceptor(), new[] { new ErrorLogBehavior() }, new[] { ftpURI, ftpName, ftpPWD });

            var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");

            //if (ftphelper.Exists(thumbUrl))
            //{
            //    return picServerUrl + "/"+  thumbUrl;
            //}
            //if (File.Exists(HttpContext.Current.Server.MapPath(thumbUrl)))
            //{
            //    return thumbUrl;
            //}
            return picServerUrl + "/"+ String.Format(imageurl, thumbName);
        }

        /// <summary>
        /// 删除物理物件
        /// </summary>
        /// <param name="FileUrls"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static bool DeleteFile(List<string> FileUrls, ref string Error)
        {
            try
            {
                if (FileUrls != null && FileUrls.Count > 0)
                {
                    foreach (string FileUrl in FileUrls)
                    {
                        if (File.Exists(HttpContext.Current.Server.MapPath(FileUrl)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(FileUrl));
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }

        public static bool DeleteFile(List<Maticsoft.Model.Ms.ThumbnailSize> thumbnailSizes, string path,
                                      ref string Error)
        {
            try
            {
                if (thumbnailSizes != null && thumbnailSizes.Count > 0)
                {
                    foreach (var thumb in thumbnailSizes)
                    {
                        string pathUrl = String.Format(path, thumb.ThumName);
                        if (File.Exists(HttpContext.Current.Server.MapPath(pathUrl)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(pathUrl));
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }

        /// <summary>
        /// 删除物理文件，（包括又拍云上面的文件删除）
        /// </summary>
        /// <param name="areaType"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType areaType, string path)
        {
            try
            {
                ApplicationKeyType applicationKeyType = ApplicationKeyType.SNS;
                switch (areaType)
                {
                    case EnumHelper.AreaType.CMS:
                        applicationKeyType = ApplicationKeyType.CMS;
                        break;
                    case EnumHelper.AreaType.SNS:
                        applicationKeyType = ApplicationKeyType.SNS;
                        break;
                    case EnumHelper.AreaType.Shop:
                        applicationKeyType = ApplicationKeyType.Shop;
                        break;
                    default:
                        applicationKeyType = ApplicationKeyType.SNS;
                        break;
                }
                if (path.Contains("http://"))
                {
                    return Maticsoft.BLL.SysManage.UpYunManager.DeleteImage(path, applicationKeyType);
                }
                List<Maticsoft.Model.Ms.ThumbnailSize> thumbnailSizes =
             Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(areaType);
                if (thumbnailSizes != null && thumbnailSizes.Count > 0)
                {
                    foreach (var thumb in thumbnailSizes)
                    {
                        string pathUrl = String.Format(path, thumb.ThumName);
                        if (File.Exists(HttpContext.Current.Server.MapPath(pathUrl)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(pathUrl));
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Maticsoft.Model.SysManage.ErrorLog logModel = new Model.SysManage.ErrorLog();
                logModel.Loginfo = "删除图片【" + path + "】失败";
                logModel.OPTime = DateTime.Now;
                logModel.StackTrace = e.Message;
                logModel.Url = "";
                Maticsoft.BLL.SysManage.ErrorLog.Add(logModel);
                return false;
            }
        }


        public static string GetNewFileName(string OldFileName)
        {
            if (!string.IsNullOrEmpty(OldFileName) && OldFileName.Contains("."))
            {
                return CreateIDCode() + "." + OldFileName.Substring(OldFileName.LastIndexOf(".") + 1);
            }
            return "";
        }

        public static bool FileRemove(string OldPath, string NewPath, ref string RefNewPath)
        {
            if (string.IsNullOrEmpty(OldPath) || string.IsNullOrEmpty(NewPath))
            {
                return true;
            }
            try
            {
                string FileName = Path.GetFileName(OldPath);
                string AllOldPath = HttpContext.Current.Server.MapPath(OldPath);
                string AllNewPath = HttpContext.Current.Server.MapPath(NewPath);
                if (System.IO.File.Exists(AllOldPath))
                {
                    RefNewPath = NewPath + FileName;
                    if (System.IO.File.Exists(AllNewPath + FileName))
                    {
                        System.IO.File.Delete(AllNewPath + FileName);
                    }
                    System.IO.File.Move(AllOldPath, AllNewPath + FileName);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个随意的时间戳
        /// </summary>
        /// <returns></returns>

        public static string CreateIDCode()
        {
            DateTime Time1 = DateTime.Now.ToUniversalTime();
            DateTime Time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = Time1 - Time2;   //span就是两个日期之间的差额
            string t = span.TotalMilliseconds.ToString("0");
            return t;
        }

        /// <summary>
        /// 图片的裁剪
        /// </summary>
        /// <param name="imgname">图片的名字</param>
        /// <param name="uploadpath">存放的位置</param>
        /// <param name="SmallImageSize">小图的大小 长X宽的形式</param>
        /// <param name="BigImageSize">大图的大小 长X宽的形式</param>
        /// <param name="SmallImagePath">out 小图的保存的位置</param>
        /// <param name="BigImagePath">out 大图保存的位置</param>
        /// <returns></returns>
        public static bool ImageCutMethod(string imgname, string uploadpath, string SmallImageSize, string BigImageSize, ref string SmallImagePath, ref string BigImagePath)
        {
            try
            {
                //生成小图
                string SthumbImage = "S_" + imgname;
                string SthumbImagePath = HttpContext.Current.Server.MapPath(SmallImagePath + SthumbImage);
                int SWindthInt = 400;
                int SHeightInt = 400;
                if (SmallImageSize != null && SmallImageSize.Split('X').Length > 1)
                {
                    string[] Size = SmallImageSize.Split('X');
                    SWindthInt = Common.Globals.SafeInt(Size[0], 400);
                    SHeightInt = Common.Globals.SafeInt(Size[1], 400);
                }
                MakeWaterThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), SthumbImagePath, SWindthInt, SHeightInt, MakeThumbnailMode.Auto);

                ///生成大图
                string BthumbImage = "B_" + imgname;
                string BthumbImagePath = HttpContext.Current.Server.MapPath(BigImagePath + BthumbImage);

                int BWindthInt = 800;
                int BHeightInt = 800;
                if (BigImageSize != null && BigImageSize.Split('X').Length > 1)
                {
                    string[] Size = BigImageSize.Split('X');
                    BWindthInt = Common.Globals.SafeInt(Size[0], 800);
                    BHeightInt = Common.Globals.SafeInt(Size[1], 800);
                }
                MakeWaterThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), BthumbImagePath, BWindthInt, BHeightInt, MakeThumbnailMode.Auto);
                SmallImagePath = uploadpath + SthumbImage;
                BigImagePath = uploadpath + BthumbImage;
                return true;
            }
            catch (Exception)
            {
                SmallImagePath = "";
                BigImagePath = "";
                return false;
            }
        }

        public static int StartIndex(int PageSize, int PageIndex)
        {
            return PageSize * (PageIndex - 1) + 1;
        }

        public static int EndIndex(int PageSize, int PageIndex)
        {
            return PageSize * PageIndex;
        }

        public static bool UnpackFiles(string zipFile, string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var zis = new ZipInputStream(File.OpenRead(zipFile));
                ZipEntry theEntry = null;
                while ((theEntry = zis.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (directoryName != string.Empty)
                        Directory.CreateDirectory(directory + directoryName);

                    if (fileName != string.Empty)
                    {
                        FileStream streamWriter = File.Create(Path.Combine(directory, theEntry.Name));
                        int size = 2048;
                        var data = new byte[size];
                        while (true)
                        {
                            size = zis.Read(data, 0, data.Length);
                            if (size > 0)
                                streamWriter.Write(data, 0, size);
                            else
                                break;
                        }

                        streamWriter.Close();
                    }
                }

                zis.Close();
                return true;
            }
            catch (Exception)
            {
                return false;

                throw;
            }
        }

        #region  生成水印及缩略图
        /// <summary>
        /// 生成水印及缩略图
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        /// <param name="width"></param>
        /// <param name="Height"></param>
        /// <param name="mode"></param>
        public static void MakeWaterThumbnail(string oldpath, string newpath, int width, int Height, MakeThumbnailMode mode)
        {
            string waterMarkType = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkType");
            int type = Common.Globals.SafeInt(waterMarkType, 0);
            if (type == 0)
            {
                MakeTextWaterThumbnail(oldpath, newpath, width, Height, mode);
            }
            else
            {
                MakeImageWaterThumbnail(oldpath, newpath, width, Height, mode);
            }
        }

        /// <summary>
        /// 文字水印+缩略图
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        /// <param name="_watermarkText"></param>
        public static void MakeTextWaterThumbnail(string oldpath, string newpath, int width, int Height, MakeThumbnailMode mode)
        {
            string waterMarkContent = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkContent");
            if (String.IsNullOrWhiteSpace(waterMarkContent))
            {
                waterMarkContent = "Maticsoft";
            }
            string waterMarkFont = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFont");
            if (String.IsNullOrWhiteSpace(waterMarkFont))
            {
                waterMarkFont = "arial";
            }
            string waterMarkFontSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFontSize");
            if (String.IsNullOrWhiteSpace(waterMarkFontSize))
            {
                waterMarkFontSize = "14";
            }
            string waterMarkPosition = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (String.IsNullOrWhiteSpace(waterMarkPosition))
            {
                waterMarkPosition = "WM_CENTER";
            }
            string waterMarkFontColor = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFontColor");
            if (String.IsNullOrWhiteSpace(waterMarkFontColor))
            {
                waterMarkFontColor = "#FFFFFF";
            }
            try
            {
                Maticsoft.Common.ImageTools.MakeTextWaterThumbnail(oldpath, newpath, width, Height, mode, System.Drawing.Imaging.ImageFormat.Png, waterMarkContent, waterMarkPosition, waterMarkFont, Common.Globals.SafeInt(waterMarkFontSize, 14), waterMarkFontColor);

                //  , string _watermarkPosition = "WM_CENTER", string fontStyle = "arial", int fontSize = 14, string color = "#FFFFFF"
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 生成水印
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        public static void MakeWater(string oldpath, string newpath)
        {
            string waterMarkType = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkType");
            int type = Common.Globals.SafeInt(waterMarkType, 0);
            if (type == 0)
            {
                MakeTextWater(oldpath, newpath);
            }
            else
            {
                MakeImageWater(oldpath, newpath);
            }
        }

        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        /// <param name="_watermarkText"></param>
        public static void MakeTextWater(string oldpath, string newpath)
        {
            string waterMarkContent = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkContent");
            if (String.IsNullOrWhiteSpace(waterMarkContent))
            {
                waterMarkContent = "Maticsoft ";
            }
            string waterMarkFont = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFont");
            if (String.IsNullOrWhiteSpace(waterMarkFont))
            {
                waterMarkFont = "arial";
            }
            string waterMarkFontSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFontSize");
            if (String.IsNullOrWhiteSpace(waterMarkFontSize))
            {
                waterMarkFontSize = "14";
            }
            string waterMarkPosition = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (String.IsNullOrWhiteSpace(waterMarkPosition))
            {
                waterMarkPosition = "WM_CENTER";
            }
            string waterMarkFontColor = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFontColor");
            if (String.IsNullOrWhiteSpace(waterMarkFontColor))
            {
                waterMarkFontColor = "#FFFFFF";
            }
            try
            {
                Maticsoft.Common.ImageTools.addWatermarkText(oldpath, newpath, waterMarkContent, waterMarkPosition, waterMarkFont, Common.Globals.SafeInt(waterMarkFontSize, 14), waterMarkFontColor);

                //  , string _watermarkPosition = "WM_CENTER", string fontStyle = "arial", int fontSize = 14, string color = "#FFFFFF"
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 图片水印+缩略图
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        public static void MakeImageWaterThumbnail(string oldpath, string newpath, int width, int Height, MakeThumbnailMode mode)
        {
            string ImageMarkPosition = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (String.IsNullOrWhiteSpace(ImageMarkPosition))
            {
                ImageMarkPosition = "WM_CENTER";
            }
            string waterMarkTransparent = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkTransparent");
            if (String.IsNullOrEmpty(waterMarkTransparent))
            {
                waterMarkTransparent = "30";
            }
            string waterMarkPhotoUrl = Maticsoft.BLL.SysManage.ConfigSystem.GetValue("System_waterMarkPhotoUrl");
            if (String.IsNullOrEmpty(waterMarkPhotoUrl))
            {
                waterMarkPhotoUrl = "/Upload/WebSiteLogo/sitelogo.png";
            }
            try
            {
                Maticsoft.Common.ImageTools.MakeImageWaterThumbnail(oldpath, newpath, width, Height, mode, System.Drawing.Imaging.ImageFormat.Png, HttpContext.Current.Server.MapPath(waterMarkPhotoUrl), ImageMarkPosition, Common.Globals.SafeInt(waterMarkTransparent, 30));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 图片水印
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        public static void MakeImageWater(string oldpath, string newpath)
        {
            string ImageMarkPosition = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (String.IsNullOrWhiteSpace(ImageMarkPosition))
            {
                ImageMarkPosition = "WM_CENTER";
            }
            string waterMarkTransparent = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkTransparent");
            if (String.IsNullOrEmpty(waterMarkTransparent))
            {
                waterMarkTransparent = "30";
            }
            string waterMarkPhotoUrl = Maticsoft.BLL.SysManage.ConfigSystem.GetValue("System_waterMarkPhotoUrl");
            if (String.IsNullOrEmpty(waterMarkPhotoUrl) || !File.Exists(HttpContext.Current.Server.MapPath(waterMarkPhotoUrl)))
            {
                waterMarkPhotoUrl = "/Upload/WebSiteLogo/sitelogo.png";
            }
            if (File.Exists(HttpContext.Current.Server.MapPath(waterMarkPhotoUrl)))
            {
                try
                {
                    Maticsoft.Common.ImageTools.addWatermarkImage(oldpath, newpath, HttpContext.Current.Server.MapPath(waterMarkPhotoUrl), ImageMarkPosition, Common.Globals.SafeInt(waterMarkTransparent, 30));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        else
            {
                 File.Copy(oldpath, newpath,true);
            }
        }

        #endregion

        #region  时间格式
        /// <summary>
        /// 时间格式转换
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateToString(DateTime dt)
        {
            BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);
            string date = String.IsNullOrWhiteSpace(WebSiteSet.Date_Format) ? "yyyy-MM-dd" : WebSiteSet.Date_Format;
            string time = String.IsNullOrWhiteSpace(WebSiteSet.Time_Format) ? "HH:mm:ss" : WebSiteSet.Time_Format;
            return dt.ToString(date + " " + time);
        }
        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateToString(object dt)
        {
            DateTime date = Common.Globals.SafeDateTime(dt.ToString(), DateTime.Now);
            return DateToString(date);
        }
        #endregion


        #region 根据模板区域获取模板名称

        public static List<DirectoryInfo> GetThemeList(string AreaName)
        {
            string AreaBath = "/Areas/{0}/Themes";
            List<DirectoryInfo> themeList = new List<DirectoryInfo>();
            if (Directory.Exists(HttpContext.Current.Server.MapPath(String.Format(AreaBath, AreaName))))
            {
                var dirs = Directory.GetDirectories(HttpContext.Current.Server.MapPath(String.Format(AreaBath, AreaName)));
                if (dirs != null && dirs.Length > 0)
                {
                    foreach (var dir in dirs)
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                        themeList.Add(directoryInfo);
                    }
                }
            }
            return themeList;
        }

        #endregion

        #region  根据主区域获取模板
        public static List<Maticsoft.Model.Ms.Theme> GetThemes(string AreaName)
        {
            string AreaBath = "/Areas/{0}/Themes";

            List<Maticsoft.Model.Ms.Theme> themeList = new List<Maticsoft.Model.Ms.Theme>();
            if (Directory.Exists(HttpContext.Current.Server.MapPath(String.Format(AreaBath, AreaName))))
            {
                var dirs = Directory.GetDirectories(HttpContext.Current.Server.MapPath(String.Format(AreaBath, AreaName)));
                if (dirs != null && dirs.Length > 0)
                {
                    foreach (var dir in dirs)
                    {
                        Maticsoft.Model.Ms.Theme model = new Theme();
                        DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                        model.Name = directoryInfo.Name;
                        string filePath = String.Format(AreaBath, AreaName) + "/" + directoryInfo.Name;
                        model = GetThemeModel(filePath, model);
                        themeList.Add(model);
                    }
                }
            }

            return themeList;
        }


        public static Maticsoft.Model.Ms.Theme GetThemeModel(string FilePath, Maticsoft.Model.Ms.Theme model)
        {
            string ThemeText = "/Theme.xml";
            string ThemePhoto = "/Theme.png";
            string TextInfoPath = FilePath + ThemeText;
            if (File.Exists(HttpContext.Current.Server.MapPath(TextInfoPath)))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(HttpContext.Current.Server.MapPath(TextInfoPath));
                XmlElement rootElement = doc.DocumentElement;
                if (rootElement == null)
                {
                    return model;
                }
                model.Author = rootElement.SelectSingleNode("Author").InnerText;
                model.Description = rootElement.SelectSingleNode("Description").InnerText;
                model.Language = rootElement.SelectSingleNode("Language").InnerText;
            }
            string savePhoto = FilePath + ThemePhoto;
            model.PreviewPhotoSrc = savePhoto;
            return model;
        }

        #endregion

        public static string MoveImage(string ImageUrl, string savePath, string saveThumbsPath, Maticsoft.Model.Ms.EnumHelper.AreaType areaType)
        {
            try
            {
                var saveWay = "";
                if (areaType == Maticsoft.Model.Ms.EnumHelper.AreaType.SNS)
                {
                    saveWay = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay");
                }
                if (areaType == Maticsoft.Model.Ms.EnumHelper.AreaType.CMS)
                {
                    saveWay = BLL.SysManage.ConfigSystem.GetValueByCache("CMS_ImageStoreWay");
                }
                if (saveWay == "1")
                {
                    return ImageUrl + "|" + ImageUrl;
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(saveThumbsPath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(saveThumbsPath));

                    List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                        Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(areaType);

                    string imgname = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string destImage = "";
                    string originalUrl = "";
                    string thumbUrl = saveThumbsPath + imgname;
                    //首先移动原图片

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    {
                        originalUrl = String.Format(savePath + imgname, "");
                        System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(originalUrl));

                    }
                    if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                    {
                        foreach (var thumbSize in ThumbSizeList)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName))))
                            {
                                destImage = String.Format(thumbUrl, thumbSize.ThumName);
                                System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName)), HttpContext.Current.Server.MapPath(destImage));

                            }
                        }
                    }
                    return originalUrl + "|" + thumbUrl;
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