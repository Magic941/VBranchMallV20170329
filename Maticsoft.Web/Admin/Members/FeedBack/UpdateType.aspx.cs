using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.FeedBack
{
    public partial class UpdateType : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 281; } } 
        Maticsoft.BLL.Members.FeedbackType typeBll = new BLL.Members.FeedbackType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        protected int Id
        {
            get
            {
                int id = 0;
                string strId = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strId))
                {
                    id = Globals.SafeInt(strId, 0);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Members.FeedbackType model = typeBll.GetModel(Id);
            if (null != model)
            {
                this.txtName.Text = model.TypeName;
                this.txtDesc.Text = model.Description;
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string TypeName = this.txtName.Text.Trim();
            if (TypeName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "类型名称不能为空！");
                return;
            }
            Maticsoft.Model.Members.FeedbackType typeModel = typeBll.GetModel(Id);
            if (null != typeModel)
            {
                typeModel.TypeName = TypeName;
                typeModel.Description = this.txtDesc.Text;

                if (typeBll.Update(typeModel))
                {
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "TypeList.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                }
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TypeList.aspx");
        }
    }
}
