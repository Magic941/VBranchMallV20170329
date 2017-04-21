using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.SNS
{
    public class PostsPage
    {
            public List<Maticsoft.ViewModel.SNS.Posts> DataList = new List<ViewModel.SNS.Posts>();
            public List<Maticsoft.Model.SNS.AlbumType> AlbumTypeList=new List<Model.SNS.AlbumType>();
            public int PageSize{set ;get;}
            public string Type{get;set;}
            public int DataCount{set;get;}
            public  int UserID{set;get;}
            public string NickName { set; get; }
            public Maticsoft.Model.SNS.PostsSet Setting { set; get; }
    }
}
