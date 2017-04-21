using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Text;
using Maticsoft.BLL.SysManage;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.MembershipManage
{
    public partial class UserLog : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 288; } } //用户管理_会员日志信息管理页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
                gridView.OnBind();
                this.txtName.Text = UserName;
                int count = Maticsoft.BLL.SysManage.UserLog.GetCount(" UserName ='" + UserName + "'");
                if (count == 0)
                {
                    MessageBox.ShowFailTip(this," 该用户没有日志","List.aspx");
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            Maticsoft.BLL.SysManage.UserLog.Delete(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }
        public string UserName
        {
            get
            {
                string _user ="";
                if (!string.IsNullOrWhiteSpace(Request.Params["user"]))
                {
                    _user = Request.Params["user"];
                }
                return _user;
            }
        }
        #region gridView

        public void BindData()
        {

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" UserName ='" + UserName + "'");
          
            ds = Maticsoft.BLL.SysManage.UserLog.GetList(strWhere.ToString());

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
            BLL.SysManage.UserLog.Delete(ID);
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
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
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