using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin
{
    public partial class ErrorPage : PageBaseAdmin
    {
        public string ErrorMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ErrorMsg"] != null)
            {
                ErrorMessage = Session["ErrorMsg"].ToString();
                Session["ErrorMsg"] = null;
                Server.ClearError(); 
            }
        }
    }
}