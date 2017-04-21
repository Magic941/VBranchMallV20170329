using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.PromoteSales
{
    [Serializable]
    public class GroupBuyHistory
    {
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public string ProductName { get; set; }
        public int SaleCount { get; set; }

    }
}
