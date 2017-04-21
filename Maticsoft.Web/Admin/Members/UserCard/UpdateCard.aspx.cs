using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Members.UserCard
{
    public partial class UpdateCard : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.UserCard cardBll = new BLL.Members.UserCard();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Maticsoft.Model.Members.UserCard model = cardBll.GetModel(CardCode);
                this.lblCardCode.Text = model.CardCode;
                this.lblCreatedDate.Text = model.CreatedDate.ToString("yyyy-MM-dd");
                Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User(model.UserId);
                this.lblUserName.Text = user == null ? "" : user.UserName;
                this.txtValue.Text = model.CardValue.ToString();
                this.rblStatus.SelectedValue = model.Status.ToString();
                this.rblType.SelectedValue = model.Type.ToString();
            }
        }

        public string CardCode
        {
            get
            {
                string  _cardCode = "";
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    _cardCode = Request.Params["id"] ;
                }
                return _cardCode;
            }
        }

      

        public void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            Maticsoft.Model.Members.UserCard model = cardBll.GetModel(CardCode);
            model.Type = Common.Globals.SafeInt(rblType.SelectedValue, 0);
            model.Status = Common.Globals.SafeInt(rblStatus.SelectedValue, 0);
            model.CardValue = Common.Globals.SafeDecimal(txtValue.Text, 0);
            if (cardBll.Update(model))
            {
                Common.MessageBox.ShowSuccessTip(this, "编辑成功", "CardList.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败，请稍候再试");
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("CardList.aspx");
        }
    }
}