using System;
using System.Data;
using Maticsoft.Accounts.Bus;
using Maticsoft.Accounts.Data;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Controls
{
    /// <summary>
    /// 数据权限
    /// </summary>
    public partial class UCDroplistPermission : UserControlBase
    {

        Permissions perm = new Permissions();

        private int userid;
        /// <summary>
        /// 当前登录用户的ID
        /// </summary>
        public int UserID
        {
            set
            {
                userid = value;
            }
        }

        /// <summary>
        /// 当前选中的权限代码
        /// </summary>
        public int PermissionID
        {
            get
            {
                if (HiddenFieldPermID.Value.Length > 0)
                {
                    return Convert.ToInt32(HiddenFieldPermID.Value);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value > 0)
                {
                    HiddenFieldPermID.Value = value.ToString();
                    perm.GetPermissionDetails(value);
                    lblPermName.Text = perm.Description;

                    if (droplistPermCategories.Items.FindByValue(perm.CategoryID.ToString()) != null)
                    {
                        droplistPermCategories.SelectedValue = perm.CategoryID.ToString();
                        FillCategoryList(perm.CategoryID);
                        if (radbtnlistPermission.Items.FindByValue(value.ToString()) != null)
                        {
                            radbtnlistPermission.SelectedValue = value.ToString();
                        }
                    }
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BiudPermTree();
                BindCategorylist();
                if (droplistPermCategories.Items.Count > 0)
                {
                    FillCategoryList(int.Parse(droplistPermCategories.SelectedValue));
                }
            }
            else
            {
                if (droplistPermCategories.SelectedItem != null && droplistPermCategories.SelectedValue.Length > 0)
                {
                    string cateid = droplistPermCategories.SelectedValue;
                    FillCategoryList(int.Parse(cateid));
                }
                if (HiddenFieldPermID.Value.Length > 0)
                {
                    if (HiddenFieldPermID.Value != "undefined")
                    {
                        perm.GetPermissionDetails(int.Parse(HiddenFieldPermID.Value));
                        lblPermName.Text = perm.Description;
                    }
                    if (droplistPermCategories.SelectedValue == perm.CategoryID.ToString())
                    {
                        if (radbtnlistPermission.Items.FindByValue(HiddenFieldPermID.Value) != null)
                            radbtnlistPermission.SelectedValue = HiddenFieldPermID.Value;
                    }
                }
            }
        }
        #region 选择权限
        private void BindCategorylist()
        {
            DataSet allCategories = AccountsTool.GetAllCategories();
            droplistPermCategories.DataSource = allCategories.Tables[0];
            droplistPermCategories.DataTextField = "Description";
            droplistPermCategories.DataValueField = "CategoryID";
            droplistPermCategories.DataBind();
        }
        public void btnLoad_Click(object sender, EventArgs e)
        {
            if (droplistPermCategories.SelectedItem != null && droplistPermCategories.SelectedValue.Length > 0)
            {
                string cateid = droplistPermCategories.SelectedValue;
                FillCategoryList(int.Parse(cateid));
            }
        }
        public void droplistPermCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (droplistPermCategories.SelectedItem != null && droplistPermCategories.SelectedValue.Length > 0)
            {
                string cateid = droplistPermCategories.SelectedValue;
                FillCategoryList(int.Parse(cateid));
            }
        }
        public void FillCategoryList(int categoryId)
        {
            DataView dv = null;
            string idStr = BLL.SysManage.ConfigSystem.GetValueByCache("ReservedRoleIDs", ApplicationKeyType.System);
            if (!string.IsNullOrWhiteSpace(idStr))
            {
                string[] ids = idStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                int id;
                foreach (string item in ids)
                {
                    id = Globals.SafeInt(item, -1);
                    if (id > 0 && UserPrincipal.HasRole(id))
                    {
                        DataSet dsPerm = AccountsTool.GetPermissionsByCategory(categoryId);
                        dv = dsPerm.Tables[0].DefaultView;
                        break;  //如此用户拥有一个保留角色就加载全部权限供菜单分配使用
                    }
                }
            }
            if (dv == null)
            {
                DataSet dsPerm = UserPrincipal.PermissionLists;
                dv = dsPerm.Tables[0].DefaultView;
                dv.RowFilter = "CategoryID=" + categoryId;
            }

            radbtnlistPermission.DataSource = dv;
            radbtnlistPermission.DataValueField = "PermissionID";
            radbtnlistPermission.DataTextField = "Description";
            radbtnlistPermission.DataBind();

        }

        #endregion



    }
}