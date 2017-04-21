using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class SelectExportProduct : System.Web.UI.Page
    {
        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                BindCategories();
               this.litDesc.Text = "选择需要导出的商品";
               
            }
        }
        //public int SelectType
        //{
        //    get
        //    {
        //        int type = 0;
        //        if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
        //        {
        //            type = Globals.SafeInt(Request.Params["type"], 0);
        //        }
        //        return type;
        //    }
        //}

        //private void BindExistReleatedProducts()
        //{
        //    BLL.Shop.Products.RelatedProduct manage = new BLL.Shop.Products.RelatedProduct();
        //    List<Model.Shop.Products.RelatedProduct> list = manage.GetModelList(ProductId);
        //    if (list != null && list.Count > 0)
        //    {
        //        StringBuilder strExistInfo = new StringBuilder();
        //        list.ForEach(info =>
        //        {
        //            strExistInfo.Append(info.RelatedId);
        //            strExistInfo.Append(",");
        //        });
        //        this.hfSelectedData.Value = strExistInfo.ToString();
        //    }
        //}

        private void BindCategories()
        {
            Maticsoft.BLL.Shop.Products.CategoryInfo bll = new BLL.Shop.Products.CategoryInfo();
            DataSet ds = bll.GetList("  Depth = 1 ");

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.drpProductCategory.DataSource = ds;
                this.drpProductCategory.DataTextField = "Name";
                this.drpProductCategory.DataValueField = "CategoryId";
                this.drpProductCategory.DataBind();
            }
            this.drpProductCategory.Items.Insert(0, new ListItem("请选择", string.Empty));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        #region BindData

        public void BindData()
        {
            //加载搜索数据
            BindSearchProduct();
            //加载选择数据
            BindAddProduct();
        }

        private void BindAddProduct()
        {
            BLL.Shop.Products.ProductStationMode stationModeManage = new BLL.Shop.Products.ProductStationMode();
            string selectedIds = hfSelectedData.Value;
            int recordCount = productManage.GetRecordCountEx(string.IsNullOrEmpty(selectedIds) ? "0" : selectedIds, txtProductName.Text, "");
            DataSet ds = productManage.GetListByPage(productManage.GetListExSql(string.IsNullOrEmpty(selectedIds) ? "0" : selectedIds, txtProductName.Text, ""), "",
                                             anpAddedProducts.StartRecordIndex, anpAddedProducts.EndRecordIndex);
            anpAddedProducts.RecordCount = recordCount;
            dlstAddedProducts.DataSource = ds;
            dlstAddedProducts.DataBind();
        }

        private void BindSearchProduct()
        {
            if (anpSearchProducts.RecordCount == 0)
            {
                anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            }
            ////分页数据重置
            //string productName = ViewState["OldProductName"] as string;
            //if (txtProductName.Text != productName)
            //{
            //    ViewState["OldProductName"] = txtProductName.Text;
            //    anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            //}

            //获取已选择数据
            string selectedIds = hfSelectedData.Value;
            string[] pIds = selectedIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            // List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetSKU4AttrVal(txtProductName.Text,  drpProductCategory.SelectedValue, hfSelectedData.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),  anpSearchProducts.StartRecordIndex,
            //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
            //anpSearchProducts.EndRecordIndex, out recordCount);

            List<Model.Shop.Products.ProductInfo> prodList =
                productManage.DataTableToList(
                    productManage.GetListByPage(
                        productManage.GetListExSql("", txtProductName.Text, selectedIds,
                                                   Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0)), "",
                        anpSearchProducts.StartRecordIndex, anpSearchProducts.EndRecordIndex).Tables[0]);

            anpSearchProducts.RecordCount = productManage.GetRecordCount(productManage.GetListExSql("", txtProductName.Text, selectedIds,
                                                   Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0)));

            //设置全选数据 供JavaScript使用

             var currentAllData = (from item in prodList select item.ProductId).Distinct().ToArray();
             string currentAllDataStr= string.Join(",", currentAllData);
            hfCurrentAllData.Value = string.IsNullOrEmpty(currentAllDataStr)
                                         ? currentAllDataStr.TrimStart(',').TrimEnd(',')
                                         : "";
            dlstSearchProducts.DataSource = prodList;
            dlstSearchProducts.DataBind();
        }

        #endregion BindData

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.hfSelectedData.Value = "";
            BindData();
        }
    }
}