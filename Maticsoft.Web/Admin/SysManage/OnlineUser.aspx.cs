using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class OnlineUser : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 79; } } //系统管理_是否显示在线用户页面

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView1.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView1.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView1.OnBind();
                    }
                }
            }
        }

        #region gridView

        public void BindData()
        {
            DataTable table = (DataTable)Application["OnlineUsers"];
            gridView1.DataSource = table;
            gridView1.DataBind();

            //for (int i =0; i < gridView1.Rows.Count; i++) {
            //    gridView1.Rows[i].Cells[0].Text =Convert.ToString(i);
            //}
        }

        #endregion gridView

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView1.PageIndex = e.NewPageIndex;
            gridView1.DataBind();
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
    }
}