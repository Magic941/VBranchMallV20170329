using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.CMS
{
    public class Photo
    {
        private Webdiyer.WebControls.Mvc.PagedList<Model.CMS.Photo> _PhotoPagedList;
        public Webdiyer.WebControls.Mvc.PagedList<Model.CMS.Photo> PhotoPagedList
        {
            get { return _PhotoPagedList; }
            set
            {
                _PhotoPagedList = value;
                if (value == null || value.Count < 1) return;
                List<Model.CMS.Photo>[] list = new[] { new List<Model.CMS.Photo>(), new List<Model.CMS.Photo>(), new List<Model.CMS.Photo>(), new List<Model.CMS.Photo>() };
                int index = 0;
                value.ForEach(Photo =>
                {
                    //reset
                    if (index == 4) index = 0;
                    list[index++].Add(Photo);
                });
                this.PhotoList4ForCol = list;
            }
        }
        public List<Model.CMS.Photo>[] PhotoList4ForCol { get; set; }
        public List<Model.CMS.Photo> PhotoListWaterfall { get; set; }
        public List<Model.CMS.PhotoClass> PhotoClassList { get; set; }

    }
}
