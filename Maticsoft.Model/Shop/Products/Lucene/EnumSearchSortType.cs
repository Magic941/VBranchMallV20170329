using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Products.Lucene
{
    /// <summary>
    /// 商品索引相关的枚举
    /// </summary>
    public static class ProductIndexEnum
    {
        /// <summary>
        /// 商品搜索的排序类型
        /// </summary>
        public enum EnumSearchSortType
        {

            Default = 0,
            /// <summary>
            /// 销量正排，从小到大
            /// </summary>
            SaleCountUp = 1,
            /// <summary>
            /// 销售倒排，从大到小
            /// </summary>
            SaleCountDown = 2,
            /// <summary>
            /// 上架时间,最新上架在后面
            /// </summary>
            AddedDateUp = 3,

            /// <summary>
            /// 上架时间,最新上架在前面
            /// </summary>
            AddedDateDown = 4,
            /// <summary>
            /// 商品售价,最便宜的在前面
            /// </summary>
            PriceUp = 5,

            /// <summary>
            /// 价格从最贵的到最便宜的
            /// </summary>
            PriceDown = 6

        }

        /// <summary>
        /// 商品索引动作类型
        /// </summary>
        public enum EnumProductIndexAction
        {

            Add = 1,
            Update = 2,
            Delete = 3

        }
    }
}
