using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.Ms.WaterMarks
{
    public partial class Setting : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 341; } } //设置_水印设置页
        Hashtable ht;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            string value = Maticsoft.BLL.SysManage.ConfigSystem.GetValue("System_ThumbImage_AddWater");
            this.radlStatus.SelectedValue = value;
            this.txtIsAddWater.Value = value;

            string waterMarkType = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkType");
            if (!string.IsNullOrEmpty(waterMarkType))
            {
                ddlType.SelectedValue = waterMarkType;
            }
            string waterMarkContent = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkContent");
            if (!string.IsNullOrEmpty(waterMarkContent))
            {
                txtWaterWords.Text = waterMarkContent;
            }
            string waterMarkFont = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFont");
            if (!string.IsNullOrEmpty(waterMarkFont))
            {
                ddlFont.SelectedValue = waterMarkFont;

            }
            string waterMarkFontSize = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFontSize");
            if (!string.IsNullOrEmpty(waterMarkFontSize))
            {
                txtWordsSize.Text = waterMarkFontSize;

            }

            string waterMarkPosition = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (!string.IsNullOrEmpty(waterMarkPosition))
            {
                ddlPosition.SelectedValue = waterMarkPosition;

            }
            string waterMarkFontColor = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFontColor");
            if (!string.IsNullOrEmpty(waterMarkFontColor))
            {
                txtColor.Text = waterMarkFontColor;

            }
            string IswaterMarkThumPic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_IswaterMarkThumPic");
            if (!string.IsNullOrEmpty(IswaterMarkThumPic) && Common.Globals.SafeInt(IswaterMarkThumPic, 0) > 0)
            {
                chkThum.Checked = true;

            }
            string IswaterMarkHDPic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_IswaterMarkHDPic");
            if (!string.IsNullOrEmpty(IswaterMarkThumPic) && Common.Globals.SafeInt(IswaterMarkHDPic, 0) > 0)
            {
                chkHD.Checked = true;

            }
            string waterMarkTransparent = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkTransparent");
            if (!string.IsNullOrEmpty(waterMarkTransparent))
            {
                txtTransparent.Text = waterMarkTransparent;

            }
            string waterMarkPhotoUrl = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkPhotoUrl");
            if (!string.IsNullOrEmpty(waterMarkPhotoUrl))
            {
                logo.Src = waterMarkPhotoUrl;
                hfLogoUrl.Value = waterMarkPhotoUrl;
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            ht = Maticsoft.BLL.SysManage.ConfigSystem.GetHashListByCache(Maticsoft.Model.SysManage.ApplicationKeyType.Shop);
            int error = 0;
            error += UpdateConfigSystem("System_waterMarkType", "生成水印的类型", ddlType.SelectedValue);
            error += UpdateConfigSystem("System_waterMarkContent", "生成水印的内容", txtWaterWords.Text);
            error += UpdateConfigSystem("System_waterMarkFont", "生成水印的内容的字体", ddlFont.SelectedValue);
            error += UpdateConfigSystem("System_waterMarkFontSize", "生成水印的内容的字体的大小", txtWordsSize.Text);
            error += UpdateConfigSystem("System_waterMarkPosition", "生成水印内容的位置", ddlPosition.SelectedValue);
            error += UpdateConfigSystem("System_IswaterMarkThumPic", "是否生成给缩略图生成水印(0:否 1：是)", chkThum.Checked ? "1" : "0");
            error += UpdateConfigSystem("System_IswaterMarkHDPic", "是否生成给清晰图生成水印(0:否 1：是)", chkHD.Checked ? "1" : "0");
            error += UpdateConfigSystem("System_waterMarkTransparent", "水印的透明度", txtTransparent.Text);
            error += UpdateConfigSystem("System_waterMarkFontColor", "水印的颜色", txtColor.Text);

            #region 处理图片

            if (ddlType.SelectedValue == "1")
            {

                if (string.IsNullOrEmpty(hfLogoUrl.Value))
                {
                    Common.MessageBox.ShowSuccessTip(this, "请上传文件");
                    return;
                }

                error += UpdateConfigSystem("System_waterMarkPhotoUrl", "水印的图片", hfLogoUrl.Value);
                logo.Src = hfLogoUrl.Value;
            }

            #endregion


            if (error == 0)
            {
                MessageBox.ShowSuccessTip(this, "保存成功");
            }
            else
            {
                MessageBox.ShowFailTip(this, "出现异常，请重试");
            }



        }


        private int UpdateConfigSystem(string key, string description, string value, Model.SysManage.ApplicationKeyType type = Model.SysManage.ApplicationKeyType.Shop)
        {
            try
            {
                BLL.SysManage.ConfigSystem.Modify(key, value, description, type);
                if (ht != null)
                {
                    ht[key] = value;
                }
                return 0;
            }
            catch (Exception)
            {
                return 1;

            }



        }


        protected void ReSet_Click(object sender, EventArgs e)
        {

            Response.Redirect("Setting.aspx");
        }

        protected void radlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.radlStatus.SelectedValue;
            BLL.SysManage.ConfigSystem.Modify("System_ThumbImage_AddWater", value, "图片缩略图是否添加水印", ApplicationKeyType.System);
            Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);//清除网站设置的缓存文件
            Response.Redirect("Setting.aspx");
        }
    }
}