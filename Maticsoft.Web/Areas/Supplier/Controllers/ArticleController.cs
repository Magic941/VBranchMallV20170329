using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.Supplier.Controllers
{
    public class ArticleController : SupplierControllerBase
    {
        //
        // GET: /Supplier/Article/

        public ActionResult Index()
        {
            return View();
        }

    }
}
