using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Enterprise
{
    public partial class RoleAssignment : PageBaseEnterprise
    {
        List<string> ReservedRoleIDs = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("ReservedRoleIDs"), ',', true);

        public void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Check
                string departmentId = this.DepartmentId;
                string userType = this.UserType;
                if (string.IsNullOrWhiteSpace(departmentId) || string.IsNullOrWhiteSpace(UserType)) return;

                int enterpriseId = Globals.SafeInt(departmentId, -1);
                if (enterpriseId < 1) return;

                //获取企业信息
                BLL.Ms.Enterprise enterpriseBll = new BLL.Ms.Enterprise();
                Model.Ms.Enterprise enterprise = enterpriseBll.GetModel(enterpriseId);
                lblName.Text = enterprise.Name;

                //获取下属用户
                User userBll = new User();
                ltbUser.DataSource = userBll.GetUserList(userType, departmentId, string.Empty);
                ltbUser.DataTextField = "UserName";
                ltbUser.DataValueField = "UserID";
                ltbUser.DataBind();
                //删除自己
                for (int i = 0; i < ltbUser.Items.Count; i++)
                {
                    if (ltbUser.Items[i].Text == this.CurrentUser.UserName)
                    {
                        ltbUser.Items.RemoveAt(i);
                    }
                }
                ltbUser.SelectedIndex = 0;

                //获取可分配角色
                DataSet dsRole = userBll.GetAssignRolesByUser(this.CurrentUser.UserID);
                cblRole.DataSource = dsRole.Tables[0].DefaultView;
                cblRole.DataTextField = "Description";
                cblRole.DataValueField = "RoleID";
                cblRole.DataBind();

                //移除保留角色
                for (int n = 0; n < cblRole.Items.Count; n++)
                {
                    if (ReservedRoleIDs.Contains(cblRole.Items[n].Value))
                    {
                        cblRole.Items.Remove(cblRole.Items[n]);
                    }
                }

                //设置选择用户角色
                SelectionRoles();
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            int selectUserId, roleId;
            selectUserId = this.SelectedUserId;
            User userBll = new User(selectUserId);
            foreach (ListItem item in cblRole.Items)
            {
                roleId = Globals.SafeInt(item.Value, -1);
                if (item.Selected == true)
                {
                    userBll.AddToRole(roleId);
                }
                else
                {
                    userBll.RemoveRole(roleId);
                }
            }
        }

        protected void ltbUser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionRoles();
        }

        private void SelectionRoles()
        {
            //Clear Selection
            cblRole.ClearSelection();

            //选择已有角色
            AccountsPrincipal newUser = new AccountsPrincipal(this.SelectedUserId);
            if (newUser.Roles.Count < 1) return;
            for (int i = 0; i < newUser.Roles.Count; i++)
            {
                foreach (ListItem item in cblRole.Items)
                {
                    if (item.Text == newUser.Roles[i].ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public string DepartmentId
        {
            get { return this.CurrentUser.DepartmentID; }
        }

        public string UserType
        {
            get { return "EE"; }
        }

        protected int SelectedUserId
        {
            get { return Globals.SafeInt(ltbUser.SelectedValue, -1); }
        }
    }
}
