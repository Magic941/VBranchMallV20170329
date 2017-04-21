using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Maticsoft.Accounts.Bus;
using System.Text;
using System.Collections;
using Maticsoft.Common;
namespace Maticsoft.Web.Enterprise
{
    public partial class Left : PageBaseEnterprise
    {
        public string strMenuTree = "";
        public string NodeName = "";
        bool MenuExpanded = Globals.SafeBool(BLL.SysManage.ConfigSystem.GetValueByCache("MenuExpanded"), false);
        Hashtable TreeListofLang;
        Maticsoft.BLL.SysManage.MultiLanguage bllML = new BLL.SysManage.MultiLanguage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                if ((Request.Params["id"] != null) && (Request.Params["id"].ToString() != ""))
                {
                    string strLanguage = "zh-CN";
                    if (Session["language"] != null)
                    {
                        strLanguage = Session["language"].ToString();
                    }
                    TreeListofLang = bllML.GetHashValueListByLangCache("TreeText", strLanguage);
                    string id = Request.Params["id"];                   
                    Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
                    Maticsoft.Model.SysManage.SysNode model = sm.GetModelByCache(Convert.ToInt32(id));
                    NodeName = model.TreeText;
                    Page.Title = NodeName;
                    //0:admin后台 1:企业后台  2:代理商后台 3:用户后台
                    DataSet ds = sm.GetAllEnabledTreeByType(1);
                    LoadMenu(ds.Tables[0], model.NodeID);

                }
            }
        }

        public void LoadMenu(DataTable dt, int NodeId)
        {
            bool hasLevel3 = false;
            DataRow[] drs = dt.Select("ParentID= " + NodeId);

            if (drs.Length > 0)
            {
                string nodeid = drs[0]["NodeID"].ToString();
                DataRow[] drs3 = dt.Select("ParentID= " + nodeid);
                if (drs3.Length > 0)//it have level 3
                {
                    hasLevel3 = true;
                }
            }

            StringBuilder strtemp = new StringBuilder();
            if (!hasLevel3) //level=2
            {
                strtemp.Append("<ul class=\"open\">");
                strtemp.AppendFormat("<span class=\"span_open\">{0}</span>", NodeName);
                foreach (DataRow r in drs)
                {
                    string nodeid = r["NodeID"].ToString();
                    string text = r["TreeText"].ToString();
                    if (TreeListofLang[nodeid] != null)
                    {
                        text = TreeListofLang[nodeid].ToString();
                    }
                    string parentid = r["ParentID"].ToString();
                    string location = r["Location"].ToString();
                    string url = r["Url"].ToString();
                    string imageurl = r["ImageUrl"].ToString();
                    int permissionid = int.Parse(r["PermissionID"].ToString().Trim());
                    if ((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid)))
                    {
                        strtemp.AppendFormat("<li><a href=\"{0}\" target=\"mainFrame\">{1}</a></li>", url, text);
                    }
                }
                strtemp.Append("</ul>");
            }
            else
            {
                foreach (DataRow r in drs)
                {
                    string nodeid = r["NodeID"].ToString();
                    string text = r["TreeText"].ToString();
                    if (TreeListofLang[nodeid] != null)
                    {
                        text = TreeListofLang[nodeid].ToString();
                    }
                    string parentid = r["ParentID"].ToString();
                    string location = r["Location"].ToString();
                    string url = r["Url"].ToString();
                    string imageurl = r["ImageUrl"].ToString();
                    int permissionid = int.Parse(r["PermissionID"].ToString().Trim());

                    if ((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid)))
                    {
                        strtemp.Append("<ul class=\"open\">");
                        strtemp.AppendFormat("<span class=\"span_open\">{0}</span>", text);
                        DataRow[] drs3 = dt.Select("ParentID= " + nodeid);
                        strtemp.Append(LoadMenu3(drs3));
                        strtemp.Append("</ul>");
                    }
                }
            }

            strMenuTree = strtemp.ToString();
        }

        public string LoadMenu3(DataRow[] dr3)
        {
            StringBuilder strtemp = new StringBuilder();
            foreach (DataRow dr in dr3)
            {
                string nodeid = dr["NodeID"].ToString();
                string text = dr["TreeText"].ToString();
                if (TreeListofLang[nodeid] != null)
                {
                    text = TreeListofLang[nodeid].ToString();
                }
                string parentid = dr["ParentID"].ToString();
                string location = dr["Location"].ToString();
                string url = dr["Url"].ToString();
                string imageurl = dr["ImageUrl"].ToString();
                int permissionid = int.Parse(dr["PermissionID"].ToString().Trim());

                if ((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid)))
                {
                    strtemp.Append("<li><a href=\"" + url + "\" target=\"mainFrame\">" + text + "</a></li>");
                }
            }
            return strtemp.ToString();

        }
    }
}