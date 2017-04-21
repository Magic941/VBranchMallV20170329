using System;
using System.Web.UI;
using Maticsoft.Common;
namespace Maticsoft.Web.CMS.ClassType
{
    public partial class Modify : PageBaseAdmin
    {
        Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
        protected override int Act_PageLoad { get { return 216; } }   //CMS_类型管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ClassTypeID >= 0)
                {
                    ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent, "List.aspx");
                }
            }
        }
        public int ClassTypeID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }


        private void ShowInfo()
        {
            Maticsoft.Model.CMS.ClassType model = bll.GetModelByCache(ClassTypeID);
            if (null != model)
            {
                this.lblClassTypeID.Text = model.ClassTypeID.ToString();
                this.txtClassTypeName.Text = model.ClassTypeName;
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtClassTypeName.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.CMS.ClassErrorNameNotNull);
                return;
            }

            Maticsoft.Model.CMS.ClassType model = new Maticsoft.Model.CMS.ClassType();
            model.ClassTypeID = Globals.SafeInt(lblClassTypeID.Text, 0);
            model.ClassTypeName = txtClassTypeName.Text;

            if (bll.Update(model))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "List.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError, "List.aspx");
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}
