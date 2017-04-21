using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Ms;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.Guestbook
{
    public partial class ReplyGuestBook : PageBaseAdmin
    {
        Maticsoft.BLL.Ms.EmailTemplet EmailBll=new EmailTemplet();
        protected override int Act_PageLoad { get { return 286; } } //客服管理_客户反馈_回复页
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int Id
        {
            get
            {
                int id = -1;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], -1);
                }
                return id;
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.Members.Guestbook bll=new BLL.Members.Guestbook();
            Maticsoft.Model.Members.Guestbook model=bll.GetModel(Id);
            try
            {
             if (model != null)
            {
                model.ReplyDescription = TxtReply.Text;
                model.Status = 1;
                model.HandlerDate = DateTime.Now;
                model.HandlerNickName = CurrentUser.NickName;
                model.HandlerUserID = CurrentUser.UserID;
                if (bll.Update(model))//EmailBll.SendGuestBookEmail(model)&&
                {
                    Common.MessageBox.ShowSuccessTip(this,"发送成功");
                    lblTip.Visible = true;
                }
                else
                {
                    Common.MessageBox.ShowFailTip(this, "发送失败，请检查邮件配置");
                    lblTip.InnerText = "出现异常，请重试";
                    lblTip.Visible = true;

                }

            }
    
            }
            catch (Exception)
            {
                Common.MessageBox.ShowFailTip(this, "发送失败，请检查邮件配置");
            }
          

        }
    }
}