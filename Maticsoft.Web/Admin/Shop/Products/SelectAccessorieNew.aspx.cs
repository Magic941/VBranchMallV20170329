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

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class SelectAccessorieNew : PageBaseAdmin
    {
        private BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ProductId > 0)
                    BindExistAccessoriesValue();
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

        private void BindExistAccessoriesValue()
        {
            BLL.Shop.Products.AccessoriesValue manage = new BLL.Shop.Products.AccessoriesValue();
            //List<Model.Shop.Products.AccessoriesValue> list = manage.GetListByAccessoriesId(ProductId);
            //if (list != null && list.Count > 0)
            //{
            //    StringBuilder strExistAcc = new StringBuilder();
            //    list.ForEach(info =>
            //    {
            //        strExistAcc.Append(info.SkuId);
            //        strExistAcc.Append(",");
            //    });
            //    this.hfSelectedData.Value = strExistAcc.ToString();
            //}
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
                anpAddedProducts.RecordCount = 0;
                dlstAddedProducts.DataSource = null;
                dlstAddedProducts.DataBind();
                return;
            }

            //Check Data
            string[] skus = selectedskus.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (skus.Length < 0) return;

            int recordCount;
            List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetSKU4AttrVal(

                //去除重复数据
                skus.Distinct().ToArray(),
                anpAddedProducts.StartRecordIndex,

                //存储过程处理 这里取每页数量 防止后设置RecordCount出现被截断数据问题
                anpAddedProducts.EndRecordIndex, out recordCount, ProductId);
            anpAddedProducts.RecordCount = recordCount;
            dlstAddedProducts.DataSource = skuList;
            dlstAddedProducts.DataBind();
        }

        private void BindSearchProduct()
        {
            if (anpSearchProducts.RecordCount == 0)
            {
                anpSearchProducts.RecordCount = anpSearchProducts.PageSize;
            }

            int recordCount;
            List<Maticsoft.Model.Shop.Products.SKUInfo> skuList = manage.GetSKU4AttrVal(txtProductName.Text, drpProductCategory.SelectedValue, hfSelectedData.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries), anpSearchProducts.StartRecordIndex, anpSearchProducts.EndRecordIndex, out recordCount, ProductId);
            anpSearchProducts.RecordCount = recordCount;

            //设置全选数据 供JavaScript使用
            if (skuList != null && skuList.Count > 0)
            {
                StringBuilder tmpSkuIds = new StringBuilder();
                skuList.ForEach(info =>
                {
                    tmpSkuIds.Append(info.SkuId);
                    tmpSkuIds.Append(",");
                });
                hfCurrentAllData.Value = tmpSkuIds.ToString();
            }
            else
            {
                hfCurrentAllData.Value = string.Empty;
            }

            dlstSearchProducts.DataSource = skuList;
            dlstSearchProducts.DataBind();
        }

        #endregion BindData

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void rptProducts_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptSKUItems = e.Item.FindControl("rptSKUItems") as Repeater;
                rptSKUItems.DataSource = ((Maticsoft.Model.Shop.Products.SKUInfo)e.Item.DataItem).SkuItems;
                rptSKUItems.DataBind();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfSelectedData.Value = "";
            BindData();
        }
    }
}