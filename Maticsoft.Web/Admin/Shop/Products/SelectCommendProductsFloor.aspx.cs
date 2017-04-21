using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class SelectCommendProductsFloor : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 479; } } //Shop_商品推荐管理_列表页
        protected new int Act_AddData = 480;    //Shop_商品推荐管理_添加数据
        protected new int Act_DelData = 481;    //Shop_商品推荐管理_删除数据

        private BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();
        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
        BLL.Shop.Products.ProductStationMode stationModeManage = new BLL.Shop.Products.ProductStationMode();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                liDelAll.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                if (ProductId > 0)
                    BindExistReleatedProducts();
                BindCategories();
                BindSupplier();
                BindData(false);
                BindShopGoodType();

                //if (SelectType == 0)
                //{
                //    this.litDesc.Text = "需要推荐的商品";
                //}
                //if (SelectType == 1)
                //{
                //    this.litDesc.Text = "需要热卖的商品";
                //}
                //if (SelectType == 2)
                //{
                //    this.litDesc.Text = "需要特价的商品";
                //}
                //if (SelectType == 3)
                //{

                //    this.litDesc.Text = "最新商品推荐";
                //}
                //if (SelectType == 4)
                //{
                //    this.litDesc.Text = "首页分类推荐";
                //}
                if (SelectType == 5)
                {
                    this.litDesc.Text = "首页楼层显示";
                }
                //if (SelectType == -1)
                //{
                //    this.litDesc.Text = "选择要导出的商品";
                //}
            }
        }

        public long ProductId
        {
            get
            {
                long pid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["pid"]))
                {
                    pid = Globals.SafeLong(Request.Params["pid"], 0);
                }
                return pid;
            }
        }

        public int SelectType
        {
            get
            {
                int type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    type = Globals.SafeInt(Request.Params["type"], 0);

                }
                return type;
            }
        }

        private void BindExistReleatedProducts()
        {
            BLL.Shop.Products.RelatedProduct manage = new BLL.Shop.Products.RelatedProduct();
            List<Model.Shop.Products.RelatedProduct> list = manage.GetModelList(ProductId);
            if (list != null && list.Count > 0)
            {
                StringBuilder strExistInfo = new StringBuilder();
                list.ForEach(info =>
                {
                    strExistInfo.Append(info.RelatedId);
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


        /// <summary>
        /// 供应商
        /// </summary>
        private void BindSupplier()
        {
            Maticsoft.BLL.Shop.Supplier.SupplierInfo infoBll = new BLL.Shop.Supplier.SupplierInfo();
            DataSet ds = infoBll.GetList("  Status = 1 ");

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.ddlSupplier.DataSource = ds;
                this.ddlSupplier.DataTextField = "Name";
                this.ddlSupplier.DataValueField = "SupplierId";
                this.ddlSupplier.DataBind();
            }
            this.ddlSupplier.Items.Insert(0, new ListItem("请选择", string.Empty));
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
            int categoryId = Maticsoft.Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0);
            int supplierId = Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0);
            DataSet ds = stationModeManage.GetStationMode(SelectType, categoryId, txtProductName.Text, supplierId);
  
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
            //如未选择数据, 执行清空操作
            if (string.IsNullOrWhiteSpace(selectedskus))
            {
                dlstAddedProducts.DataSource = null;
                dlstAddedProducts.DataBind();
                return;
            }

            string[] skus = selectedskus.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (skus.Length < 0) return;

            int recordCount = productManage.GetProductRecListCount(skus.Distinct().ToArray());

            int endIndex = anpAddedProducts.StartRecordIndex + anpAddedProducts.PageSize;
            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetProductRecListByPage(skus.Distinct().ToArray(), anpAddedProducts.StartRecordIndex, endIndex, int.Parse(FloorList.SelectedValue.ToString()));
            anpAddedProducts.RecordCount = recordCount;
            dlstAddedProducts.DataSource = prodList;
            dlstAddedProducts.DataBind();
        }

        private void BindSearchProduct(bool isClear)
        {
            if (anpSearchProducts.RecordCount == 0)
            {
                anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            }
            int endIndex = anpSearchProducts.StartRecordIndex + anpSearchProducts.PageSize;
            int categoryId = Maticsoft.Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0);
            int supplierId = Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0);
            string pName = txtProductName.Text.Trim();
            int totalCount = productManage.GetProductNoRecCount(categoryId, pName, SelectType, supplierId);
            List<Model.Shop.Products.ProductInfo> prodList = new List<ProductInfo>();
            if (totalCount > 0)
            {
                prodList = productManage.GetProductNoRecList(categoryId, supplierId, pName, SelectType, anpSearchProducts.StartRecordIndex, endIndex);
            }
            anpSearchProducts.RecordCount = totalCount;

            //设置全选数据 供JavaScript使用

            if (prodList != null && prodList.Count > 0)
            {
                StringBuilder tmpSkuIds = new StringBuilder();
                prodList.ForEach(info =>
                {
                    if (info.ProductId != ProductId)
                    {
                        tmpSkuIds.Append(info.ProductId);
                    }
                    tmpSkuIds.Append(",");
                });
                hfCurrentAllData.Value = tmpSkuIds.ToString();
            }
            else
            {
                hfCurrentAllData.Value = string.Empty;
            }
            dlstSearchProducts.DataSource = prodList;
            dlstSearchProducts.DataBind();
        }
        protected void dlstAddedProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
            {
                HtmlGenericControl lbtnDel = (HtmlGenericControl)e.Item.FindControl("lbtnDel");
                lbtnDel.Visible = false;
            }
        }
        protected void dlstSearchProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
            {
                HtmlGenericControl lbtnAdd = (HtmlGenericControl)e.Item.FindControl("lbtnAdd");
                lbtnAdd.Visible = false;
            }

        }
        #endregion BindData

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            BindData(false);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            int categoryId = Maticsoft.Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0);
            stationModeManage.DeleteByType(SelectType, categoryId);

            hfSelectedData.Value = "";
            BindData(true);
        }

        protected void FloorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取已选择数据
            int categoryId = Maticsoft.Common.Globals.SafeInt(drpProductCategory.SelectedValue, 0);
            int supplierId = Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0);
            DataSet ds = stationModeManage.GetStationMode(SelectType, categoryId, txtProductName.Text, supplierId);
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

            int recordCount = productManage.GetProductRecListCount(skus.Distinct().ToArray());

            int endIndex = anpAddedProducts.StartRecordIndex + anpAddedProducts.PageSize;
            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetProductRecListByPage(skus.Distinct().ToArray(), anpAddedProducts.StartRecordIndex, endIndex, int.Parse(FloorList.SelectedValue.ToString()));
            anpAddedProducts.RecordCount = recordCount;
            dlstAddedProducts.DataSource = prodList;
            dlstAddedProducts.DataBind();
        }


        /// <summary>
        /// 绑定商品分类
        /// </summary>
        private void BindShopGoodType()
        {
            Maticsoft.BLL.Shop.Products.GoodsType GoodTypeBll = new BLL.Shop.Products.GoodsType();

            DataSet ds = GoodTypeBll.GetList("");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.ddlGoodType.DataSource = ds;
                this.ddlGoodType.DataTextField = "GoodTypeName";
                this.ddlGoodType.DataValueField = "GoodTypeID";
                this.ddlGoodType.DataBind();
            }
            ddlSupplier.SelectedIndex = 0;
        }
    }
}