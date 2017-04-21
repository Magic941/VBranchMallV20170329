using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Web.Areas.Shop.Controllers;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    /// <summary>
    /// Mobile网站前台基类
    /// </summary>
    [MPageError]
    public class MPageControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {
        //
        // GET: /Mobile/MPageControllerBase/

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

    }
}
