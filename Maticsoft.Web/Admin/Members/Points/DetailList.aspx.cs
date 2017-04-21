using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.Points
{
    public partial class DetailList : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.PointsDetail detailBll = new Maticsoft.BLL.Members.PointsDetail();
        private Maticsoft.BLL.Members.PointsRule ruleBll = new BLL.Members.PointsRule();
        private Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();

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
            if (!String.IsNullOrWhiteSpace(this.txtFrom.Text) && Common.PageValidate.IsDateTime(this.txtFrom.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" CreatedDate >='" + this.txtFrom.Text + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(this.txtTo.Text) && Common.PageValidate.IsDateTime(this.txtTo.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" CreatedDate <='" + this.txtTo.Text + "' ");
            }
            gridView.DataSetSource = detailBll.GetList(-1, strWhere.ToString(), " CreatedDate desc");
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



        protected string GetRuleName(object target)
        {
            //0:取消关注、1:关注、
            string str = "未知";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int ruleId = Common.Globals.SafeInt(target,0);
                str = ruleBll.GetRuleName(ruleId);
            }
            return str;
        }

        protected string GetUserName(object target)
        {
            //0:取消关注、1:关注、
            string str = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int userId = Common.Globals.SafeInt(target, -1);
                Maticsoft.Model.Members.Users userModel = userBll.GetModelByCache(userId);
                str = userModel == null ? str : userModel.UserName;
            }
            return str;
        }
        
        #endregion

    }
}