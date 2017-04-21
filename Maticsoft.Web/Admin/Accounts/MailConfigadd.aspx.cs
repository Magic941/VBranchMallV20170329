using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Accounts
{
    public partial class MailConfigadd : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override int Act_PageLoad { get { return 206; } } 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.MailConfig model = new Maticsoft.Model.MailConfig();
            model.Mailaddress = txtMailaddress.Text;
            model.Password = txtPassword.Text;
            model.POPPort = Convert.ToInt32(txtPOPPort.Text);
            model.POPServer = txtPOPServer.Text;
            model.POPSSL = chkPOPSSL.Checked;
            model.SMTPPort = Convert.ToInt32(txtSMTPPort.Text);
            model.SMTPServer = txtSMTPServer.Text;
            model.SMTPSSL = chkSMTPSSL.Checked;
            model.Username = txtUsername.Text;
            if (this.CurrentUser != null)
            {
                model.UserID = this.CurrentUser.UserID;
            }
            Maticsoft.BLL.MailConfig bll = new Maticsoft.BLL.MailConfig();
            if (!bll.Exists(model.UserID, model.Mailaddress))
            {
                bll.Add(model);
                Response.Redirect("mailconfiglist.aspx");
            }
            else
            {
                lblInfo.Visible = true;
                lblInfo.Text = "This account already exists";
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("MailConfiglist.aspx");
        }
    }
}