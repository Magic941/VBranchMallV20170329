using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Model.SysManage;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Setting
{
    public partial class Config : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 647; } } //移动营销_移动设置页
        private readonly string _uploadFolder = string.Format("/{0}/QR/",
           MvcApplication.UploadFolder);
        private readonly string _uploadFolderMapPath;

        private readonly string websiteImg;
        private readonly string androidImg;

        private const string KEY_WEBSITE = "WebChat_QR_Url";

        private const ApplicationKeyType applicationKeyType = ApplicationKeyType.Mobile;

        public Config()
        {
            _uploadFolderMapPath = HttpContext.Current.Server.MapPath(_uploadFolder);
            websiteImg = _uploadFolderMapPath + "website.png";
            androidImg = _uploadFolderMapPath + "android.png";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //配置微信的接口相关信息
                this.txtUrl.Text = "http://" + Common.Globals.DomainFullName + "/wcapi.aspx";
                ShowInfo();
            }
        }

        private void ShowInfo()
        {

            this.txtWeChatAppId.Text = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            this.txtWeChatAppSercet.Text = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            this.txtWeChatOriginalId.Text = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            this.chkWeChatLogin.Checked = Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AutoLogin", -1, CurrentUser.UserType), false);
            this.chkWeChatTransfer.Checked = Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_CustomTransfer", -1, CurrentUser.UserType), false);

            bool IsApprove = Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_IsApprove", -1, CurrentUser.UserType), false);
            if (IsApprove)
            {
                this.chkWeChatLogin.Enabled = true;
                this.rblTaskMsg.Enabled = true;
                this.chkWeChatTransfer.Enabled = true;
            }
            else
            {
                this.chkWeChatLogin.Enabled = false;
                this.chkWeChatLogin.Checked = false;
                this.rblTaskMsg.Enabled = false;
                this.rblTaskMsg.SelectedValue = "0";
                this.chkWeChatTransfer.Enabled = false;
                this.chkWeChatTransfer.Checked = false;
            }
            this.chkApprove.Checked = IsApprove;

            this.chkHideOptionMenu.Checked = Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_HideMenu", -1, CurrentUser.UserType), false);

            string token = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("System_WeChat_Token");
            this.rblTaskMsg.SelectedValue = Common.Globals.SafeInt(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_LocationMsg", -1, CurrentUser.UserType), 0).ToString();
            string domain = Common.Globals.TopLevelDomain;
            this.txtToken.Text = String.IsNullOrWhiteSpace(token) ? domain : token;

        }
        protected void btnSaveOpenId_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.txtWeChatOriginalId.Text))
            {
                Common.MessageBox.ShowFailTip(this, "请填写微信原始ID!");
                return;
            }

            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_OpenId", this.txtWeChatOriginalId.Text, -1, CurrentUser.UserType, "微信原始ID", false);

            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_AppId", this.txtWeChatAppId.Text, -1, CurrentUser.UserType, "微信AppId", false);
            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_AppSercet", this.txtWeChatAppSercet.Text, -1, CurrentUser.UserType, "微信AppSercet", false);
            Maticsoft.BLL.SysManage.ConfigSystem.Modify("System_WeChat_Token", this.txtToken.Text, "微信Token", ApplicationKeyType.System);



            //微信APi
            Cache.Remove("ConfigSystemHashList_" + applicationKeyType); //清除缓存
            Cache.Remove("ConfigSystemHashList");
            Maticsoft.WeChat.BLL.Core.Config.ClearCache();
            ShowInfo();
            Common.MessageBox.ShowSuccessTip(this, "设置成功!");

        }

        protected void btnSaveOther_Click(object sender, EventArgs e)
        {

            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_AutoLogin", this.chkWeChatLogin.Checked.ToString(), -1, CurrentUser.UserType, "微信自动登录", false);
            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_CustomTransfer", this.chkWeChatTransfer.Checked.ToString(), -1, CurrentUser.UserType, "微信多客服功能", false);

            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_HideMenu", this.chkHideOptionMenu.Checked.ToString(), -1, CurrentUser.UserType, "微信隐藏网页右上角按钮", false);

            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_LocationMsg", this.rblTaskMsg.SelectedValue, -1, CurrentUser.UserType, "微信任务消息推送设置", false);

            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_IsApprove", this.chkApprove.Checked.ToString(), -1, CurrentUser.UserType, "微信公众号是否认证，并且有高级接口", false);

            //微信APi
            Cache.Remove("ConfigSystemHashList_" + applicationKeyType); //清除缓存
            Cache.Remove("ConfigSystemHashList");
            Maticsoft.WeChat.BLL.Core.Config.ClearCache();
            ShowInfo();
            Common.MessageBox.ShowSuccessTip(this, "设置成功!");

        }


        protected void Check_Changed(object sender, System.EventArgs e)
        {
            bool IsApprove = this.chkApprove.Checked;
            if (IsApprove)
            {
                this.chkWeChatLogin.Enabled = true;
                this.rblTaskMsg.Enabled = true;
                this.chkWeChatTransfer.Enabled = true;
            }
            else
            {
                this.chkWeChatLogin.Enabled = false;
                this.chkWeChatLogin.Checked = false;
                this.rblTaskMsg.Enabled = false;
                this.rblTaskMsg.SelectedValue = "0";
                this.chkWeChatTransfer.Enabled = false;
                this.chkWeChatTransfer.Checked = false;
            }
        }



    }
}