/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
*
* Ver        变更日期                           负责人          变更内容
* ───────────────────────────────────
* V0.01   2012年6月13日 20:46:27    Rock            创建
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.UI;
namespace Maticsoft.Web.Admin.Shop.ProductType
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 514; } } //Shop_商品类型管理_详细页
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int TypeId = (Convert.ToInt32(strid));
                    ShowInfo(TypeId);
                }
            }
        }

        private void ShowInfo(int TypeId)
        {
            Maticsoft.BLL.Shop.Products.ProductType bll = new Maticsoft.BLL.Shop.Products.ProductType();
            Maticsoft.Model.Shop.Products.ProductType model = bll.GetModel(TypeId);
            this.lblTypeId.Text = model.TypeId.ToString();
            this.lblTypeName.Text = model.TypeName;
            this.lblRemark.Text = model.Remark;

        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}

