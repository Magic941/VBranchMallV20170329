/**
* OrderDetailModel.cs
*
* 功 能： [N/A]
* 类 名： OrderDetailModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/21 16:26:41  Rock    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Collections.Generic;

namespace Maticsoft.ViewModel.Shop
{
   public  class OrderDetailModel
   {
        public Model.Shop.Order.OrderInfo OrderInfo { get; set; }
        public List<Model.Shop.Order.OrderItems> ListOrderItems { get; set; }
        public List<Model.Shop.Order.OrderAction> ListOrderAction { get; set; }
        public List<Model.Shop.Order.OrderRemark> ListOrderRemark { get; set; }
    }
}
