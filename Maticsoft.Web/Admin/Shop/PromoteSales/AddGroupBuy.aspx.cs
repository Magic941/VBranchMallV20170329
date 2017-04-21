using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.PromoteSales;
using ProductImage = Maticsoft.Model.Shop.Products.ProductImage;
using Maticsoft.BLL.SysManage;
using Maticsoft.Model.Settings;
using System.Collections;

using System.Data.SqlTypes;
namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    public partial class AddGroupBuy : PageBaseAdmin
    {


        private string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));

        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryInfoBll = new CategoryInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo productInfoBll = new ProductInfo();
        private Maticsoft.BLL.Shop.PromoteSales.GroupBuy buyBll = new GroupBuy();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ProductImagesThumbSize
                Size thumbSize = Maticsoft.Common.StringPlus.SplitToSize(
                    BLL.SysManage.ConfigSystem.GetValueByCache(SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                    '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
                //hfProductImagesThumbSize.Value = thumbSize.Width + "," + thumbSize.Height;

                //商品分类
                this.ddlCateList.DataSource = categoryInfoBll.GetList("Depth=1");
                ddlCateList.DataTextField = "Name";
                ddlCateList.DataValueField = "CategoryId";
                ddlCateList.DataBind();
                ddlCateList.Items.Insert(0, new ListItem("请选择", "0"));
                this.txtSequence.Text = (buyBll.MaxSequence() + 1).ToString();

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = new Model.Shop.PromoteSales.GroupBuy();
            long productId = Common.Globals.SafeLong(this.ddlProduct.SelectedValue, 0);
            int groupCount=Common.Globals.SafeInt(this.txtGroupCount.Text, 0);
            int maxCount=Common.Globals.SafeInt(this.txtMaxCount.Text, 0);
            int promotionLimitQu = Common.Globals.SafeInt(this.txtPromotionLimitQu.Text, 1);
       
            if (productId == 0)
            {
                Common.MessageBox.ShowFailTip(this, "请选择团购商品！");
                return;
            }
            decimal price = Common.Globals.SafeDecimal(txtPrice.Text, -1);
            if (price == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填写团购价格");
                return;
            }
            //if (String.IsNullOrWhiteSpace(txtStartDate.Text))
            //{
            //    Common.MessageBox.ShowFailTip(this, "请选择活动开始时间");
            //    return;
            //}
            if (String.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                Common.MessageBox.ShowFailTip(this, "请选择活动结束时间");
                return;
            }
            if (maxCount < groupCount)
            {
                Common.MessageBox.ShowFailTip(this, "限购总数量必须大于团购满足数量");
                return;
            }
            if (promotionLimitQu>groupCount)
            {
                Common.MessageBox.ShowFailTip(this, "限单笔购数量必须小于团购满足数量");
                return;
            }
            //判重
            if (buyBll.IsExists(productId))
            {
                Common.MessageBox.ShowFailTip(this, "该商品已加入团购活动");
                return;
            }
            int? selectedRegionId = Common.Globals.SafeInt(ajaxRegion.SelectedValue, 217);
            //if (selectedRegionId == -1)
            //{
            //    Common.MessageBox.ShowFailTip(this, "请选择团购地区");
            //    return;
            //}
            if (ddlCateList.SelectedValue == "0")
            {
                Common.MessageBox.ShowFailTip(this, "请选择商品分类");
                return;
            }
            string categoryId = ddlCateList.SelectedValue;
            string path = ddlCateList2.SelectedValue;
            BLL.Shop.Products.ProductInfo bllProductInfo=new ProductInfo();
            Maticsoft.Model.Shop.Products.ProductInfo productModel = bllProductInfo.GetModelByCache(productId);
            buyModel.Description = this.txtDesc.Text;
            buyModel.EndDate = Common.Globals.SafeDateTime(txtEndDate.Text,DateTime.Now);
            buyModel.Price = Common.Globals.SafeDecimal(price, 0);
            buyModel.ProductId = productId;
            buyModel.GroupBase = Common.Globals.SafeInt(txtGroupBase.Text, 0);
            buyModel.Sequence = Common.Globals.SafeInt(txtSequence.Text, 0);
            buyModel.Status = chkStatus.Checked ? 1 : 0;
            buyModel.PromotionType = chkPromotionType.Checked ? 1 : 0;
            buyModel.FinePrice = Common.Globals.SafeDecimal(this.txtFinePrice.Text, 0);
            buyModel.RegionId = selectedRegionId.Value;
            buyModel.GroupCount = groupCount;
            buyModel.MaxCount = maxCount;
            buyModel.StartDate = Common.Globals.SafeDateTime(txtStartDate.Text, DateTime.MinValue);
            buyModel.CategoryId = Common.Globals.SafeInt(categoryId, 0);
            buyModel.PromotionLimitQu = promotionLimitQu;


            //string splitProductImages = hfProductImages.Value;

            //string[] productImages = new string[0];


            //if (!string.IsNullOrWhiteSpace(splitProductImages))
            //{
            //    productImages = splitProductImages.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //}





            //待上传的图片名称
            string savePath = string.Format("/Upload/Shop/Images/Product/GroupBy/{0}/", DateTime.Now.ToString("yyyyMMdd"));



            ArrayList imageList = new ArrayList();

            if (string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择要上传的图片！");
                return;
            }

            if (!string.IsNullOrWhiteSpace(HiddenField_ISModifyImage.Value))
            {
                string imageUrl = string.Format(hfFileUrl.Value, "");
                imageList.Add(imageUrl.Replace(tempFile, ""));
                buyModel.GroupBuyImage = imageUrl.Replace(tempFile, savePath);
            }
            else
            {
                buyModel.GroupBuyImage = this.hfFileUrl.Value;
            }

            //Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(savePath), imageList);

            if (imageList.Count > 0)
            {
                foreach (var file in imageList)
                {
                    Maticsoft.BLL.SysManage.FileManager.MoveImageForFTP(tempFile + file, savePath);
                }
            }


            if (path == "0")
            {
                buyModel.CategoryPath = categoryId;
            }
            else
            {
                buyModel.CategoryPath = categoryId+"|"+path;
            }
            if (null != productModel)
            {
                buyModel.ProductName = productModel.ProductName;
                buyModel.ProductCategory = ddlCateList.SelectedItem.Text;
                //buyModel.GroupBuyImage = productModel.ThumbnailUrl1;
            }
            if (buyBll.Add(buyModel) > 0)
            {
                Common.MessageBox.ShowSuccessTip(this, "操作成功", "GroupBuyList.aspx");
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

            ddlProduct.DataSource = productInfoBll.GetProductsByCid(categoryId);
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