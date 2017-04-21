using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Supplier
{
    public partial class WeChatUser : Maticsoft.WeChat.Model.Core.User
    {
        public string GroupName{get;set;}
        public string UserStatus { get; set; }
    }
}
