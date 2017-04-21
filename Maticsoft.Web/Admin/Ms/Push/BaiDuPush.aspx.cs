using System;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.Ms.Push
{
    public partial class BaiDuPush : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 322; } }   //设置_App信息推送页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //百度云推送
                this.txtApiKey.Text = BLL.SysManage.ConfigSystem.GetValueByCache("API_BaiDuPushApiKey", ApplicationKeyType.OpenAPI);
                this.txtSecretKey.Text = BLL.SysManage.ConfigSystem.GetValueByCache("API_BaiDuPushSecretKey", ApplicationKeyType.OpenAPI);
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string secretKey = txtSecretKey.Text;
            string apiKey = txtApiKey.Text;
            if (string.IsNullOrWhiteSpace(secretKey) || string.IsNullOrWhiteSpace(apiKey))
            {
                Common.MessageBox.ShowFailTip(this, "您还未设置API信息, 请先配置一下!");
                return;
            }
            BaiduPush Bpush = new BaiduPush("POST", secretKey);

            //发送通知
            uint message_type = 1;
            BaiduPushNotification notification = new BaiduPushNotification();
            notification.title = string.Empty;
            notification.description = txtMessage.Text;
            string messages = notification.getJsonString();
            string method = "push_msg";
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            uint device_type = 3;   //Android
            uint unixTime = (uint)ts.TotalSeconds;

            string messageksy = DateTime.Now.ToString("yyyyMMddHHmmss");
            string response = string.Empty;
            try
            {
                PushOptions options = new PushOptions(method, apiKey, device_type, messages, messageksy, unixTime);
                options.message_type = message_type;

                response = Bpush.PushMessage(options);

                txtMessage.Text = string.Empty;
                Common.MessageBox.ShowSuccessTip(this, "发送成功!");
            }
            catch (Exception ex)
            {
                Common.MessageBox.ShowFailTip(this, "发送失败: " + ex.Message);

                LogHelp.AddErrorLog("APP_Push_Baidu_ERROR: " + response, ex.StackTrace, this.Request);
            }
        }
    }
}
