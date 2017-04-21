using System;
using System.Web.UI;
namespace Maticsoft.Web.FriendlyLink.FLinks
{
    public partial class Modify : PageBaseAdmin
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
            this.imgImgUrl.ImageUrl = model.ImgUrl;
            this.txtLinkUrl.Text = model.LinkUrl;
            this.txtLinkDesc.Text = model.LinkDesc;
            this.dropState.SelectedValue = model.State.ToString();
            this.txtOrderID.Text = model.OrderID.ToString();
            this.txtContactPerson.Text = model.ContactPerson;
            this.txtEmail.Text = model.Email;
            this.txtTelPhone.Text = model.TelPhone;
            this.dropTypeID.SelectedValue = model.TypeID.ToString();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(this.lblID.Text);
            string Name = this.txtName.Text;
            string ImgUrl = this.txtImgUrl.Text;
            string LinkUrl = this.txtLinkUrl.Text;
            string LinkDesc = this.txtLinkDesc.Text;
            int State = int.Parse(this.dropState.SelectedValue);
            int OrderID = int.Parse(this.txtOrderID.Text);
            string ContactPerson = this.txtContactPerson.Text;
            string Email = this.txtEmail.Text;
            string TelPhone = this.txtTelPhone.Text;
            int TypeID = int.Parse(this.dropTypeID.SelectedValue);
            Maticsoft.Model.Settings.FriendlyLink model = new Maticsoft.Model.Settings.FriendlyLink();
            model.ID = ID;
            model.Name = Name;
            model.ImgUrl = ImgUrl;
            model.LinkUrl = LinkUrl;
            model.LinkDesc = LinkDesc;
            model.State = State;
            model.OrderID = OrderID;
            model.ContactPerson = ContactPerson;
            model.Email = Email;
            model.TelPhone = TelPhone;
            model.TypeID = TypeID;

            Maticsoft.BLL.Settings.FriendlyLink bll = new Maticsoft.BLL.Settings.FriendlyLink();
            if (bll.Update(model))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "list.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError, "List.aspx");
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
