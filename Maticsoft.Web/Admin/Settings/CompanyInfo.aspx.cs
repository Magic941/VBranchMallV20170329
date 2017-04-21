using System;
using Maticsoft.Model.SysManage;
using Maticsoft.Common;
using System.Collections;

namespace Maticsoft.Web.Admin.Settings
{
    public partial class CompanyInfo : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 160; } } //网站管理_是否显示网站设置页面
        protected new int Act_UpdateData = 161;    //网站管理_网站设置_编辑网站信息
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
            this.txtWebSiteDomain.Text = Globals.HtmlDecode(WebSiteSet.WebSite_Domain);
            this.txtCompanyName.Text = Globals.HtmlDecode(WebSiteSet.Company_Name);
            this.txtCompanyAddress.Text = Globals.HtmlDecode(WebSiteSet.Company_Address);
            this.txtCompanyTelephone.Text = Globals.HtmlDecode(WebSiteSet.Company_Telephone);
            this.txtCompanyFax.Text = Globals.HtmlDecode(WebSiteSet.Company_Fax);
            this.txtCompanyMail.Text = Globals.HtmlDecode(WebSiteSet.Company_Mail);
          
            if (!string.IsNullOrWhiteSpace(WebSiteSet.WebSite_Logo))
            {
                this.imgLogo.ImageUrl = WebSiteSet.WebSite_Logo;
            }
 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                WebSiteSet.WebSite_Domain = Globals.HtmlEncode(this.txtWebSiteDomain.Text.Trim().Replace("\n", ""));
                WebSiteSet.Company_Name = Globals.HtmlEncode(this.txtCompanyName.Text.Trim().Replace("\n", ""));
                WebSiteSet.Company_Address = Globals.HtmlEncode(this.txtCompanyAddress.Text.Trim().Replace("\n", ""));
                WebSiteSet.Company_Telephone = Globals.HtmlEncode(this.txtCompanyTelephone.Text.Trim().Replace("\n", ""));
                WebSiteSet.Company_Fax = Globals.HtmlEncode(this.txtCompanyFax.Text.Trim().Replace("\n", ""));
                WebSiteSet.Company_Mail = Globals.HtmlEncode(this.txtCompanyMail.Text.Trim().Replace("\n", ""));
                
                string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                string ImageFile = "/Upload/WebSiteLogo";
                ArrayList imageList = new ArrayList();

                if (!string.IsNullOrWhiteSpace(this.hfs_ICOPath.Value))
                {
                    string imageUrl = string.Format(this.hfs_ICOPath.Value, "");
                    imageList.Add(imageUrl.Replace(tempFile, ""));
                    WebSiteSet.WebSite_Logo = imageUrl.Replace(tempFile, ImageFile);
                }

                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);//清除网站设置的缓存文件

                this.btnReset.Enabled = false;
                this.btnSave.Enabled = false;


                Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "CompanyInfo.aspx");
            }
            catch (Exception)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "CompanyInfo.aspx");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundData();
        }
    }
}