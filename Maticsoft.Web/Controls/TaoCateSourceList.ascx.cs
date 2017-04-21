using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Controls
{
    public partial class TaoCateSourceList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string SelectedValue
        {
            get { return hfSelectedNode.Value; }
            set { hfSelectedNode.Value = value; }
        }

        public bool IsNull;
    }
}