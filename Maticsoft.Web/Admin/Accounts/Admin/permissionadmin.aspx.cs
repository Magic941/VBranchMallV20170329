using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class permissionadmin : PageBaseAdmin
    {
        protected int Act_ShowReservedPerm = 14; //查看系统保留权限
        protected new int Act_DeleteList = 37;    //系统管理_角色管理_添加角色
        protected override int Act_PageLoad { get { return 38; } } //系统管理_是否显示权限管理
        protected int Act_AddPerType = 39;//系统管理_权限管理_增加权限类别
        protected new int Act_DelData = 40;    //系统管理_权限管理_删除权限
        protected int Act_AddPerData = 41;//系统管理_权限管理_增加权限数据
        protected new int Act_UpdateData = 42;    //系统管理_权限管理_编辑权限数据
        protected int Act_RemoveData = 43;    //系统管理_权限管理_移动权限数据
        private List<string> ReservedPermIDs = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("ReservedPermIDs"), ',', true);
        private Permissions bllPerm = new Permissions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //是否具有删除权限的权限。
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                //是否有增加权限类别的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddPerType)) && GetPermidByActID(Act_AddPerType) != -1)
                {
                    btnSaveCategories.Visible = false;
                }
                //是否有删除权限类别的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDeleteClass.Visible = false;
                }
                //是否有增加权限数据的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddPerData)) && GetPermidByActID(Act_AddPerData) != -1)
                {
                    btnSavePermissions.Visible = false;
                }
                //是否有移动权限数据的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_RemoveData)) && GetPermidByActID(Act_RemoveData) != -1)
                {
                    droplistCategories.Visible = false;
                }

                CategoriesDatabind();
                if (ClassList.SelectedItem != null)
                {
                    gridView.OnBind();
                }
            }
        }

        public void CategoriesDatabind()
        {
            DataSet CategoriesList = AccountsTool.GetAllCategories();
            ClassList.DataSource = CategoriesList;
            ClassList.DataTextField = "Description";
            ClassList.DataValueField = "CategoryID";
            ClassList.DataBind();

            droplistCategories.DataSource = CategoriesList;
            droplistCategories.DataTextField = "Description";
            droplistCategories.DataValueField = "CategoryID";
            droplistCategories.DataBind();
            droplistCategories.Items.Insert(0,new ListItem("移动到...",""));
        }

        public void ClassList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            gridView.OnBind();
        }

        /// <summary>
        /// 保存权限类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSaveCategories_Click(object sender, EventArgs e)
        {
            string Category = this.CategoriesName.Text.Trim();
            if (Category != "")
            {
                PermissionCategories bllcate = new PermissionCategories();
                bllcate.Create(Category);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加权限类别：【{0}】", this.CategoriesName.Text.Trim()), this);
                CategoriesDatabind();
                if (this.ClassList.SelectedItem != null)
                {
                    gridView.OnBind();
                }
                this.CategoriesName.Text = "";
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipNoNull);
                return;
            }
        }

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnDeleteClass_Click(object sender, EventArgs e)
        {
            if (ClassList.SelectedItem != null && ClassList.SelectedValue.Length > 0)
            {
                int CategoryId = int.Parse(ClassList.SelectedValue);
                PermissionCategories bllcate = new PermissionCategories();
                if (!bllcate.ExistsPerm(CategoryId))
                {
                    bllcate.Delete(CategoryId);
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除权限类别：【{0}】", this.ClassList.SelectedItem.Text), this);
                    CategoriesDatabind();
                    if (ClassList.SelectedItem != null)
                    {
                        gridView.OnBind();
                    }
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipPermCateNoDelete);
                    return;
                }
            }
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSavePermissions_Click(object sender, EventArgs e)
        {
            string Permissions = this.PermissionsName.Text.Trim();
            if (Permissions != "")
            {
                int CategoryId = int.Parse(ClassList.SelectedValue);
                bllPerm.Create(CategoryId, Permissions);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加权限数据：【{0}】", this.PermissionsName.Text.Trim()), this);
                if (this.ClassList.SelectedItem != null)
                {
                    gridView.OnBind();
                }
                this.PermissionsName.Text = "";
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipNoNull);
                return;
            }
        }

        #region gridView

        public void BindData()
        {
            //是否有编辑权限数据的权限
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[2].Visible = false;
            }
            if (ClassList.SelectedItem != null && ClassList.SelectedValue.Length > 0)
            {
                int CategoryId = int.Parse(ClassList.SelectedValue);
                DataSet ds = AccountsTool.GetPermissionsByCategory(CategoryId);
                gridView.DataSetSource = ds;
            }
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
                object obj1 = DataBinder.Eval(e.Row.DataItem, "PermissionID");
                if ((obj1 != null) && ((obj1.ToString() != "")))
                {
                    //保留权限
                    if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ShowReservedPerm)))
                    {
                        if (ReservedPermIDs.Contains(obj1.ToString()))
                        {
                            e.Row.Visible = false;
                        }
                    }
                }
            }
        }

        public void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridView.EditIndex = e.NewEditIndex;
            gridView.OnBind();
        }

        public void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridView.EditIndex = -1;
            gridView.OnBind();
        }

        public void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gridView.DataKeys[e.RowIndex].Values[0].ToString();
            string Description = ((TextBox)gridView.Rows[e.RowIndex].FindControl("TBDescription")).Text;
            if (Description == "")
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipNoNull);
                return;
            }
            bllPerm.Update(int.Parse(id), Description);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑权限数据：【{0}】", Description), this);
            gridView.EditIndex = -1;
            gridView.OnBind();
        }

        private List<int> GetSelIDlist()
        {
            List<int> idlist = new List<int>();
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.DataKeys[i].Value.ToString() + ",";
                        idlist.Add(Convert.ToInt32(gridView.DataKeys[i].Value));
                    }
                }
            }
            return idlist;
        }

        /// <summary>
        /// 批量删除权限数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<int> idlist = GetSelIDlist();
            if (idlist.Count == 0)
                return;
            string tipmsg = Resources.Site.TooltipDelOK;
            foreach (int id in idlist)
            {
                try
                {
                    bllPerm.Delete(id);
                }
                catch
                {
                    tipmsg = id.ToString() + "," + tipmsg;
                }
            }
            if (tipmsg != Resources.Site.TooltipDelOK)
            {
                tipmsg += Resources.Site.TooltipUpdateError;
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, tipmsg);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除权限数据", this);
            gridView.OnBind();
        }

        /// <summary>
        /// 批量移动权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void droplistCategories_Changed(object sender, EventArgs e)
        {
            List<int> idlist = GetSelIDlist();
            if (idlist.Count == 0)
                return;
            string stridlist = Maticsoft.Common.StringPlus.GetArrayStr(idlist);

            if (!String.IsNullOrWhiteSpace(droplistCategories.SelectedValue))
            {
                int CategoriesID = Common.Globals.SafeInt(droplistCategories.SelectedValue, 0);
                bllPerm.UpdateCategory(stridlist, CategoriesID);

                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量移动权限数据", this);
                gridView.OnBind();
            }
        }

        #endregion gridView
    }
}