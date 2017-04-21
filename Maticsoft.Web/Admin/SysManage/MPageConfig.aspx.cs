using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Model.SysManage;
using Maticsoft.Common;
using System.Collections;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class MPageConfig : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 160; } } //网站管理_是否显示网站设置页面
        protected new int Act_UpdateData = 161;    //网站管理_网站设置_编辑网站信息
        public string SeoSetting = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    btnSave.Visible = false;
                }
                BoundData();
            }
          
        }


        private void BoundData()
        {
            this.txtMPageName.Text = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Name");
            string MPageLogo = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Logo");
            if (!string.IsNullOrWhiteSpace(MPageLogo))
            {
                this.imgLogo.ImageUrl = MPageLogo;
            }
            this.txtMPageTel.Text = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Phone");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.SysManage.ConfigSystem.Modify("WeChat_MPage_Name", Globals.HtmlEncode(this.txtMPageName.Text), "微官网名称", ApplicationKeyType.System);
                string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                string ImageFile = "/Upload/WebSiteLogo";
                ArrayList imageList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.hfs_ICOPath.Value))
                {
                    string imageUrl = string.Format(this.hfs_ICOPath.Value, "");
                    imageList.Add(imageUrl.Replace(tempFile, ""));
                    string logoPath = imageUrl.Replace(tempFile, ImageFile);
                    BLL.SysManage.ConfigSystem.Modify("WeChat_MPage_Logo", logoPath, "微官网Logo", ApplicationKeyType.System);
                }
                BLL.SysManage.ConfigSystem.Modify("WeChat_MPage_Phone",  this.txtMPageTel.Text, "微官网号码", ApplicationKeyType.System);

                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);//清除网站设置的缓存文件
                Cache.Remove("ConfigSystemHashList");

                this.btnSave.Enabled = false;
                Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "MPageConfig.aspx");
            }
            catch (Exception)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "MPageConfig.aspx");
            }
        }

   
     
    }
}