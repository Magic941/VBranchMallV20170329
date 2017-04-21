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
using Maticsoft.BLL.Shop.Order;

namespace Maticsoft.Web.Admin.Statistics
{
    public partial class Commission : PageBaseAdmin
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
            BLL.Shop.Order.OrderItems itemBll=new OrderItems();
            decimal deRate = BLL.SysManage.ConfigSystem.GetDecimalValueByCache("Shop_DE_Commission");
               decimal cpRate = BLL.SysManage.ConfigSystem.GetDecimalValueByCache("Shop_CP_Commission");
           DataSet ds= itemBll.GetCommission(deRate, cpRate);
            //设计师
            int? deQuantity = ds.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? dePrice = ds.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!deQuantity.HasValue) deQuantity = 0;
            if (!dePrice.HasValue) dePrice = 0;
            lblDEQuantity.Text = deQuantity.Value.ToString();
            lblDEPrice.Text = dePrice.Value.ToString("C2");

            //CP
            int? cpQuantity = ds.Tables[0].Rows[1].Field<int?>("ToalQuantity");
            decimal? cpPrice = ds.Tables[0].Rows[1].Field<decimal?>("ToalPrice");
            if (!cpQuantity.HasValue) cpQuantity = 0;
            if (!cpPrice.HasValue) cpPrice = 0;
            lblCPQuantity.Text = cpQuantity.Value.ToString();
            lblCPPrice.Text = cpPrice.Value.ToString("C2");

            lblToalQuantity.Text = (deQuantity + cpQuantity).Value.ToString();
            lblToalPrice.Text = (dePrice + cpPrice).Value.ToString("C2");
        }


    }
}
