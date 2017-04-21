using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using System.Data;
namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class editrole : PageBaseAdmin
    {
        private Role currentRole;
        public string RoleID = "";
        protected override int Act_PageLoad { get { return 199; } }  //系统管理_角色管理_编辑页面 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                lblRoleID.Text = Request["RoleID"];
                DoInitialDataBind();
                CategoryDownList_SelectedIndexChanged(sender, e);
            }
            RoleID = lblRoleID.Text;
        }

        //绑定类别列表
        private void DoInitialDataBind()
        {            
            currentRole = new Role(Convert.ToInt32(lblRoleID.Text));
            RoleLabel.Text = currentRole.Description;
            this.TxtNewname.Text = currentRole.Description;

            DataSet allCategories = AccountsTool.GetAllCategories();
            CategoryDownList.DataSource = allCategories.Tables[0];
            CategoryDownList.DataTextField = "Description";
            CategoryDownList.DataValueField = "CategoryID";
            CategoryDownList.DataBind();
        }

        //选择类别，填充2个listbox
        public void CategoryDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int categoryID = Convert.ToInt32(CategoryDownList.SelectedItem.Value);
            FillCategoryList(categoryID);
            SelectCategory(categoryID, false);
        }
        
        //填充非权限列表
        public void FillCategoryList(int categoryId)
        {
            currentRole = new Role(Convert.ToInt32(lblRoleID.Text));
            DataTable categories = currentRole.NoPermissions.Tables["Categories"];
            DataRow currentCategory = categories.Rows.Find(categoryId);
            if (currentCategory != null)
            {
                DataRow[] permissions = currentCategory.GetChildRows("PermissionCategories");
                CategoryList.Items.Clear();
                foreach (DataRow currentRow in permissions)
                {
                    CategoryList.Items.Add(
                        new ListItem((string)currentRow["Description"], Convert.ToString(currentRow["PermissionID"])));
                }
            }
        }


        //填充已有权限listbox
        public void SelectCategory(int categoryId, bool forceSelection)
        {
            currentRole = new Role(Convert.ToInt32(lblRoleID.Text));
            DataTable categories = currentRole.Permissions.Tables["Categories"];
            DataRow currentCategory = categories.Rows.Find(categoryId);
            if (currentCategory != null)
            {
                DataRow[] permissions = currentCategory.GetChildRows("PermissionCategories");

                PermissionList.Items.Clear();
                foreach (DataRow currentRow in permissions)
                {
                    PermissionList.Items.Add(
                        new ListItem((string)currentRow["Description"], Convert.ToString(currentRow["PermissionID"])));
                }
            }
        }

        //增加权限
        public void AddPermissionButton_Click(object sender, System.EventArgs e)
        {
            if (this.CategoryList.SelectedIndex > -1)
            {
                int currentRole = Convert.ToInt32(lblRoleID.Text);
                //RoleID = Request["RoleID"];
                Role bizRole = new Role(currentRole);
                bizRole.AddPermission(Convert.ToInt32(this.CategoryList.SelectedValue));
                CategoryDownList_SelectedIndexChanged(sender, e);
            }

        }

        //移除权限
        public void RemovePermissionButton_Click(object sender, System.EventArgs e)
        {
            if (this.PermissionList.SelectedIndex > -1)
            {
                int currentRole = Convert.ToInt32(lblRoleID.Text);
                //RoleID = Request["RoleID"];
                Role bizRole = new Role(currentRole);
                bizRole.RemovePermission(Convert.ToInt32(this.PermissionList.SelectedValue));
                CategoryDownList_SelectedIndexChanged(sender, e);
            }
        }



        public void RemoveRoleButton_Click(object sender, System.EventArgs e)
        {
            int currentRole = Convert.ToInt32(lblRoleID.Text);
            Role bizRole = new Role(currentRole);
            bizRole.Delete();
            Server.Transfer("RoleAdmin.aspx");
        }

        public void BtnUpName_Click(object sender, EventArgs e)
        {
            string newname = this.TxtNewname.Text.Trim();
            currentRole = new Role(Convert.ToInt32(lblRoleID.Text));
            currentRole.Description = newname;
            currentRole.Update();
            DoInitialDataBind();
            lblTiptool.Text = Resources.Site.TooltipUpdateOK;// "修改成功！";
        }

        public void Button2_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect("RoleAdmin.aspx");
        }



    }
}
