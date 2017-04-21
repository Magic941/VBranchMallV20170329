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
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class SelectRelatedProducts : PageBaseAdmin
    {
        private BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();
        private BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ProductId > 0)
                    BindExistReleatedProducts();
                BindCategories();
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

        private void BindExistReleatedProducts()
        {
            BLL.Shop.Products.RelatedProduct manage = new BLL.Shop.Products.RelatedProduct();
            //List<Model.Shop.Products.RelatedProduct> list = manage.GetModelList(ProductId);
            //if (list != null && list.Count > 0)
            //{
            //    StringBuilder strExistInfo = new StringBuilder();
            //    list.ForEach(info =>
            //    {
            //        strExistInfo.Append(info.RelatedId);
            //        strExistInfo.Append(",");
            //    });
            //    this.hfSelectedData.Value = strExistInfo.ToString();
            //    HiddenField_SelectRelatedData.Value = "208_1";
            //}

            DataSet ds = manage.IsDoubleRelated(ProductId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder strExistRelatedInfo = new StringBuilder();
                StringBuilder strExistInfo = new StringBuilder();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strExistRelatedInfo.AppendFormat("{0}_{1}", ds.Tables[0].Rows[i]["RelatedId"],
                                              ds.Tables[0].Rows[i]["IsRelated"]);
                    strExistRelatedInfo.Append(",");
                    strExistInfo.Append(ds.Tables[0].Rows[i]["RelatedId"]);
                    strExistInfo.Append(",");
                }
                this.hfSelectedData.Value = strExistInfo.ToString();
                this.HiddenField_SelectRelatedData.Value = strExistRelatedInfo.ToString();
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
            //获取已选择数据
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

            int recordCount;

            //List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetSKU4AttrVal(
            //去除重复数据
            //skus.Distinct().ToArray(),
            //anpAddedProducts.StartRecordIndex,
            //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
            //anpAddedProducts.PageSize, out recordCount);

            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetProductsList(skus.Distinct().ToArray(), anpAddedProducts.StartRecordIndex, anpAddedProducts.EndRecordIndex, out recordCount, ProductId);
            anpAddedProducts.RecordCount = recordCount;
            dlstAddedProducts.DataSource = prodList;
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
            string selectedskus = hfSelectedData.Value;
            string pIds=string.Empty;
            if (!string.IsNullOrWhiteSpace(selectedskus))
            {
               pIds= selectedskus.TrimEnd(',');
            }
            int recordCount;

            // List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetSKU4AttrVal(txtProductName.Text,  drpProductCategory.SelectedValue, hfSelectedData.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),  anpSearchProducts.StartRecordIndex,
            //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
            //anpSearchProducts.EndRecordIndex, out recordCount);

            List<Model.Shop.Products.ProductInfo> prodList = productManage.GetProductsList(pIds, txtProductName.Text, drpProductCategory.SelectedValue, anpSearchProducts.StartRecordIndex, anpSearchProducts.EndRecordIndex, out recordCount, ProductId);

            anpSearchProducts.RecordCount = recordCount;

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