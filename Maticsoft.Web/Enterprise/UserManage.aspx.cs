using System;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
namespace Maticsoft.Web.Enterprise
{
    public partial class UserManage : PageBaseEnterprise
    {
        BLL.Members.Users blluser = new BLL.Members.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentUser != null)
                {
                    if (!string.IsNullOrWhiteSpace(CurrentUser.DepartmentID))
                    {
                        AspNetPager1.RecordCount = blluser.GetRecordCount("DepartmentID = " + CurrentUser.DepartmentID + " AND UserID <> " + CurrentUser.UserID);
                        bindData();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.hfIsEidt.Value))
            {
                //创建新用户
                User newUser = new User();

                if (newUser.HasUserByUserName(txtUserName.Text))//验证注册名存在与否
                {
                    Maticsoft.Common.MessageBox.Show(this, Resources.Site.TooltipUserExist);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    Maticsoft.Common.MessageBox.Show(this, "请输入用户名！");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Trim().Length < 6)
                {
                    Maticsoft.Common.MessageBox.Show(this, "请输入正确的用户密码！");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    if (!PageValidate.IsPhone(txtPhone.Text))
                    {
                        Maticsoft.Common.MessageBox.Show(this, "请输入正确的联系电话！");
                        return;
                    }
                    else
                    {
                        newUser.Phone = txtPhone.Text;
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    if (!PageValidate.IsEmail(txtEmail.Text))
                    {
                        Maticsoft.Common.MessageBox.Show(this, "请输入正确的邮箱地址！");
                        return;
                    }
                    else
                    {
                        newUser.Email = txtEmail.Text;
                    }
                }
                if (rdoActive.Checked)
                {
                    newUser.Activity = true;
                }
                else
                {
                    newUser.Activity = false;
                }
                newUser.UserName = txtUserName.Text;
                newUser.TrueName = txtTrueName.Text;
                newUser.Password = Maticsoft.Accounts.Bus.AccountsPrincipal.EncryptPassword(txtPassword.Text);
                newUser.UserType = "EE";
                newUser.DepartmentID = CurrentUser.DepartmentID;
                newUser.User_dateCreate = DateTime.Now;
                newUser.User_iCreator = CurrentUser.UserID;
                newUser.Style = 1;
                int newUid = newUser.Create();
                if (0 < newUid)
                {
                    BLL.Members.Users userBll = new BLL.Members.Users();
                    //userBll.CreateAccountUser(newUid);
                    Maticsoft.Common.MessageBox.ShowAndRedirect(this, "保存成功！", "UserManage.aspx");
                }
            }
            else
            {
                //编辑用户
                if (hfIsEidt.Value.Equals("edit") && !string.IsNullOrWhiteSpace(hfUid.Value))
                {
                    if (!PageValidate.IsNumber(hfUid.Value))
                    {
                        return;
                    }
                    AccountsPrincipal user = new AccountsPrincipal(int.Parse(hfUid.Value));
                    if (null != user)
                    {
                        User EditUser = new Maticsoft.Accounts.Bus.User(user);
                        if (null != EditUser)
                        {
                            EditUser.TrueName = txtTrueName.Text.Trim();
                            if (txtPassword.Text.Trim() != "")
                            {
                                EditUser.Password = AccountsPrincipal.EncryptPassword(txtPassword.Text);
                            }
                            EditUser.UserType = "EE";
                            EditUser.Activity = true;
                            EditUser.DepartmentID = CurrentUser.DepartmentID;
                            EditUser.User_dateCreate = DateTime.Now;
                            EditUser.User_iCreator = CurrentUser.UserID;

                            if (rdoActive.Checked)
                            {
                                EditUser.Activity = true;
                            }
                            else
                            {
                                EditUser.Activity = false;
                            }
                            if (!EditUser.Update())
                            {
                                this.lblMsg.Text = Resources.Site.TooltipUpdateError;
                            }
                            else
                            {
                                hfIsEidt.Value = string.Empty;
                                Maticsoft.Common.MessageBox.ShowAndRedirect(this, "修改成功！", "UserManage.aspx");
                            }
                        }
                    }
                }
            }
        }


        #region

        void bindData()
        {
            if (Globals.SafeInt(CurrentUser.DepartmentID,-1) <1) return;
            Repeater1.DataSource = blluser.GetListByPage("DepartmentID =" + CurrentUser.DepartmentID + " AND UserID <> " + CurrentUser.UserID, "", AspNetPager1.StartRecordIndex, AspNetPager1.EndRecordIndex);
            Repeater1.DataBind();
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            bindData();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandArgument.Equals("delete"))
            {
                //int id = e.Item.ItemIndex;
                HiddenField hid = (HiddenField)Repeater1.Items[e.Item.ItemIndex].FindControl("HiddenField_ID");
                if (CurrentUser.UserID.ToString() == hid.Value)
                {
                    Maticsoft.Common.MessageBox.Show(this, "您不能删除当前登陆用户！");
                    return;
                }
                blluser.Delete(int.Parse(hid.Value));
                this.txtEmail.Text = string.Empty;
                this.txtPassword.Text = string.Empty;
                this.txtPhone.Text = string.Empty;
                this.txtTrueName.Text = string.Empty;
                this.txtUserName.Text = string.Empty;
                hfIsEidt.Value = string.Empty;
                this.btnCancel.Visible = false;
                bindData();
            }
            if (e.CommandArgument.Equals("edit"))
            {
                rdoActive.Checked = false;
                rdoNoActive.Checked = false;
                hfUid.Value = e.CommandName;
                int uid = Globals.SafeInt(e.CommandName, 0);
                if (0 < uid)
                {
                    User EditUser = new User(uid);
                    if (null != EditUser)
                    {
                        txtUserName.Text = EditUser.UserName;
                        txtUserName.Enabled = false;
                        txtTrueName.Text = EditUser.TrueName;
                        txtPhone.Text = EditUser.Phone;
                        txtEmail.Text = EditUser.Email;
                        hfIsEidt.Value = "edit";
                        btnCancel.Visible = true;
                    }

                    if (EditUser.Activity)
                    {
                        rdoActive.Checked = true;
                    }
                    else
                    {
                        rdoNoActive.Checked = true;
                    }
                }
            }
        }

        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtUserName.Enabled = true;
            txtTrueName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            hfIsEidt.Value = "";
            btnCancel.Visible = false;

            rdoActive.Checked = false;
            rdoNoActive.Checked = true;
        }
    }
}