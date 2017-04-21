using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Products.Lucene
{
    public class ProductSearchResult : ProductIndexAPIBaseModel
    {
        /// <summary>
        /// 商品搜索 数据
        /// </summary>
        public List<ProductInfoForProductIndex> ProductsResult { get; set; }

        /// <summary>
        /// 所有产品名称
        /// </summary>
        public List<string> ProductNames { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 当前页尺寸
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 查询总数
        /// </summary>
        public int SearchCount { get; set; }
    }
}
