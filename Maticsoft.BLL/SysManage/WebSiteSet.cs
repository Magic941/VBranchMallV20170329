/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：WebSiteSet.cs
// 文件功能描述：
//
// 创建标识：
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using Maticsoft.Model.SysManage;

namespace Maticsoft.BLL.SysManage
{
    /// <summary>
    /// 网站设置
    /// </summary>
    public class WebSiteSet
    {
        private ApplicationKeyType applicationKeyType = ApplicationKeyType.System;

        public WebSiteSet(ApplicationKeyType keyType)
        {
            applicationKeyType = keyType;
        }

        #region  Baseinfo

        public const string WEB_NAME = "WebName";

        public string WebName
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.WEB_NAME, applicationKeyType); }
            set { ConfigSystem.Modify(WEB_NAME, value, WEB_NAME, applicationKeyType); }
        }

        public const string BASE_HOST = "BaseHost";

        public string BaseHost
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.BASE_HOST, applicationKeyType); }
            set { ConfigSystem.Modify(BASE_HOST, value, BASE_HOST, applicationKeyType); }
        }

        public const string WEB_POWERBY = "WebPowerBy";

        public string WebPowerBy
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.WEB_POWERBY, applicationKeyType); }
            set { ConfigSystem.Modify(WEB_POWERBY, value, WEB_POWERBY, applicationKeyType); }
        }

        public const string LOGO_PATH = "LogoPath";

        public string LogoPath
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.LOGO_PATH, applicationKeyType); }
            set { ConfigSystem.Modify(LOGO_PATH, value, LOGO_PATH, applicationKeyType); }
        }

        public const string WEB_RECORD = "WebRecord";

        public string WebRecord
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.WEB_RECORD, applicationKeyType); }
            set { ConfigSystem.Modify(WEB_RECORD, value, WEB_RECORD, applicationKeyType); }
        }

        public const string WEB_TITLE = "Title";

        public string WebTitle
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.WEB_TITLE, applicationKeyType); }
            set { ConfigSystem.Modify(WEB_TITLE, value, WEB_TITLE, applicationKeyType); }
        }

        public const string WEB_DESCRIPTION = "Description";

        public string Description
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.WEB_DESCRIPTION, applicationKeyType); }
            set { ConfigSystem.Modify(WEB_DESCRIPTION, value, WEB_DESCRIPTION, applicationKeyType); }
        }

        public const string KEY_WORDS = "Keywords";

        public string KeyWords
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.KEY_WORDS, applicationKeyType); }
            set { ConfigSystem.Modify(KEY_WORDS, value, KEY_WORDS, applicationKeyType); }
        }

        public const string KEY_PAGEFOOTJS = "PageFootJs";

        public string PageFootJs
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.KEY_PAGEFOOTJS, applicationKeyType); }
            set { ConfigSystem.Modify(KEY_PAGEFOOTJS, value, KEY_PAGEFOOTJS, applicationKeyType); }
        }

        //百度分享的用户id
        public const string WEB_BAIDUSHAREUSERID = "BaiduShareUserId";

        public string BaiduShareUserId
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.WEB_BAIDUSHAREUSERID, applicationKeyType); }
            set
            {
                string uid = Common.Globals.SafeInt(value, 0) == 0 ? "0" : value;
                ConfigSystem.Modify(WEB_BAIDUSHAREUSERID, uid, "百度分享的用户id", applicationKeyType);
            }
        }

        #endregion

        #region CompanyInfo

        //网站Logo
        public const string WEBSITE_LOGO = "WebSiteLogo";

        public string WebSite_Logo
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.WEBSITE_LOGO, applicationKeyType); }
            set { ConfigSystem.Modify(WEBSITE_LOGO, value, WEBSITE_LOGO, applicationKeyType); }
        }

        //网站域名
        public const string WEBSITE_DOMAIN = "WebSiteDomain";

        public string WebSite_Domain
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.WEBSITE_DOMAIN, applicationKeyType); }
            set { ConfigSystem.Modify(WEBSITE_DOMAIN, value, WEBSITE_DOMAIN, applicationKeyType); }
        }

        //公司名称
        public const string COMPANY_NAME = "CompanyName";

        public string Company_Name
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.COMPANY_NAME, applicationKeyType); }
            set { ConfigSystem.Modify(COMPANY_NAME, value, COMPANY_NAME, applicationKeyType); }
        }

        //公司地址
        public const string COMPANY_ADDRESS = "CompanyAddress";

        public string Company_Address
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.COMPANY_ADDRESS, applicationKeyType); }
            set { ConfigSystem.Modify(COMPANY_ADDRESS, value, COMPANY_ADDRESS, applicationKeyType); }
        }

        //公司电话
        public const string COMPANY_TELEPHONE = "CompanyTelephone";

        public string Company_Telephone
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.COMPANY_TELEPHONE, applicationKeyType); }
            set { ConfigSystem.Modify(COMPANY_TELEPHONE, value, COMPANY_TELEPHONE, applicationKeyType); }
        }

        //公司传真
        public const string COMPANY_FAX = "CompanyFax";

        public string Company_Fax
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.COMPANY_FAX, applicationKeyType); }
            set { ConfigSystem.Modify(COMPANY_FAX, value, COMPANY_FAX, applicationKeyType); }
        }

        //公司邮箱
        public const string COMPANY_MAIL = "CompanyMail";

        public string Company_Mail
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.COMPANY_MAIL, applicationKeyType); }
            set { ConfigSystem.Modify(COMPANY_MAIL, value, COMPANY_MAIL, applicationKeyType); }
        }

        #endregion

        #region BasicConfig


        //前台语言
        public const string FOREGROUND_LANGUAGE = "ForegroundLanguage";

        public string ForeGround_Language
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.FOREGROUND_LANGUAGE, applicationKeyType); }
            set { ConfigSystem.Modify(FOREGROUND_LANGUAGE, value, FOREGROUND_LANGUAGE, applicationKeyType); }
        }

        //时区信息
        public const string TIMEZONE_INFORMATION = "TimeZoneInformation";

        public string Timezone_Information
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.TIMEZONE_INFORMATION, applicationKeyType); }
            set { ConfigSystem.Modify(TIMEZONE_INFORMATION, value, TIMEZONE_INFORMATION, applicationKeyType); }
        }

        //时间格式

        public const string TIME_FORMAT = "TimeFormat";

        public string Time_Format
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.TIME_FORMAT, applicationKeyType); }
            set { ConfigSystem.Modify(TIME_FORMAT, value, TIME_FORMAT, applicationKeyType); }
        }

        //日期格式

        public const string DATE_FORMAT = "DateFormat";

        public string Date_Format
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.DATE_FORMAT, applicationKeyType); }
            set { ConfigSystem.Modify(DATE_FORMAT, value, DATE_FORMAT, applicationKeyType); }
        }


        //上传图片大小限制

        public const string SHOP_IMAGESIZES = "Shop_ImageSizes";

        public string Shop_ImageSizes
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.SHOP_IMAGESIZES, applicationKeyType); }
            set { ConfigSystem.Modify(SHOP_IMAGESIZES, value, SHOP_IMAGESIZES, applicationKeyType); }
        }

        //产品缩略图宽度

        public const string SHOP_THUMBIMAGEWIDTH = "Shop_ThumbImageWidth";

        public string Shop_ThumbImageWidth
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.SHOP_THUMBIMAGEWIDTH, applicationKeyType); }
            set { ConfigSystem.Modify(SHOP_THUMBIMAGEWIDTH, value, SHOP_THUMBIMAGEWIDTH, applicationKeyType); }
        }

        //产品缩略图高度

        public const string SHOP_THUMBIMAGEHEIGHT = "Shop_ThumbImageHeight";

        public string Shop_ThumbImageHeight
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.SHOP_THUMBIMAGEHEIGHT, applicationKeyType); }
            set { ConfigSystem.Modify(SHOP_THUMBIMAGEHEIGHT, value, SHOP_THUMBIMAGEHEIGHT, applicationKeyType); }
        }

        //产品清晰图宽度

        public const string SHOP_NORMALIMAGEWIDTH = "Shop_NormalImageWidth";

        public string Shop_NormalImageWidth
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.SHOP_NORMALIMAGEWIDTH, applicationKeyType); }
            set { ConfigSystem.Modify(SHOP_NORMALIMAGEWIDTH, value, SHOP_NORMALIMAGEWIDTH, applicationKeyType); }
        }

        //产品清晰图高度

        public const string SHOP_NORMALIMAGEHEIGHT = "Shop_NormalImageHeight";

        public string Shop_NormalImageHeight
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.SHOP_NORMALIMAGEHEIGHT, applicationKeyType); }
            set { ConfigSystem.Modify(SHOP_NORMALIMAGEHEIGHT, value, SHOP_NORMALIMAGEHEIGHT, applicationKeyType); }
        }


        #endregion

        #region RegistStatement

        //会员注册声明信息

        public const string REGIST_STATEMENT = "RegistStatement";

        public string RegistStatement
        {
            get { return ConfigSystem.GetValueByCache(WebSiteSet.REGIST_STATEMENT, applicationKeyType); }
            set { ConfigSystem.Modify(REGIST_STATEMENT, value, REGIST_STATEMENT, applicationKeyType); }
        }

        #endregion

    }
}