using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.SysManage
{
    public partial class ConfigSystem : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 57; } } //系统管理_是否显示参数管理
        protected int Act_AddConfigType = 58;//系统管理_参数管理_添加参数类别
        protected new int Act_DelData = 61;    //系统管理_参数管理_删除参数信息
        protected new int Act_UpdateData = 60;    //系统管理_参数管理_编辑参数信息
        protected new int Act_AddData = 59;    //系统管理_参数管理_添加参数
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //是否有增加参数类别的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddConfigType)) && GetPermidByActID(Act_AddConfigType) != -1)
                {
                    txtTypeName.Visible = false;
                    btnSaveType.Visible = false;
                }
                //是否有增加参数数据的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    TableAdd.Visible = false;
                }
                BindConfigType();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtKeyName.Text.Trim() == "")
            {
                lblToolTip.ForeColor = Color.Red;
                lblToolTip.Text = Resources.SysManage.ErrorKeyNameNotNull;
                return;
            }
            if (txtValue.Text.Trim() == "")
            {
                lblToolTip.ForeColor = Color.Red;
                lblToolTip.Text = Resources.SysManage.ErrorValueNotNull;
                return;
            }
            if (txtDescription.Text.Trim() == "")
            {
                lblToolTip.ForeColor = Color.Red;
                lblToolTip.Text = Resources.SysManage.fieldDescription + Resources.SysManage.ErrorContentNotNull;
                return;
            }

            Maticsoft.BLL.SysManage.ConfigSystem.Add(txtKeyName.Text.Trim(),
                txtValue.Text.Trim(), txtDescription.Text.Trim(),
                Common.Globals.SafeEnum(
                dropConfigType.SelectedValue, Model.SysManage.ApplicationKeyType.System));
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipAddSuccess);

            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加系统参数：【{0}】", txtKeyName.Text.Trim()), this);
            gridView.OnBind();
            lblToolTip.Text = "";
            txtKeyName.Text = "";
            txtValue.Text = "";
            txtDescription.Text = "";


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region  加载参数类别
        private void BindConfigType()
        {
            BLL.SysManage.ConfigType bllCT = new BLL.SysManage.ConfigType();
            DataSet dsct = bllCT.GetList("");

            dropConfigType.DataSource = dsct;
            dropConfigType.DataTextField = "TypeName";
            dropConfigType.DataValueField = "KeyType";
            dropConfigType.DataBind();


            dropConfigTypeSearch.DataSource = dsct;
            dropConfigTypeSearch.DataTextField = "TypeName";
            dropConfigTypeSearch.DataValueField = "KeyType";
            dropConfigTypeSearch.DataBind();
            dropConfigTypeSearch.Items.Insert(0, Resources.Site.PleaseSelect);

        }
        protected void btnSaveType_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTypeName.Text.Trim()))
            {
                BLL.SysManage.ConfigType bllCT = new BLL.SysManage.ConfigType();
                bllCT.Add(txtTypeName.Text.Trim());
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加参数类别：【{0}】", txtTypeName.Text.Trim()), this);
                txtTypeName.Text = "";
                BindConfigType();
            }
        }
        #endregion

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();
            string strWhere = "";
            if (dropConfigTypeSearch.SelectedIndex > 0)
            {
                strWhere = string.Format(" KeyType={0} ", dropConfigTypeSearch.SelectedValue);
            }
            if (txtKeyWord.Text.Trim() != "")
            {
                if (strWhere.Length > 1)
                {
                    strWhere += " and ";
                }
                strWhere += string.Format(" KeyName like '%{0}%' ", this.txtKeyWord.Text.Trim());
            }
            ds = Maticsoft.BLL.SysManage.ConfigSystem.GetList(strWhere);
            gridView.DataSetSource = ds;
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
            }
        }


        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            Maticsoft.BLL.SysManage.ConfigSystem.Delete(ID); 
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除系统参数", this);
            gridView.OnBind();
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);

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
            string keyname = (gridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text;
            string Value = (gridView.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox).Text;
            string Description = (gridView.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text;

            if ((Value.Length == 0))
            {
                MessageBox.Show(this, Resources.Site.TooltipNoNull);
                return;
            }

            Maticsoft.BLL.SysManage.ConfigSystem.Update(int.Parse(id), keyname, Value, Description);
            gridView.EditIndex = -1;
            gridView.OnBind();
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑系统参数：【{0}】", keyname), this);
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);

        }
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
                    if (gridView.Rows[i].Cells[8].Text != "")
                    {
                        idlist += gridView.Rows[i].Cells[8].Text + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }

            return idlist;
        }


        #endregion

        protected void btnRestartApp_Click(object sender, EventArgs e)
        {
            System.Web.HttpRuntime.UnloadAppDomain();
        }
    }
}
