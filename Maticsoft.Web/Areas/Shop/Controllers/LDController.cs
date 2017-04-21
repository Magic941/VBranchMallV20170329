using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class LDController:ShopControllerBase
    {
        public ViewResult Index(string v)
        {
            //return View("~/Areas/Shop/Themes/M1/Views/LD/"+viewName + ".cshtml");
            return View(v);
        }

        public ViewResult Specialty(string ViewName = "Specialty")
        {
            return View(ViewName);
        }
    }
}