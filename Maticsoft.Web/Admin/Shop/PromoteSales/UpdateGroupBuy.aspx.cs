using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.PromoteSales;
using Maticsoft.Common;
using System.Collections;

namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    public partial class UpdateGroupBuy : System.Web.UI.Page
    {

        private string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));

        private Maticsoft.BLL.Shop.Products.ProductInfo productInfoBll = new ProductInfo();
        private Maticsoft.BLL.Shop.PromoteSales.GroupBuy buyBll = new GroupBuy();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        public int BuyId
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
            Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = buyBll.GetModel(BuyId);
            if (buyModel != null)
            {
                this.txtDesc.Text = buyModel.Description;
                txtPrice.Text = buyModel.Price.ToString("F");
                txtEndDate.Text = buyModel.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                lblProductName.Text = productInfoBll.GetProductName(buyModel.ProductId);
                chkStatus.Checked = buyModel.Status == 1;
                chkPromotionType.Checked = buyModel.PromotionType == 1;
                txtStartDate.Text = buyModel.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                txtFinePrice.Text = buyModel.FinePrice.ToString("F");
                txtGroupCount.Text = buyModel.GroupCount.ToString();
                txtMaxCount.Text = buyModel.MaxCount.ToString();
                txtSequence.Text = buyModel.Sequence.ToString();
                txtGroupBase.Text = buyModel.GroupBase.ToString();
                ajaxRegion.Area_iID = buyModel.RegionId;
                ajaxRegion.SelectedValue = buyModel.RegionId.ToString();
                txtPromotionLimitQu.Text = buyModel.PromotionLimitQu.ToString();
                txtLeastbuyNum.Text = buyModel.LeastbuyNum.ToString() != null ? buyModel.LeastbuyNum.ToString():"1";
                    //this.ajaxRegion.Area_iID = info.RegionId.Value;
                    //this.ajaxRegion.SelectedValue = info.RegionId.Value.ToString();
               
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.PromoteSales.GroupBuy buyModel = buyBll.GetModel(BuyId);
            decimal price = Common.Globals.SafeDecimal(txtPrice.Text, -1);
            int groupCount = Common.Globals.SafeInt(this.txtGroupCount.Text, 0);
            int maxCount = Common.Globals.SafeInt(this.txtMaxCount.Text, 0);
            int promotionLimitQu = Common.Globals.SafeInt(this.txtPromotionLimitQu.Text, 1);
            int leastbuynum = Common.Globals.SafeInt(this.txtLeastbuyNum.Text,1);
            if (price == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填写团购价格");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtStartDate.Text))
            {
                Common.MessageBox.ShowFailTip(this, "请选择活动开始时间");
                return;
            }
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
            int? selectedRegionId = Common.Globals.SafeInt(ajaxRegion.SelectedValue, -1);
            if (selectedRegionId == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请选择团购地区");
                return;
            }
            Maticsoft.Model.Shop.Products.ProductInfo pro = productInfoBll.GetModelByCache(buyModel.ProductId);
            buyModel.Description = this.txtDesc.Text;
            buyModel.EndDate = Common.Globals.SafeDateTime(txtEndDate.Text, DateTime.Now);
            buyModel.Price = Common.Globals.SafeDecimal(price, 0);
            buyModel.Status = chkStatus.Checked ? 1 : 0;
            buyModel.PromotionType = chkPromotionType.Checked ? 1 : 0;
            buyModel.FinePrice = Common.Globals.SafeDecimal(this.txtFinePrice.Text, 0);
            buyModel.GroupCount = groupCount;
            buyModel.MaxCount = maxCount;
            buyModel.RegionId = selectedRegionId.Value;
            buyModel.GroupBase = int.Parse(this.txtGroupBase.Text);
            buyModel.Sequence = int.Parse(this.txtSequence.Text);
            buyModel.PromotionLimitQu = promotionLimitQu;
            buyModel.LeastbuyNum = leastbuynum;




            //待上传的图片名称
            string savePath = string.Format("/Upload/Shop/Images/Product/GroupBy/{0}/", DateTime.Now.ToString("yyyyMMdd"));



            ArrayList imageList = new ArrayList();

            if (!string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
            {

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
            }



            if (null != pro)
            {
                buyModel.ProductName = pro.ProductName;
                //buyModel.GroupBuyImage = pro.ThumbnailUrl1;  
            }
           
            buyModel.StartDate = Common.Globals.SafeDateTime(txtStartDate.Text, DateTime.MinValue);
            if (buyBll.Update(buyModel))
            {
                Common.MessageBox.ShowSuccessTip(this, "操作成功");
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>closefrm(1);</script>");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }
        }

    }
}