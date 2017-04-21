using System.Web.Mvc;
using Maticsoft.Web.Controllers;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class SocialController : SocialControllerBase
    {
        public override ActionResult RedirectToUserBind()
        {
            return RedirectToAction("Index", "Home", new { area = "MPage" });
        }
        public override ActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home", new { area = "MPage" });
        }
    }
}
