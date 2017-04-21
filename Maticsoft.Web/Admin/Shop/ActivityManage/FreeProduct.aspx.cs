using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.ActivityManage;
using Maticsoft.Common;


namespace Maticsoft.Web.Admin.Shop.ActivityManage
{
    public partial class FreeProdut : System.Web.UI.Page
    {
        //protected override int Act_PageLoad { get { return 559; } } //管理_设置应用商品页
        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        private BLL.Shop.ActivityManage.AMPBLL ampBll = new AMPBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindExistRuleProducts();
                BindCategories();
                BindData(false);
            }
        }

        public int AMId
        {
            get
            {
                int pid = 0;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AMId"]))
                {
                    pid = Globals.SafeInt(Request.QueryString["AMId"], 0);
                }
                return pid;
            }
        }


        private void BindExistRuleProducts()
        {
            List<Maticsoft.Model.Shop.ActivityManage.AMPModel> list = ampBll.GetModelList(" AMId=" + AMId);
            if (list != null && list.Count > 0)
            {
                StringBuilder strExistInfo = new StringBuilder();
                list.ForEach(info =>
                {
                    strExistInfo.Append(info.ProductId);
                    strExistInfo.Append(",");
                });
                this.hfSelectedData.Value = strExistInfo.ToString();
            }
        }

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
            BindData(false);
        }

        #region BindData

        public void BindData(bool isClear)
        {
            //加载搜索数据
            BindSearchProduct(isClear);

            //加载选择数据
            BindAddProduct();
        }


        private void BindAddProduct()
        {
            //获取已选择数据
            DataSet ds = ampBll.GetAMProducts(AMId, drpProductCategory.SelectedValue, txtProductName.Text, txtProductNum.Text);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder strPIds = new StringBuilder();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strPIds.Append(ds.Tables[0].Rows[i]["ProductId"]);
                    strPIds.Append(",");
                }
                hfSelectedData.Value = strPIds.ToString().TrimEnd(',');
            }
            else
            {
                hfSelectedData.Value = "";
            }
            string selectedskus = hfSelectedData.Value;
            if (anpAddedProducts.RecordCount == 0)
            {
                anpAddedProducts.RecordCount = anpAddedProducts.PageSize;
            }
            ////分页数据重置
            //string selectedData = ViewState["OldSelectedData"] as string;
            //if (selectedskus != selectedData)
            //{
            //    ViewState["OldSelectedData"] = selectedskus;
            //    anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            //}
            //如未选择数据, 执行清空操作
            if (string.IsNullOrWhiteSpace(selectedskus))
            {
                dlstAddedProducts.DataSource = null;
                dlstAddedProducts.DataBind();
                return;
            }

            //Check Data
            string[] skus = selectedskus.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (skus.Length < 0) return;



            //List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetSKU4AttrVal(
            //去除重复数据
            //skus.Distinct().ToArray(),
            //anpAddedProducts.StartRecordIndex,
            //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
            //anpAddedProducts.PageSize, out recordCount);

            int endIndex = anpAddedProducts.StartRecordIndex + anpAddedProducts.PageSize;

            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetAMProductList(
                skus.Distinct().ToArray(), anpAddedProducts.StartRecordIndex, endIndex);
            anpAddedProducts.RecordCount = productManage.GetAMProductCount(skus.Distinct().ToArray());
            dlstAddedProducts.DataSource = prodList;
            dlstAddedProducts.DataBind();
        }

        private void BindSearchProduct(bool isClear)
        {
            if (anpSearchProducts.RecordCount == 0)
            {
                anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            }


            //获取已选择数据
            string selectedskus = hfSelectedData.Value;
            string[] pIds = selectedskus.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (pIds.Length < 0) return;

            int endIndex = anpSearchProducts.StartRecordIndex + anpSearchProducts.PageSize;

            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetNoAMProductList(
                txtProductName.Text,
                txtProductNum.Text,
                drpProductCategory
                    .SelectedValue,
                anpSearchProducts
                    .StartRecordIndex,
                endIndex);

            anpSearchProducts.RecordCount = productManage.GetNoAMProductCount(txtProductName.Text,
              drpProductCategory.SelectedValue);



            //设置全选数据 供JavaScript使用

            if (prodList != null && prodList.Count > 0)
            {
                StringBuilder tmpSkuIds = new StringBuilder();
                prodList.ForEach(info =>
                {
                    tmpSkuIds.Append(",");
                });
                hfCurrentAllData.Value = tmpSkuIds.ToString();
            }
            else
            {
                hfCurrentAllData.Value = string.Empty;
            }

            //编辑商品时 相关商品排除自己的
            //if (ProductId > 0)
            //{
            //    var list = prodList.Select(info => info.ProductId != ProductId);
            //    dlstSearchProducts.DataSource = list;
            //}
            //else
            //{
            //    dlstSearchProducts.DataSource = prodList;
            //}
            dlstSearchProducts.DataSource = prodList;
            dlstSearchProducts.DataBind();
        }

        #endregion BindData

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            BindData(false);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ampBll.DeleteByRule(AMId);
            hfSelectedData.Value = "";
            BindData(true);
        }
    }
}