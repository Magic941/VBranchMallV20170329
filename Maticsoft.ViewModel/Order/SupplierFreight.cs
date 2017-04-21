using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Order
{
    /*
     * 存放商家对应的物流方式
     * **/
    public class SupplierFreight
    {
        
        /// <summary>
        /// 商家ID
        /// </summary>
        public int SupplierId { get; set; }
        /// <summary>
        /// 运送方式ID
        /// </summary>
        public int ShippingTypeID { get; set; }
        /// <summary>
        /// 发票信息
        /// </summary>
        public string Remark { get; set; }
        public SupplierFreight() {
            SupplierId = 0;
            ShippingTypeID = 0;
            Remark = string.Empty;
        }
    }
}
