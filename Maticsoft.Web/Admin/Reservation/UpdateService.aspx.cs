using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.Settings;
using System.Drawing;
using System.Collections;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Admin.Members.Reservation
{
    public partial class UpdateService : System.Web.UI.Page
    {
        Model.Appt.Services serverModel = new Model.Appt.Services();
        private string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        BLL.Appt.Services bll = new BLL.Appt.Services();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Size thumbSize = Maticsoft.Common.StringPlus.SplitToSize(
                  BLL.SysManage.ConfigSystem.GetValueByCache(SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                  '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
                hfProductImagesThumbSize.Value = thumbSize.Width + "," + thumbSize.Height;
                Maticsoft.Model.Appt.Services model = bll.GetModel(ServiceID);
                if (model == null)
                {
                    Response.Redirect("ReservationService.aspx");
                }
                ddlServiceType.Value = model.ServiceType.ToString();
                txtServiceName.Text = model.Name.ToString();
                rblPay.SelectedValue = model.IsPay.ToString().ToLower();
                txtDescription.Text = model.Description.ToString();
                txtRemark.Text = model.Remark.ToString();
                txtSummary.Text = model.Summary.ToString();
                rblRuleType.SelectedValue = model.RuleType.ToString();

                txtStartDate.Text = model.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                txtEndDate.Text = model.EndDate.ToString("yyyy-MM-dd HH:mm:ss");

                txtMaxCount.Text = model.MaxCount.ToString();
                hfImage0.Value = GetImageUrl(ServiceID);
            }
        }

        protected int ServiceID
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 图片
            string[] productImages = new string[0];
            string splitProductImages = hfProductImages.Value;
            if (!string.IsNullOrWhiteSpace(splitProductImages))
            {
                productImages = splitProductImages.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (productImages.Length == 0)
            {
                serverModel.ImageUrl = serverModel.ThumbnailUrl = "/Content/themes/base/Shop/images/none.png";
            }

            //待上传的图片名称
            string savePath = string.Format("/Upload/Reservation/Images/Product/{0}/", DateTime.Now.ToString("yyyyMMdd"));
            string saveThumbsPath = "/Upload/Reservation/Images/ProductThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            ArrayList imageList = new ArrayList();
            for (int i = 0; i < productImages.Length; i++)
            {
                if (i == 0)
                {
                    //主图片
                    string imageUrl = string.Format(productImages[i], "");
                    string MainThumbnailUrl1 = productImages[i];
                    serverModel.ImageUrl = imageUrl.Replace(tempFile, savePath);
                    serverModel.ThumbnailUrl = MainThumbnailUrl1.Replace(tempFile, saveThumbsPath);
                    imageList.Add(imageUrl.Replace(tempFile, ""));
                    imageList.Add(MainThumbnailUrl1.Replace(tempFile, ""));
                }
                else
                {
                    //附图片
                    string AttachImageUrl = string.Format(productImages[i], "");
                    string AttachThumbnailUrl1 = productImages[i];
                    serverModel.ProductImages.Add(
                        new ProductImage
                        {
                            ImageUrl = AttachImageUrl.Replace(tempFile, savePath),
                            ThumbnailUrl1 = AttachThumbnailUrl1.Replace(tempFile, saveThumbsPath),
                        }
                        );
                    imageList.Add(AttachImageUrl.Replace(tempFile, ""));
                    imageList.Add(AttachThumbnailUrl1.Replace(tempFile, ""));
                }
                Maticsoft.BLL.Shop.Products.ProductImage.MoveImage(productImages[i], savePath, saveThumbsPath);
            }
            #endregion
            serverModel.Name = txtServiceName.Text.Trim();
            serverModel.StartDate = Common.Globals.SafeDateTime(txtStartDate.Text.Trim(), DateTime.Now);
            serverModel.EndDate = Common.Globals.SafeDateTime(txtEndDate.Text.Trim(), DateTime.Now);
            serverModel.IsPay = Common.Globals.SafeBool(rblPay.Text, false);
            serverModel.ServiceType = Common.Globals.SafeInt(ddlServiceType.Value, -1);
            serverModel.RuleType = Common.Globals.SafeInt(rblRuleType.Text, -1);
            serverModel.MaxCount = Common.Globals.SafeInt(txtMaxCount.Text, 0);
            serverModel.Summary = txtSummary.Text;
            serverModel.Description = txtDescription.Text;
            serverModel.Remark = txtRemark.Text;
            serverModel.ServiceId = ServiceID;
            if (bll.Update(serverModel))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "修改成功！", "ReservationService.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "系统忙，请稍后再试！");
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReservationService.aspx");
        }

        public string GetImageUrl(int serverID)
        {
            Model.Appt.Services model = bll.GetModel(serverID);
            string originalUrl="";
            if (model != null)
            {
                if (model.ThumbnailUrl != null)
                {
                    string[] imageUrl = model.ThumbnailUrl.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < imageUrl.Length; i++)
                    {
                        originalUrl = imageUrl[i].ToString().Replace( "{0}","T150X150_");
                    }
                }
            }
            return originalUrl;
        }
    }
}