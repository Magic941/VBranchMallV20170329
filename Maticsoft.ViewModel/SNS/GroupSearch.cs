using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.SNS
{
    /// <summary>
    /// 搜索小组model
    /// </summary>
    public  class GroupSearch
    {   
        //搜索的列表
        public Webdiyer.WebControls.Mvc.PagedList<Model.SNS.Groups> SearchList { set; get; }
        //推荐的列表
        public List<Maticsoft.Model.SNS.Groups> RecommandList { set; get; }
        //最热小组的列表
        public List<Maticsoft.Model.SNS.Groups> HotList { set; get; }

    }
}
