/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
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

namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 530; } } //Shop_商品分类管理_详细页
        public string strid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int CategoryId = (Convert.ToInt32(strid));
                    ShowInfo(CategoryId);
                }
            }
        }

        private void ShowInfo(int CategoryId)
        {
            Maticsoft.BLL.Shop.Products.CategoryInfo bll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
            Maticsoft.Model.Shop.Products.CategoryInfo model = bll.GetModel(CategoryId);
            this.lblCategoryId.Text = model.CategoryId.ToString();
            this.lblName.Text = model.Name;
            this.lblDisplaySequence.Text = model.DisplaySequence.ToString();
            this.lblMeta_Description.Text = model.Meta_Description;
            this.lblMeta_Keywords.Text = model.Meta_Keywords;
            this.lblDescription.Text = model.Description;
            this.lblParentCategoryId.Text = model.ParentCategoryId.ToString();
            this.lblDepth.Text = model.Depth.ToString();
            this.lblPath.Text = model.Path;
            this.lblRewriteName.Text = model.RewriteName;
            this.lblSKUPrefix.Text = model.SKUPrefix;
            this.lblAssociatedProductType.Text = model.AssociatedProductType.ToString();
            this.lblNotes1.Text = model.Notes1;
            this.lblNotes2.Text = model.Notes2;
            this.lblNotes3.Text = model.Notes3;
            this.lblNotes4.Text = model.Notes4;
            this.lblNotes5.Text = model.Notes5;
            this.lblTheme.Text = model.Theme;
            this.lblHasChildren.Text = model.HasChildren ? "是" : "否";
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}