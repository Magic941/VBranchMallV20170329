/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年6月13日 15:59:47 ROCK 修改
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Shop.Brands
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 400; } } //Shop_品牌管理_详细页
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.chkProductTpyes.DataBind();
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int BrandId = (Convert.ToInt32(strid));
                    ShowInfo(BrandId);
                }
            }
        }

        private void ShowInfo(int BrandId)
        {
            Maticsoft.BLL.Shop.Products.BrandInfo bll = new Maticsoft.BLL.Shop.Products.BrandInfo();
            Maticsoft.Model.Shop.Products.BrandInfo model = bll.GetModel(BrandId);
            this.lblBrandId.Text = model.BrandId.ToString();
            this.lblBrandName.Text = model.BrandName;
            this.lblBrandSpell.Text = model.BrandSpell;
            this.lblMeta_Description.Text = model.Meta_Description;
            this.lblMeta_Keywords.Text = model.Meta_Keywords;
            this.imgLogo.ImageUrl = model.Logo;
            this.lblCompanyUrl.Text = model.CompanyUrl;

            Maticsoft.Model.Shop.Products.BrandInfo modelList = bll.GetRelatedProduct(BrandId, null);
            foreach (ListItem item in this.chkProductTpyes.Items)
            {
                if (modelList.ProductTypeIdOrBrandsId.Contains(int.Parse(item.Value)))
                {
                    item.Selected = true;
                }
            }

      
            this.lblDescription.Text = model.Description;
            this.lblDisplaySequence.Text = model.DisplaySequence.ToString();
            this.lblTheme.Text = model.Theme;

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Alist.aspx");
        }
    }

}
