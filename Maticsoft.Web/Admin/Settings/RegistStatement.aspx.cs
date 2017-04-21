using System;
using Maticsoft.Model.SysManage;
using Maticsoft.Common;
using System.Collections;

namespace Maticsoft.Web.Admin.Settings
{
    public partial class RegistStatement : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 160; } }
        protected new int Act_UpdateData = 161;
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

        BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

        public void BoundData()
        {
            this.txtContent.Text = Globals.HtmlDecode(WebSiteSet.RegistStatement);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //try
            //{
                string txtcontent = this.txtContent.Text.Trim();
                txtcontent = txtcontent.Replace("\n", "");
                WebSiteSet.RegistStatement = Globals.HtmlDecode(txtcontent);
                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);
                this.btnSave.Enabled = false;
                this.btnReset.Enabled = false;
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "RegistStatement.aspx");
            //}
            //catch
            //{
            //    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "RegistStatement.aspx");
            //}
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundData();
        }

       


    }

    
}