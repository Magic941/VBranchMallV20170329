using System;
using Maticsoft.BLL.SNS;
using Maticsoft.BLL.Shop;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.Settings
{
    public partial class APIConfig : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 112; } } //运营管理_是否显示API接口设置页面
        protected new int Act_UpdateData = 113;    //运营管理_是否显示API接口_编辑接口信息
        private const ApplicationKeyType applicationKeyType = ApplicationKeyType.OpenAPI;

        protected string BaiduStr = string.Empty;
        protected string TaokeStr = string.Empty;
        protected string SinaStr = string.Empty;
        protected string QQStr = string.Empty;

        protected string TencentStr = string.Empty;
        protected string VideoStr = string.Empty;
        protected string ShopTaoStr = string.Empty;
        protected string TaoCodeStr = string.Empty;
        protected string WeChatStr = string.Empty;

        protected string ExpressStr = string.Empty;

        


        
        protected void Page_Load(object sender, EventArgs e)
        {
            //是否有编辑信息的权限
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                btnSave.Visible = false;
            }
            if (!IsPostBack)
            {
                //是否显示
                TaokeStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_TaokeApi_IsShow")  ? TaokeStr : "display: none";
                SinaStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_SinaApi_IsShow")?SinaStr:  "display: none";
                QQStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_QQApi_IsShow") ? QQStr : "display: none";
                TencentStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_TencentApi_IsShow") ? TencentStr : "display: none";
                BaiduStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_BaiduApi_IsShow") ? BaiduStr : "display: none";
                VideoStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_VideoApi_IsShow") ? VideoStr : "display: none";

                ShopTaoStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_ShopTaoApi_IsShow") ? ShopTaoStr : "display: none";
                TaoCodeStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_TaoCode_IsShow") ? TaoCodeStr : "display: none";
                WeChatStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_WeChatApi_IsShow") ? WeChatStr : "display: none";
                ExpressStr = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_ExpressApi_IsShow") ? ExpressStr : "display: none";

                this.txtSinaCallBack.Text = "http://" + Common.Globals.DomainFullName + "/social/sinacallback";
                this.txtTaoCallback.Text = "http://" + Common.Globals.DomainFullName + "/Social/TaoBaoCallback";

                BoundData();
            }
        }



        private BLL.SNS.TaoBaoConfig TaoBaoConfig = new BLL.SNS.TaoBaoConfig(applicationKeyType);
        private BLL.Shop.TaoBaoConfig ShopTaoConfig = new BLL.Shop.TaoBaoConfig(applicationKeyType);//卖家淘宝API设置

        private void BoundData()
        {
            //三方登录设置
            this.txtDengluAPPID.Text = BLL.SysManage.ConfigSystem.GetValueByCache("DengluAPPID", applicationKeyType);
            this.txtDengluAPIKEY.Text = BLL.SysManage.ConfigSystem.GetValueByCache("DengluAPIKEY", applicationKeyType);


            //社会化媒体设置
            this.txtSinaAppKey.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaAppId", applicationKeyType);
            this.txtSinaAppSercet.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaSercet", applicationKeyType);
            this.txtQQAPPID.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQAppId", applicationKeyType);
            this.txtQQAPPKEY.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQSercet", applicationKeyType);
            this.txtTencentAppId.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentAppId", applicationKeyType);
            this.txtTencentSercet.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentSercet", applicationKeyType);

            //淘宝客设置
            this.txtAppKey.Text = Globals.HtmlDecode(TaoBaoConfig.TaoBaoAppkey);
            this.txtAppsecret.Text = Globals.HtmlDecode(TaoBaoConfig.TaobaoAppsecret);
            this.txtApiUrl.Text = Globals.HtmlDecode(TaoBaoConfig.TaobaoApiUrl);
           
            //卖家淘宝API设置
            this.txtShopAppKey.Text = Globals.HtmlDecode(ShopTaoConfig.TaoBaoAppkey);
            this.txtShopAppsecret.Text = Globals.HtmlDecode(ShopTaoConfig.TaobaoAppsecret);
            this.txtShopApiUrl.Text = String.IsNullOrWhiteSpace(Globals.HtmlDecode(ShopTaoConfig.TaobaoApiUrl)) ? "http://gw.api.taobao.com/router/rest" : Globals.HtmlDecode(ShopTaoConfig.TaobaoApiUrl);

            //商品开启本地重定向跳转
            this.chkIsOpenR.Checked = BLL.SysManage.ConfigSystem.GetBoolValueByCache("SNS_Product_OpenRedirect");

            //百度云推送
            this.txtBaiDuPushApiKey.Text = BLL.SysManage.ConfigSystem.GetValueByCache("API_BaiDuPushApiKey", applicationKeyType);
            this.txtBaiDuPushSecretKey.Text = BLL.SysManage.ConfigSystem.GetValueByCache("API_BaiDuPushSecretKey", applicationKeyType);

            //视频接口设置
            this.txtYouKuAPI.Text = BLL.SysManage.ConfigSystem.GetValueByCache("YouKuAPI", applicationKeyType);

            //淘点金代码
             this.txtTaoCode.Text=BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_SNS_TaoBaoCode", applicationKeyType);

            //物流接口
             this.txtExpressKey.Text = BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_Express_ApiKey", applicationKeyType);
          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                //淘宝客设置
                TaoBaoConfig.TaoBaoAppkey = Globals.HtmlEncode(this.txtAppKey.Text);
                TaoBaoConfig.TaobaoAppsecret = Globals.HtmlEncode(this.txtAppsecret.Text);
                TaoBaoConfig.TaobaoApiUrl = Globals.HtmlEncode(this.txtApiUrl.Text);

                //卖家淘宝API设置
                ShopTaoConfig.TaoBaoAppkey = Globals.HtmlEncode(this.txtShopAppKey.Text);
                ShopTaoConfig.TaobaoAppsecret = Globals.HtmlEncode(this.txtShopAppsecret.Text);
                ShopTaoConfig.TaobaoApiUrl = Globals.HtmlEncode(this.txtShopApiUrl.Text);

                //商品开启本地重定向跳转
                BLL.SysManage.ConfigSystem.Modify("SNS_Product_OpenRedirect", this.chkIsOpenR.Checked.ToString(),
                    "分享商品开启本地重定向跳转，True 为确认开启，False 为不开启，默认为不开启", ApplicationKeyType.SNS);

                //视频接口设置
                EditKey("YouKuAPI", this.txtYouKuAPI.Text, "优酷视频接口");

                //社会化媒体设置
                EditKey("Social_SinaAppId", this.txtSinaAppKey.Text, "新浪媒体SinaAppKey");
                EditKey("Social_SinaSercet", this.txtSinaAppSercet.Text, "新浪媒体SinaAppSerce");
                EditKey("Social_QQAppId", this.txtQQAPPID.Text, "QQ媒体QQAPPID");
                EditKey("Social_QQSercet", this.txtQQAPPKEY.Text, "QQ媒体txtQQAPPKEY");
                EditKey("Social_TencentAppId", this.txtTencentAppId.Text, "腾讯微博媒体txtTencentAppId");
                EditKey("Social_TencentSercet", this.txtTencentSercet.Text, "腾讯微博媒体Social_TencentSercet");

                //淘点金代码
                BLL.SysManage.ConfigSystem.Modify("OpenAPI_SNS_TaoBaoCode", this.txtTaoCode.Text, "淘点金代码",
                                                  applicationKeyType);

                //物流接口
                BLL.SysManage.ConfigSystem.Modify("OpenAPI_Express_ApiKey", this.txtExpressKey.Text, "物流接口ApiKey",
                                                applicationKeyType);
      
            
                //百度云推送
                EditKey("API_BaiDuPushApiKey", this.txtBaiDuPushApiKey.Text, "百度云推送API_BaiDuPushApiKey");
                EditKey("API_BaiDuPushSecretKey", this.txtBaiDuPushSecretKey.Text, "百度云推送API_BaiDuPushSecretKey");

                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.SNS);
                Cache.Remove("ConfigSystemHashList_" + applicationKeyType);    //清除网站设置的缓存文件
                Cache.Remove("ConfigSystemHashList_" + TaoBaoConfig.applicationKeyType);    //清除网站设置的缓存文件

                this.btnReset.Enabled = false;
                this.btnSave.Enabled = false;
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "设置三方登录、淘宝客、百度云推送、视频接口成功", this);

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "APIConfig.aspx");
            }
            catch (Exception)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "APIConfig.aspx");
            }
        }

        public bool EditKey(string key, string value, string description)
        {
            return BLL.SysManage.ConfigSystem.Modify(key, value, description, applicationKeyType);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundData();
        }
    }
}