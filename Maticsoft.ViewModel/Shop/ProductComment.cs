using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Shop
{
   public class ProductComment
    {
        /// <summary>
        /// 评论对应的产品信息
        /// </summary>
        public Model.Shop.Products.ProductInfo ProductInfo { get; set; }

      /// <summary>
      /// 产品评论
      /// </summary>
      public Model.Shop.Products.ProductReviews Comment { get; set; }
    }
}
