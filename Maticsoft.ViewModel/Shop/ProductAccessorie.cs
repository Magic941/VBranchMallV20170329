/**
* ProductAccessorie.cs
*
* 功 能： [N/A]
* 类 名： ProductAccessorie
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/30 14:41:54  Rock    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Shop
{
    public class ProductAccessorie
    {
        /// <summary>
        /// 组合信息
        /// </summary>
        public Model.Shop.Products.ProductAccessorie ProductAccessorieInfo { get; set; }

        /// <summary>
        /// 商品SKU列表
        /// </summary>
        public  List<Model.Shop.Products.SKUInfo> SkuInfo { get; set; }
    }
}
