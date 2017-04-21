using System;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.ExpressTemplate
{
    public partial class ExpressTemplates : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return -1; } } //Shop_快递单管理_列表页
        protected new int Act_UpdateData = -1;    //Shop_快递单管理_编辑数据

        BLL.Shop.Sales.ExpressTemplate bll = new BLL.Shop.Sales.ExpressTemplate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
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
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(txtExpressName.Text))
            {
                strWhere.Append(" ExpressName like '%" + txtExpressName.Text + "%'");
            }
            if (dropShippingStatus.SelectedIndex > 0)
            {
                if (strWhere.Length > 1) strWhere.Append(" and ");
                strWhere.Append(" IsUse = " + dropShippingStatus.SelectedValue);
            }
            gridView.DataSetSource = bll.GetList(-1, strWhere.ToString(), "ExpressId");
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

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    LinkButton linkComplete = (LinkButton)e.Row.FindControl("Modify");
                    linkComplete.Visible = false;
                    LinkButton linkCancel = (LinkButton)e.Row.FindControl("lnkCopy");
                    linkCancel.Visible = false;
                    LinkButton linkReturn = (LinkButton)e.Row.FindControl("lnkDel");
                    linkReturn.Visible = false;
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
            if (bll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Use")
            {
                int expressId = Common.Globals.SafeInt(e.CommandArgument, 0);

                Model.Shop.Sales.ExpressTemplate model = bll.GetModel(expressId);
                if (model == null ) return;

                model.IsUse = !model.IsUse;
                if (bll.Update(model))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败，请稍候再试！");
                }
            }
            gridView.OnBind();
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

        #endregion

    }
}