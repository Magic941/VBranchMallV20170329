using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Enterprise
{
    public partial class Top : PageBaseEnterprise
    {
        public string username = "";
        public string strMenu = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (TreeType > -1) return;
                username = CurrentUser.TrueName;
                Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
                //0:admin后台 1:企业后台  2:代理商后台 3:用户后台
                DataSet ds = sm.GetEnabledTreeByParentId(0, 1, true);
                LoadTree(ds.Tables[0]);
            }
        }

        public int TreeType
        {
            get { return Globals.SafeInt(Request.Params["TreeType"], -1); }
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
                    strtemp.Append("<li id=\"Tab" + n.ToString() + "\">");
                    strtemp.Append("<a href=\"left.aspx?id=" + nodeid + "\" target=\"leftFrame\" onclick=\"javascript:switchTab('TabPage1','Tab" + n.ToString() + "');\">" + text);
                    strtemp.Append("</a></li>");
                }
                n++;

            }
            strMenu = strtemp.ToString();


        }
    }
}