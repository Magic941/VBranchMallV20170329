using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.SNS
{
   public  class Stars
    {
       //达人
       public List<ViewModel.SNS.ViewStar> StarList { get; set; }

       private Webdiyer.WebControls.Mvc.PagedList<ViewModel.SNS.ViewStar> _StarPagedList;
       public Webdiyer.WebControls.Mvc.PagedList<ViewModel.SNS.ViewStar> StarPagedList
       {
           get { return _StarPagedList; }
           set
           {
               _StarPagedList = value;
               if (value == null || value.Count < 1) return;
               List<ViewModel.SNS.ViewStar>[] list = new[] { new List<ViewModel.SNS.ViewStar>(), new List<ViewModel.SNS.ViewStar>(), new List<ViewModel.SNS.ViewStar>() };
               int index = 0;
               value.ForEach(Star =>
               {
                   //reset
                   if (index == 3) index = 0;
                   list[index++].Add(Star);
               });
               this.StarList3ForCol = list;
           }
       }
       public List<ViewModel.SNS.ViewStar>[] StarList3ForCol { get; set; }
       //明星达人
       public List<ViewModel.SNS.StarRank> HotStarList = new List<ViewModel.SNS.StarRank>();
       //新晋达人
       public List<ViewModel.SNS.ViewStar> StarNewList = new List<ViewModel.SNS.ViewStar>();
       //达人排行
       public List<ViewModel.SNS.StarRank> StarRankList = new List<ViewModel.SNS.StarRank>();
    }
}
