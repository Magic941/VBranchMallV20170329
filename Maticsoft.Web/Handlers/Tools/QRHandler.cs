/**
* QRHandler.cs
*
* 功 能： QR组件
* 类 名： QRHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/08/12 18:10:00  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using System.Net;

namespace Maticsoft.Web.Handlers.Tools
{
    #region ImageMimeTypeEx
    public static class ImageMimeTypeEx
    {
        public static string GetMimeType(this Image image)
        {
            return image.RawFormat.GetMimeType();
        }

        public static string GetMimeType(this ImageFormat imageFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }
    }
    #endregion

    public class QRHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod != "GET") return;
            string action = context.Request.QueryString["action"];
            switch (action)
            {
                case "DeQRCode":
                    DeQRCode(context);
                    break;
                default:
                    GenQRCode(context);
                    break;
            }
        }
        #endregion

        #region 生成QRCode
        private static void GenQRCode(HttpContext context)
        {
            string content = context.Request.QueryString["content"];
            if (string.IsNullOrWhiteSpace(content)) return;
            content = Common.Globals.UrlDecode(content);

            string level = context.Request.QueryString["level"];
            level = string.IsNullOrWhiteSpace(level) ? "M" : level.ToUpper();

            string format = context.Request.QueryString["format"];
            format = string.IsNullOrWhiteSpace(format) ? "png" : format.ToLower();

            int margin = Common.Globals.SafeInt(context.Request.QueryString["margin"], 4);
            int size = Common.Globals.SafeInt(context.Request.QueryString["size"], 100);

            ErrorCorrectionLevel errorCorrectionLevel;
            ImageFormat imgFormat;
            BarcodeFormat barcodeFormat = Common.Globals.SafeEnum(context.Request.QueryString["mod"],
                BarcodeFormat.QR_CODE);


            #region ErrorCorrectionLevel
            switch (level)
            {
                case "L":
                    errorCorrectionLevel = ErrorCorrectionLevel.L;
                    break;
                case "M":
                    errorCorrectionLevel = ErrorCorrectionLevel.M;
                    break;
                case "Q":
                    errorCorrectionLevel = ErrorCorrectionLevel.Q;
                    break;
                case "H":
                    errorCorrectionLevel = ErrorCorrectionLevel.H;
                    break;
                default:
                    errorCorrectionLevel = ErrorCorrectionLevel.M;
                    break;
            }
            #endregion

            #region ImageFormat
            switch (format)
            {
                case "jpeg":
                    imgFormat = ImageFormat.Jpeg;
                    break;
                case "gif":
                    imgFormat = ImageFormat.Gif;
                    break;
                case "bmp":
                    imgFormat = ImageFormat.Bmp;
                    break;
                default:
                    imgFormat = ImageFormat.Png;
                    break;
            }
            #endregion

            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,                      //禁用ECI编码段: use UTF-8 encoding and the ECI segment is omitted
                CharacterSet = "UTF-8",                 //使用UTF-8编码
                Width = size,                           //设置宽度
                Height = size,                          //设置高度
                Margin = margin,                        //设置间隙
                ErrorCorrection = errorCorrectionLevel  //容错等级
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = barcodeFormat;            //采用QR编码
            writer.Options = options;

            context.Response.Clear();
            context.Response.ContentType = imgFormat.GetMimeType(); //设置输出流ContentType

            using (Bitmap image = writer.Write(content))    //输出二维码图像
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, imgFormat);
                    ms.WriteTo(context.Response.OutputStream);
                }
            }
            context.Response.Output.Flush();
            context.Response.End();
        }
        #endregion

        #region 解析QRCode
        private static void DeQRCode(HttpContext context)
        {
            string dataUrl = context.Request.QueryString["url"];
            if (string.IsNullOrWhiteSpace(dataUrl)) return;

            dataUrl = Common.Globals.UrlDecode(dataUrl);

            context.Response.Clear();
            context.Response.Write(Decode(dataUrl));

            context.Response.Output.Flush();
            context.Response.End();
        }

        public static string Decode(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return string.Empty;
            BarcodeReader reader = new BarcodeReader();
            Result result;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true;

                WebProxy proxy = new WebProxy();
                proxy.BypassProxyOnLocal = true;
                proxy.UseDefaultCredentials = true;

                request.Proxy = proxy;
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream == null) return string.Empty;

                    Bitmap bmp = new Bitmap(stream);
                    result = reader.Decode(bmp);
                }
            }
            catch (WebException)
            {
                return string.Empty;
                //throw;
            }
            return result.Text;
        }
        #endregion
    }
}