using System;
using System.Web.UI;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 213; } } 
        public string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                id = Request.Params["id"];
                if (id == null || id.Trim() == "")
                {
                    Response.Redirect("treelist.aspx?TreeType=" + this.TreeType);
                    Response.End();
                }

                Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
                SysNode node = sm.GetNode(int.Parse(id));
                lblID.Text = id;
                this.lblOrderid.Text = node.OrderID.ToString();
                lblName.Text = node.TreeText;
                if (node.ParentID == 0)
                {
                    this.lblTarget.Text = Resources.Site.lblRootDirectory;
                }
                else
                {
                    lblTarget.Text = sm.GetNode(node.ParentID).TreeText;
                }
                lblUrl.Text = node.Url;
                Image1.ImageUrl = node.ImageUrl;
                Maticsoft.Accounts.Bus.Permissions perm = new Maticsoft.Accounts.Bus.Permissions();
                if (node.PermissionID == -1)
                {
                    this.lblPermission.Text =Resources.SysManage.lblPermissionText;
                }
                else
                {
                    this.lblPermission.Text = perm.GetPermissionName(node.PermissionID);
                }

                //菜单类型
                switch (node.TreeType)
                {
                    case 0:
                        this.lblTreeType.Text =Resources.SysManage.dropBackendSystem;
                        break;
                    case 1:
                        this.lblTreeType.Text =Resources.SysManage.dropBackendEnterprise;
                        break;
                    case 2:
                        this.lblTreeType.Text =Resources.SysManage.dropBackendAgent;
                        break;
                    case 3:
                        this.lblTreeType.Text =Resources.SysManage.dropBackendUser;
                        break;
                    default:
                        break;
                }
                //是否启用菜单
                lblEnable.Text = node.Enabled ?Resources.SysManage.lblEnableTrue:Resources.SysManage.lblEnableFalse;
                lblDescription.Text = node.Comment;               

            }
        }

        public int TreeType
        {
            get { return Globals.SafeInt(Request.Params["TreeType"], -1); }
        }

        protected void btnCancle_Click(object sender, System.EventArgs e)
        {                      
            Response.Redirect("treelist.aspx?TreeType=" + this.TreeType);
        }
    }
}
