using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.WeChat.Setting
{
    public partial class EmailTemplate : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            SubMsgTitle.Text = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_SubMsgEmailTitle", -1, CurrentUser.UserType);
            SubMsgDesc.Text = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_SubMsgEmailDesc", -1, CurrentUser.UserType);
            NoMsgTitle.Text = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_NoMsgEmailTitle", -1, CurrentUser.UserType);
            NoMsgDesc.Text = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_NoMsgEmailDesc", -1, CurrentUser.UserType);
            this.chkNoMsg.Checked = Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_ChkNoMsg", -1, CurrentUser.UserType), false);
            this.chkSubscribe.Checked = Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_ChkSubscribe", -1, CurrentUser.UserType), false);
            txtWCEmail.Text = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_Email", -1, CurrentUser.UserType);
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_SubMsgEmailTitle", SubMsgTitle.Text, -1, CurrentUser.UserType, "关注邮件模版标题");
            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_SubMsgEmailDesc", SubMsgDesc.Text, -1, CurrentUser.UserType, "关注邮件模版内容");
            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_NoMsgEmailTitle", NoMsgTitle.Text, -1, CurrentUser.UserType, "默认消息邮件标题");
            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_NoMsgEmailDesc", NoMsgDesc.Text, -1, CurrentUser.UserType, "默认消息邮件内容");

            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_Email", txtWCEmail.Text, -1, CurrentUser.UserType, "接收邮件邮箱");

            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_ChkNoMsg", this.chkNoMsg.Checked.ToString(), -1, CurrentUser.UserType, "微信回复是否自动发送邮件信息给管理员");
            Maticsoft.WeChat.BLL.Core.Config.Modify("WeChat_ChkSubscribe", this.chkSubscribe.Checked.ToString(), -1, CurrentUser.UserType, "微信用户关注自动发送邮件通知管理员");

            Maticsoft.WeChat.BLL.Core.Config.ClearCache();
            Common.MessageBox.ShowSuccessTip(this, "设置成功!");
        }

    }
}