using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Accounts.Bus;
using System.Configuration;
using System.Text;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin
{
    public partial class Left : PageBaseAdmin
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
                    DataSet ds = sm.GetAllTree();
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

            //string menuCssClass = "guideexpand";
            //if (!MenuExpanded)
            //{
            //    menuCssClass = "guidecollapse";
            //}

            StringBuilder strtemp = new StringBuilder();
            if (!hasLevel3) //level=2
            {
                strtemp.Append("<div style=\"height: 5px\"></div>");                
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
                        strtemp.AppendFormat("<div class=\"leftothermenu\"><a href=\"{0}\" target=\"mainFrame\">{1}</a></div>", url, text);
                    }
                }                
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
                        strtemp.AppendFormat("<span class=\"span_open\">{0}</span>", text);//menuCssClass
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
                    strtemp.AppendFormat("<li><a href=\"{0}\" target=\"mainFrame\">{1}</a></li>", url, text);
                }
            }
            return strtemp.ToString();

        }



        /*
        protected System.Web.UI.WebControls.Label lblMsg;        
        public string strWelcome;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                if (Session[Maticsoft.Common.Globals.SESSIONKEY_ADMIN] == null)
                {
                    return;
                }                
                Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
                DataSet ds = sm.GetTreeList("");
                BindTreeView("mainFrame", ds.Tables[0]);
                //if (this.TreeView1.Nodes.Count == 0)
                //{
                //    strWelcome += "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;但你没有任何模块的访问权";
                //}

            }
        }

        //邦定根节点
        public void BindTreeView(string TargetFrame, DataTable dt)
        {
            DataRow[] drs = dt.Select("ParentID= " + 0);//　选出所有子节点	

            //菜单状态
            string MenuExpanded = ConfigurationManager.AppSettings.Get("MenuExpanded");
            bool menuExpand = bool.Parse(MenuExpanded);

            TreeView1.Nodes.Clear(); // 清空树
            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                string parentid = r["ParentID"].ToString();
                string location = r["Location"].ToString();
                string url = r["Url"].ToString();
                string imageurl = r["ImageUrl"].ToString();
                int permissionid = int.Parse(r["PermissionID"].ToString().Trim());
                string framename = TargetFrame;

                //treeview set
                this.TreeView1.Font.Name = "宋体";
                this.TreeView1.Font.Size = FontUnit.Parse("9");

                //权限控制菜单		
                if ((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid)))//绑定用户有权限的和没设权限的（即公开的菜单）
                {
                    TreeNode rootnode = new TreeNode();
                    rootnode.Text = text;
                    rootnode.Value = nodeid;
                    rootnode.NavigateUrl = url;
                    rootnode.Target = framename;
                    rootnode.Expanded = menuExpand;
                    rootnode.ImageUrl = imageurl;
                    rootnode.SelectAction = TreeNodeSelectAction.SelectExpand;

                    TreeView1.Nodes.Add(rootnode);

                    int sonparentid = int.Parse(nodeid);// or =location
                    CreateNode(framename, sonparentid, rootnode, dt);
                }
            }
        }

        //邦定任意节点
        public void CreateNode(string TargetFrame, int parentid, TreeNode parentnode, DataTable dt)
        {
            DataRow[] drs = dt.Select("ParentID= " + parentid);//选出所有子节点			
            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                string location = r["Location"].ToString();
                string url = r["Url"].ToString();
                string imageurl = r["ImageUrl"].ToString();
                int permissionid = int.Parse(r["PermissionID"].ToString().Trim());
                string framename = TargetFrame;

                //权限控制菜单
                if ((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid)))
                {
                    TreeNode node = new TreeNode();
                    node.Text = text;
                    node.Value = nodeid;
                    node.NavigateUrl = url;
                    node.Target = TargetFrame;
                    node.ImageUrl = imageurl;
                    node.SelectAction = TreeNodeSelectAction.SelectExpand;
                    int sonparentid = int.Parse(nodeid);// or =location

                    if (parentnode == null)
                    {
                        TreeView1.Nodes.Clear();
                        parentnode = new TreeNode();
                        TreeView1.Nodes.Add(parentnode);
                    }
                    parentnode.ChildNodes.Add(node);
                    CreateNode(framename, sonparentid, node, dt);
                }//endif

            }//endforeach		

        }
        */


    }
}