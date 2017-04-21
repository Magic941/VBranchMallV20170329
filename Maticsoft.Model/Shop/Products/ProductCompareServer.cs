namespace Maticsoft.Model.Shop.Products
{
    public class ProductCompareServer
    {
        private string _attrName;

        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttrName
        {
            get { return _attrName; }
            set { _attrName = value; }
        }

        private string _product1;

        /// <summary>
        /// 对比商品1
        /// </summary>
        public string Product1
        {
            get { return _product1; }
            set { _product1 = value; }
        }

        private string _product2;

        /// <summary>
        /// 对比商品2
        /// </summary>
        public string Product2
        {
            get { return _product2; }
            set { _product2 = value; }
        }

        private string _product3;

        /// <summary>
        /// 对比商品3
        /// </summary>
        public string Product3
        {
            get { return _product3; }
            set { _product3 = value; }
        }

        private string _product4;

        /// <summary>
        /// 对比商品4
        /// </summary>
        public string Product4
        {
            get { return _product4; }
            set { _product4 = value; }
        }
    }
}