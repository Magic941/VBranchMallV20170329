using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Supplier
{
    public class SupplierComm
    {
        /// <summary>
        /// 商家列表
        /// </summary>
        private List<Maticsoft.Model.Shop.Supplier.SupplierInfo> _supplierList;
        /// <summary>
        /// 品牌列表
        /// </summary>
        private List<Maticsoft.Model.Shop.Products.BrandInfo> _brandList;
        /// <summary>
        /// 图片服务器地址
        /// </summary>
        private string _serverURL = string.Empty;

        public List<Maticsoft.Model.Shop.Supplier.SupplierInfo> SupplierList
        {
            get { return _supplierList; }
            set { _supplierList = value; }
        }

        public List<Maticsoft.Model.Shop.Products.BrandInfo> BrandList
        {
            get { return _brandList; }
            set { _brandList = value; }
        }

        public string ServerURL
        {
            get { return _serverURL; }
            set { _serverURL = value; }
        }
    }
}
