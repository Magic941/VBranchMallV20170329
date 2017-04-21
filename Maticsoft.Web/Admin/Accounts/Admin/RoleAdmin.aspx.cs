using System;
using System.Data;
using System.Web.UI;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class RoleAdmin : PageBaseAdmin
    {
        protected int Act_ShowReservedRole = 13; //查看系统保留角色
        protected override int Act_PageLoad { get { return 31; } } //系统管理_是否显示角色管理
        protected new int Act_AddData = 32;    //系统管理_角色管理_添加角色
        private string ReservedRoleIDs = BLL.SysManage.ConfigSystem.GetValueByCache("ReservedRoleIDs").Trim();

        private DataSet roles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    divAdd.Visible = false;
                } 
                dataBind();
            }
        }

        private void dataBind()
        {
            roles = AccountsTool.GetRoleList();
            DataView dv = roles.Tables["Roles"].DefaultView;

            //保留角色
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ShowReservedRole)))
            {
                dv.RowFilter = "RoleID not in (" + ReservedRoleIDs + ")";
            }
            RoleList.DataSource = dv; //roles.Tables["Roles"];
            RoleList.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtRoleName.Text.Trim().Length <= 0)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.SysManage.ErrorRoleIsNull);
                return;
            }
            try
            {
                Role role = new Role();
                if (!role.RoleExists(txtRoleName.Text.Trim()))
                {
                    role.Description = txtRoleName.Text;
                    role.Create();
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加角色：【{0}】", txtRoleName.Text), this);
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDataExist);
                }
            }
            catch (Exception ex)
            {
                lblToolTip.Text = ex.Message;
                return;
            }
            txtRoleName.Text = "";
            dataBind();
        }
    }
}