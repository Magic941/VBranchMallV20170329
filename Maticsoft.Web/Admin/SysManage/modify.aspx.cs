using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
namespace Maticsoft.Web.Admin.SysManage
{
    public partial class modify : PageBaseAdmin
    {
        Maticsoft.BLL.SysManage.MultiLanguage bllML = new Maticsoft.BLL.SysManage.MultiLanguage();
        public string id = "";
        protected override int Act_PageLoad { get { return 210; } } 
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Safe
               // if (this.TreeType < 0) return;

                //得到现有菜单
                BiudTree(this.TreeType);

                drpTreeType.SelectedValue = this.TreeType.ToString();
                drpTreeType.Enabled = false;

                ////绑定模块列表
                //BindModule();
                //BindModuledept();
                //BindDept();
                BindImages();
                if ((Request.Params["id"] != null) && (Request.Params["id"].ToString() != ""))
                {
                    id = Request.Params["id"];
                    if (id == null || id.Trim() == "")
                    {
                        Response.Redirect("treelist.aspx?TreeType=" + this.TreeType);
                        Response.End();
                    }
                    else
                    {
                        ShowInfo(id);
                    }
                }
            }

        }

        public int TreeType
        {
            get { return Globals.SafeInt(Request.Params["TreeType"], -1); }
        }

        private void ShowInfo(string id)
        {
            Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
            SysNode node = sm.GetNode(int.Parse(id));

            this.lblID.Text = id;
            this.txtOrderid.Text = node.OrderID.ToString();
            this.txtTreeText.Text = node.TreeText;
            //menu
            if (node.ParentID == 0)
            {
                this.listTarget.SelectedIndex = 0;
            }
            else
            {
                for (int m = 0; m < this.listTarget.Items.Count; m++)
                {
                    if (this.listTarget.Items[m].Value == node.ParentID.ToString())
                    {
                        this.listTarget.Items[m].Selected = true;
                    }
                }
            }
            this.txtUrl.Text = node.Url;
            //this.txtImgUrl.Text=node.ImageUrl;
            this.txtDescription.Text = node.Comment;

            //Permission
            this.UCDroplistPermission1.PermissionID = node.PermissionID;

            ////module
            //for (int n = 0; n < this.dropModule.Items.Count; n++)
            //{
            //    if (this.dropModule.Items[n].Value == node.ModuleID.ToString())
            //    {
            //        this.dropModule.Items[n].Selected = true;
            //    }
            //}

            ////module
            //for (int n = 0; n < this.Dropdepart.Items.Count; n++)
            //{
            //    if (this.Dropdepart.Items[n].Value == node.KeShiDM.ToString())
            //    {
            //        this.Dropdepart.Items[n].Selected = true;
            //    }
            //}

            //image
            for (int n = 0; n < this.imgsel.Items.Count; n++)
            {
                if (this.imgsel.Items[n].Value == node.ImageUrl)
                {
                    this.imgsel.Items[n].Selected = true;
                    this.hideimgurl.Value = node.ImageUrl;
                }
            }
            //			if(node.KeshiPublic=="true")
            //			{
            //				this.chkPublic.Checked=true;
            //			}

            drpTreeType.SelectedValue = node.TreeType.ToString();
            chkEnable.Checked = node.Enabled;
        }

       


        #region

        //菜单
        private void BiudTree(int treeType)
        {            
            //			Maticsoft.BLL.SysManage.SysTree sm=new Maticsoft.BLL.SysManage.SysTree();			
            //			DataTable dt=sm.GetTreeList("").Tables[0];
            //
            //
            //			this.listTarget.Items.Clear();
            //			//加载树
            //			this.listTarget.Items.Add(new ListItem(Resources.Site.lblRootDirectory,"0"));
            //			DataRow [] drs = dt.Select("ParentID= " + 0);			
            //			foreach( DataRow r in drs )
            //			{
            //				string nodeid=r["NodeID"].ToString();				
            //				string text=r["TreeText"].ToString();					
            //				string parentid=r["ParentID"].ToString();
            //				string permissionid=r["PermissionID"].ToString();
            //				text="╋"+text;				
            //				this.listTarget.Items.Add(new ListItem(text,nodeid));
            //				int sonparentid=int.Parse(nodeid);
            //				BindNode( sonparentid, dt);
            //
            //			}	
            //			this.listTarget.DataBind();	
            //		


            Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
            DataTable dt;
            if (treeType > -1)
            {
                dt = sm.GetTreeList("TreeType = " + treeType).Tables[0];
            }
            else
            {
                dt = sm.GetTreeList("").Tables[0];
            }


            this.listTarget.Items.Clear();
            //加载树
            this.listTarget.Items.Add(new ListItem(Resources.Site.lblRootDirectory, "0"));
            DataRow[] drs = dt.Select("ParentID= " + 0);
            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                //string parentid=r["ParentID"].ToString();
                //string permissionid=r["PermissionID"].ToString();
                text = "╋" + text;
                this.listTarget.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank = "├";

                BindNode(sonparentid, dt, blank);

            }
            this.listTarget.DataBind();


        }

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            DataRow[] drs = dt.Select("ParentID= " + parentid);

            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                //string permissionid=r["PermissionID"].ToString();
                text = blank + "『" + text + "』";

                this.listTarget.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank2 = blank + "─";


                BindNode(sonparentid, dt, blank2);
            }
        }

        //
        //		private void BindNode(int parentid,DataTable dt)
        //		{
        //			DataRow [] drs = dt.Select("ParentID= " + parentid );			
        //			foreach( DataRow r in drs )
        //			{
        //				string nodeid=r["NodeID"].ToString();				
        //				string text=r["TreeText"].ToString();					
        //				string permissionid=r["PermissionID"].ToString();
        //				text="  "+"├『"+text+"』";			
        //				this.listTarget.Items.Add(new ListItem(text,nodeid));
        //				int sonparentid=int.Parse(nodeid);
        //				BindNode( sonparentid, dt);
        //			}
        //		}
        

        //		private void BindModule()
        //		{
        //			Maticsoft.BLL.SysManage.SysTree sm=new Maticsoft.BLL.SysManage.SysTree();
        //			DataSet ds=sm.GetTypeModules("function");
        //			this.dropModule.DataSource=ds;
        //			this.dropModule.DataTextField="Description";
        //			this.dropModule.DataValueField="ModuleID";
        //			this.dropModule.DataBind();
        //			this.dropModule.Items.Insert(0,Resources.Site.PleaseSelect);
        //		}

        //		private void BindModuledept()
        //		{
        //			Maticsoft.BLL.SysManage.SysTree sm=new Maticsoft.BLL.SysManage.SysTree();
        //			DataSet ds=sm.GetTypeModules("dept");
        //			this.dropModuleDept.DataSource=ds;
        //			this.dropModuleDept.DataTextField="Description";
        //			this.dropModuleDept.DataValueField="ModuleID";
        //			this.dropModuleDept.DataBind();
        //			this.dropModuleDept.Items.Insert(0,Resources.Site.PleaseSelect);
        //		}

        //		private void BindDept()
        //		{
        //			DataSet ds=Maticsoft.BLL.PubConstant.GetAllKeShi();
        //			this.Dropdepart.DataSource=ds.Tables[0].DefaultView;
        //			this.Dropdepart.DataTextField="KeShimch";
        //			this.Dropdepart.DataValueField="KeShidm";
        //			this.Dropdepart.DataBind();
        //			this.Dropdepart.Items.Insert(0,Resources.Site.PleaseSelect);

        //		}

        private void BindImages()
        {
            string dirpath = Server.MapPath("../Images/MenuImg");
            DirectoryInfo di = new DirectoryInfo(dirpath);
            FileInfo[] rgFiles = di.GetFiles("*.gif");
            this.imgsel.Items.Clear();
            foreach (FileInfo fi in rgFiles)
            {
                ListItem item = new ListItem(fi.Name, "Images/MenuImg/" + fi.Name);
                this.imgsel.Items.Add(item);
            }
            FileInfo[] rgFiles2 = di.GetFiles("*.jpg");
            foreach (FileInfo fi in rgFiles2)
            {
                ListItem item = new ListItem(fi.Name, "Images/MenuImg/" + fi.Name);
                this.imgsel.Items.Add(item);
            }
            this.imgsel.Items.Insert(0, Resources.Site.lblDefaultIcon);
            this.imgsel.DataBind();
        }


        #endregion

      

        protected void btnSave_Click(object sender, System.EventArgs e)
        {

            string id = Maticsoft.Common.PageValidate.InputText(this.lblID.Text, 10);
            string orderid = Maticsoft.Common.PageValidate.InputText(this.txtOrderid.Text, 5);
            string treeText = txtTreeText.Text;
            string url = Maticsoft.Common.PageValidate.InputText(txtUrl.Text, 100);
            //string imgUrl=Maticsoft.Common.PageValidate.InputText(txtImgUrl.Text,100);
            string imgUrl = this.hideimgurl.Value;
            string target = this.listTarget.SelectedValue;
            int parentid = int.Parse(target);

            string strErr = "";

            if (orderid.Trim() == "")
            {
                strErr += Resources.SysManage.ErrorIDNotNull+"\\n";
            }
            try
            {
                int.Parse(orderid);
            }
            catch
            {
                strErr += Resources.SysManage.ErrorIDFormalError+"\\n";
            }
            if (treeText.Trim() == "")
            {
                strErr +=Resources.SysManage.ErrorNameNotNull+ "\\n";
            }
            if (strErr != "")
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, strErr);
                return;
            }

            int permission_id = -1;
            if (UCDroplistPermission1.PermissionID > 0)
            {
                permission_id = UCDroplistPermission1.PermissionID; // int.Parse(this.listPermission.SelectedValue);
            }
            int moduleid = -1;

            //if (this.dropModule.SelectedIndex > 0)
            //{
            //    moduleid = int.Parse(this.dropModule.SelectedValue);
            //}
            //int moduledeptid = -1;
            //if (this.dropModuleDept.SelectedIndex > 0)
            //{
            //    moduledeptid = int.Parse(this.dropModuleDept.SelectedValue);
            //}
            int keshidm = -1;
            //if (this.Dropdepart.SelectedIndex > 0)
            //{
            //    keshidm = int.Parse(this.Dropdepart.SelectedValue);
            //}
            string keshipublic = "false";

            //if (this.chkPublic.Checked)
            //{
            //    keshipublic = "true";
            //}
            string comment = Maticsoft.Common.PageValidate.InputText(txtDescription.Text, 100);
            
            Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
            SysNode node = sm.GetNode(Globals.SafeInt(id, 0));
            node.OrderID = int.Parse(orderid);
            node.TreeText = treeText;
            node.ParentID = parentid;
            node.Location = parentid + "." + orderid;
            node.Comment = comment;
            node.Url = url.Replace(@"\", "/");
            node.PermissionID = permission_id;
            node.ImageUrl = imgUrl;
            node.ModuleID = moduleid;
            node.KeShiDM = keshidm;
            node.KeshiPublic = keshipublic;
            node.TreeType = Globals.SafeInt(drpTreeType.SelectedValue, 0);
            node.Enabled = chkEnable.Checked;
            sm.UpdateNode(node);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑菜单：【{0}】", txtTreeText.Text), this);
            Response.Redirect("show.aspx?id=" + id + "&TreeType=" + this.TreeType);
        }


        protected void btnCancle_Click(object sender, System.EventArgs e)
        {
            lblID.Text = "";
            txtTreeText.Text = "";
            txtUrl.Text = "";
            txtDescription.Text = "";
            Response.Redirect("show.aspx?id=" + id + "&TreeType=" + this.TreeType);
        }

    }
}
