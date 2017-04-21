using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Products.Lucene
{
    /// <summary>
    /// 商品索引类型
    /// </summary>
    public enum  IndexType
    {
        /// <summary>
        ///插入索引
        /// </summary>
        Insert=0,
        /// <summary>
        ///修改索引
        /// </summary>
        Modify=1,

        /// <summary>
        ///删除索引
        /// </summary>
        Delete=2
    }
}
