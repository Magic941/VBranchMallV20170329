using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.WholeSale
{
    public partial class SalesProduct : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 559; } } //Shop_批发规则管理_设置应用商品页
        public int RuleId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    RuleId = Globals.SafeInt(Request.QueryString["id"], 0);
                }
            }
        }
    }
}