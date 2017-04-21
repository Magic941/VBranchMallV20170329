using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class UserRoleAssignment : PageBaseAdmin
    {
        List<string> ReservedRoleIDs = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("ReservedRoleIDs"), ',', true);
        protected string DefaultText = Resources.SysManage.lstDefaultUser;
        protected override int Act_PageLoad { get { return 203; } } //系统管理_用户角色分配管理_分配角色设置页

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
                ltbUser.SelectedIndex = 0;

                //识别关联用户
                for (int i = 0; i < ltbUser.Items.Count; i++)
                {
                    if (ltbUser.Items[i].Text == enterprise.UserName)
                    {
                        ltbUser.Items[i].Text += DefaultText;
                    }
                }

                //获取可分配角色
                DataSet dsRole = AccountsTool.GetRoleList();
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
                    userBll.AddAssignRole(selectUserId, roleId);
                }
                else
                {
                    userBll.DeleteAssignRole(selectUserId, roleId);
                }
            }
        }

        public void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Ms/Enterprise/EnterpriseRoleAssignment.aspx");
        }

        protected void ltbUser_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectionRoles();
        }

        private void SelectionRoles()
        {
            //Clear Selection
            cblRole.ClearSelection();

            //Set Selection
            User userBll = new User(this.SelectedUserId);
            DataSet dsRoles = userBll.GetAssignRolesByUser(this.SelectedUserId);
            if (dsRoles == null || dsRoles.Tables[0].Rows.Count < 1) return;
            for (int i = 0; i < dsRoles.Tables[0].Rows.Count; i++)
            {
                foreach (ListItem item in cblRole.Items)
                {
                    if (item.Text == dsRoles.Tables[0].Rows[i]["Description"].ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public string DepartmentId
        {
            get
            {
                return Request.Params["DepartmentId"];
            }
        }

        public string UserType
        {
            get
            {
                return Request.Params["UserType"];
            }
        }

        protected int SelectedUserId
        {
            get { return Globals.SafeInt(ltbUser.SelectedValue, -1); }
        }
    }
}
