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
    public partial class AwardList : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Activity.ActivityInfo infoBll = new Maticsoft.WeChat.BLL.Activity.ActivityInfo();
        private Maticsoft.WeChat.BLL.Activity.ActivityAward awardBll = new Maticsoft.WeChat.BLL.Activity.ActivityAward();
        private Maticsoft.WeChat.BLL.Activity.ActivityCode codeBll = new Maticsoft.WeChat.BLL.Activity.ActivityCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        /// <summary>
        /// 奖品类型
        /// </summary>
        protected int AwardType
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    id = Globals.SafeInt(Request.Params["type"], 0);
                }
                return id;
            }
        }

        #region gridView

        public void BindData()
        {
            if(AwardType==1)
            {
                gridView.Columns[3].Visible = false;
            }
            StringBuilder whereStr = new StringBuilder();
            string keyword = this.txtKeyword.Text;
            whereStr.AppendFormat(" ActivityId ={0}", ActivityId);
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                whereStr.AppendFormat(" and GiftName Like '%{0}%'", keyword);
            }
            DataSet ds = awardBll.GetList(whereStr.ToString());
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

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
            int awardId = (int)gridView.DataKeys[e.RowIndex].Value;
            int count = codeBll.GetRecordCount(" AwardId=" + awardId);
            if (count > 0)
            {
                MessageBox.ShowFailTip(this, "该奖品已经参与活动！不能删除");
                gridView.OnBind();
                return;
            }
            if (awardBll.Delete(awardId))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCoupon")
            {
                if (e.CommandArgument != null)
                {
                    //int ruleId = Common.Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    //if (infoBll.DeleteEx(ruleId))
                    //{
                    //    MessageBox.ShowSuccessTip(this, "操作成功！");
                    //    gridView.OnBind();
                    //}
                }
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        /// <summary>
        /// 获取活动名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetActivity(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int activityId = Common.Globals.SafeInt(target, 0);
                Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = infoBll.GetModel(activityId);
                str = infoModel == null ? "" : infoModel.Name;
            }
            return str;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GeStatusName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "不启用";
                        break;
                    case 1:
                        str = "启用";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        #endregion
    }
}