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
using System.Text;
using Maticsoft.BLL.Pay;
using Maticsoft.Common;
using System.Data;

namespace Maticsoft.Web.Admin.Statistics
{
    public partial class RechargeAndTx : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //ShowInfo();
            }
        }
        public void btnCount_Click(object sender, EventArgs e)
        {
            string startDate = this.BeginTime.Text;
            string endDate = this.EndTime.Text;
            Maticsoft.BLL.Pay.RechargeRequest rechargeRequest=new RechargeRequest();
            Maticsoft.BLL.Pay.BalanceDrawRequest balanceDrawRequest=new BalanceDrawRequest();
            int rechargeCount = rechargeRequest.GetTotalCount(startDate, endDate);
            int rechargeAmount = rechargeRequest.GetTotalAmount(startDate, endDate);
            int balaceDrawCount = balanceDrawRequest.GetTotalCount(startDate, endDate);
            int balaceDrawAmount = balanceDrawRequest.GetTotalAmount(startDate, endDate);
            this.lblToalCount1.Text = rechargeCount.ToString();
            this.lblToalCount2.Text = rechargeAmount.ToString("C");
            this.lblToalAmount1.Text = balaceDrawCount.ToString();
            this.lblToalAmount2.Text = balaceDrawAmount.ToString("C");
        }

        //private void ShowInfo()
        //{
        //    BLL.Shop.Supplier.SupplierInfo bll = new BLL.Shop.Supplier.SupplierInfo();
        //    DataSet ds = bll.GetStatisticsSupply(SupplierId);

        //    //剩余
        //    int? remainQuantity = ds.Tables[0].Rows[0].Field<int?>("ToalQuantity");
        //    decimal? remainPrice = ds.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
        //    if (!remainQuantity.HasValue) remainQuantity = 0;
        //    if (!remainPrice.HasValue) remainPrice = 0;
        //    lblRemainQuantity.Text = remainQuantity.Value.ToString();
        //    lblRemainPrice.Text = remainPrice.Value.ToString("C2");

        //    //已售
        //    int? soldQuantity = ds.Tables[0].Rows[1].Field<int?>("ToalQuantity");
        //    decimal? soldPrice = ds.Tables[0].Rows[1].Field<decimal?>("ToalPrice");
        //    if (!soldQuantity.HasValue) soldQuantity = 0;
        //    if (!soldPrice.HasValue) soldPrice = 0;
        //    lblSoldQuantity.Text = soldQuantity.Value.ToString();
        //    lblSoldPrice.Text = soldPrice.Value.ToString("C2");

        //    lblToalQuantity.Text = (remainQuantity + soldQuantity).Value.ToString();
        //    lblToalPrice.Text = (remainPrice + soldPrice).Value.ToString("C2");
        //}


    }
}
