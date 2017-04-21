using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using System.Web.Security;
using Maticsoft.Common;

namespace Maticsoft.Web.Enterprise
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            #region 校验码
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

            #endregion

            #region  验证用户
            string userName = Maticsoft.Common.PageValidate.InputText(txtUsername.Text.Trim(), 30);
            string Password = Maticsoft.Common.PageValidate.InputText(txtPass.Text.Trim(), 30);
            AccountsPrincipal newUser = AccountsPrincipal.ValidateLogin(userName, Password);
            if (newUser != null)
            {
                User currentUser = new Maticsoft.Accounts.Bus.User(newUser);

                if (currentUser.UserType != "EE")
                {
                    this.lblMsg.Text = "您不是渠道商不能从该入口登录！";
                    return;
                }

                Context.User = newUser;
                if (((SiteIdentity)User.Identity).TestPassword(Password) == 0)
                {
                    try
                    {
                        this.lblMsg.Text = "密码错误！";
                        LogHelp.AddUserLog(userName, "", lblMsg.Text, this);
                    }
                    catch
                    {
                        Response.Redirect("/Member/Login.aspx");
                    }
                }
                else
                {                                         
                    if (!currentUser.Activity)
                    {
                        Maticsoft.Common.MessageBox.Show(this, "对不起，该帐号尚未激活，请联系管理员！");
                        return;
                    }

                    #region 单用户登录模式
                    //单用户登录模式
                    //SingleLogin slogin = new SingleLogin();

                    ////if (slogin.IsLogin(currentUser.UserID))
                    ////{
                    ////    Maticsoft.Common.MessageBox.Show(this, "对不起，你的帐号已经登录！");
                    ////    return;
                    ////}

                    //slogin.UserLogin(currentUser.UserID);
                    #endregion

                    FormsAuthentication.SetAuthCookie(userName, false);
                    Session[Globals.SESSIONKEY_ENTERPRISE] = currentUser;
                    Session["Style"] = currentUser.Style;
                    //log
                    LogHelp.AddUserLog(currentUser.UserName, currentUser.UserType, "登录成功", this);

                    #region 更新最新的登录时间
                    //Maticsoft.BLL.SysManage.UsersExp uBll = new BLL.SysManage.UsersExp();
                    //Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();
                    //uModel = uBll.GetUsersExpModel(currentUser.UserID);
                    //if (uModel != null)
                    //{
                    //    uModel.LastAccessIP = Request.UserHostAddress;
                    //    uModel.LastLoginTime = DateTime.Now;
                    //    uBll.UpdateUsersExp(uModel);
                    //} 
                    #endregion

                    //选择语言
                    //string strLanguage = dropLanguage.SelectedValue;
                    //Session["language"] = strLanguage;
                    //HttpCookie mCookie = new HttpCookie("language");
                    //mCookie.Value = strLanguage;
                    //mCookie.Expires = DateTime.MaxValue;
                    //Response.AppendCookie(mCookie);


                    if (Session["returnPage"] != null)
                    {
                        string returnpage = Session["returnPage"].ToString();
                        Session["returnPage"] = null;
                        Response.Redirect(returnpage);
                    }
                    else
                    {
                        Response.Redirect("index.html");
                    }
                }
            }
            else
            {
                this.lblMsg.Text = "登录失败，请确认用户名或密码是否正确。";
                //log
                LogHelp.AddUserLog(userName, "", "登录失败!", this);
            }

            #endregion

        }
    }
}