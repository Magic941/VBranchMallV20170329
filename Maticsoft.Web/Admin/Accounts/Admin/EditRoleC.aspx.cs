using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class EditRoleC : PageBaseAdmin
    {
        private Role currentRole;
        private List<int> rolePermissionlist;
        public string RoleID = "";
        protected override int Act_PageLoad { get { return 199; } }  //系统管理_角色管理_编辑页面 

        protected int Act_ShowReservedPerm = 14; //查看系统保留权限

        protected new int Act_UpdateData = 33;    //系统管理_角色管理_修改角色
        protected new int Act_DelData = 34;    //系统管理_用户管理_删除角色
        protected int Act_DelUserData = 35;    //系统管理_用户管理_从角色中移除用户
        protected int Act_UpdateUserData = 36;    //系统管理_用户管理_保存角色所拥有的权限

        private List<string> ReservedRoleIDs = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("ReservedRoleIDs"), ',', true);
        private string ReservedPermIDs = BLL.SysManage.ConfigSystem.GetValueByCache("ReservedPermIDs");
        private User bllUser = new Maticsoft.Accounts.Bus.User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblRoleID.Text = Request["RoleID"];
                if (!ReservedRoleIDs.Contains(lblRoleID.Text))
                {
                    RemoveRoleButton.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    BtnUpName.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    RemoveRoleButton.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelUserData)) && GetPermidByActID(Act_DelUserData) != -1)
                {
                    btnRemove.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateUserData)) && GetPermidByActID(Act_UpdateUserData) != -1)
                {
                    btnSave.Visible = false;
                }
                
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

                gridView.OnBind();

                DoInitialDataBind();
                CategoryDownList_SelectedIndexChanged(sender, e);
            }
            RoleID = lblRoleID.Text;
        }

        #region 权限设置

        //绑定类别列表
        private void DoInitialDataBind()
        {
            currentRole = new Role(Convert.ToInt32(lblRoleID.Text));
            RoleLabel.Text = currentRole.Description;
            this.TxtNewname.Text = currentRole.Description;

            DataSet allCategories = AccountsTool.GetAllCategories();
            if (!DataSetTools.DataSetIsNull(allCategories))
            {
                CategoryDownList.DataSource = allCategories.Tables[0];
                CategoryDownList.DataTextField = "Description";
                CategoryDownList.DataValueField = "CategoryID";
                CategoryDownList.DataBind();
            }
        }

        private void GetRolePermissionlist()
        {
            DataSet ds = currentRole.Permissions;
            if (!DataSetTools.DataSetIsNull(ds))
            {
                rolePermissionlist = new List<int>();
                DataTable dt = ds.Tables["Permissions"];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        rolePermissionlist.Add(Convert.ToInt32(dr["PermissionID"]));
                    }
                }
            }
        }

        //选择类别，填充2个listbox
        public void CategoryDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            chkAll.Checked = false;
            if (CategoryDownList.SelectedItem != null && CategoryDownList.SelectedValue.Length > 0 && PageValidate.IsNumber(CategoryDownList.SelectedValue))
            {
                FillCategoryList(Convert.ToInt32(CategoryDownList.SelectedItem.Value));
            }
        }

        //填充非权限列表
        public void FillCategoryList(int categoryId)
        {
            currentRole = new Role(Convert.ToInt32(lblRoleID.Text));
            GetRolePermissionlist();
            DataSet ds = AccountsTool.GetPermissionsByCategory(categoryId);
            if (!DataSetTools.DataSetIsNull(ds))
            {
                DataView dv = ds.Tables[0].DefaultView;
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ShowReservedPerm)))
                {
                    dv.RowFilter = "PermissionID not in (" + ReservedPermIDs + ")";
                }
                chkPermissions.DataSource = dv;
                chkPermissions.DataValueField = "PermissionID";
                chkPermissions.DataTextField = "Description";
                chkPermissions.DataBind();
            }
        }

        public void chkPermissions_DataBinding(object sender, EventArgs e)
        {
            foreach (ListItem item in ((CheckBoxList)sender).Items)
            {
                if (rolePermissionlist.Contains(Convert.ToInt32(item.Value)))
                {
                    item.Selected = true;
                }
            }
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            Role bllRole = new Role();
            bllRole.RoleID = Convert.ToInt32(lblRoleID.Text);
            foreach (ListItem item in chkPermissions.Items)
            {
                if (item.Selected)
                {
                    bllRole.AddPermission(Convert.ToInt32(item.Value));
                }
                else
                {
                    bllRole.RemovePermission(Convert.ToInt32(item.Value));
                }
            }

            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "编辑角色权限", this);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
        }

        #region 角色编辑

        public void RemoveRoleButton_Click(object sender, System.EventArgs e)
        {
            if (ReservedRoleIDs.Contains(lblRoleID.Text))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.ErrorCannotDeleteRole);
                return;
            }
            int currentRole = Convert.ToInt32(lblRoleID.Text);
            Role bizRole = new Role(currentRole);
            bizRole.Delete();
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除角色：【{0}】", this.TxtNewname.Text.Trim()), this);
            Server.Transfer("RoleAdmin.aspx");
        }

        public void BtnUpName_Click(object sender, EventArgs e)
        {
            string newname = this.TxtNewname.Text.Trim();
            currentRole = new Role(Convert.ToInt32(lblRoleID.Text));
            currentRole.Description = newname;
            currentRole.Update();
            DoInitialDataBind();
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑角色：【{0}】", newname), this);
            lblTiptool.Text = Resources.Site.TooltipUpdateOK;// "修改成功！";
        }

        public void btnBach_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect("RoleAdmin.aspx");
        }

        #endregion 角色编辑

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                foreach (ListItem item in chkPermissions.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in chkPermissions.Items)
                {
                    item.Selected = false;
                }
            }
        }

        #endregion 权限设置

        #region gridView

        public void BindData()
        {
            gridView.DataSetSource = bllUser.GetUsersByRole(int.Parse(lblRoleID.Text));
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }

                object obj2 = DataBinder.Eval(e.Row.DataItem, "UserID");
                if (obj2 != null)
                {
                    List<string> UserIDlist = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("AdminUserID"), ',', true);
                    if (UserIDlist.Contains(obj2.ToString()))
                    {
                        CheckBox ChkBxItem = (CheckBox)e.Row.FindControl(gridView.CheckBoxID);
                        if (ChkBxItem != null && ChkBxItem.Checked)
                        {
                            ChkBxItem.Visible = false;
                        }
                        //LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                        //if (linkbtnDel != null)
                        //{
                        //    linkbtnDel.Visible = false;
                        //}
                    }
                    //else
                    //{
                    //    LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                    //    if (linkbtnDel != null)
                    //    {
                    //        linkbtnDel.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
                    //    }
                    //}
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
            List<string> UserIDlist = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("AdminUserID"), ',', true);
            if (UserIDlist.Contains(ID))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.ErrorCannotDeleteID);
                return;
            }
            try
            {
                User User2 = new User(int.Parse(ID));
                User2.Delete();
                new BLL.Members.UsersExp().DeleteUsersExp(User2.UserID);
                gridView.OnBind();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547)
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.ErrorCannotDeleteUser);
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    if (gridView.DataKeys[i].Value != null)
                    {
                        int userid = Convert.ToInt32(gridView.DataKeys[i].Value);
                        bllUser.RemoveRole(userid, int.Parse(lblRoleID.Text));
                        User currentUser = new User(userid);
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("从角色中移除用户：【{0}】", currentUser.UserName), this);
                    }
                }
            }
            Common.MessageBox.ShowSuccessTip(this, "移除成功！");
            gridView.OnBind();
        }

        #endregion gridView
    }
}