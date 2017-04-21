using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.ViewModel.Member;
using Maticsoft.Accounts.Bus;
using System.Web.Security;
using Maticsoft.Common;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class AccountController : COMControllerBase
    {
        //
        // GET: /COM/Account/

        public ActionResult Index()
        {
            return View();
        }

        #region 登录
        public ActionResult Login(string returnUrl)
        {

            if (!string.IsNullOrWhiteSpace(returnUrl))
                ViewBag.returnUrl = returnUrl;
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType == "AA")
                return Redirect(ViewBag.BasePath + "admin/Couponex");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(model.UserName, model.Password);
                if (userPrincipal == null)
                {
                    ModelState.AddModelError("Message", "用户名或密码不正确, 请重新输入!");
                    return View(model);
                }
                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    ModelState.AddModelError("Message", "对不起，该帐号已被冻结或未激活，请联系管理员！");
                    return View(model);
                }
                HttpContext.User = userPrincipal;
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                Session[Maticsoft.Common.Globals.SESSIONKEY_ADMIN] = currentUser;
                returnUrl = Server.UrlDecode(returnUrl);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect(ViewBag.BasePath + "admin/Couponex");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AjaxLogin(string UserName, string UserPwd)
        {
            if (ModelState.IsValid)
            {
                AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(UserName, UserPwd);
                if (userPrincipal != null)
                {
                    User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                    if (!currentUser.Activity)
                    {
                        ModelState.AddModelError("Message", "对不起，该帐号已被冻结，请联系管理员！");
                    }
                   
                    HttpContext.User = userPrincipal;
                    FormsAuthentication.SetAuthCookie(UserName, true);
                    Session[Maticsoft.Common.Globals.SESSIONKEY_ADMIN] = currentUser;
                    return Content("1");
                }
                else
                {
                    return Content("0");
                }
            }
            return Content("0");
        }
        #region 退出
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(Globals.SESSIONKEY_ADMIN);
            Session.Clear();
            Session.Abandon();
            return Redirect(ViewBag.BasePath);
            // return RedirectToAction("Index", "Home", new { id=1,viewname = "url"});
        }
        #endregion
        #endregion


    }
}
