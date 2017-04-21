using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Products.Lucene
{
    /// <summary>
    /// 商品索引类型,供本地存储使用
    /// </summary>
    public class ProductIndexLocalData
    {
        public long ProductId { get; set; }

        public ProductIndexEnum.EnumProductIndexAction ProductIndexActionType { get; set; }
    }
}
