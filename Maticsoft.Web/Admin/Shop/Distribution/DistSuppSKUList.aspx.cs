using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Data;

namespace Maticsoft.Web.Admin.Shop.Distribution
{
    public partial class DistSuppSKUList : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Distribution.SuppDistSKU suppSkuBll = new BLL.Shop.Distribution.SuppDistSKU();
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo suppBll = new BLL.Shop.Supplier.SupplierInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo infoBll = new BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new BLL.Shop.Products.SKUInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //提取分销商名称
                DataSet ds = suppBll.GetList("Status=1");
                ddlSupplier.DataSource = ds;
                ddlSupplier.DataTextField = "Name";
                ddlSupplier.DataValueField = "SupplierId";
                ddlSupplier.DataBind();
                ddlSupplier.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }

        #region gridView

        public void BindData()
        {
            int supplierId = Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0);
            if (supplierId > 0)
            {
                gridView.DataSetSource = suppSkuBll.GetAllList(supplierId);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                if (e.CommandArgument != null)
                {
                    int supplierId= Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0); 
                    string sku = e.CommandArgument.ToString();
                    if (supplierId == 0 || String.IsNullOrWhiteSpace(sku))
                    {
                        return;
                    }
                    if (suppSkuBll.DeleteEx(supplierId, sku))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
                }
            }
        }
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }
        #endregion

        protected string GetProductName(object target)
        {
            long productId = Common.Globals.SafeInt(target, 0);
            Maticsoft.Model.Shop.Products.ProductInfo infoModel = infoBll.GetModel(productId);
            return infoModel == null ? "该商品不存在" : infoModel.ProductName;
        }

        protected string GetSKUStr(object target)
        {
            string sku = target.ToString();
            List<Maticsoft.Model.Shop.Products.SKUItem> itemList = skuBll.GetSKUItemsBySku(sku);
            if (itemList == null || itemList.Count == 0)
            {
                return "";
            }
            string str="";
            foreach (var item in itemList)
            {
                str += item.AttributeName + "：" + item.AV_ValueStr + "  ";
            }
            return str;
        }
        
    }
}