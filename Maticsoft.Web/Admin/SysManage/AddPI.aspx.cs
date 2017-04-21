using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using System.Data;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class AddPI : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 212; } } //系统管理_定制菜单页
        Maticsoft.BLL.SysManage.TreeFavorite sm = new Maticsoft.BLL.SysManage.TreeFavorite();
        //List<int> nodeidlist = new List<int>();
        //public string FavoriteMenu = "";
        //public string ShortcutMenu = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Safe
                if (!Context.User.Identity.IsAuthenticated) return;
                if (this.TreeType < 0) return;

                if (this.CurrentUser != null)
                {
                    User currentUser = this.CurrentUser;
                    // Maticsoft.BLL.SysManage.TreeFavorite sm = new Maticsoft.BLL.SysManage.TreeFavorite();
                    DataSet ds = sm.GetMenuListByUser(currentUser.UserID);
                    listboxSysManage.DataSource = ds;
                    listboxSysManage.DataTextField = "TreeText";
                    listboxSysManage.DataValueField = "NodeID";
                    listboxSysManage.DataBind();
                }

            }
        }

        public int TreeType
        {
            get { return Globals.SafeInt(Request.Params["TreeType"], -1); }
        }

        protected void btnUP_Click(object sender, EventArgs e)
        {
            int i = listboxSysManage.SelectedIndex;
            if (i > 0)
            {
                string text = listboxSysManage.Items[i - 1].Text;
                string val = listboxSysManage.Items[i - 1].Value;
                listboxSysManage.Items[i - 1].Text = listboxSysManage.Items[i].Text;
                listboxSysManage.Items[i - 1].Value = listboxSysManage.Items[i].Value;
                listboxSysManage.Items[i].Text = text;
                listboxSysManage.Items[i].Value = val;
                listboxSysManage.SelectedIndex = i - 1;
            }
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {

            if (listboxSysManage.SelectedItem != null)
            {
                int i = listboxSysManage.SelectedIndex;
                if (i < listboxSysManage.Items.Count - 1)
                {
                    string text = listboxSysManage.Items[i + 1].Text;
                    string val = listboxSysManage.Items[i + 1].Value;
                    listboxSysManage.Items[i + 1].Text = listboxSysManage.Items[i].Text;
                    listboxSysManage.Items[i + 1].Value = listboxSysManage.Items[i].Value;
                    listboxSysManage.Items[i].Text = text;
                    listboxSysManage.Items[i].Value = val;
                    listboxSysManage.SelectedIndex = i + 1;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.CurrentUser == null) return;
            User currentUser = this.CurrentUser;
            int count = listboxSysManage.Items.Count;
            for (int i = 0; i < count; i++)
            {
                int NodeID = Convert.ToInt32(listboxSysManage.Items[i].Value);
                int OrderID = i + 1;
                sm.UpDate(OrderID, currentUser.UserID, NodeID);
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TreeFavorite.aspx?TreeType=" + this.TreeType);
        }
    }
}