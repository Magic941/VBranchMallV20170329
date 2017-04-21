using System;

namespace Maticsoft.Web.Controls
{
    public partial class copyright : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.litCopyright.Text = BLL.SysManage.ConfigSystem.GetValueByCache("WebPowerBy");
            string webRecord = BLL.SysManage.ConfigSystem.GetValueByCache("WebRecord");
            if (!string.IsNullOrWhiteSpace(webRecord))
            {
                this.LitWebRecord.Text = "<br />" + Server.HtmlEncode(webRecord);
            }
        }
    }
}