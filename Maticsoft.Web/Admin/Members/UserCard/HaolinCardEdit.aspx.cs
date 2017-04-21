using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Services;

namespace Maticsoft.Web.Admin.Members.UserCard
{
    public partial class HaolinCardEdit : System.Web.UI.Page
    {
        string cardNo = string.Empty;
        Maticsoft.BLL.Shop_CardUserInfo userCard = new BLL.Shop_CardUserInfo();
        public string baseuri = System.Configuration.ConfigurationManager.AppSettings["CardURL"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                cardNo = Request["ID"].ToString();
                SetDefault(cardNo);
            }
        }

        private void SetDefault(string cardNo)
        {
            Maticsoft.Model.Shop_CardUserInfo card = userCard.GetModelByCard(cardNo);
            this.txtName.Text = card.Name;
            this.txtEmail.Text =card.Email;
            this.txtMobile.Text = card.Moble;
            this.txtCardId.Text = card.CardId;
            this.txtAddress.Text = card.Address;
            this.txtActiveDate.Text = card.ActiveDate.ToString();
            this.txtCardNo.Text = card.CardNo;
            this.txtUsreCode.Text = card.UserName;
            this.txtInsureOneName.Text = card.NameOne;
            this.txtInsureOneID.Text = card.NameOneCardId;
            this.txtInsureOneRelationship.Text = card.RelationshipOne;

            this.txtInsureTwoName.Text = card.NameTwo;
            this.txtInsureTwoID.Text = card.NameTwoCardId;
            this.txtInsureTwoRelationship.Text = card.RelationshipTwo;

            //this.dlSex.SelectedIndex = Convert.ToInt32(card.Sex.Trim()=="男"?"0":"1");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cardNo = Request["ID"].ToString();
            Maticsoft.Model.Shop_CardUserInfo card = userCard.GetModelByCard(cardNo);
            card.Name = this.txtName.Text.Trim();
            card.Email = this.txtEmail.Text.Trim();
            card.Moble = this.txtMobile.Text.Trim();
            card.CardId = this.txtCardId.Text.Trim();
            card.Address = this.txtAddress.Text.Trim();
            card.Sex = Convert.ToInt16(this.dlSex.SelectedValue.Trim());

            card.NameOne = this.txtInsureOneName.Text;
            card.NameOneCardId = this.txtInsureOneID.Text;
            card.RelationshipOne = this.txtInsureOneRelationship.Text;

            card.NameTwo = this.txtInsureTwoName.Text;
            card.NameTwoCardId = this.txtInsureTwoID.Text;
            card.RelationshipTwo = this.txtInsureTwoRelationship.Text;

            if (string.IsNullOrEmpty(card.ActiveDate.ToString()))
            {
                card.ActiveDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
            }
            if (string.IsNullOrEmpty(card.InsureActiveDate.ToString()))
            {
                card.InsureActiveDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
            }
            if (string.IsNullOrEmpty(card.OutDate.ToString()))
            {
                card.OutDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
            }
            if (string.IsNullOrEmpty(card.CREATEDATE.ToString()))
            {
                card.CREATEDATE = Convert.ToDateTime("1900-01-01 00:00:00.000");
            }
            if (string.IsNullOrEmpty(card.MODIFYDATE.ToString()))
            {
                card.MODIFYDATE = Convert.ToDateTime("1900-01-01 00:00:00.000");
            }
            if (string.IsNullOrEmpty(card.InsureDate.ToString()))
            {
                card.InsureDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
            }
            if (string.IsNullOrEmpty(card.ActiveDate.ToString()))
            {
                card.ActiveDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
            }

            var helper = new APIHelper(baseuri);

            string result = helper.UpdateHaolinCard(card);
            if (result != "\"Ok\"")
            {
                return;
            }
            if (!userCard.Update(card))
            {
                return;
            }
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>closefrm();</script>");
        }
    }
}