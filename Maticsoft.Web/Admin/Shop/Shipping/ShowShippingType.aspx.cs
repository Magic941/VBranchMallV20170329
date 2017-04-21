using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Shipping
{
    public partial class ShowShippingType : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 523; } } //Shop_配送方式管理_详细页
        private Maticsoft.BLL.Shop.Shipping.ShippingType typeBll = new BLL.Shop.Shipping.ShippingType();
        Maticsoft.BLL.Shop.Shipping.ShippingPayment payBll = new BLL.Shop.Shipping.ShippingPayment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ckPayType.DataSource = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.ALL);
                this.ckPayType.DataTextField = "Name";
                this.ckPayType.DataValueField = "ModeId";
                this.ckPayType.DataBind();
                BindData();
            }
        }


        #region 编号
        /// <summary>
        /// 可选项Id
        /// </summary>
        public int ModeId
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid))
                {
                    id = Globals.SafeInt(strid, 0);
                }
                return id;
            }
        }
        #endregion

        public void BindData()
        {
            Maticsoft.Model.Shop.Shipping.ShippingType typeModel = typeBll.GetModel(ModeId);
            if (typeModel != null)
            {
                this.lblAddPrice.Text = typeModel.AddPrice.ToString();
                this.lblAddWeight.Text = typeModel.AddWeight.ToString();
                this.lblDesc.Text = typeModel.Description;
                this.lblPrice.Text = typeModel.Price.ToString();
                this.lblWeight.Text = typeModel.Weight.ToString();
                this.lblAddWeight.Text = typeModel.AddWeight.ToString();
                this.lblCompanyName.Text = typeModel.ExpressCompanyName;
                this.lblName.Text = typeModel.Name;
            }
            List<Maticsoft.Model.Shop.Shipping.ShippingPayment> payments =
                payBll.GetModelList(" ShippingModeId=" + ModeId);
   

                for (int i = 0; i < this.ckPayType.Items.Count; i++)
                {
                    if (
                        payments.Select(c => c.PaymentModeId)
                                .Contains(Common.Globals.SafeInt(ckPayType.Items[i].Value, 0)))
                    {
                        ckPayType.Items[i].Selected = true;

                    }
                    ckPayType.Items[i].Enabled = false;
                }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShippingType.aspx");
        }
    }
}
