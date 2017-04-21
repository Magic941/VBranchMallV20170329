using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Activity
{
    public partial class CodeList : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Activity.ActivityCode codeBll = new Maticsoft.WeChat.BLL.Activity.ActivityCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
              
            }
        }
        /// <summary>
        /// 活动编号
        /// </summary>
        protected int ActivityId
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }
        #region gridView

        public void BindData()
        {
            StringBuilder whereStr = new StringBuilder();
            int status = Common.Globals.SafeInt(ddlStatus.SelectedValue, -1);
            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;
            whereStr.AppendFormat(" ActivityId ={0}", ActivityId);
            //状态
            if (status != -1)
            {
                whereStr.AppendFormat(" and Status ={0}", status);
            }
         
            if (!String.IsNullOrWhiteSpace(endDate))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" EndDate <='{0}'", endDate);
            }
            //结束时间
            if (!String.IsNullOrWhiteSpace(startDate))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" StartDate >='{0}'", startDate);
            }
            string keyword = this.txtKeyword.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                if (!String.IsNullOrWhiteSpace(whereStr.ToString()))
                {
                    whereStr.Append(" and ");
                }
                whereStr.AppendFormat(" CodeName = '{0}'", keyword);
            }
            DataSet ds = codeBll.GetList(whereStr.ToString());
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
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

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatusName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "未领取";
                        break;
                    case 1:
                        str = "已领取";
                        break;
                    case 2:
                        str = "已兑换";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (codeBll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();

        }


        protected void ddlAction_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            int status = Common.Globals.SafeInt(ddlAction.SelectedValue, -1);
            if (status == -1)
                return;
            if (codeBll.UpdateStatusList(idlist, status))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败");
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
                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
                        idlist += "'" + gridView.DataKeys[i].Value.ToString() + "',";
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