using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class MPageErrorAttribute : FilterAttribute, IExceptionFilter
    {
        #region IExceptionFilter 成员

        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Error");
        }

        #endregion

    }
}
