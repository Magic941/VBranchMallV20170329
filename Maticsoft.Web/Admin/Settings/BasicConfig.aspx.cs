using System;
using Maticsoft.Model.SysManage;
using Maticsoft.Common;
using System.Collections;

namespace Maticsoft.Web.Admin.Settings
{
    public partial class BasicConfig : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 160; } }
        protected new int Act_UpdateData = 161;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    btnSave.Visible = false;
                }
                BoundDate();
            }
        }
        BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

        BLL.SysManage.WebSiteSet WebSiteSetShop = new BLL.SysManage.WebSiteSet(ApplicationKeyType.Shop);

       

        private void BoundDate()
        {
            this.ForeLanguage.Text = Globals.HtmlDecode(WebSiteSet.ForeGround_Language);
            this.TimeZone.Text = Globals.HtmlDecode(WebSiteSet.Timezone_Information);
            this.TimeFormat.Text = Globals.HtmlDecode(WebSiteSet.Time_Format);
            this.DateFormat.Text = Globals.HtmlDecode(WebSiteSet.Date_Format);

            this.ImageSizes.Text = Globals.HtmlDecode(WebSiteSetShop.Shop_ImageSizes);
            this.ThumbImgWidth.Text = Globals.HtmlDecode(WebSiteSetShop.Shop_ThumbImageWidth);
            this.ThumbImgHeight.Text = Globals.HtmlDecode(WebSiteSetShop.Shop_ThumbImageHeight);
            this.NormalImgWidth.Text = Globals.HtmlDecode(WebSiteSetShop.Shop_NormalImageWidth);
            this.NormalImgHeight.Text = Globals.HtmlDecode(WebSiteSetShop.Shop_NormalImageHeight);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                WebSiteSet.ForeGround_Language = Globals.HtmlEncode(this.ForeLanguage.Text.Trim().Replace("\n", ""));
                WebSiteSet.Timezone_Information = Globals.HtmlEncode(this.TimeZone.Text.Trim().Replace("\n", ""));
                WebSiteSet.Time_Format = Globals.HtmlEncode(this.TimeFormat.Text.Trim().Replace("\n", ""));
                WebSiteSet.Date_Format = Globals.HtmlEncode(this.DateFormat.Text.Trim().Replace("\n", ""));
                WebSiteSetShop.Shop_ImageSizes = Globals.HtmlEncode(this.ImageSizes.Text.Trim().Replace("\n", ""));
                WebSiteSetShop.Shop_ThumbImageWidth = Globals.HtmlEncode(this.ThumbImgWidth.Text.Trim().Replace("\n", ""));
                WebSiteSetShop.Shop_ThumbImageHeight = Globals.HtmlEncode(this.ThumbImgHeight.Text.Trim().Replace("\n", ""));
                WebSiteSetShop.Shop_NormalImageWidth = Globals.HtmlEncode(this.NormalImgWidth.Text.Trim().Replace("\n", ""));
                WebSiteSetShop.Shop_NormalImageHeight = Globals.HtmlEncode(this.NormalImgHeight.Text.Trim().Replace("\n", ""));

                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);//清除网站设置的缓存文件
                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.Shop);//清除网站设置的缓存文件

                this.btnSave.Enabled = false;
                this.btnReset.Enabled = false;

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "BasicConfig.aspx");
            }
            catch
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "BasicConfig.aspx");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundDate();
        }

        

        
    }

    
    
}