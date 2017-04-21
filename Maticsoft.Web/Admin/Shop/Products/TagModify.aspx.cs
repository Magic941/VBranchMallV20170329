using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class TagModify : PageBaseAdmin
    {
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
        BLL.Shop.Products.ProductInfo manage = new BLL.Shop.Products.ProductInfo();
        BLL.Shop.Products.ProductCategories procatebll = new BLL.Shop.Products.ProductCategories();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string pid= Request.Params["pid"];
                if (pid.Trim()!="0")
                {
                    this.hidPid.Value = pid;
                    this.isedittype.SelectedValue = "0";
                }
                else
                {
                    this.isedittype.SelectedValue = "1";
                    this.isedittype.Enabled = false;
                }
                BindTree();
              
            }
        }
        protected int SaleStatus
        {
            get
            {
                int status = -1;
                if (!string.IsNullOrWhiteSpace(Request.Params["SaleStatus"]))
                {
                    status = Common.Globals.SafeInt(Request.Params["SaleStatus"], -1);
                }
                return status;
            }
        }
        #region 绑定菜单树
        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem(Resources.Site.All, ""));
            Maticsoft.BLL.Shop.Products.CategoryInfo bllCategoryInfo = new BLL.Shop.Products.CategoryInfo();
            DataSet ds = bllCategoryInfo.GetList("");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                DataTable dt = ds.Tables[0];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    DataRow[] dr = dt.Select("ParentCategoryId= " + 0);
                    foreach (DataRow item in dr)
                    {
                        string nodeid = item["CategoryId"].ToString();
                        string text = item["Name"].ToString();
                        string parentid = item["ParentCategoryId"].ToString();
                        text = "╋" + text;
                        this.dropParentID.Items.Add(new ListItem(text, nodeid));
                        int sonparentid = int.Parse(nodeid);
                        string blank = "├";
                        BindNode(sonparentid, dt, blank);
                    }
                }
            }
            this.dropParentID.DataBind();

        }
        private void BindNode(int parentid, DataTable dt, string blank)
        {
            DataRow[] dr = dt.Select("ParentCategoryId= " + parentid);
            foreach (DataRow item in dr)
            {
                string nodeid = item["CategoryId"].ToString();
                string text = item["Name"].ToString();
                text = blank + "『" + text + "』";
                this.dropParentID.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank2 = blank + "─";
                BindNode(sonparentid, dt, blank2);
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int flag=0;
            int updatetype =int.Parse(this.rbltag.SelectedValue); //数据库是追加还是覆盖
            if (this.isedittype.SelectedValue == "1")
            {
                if (this.dropParentID.SelectedValue == "")
                {
                    MessageBox.Show(this, "请选择商品类型.");
                    return;
                }
                 DataSet ds=procatebll.GetList("CategoryId=" + this.dropParentID.SelectedValue);
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     flag = manage.ChangeProductsTag(int.Parse(this.dropParentID.SelectedValue), Globals.SafeString(this.txttag.Text, ""),updatetype);
                     if (flag > 0)
                     {
                         if (this.SaleStatus == 1)
                         {
                             DataSet dsstatus = manage.GetProductsTagCategories(int.Parse(this.dropParentID.SelectedValue),this.SaleStatus);
                             for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                             {
                                 long ProductId = long.Parse(ds.Tables[0].Rows[i]["ProductId"].ToString());
                                 Maticsoft.BLL.Products.Lucene.ProductIndexManagerLocal.productIndex.Mod(ProductId);
                             }
                         }
                     }
                 }
                 else
                 {
                     MessageBox.Show(this, "此类型商品类型无产品");
                     return;
                 }

            }
            else
            {
                
                if (string.IsNullOrEmpty(this.hidPid.Value.Trim()))
                {
                    MessageBox.Show(this, "请选择商品");
                    return;
                }
                flag = manage.ChangeProductsTag(this.hidPid.Value.Trim(), Globals.SafeString(this.txttag.Text, ""),updatetype);
                if (flag > 0)
                {
                    string[] arys = this.hidPid.Value.Split(',');
                    for (int i = 0; i <arys.Length; i++)
                    {
                        long ProductId = long.Parse(arys[i]);
                        Maticsoft.BLL.Products.Lucene.ProductIndexManagerLocal.productIndex.Mod(ProductId);
                    }
                }
            }
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>closefrm(" +flag+ ");</script>");
           
        }
    }
}