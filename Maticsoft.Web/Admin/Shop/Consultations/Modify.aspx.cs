/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Web.UI;
using Maticsoft.Common;
using Maticsoft.BLL.SysManage;

namespace Maticsoft.Web.Admin.Shop.Consultations
{
    public partial class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.ProductConsults bll = new Maticsoft.BLL.Shop.Products.ProductConsults();
        protected override int Act_PageLoad { get { return 405; } } //Shop_商品咨询管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ConsultationId>0)
                {
                    ShowInfo(ConsultationId);
                }
            }
        }

        public int ConsultationId
        {
            get
            {
                int cid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    cid = Globals.SafeInt(Request.Params["id"], 0);
                }
                return cid;
            }
        }

        private void ShowInfo(int ConsultationId)
        {
            Maticsoft.Model.Shop.Products.ProductConsults model = new Maticsoft.Model.Shop.Products.ProductConsults();
            this.txtReplyText.Text = model.ReplyText;
            this.chkIsStatus.Checked = model.Status==1;
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string ReplyText = this.txtReplyText.Text;
            bool IsStatus = this.chkIsStatus.Checked;

            Maticsoft.Model.Shop.Products.ProductConsults model = bll.GetModel(ConsultationId);
            model.ConsultationId = ConsultationId;
            model.ReplyDate = DateTime.Now;
            model.IsReply = true;
            model.ReplyText = ReplyText;
            model.ReplyUserId = CurrentUser.UserID;
            model.ReplyUserName = CurrentUser.UserName;
            model.Status = IsStatus ? 1 : 0;
            //model.IsTop = IsTop;

            if (bll.Update(model))
            {
                if (chbSendEmail.Checked)
                {
                    //发送邮件
                    SendEmail(model.UserEmail);
                }
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "网络异常，请稍后再试");
                return;
            }
        }

        private void SendEmail(string  email)
        {
            Model.MailConfig site = new BLL.MailConfig().GetModel();
            WebSiteSet webSet = new WebSiteSet(Maticsoft.Model.SysManage.ApplicationKeyType.Shop);
            string WebName = webSet.WebName;
            string content = string.Format("您对商品【{0}】的咨询有了新的回复，请及时查看！", WebName);// EmailBodyTemplate
            Maticsoft.Email.Model.EmailQueue model = new Email.Model.EmailQueue();
            model.EmailTo = site.Mailaddress;
            model.EmailSubject = string.Format("{0}回复通知", WebName);
            model.EmailFrom = site.Mailaddress;
            model.EmailBody = content;
            model.EmailPriority = 0;
            model.IsBodyHtml = false;
            model.NextTryTime = DateTime.Now;
            Maticsoft.Email.EmailManage.PushQueue(model);
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
        }
    }
}