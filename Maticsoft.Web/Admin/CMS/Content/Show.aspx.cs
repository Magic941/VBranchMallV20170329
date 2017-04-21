using System;
using System.Web.UI;
using Maticsoft.Common;
namespace Maticsoft.Web.CMS.Content
{
    public partial class Show : PageBaseAdmin
    {
        Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        public string strid = string.Empty;
        public string strClassID = string.Empty;
        public string localVideoUrl = string.Empty;
        protected override int Act_PageLoad { get { return 230; } } //CMS_���ݹ���_��ϸҳ
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ClassID > 0)
                {
                    ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent, "List.aspx");
                }

                if (ClassID > 0)
                {
                    strClassID = "?classid=" + ClassID;
                }
            }
        }

        public int ContentID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        public int ClassID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["classid"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Content model = bll.GetModel(ContentID);

            if (null != model)
            {
                this.lblContentID.Text = model.ContentID.ToString();
                this.lblTitle.Text = Globals.HtmlDecode(model.Title);
                this.lblSubTitle.Text = Globals.HtmlDecode(model.SubTitle);
                this.txtSummary.Text = Globals.HtmlDecode(model.Summary);
                this.lblContent.Text = model.Description;
                this.imgUrl.ImageUrl = model.ImageUrl;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
                this.lblLastEditUserID.Text = model.LastEditUserID.ToString();
                this.lblLastEditDate.Text = model.LastEditDate.ToString();
                this.lblLinkUrl.Text = Globals.HtmlDecode(model.LinkUrl);
                this.lblPvCount.Text = model.PvCount.ToString();
                this.lblState.Text = GetStatusName(model.State);
                this.lblClassID.Text = model.ClassID.ToString();
                this.lblKeywords.Text = model.Keywords;
                this.lblOrders.Text = model.Sequence.ToString();
                this.chkIsCom.Checked = model.IsRecomend;
                this.chkIsHot.Checked = model.IsHot;
                this.chkIsColor.Checked = model.IsColor;
                this.chkIsTop.Checked = model.IsTop;
                if (!string.IsNullOrWhiteSpace(model.Attachment))
                {
                    this.lnkAttachment.NavigateUrl = model.Attachment;
                }
                else
                {
                    this.lnkAttachment.Visible = false;
                }
                this.lblRemary.Text = Globals.HtmlDecode(model.Remary);
            }
        }

        private string GetStatusName(int state)
        {
            string str = "";
            switch (state)
            {
                case 0:
                    str = "�ѷ���";
                    break;
                case 1:
                    str = "�����";
                    break;
                case 2:
                    str = "�ݸ�";
                    break;
                default:
                    str = "�ѷ���";
                    break;
            }
            return str;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdersList.aspx");
        }
    }
}
