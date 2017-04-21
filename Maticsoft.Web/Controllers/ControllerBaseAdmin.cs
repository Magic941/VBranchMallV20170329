using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Common;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Controllers
{
    public abstract class ControllerBaseAdmin : ControllerBase
    {

        #region 权限控制
        //子类通过 new 重写该值
        //protected int Act_DeleteList = 1; //批量删除按钮

        protected int permissionid = -1;

        /// <summary>
        /// 页面访问需要的权限。可以在不同页面继承里来控制不同页面的权限。默认-1为无限制。
        /// </summary>
        public int PermissionID
        {
            set
            {
                permissionid = value;
            }
            get
            {
                return permissionid;
            }
        }

        #endregion

        #region 初始化

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            
        }

        protected override bool InitializeComponent(ActionExecutingContext filterContext)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = RedirectToLogin(filterContext);
                return false;
            }
            try
            {
                userPrincipal = new AccountsPrincipal(HttpContext.User.Identity.Name);
            }
            catch (System.Security.Principal.IdentityNotMappedException)
            {
                //用户在DB中不存在 退出
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Remove(Globals.SESSIONKEY_ADMIN);
                Session.Clear();
                Session.Abandon();
                filterContext.Result = RedirectToLogin(filterContext);
                return false;
            }
            if (Session[Globals.SESSIONKEY_ADMIN] == null)
            {
                currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                Session[Globals.SESSIONKEY_ADMIN] = currentUser;
                Session["Style"] = currentUser.Style;
            }
            else
            {
                currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_ADMIN];
                Session["Style"] = currentUser.Style;
            }
          

            if (CurrentUser == null || (CurrentUser.UserType != "AA" ) )
            {
                filterContext.Result = RedirectToLogin(filterContext);
                return false;
            }

            //追加权限验证
            ValidatingPermission();
            return true;
        }

        public abstract ActionResult RedirectToLogin(ActionExecutingContext filterContext);

        private void ValidatingPermission()
        {
            if (HttpContext.User.Identity.IsAuthenticated && userPrincipal != null)
            {
                if ((PermissionID != -1) && (!userPrincipal.HasPermissionID(PermissionID)))
                {
                    Response.Clear();
                    Response.Write("<script defer>window.alert('" + Resources.Site.TooltipNoPermission + "');history.back();</script>");
                    Response.End();
                }
            }
            else
            {
                Response.Clear();
                Response.Write("<script defer>window.alert('" + Resources.Site.TooltipNoAuthenticated + "');parent.location='" + DefaultLogin + "';</script>");
                Response.End();
            }
        }
        #endregion

    }
}
