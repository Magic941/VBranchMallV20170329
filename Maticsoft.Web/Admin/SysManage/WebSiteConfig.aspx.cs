using System;
using Maticsoft.Model.SysManage;
using Maticsoft.Common;
using System.Collections;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class WebSiteConfig : PageBaseAdmin
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

        BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

        private void BoundData()
        {
            this.txtWebSiteName.Text = Globals.HtmlDecode(WebSiteSet.WebName);
            this.txtBaseHost.Text = Globals.HtmlDecode(WebSiteSet.BaseHost);
            this.txtCopyRight.Text = Globals.HtmlDecode(WebSiteSet.WebPowerBy);
            if (!string.IsNullOrWhiteSpace(WebSiteSet.LogoPath))
            {
                this.imgLogo.ImageUrl = WebSiteSet.LogoPath;
            }
            this.txtWebRecord.Text = Globals.HtmlDecode(WebSiteSet.WebRecord);
         
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                WebSiteSet.WebName = Globals.HtmlEncode(this.txtWebSiteName.Text.Trim().Replace("\n", ""));
                WebSiteSet.BaseHost = Globals.HtmlEncode(this.txtBaseHost.Text.Trim().Replace("\n", ""));
                WebSiteSet.WebPowerBy = Globals.HtmlEncode(this.txtCopyRight.Text.Trim().Replace("\n", ""));
                string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                string ImageFile = "/Upload/WebSiteLogo";
                ArrayList imageList = new ArrayList();


                if (!string.IsNullOrWhiteSpace(this.hfs_ICOPath.Value))
                {
                    string imageUrl = string.Format(this.hfs_ICOPath.Value, "");
                    imageList.Add(imageUrl.Replace(tempFile, ""));
                    WebSiteSet.LogoPath = imageUrl.Replace(tempFile, ImageFile);
                }


                WebSiteSet.WebRecord = Globals.HtmlEncode(this.txtWebRecord.Text.Trim().Replace("\n", "").Trim());
                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);//清除网站设置的缓存文件

                this.btnReset.Enabled = false;
                this.btnSave.Enabled = false;


                Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "WebSiteConfig.aspx");
            }
            catch (Exception)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "WebSiteConfig.aspx");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundData();
        }

        public bool UpdateKey(string keyName, string value, string desc)
        {
            return BLL.SysManage.ConfigSystem.Modify(keyName, value, desc, ApplicationKeyType.Shop);
        }
    }
}