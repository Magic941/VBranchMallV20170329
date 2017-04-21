using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Products.Lucene
{
    /// <summary>
    /// 商品索引接口基类型,所有返回该类型均继承该类型
    /// wusg 20140714
    /// </summary>
    public  class ProductIndexAPIBaseModel
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }

    }
}
