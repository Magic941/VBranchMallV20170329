using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Supplier
{
    public partial class WeChatMenu : Maticsoft.WeChat.Model.Core.Menu
    {
       public string MenuStatus { get; set; }
       public string MenuType { get; set; }
    }
}
