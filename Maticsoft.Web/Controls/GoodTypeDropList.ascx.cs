
using System;

namespace Maticsoft.Web.Controls
{
    public partial class GoodTypeDropList : System.Web.UI.UserControl
    {
        public void Page_Load(object sender, EventArgs e)
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