using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class MultiLanguage : PageBaseAdmin
    {
        private Maticsoft.BLL.SysManage.MultiLanguage bll = new Maticsoft.BLL.SysManage.MultiLanguage();

        protected override int Act_PageLoad { get { return 63; } } //系统管理_是否显示多语言管理菜单

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindLanguage();
            }
        }

        public void BindLanguage()
        {
            DataSet ds = bll.GetLanguageList();
            dropLanguage.DataSource = ds;
            dropLanguage.DataTextField = "Language_cName";
            dropLanguage.DataValueField = "Language_cCode";
            dropLanguage.DataBind();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            lbltip1.Text = "";
            if ((txtMultiLang_cField.Text.Length > 0) && (txtMultiLang_cValue.Text.Length > 0) && txtMultiLang_iPKValue.Text.Length > 0)
            {
                string MultiLang_cField = txtMultiLang_cField.Text.Trim();
                int MultiLang_iPKValue = Convert.ToInt32(txtMultiLang_iPKValue.Text.Trim());
                string MultiLang_cValue = txtMultiLang_cValue.Text.Trim();
                string lang = dropLanguage.SelectedValue;
                if (!bll.Exists(MultiLang_cField, MultiLang_iPKValue, lang))
                {
                    bll.Add(MultiLang_cField, MultiLang_iPKValue, lang, MultiLang_cValue);
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("添加多语言数据：【{0}】",  txtMultiLang_cField.Text.Trim()), this);
                    txtMultiLang_cField.Text = "";
                    txtMultiLang_iPKValue.Text = "";
                    txtMultiLang_cValue.Text = "";
                    gridView.OnBind();
                }
                else
                {
                    this.lbltip1.Text = Resources.Site.TooltipDataExist;
                }
            }
            else
            {
                this.lbltip1.Text = Resources.Site.TooltipNoNull;
            }
        }

        public void btnSearch_Click(object sender, EventArgs e)
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

            #endregion gridView

            string strWhere = "";
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere = "MultiLang_cField like '%" + txtKeyword.Text.Trim() +
                    "%' or MultiLang_cValue like '%" + txtKeyword.Text.Trim() + "%'";
            }
            DataSet ds = bll.GetList(strWhere);
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
                //object obj1 = DataBinder.Eval(e.Row.DataItem, "userid");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    int userid = Convert.ToInt32(obj1);
                //    Maticsoft.Accounts.Bus.User user = new User(userid);
                //    e.Row.Cells[3].Text = user.TrueName;
                //}
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

        public void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (gridView.DataKeys[e.RowIndex].Value != null)
            {
                int id = Convert.ToInt32(gridView.DataKeys[e.RowIndex].Value);
                bll.Delete(id);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除多语言数据", this);
                gridView.OnBind();
            }
        }

        public void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gridView.DataKeys[e.RowIndex].Values[0].ToString();
            string Description = ((TextBox)gridView.Rows[e.RowIndex].FindControl("TBDescription")).Text;
            if (Description == "")
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipNoNull);
                return;
            }

            bll.Update(int.Parse(id), Description);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "编辑多语言数据", this);
            gridView.EditIndex = -1;
            gridView.OnBind();
        }

        #endregion
    }
}