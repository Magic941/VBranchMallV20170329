using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.Web.Controllers;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class COMControllerBaseAdmin : ControllerBaseAdmin
    {

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
            return RedirectToAction("Login", "Account", new { area = "COM" });
        }

    }
}
