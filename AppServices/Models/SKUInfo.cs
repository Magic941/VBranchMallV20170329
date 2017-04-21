using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppServices.Models
{
  
    public  class SKUInfo
    {
        public SKUInfo()
        { }
        #region Model
        private long _skuid;
        private long _productid;
        private string _sku;
        private int? _weight;
        private int _stock;
        private int _alertstock;
        private decimal? _costprice = 0;
        private decimal? _costprice2 = 0;
        private decimal _saleprice;
        private bool _upselling;
        /// <summary>
        /// 
        /// </summary>
        public long SkuId
        {
            set { _skuid = value; }
            get { return _skuid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SKU
        {
            set { _sku = value; }
            get { return _sku; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Stock
        {
            set { _stock = value; }
            get { return _stock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AlertStock
        {
            set { _alertstock = value; }
            get { return _alertstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CostPrice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? CostPrice2
        {
            set { _costprice2 = value; }
            get { return _costprice2; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal SalePrice
        {
            set { _saleprice = value; }
            get { return _saleprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Upselling
        {
            set { _upselling = value; }
            get { return _upselling; }
        }
        #endregion Model

    }
}