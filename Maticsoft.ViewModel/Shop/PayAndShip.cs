/**
* PayAndShipModel.cs
*
* 功 能： [N/A]
* 类 名： PayAndShipModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/20 15:14:10  Ben    初版
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
    public class PayAndShip
    {
        public List<Maticsoft.Payment.Model.PaymentModeInfo> ListPaymentMode { get; set; }
        public List<Maticsoft.Model.Shop.Shipping.ShippingType> ListShippingType { get; set; }

        public Maticsoft.Payment.Model.PaymentModeInfo CurrentPaymentMode { get; set; }
        public Model.Shop.Shipping.ShippingType CurrentShippingType { get; set; }
    }
}
