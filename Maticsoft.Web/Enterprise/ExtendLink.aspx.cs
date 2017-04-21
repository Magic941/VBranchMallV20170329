using System;

namespace Maticsoft.Web.Enterprise
{
    public partial class ExtendLink : PageBaseEnterprise
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentUser.DepartmentID == "-1")
                {
                    //Response.Redirect("applyFailed.htm");
                }
                this.txtUrl.Text = Request.Url.Authority + "/ExtendRegion_" + CurrentUser.DepartmentID;
            }
        }
    }
}