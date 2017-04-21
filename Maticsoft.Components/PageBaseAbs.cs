using System;
using System.Collections;
using System.Globalization;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web
{
    public interface IPageBaseMessageTip
    {
        string TooltipDelConfirm { get; }
        string lblTrue { get; }
        string lblFalse { get; }
        string TooltipNoPermission { get; }
        string TooltipNoAuthenticated { get; }
        string TooltipForceLogin { get; }
    }
    /// <summary>
    /// 页面基类接口
    /// </summary>
    public interface IPageBaseOption
    {
        string DefaultLogin { get; }
        string DefaultLoginEnterprise { get; }
        string DefaultLoginAdmin { get; }
    }

    /// <summary>
    /// 页面层(表示层)基类,所有前台页面继承，无权限验证
    /// </summary>
    public abstract class PageBaseAbs : System.Web.UI.Page
    {
        protected static IPageBaseOption PageBaseOption;
        protected static IPageBaseMessageTip PageBaseMessageTip;
        public PageBaseAbs(IPageBaseOption option, IPageBaseMessageTip message)
        {
            PageBaseOption = option;
            PageBaseMessageTip = message;

            DefaultLogin = PageBaseOption.DefaultLogin;
            DefaultLoginEnterprise = PageBaseOption.DefaultLoginEnterprise;
            ToolTipDelete = PageBaseMessageTip.TooltipDelConfirm;
        }

        protected readonly string DefaultLogin;
        protected readonly string DefaultLoginEnterprise;
        protected static Hashtable ActHashtab;
        public readonly string ToolTipDelete;

        #region 用户信息

        protected AccountsPrincipal userPrincipal;
        /// <summary>
        ///  权限角色验证对象
        /// </summary>
        public AccountsPrincipal UserPrincipal
        {
            get
            {
                return userPrincipal;
            }
        }
        protected Maticsoft.Accounts.Bus.User currentUser;
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public Maticsoft.Accounts.Bus.User CurrentUser
        {
            get
            {
                return currentUser;
            }
        }
        #endregion

        #region 初始化
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
            this.Error += new System.EventHandler(PageBase_Error);

            //SingleLogin slogin = new SingleLogin();
            //if (slogin.ValidateForceLogin())
            //{
            //    Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipForceLogin + "');parent.location='" + defaullogin + "';</script>");
            //}

            Actions bllAction = new Actions();
            ActHashtab = bllAction.GetHashListByCache();

        }

        /// <summary>
        /// 加载已登录用户对象和Style数据, 子类可替换此基础逻辑
        /// </summary>
        public virtual void InitializeComponent()
        {
            //DONE: 20120922单点登录功能恢复 BEN ADD
            if (Context.User.Identity.IsAuthenticated)
            {
                userPrincipal = new AccountsPrincipal(Context.User.Identity.Name);
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
            }
        }

        #endregion

        #region 子类实现
        protected abstract void PageError(object sender, System.EventArgs e);
        #endregion

        #region 错误处理
        //错误处理
        protected void PageBase_Error(object sender, System.EventArgs e)
        {
            PageError(sender, e);
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 根据功能行为编号得到所属权限编号
        /// </summary>
        /// <returns></returns>
        public int GetPermidByActID(int ActionID)
        {
            object obj = ActHashtab[ActionID.ToString()];
            if (obj != null && obj.ToString().Length > 0)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 转换Bool类型的文本多语言描述
        /// </summary>
        /// <param name="bValid"></param>
        /// <returns></returns>
        public string GetboolText(string boolValue)
        {
            return boolValue.Trim().ToLower() == "true" ? "<span style=\"color: #006600;\">" + PageBaseMessageTip.lblTrue + "</span>" : "<span style=\"color: #800000;\">" + PageBaseMessageTip.lblFalse + "</span>";

        }

        protected void SignOut(string sessionKey)
        {
            if (Globals.IsPublicSession)
            {

            }

        }

        protected void GoPage()
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["return"]))
            {
                //说明用户是被另一个页面导过来的,登录成功,我们把用户再导回去.
                Response.Redirect(Request.QueryString["return"]);
            }
            else
            {
                if (currentUser != null)
                {
                    switch (currentUser.UserType)//UU用户，EE商户，AG代理商，AA管理员
                    {
                        case "AA":
                            Response.Redirect("/Admin/Main.htm");
                            break;
                        case "UU":
                            Response.Redirect("/Member/index.aspx");
                            break;
                        case "EE":
                            Response.Redirect("/Enterprise/index.aspx");
                            break;
                        case "AG":
                            Response.Redirect("/Agent/index.aspx");
                            break;
                        default:
                            break;
                    }
                }

            }
        }



        #endregion

        #region 字段数据转换

        /// <summary>
        /// 百分率转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string TranslateToPercent(string str)
        {
            decimal weight = Convert.ToDecimal(str);
            return weight.ToString("#0.##%", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 审批状态
        /// </summary>
        /// <param name="Value">审核人</param>
        /// <returns></returns>
        public string GetApprovedText(object Value)
        {
            bool isApproved = false;
            if (Value != null && Value.ToString().Length > 0)
            {
                if (Convert.ToInt32(Value) > 0)
                {
                    isApproved = true;
                }
            }
            return isApproved ? "<span style=\"color: #006600;\">" + PageBaseMessageTip.lblTrue + "</span>" : "<span style=\"color: #800000;\">" + PageBaseMessageTip.lblFalse + "</span>";
        }

        #endregion
    }
}