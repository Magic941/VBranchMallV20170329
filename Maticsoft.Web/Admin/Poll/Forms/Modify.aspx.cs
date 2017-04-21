using System;

namespace Maticsoft.Web.Forms
{
    public partial class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Poll.Forms bll = new Maticsoft.BLL.Poll.Forms();
        protected override int Act_PageLoad { get { return 355; } } //客服管理_问卷管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            //Master.TabTitle = "信息添加，请详细填写下列信息";
            if (!IsPostBack)
            {
                if (Fid > 0)
                {
                    BindData(Fid);
                }
            }
        }

        private void BindData(int fid)
        {
            Model.Poll.Forms model = bll.GetModel(fid);
            if (model != null)
            {
                this.txtName.Text = model.Name;
                this.txtDescription.Text = model.Description;
                this.chkIsActive.Checked = model.IsActive;
            }
        }

        public int Fid
        {
            get
            {
                int fid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["fid"]))
                {
                    fid = Common.Globals.SafeInt(Request.Params["fid"], 0);
                }
                return fid;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = false;
            this.btnCancle.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                this.btnAdd.Enabled = true;
                this.btnCancle.Enabled = true;
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Poll.ErrorFormsNameNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                this.btnAdd.Enabled = true;
                this.btnCancle.Enabled = true;
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Poll.ErrorFormsExplainNull);
                return;
            }
            string Name = this.txtName.Text;
            string Description = this.txtDescription.Text;

            Maticsoft.Model.Poll.Forms model = new Maticsoft.Model.Poll.Forms();
            model.Name = Name;
            model.Description = Description;
            model.FormID = Fid;
            model.IsActive = this.chkIsActive.Checked;

            int id = bll.Update(model);
            if (id > 0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "index.aspx");
            }
            else
            {
                this.btnAdd.Enabled = true;
                this.btnCancle.Enabled = true;
                Maticsoft.Common.MessageBox.ShowFailTip(this, "保存失败！");
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}