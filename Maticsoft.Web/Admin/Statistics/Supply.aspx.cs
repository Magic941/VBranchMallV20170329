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
using System.Data;

namespace Maticsoft.Web.Admin.Statistics
{
    public partial class Supply : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        private void ShowInfo()
        {
            BLL.Shop.Supplier.SupplierInfo bll = new BLL.Shop.Supplier.SupplierInfo();
            DataSet ds = bll.GetStatisticsSupply(-1);

            //剩余
            int? remainQuantity = ds.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? remainPrice = ds.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!remainQuantity.HasValue) remainQuantity = 0;
            if (!remainPrice.HasValue) remainPrice = 0;
            lblRemainQuantity.Text = remainQuantity.Value.ToString();
            lblRemainPrice.Text = remainPrice.Value.ToString("C2");

            //已售
            int? soldQuantity = ds.Tables[0].Rows[1].Field<int?>("ToalQuantity");
            decimal? soldPrice = ds.Tables[0].Rows[1].Field<decimal?>("ToalPrice");
            if (!soldQuantity.HasValue) soldQuantity = 0;
            if (!soldPrice.HasValue) soldPrice = 0;
            lblSoldQuantity.Text = soldQuantity.Value.ToString();
            lblSoldPrice.Text = soldPrice.Value.ToString("C2");

            lblToalQuantity.Text = (remainQuantity + soldQuantity).Value.ToString();
            lblToalPrice.Text = (remainPrice + soldPrice).Value.ToString("C2");
        }


    }
}
