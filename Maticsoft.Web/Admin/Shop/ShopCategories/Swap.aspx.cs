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
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    public partial class Swap : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 531; } } //Shop_商品分类管理_批量转移页
        private Maticsoft.BLL.Shop.Products.CategoryInfo bll = new Maticsoft.BLL.Shop.Products.CategoryInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.Params["id"].Trim()))
                {
                    this.CategoriesDropList2.SelectedValue = Request.Params["id"];
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int fromCategoryId = Globals.SafeInt(this.CategoriesDropList2.SelectedValue, 0);
            int toCategoryId = Globals.SafeInt(this.CategoriesDropList1.SelectedValue, 0);

            if (bll.DisplaceCategory(fromCategoryId, toCategoryId))
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowSuccessTip(this, "商品转移成功，正在跳转列表页...", "list.aspx");
            }
            else
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowFailTip(this, "商品转移失败！");
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}