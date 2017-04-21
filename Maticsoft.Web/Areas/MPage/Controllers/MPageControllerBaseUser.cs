using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    [MPageError]
    public class MPageControllerBaseUser : Maticsoft.Web.Controllers.ControllerBaseUser
    {
        //
        // GET: /Mobile/MobileControllerBaseUser/

        #region 覆盖父类的  ViewResult View 方法 用于ViewName动态判空
        protected new ViewResult View(string viewName, object model)
        {
            return !string.IsNullOrWhiteSpace(viewName) ? base.View(viewName, model) : View(model);
        }

        protected new ViewResult View(string viewName)
        {
            return !string.IsNullOrWhiteSpace(viewName) ? base.View(viewName) : View();
        }
        #endregion

        public override ActionResult RedirectToLogin(ActionExecutingContext filterContext)
        {
            string rawurl = Request.RawUrl;
            return Redirect(ViewBag.BasePath+"a/l/?returnUrl=" + Server.UrlEncode(rawurl));
            //return RedirectToAction("Login", "Account", new {id=1, area = "MPage", returnUrl = Server.UrlEncode(rawurl),viewname="url"});
        }
    }
}
