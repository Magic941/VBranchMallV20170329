using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.PromoteSales;

namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    public partial class AddCountDown : PageBaseAdmin
    {
        private  Maticsoft.BLL.Shop.Products.CategoryInfo categoryInfoBll=new CategoryInfo();
        private  Maticsoft.BLL.Shop.Products.ProductInfo productInfoBll=new ProductInfo();
        private  Maticsoft.BLL.Shop.PromoteSales.CountDown downBll=new CountDown();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //商品分类
                this.ddlCateList.DataSource = categoryInfoBll.GetList("Depth=1");
                ddlCateList.DataTextField = "Name";
                ddlCateList.DataValueField = "CategoryId";
                ddlCateList.DataBind();
                ddlCateList.Items.Insert(0, new ListItem("请选择", "0"));
                this.txtSequence.Text = (downBll.MaxSequence() + 1).ToString();

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.PromoteSales.CountDown downModel = new Model.Shop.PromoteSales.CountDown();
            long productId = Common.Globals.SafeLong(this.ddlProduct.SelectedValue, 0);
            if (productId == 0)
            {
                Common.MessageBox.ShowFailTip(this, "请选择限时抢购商品！");
                return;
            }
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
            //判重
            if (downBll.IsExists(productId))
            {
                Common.MessageBox.ShowFailTip(this, "该商品已加入限时抢购活动");
                return;
            }
            downModel.Description = this.txtDesc.Text;
            downModel.EndDate = Common.Globals.SafeDateTime(txtEndDate.Text, DateTime.Now);
            downModel.Price = Common.Globals.SafeDecimal(price,0);
            downModel.ProductId = productId;
            downModel.Sequence = Common.Globals.SafeInt(txtSequence.Text, 0);
            downModel.Status = chkStatus.Checked ? 1 : 0;
            if (downBll.Add(downModel)>0)
            {
                Common.MessageBox.ShowSuccessTip(this, "操作成功", "CountDownList.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }
        }

        protected void ddlCateList_Changed(object sender, EventArgs e)
        {
            int categoryId = Common.Globals.SafeInt(ddlCateList.SelectedValue, 0);
            if (categoryId == 0)
            {
                ddlCateList2.Visible = false;
                return;
            }

            //绑定二级分类
            this.ddlCateList2.DataSource = categoryInfoBll.GetList("ParentCategoryId=" + categoryId);
            ddlCateList2.DataTextField = "Name";
            ddlCateList2.DataValueField = "CategoryId";
            ddlCateList2.DataBind();
            ddlCateList2.Items.Insert(0, new ListItem("请选择", "0"));
            ddlCateList2.Visible = true;

            ddlProduct.DataSource= productInfoBll.GetProductsByCid(categoryId);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
        }

        protected void ddlCateList2_Changed(object sender, EventArgs e)
        {
            int categoryId = Common.Globals.SafeInt(ddlCateList2.SelectedValue, 0);
            if (categoryId == 0)
            {
                categoryId = Common.Globals.SafeInt(ddlCateList.SelectedValue, 0);
            }

            ddlProduct.DataSource = productInfoBll.GetProductsByCid(categoryId);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
        }
    }
}