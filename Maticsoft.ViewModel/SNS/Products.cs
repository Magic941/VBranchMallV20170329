using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Model.SNS;

namespace Maticsoft.ViewModel.SNS
{
    public  class Products:Maticsoft.Model.SNS.Products
    {
        public List<Maticsoft.Model.SNS.Comments> commentlist;

    }

    public class ProductAlbum
    {
        public List<Maticsoft.Model.SNS.UserAlbums> UserAlbums = new List<UserAlbums>();
        public List<Maticsoft.Model.SNS.Categories> ProductCateList = new List<Maticsoft.Model.SNS.Categories>();
    }
}
