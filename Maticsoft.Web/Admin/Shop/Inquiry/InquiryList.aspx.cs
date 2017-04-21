using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Ms;
using Maticsoft.BLL.Shop.Inquiry;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Inquiry
{
    public partial class InquiryList :PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Inquiry.InquiryInfo infoBll = new InquiryInfo();
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

        #region gridView

        public void BindData()
        {

            StringBuilder whereStr = new StringBuilder();
            string keyword = this.txtKeyword.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                whereStr.AppendFormat(" LeaveMsg Like '%{0}%'", keyword);
            }
            DataSet ds = infoBll.GetList(-1, whereStr.ToString(), " CreatedDate desc");
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
            long inquiryId = Common.Globals.SafeLong(gridView.DataKeys[e.RowIndex].Value,0);
            infoBll.DeleteEx(inquiryId);
            gridView.OnBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
    
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetRegionName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int regionId = Common.Globals.SafeInt(target, 0);
                Maticsoft.BLL.Ms.Regions regionBll=new Regions();
                str = regionBll.GetFullNameById4Cache(regionId);
            }
            return str;
        }

        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetUserName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int userId = Common.Globals.SafeInt(target, 0);
                Maticsoft.Accounts.Bus.User userModel = new User(userId);
                str = userModel == null ? "" : userModel.NickName;
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
                    case 1:
                        str = "未处理";
                        break;
                    case 2:
                        str = "已处理";
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