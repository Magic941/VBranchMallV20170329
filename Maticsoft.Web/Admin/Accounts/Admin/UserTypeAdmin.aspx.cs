using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class UserTypeAdmin : PageBaseAdmin
    {
        private UserType userTypeManage = new UserType();
        protected override int Act_PageLoad { get { return 49; } } //系统管理_是否显示用户类别管理
        protected new int Act_AddData = 48;    //系统管理_用户类别管理_添加用户类别
        protected new int Act_UpdateData = 50;    //系统管理_用户类别管理_编辑用户类别
        protected new int Act_DelData = 51;    //系统管理_用户类别管理_删除用户类别

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData)!=-1)
                {
                    liAdd.Visible = false;
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

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[2].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[3].Visible = false;
            }
            DataSet ds = new DataSet();

            ds = userTypeManage.GetAllList();
            gridView.DataSetSource = ds;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
            try
            {
                userTypeManage.Delete(ID);
                string keyname = gridView.Rows[e.RowIndex].Cells[0].Text;
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除用户类别：【{0}】", keyname), this);
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

        #endregion gridView

        protected void gridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
            }
        }
    }
}