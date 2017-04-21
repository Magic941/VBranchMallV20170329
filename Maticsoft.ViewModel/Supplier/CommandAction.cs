using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Supplier
{
    public partial class CommandAction : Maticsoft.WeChat.Model.Core.Command
    {
        public string ActionName { get; set; }
        public string ActionTarget { get; set; }
        public string ActionType { get; set; }
        public string ActionStatus { get; set; }
    }
}
