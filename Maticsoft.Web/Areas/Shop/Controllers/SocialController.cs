using System;
using System.Web.Mvc;
using Maticsoft.Web.Controllers;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class SocialController : SocialControllerBase
    {
        public override ActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "UserCenter", new { area = "Shop" });
        }
        public override ActionResult RedirectToUserBind()
        {
            return RedirectToAction("UserBind", "UserCenter", new { area = "Shop" });
        }
    }
}
