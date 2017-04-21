using System;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Members;
using Maticsoft.Model.Members;

namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class AddUser : PageBaseAdmin
    {
        public string adminname = "Management";
        private UserType userTypeManage = new UserType();
        protected override int Act_PageLoad { get { return 196; } } //系统管理_是否显示用户管理 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                radbtnlistUserType.DataSource = userTypeManage.GetAllList();
                radbtnlistUserType.DataTextField = "Description";
                radbtnlistUserType.DataValueField = "UserType";
                radbtnlistUserType.DataBind();
            }
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            User newUser = new User();
            string strErr = "";
            if (newUser.HasUserByUserName(txtUserName.Text))
            {
                strErr += Resources.Site.TooltipUserExist;
            }
            if (strErr != "")
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, strErr);
                return;
            }
            newUser.UserName = txtUserName.Text;
            newUser.Password = AccountsPrincipal.EncryptPassword(txtPassword.Text);
            newUser.NickName = newUser.UserName;    //昵称和用户名相同 SNS模块使用
            newUser.TrueName = txtTrueName.Text;
            newUser.Sex = "1";
            //if (RadioButton1.Checked)
            //    newUser.Sex = "1";//男
            //else
            //    newUser.Sex = "0";//女
            newUser.Phone = txtPhone.Text.Trim();
            newUser.Email = txtEmail.Text;
            newUser.EmployeeID = 0;
            //newUser.DepartmentID=this.Dropdepart.SelectedValue;
            newUser.Activity = true;
            newUser.UserType = radbtnlistUserType.SelectedValue;
            newUser.Style = 1;
            newUser.User_dateCreate = DateTime.Now;
            newUser.User_iCreator = CurrentUser.UserID;
            newUser.User_dateValid = DateTime.Now;
            newUser.User_cLang = "zh-CN";

            int userid = newUser.Create();
            if (userid == -100)
            {
                //ERROR
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUserExist);
                return;
            }
            UsersExp bllUsersExp = new UsersExp();
            bllUsersExp.AddUsersExp(new UsersExpModel
            {
                UserID = userid,
                LastAccessTime = DateTime.Now,
                LastLoginTime = DateTime.Now,
                LastPostTime = DateTime.Now
            });
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加用户：【{0}】", txtUserName.Text), this);
            Response.Redirect("RoleAssignment.aspx?UserID=" + userid);
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserAdmin.aspx");
        }
    }
}
