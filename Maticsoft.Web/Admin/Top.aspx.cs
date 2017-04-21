using System;
using System.Data;
using System.Text;
namespace Maticsoft.Web.Admin
{
    public partial class Top : PageBaseAdmin
    {
        public string username = "";
        public string strMenu = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            username = CurrentUser.TrueName;
            if (!IsPostBack)
            {

                Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
                DataSet ds = sm.GetTreeList("ParentID=0");
                LoadTree(ds.Tables[0]);
            }
        }

        public void LoadTree(DataTable dt)
        {
            int n = 1;
            StringBuilder strtemp = new StringBuilder();
            string url = "Left.aspx";
            foreach (DataRow r in dt.Rows)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                //if (TreeListofLang[nodeid] != null)
                //{
                //    text = TreeListofLang[nodeid].ToString();
                //}
                string parentid = r["ParentID"].ToString();
                string location = r["Location"].ToString();
                if (r["Url"] != null && r["Url"].ToString().Length > 0)
                {
                    url = r["Url"].ToString();
                }
                string imageurl = r["ImageUrl"].ToString();
                int permissionid = int.Parse(r["PermissionID"].ToString().Trim());

                if ((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid)))
                {
                    strtemp.Append("<li id=\"Tab"+n.ToString()+"\">");
                    strtemp.Append("<a href=\"left.aspx?id=" + nodeid + "\" target=\"leftFrame\" onclick=\"javascript:switchTab('TabPage1','Tab" + n.ToString() + "');\">" + text);                    
                    strtemp.Append("</a></li>");
                }
                n++;

            }
            strMenu = strtemp.ToString();


        }
    }
}