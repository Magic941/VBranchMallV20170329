using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Common;

namespace Maticsoft.Web.Admin.JLT.UserAttendance
{
    public partial class Panel : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int ClassID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["ClassID"];
                if (!string.IsNullOrEmpty(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }
    }
}