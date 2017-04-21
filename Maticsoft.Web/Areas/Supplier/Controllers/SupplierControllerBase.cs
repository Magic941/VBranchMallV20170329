using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Areas.Supplier.Controllers
{
    public class SupplierControllerBase : Maticsoft.Web.Controllers.ControllerBaseSupplier
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
            return Redirect("/SP/Account/Login");
        }

        #region 微信数据
        public string OpenId
        {
            get
            {
                return Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", SupplierId, currentUser.UserType);
            }
        }

        public string AppId
        {
            get
            {
                return Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", SupplierId, currentUser.UserType);
            }
        }

        public string AppSercet
        {
            get
            {
                return Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", SupplierId, currentUser.UserType);
            }
        }

        #endregion

    }
}
