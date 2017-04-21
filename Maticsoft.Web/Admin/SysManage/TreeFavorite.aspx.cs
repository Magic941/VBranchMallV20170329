using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class TreeFavorite : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 212; } } //系统管理_定制菜单页
        protected int Act_TreeFavorite = 191;//定制快捷菜单
        //AccountsPrincipal accountsPrincipal;
        Maticsoft.BLL.SysManage.SysTree smbll = new Maticsoft.BLL.SysManage.SysTree();
        Maticsoft.BLL.SysManage.TreeFavorite tfbll = new Maticsoft.BLL.SysManage.TreeFavorite();
        List<int> nodeidlist = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Safe
                if (!Context.User.Identity.IsAuthenticated) return;
                if (this.TreeType < 0) return;
                if (this.CurrentUser == null) return;

                //accountsPrincipal = new AccountsPrincipal(this.CurrentUser.UserName);
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_TreeFavorite)))
                {
                    btnSave.Visible = false;
                }

                nodeidlist = tfbll.GetNodeIDsByUser(this.CurrentUser.UserID);
                DataSet ds = smbll.GetTreeSonList(0, this.TreeType, UserPrincipal.PermissionsID);
                listMenus.DataSource = ds;
                listMenus.DataBind();
            }
        }

        public int TreeType
        {
            get { return Globals.SafeInt(Request.Params["TreeType"], -1); }
        }

        public void listMenus_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
             e.Item.ItemType == ListItemType.AlternatingItem)
            {
                object objNode = DataBinder.Eval(e.Item.DataItem, "NodeID");
                object objperm = DataBinder.Eval(e.Item.DataItem, "PermissionID");
                bool Enabled = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Enabled"));
                int permissionid = Convert.ToInt32(objperm);

                if (((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid))) && Enabled)
                {
                    TreeView treeNode = (TreeView)e.Item.FindControl("treeMenu");
                    DataSet ds = smbll.GetAllEnabledTreeByType(this.TreeType);
                    BindTreeView(treeNode, ds.Tables[0], (int)objNode);
                }
            }
        }

        //邦定根节点
        public void BindTreeView(TreeView treeview, DataTable dt, int rootNodeid)
        {
            DataRow[] drs = dt.Select("ParentID= " + rootNodeid);//　选出所有子节点	

            bool menuExpand = true;

            treeview.Nodes.Clear(); // 清空树

            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                string parentid = r["ParentID"].ToString();
                string location = r["Location"].ToString();
                string url = r["Url"].ToString();
                string imageurl = r["ImageUrl"].ToString();
                int permissionid = int.Parse(r["PermissionID"].ToString().Trim());
                bool Enabled = Convert.ToBoolean(r["Enabled"]);

                //权限控制菜单		
                if (((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid))) && Enabled) //绑定用户有权限的和没设权限的（即公开的菜单）
                {
                    TreeNode rootnode = new TreeNode();
                    rootnode.Text = text;
                    rootnode.Value = nodeid;
                    //rootnode.NodeData = nodeid;
                    rootnode.NavigateUrl = (url.ToLower().StartsWith("/admin/") ? "" : "/admin/") + url;
                    //rootnode.Target = framename;
                    rootnode.Expanded = menuExpand;
                    // rootnode.ImageUrl = (imageurl.ToLower().StartsWith("/admin/") ? "" : "/admin/") + imageurl;

                    if (nodeidlist.Contains(Convert.ToInt32(nodeid)))
                    {
                        rootnode.Checked = true;
                    }
                    else
                    {
                        rootnode.Checked = false;
                    }

                    treeview.Nodes.Add(rootnode);

                    int sonparentid = int.Parse(nodeid);// or =location
                    CreateNode(sonparentid, rootnode, dt);
                }
            }
        }

        //邦定任意节点
        public void CreateNode(int parentid, TreeNode parentnode, DataTable dt)
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
                bool Enabled = Convert.ToBoolean(r["Enabled"]);

                //权限控制菜单
                if (((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid))) && Enabled)
                {

                    TreeNode node = new TreeNode();
                    node.Text = text;
                    node.Value = nodeid;
                    //node.NodeData = nodeid;
                    node.NavigateUrl = (url.ToLower().StartsWith("/admin/") ? "" : "/admin/") + url;
                    //node.Target = TargetFrame;
                    // node.ImageUrl = (imageurl.ToLower().StartsWith("/admin/") ? "" : "/admin/") + imageurl;
                    node.Expanded = false;
                    int sonparentid = int.Parse(nodeid);// or =location

                    if (nodeidlist.Contains(Convert.ToInt32(nodeid)))
                    {
                        node.Checked = true;
                    }
                    else
                    {
                        node.Checked = false;
                    }

                    //if (parentnode == null)
                    //{
                    //    TreeView1.Nodes.Clear();
                    //    parentnode = new TreeNode();
                    //    TreeView1.Nodes.Add(parentnode);
                    //}
                    parentnode.ChildNodes.Add(node);
                    CreateNode(sonparentid, node, dt);
                }//endif

            }//endforeach		

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.CurrentUser == null) return;
            tfbll.DeleteByUser(this.CurrentUser.UserID);
            for (int i = 0; i < listMenus.Items.Count; i++)
            {
                TreeView treelist = (TreeView)listMenus.Items[i].FindControl("treeMenu");
                foreach (TreeNode node in treelist.CheckedNodes)
                {
                    int nodeid = Convert.ToInt32(node.Value);
                    tfbll.Add(this.CurrentUser.UserID, nodeid);
                }
            }

            lblTooltip.Text = Resources.Site.TooltipSaveOK;

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("addpi.aspx?TreeType=" + this.TreeType);
        }
    }
}
