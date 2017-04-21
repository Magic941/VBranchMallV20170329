using System;
using System.Globalization;
using System.IO;
using System.Web;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.ExpressTemplate.Flex
{
    public partial class UploadFile : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpFileCollection files = base.Request.Files;
            if (files.Count > 0)
            {
                string str = HttpContext.Current.Request.MapPath(
                    Globals.ApplicationPath + "/Upload/ExpressTemplate");
                HttpPostedFile file = files[0];
                if ((file == null) || (file.ContentLength <= 0))
                {
                    base.Response.Write("2");
                }
                else
                {
                    string extension = Path.GetExtension(file.FileName);
                    if (((extension.ToLower() != ".jpg") && (extension.ToLower() != ".gif")) && ((extension.ToLower() != ".jpeg") && (extension.ToLower() != ".png")))
                    {
                        base.Response.Write("1");
                    }
                    else
                    {
                        string s = DateTime.Now.ToString("yyyyMMdd") + new Random().Next(0x2710, 0x1869f).ToString(CultureInfo.InvariantCulture) + extension;
                        string filename = str + "/" + s;

                        if (!Directory.Exists(str))
                            Directory.CreateDirectory(str);

                        try
                        {
                            file.SaveAs(filename);
                            base.Response.Write(s);
                        }
                        catch
                        {
                            base.Response.Write("0");
                        }
                    }
                }
            }
            else
            {
                base.Response.Write("2");
            }
        }
    }
}

