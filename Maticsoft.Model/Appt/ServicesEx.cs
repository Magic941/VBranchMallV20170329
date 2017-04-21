using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Model.Appt
{
    public partial class Services
    {
        private List<ProductImage> productImageList = new List<ProductImage>();
        /// <summary>
        /// 商品图片
        /// </summary>
        public List<ProductImage> ProductImages
        {
            get { return productImageList; }
            set { productImageList = value; }
        }
    }
}
