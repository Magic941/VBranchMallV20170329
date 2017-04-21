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
using System.Drawing.Drawing2D;




namespace Maticsoft.Web
{
    public partial class MyCert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["truename"] != null && !string.IsNullOrWhiteSpace(Request["truename"].ToString()))
            {

                string truename = Request.QueryString["truename"];
                string imgurl = Server.MapPath(@"\Upload\User\Certificate\cert.png");
                System.Drawing.Image myImage = System.Drawing.Image.FromFile(imgurl);
                Bitmap map = new Bitmap(myImage);
                myImage.Dispose();
                Graphics graphics = Graphics.FromImage(map);
                // graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                System.Drawing.Color a = System.Drawing.Color.FromArgb(25, 25, 25);
                SolidBrush brush = new SolidBrush(a);

                PointF P = new PointF(300, 360);
                FontFamily fs = new FontFamily("微软雅黑");
                Font font = new Font(fs, 20);
                graphics.DrawString(truename.Trim(), font, brush, P);
                // map.Save(@"E:\b2c\B2C分支\外网分支B2C\Maticsoft.Web\Upload\User\Certificate\cert.png", ImageFormat.Jpeg);
                MemoryStream ms = new MemoryStream();
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Png);





                Response.Cache.SetNoStore();
                Response.ClearContent();
                Response.ContentType = "image/Jpeg";
                Response.BinaryWrite(ms.ToArray());
                font.Dispose();
                graphics.Dispose();
                map.Dispose();
            }
           
        }
    }
}