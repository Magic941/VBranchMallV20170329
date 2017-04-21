using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Maticsoft.Web.Admin.Ms.EmailTemplet
{
    public partial class AddTemplet : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 311; } } //设置_邮件模版管理_添加页
        Maticsoft.BLL.Ms.EmailTemplet bll = new BLL.Ms.EmailTemplet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            Maticsoft.Model.Ms.EmailTemplet model = new Model.Ms.EmailTemplet();
            model.EmailType = this.ddlType.SelectedValue;
            model.EmailPriority = -1;
            model.EmailSubject = this.txtSubject.Text;
            model.EmailDescription = this.txtDescription.Text;
            model.EmailBody = this.txtBody.Text;
            if (bll.Add(model)>0)
            {
                Response.Redirect("TempletList.aspx");
            }
            else
            {
                this.lblMsg.ForeColor = Color.Red;
                this.lblMsg.Text = "添加邮件模板出错！";
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TempletList.aspx");
        }
    }
}
