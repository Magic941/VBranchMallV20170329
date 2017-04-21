using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.ViewModel.SNS
{
     public  class TopicReply
     {   
         public  Maticsoft.Model.Members.UsersExpModel TopicPostUser{set;get;}
         public   Maticsoft.Model.SNS.GroupTopics Topic {set; get;}
         public Maticsoft.Model.SNS.Groups Group { set; get; }
         public List<Maticsoft.Model.SNS.GroupTopics> UserPostTopics { set; get; }
         public List<Maticsoft.Model.SNS.Groups> UserJoinGroups { set; get; }
         public List<Maticsoft.Model.SNS.GroupTopics> HotTopic { set; get; }
         public PagedList<Maticsoft.Model.SNS.GroupTopicReply> TopicsReply { set; get; }

    }
}
