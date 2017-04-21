using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Maticsoft.Web.Admin.Ms.EmailTemplet
{
    public partial class UpdateTemplet : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 312; } } //设置_邮件模版管理_编辑页
        Maticsoft.BLL.Ms.EmailTemplet bll = new BLL.Ms.EmailTemplet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Maticsoft.Model.Ms.EmailTemplet model = bll.GetModel(Type);
                if (model == null)
                {
                    Response.Redirect("TempletList.aspx");
                }
                this.txtBody.Text = model.EmailBody;
                this.txtDescription.Text = model.EmailDescription;
               this.ddlType.SelectedValue= model.EmailType;
                this.txtSubject.Text = model.EmailSubject;
            }
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            Maticsoft.Model.Ms.EmailTemplet model = bll.GetModel(Type);
            if (null != model)
            {
                model.EmailSubject = this.txtSubject.Text;
                model.EmailDescription = this.txtDescription.Text;
                model.EmailBody = this.txtBody.Text;

                if (bll.Update(model))
                {
                    Response.Redirect("TempletList.aspx");
                }
                else
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = "编辑邮件模板出错！";
                }
            }
        }
        public int Type
        {
            get
            {
                int _type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    _type =Common.Globals.SafeInt( Request.Params["type"],0);
                }
                return _type;
            }
        }
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TempletList.aspx");
        }
    }
}