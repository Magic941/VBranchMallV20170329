using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Maticsoft.Json;
using Maticsoft.Services;

namespace Maticsoft.Web.Admin.Members.UserCard
{
    public partial class HaolinCardList : System.Web.UI.Page
    {
        Maticsoft.BLL.Shop_CardUserInfo userCard = new BLL.Shop_CardUserInfo();
        public string baseuri = System.Configuration.ConfigurationManager.AppSettings["CardURL"];
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        #region 列表显示
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        public void BindData()
        {

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtUserName.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" Name like '%" + txtUserName.Text + "%' ");
            }

            if (txtCardNo.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" CardNo like '%" + txtCardNo.Text + "%' ");
            }

            if (txtUserNo.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" UserId like '%" + txtUserNo.Text + "%' ");
            }

            if (txtInsureID.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" NameOneCardId like '%" + txtInsureID.Text.Trim() + "%'  or NameTwoCardId like '%" + txtInsureID.Text.Trim() + "%'");
            }

            if (txtInsureName.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" NameOne like '%" + txtInsureName.Text.Trim() + "%'  or NameTwo like '%" + txtInsureName.Text.Trim() + "%'");
            }

            ds = userCard.GetListByPage(strWhere.ToString(), "Id", (gridView.PageIndex * gridView.PageSize + 1), (gridView.PageIndex * gridView.PageSize + 1) + 99);
            gridView.DataSetSource = ds;
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
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        #endregion
    }
}