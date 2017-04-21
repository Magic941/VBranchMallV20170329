using System;
using System.Drawing;
using System.Web.UI;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class UserUpdate : PageBaseAdmin
    {
        private UserType userTypeManage = new UserType();
        public string userid = "";
        protected override int Act_PageLoad { get { return 197; } } //系统管理_用户管理_编辑页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                User currentUser;
                if ((Request.Params["userid"] != null) && (Request.Params["userid"].ToString() != ""))
                {
                    int userid = int.Parse(Request["userid"]);
                    currentUser = new User(userid);
                    if (currentUser == null)
                    {
                        Response.Write("<script language=javascript>window.alert('"+Resources.Site.TooltipUserExist+"\\');history.back();</script>");
                        return;
                    }

                    dropUserType.DataSource = userTypeManage.GetAllList();
                    dropUserType.DataTextField = "Description";
                    dropUserType.DataValueField = "UserType";
                    dropUserType.DataBind();

                    this.lblName.Text = currentUser.UserName;
                    txtTrueName.Text = currentUser.TrueName;
                    //if (currentUser.Sex != null)
                    //{
                    //    if (currentUser.Sex.Trim().ToUpper() == "1")
                    //        RadioButton1.Checked = true;
                    //    else
                    //        RadioButton2.Checked = true;
                    //}
                    this.txtPhone.Text = currentUser.Phone;
                    txtEmail.Text = currentUser.Email;
                    if (currentUser.EmployeeID > 0)
                    {
                        txtEmployeeID.Text = currentUser.EmployeeID.ToString();
                    }
                    dropUserType.SelectedValue = currentUser.UserType;
                    chkActive.Checked = !currentUser.Activity;
                    
                    //for(int i=0;i<this.Dropdepart.Items.Count;i++)
                    //{
                    //    if(this.Dropdepart.Items[i].Value==currentUser.DepartmentID)
                    //    {
                    //        this.Dropdepart.Items[i].Selected=true;
                    //    }
                    //}
                    //this.dropStyle.SelectedIndex = currentUser.Style - 1;
                    //AccountsPrincipal user = new AccountsPrincipal(userid);
                    //BindRoles(user);
                }
            }
        }

        private void BindRoles(AccountsPrincipal user)
        {
            //if (user.Permissions.Count > 0)
            //{
            //    RoleList.Visible = true;
            //    ArrayList Permissions = user.Permissions;
            //    RoleList.Text = "Permissions:<ul>";
            //    for (int i = 0; i < Permissions.Count; i++)
            //    {
            //        RoleList.Text += "<li>" + Permissions[i] + "</li>";
            //    }
            //    RoleList.Text += "</ul>";
            //}
        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string username = this.lblName.Text.Trim();
            AccountsPrincipal user = new AccountsPrincipal(username);
            User currentUser = new Maticsoft.Accounts.Bus.User(user);

            currentUser.UserName = username;
            currentUser.TrueName = txtTrueName.Text.Trim();
            if (txtPassword.Text.Trim() != "")
            {
                currentUser.Password = AccountsPrincipal.EncryptPassword(txtPassword.Text);
            }
            //if (RadioButton1.Checked)
            //    currentUser.Sex = "1";
            //else
            //    currentUser.Sex = "0";

            currentUser.UserType = dropUserType.SelectedValue;
            currentUser.Phone = this.txtPhone.Text.Trim();
            currentUser.Email = txtEmail.Text.Trim();
            if (txtEmployeeID.Text.Length > 0)
            {
                currentUser.EmployeeID = Convert.ToInt32(txtEmployeeID.Text);
            }
            else
            {
                currentUser.EmployeeID = -1;
            }
            currentUser.Activity = !chkActive.Checked;
    		          
            //int style = int.Parse(this.dropStyle.SelectedValue);
            //currentUser.Style = style;

            if (!currentUser.Update())
            {
                this.lblMsg.ForeColor = Color.Red;
                this.lblMsg.Text = Resources.Site.TooltipUpdateError;
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑用户：【{0}】", username), this);
                Response.Redirect("useradmin.aspx");
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserAdmin.aspx");
        }

    }
}
