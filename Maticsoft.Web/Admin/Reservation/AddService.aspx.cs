using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Maticsoft.Model.Settings;
using System.Collections;
using Maticsoft.Model.Shop.Products;
using System.Text;

namespace Maticsoft.Web.Admin.Members.Reservation
{
    public partial class AddService : System.Web.UI.Page
    {
        BLL.Appt.Services bll = new BLL.Appt.Services();
        Model.Appt.Services serverModel = new Model.Appt.Services();
        private string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Size thumbSize = Maticsoft.Common.StringPlus.SplitToSize(
                   BLL.SysManage.ConfigSystem.GetValueByCache(SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                   '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
                hfProductImagesThumbSize.Value = thumbSize.Width + "," + thumbSize.Height;
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
                serverModel.ImageUrl = serverModel.ThumbnailUrl="/Content/themes/base/Shop/images/none.png";
            }

            //待上传的图片名称
            string savePath = string.Format("/Upload/Appt/Images/Reservation/{0}/", DateTime.Now.ToString("yyyyMMdd"));
            string saveThumbsPath = "/Upload/Appt/Images/ReservationThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            ArrayList imageList = new ArrayList();
            StringBuilder imageSB = new StringBuilder();
            StringBuilder thumbSB = new StringBuilder();
            for (int i = 0; i < productImages.Length; i++)
            {
                 string imageUrl = string.Format(productImages[i], "");
                 string thumbnailUrl = productImages[i];
                 imageUrl = imageUrl.Replace(tempFile, savePath) + "|";
                 thumbnailUrl = thumbnailUrl.Replace(tempFile, saveThumbsPath) + "|";
                 imageSB.Append(imageUrl);
                 thumbSB.Append(thumbnailUrl);
                Maticsoft.BLL.Appt.Services.MoveImage(productImages[i], savePath, saveThumbsPath);
            }
            #endregion 
            serverModel.ImageUrl = imageSB.ToString();
            serverModel.ThumbnailUrl = thumbSB.ToString();
            serverModel.Name = txtServiceName.Text.Trim();
            serverModel.StartDate =Common.Globals.SafeDateTime(txtStartDate.Text.Trim(),DateTime.Now);
            serverModel.EndDate = Common.Globals.SafeDateTime(txtEndDate.Text.Trim(),DateTime.Now);
            serverModel.IsPay = Common.Globals.SafeBool(rblSex.Text,false);
            serverModel.ServiceType = Common.Globals.SafeInt(ddlServiceType.Value,-1);
            serverModel.RuleType = Common.Globals.SafeInt(rblRuleType.Text, -1);
            serverModel.MaxCount = Common.Globals.SafeInt(txtMaxCount.Text, 0);
            serverModel.Summary = txtSummary.Text;
            serverModel.Description = txtDescription.Text;
            serverModel.Remark = txtRemark.Text;
            if (bll.Add(serverModel) > 0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "ReservationService.aspx");
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
    }
}