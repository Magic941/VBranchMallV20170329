using System;
using System.IO;
using System.Text;
using System.Web;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.ExpressTemplate.Flex
{
    public partial class XmlData : PageBaseAdmin
    {
        BLL.Shop.Sales.ExpressTemplate bll = new BLL.Shop.Sales.ExpressTemplate();
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = base.Request.Form["xmlname"];
            string s = base.Request.Form["xmldata"];
            string str3 = base.Request.Form["expressname"];
            if ((!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str3)) && bll.Add(new Model.Shop.Sales.ExpressTemplate { ExpressName = str3, XmlFile = str + ".xml" }) > 0)
            {
                string path = HttpContext.Current.Request.MapPath(
                        Globals.ApplicationPath + string.Format(
                            "/{0}/ExpressTemplate/",
                            MvcApplication.UploadFolder));

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (FileStream stream = new FileStream(
                    Path.Combine(path,str+".xml") , FileMode.Create))
                {
                    byte[] bytes = new UTF8Encoding().GetBytes(s);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                    stream.Close();
                }
            }
        }
    }
}

