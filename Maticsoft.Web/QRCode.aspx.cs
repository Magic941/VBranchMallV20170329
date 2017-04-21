using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ThoughtWorks;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Maticsoft.Web
{
    public partial class QRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Url"] != null && !string.IsNullOrWhiteSpace(Request["Url"].ToString()))
            {
                string url = Request["Url"].ToString().Trim();
                Bitmap bt;
                string enCodeString = url;
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 4;
                qrCodeEncoder.QRCodeVersion = 8;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);


                Bitmap bLogo = new Bitmap(Server.MapPath("/Areas/MShop/Themes/M1/Content/images/Logo4Code.png")); 
                bLogo = new Bitmap(bLogo, 80, 80);                  
                int Y = bt.Height;
                int X = bt.Width;
                Point point = new Point(X / 2 - 40, Y / 2 - 40);
                Graphics g = Graphics.FromImage(bt);              
                g.DrawImage(bLogo, point);

                MemoryStream ms = new MemoryStream();
                bt.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                Response.Cache.SetNoStore();	
                Response.ClearContent();
                Response.ContentType = "image/Jpeg";
                Response.BinaryWrite(ms.ToArray());
                bt.Dispose();
            }
        }
    }
}