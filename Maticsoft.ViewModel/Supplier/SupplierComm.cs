using System.Collections.Generic;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.ViewModel.Supplier
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
        /// 分页列表数据
        /// </summary>
        private PagedList<Model.Shop.Supplier.SupplierInfo> _supplierPagedList;

        /// <summary>
        /// 图片服务器地址
        /// </summary>
        /// 
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

        public PagedList<Model.Shop.Supplier.SupplierInfo> SupplierPagedList
        {
            get { return _supplierPagedList; }
            set { _supplierPagedList = value; }
        }
    }
}

