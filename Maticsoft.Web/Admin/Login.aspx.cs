using System;
using System.Web;
using System.Web.Security;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = MvcApplication.SiteName+"-系统登录";

            if (Session[Globals.SESSIONKEY_ADMIN] != null)
            {
                Response.Redirect("main.htm");
            }


            if (Maticsoft.Common.ConfigHelper.GetConfigBool("LocalTest"))
            {
                AccountsPrincipal newUser = AccountsPrincipal.ValidateLogin("admin", "1");
                User currentUser = new Maticsoft.Accounts.Bus.User(newUser);
                Context.User = newUser;
                FormsAuthentication.SetAuthCookie(currentUser.UserName, false);
                Session[Maticsoft.Common.Globals.SESSIONKEY_ADMIN] = currentUser;
                Session["Style"] = currentUser.Style;

                //选择语言
                Session["language"] = "zh-CN";
                HttpCookie mCookie = new HttpCookie("language");
                mCookie.Value = "zh-CN";
                mCookie.Expires = DateTime.MaxValue;
                Response.AppendCookie(mCookie);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "自动登录成功, 正在为您跳转..", "main.htm");
            }
        }

        public void btnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
            //{
            //    int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
            //    if (PassErroeCount > 3)
            //    {
            //        txtUsername.Enabled = false;
            //        txtPass.Enabled = false;
            //        btnLogin.Enabled = false;
            //        this.lblMsg.Text = "对不起，你已经登录错误三次，系统锁定，请联系管理员！";
            //        return;
            //    }
            //}
            if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
            {
                if (Session["CheckCode"].ToString().ToLower() != this.CheckCode.Value.ToLower())
                {
                    this.lblMsg.Text = "验证码错误!";
                    Session["CheckCode"] = null;
                    return;
                }
                else
                {
                    Session["CheckCode"] = null;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            #region

            string userName = Maticsoft.Common.PageValidate.InputText(txtUsername.Text.Trim(), 30);
            string Password = Maticsoft.Common.PageValidate.InputText(txtPass.Text.Trim(), 30);
            //string userName = "admin";
            //string Password = "1";
            AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(userName, Password);
            if (userPrincipal != null)
            {
                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                if (currentUser.UserType != "AA")
                {
                    this.lblMsg.Text = "您非管理员用户，您没有权限登录后台系统！";
                    return;
                }
                Context.User = userPrincipal;
                if (((SiteIdentity)User.Identity).TestPassword(Password) == 0)
                {
                    try
                    {
                        this.lblMsg.Text = "密码错误！";
                        LogHelp.AddUserLog(userName, "", lblMsg.Text, this);
                    }
                    catch
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    if (!currentUser.Activity)
                    {
                        Maticsoft.Common.MessageBox.ShowSuccessTip(this, "对不起，该帐号已被冻结，请联系管理员！");
                        return;
                    }

                    #region 单用户登录模式

                    //单用户登录模式
                    //SingleLogin slogin = new SingleLogin();

                    ////if (slogin.IsLogin(currentUser.UserID))
                    ////{
                    ////    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "对不起，你的帐号已经登录！");
                    ////    return;
                    ////}
                    //slogin.UserLogin(currentUser.UserID);

                    #endregion 单用户登录模式

                    FormsAuthentication.SetAuthCookie(userName, false);

                    Session[Maticsoft.Common.Globals.SESSIONKEY_ADMIN] = currentUser;
                    Session["Style"] = currentUser.Style;

                    //log
                    LogHelp.AddUserLog(currentUser.UserName, currentUser.UserType, "登录成功", this);

                    //选择语言
                    string strLanguage = dropLanguage.SelectedValue;
                    Session["language"] = strLanguage;
                    HttpCookie mCookie = new HttpCookie("language");
                    mCookie.Value = strLanguage;
                    mCookie.Expires = DateTime.MaxValue;
                    Response.AppendCookie(mCookie);

                    if (Session["returnPage"] != null)
                    {
                        string returnpage = Session["returnPage"].ToString();
                        Session["returnPage"] = null;
                        Response.Redirect(returnpage);
                    }
                    else
                    {
                        Response.Redirect("main.htm");
                    }
                }
            }
            else
            {
                this.lblMsg.Text = "登录失败，请确认用户名或密码是否正确。";
                if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
                {
                    int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
                    Session["PassErrorCountAdmin"] = PassErroeCount + 1;
                }
                else
                {
                    Session["PassErrorCountAdmin"] = 1;
                }

                //log
                LogHelp.AddUserLog(userName, "", "登录失败!", this);
            }

            #endregion
        }
    }
}