using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Order
{
    public class OrderInfoExPage
    {
        public DateTime GeneratedDate { get; set; }
        public int Product { get; set; }
        public int ToalQuantity { get; set; }
        public string ProductName { get; set; }
        public string SKUs { get; set; }
    }
}
