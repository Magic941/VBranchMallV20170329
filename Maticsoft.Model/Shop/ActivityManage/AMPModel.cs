using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.ActivityManage
{
    [Serializable]
    public partial class AMPModel
    {
        public AMPModel() { }

        #region Model
        private int _ampid;
        private int _amid;
        private int _productid;
        private string _productname;
        private int _supplierid;
        private int _giftsproid;
        private string _giftsproname;
        private string _productcode;

        private string _suppliername;
        private string _amname;

        public int AMPId
        {
            set { _ampid = value; }
            get { return _ampid; }
        }
        public int AMId
        {
            set { _amid = value; }
            get { return _amid; }
        }
        public int ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        public int SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        public int GiftsProId
        {
            set { _giftsproid = value; }
            get { return _giftsproid; }
        }
        public string GiftsProName
        {
            set { _giftsproname = value; }
            get { return _giftsproname; }
        }
        public string AMName
        {
            set { _amname = value; }
            get { return _amname; }
        }
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        #endregion
    }
}
