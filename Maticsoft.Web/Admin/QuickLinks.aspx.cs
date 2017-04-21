using System;
using System.Data;
using System.Text;
using System.Web.UI;
namespace Maticsoft.Web.Admin
{
    public partial class QuickLinks : PageBaseAdmin
    {
        public string FavoriteMenu = "";
        public string ShortcutMenu = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Context.User.Identity.IsAuthenticated) return;
                if (this.CurrentUser != null)
                {
                    Maticsoft.BLL.SysManage.TreeFavorite sm = new Maticsoft.BLL.SysManage.TreeFavorite();
                    DataSet ds = sm.GetMenuListByUser(this.CurrentUser.UserID);
                    LoadMenu(ds.Tables[0]);
                }

            }
        }


        public void LoadMenu(DataTable dt)
        {
            StringBuilder strtemp = new StringBuilder();
            DataRow[] rows = dt.Select("", "OrderID");
            foreach (DataRow r in rows)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                string url = r["Url"].ToString();
                //string imageurl = r["ImageUrl"].ToString();
                strtemp.AppendFormat("<li><a href=\"{0}\" target=\"mainFrame\">{1}</a></li>", url, text);

            }
            FavoriteMenu = strtemp.ToString();
        }


    }
}
