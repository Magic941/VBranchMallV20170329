using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Controllers
{
    public abstract class ControllerBaseSupplier : ControllerBase
    {

        #region 供应商ID
        public int SupplierId
        {
            get
            {
                if (CurrentUser.UserType == "AG")
                {
                    return Globals.SafeInt(Session["Agent_SupplierId"], -1);
                }
                return Globals.SafeInt(currentUser.DepartmentID, -1);
            }
        }
        #endregion

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

            #region 固定当前区域的基础路径
            ViewBag.BasePath = "/SP/";
            #endregion
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
                Session.Remove(Globals.SESSIONKEY_SUPPLIER);
                Session.Clear();
                Session.Abandon();
                filterContext.Result = RedirectToLogin(filterContext);
                return false;
            }
            if (Session[Globals.SESSIONKEY_SUPPLIER] == null)
            {
                currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                Session[Globals.SESSIONKEY_SUPPLIER] = currentUser;
                Session["Style"] = currentUser.Style;
            }
            else
            {
                currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_SUPPLIER];
                Session["Style"] = currentUser.Style;
            }

            if (CurrentUser.UserType == "AG" && !string.IsNullOrWhiteSpace(Request.QueryString["AGSupplierId"]))
            {
                Session["Agent_SupplierId"] = Request.QueryString["AGSupplierId"];
                if (SupplierId > 0)
                {
                    Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
                    Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModelByCache(SupplierId);
                    if (model.AgentId.ToString() != CurrentUser.DepartmentID)
                    {
                        filterContext.Result = RedirectToLogin(filterContext);
                        return false;
                    }
                }
            }

            if (CurrentUser == null || (CurrentUser.UserType != "SP" && CurrentUser.UserType != "AG") ||
                (CurrentUser.UserType == "AG" && SupplierId < 1))
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
