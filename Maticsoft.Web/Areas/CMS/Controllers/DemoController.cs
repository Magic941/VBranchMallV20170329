using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    public class DemoController : CMSControllerBase
    {
        //
        // GET: /CMS/Demo/

        public ActionResult Index()
        {
            return View("UploadImg");
        }

    }
}
