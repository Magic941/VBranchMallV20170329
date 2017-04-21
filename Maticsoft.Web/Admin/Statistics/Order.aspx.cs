/**
* Order.cs
*
* 功 能： [N/A]
* 类 名： Order.cs
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
    public partial class Order : PageBaseAdmin
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
            //订单状态 -4 系统锁定 | -3 后台锁定 | -2 用户锁定 | -1 死单(取消) | 0 未处理 | 1 活动 | 2 已完成

            //未支付
            DataSet dsNotPay = BLL.Shop.Order.OrderManage.Stat4OrderStatus(0);
            int? notPayQuantity = dsNotPay.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? notPayPrice = dsNotPay.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!notPayQuantity.HasValue) notPayQuantity = 0;
            if (!notPayPrice.HasValue) notPayPrice = 0;
            lblNotPayQuantity.Text = notPayQuantity.Value.ToString();
            lblNotPayPrice.Text = notPayPrice.Value.ToString("C2");

            //已支付 进行中
            DataSet dsPayment = BLL.Shop.Order.OrderManage.Stat4OrderStatus(1);
            int? paymentQuantity = dsPayment.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? paymentPrice = dsPayment.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!paymentQuantity.HasValue) paymentQuantity = 0;
            if (!paymentPrice.HasValue) paymentPrice = 0;
            lblPaymentQuantity.Text = paymentQuantity.Value.ToString();
            lblPaymentPrice.Text = paymentPrice.Value.ToString("C2");

            //已完成
            DataSet dsComplete = BLL.Shop.Order.OrderManage.Stat4OrderStatus(2);
            int? completeQuantity = dsComplete.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? completePrice = dsComplete.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!completeQuantity.HasValue) completeQuantity = 0;
            if (!completePrice.HasValue) completePrice = 0;
            lblCompleteQuantity.Text = completeQuantity.Value.ToString();
            lblCompletePrice.Text = completePrice.Value.ToString("C2");
        }


    }
}
