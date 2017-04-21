using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Shop
{
    public class CardModel
    {
        public Model.Shop_CardUserInfo CardUserInfo { get; set; }

        public Model.Shop_CardType CardType { get; set; }

        public Model.Shop_Card CardInfo { get; set; }
    }
}
