using System;
using System.Web.UI;
namespace Maticsoft.Web.FriendlyLink.FLinks
{
    public partial class ShowNew : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 382; } } //设置_友情链接管理_详细页
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int ID = (Convert.ToInt32(strid));
                    ShowInfo(ID);
                }
            }
        }

        private void ShowInfo(int ID)
        {
            Maticsoft.BLL.Settings.FriendlyLink bll = new Maticsoft.BLL.Settings.FriendlyLink();
            Maticsoft.Model.Settings.FriendlyLink model=bll.GetModel(ID);
            this.lblID.Text = model.ID.ToString();
            this.lblName.Text = model.Name;
            this.imgImgUrl.ImageUrl = model.ImgUrl;
            this.lblImgWidth.Text = model.ImgWidth.ToString();
            this.lblImgHeight.Text = model.ImgWidth.ToString();
            this.lblLinkUrl.Text = model.LinkUrl;
            this.lblLinkDesc.Text = model.LinkDesc;
            this.lblIsDisplay.Text = null;
            bool IsDisplayValue = model.IsDisplay;
            if(IsDisplayValue)
            {
                lblIsDisplay.Text=Resources.SiteSetting.lblYes;
            }
            else
            {
                lblIsDisplay.Text=Resources.SiteSetting.lblNo;
            }
            this.lblOrderID.Text = model.OrderID.ToString();
            this.lblContactPerson.Text = model.ContactPerson;
            this.lblEmail.Text = model.Email;
            this.lblTelPhone.Text = model.TelPhone;
            this.lblTypeID.Text = null;
            int TypeID = Convert.ToInt32(model.TypeID);
            switch (TypeID)
            {
                case 0:
                    this.lblTypeID.Text = Resources.SiteSetting.lblImgLink;
                    break;
                case 1:
                    this.lblTypeID.Text = Resources.SiteSetting.lblTextLink;
                    break;
                default:
                    this.lblTypeID.Text = Resources.Site.Unknown;
                    break;
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("listnew.aspx");
        }
    }
}
