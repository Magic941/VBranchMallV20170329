using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maticsoft.Common;
using System.Drawing;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin
{
    public partial class MLShow : PageBaseAdmin
    {
        Maticsoft.BLL.SysManage.MultiLanguage bllML = new Maticsoft.BLL.SysManage.MultiLanguage();

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
                BindLanguage();

                string f = Request.Params["f"];
                string k = Request.Params["k"];
                if (!string.IsNullOrWhiteSpace(f))
                {
                    lblF.Text = Request.Params["f"];
                    if (!string.IsNullOrWhiteSpace(k))
                    {
                        lblK.Text = k;
                        gridView.OnBind();
                    }                   
                }
                
            }
        }


        public void BindLanguage()
        {
            dropLanguage.DataSource = bllML.GetLanguageListByCache();
            dropLanguage.DataTextField = "Language_cName";
            dropLanguage.DataValueField = "Language_cCode";
            dropLanguage.DataBind();
        }

        public void btnAddMValue_Click(object sender, System.EventArgs e)
        {           
            try
            {
                if (txtMValue.Text.Length > 0)
                {
                    if (bllML.Exists(lblF.Text, Globals.SafeInt(lblK.Text, 0), dropLanguage.SelectedValue))
                    {
                        lblML.Text = Resources.Site.TooltipDataExist;
                        return;
                    }
                    bllML.Add(lblF.Text, Globals.SafeInt(lblK.Text, 0), dropLanguage.SelectedValue, txtMValue.Text);
                    txtMValue.Text = "";
                    gridView.OnBind();
                }
            }
            catch
            {
                lblML.Text = Resources.Site.TooltipSaveError;
            }
        }

        #region gridView

        public void BindData()
        {
            gridView.DataSetSource = bllML.GetLangListByValue(lblF.Text, Globals.SafeInt(lblK.Text,0));
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
                object obj1 = DataBinder.Eval(e.Row.DataItem, "MultiLang_cLang");
                if ((obj1 != null) && ((obj1.ToString() != "")))
                {
                    e.Row.Cells[0].Text = bllML.GetLanguageNameByCache(obj1.ToString());
                }     
            }
        }
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bllML.Delete((int)gridView.DataKeys[e.RowIndex].Value);
            gridView.OnBind();
        }
        
        #endregion


    }
}
