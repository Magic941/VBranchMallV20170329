using System;
using System.Web;
using System.Net;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.Ms.QR
{
    public partial class QRCode : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 323; } } //设置_二维码生成页
        private readonly string _uploadFolder = string.Format("/{0}/QR/",
           MvcApplication.UploadFolder);
        private readonly string _uploadFolderMapPath;
       
        private readonly string websiteImg;
        private readonly string androidImg;

        private const string KEY_WEBSITE = "QR_URL_WEBSITE";
        private const string KEY_ANDROID = "QR_URL_ANDROID";

        private const ApplicationKeyType applicationKeyType = ApplicationKeyType.Mobile;

        public QRCode()
        {
            _uploadFolderMapPath = HttpContext.Current.Server.MapPath(_uploadFolder);
            websiteImg = _uploadFolderMapPath + "website.png";
            androidImg = _uploadFolderMapPath + "android.png";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        private void ShowInfo()
        {
            txtWebsiteURL.Text = BLL.SysManage.ConfigSystem.GetValueByCache(KEY_WEBSITE, applicationKeyType);
            txtAndroidURL.Text = BLL.SysManage.ConfigSystem.GetValueByCache(KEY_ANDROID, applicationKeyType);

            if (string.IsNullOrWhiteSpace(txtWebsiteURL.Text)) txtWebsiteURL.Text = "http://";
            if (string.IsNullOrWhiteSpace(txtAndroidURL.Text)) txtAndroidURL.Text = "http://";
        }

        protected void btnGen_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtContent.Text))
            //{
            //    Common.MessageBox.ShowSuccessTip(this, "请输入二维码内容!");
            //    return;
            //}

            string websiteUrl = (!txtWebsiteURL.Text.StartsWith("http") ? "http://" : "") + txtWebsiteURL.Text;
            string androidUrl = (!txtAndroidURL.Text.StartsWith("http") ? "http://" : "") + txtAndroidURL.Text;

            //清空, 前台不显示
            if (websiteUrl == "http://") websiteUrl = string.Empty;
            if (androidUrl == "http://") androidUrl = string.Empty;

            //保存到参数表
            BLL.SysManage.ConfigSystem.Modify(KEY_WEBSITE, websiteUrl, KEY_WEBSITE, applicationKeyType);
            BLL.SysManage.ConfigSystem.Modify(KEY_ANDROID, androidUrl, KEY_ANDROID, applicationKeyType);

            #region 设置二维码参数

            int size = Common.Globals.SafeInt(txtSize.Text, -1);
            if (size < 0) txtSize.Text = (size = 200).ToString();

            string level = drpFaultRate.SelectedValue;
            string format = droImgFormat.SelectedValue;

            int margin = Common.Globals.SafeInt(txtMargin.Text, -1);
            if (margin < 0) txtMargin.Text = (margin = 4).ToString();

            string baseURL = string.Format("/tools/qr/gen.aspx?margin={0}&size={1}&level={2}&format={3}&content={4}",
                margin, size, level, format, "{0}");

            #endregion

            if (!System.IO.Directory.Exists(_uploadFolderMapPath))
            {
                System.IO.Directory.CreateDirectory(_uploadFolderMapPath);
            }
            WebClient webClient = new WebClient();
            try
            {
                if (string.IsNullOrWhiteSpace(websiteUrl))
                {
                    //清空, 前台不显示
                    if (System.IO.File.Exists(websiteImg))
                        System.IO.File.Delete(websiteImg);
                }
                else
                {
                    //重新生成二维码
                    websiteUrl = "http://" + Common.Globals.DomainFullName +
                                 string.Format(baseURL, Common.Globals.UrlEncode(websiteUrl));
                    webClient.DownloadFile(websiteUrl, websiteImg);
                }

                if (string.IsNullOrWhiteSpace(androidUrl))
                {
                    //清空, 前台不显示
                    if (System.IO.File.Exists(androidImg))
                        System.IO.File.Delete(androidImg);
                }
                else
                {
                    //重新生成二维码
                    androidUrl = "http://" + Common.Globals.DomainFullName +
                                 string.Format(baseURL, Common.Globals.UrlEncode(androidUrl));
                    webClient.DownloadFile(androidUrl, androidImg);
                }
            }
            catch (Exception ex)
            {
                Common.MessageBox.ShowFailTip(this, "二维码生成失败! " + ex.Message);
                return;
            }
            //string content = Common.Globals.UrlEncode(txtContent.Text);
            //imgResult.ImageUrl = string.Format(
            //    "/tools/qr/gen.aspx?margin={0}&size={1}&level={2}&format={3}&content={4}",
            //    margin, size, level, format, content);

            Cache.Remove("ConfigSystemHashList_" + applicationKeyType); //清除缓存
            ShowInfo();
            Common.MessageBox.ShowSuccessTip(this, "二维码生成成功!");
        }
    }
}
