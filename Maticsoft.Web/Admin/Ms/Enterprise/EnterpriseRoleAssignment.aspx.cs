using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;


namespace Maticsoft.Web.Admin.Ms.Enterprise
{
    public partial class EnterpriseRoleAssignment : PageBaseAdmin
    {
        Maticsoft.BLL.Ms.Enterprise bll = new Maticsoft.BLL.Ms.Enterprise();
        protected override int Act_PageLoad { get { return 321; } } //企业管理_用户权限设置_列表页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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

        #region 搜索 删除  审核 冻结
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        #endregion

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
            //    gridView.Columns[6].Visible = true;
            //}
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}
            #endregion
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat("Name like '%{0}%' ", txtKeyword.Text.Trim());
            }
            string value=dropState.SelectedValue;
            if (PageValidate.IsNumber(value) && value != "-1")
            {
                if (strWhere.ToString().Length > 0)
                {
                    strWhere.Append(" and Status=" + dropState.SelectedValue);
                }
                else
                {
                    strWhere.Append(" Status=" + dropState.SelectedValue);
                }
            }
            gridView.DataSource = bll.GetList(strWhere.ToString());
            gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void gridView_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                //e.Row.Cells[0].Text = "<input id='Checkbox2' type='checkbox' onclick='CheckAll()'/><label></label>";
            }
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
        }
        #endregion


        //	<asp:BoundField DataField="" HeaderText="0未审核  1正常  2冻结 " SortExpression="Status" ItemStyle-HorizontalAlign="Center"  /> 
        public string GetStatus(object target)
        {
            string status = Resources.Site.ParameterError;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        status = Resources.Site.Unaudited;
                        break;
                    case "1":
                        status = Resources.Site.Normal;
                        break;
                    case "2":
                        status = Resources.Site.Freeze;
                        break;
                }
            }
            return status;
        }

    }
}
