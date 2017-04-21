using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Web.Areas.MShop.Controllers;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class COMControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {
        #region UserName
        public string UserOpen
        {
            get
            {
                if (Session["WeChat_UserName"] != null)
                {
                    return Session["WeChat_UserName"].ToString();
                }
                return  String.Empty;
            }
        }
        #endregion

        #region  OpenId
        public string OpenId
        {
            get
            {
                if (Session["WeChat_OpenId"] != null)
                {
                    return Session["WeChat_OpenId"].ToString();
                }
                return  String.Empty;
            }
        }
        #endregion

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
