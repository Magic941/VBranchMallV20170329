/**
* ProductList.cs
*
* 功 能： 商品列表
* 类 名： ProductList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/14 19:32:26  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Collections.Generic;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.ViewModel.Shop
{
    public class ProductListModel
    {
        public List<Model.Shop.Products.CategoryInfo> CategoryList { get; set; }
        public List<Model.Shop.Products.CategoryInfo> SubCategoryList { get; set; }

        public List<Model.Shop.Products.CategoryInfo> CategoryPathList { get; set; }

        public PagedList<Model.Shop.Products.ProductInfo> ProductPagedList { get; set; }
        public PagedList<Model.Shop.Products.ProductInfoForProductIndex> ProductPagedListForProductIndex { get; set; }
        private List<Model.Shop.Products.ProductInfo>[] _productt4FourCol;

        public List<Model.Shop.Products.ProductInfo>[] ProductList4FourCol
        {
            get
            {
                if (_productt4FourCol != null) return _productt4FourCol;
                List<Model.Shop.Products.ProductInfo>[] list = new[]
                    {
                        new List<Model.Shop.Products.ProductInfo>(), new List<Model.Shop.Products.ProductInfo>(),
                        new List<Model.Shop.Products.ProductInfo>(), new List<Model.Shop.Products.ProductInfo>()
                    };
                if (ProductPagedList == null) return list;
                int index = 0;
                ProductPagedList.ForEach(image =>
                    {
                        //reset
                        if (index == 4) index = 0;
                        list[index++].Add(image);
                    });
                return list;
            }
            set { _productt4FourCol = value; }
        }

        public string CurrentCateName { set; get; }
        public int CurrentCid { set; get; }
        public string CurrentMod { set; get; }

        /// <summary>
        /// 店铺商品分类列表
        /// </summary>
        public List<Model.Shop.Supplier.SupplierCategories> SuppCategoryList { get; set; }
    }
}
