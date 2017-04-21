using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Maticsoft.ViewModel.Shop
{
    public class Express
    {
        public DateTime Date { set; get; }
        public string Content { set; get; }
       
    }

    public class ComType
    {
        public string ComName { set; get; }
        public string ComEn { set; get; }
        public bool Enabled { set; get; }
    }

    
}
