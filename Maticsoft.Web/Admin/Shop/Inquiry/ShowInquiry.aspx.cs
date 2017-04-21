using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Ms;
using Maticsoft.BLL.Shop.Inquiry;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Inquiry
{
    public partial class ShowInquiry : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Inquiry.InquiryInfo infoBll = new InquiryInfo();
        private Maticsoft.BLL.Shop.Inquiry.InquiryItem itemBll = new InquiryItem();
        private  Maticsoft.BLL.Ms.Regions regionBll=new Regions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }


        #region 询盘Id
        /// <summary>
        /// 订单Id
        /// </summary>
        public int InquiryId
        {
            get
            {
                return Maticsoft.Common.Globals.SafeInt(Request.Params["id"], 0);
            }
        }
        #endregion



        private void ShowInfo()
        {

            Maticsoft.Model.Shop.Inquiry.InquiryInfo Infomodel = infoBll.GetModel(InquiryId);
            if (Infomodel != null)
            {
                this.lblTitle.Text = "正在查看询盘的详细信息";
               Maticsoft.Accounts.Bus.User userModel=new User(Infomodel.UpdatedUserId);


                this.lblAddress.Text = regionBll.GetFullNameById4Cache(Infomodel.RegionId) + Infomodel.Address;
                lblCellPhone.Text = Infomodel.CellPhone;
                lblDate.Text = Infomodel.CreatedDate.ToString("yyyy-MM-dd");
                lblEmail.Text = Infomodel.Email;
                lblTelephone.Text = Infomodel.Telephone;
                lblUserName.Text = Infomodel.UserName;
                lblCompany.Text = Infomodel.Company;
                txtAmount.Text = Infomodel.Amount.ToString();
                txtLeaveMsg.Text = Infomodel.LeaveMsg;
                txtReplyMsg.Text = Infomodel.ReplyMsg;
                lblUpdateDate.Text = Infomodel.UpdatedDate.HasValue?Infomodel.UpdatedDate.Value.ToString("yyyy-MM-dd"):"";
                lblUpdateUser.Text = userModel.UserName;
                lblStatus.Text = Infomodel.Status == 1 ? "未处理" : "已处理";
            }
        }

        public void BindData()
        {
            gridView.DataSetSource = itemBll.GetList(" InquiryId=" + InquiryId);
        }



        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Inquiry.InquiryInfo infomodel = infoBll.GetModel(InquiryId);
            if (infomodel == null)
                return;
            if (String.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.ShowFailTip(this, "请输入总价！");
                return;
            }
            infomodel.Amount = Common.Globals.SafeDecimal(this.txtAmount.Text, 0);
            infomodel.Status = 2;
            infomodel.UpdatedDate = DateTime.Now;
            infomodel.UpdatedUserId = CurrentUser.UserID;
            infomodel.ReplyMsg = Common.Globals.HtmlDecode(this.txtReplyMsg.Text);

            if (infoBll.Update(infomodel))
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
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