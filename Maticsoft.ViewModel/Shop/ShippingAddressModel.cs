/**
* ShippingAddressModel.cs
*
* 功 能： [N/A]
* 类 名： ShippingAddressModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/30 20:49:26  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;

namespace Maticsoft.ViewModel.Shop
{
    public class ShippingAddressModel
    {
        public List<Model.Shop.Shipping.ShippingAddress> ListAddress;
        public Model.Shop.Shipping.ShippingAddress CurrentAddress;
    }
}
