using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.PhotoClass
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 250; } } //CMS_图片分类管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.CMS.PhotoClass classBll = new BLL.CMS.PhotoClass();
                txtSequence.Text = classBll.GetMaxSequence().ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
		{
            BLL.CMS.PhotoClass classBll = new BLL.CMS.PhotoClass();

            if (string.IsNullOrWhiteSpace(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorClassNameNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorClassNameNull);
                return;
            }

            if (classBll.ExistsByClassName(this.txtClassName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorClassRepeat);
                return;
			}
			if(!PageValidate.IsNumber(txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMSPhoto.ErrorOrderFormat);
                return;
			}
            
			string ClassName=this.txtClassName.Text;
            int ParentId = 0;
            if (!string.IsNullOrWhiteSpace(ddlPhotoClass.SelectedValue))
            {
                ParentId = int.Parse(this.ddlPhotoClass.SelectedValue);
           }
           
			int Sequence=int.Parse(this.txtSequence.Text);
            Model.CMS.PhotoClass classModel = classBll.GetModel(ParentId);
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
            
            Model.CMS.PhotoClass model = new Model.CMS.PhotoClass
                                               {
                                                   ClassName = ClassName,
                                                   ParentId = ParentId,
                                                   Sequence = Sequence,
                                                   Path = Path,
                                                   Depth = Depth
                                               };

            BLL.CMS.PhotoClass bll=new BLL.CMS.PhotoClass();
			bll.Add(model);
            MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
		}
    }
}
