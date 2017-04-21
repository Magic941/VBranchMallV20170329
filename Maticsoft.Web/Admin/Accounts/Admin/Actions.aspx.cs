using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class Actions : PageBaseAdmin
    {
        Maticsoft.Accounts.Bus.Actions bll = new Maticsoft.Accounts.Bus.Actions();
        Maticsoft.Accounts.Bus.Permissions bllperm = new Permissions();
        protected override int Act_PageLoad { get { return 195; } } //系统管理_功能行为管理_列表页面    
        protected new int Act_AddData = 44;    //用户管理_添加数据 
        protected new int Act_UpdateData = 45;    //系统管理_用户管理_编辑用户
        protected new int Act_DelData = 46;    //系统管理_用户管理_删除用户
        protected int Act_SetPerData = 47;    //系统管理_功能行为管理_设置功能行为权限

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //是否有增加功能行为的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    divAdd.Visible = false;
                }
                //是否有设置权限的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_SetPerData)) && GetPermidByActID(Act_SetPerData) != -1)
                {
                    DropListCategory2.Visible = false;
                    DropListPermissions2.Visible = false;
                }
                DataTable tabcategory = Maticsoft.Accounts.Bus.AccountsTool.GetAllCategories().Tables[0];
                DropListCategory.DataSource = tabcategory;
                DropListCategory.DataValueField = "CategoryID";
                DropListCategory.DataTextField = "Description";
                DropListCategory.DataBind();
                DropListCategory.Items.Insert(0, Resources.Site.PleaseSelect);

                DropListCategory2.DataSource = tabcategory;
                DropListCategory2.DataValueField = "CategoryID";
                DropListCategory2.DataTextField = "Description";
                DropListCategory2.DataBind();
                DropListCategory2.Items.Insert(0, Resources.Site.PleaseSelect);
                DropListPermissions.Visible = false;
                DropListPermissions2.Visible = false;

            }
        }

        public void DropListCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (DropListCategory.SelectedIndex > 0)
            {
                int categoryID = Convert.ToInt32(DropListCategory.SelectedItem.Value);
                DataTable tabperms = Maticsoft.Accounts.Bus.AccountsTool.GetPermissionsByCategory(categoryID).Tables[0];
                DropListPermissions.DataSource = tabperms;
                DropListPermissions.DataValueField = "PermissionID";
                DropListPermissions.DataTextField = "Description";
                DropListPermissions.DataBind();
                DropListPermissions.Items.Insert(0, Resources.Site.PleaseSelect);

                DropListPermissions.Visible = true;
            }
            else
            {
                DropListPermissions.Items.Clear();
            }
        }

        public void DropListCategory2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (DropListCategory2.SelectedIndex > 0)
            {
                int categoryID = Convert.ToInt32(DropListCategory2.SelectedItem.Value);
                DataTable tabperms = Maticsoft.Accounts.Bus.AccountsTool.GetPermissionsByCategory(categoryID).Tables[0];
                DropListPermissions2.DataSource = tabperms;
                DropListPermissions2.DataValueField = "PermissionID";
                DropListPermissions2.DataTextField = "Description";
                DropListPermissions2.DataBind();
                DropListPermissions2.Items.Insert(0, Resources.Site.PleaseSelect);
                DropListPermissions2.Visible = true;
            }
            else
            {
                DropListPermissions.Items.Clear();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text.Trim().Length > 0)
            {
                if (bll.Exists(txtDescription.Text.Trim()))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDataExist);
                    return;
                }
                else
                {
                    if (DropListPermissions.SelectedIndex > 0)
                    {
                        bll.Add(txtDescription.Text.Trim(), Convert.ToInt32(DropListPermissions.SelectedValue));
                    }
                    else
                    {
                        bll.Add(txtDescription.Text.Trim());
                    }

                    gridView.OnBind();
                }
            }
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加功能行为：【{0}】", txtDescription.Text.Trim()), this);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            #region
            //if (!Context.User.Identity.IsAuthenticated)
            //{
            //    return;
            //}
            //AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);
            //if (user.HasPermissionID(PermId_Modify))
            //{
            //    gridView.Columns[5].Visible = true;
            //}
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[6].Visible = true;
            //}
            #endregion

            string strWhere = "";
            if (txtKeywords.Text.Trim() != "")
            {
                strWhere = "Description like '%" + txtKeywords.Text.Trim() + "%'";
            }
            gridView.DataSetSource = bll.GetList(strWhere);
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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton3");
                if (linkbtnDel != null)
                {
                    if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                    {
                        linkbtnDel.Visible = false;
                    }
                }
                LinkButton linkEdit = (LinkButton)e.Row.FindControl("LinkButton4");
                if (linkEdit != null)
                {
                    if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                    {
                        linkEdit.Visible = false;
                    }
                }
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
                    int PermissionID = Convert.ToInt32(obj1);
                    //Maticsoft.Accounts.Bus.User user = new User(userid);
                    e.Row.Cells[3].Text = "(" + PermissionID + ")" + bllperm.GetPermissionName(PermissionID);
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
            bll.Update(int.Parse(id), Description);

            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑功能行为：【{0}】", Description), this);
            gridView.EditIndex = -1;
            gridView.OnBind();
        }


        //public void gridView_RowDeleting(object sender, EventArgs e)
        //{
        //    int ID = (int)gridView.DataKeys[0].Value;
        //}

        //protected override void ExtractRowValues(System.Collections.Specialized.IOrderedDictionary fieldValues, GridViewRow row, bool includeReadOnlyFields, bool includePrimaryKey)
        //{
        //    TableCell expCell = row.Cells[0];
        //    row.Cells.Remove(expCell);
        //    base.ExtractRowValues(fieldValues, row, includeReadOnlyFields, includePrimaryKey);
        //}

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bll.Delete((int)gridView.DataKeys[e.RowIndex].Value);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除功能行为", this);
            gridView.OnBind();
        }



        #endregion

        #region  权限设置
        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        protected void DropListPermissions2_Changed(object sender, EventArgs e)
        {
            if (DropListCategory2.SelectedIndex > 0)
            {
                if (DropListPermissions2.SelectedIndex <= 0)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltripPer);
                    return;
                }
                string idlist = GetSelIDlist();
                if (idlist.Length > 0)
                {
                    if ((DropListPermissions2.SelectedItem != null) && (DropListPermissions2.SelectedValue.Length > 0))
                    {
                        bll.AddPermission(idlist, Convert.ToInt32(DropListPermissions2.SelectedValue));
                    }
                    gridView.OnBind();
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltripPerNum);
                }
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType,"批量设置功能行为的权限", this);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltripPer);
            }
        }

        #endregion

    }
}
