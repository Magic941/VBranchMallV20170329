using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.CustomModel
{
    public class SupplierPrice
    {
        public int SupplierId { get; set; }
        public decimal TotalSellPrice { get; set; }
        /// <summary>
        /// 活动类型 false :商家活动  true :全场
        /// </summary>
        public bool ActiveType { get; set; }
        /// <summary>
        /// 优惠类型 1：满折 2:满减
        /// </summary>
        public int PreferentialType { get; set; }
        /// <summary>
        /// 应用的规则名
        /// </summary>
        public string AMName { get; set; }
        public decimal TotalAdjustedPrice { get; set; }

        public decimal AMDUnitValue { get; set; }
        public decimal AMDRateValue { get; set; }
        /// <summary>
        /// 优惠额
        /// </summary>
        public decimal PreferentialValue { get; set; }

        public SupplierPrice()
        {
            this.SupplierId = 0;
            this.TotalSellPrice = 0;
            this.TotalAdjustedPrice = 0;
            this.ActiveType = false;
            this.PreferentialValue = 0;
        }
    }
}
