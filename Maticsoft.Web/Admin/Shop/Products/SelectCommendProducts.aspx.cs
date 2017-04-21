/**
* SelectCategory.cs
*
* 功 能： 选择商品分类
* 类 名： SelectCategory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/7/01 11:12:07   Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

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
    public partial class SelectCommendProducts : PageBaseAdmin
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

                if (SelectType == 0)
                {
                    this.litDesc.Text = "需要推荐的商品";
                }
                if (SelectType == 1)
                {
                    this.litDesc.Text = "需要热卖的商品";
                }
                if (SelectType == 2)
                {
                    this.litDesc.Text = "需要特价的商品";
                }
                if (SelectType == 3)
                {
                    this.litDesc.Text = "需要推荐的最新商品";
                }
                if (SelectType == 4)
                {
                    this.litDesc.Text = "首页分类推荐";
                }
                if (SelectType == 5)
                {
                    this.litDesc.Text = "首页楼层显示";
                }
                if (SelectType == 6)
                {
                    this.litDesc.Text = "需要分享的商品";
                }
                if (SelectType == 7)
                {
                    this.litDesc.Text = "主题馆1";
                }
                if (SelectType == 8)
                {
                    this.litDesc.Text = "主题馆2";
                }
                if (SelectType == 9)
                {
                    this.litDesc.Text = "主题馆3";
                }
                if (SelectType == 10)
                {
                    this.litDesc.Text = "主题馆4";
                }
                if (SelectType == -1)
                {
                    this.litDesc.Text = "选择要导出的商品";
                }
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
            this.drpProductCategory.Items.Insert(0, new ListItem("", string.Empty));
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
            this.ddlSupplier.Items.Insert(0, new ListItem("", string.Empty));
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
            //DataSet ds = stationModeManage.GetStationMode(SelectType, "", "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder strPIds = new StringBuilder();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strPIds.Append(ds.Tables[0].Rows[i]["StationId"]);
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

            // int recordCount = productManage.GetProductRecListCount(skus.Distinct().ToArray());
            int recordCount = skus.Length;

            //List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetSKU4AttrVal(
            //去除重复数据
            //skus.Distinct().ToArray(),
            //anpAddedProducts.StartRecordIndex,
            //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
            //anpAddedProducts.PageSize, out recordCount);

            int endIndex = anpAddedProducts.StartRecordIndex + anpAddedProducts.PageSize;

            //(李永琴2014-11-07 16:51修改过)
            //原本的方法 List<Model.Shop.Products.ProductInfo> prodList = productManage.GetProductRecListByPage(skus.Distinct().ToArray(), anpAddedProducts.StartRecordIndex, endIndex);
            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetProductRecListByPageNew(skus.Distinct().ToArray(), anpAddedProducts.StartRecordIndex, endIndex);
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
            ////分页数据重置
            //string productName = ViewState["OldProductName"] as string;
            //if (txtProductName.Text != productName)
            //{
            //    ViewState["OldProductName"] = txtProductName.Text;
            //    anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            //}

            //获取已选择数据


            //List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetSKU4AttrVal(txtProductName.Text,  drpProductCategory.SelectedValue, hfSelectedData.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),  anpSearchProducts.StartRecordIndex,
            //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
            //anpSearchProducts.EndRecordIndex, out recordCount);
            //计算分页结束索引
            //if (!isClear)
            //{
            //    endIndex = anpSearchProducts.EndRecordIndex;
            //}
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
            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetProductRecListByPage(skus.Distinct().ToArray(), anpAddedProducts.StartRecordIndex, endIndex);
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
            //this.ddlGoodType.Items.Insert(0, new ListItem("全　部", string.Empty));
            //if (GoodTypeID != 0)
            //{
            //    ddlSupplier.SelectedValue = GoodTypeID.ToString();
            //}
            //else
            //{
            ddlSupplier.SelectedIndex = 0;
            // }
        }
        protected int GoodTypeID
        {
            get
            {
                int GoodTypeID = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["GoodTypeID"]))
                {
                    GoodTypeID = Common.Globals.SafeInt(Request.Params["GoodTypeID"], 0);
                }
                return GoodTypeID;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData(false);
        }

    }
}