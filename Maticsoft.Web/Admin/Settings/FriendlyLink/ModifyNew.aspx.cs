using System;
using System.Web.UI;
using Maticsoft.Common;
namespace Maticsoft.Web.FriendlyLink.FLinks
{
    public partial class ModifyNew : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 381; } } //设置_友情链接管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    int ID = (Convert.ToInt32(Request.Params["id"]));
                    ShowInfo(ID);
                }
            }
        }

        private void ShowInfo(int ID)
        {
            Maticsoft.BLL.Settings.FriendlyLink bll = new Maticsoft.BLL.Settings.FriendlyLink();
            Maticsoft.Model.Settings.FriendlyLink model = bll.GetModel(ID);
            this.lblID.Text = model.ID.ToString();
            this.txtName.Text = model.Name;
            this.txtImgUrl.Text = model.ImgUrl;
            this.txtImgWidth.Text = model.ImgWidth.ToString();
            this.txtImgHeight.Text = model.ImgHeight.ToString();
            this.imgImgUrl.ImageUrl = model.ImgUrl;
            this.txtLinkUrl.Text = model.LinkUrl;
            this.txtLinkDesc.Text = model.LinkDesc;
            this.radioBtnYes.Checked = model.IsDisplay;
            this.txtOrderID.Text = model.OrderID.ToString();
            this.txtContactPerson.Text = model.ContactPerson;
            this.txtEmail.Text = model.Email;
            this.txtTelPhone.Text = model.TelPhone;
            this.dropTypeID.SelectedValue = model.TypeID.ToString();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            int TypeID = int.Parse(this.dropTypeID.SelectedValue);
            if (TypeID == 1)
            {
                this.txtImgUrl.Text = "";
                this.txtImgWidth.Text = "";
                this.txtImgHeight.Text = "";
            }
            int ID = int.Parse(this.lblID.Text);
            string Name = this.txtName.Text;
            string ImgUrl = this.txtImgUrl.Text;
            int ImgWidth = Globals.SafeInt(this.txtImgWidth.Text, 0);
            int ImgHeight = Globals.SafeInt(this.txtImgHeight.Text, 0);
            string LinkUrl = this.txtLinkUrl.Text;
            string LinkDesc = this.txtLinkDesc.Text;
            bool IsDisplay = this.radioBtnYes.Checked;
            int OrderID = int.Parse(this.txtOrderID.Text);
            string ContactPerson = this.txtContactPerson.Text;
            string Email = this.txtEmail.Text;
            string TelPhone = this.txtTelPhone.Text;
            
            
            Maticsoft.Model.Settings.FriendlyLink model = new Maticsoft.Model.Settings.FriendlyLink();
            model.ID = ID;
            model.Name = Name;
            model.ImgUrl = ImgUrl;
            model.ImgWidth = ImgWidth;
            model.ImgHeight = ImgHeight;
            model.LinkUrl = LinkUrl;
            model.LinkDesc = LinkDesc;
            model.IsDisplay = IsDisplay;
            model.OrderID = OrderID;
            model.ContactPerson = ContactPerson;
            model.Email = Email;
            model.TelPhone = TelPhone;
            model.TypeID = TypeID;

            Maticsoft.BLL.Settings.FriendlyLink bll = new Maticsoft.BLL.Settings.FriendlyLink();
            if (bll.Update(model))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "listNew.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError, "ListNew.aspx");
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("listnew.aspx");
        }
    }
}
