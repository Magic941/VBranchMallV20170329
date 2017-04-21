using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Accounts
{
    public partial class UserBind : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.UserBind userBindBll=new BLL.Members.UserBind();
        protected override int Act_PageLoad { get { return 207; } } 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                userBindBll.GetWeiBoList(CurrentUser.UserID);
            }
        }


        public void BindData()
        {
            StringBuilder strWhere = new StringBuilder();
            string Status = "";// this.dropStatus.SelectedValue;
            strWhere.AppendFormat(" UserId=-1");
            if (!string.IsNullOrWhiteSpace(Status))
            {
                strWhere.AppendFormat(" and Status={0}", Status);
            }
            gridView.DataSetSource = userBindBll.GetList(-1, strWhere.ToString(), " BindId desc");
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
            if (userBindBll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
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

                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (userBindBll.DeleteList(idlist))
            {
                MessageBox.ShowAndRedirect(this, "操作成功！", "UserBind.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        protected string GetImage(object target)
        {
            string value = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int mediaId = Common.Globals.SafeInt(target.ToString(), 0);
                switch (mediaId)
                {
                    case 13:
                        value = "<img alt='QZone' src='/Admin/images/QQ.png' />";
                        break;
                    case 3:
                        value = "<img alt='Sina' src='/Admin/images/sina.png' />";
                        break;
                    default:
                        value = "未知";
                        break;
                }
            }
            return value;
        }
        
    }
}