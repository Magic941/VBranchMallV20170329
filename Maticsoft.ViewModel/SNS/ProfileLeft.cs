
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.SNS
{
    public class ProfileLeft
    {
        public List<Maticsoft.Model.SNS.UserShip> shipList { get; set; }
        public List<Maticsoft.Model.SNS.Groups> joingroupList { get; set; }
        public List<Maticsoft.Model.SNS.Groups> creategroupList { get; set; }
    }

    public class SelfRight
    {
        public Maticsoft.Model.Members.UsersExpModel UserInfo { get; set; }
        public List<Maticsoft.Model.SNS.Groups> MyGroups { get; set; }
        public List<Maticsoft.ViewModel.SNS.AlbumIndex> MyAlbum { get; set; }
      
    }
}
