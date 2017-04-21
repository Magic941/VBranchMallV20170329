using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Model.SysManage;
using System.Collections;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class SysConfig : PageBaseAdmin
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

            this.txtPageFootJs.Text = WebSiteSet.PageFootJs;
            this.txtBaiduShareUserId.Text = WebSiteSet.BaiduShareUserId;
            this.txtContent.Text = Globals.HtmlDecode(WebSiteSet.RegistStatement);
            chk_OpenLogin.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            chk_OpenRegister.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
            chk_OpenRegisterSendEmail.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_RegisterEmailCheck");
            string regStr = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RegisterToggle");

            int k = 0;
            for (int i = 0; i < this.RadioButtonRegister.Items.Count; i++)
            {
                if (this.RadioButtonRegister.Items[i].Value == regStr)
                {
                    this.RadioButtonRegister.Items[i].Selected = true;
                    k++;
                    break;
                }
            }
            if (k == 0)
            {
                RadioButtonRegister.SelectedValue = "Email";
            }
            hidradiobutRegStr.Value = RadioButtonRegister.SelectedValue;
            string buyMode = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_BuyMode");
            if (string.IsNullOrWhiteSpace(buyMode))
            {
                buyMode = "AddCart";
            }
            rdoBuyMode.SelectedValue = buyMode;
            txtRechargeRatio.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_RechargeRatio");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                string ImageFile = "/Upload/WebSiteLogo";
                ArrayList imageList = new ArrayList();
                WebSiteSet.PageFootJs = this.txtPageFootJs.Text.Trim();
                WebSiteSet.BaiduShareUserId = this.txtBaiduShareUserId.Text.Trim();
                UpdateKey("System_Close_Login", chk_OpenLogin.Checked.ToString(), "是否关闭社区的用户登录功能");
                UpdateKey("System_Close_Register", chk_OpenRegister.Checked.ToString(), "是否关闭社区的用户注册功能");
                UpdateKey("System_Close_RegisterEmailCheck", chk_OpenRegisterSendEmail.Checked.ToString(), "是否关闭商城注册邮件验证功能");
                UpdateKey("Shop_RegisterToggle", RadioButtonRegister.SelectedValue, "注册方式：1.Email 邮件注册 2.Phone 手机注册");
                UpdateKey("Shop_BuyMode", rdoBuyMode.SelectedValue, "购买方式：AddCart 加入购物车 BuyNow 立刻购买");
                UpdateKey("Shop_RechargeRatio", txtRechargeRatio.Text, "充值金额比例");

                string txtcontent = this.txtContent.Text.Trim();
                txtcontent = txtcontent.Replace("\n", "");
                WebSiteSet.RegistStatement = Globals.HtmlDecode(txtcontent);

                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);//清除网站设置的缓存文件
                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.Shop);
                this.btnReset.Enabled = false;
                this.btnSave.Enabled = false;
                Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "SysConfig.aspx");
            }
            catch (Exception)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "SysConfig.aspx");
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