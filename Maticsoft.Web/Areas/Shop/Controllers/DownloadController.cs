using System.Web.Mvc;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class DownloadController : ShopControllerBase
    {
        public ActionResult Android()
        {
            string path = Common.ConfigHelper.GetConfigString("FilePath_Android");
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "~/Download/MaticsoftShop.apk";
            }
            path = Server.MapPath(path);
            string name = System.IO.Path.GetFileName(path);
            return File(path, "application/vnd.android.package-archive", Url.Encode(name));
        }
    }
}
