using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.PhotoClass
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 251; } } //CMS_图片分类管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.ClassID != 0)
                {
                    ShowInfo(this.ClassID);
                }
            }
        }

        public int ClassID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }

        private void ShowInfo(int ClassID)
        {
            BLL.CMS.PhotoClass bll = new BLL.CMS.PhotoClass();
            Model.CMS.PhotoClass model = bll.GetModel(ClassID);
            this.txtClassName.Text = model.ClassName;
            this.txtSequence.Text = model.Sequence.ToString();
            this.ddlPhotoClass.SelectedValue = model.ParentId.ToString();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorClassNameNull);
                return;
            }
            if (!PageValidate.IsNumber(txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorOrderFormat);
                return;
            }
            BLL.CMS.PhotoClass bll = new BLL.CMS.PhotoClass();
            string ClassName = this.txtClassName.Text;
            int ParentId = 0;
            if (!string.IsNullOrWhiteSpace(this.ddlPhotoClass.SelectedValue))
            {
                ParentId = int.Parse(this.ddlPhotoClass.SelectedValue);
            }
            Model.CMS.PhotoClass classModel = bll.GetModel(ParentId);
            string Path = string.Empty;
            int Depth;
            if (classModel != null)
            {
                Path = classModel.Path + ParentId + "|";
                Depth = classModel.Depth.Value + 1;
            }
            else
            {
                Path = "0|";
                Depth = 1;
            }
            int Sequence = int.Parse(this.txtSequence.Text);
            
            Model.CMS.PhotoClass model = bll.GetModel(this.ClassID);
            model.ClassName = ClassName;
            model.Path = Path;
            model.Depth = Depth;
            model.ParentId = ParentId;
            model.Sequence = Sequence;

            if (bll.Update(model))
            {
                MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
            }
            else
            {
                MessageBox.ShowFailTip(this,Resources.Site.TooltipSaveError);
            }
        }
    }
}
