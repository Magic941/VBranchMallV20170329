/**
* ProductsStationMode.cs
*
* 功 能： [N/A]
* 类 名： ProductsStationMode
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/27 19:04:19  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
namespace Maticsoft.ViewModel.Supplier
{
    public class ProductsStationMode
    {
        public System.Web.Mvc.SelectList DrpProductCategory { get; set; }
        public Webdiyer.WebControls.Mvc.PagedList<Model.Shop.Products.ProductInfo> AddedProductList { get; set; }
        public Webdiyer.WebControls.Mvc.PagedList<Model.Shop.Products.ProductInfo> SearchProductList { get; set; }
    }
}
