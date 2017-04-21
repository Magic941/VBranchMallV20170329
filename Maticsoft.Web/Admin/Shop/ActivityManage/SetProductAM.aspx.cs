using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;


namespace Maticsoft.Web.Admin.Shop.ActivityManage
{
    public partial class SetProductAM : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 559; } } //管理_设置应用商品页
        public int AMId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    AMId = Globals.SafeInt(Request.QueryString["id"], 0);
                }
            }
        }
    }
}