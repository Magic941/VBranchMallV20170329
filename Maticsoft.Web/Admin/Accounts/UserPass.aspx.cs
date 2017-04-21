using System;
using System.Drawing;
using System.Web.UI;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Accounts
{
    public partial class UserPass : PageBaseAdmin
    {
        protected new int Act_UpdateData = 190;    //系统管理_修改密码
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    btnSave.Visible = false;
                }
                if (!Context.User.Identity.IsAuthenticated) return;
                this.lblName.Text = this.CurrentUser.UserName;
            }
        }


        public void btnSave_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                SiteIdentity SID = new SiteIdentity(User.Identity.Name);
                if (SID.TestPassword(txtOldPassword.Text) == 0)
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = Resources.Site.ErrorPasswprdError;
                }
                else
                    if (this.txtPassword.Text.Trim() != this.txtPassword1.Text.Trim())
                    {
                        this.lblMsg.ForeColor = Color.Red;
                        this.lblMsg.Text = Resources.Site.ErrorPasswprd;
                    }
                    else
                    {
                        User currentUser = this.CurrentUser;

                        currentUser.Password = AccountsPrincipal.EncryptPassword(txtPassword.Text);

                        if (!currentUser.Update())
                        {
                            Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                            //日志
                            //UserLog.AddLog(currentUser.UserName, currentUser.UserType, Request.UserHostAddress, Request.Url.AbsoluteUri,Resources.Site.TooltipUpdateFailPassword);
                        }
                        else
                        {
                            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                            //日志
                            //UserLog.AddLog(currentUser.UserName, currentUser.UserType, Request.UserHostAddress, Request.Url.AbsoluteUri,Resources.Site.TooltipUpdateSuccessfulPassword);
                        }
                    }
            }
        }
    }
}
