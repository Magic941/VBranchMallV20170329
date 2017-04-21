using System;
using Maticsoft.Common;

namespace Maticsoft.Web.CMS.ClassType
{
    public partial class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
        protected override int Act_PageLoad { get { return 194; } } //CMS_类型管理_添加页面
        
        protected void Page_Load(object sender, EventArgs e)
        {
       
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtClassTypeName.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.CMS.ClassErrorCLassNameNull);
                return;
            }
            Maticsoft.Model.CMS.ClassType model = new Maticsoft.Model.CMS.ClassType();
            model.ClassTypeName = this.txtClassTypeName.Text;
            if (bll.Add(model))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "List.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}