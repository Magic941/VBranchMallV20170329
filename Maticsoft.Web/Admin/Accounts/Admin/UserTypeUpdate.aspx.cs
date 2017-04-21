using System;
using System.Web.UI;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class UserTypeUpdate : PageBaseAdmin
    {
        private UserType userTypeManage = new UserType();
        protected override int Act_PageLoad { get { return 201; } }//系统管理_用户类别管理_编辑页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string userType = this.UserType;
                if (string.IsNullOrWhiteSpace(userType)) return;

                txtUserType.Text = userType;
                txtUserType.Enabled = false;
                txtDescription.Text = userTypeManage.GetDescription(userType);
            }
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            UserType newUserType = new UserType();
            string userType = txtUserType.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(userType) || string.IsNullOrWhiteSpace(description)) return;

            //string strErr = "";
            //if (newUserType.Exists(userType, description))
            //{
            //    strErr += Resources.Site.TooltipUserTypeExist;
            //}
            //if (strErr != "")
            //{
            //    Maticsoft.Common.MessageBox.ShowSuccessTip(this, strErr);
            //    return;
            //}
            newUserType.Update(userType, description);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑用户类别用户类别：【{0}】", userType), this);
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='UserTypeAdmin.aspx'");
            //Response.Redirect("UserTypeAdmin.aspx");
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserTypeAdmin.aspx");
        }

        public string UserType { get { return Request.Params["UserType"]; } }

    }
}
