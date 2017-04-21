using System;
using System.Web.Security;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web
{
    /// <summary>
    /// 需要权限验证的页面基类：如用户中心
    /// </summary>
    /// <remarks>停止使用, 向MVC迭代</remarks>
    [Obsolete]
    public class PageBaseUser : PageBase
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
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ValidatingPermission();
        }

        public override void InitializeComponent()
        {
            if (!Context.User.Identity.IsAuthenticated) return;
#if false   // 停止从Context.User.Identity.Name获取用户名进行自动登录
            userPrincipal = new AccountsPrincipal(Context.User.Identity.Name);
            if (Session[Globals.SESSIONKEY_USER] == null)
            {
                currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                Session[Globals.SESSIONKEY_USER] = currentUser;
                Session["Style"] = currentUser.Style;
            }
            else
            {
                currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_USER];
                Session["Style"] = currentUser.Style;
            }
#else
            if (Session[Globals.SESSIONKEY_USER] == null) return;
            currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_USER];
            if (currentUser == null) return;
            Session["Style"] = currentUser.Style;
            userPrincipal = new AccountsPrincipal(currentUser.UserName);
#endif
        }
        private void ValidatingPermission()
        {
            if (Context.User.Identity.IsAuthenticated && userPrincipal != null)
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
                FormsAuthentication.SignOut();
                Session.Remove(Globals.SESSIONKEY_SUPPLIER); // ????
                //Session.Clear();
                //Session.Abandon();
                Response.Clear();
                Response.Write("<script defer>window.alert('" + Resources.Site.TooltipNoAuthenticated + "');parent.location='" + DefaultLogin + "';</script>");
                Response.End();
            }
        }

        #endregion









    }
}