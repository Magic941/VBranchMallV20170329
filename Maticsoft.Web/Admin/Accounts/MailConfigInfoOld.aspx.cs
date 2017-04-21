using System;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.Accounts
{
    public partial class MailConfigInfoOld : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 162; } } //网站管理_是否显示邮件配置页面
        protected new int Act_UpdateData = 163;    //网站管理_邮件配置-编辑邮件信息
        Maticsoft.BLL.MailConfig bll = new Maticsoft.BLL.MailConfig();
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

        private void BoundData()
        {
            Maticsoft.Model.MailConfig model = bll.GetModel();
            if (model != null )
            {
                this.txtSMTPServer.Text = model.SMTPServer;
                this.txtSMTPPort.Text = model.SMTPPort.ToString();
                this.txtMailaddress.Text = model.Mailaddress;
                this.txtPassword.Attributes.Add("value", Maticsoft.Common.DEncrypt.DESEncrypt.Decrypt(model.Password));
                this.txtUsername.Text = model.Username;
                if (model.SMTPSSL )
                {
                    this.chkSMTPSSL.Checked = true;
                }
                else
                {
                    this.chkSMTPSSL.Checked = false;
                }
                this.HiddenField_ID.Value = model.ID.ToString();
            }
            else
            {
                this.txtSMTPServer.Text = "";
                this.txtSMTPPort.Text = "25";
                this.txtMailaddress.Text ="";
                this.txtUsername.Text ="";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.MailConfig model = bll.GetModel();
            if (model != null )
            {
                model.Mailaddress = txtMailaddress.Text;
                model.Password = Maticsoft.Common.DEncrypt.DESEncrypt.Encrypt(txtPassword.Text);
                model.SMTPPort = Convert.ToInt32(txtSMTPPort.Text);
                model.SMTPServer = txtSMTPServer.Text;
                model.SMTPSSL = chkSMTPSSL.Checked;
                model.Username = txtUsername.Text;
                bll.Update(model);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
            }
            else
            {
                Maticsoft.Model.MailConfig models = new Maticsoft.Model.MailConfig();
                models.Mailaddress = txtMailaddress.Text;
                models.Password = Maticsoft.Common.DEncrypt.DESEncrypt.Encrypt(txtPassword.Text);
                models.SMTPPort = Convert.ToInt32(txtSMTPPort.Text);
                models.SMTPServer = txtSMTPServer.Text;
                models.SMTPSSL = chkSMTPSSL.Checked;
                models.Username = txtUsername.Text;
                if (this.CurrentUser != null)
                {
                    models.UserID = this.CurrentUser.UserID;
                }
                Maticsoft.BLL.MailConfig blls = new Maticsoft.BLL.MailConfig();
                if (!bll.Exists(models.UserID, models.Mailaddress))
                {
                    bll.Add(models);
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                }
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            BoundData();
        }
    }
}