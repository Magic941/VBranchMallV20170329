/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01   
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
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.ProductAccessories
{
    public partial class Add : PageBaseAdmin
    {
        private BLL.Shop.Products.ProductInfo prodBll = new BLL.Shop.Products.ProductInfo();
        private BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Type)
                {
                    case 1:
                        Literal2.Text = "增加配件组合";
                        Literal3.Text = "增加配件组合";
                        literName.Text = "配件组合名称";
                             break;
                    case 2:
                             literName.Text = "优惠组合名称";
                             Literal2.Text = "增加优惠组合";
                             Literal3.Text = "增加优惠组合";
                        trDiscountAmount.Visible = true;
                        txtDiscountAmount.Text = "";
                       // trStock.Visible = true;
                       // txtStock.Text = "";
                        break;
                }
                if (!prodBll.Exists(ProductId))
                {
                    MessageBox.ShowFailTipScript(this, "该商品不存在或已被删除！", string.Format("window.location.href='list.aspx?pid={0}&acctype={1}'", ProductId, Type));
                }
                BindAddProduct();
            }
        }

        #region 接收参数
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
        /// <summary>
        /// 类型 1：配件   2：组合套装
        /// </summary>
        public int Type
        {
            get
            {
                int type = 0;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["acctype"]))
                {
                    type = Globals.SafeInt(Request.QueryString["acctype"], 0);
                }
                return type;
            }
        }
        #endregion

        #region 保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 选择sku商品
            string sku = hfsku.Value;
            if (String.IsNullOrWhiteSpace(sku) || !manage.ExistsEx(sku,ProductId))
            {
                MessageBox.ShowFailTip(this, "请选择一个sku商品！");
                return;
            }
            #endregion 

            #region 验证
            if (this.txtName.Text.Trim().Length==0)
            {
                MessageBox.ShowFailTip(this, "组合名称不能为空！");
                return;
            }
            if (this.txtMaxQuantity.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请输入最大购买量！");
                return;
            }
            int MaxQuantity = Globals.SafeInt(this.txtMaxQuantity.Text.Trim(), 0);
            if (this.txtMinQuantity.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请输入最小购买量！");
                return;
            }
            int MinQuantity = Globals.SafeInt(this.txtMinQuantity.Text.Trim(), 0);

            int DiscountType = Globals.SafeInt(this.RadioDiscountType.SelectedValue, 1);

            if (this.txtDiscountAmount.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请填写优惠价！");
                return;
            }
            decimal DiscountAmount = Globals.SafeDecimal(this.txtDiscountAmount.Text.Trim(), 0M);
		
            if (this.txtStock.Text.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请输入库存！");
                return;
            }
            #endregion

            int Stock = Globals.SafeInt(this.txtStock.Text.Trim(), 0);
			string Name=this.txtName.Text;
            Maticsoft.Model.Shop.Products.ProductAccessorie model = new  Model.Shop.Products.ProductAccessorie();
    		model.Name=Name;
			model.MaxQuantity=MaxQuantity;
			model.MinQuantity=MinQuantity;
			model.DiscountType=DiscountType;
			model.DiscountAmount=DiscountAmount;
			model.Type=Type;
			model.Stock=Stock;
        	model.Type = Type;
            model.ProductId = ProductId;
            BLL.Shop.Products.ProductAccessorie bll = new  BLL.Shop.Products.ProductAccessorie();
            if (bll.Add(model, sku))
            {
                MessageBox.ShowSuccessTipScript(this, "保存成功！",
                                                string.Format("window.location.href='list.aspx?pid={0}&acctype={1}'",
                                                              ProductId, Type));
            }
            else
            {
                MessageBox.ShowFailTip(this, "保存失败！");
            }
		}
        #endregion

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("list.aspx?pid={0}&acctype={1}",  ProductId, Type));
        }
        #region BindData
        private void BindAddProduct()
        {
            if (anpAddedProducts.RecordCount == 0)
            {
                anpAddedProducts.RecordCount = anpAddedProducts.PageSize;
            }
            int recordCount;
            List<Model.Shop.Products.SKUInfo> skuList = manage.GetSKUListByProdId(ProductId, anpAddedProducts.StartRecordIndex,anpAddedProducts.EndRecordIndex, out recordCount);
            anpAddedProducts.RecordCount = recordCount;
            //if (skuList == null)//没有规格的商品
            //{
            //    skuList = manage.GetListInnerJoinProd(ProductId);        
            //}
            dlstAddedProducts.DataSource = skuList;
            dlstAddedProducts.DataBind();
        }
       
        #endregion BindData

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            BindAddProduct();
        }
        protected void dlstAddedProducts_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptSKUItems = e.Item.FindControl("rptSKUItems") as Repeater;
                rptSKUItems.DataSource = ((Model.Shop.Products.SKUInfo)e.Item.DataItem).SkuItems;
                rptSKUItems.DataBind();
            }
 
        }

        protected void dlstAddedProducts_ItemCommand(object source, DataListCommandEventArgs e)
        {
        }
    }
}
