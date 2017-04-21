using System;
using System.Web.Security;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web
{
    /// <summary>
    /// 需要权限验证的页面基类：商家后台
    /// </summary>
    public class PageBaseSupplier : PageBase
    {
        private readonly BLL.Shop.Supplier.SupplierInfo supplierManage = new BLL.Shop.Supplier.SupplierInfo();
        #region 默认登录地址
        public static string DefaultLoginSupplier
        {
            get
            {
                string url = BLL.SysManage.ConfigSystem.GetValueByCache("DefaultLoginSupplier");

                if (string.IsNullOrWhiteSpace(url)) url = "/supplier/login.aspx";

                return url;
            }
        }
        #endregion

        #region 商家ID
        public int SupplierId
        {

            get
            {
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
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ValidatingPermission();
        }
        public override void InitializeComponent()
        {
            if (!Context.User.Identity.IsAuthenticated) return;

#if false   // 停止从Context.User.Identity.Name获取用户名进行自动登录
            if (Session[Globals.SESSIONKEY_SUPPLIER] == null)
            {
                userPrincipal = new AccountsPrincipal(Context.User.Identity.Name);
                currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                Session[Globals.SESSIONKEY_SUPPLIER] = currentUser;
                Session["Style"] = currentUser.Style;
            }
            else
            {
                currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_SUPPLIER];
                Session["Style"] = currentUser.Style;
            }
#else
            if (Session[Globals.SESSIONKEY_SUPPLIER] == null) return;
            currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_SUPPLIER];
            Session["Style"] = currentUser.Style;

            if (currentUser.UserType != "SP")
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Clear();
                Response.Write("<script defer>parent.location='" + DefaultLoginSupplier + "';</script>");
                Response.End();
                return;
            }

            Model.Shop.Supplier.SupplierInfo model = supplierManage.GetModelByCache(Globals.SafeInt(currentUser.DepartmentID, -1));
            if (model == null || model.Status != 1)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Clear();
                Response.Write("<script defer>parent.location='" + DefaultLoginSupplier + "';</script>");
                Response.End();
                return;
            }
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
                Response.Redirect(DefaultLoginSupplier);
            }
        }

        #endregion


        #region 错误处理
        protected override void PageError(object sender, EventArgs e)
        {
            string errMsg = "";
            Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
            Exception currentError = Server.GetLastError();
            if (currentError is System.Data.SqlClient.SqlException)
            {
                System.Data.SqlClient.SqlException sqlerr = (System.Data.SqlClient.SqlException)currentError;
                if (sqlerr != null)
                {
                    string sqlMsg = GetSqlExceptionMessage(sqlerr.Number);
                    if (sqlerr.Number == 547)
                    {
                        errMsg += "<h1 class=\"SystemTip\">" + Resources.Site.ErrorSystemTip + "</h1><br/> " +
                        "<font class=\"ErrorPageText\">" + sqlMsg + "</font>";
                    }
                    else
                    {
                        errMsg += "<h1 class=\"ErrorMessage\">" + Resources.Site.ErrorSystemTip + "</h1><hr/> " +
                        "该信息已被系统记录，请稍后重试或与管理员联系。<br/>" +
                        "错误信息： <font class=\"ErrorPageText\">" + sqlMsg + "</font>";
                        model.Loginfo = sqlMsg;
                        model.StackTrace = currentError.ToString();
                        model.Url = Request.Url.AbsoluteUri;
                    }
                }
            }
            else
            {
                errMsg += "<h1 class=\"ErrorMessage\">" + Resources.Site.ErrorSystemTip + "</h1><hr/> " +
                    "该信息已被系统记录，请稍后重试或与管理员联系。<br/>" +
                    "错误信息： <font class=\"ErrorPageText\">" + currentError.Message.ToString() + "<hr/>" +
                    "<b>Stack Trace:</b><br/>" + currentError.StackTrace + "</font>";

                model.Loginfo = currentError.Message;
                model.StackTrace = currentError.StackTrace;
                model.Url = Request.Url.AbsoluteUri;
            }
            Maticsoft.BLL.SysManage.ErrorLog.Add(model);

            Session["ErrorMsg"] = errMsg;
            Server.Transfer("~/Supplier/ErrorPage.aspx", true);

            //考虑不Response当前页面，直接弹出信息提示。根据不同信息做不同的样式处理：系统提示，系统错误
            //Response.Write(errMsg);
            //Server.ClearError();
        }
        private string GetSqlExceptionMessage(int number)
        {
            //set default value which is the generic exception message
            string error = Resources.Site.ErrorMessageSQL;
            switch (number)
            {
                case 17:

                    // 	SQL Server does not exist or access denied.
                    error = Resources.Site.ErrorMessageSQL17;
                    break;

                case 547:

                    // ForeignKey Violation
                    error = Resources.Site.ErrorMessageSQL547;
                    break;

                case 4060:

                    // Invalid Database
                    error = Resources.Site.ErrorMessageSQL4060;
                    break;

                case 18456:

                    // Login Failed
                    error = Resources.Site.ErrorMessageSQL18456;
                    break;

                case 1205:

                    // DeadLock Victim
                    error = Resources.Site.ErrorMessageSQL1205;
                    break;

                case 2627:
                    error = Resources.Site.ErrorMessageSQL2627;
                    break;

                case 2601:

                    // Unique Index/Constriant Violation
                    error = Resources.Site.ErrorMessageSQL2601;
                    break;
                default:

                    // throw a general DAL Exception
                    error = Resources.Site.ErrorMessageSQL;
                    break;
            }

            return error;
        }

        #endregion 错误处理
    }
}