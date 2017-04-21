using System;
using System.Web.Security;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web
{
    /// <summary>
    /// 需要权限验证的页面基类：企业后台
    /// </summary>
    public class PageBaseEnterprise : PageBase
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
            if (Session[Globals.SESSIONKEY_ENTERPRISE] == null)
            {
                userPrincipal = new AccountsPrincipal(Context.User.Identity.Name);
                currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                Session[Globals.SESSIONKEY_ENTERPRISE] = currentUser;
                Session["Style"] = currentUser.Style;
            }
            else
            {
                currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_ENTERPRISE];
                Session["Style"] = currentUser.Style;
            }
#else
            if (Session[Globals.SESSIONKEY_ENTERPRISE] == null) return;
            currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_ENTERPRISE];
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
                Session.Clear();
                Session.Abandon();
                Response.Redirect(DefaultLoginEnterprise);
            }
        }

        #endregion

    }
}