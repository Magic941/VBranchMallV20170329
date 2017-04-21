using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Json;
using Maticsoft.Json.Conversion;

namespace Maticsoft.Web.Admin.Shop.Distribution
{
    public partial class ProductSKUList : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo infoBll = new BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Supplier.SupplierInfo suppBll = new BLL.Shop.Supplier.SupplierInfo();
        private Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new BLL.Shop.Products.SKUInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }

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

            int cid = Common.Globals.SafeInt(this.ddlCateList.SelectedValue, 0);
            if (cid > 0)
            {
                gridView.DataSetSource = skuBll.GetSKUListByCid(cid);
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
            string str = "";
            foreach (var item in itemList)
            {
                str += item.AttributeName + "：" + item.AV_ValueStr + "  ";
            }
            return str;
        }

        #region AjaxCallback

        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "GetSuppStock":
                    writeText = GetSuppStock();
                    break;
            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string GetSuppStock()
        {
            JsonObject json = new JsonObject();
            string jsonList = this.Request.Params["List"];
            int supplierId = Common.Globals.SafeInt(this.Request.Params["SupplierId"], 0);
            Maticsoft.BLL.Shop.Products.SKUInfo skuInfoBll = new BLL.Shop.Products.SKUInfo();
            if (supplierId == 0 || string.IsNullOrWhiteSpace(jsonList))
            {
                json.Put("STATUS", "FAILED");
                return json.ToString();
            }
            JsonArray jsonArray = JsonConvert.Import<JsonArray>(jsonList);
            foreach (JsonObject jsonObject in jsonArray)
            {
                string SKU = jsonObject["SKU"].ToString();
                int Stock = Maticsoft.Common.Globals.SafeInt(jsonObject["Stock"].ToString(), 0);
                if (Stock>0)
                {
                    skuInfoBll.UpdateSuppStock(SKU, Stock, supplierId);
                }
            }
            json.Put("STATUS", "SUCCESS");
            return json.ToString();
        }

        #endregion AjaxCallback

    }
}