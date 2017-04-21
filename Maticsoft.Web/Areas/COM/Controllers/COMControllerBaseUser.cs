using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class COMControllerBaseUser : Maticsoft.Web.Controllers.ControllerBaseUser
    {
        //
        // GET: /COM/COMControllerBaseUser/
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

        #region UserName
        public string UserOpen
        {
            get
            {
                if (Session["WeChat_UserName"] != null)
                {
                    return Session["WeChat_UserName"].ToString();
                }
                return String.Empty;
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
                return String.Empty;
            }
        }
        #endregion

        public override ActionResult RedirectToLogin(ActionExecutingContext filterContext)
        {
            string rawurl = Request.RawUrl;
            bool IsAutoLogin = Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AutoLogin", -1, "AA"), false);
            #region  自动登陆
            string MShopBase = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
            bool IsNeedBind = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SyStem_WeChat_UserBind");

            if (Session[Maticsoft.Common.Globals.SESSIONKEY_USER] != null && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                return String.IsNullOrWhiteSpace(rawurl) ? Redirect(ViewBag.BasePath + "u") : Redirect(rawurl);
            }
            Maticsoft.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
            if (String.IsNullOrWhiteSpace(OpenId) || String.IsNullOrWhiteSpace(UserOpen))
            {
                return Redirect(MShopBase + "a/l/?returnUrl=" + Server.UrlEncode(rawurl));
            }
            Maticsoft.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(OpenId, UserOpen);

            if (IsNeedBind)
            {
                if (wUserModel.UserId <= 0)
                {
                    return Redirect(MShopBase + "a/l/?returnUrl=" + Server.UrlEncode(rawurl));
                }
                AccountsPrincipal userPrincipal = new AccountsPrincipal(wUserModel.UserId);
                if (userPrincipal == null)
                {
                    return Redirect(MShopBase + "a/l/?returnUrl=" + Server.UrlEncode(rawurl));
                }
                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    return Redirect(MShopBase + "a/l/?returnUrl=" + Server.UrlEncode(rawurl));
                }
                HttpContext.User = userPrincipal;
                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
                FormsAuthentication.SetAuthCookie(currentUser.UserName, true);
                return String.IsNullOrWhiteSpace(rawurl) ? Redirect(ViewBag.BasePath + "u") : Redirect(rawurl);
            }

            if (IsAutoLogin)
            {
           
                if (wUserModel.UserId <= 0)
                {
                    return Redirect(ViewBag.BasePath + "WeChat/RegBind?returnUrl=" + Server.UrlEncode(rawurl));
                }
                AccountsPrincipal userPrincipal = new AccountsPrincipal(wUserModel.UserId);
                if (userPrincipal == null)
                {
                    return Redirect(ViewBag.BasePath + "WeChat/RegBind?returnUrl=" + Server.UrlEncode(rawurl));
                }
                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    return Redirect(ViewBag.BasePath + "WeChat/RegBind?returnUrl=" + Server.UrlEncode(rawurl));
                }
                HttpContext.User = userPrincipal;
                Session[Maticsoft.Common.Globals.SESSIONKEY_USER] = currentUser;
                FormsAuthentication.SetAuthCookie(currentUser.UserName, true);
                return String.IsNullOrWhiteSpace(rawurl) ? Redirect(MShopBase + "u") : Redirect(rawurl);
            }
            #endregion
            return Redirect(MShopBase + "a/l/?returnUrl=" + Server.UrlEncode(rawurl));

            //return RedirectToAction("Login", "Account", new {id=1, area = "MShop", returnUrl = Server.UrlEncode(rawurl),viewname="url"});
        }

    }
}
