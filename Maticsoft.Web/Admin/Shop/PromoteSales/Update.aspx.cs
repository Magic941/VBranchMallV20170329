using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.PromoteSales;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    public partial class Update : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo productInfoBll = new ProductInfo();
        private Maticsoft.BLL.Shop.PromoteSales.CountDown downBll = new CountDown();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        public int DownId
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

        private void ShowInfo()
        {
           Maticsoft.Model.Shop.PromoteSales.CountDown  downModel=downBll.GetModel(DownId);
            if (downModel != null)
            {
                this.txtDesc.Text = downModel.Description;
                txtPrice.Text = downModel.Price.ToString("F");
                txtEndDate.Text = downModel.EndDate.ToString("yyyy-MM-dd");
                lblProductName.Text = productInfoBll.GetProductName(downModel.ProductId);
                chkStatus.Checked = downModel.Status == 1;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.PromoteSales.CountDown downModel = downBll.GetModel(DownId);
          
            decimal price = Common.Globals.SafeDecimal(txtPrice.Text, -1);
            if (price == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填写商品价格");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                Common.MessageBox.ShowFailTip(this, "请选择活动结束时间");
                return;
            }
            downModel.Description = this.txtDesc.Text;
            downModel.EndDate = Common.Globals.SafeDateTime(txtEndDate.Text, DateTime.Now);
            downModel.Price = Common.Globals.SafeDecimal(price, 0);
            downModel.Status = chkStatus.Checked ? 1 : 0;
            if (downBll.Update(downModel))
            {
                Common.MessageBox.ShowSuccessTipScript(this, "操作成功", "window.parent.location.reload();");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }
        } 

    }
}