using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Maticsoft.ViewModel.SNS
{
   public class Star
    {
       public Maticsoft.Model.SNS.Star StarModel { get; set; }
       public List<SelectListItem> DropList = new List<SelectListItem>();
    }
   public class StarType
   {
       public List<Maticsoft.Model.SNS.StarType> StarTypeList = new List<Model.SNS.StarType>();
   }

   public class StarRank : Model.SNS.StarRank
   {
       public StarRank(Model.SNS.StarRank sr)
        {
            this.CreatedDate = sr.CreatedDate;
            this.ID = sr.ID;
            this.EndDate = sr.EndDate;
            this.IsRecommend = sr.IsRecommend;
            this.NickName = sr.NickName;
            this.RankDate = sr.RankDate;
            this.Sequence = sr.Sequence;
            this.StartDate = sr.StartDate;
            this.Status = sr.Status;
            this.TimeUnit = sr.TimeUnit;
            this.TypeId = sr.TypeId;
            this.UserId = sr.UserId;
            this.UserGravatar = sr.UserGravatar;
        }

       private int _fansCount;

       private int _favouritesCount;

       private int _productsCount;

       private string _singature;

       private bool _isFellow;

       /// <summary>
       /// 是否被关注
       /// </summary>
       public bool IsFellow
       {
           set { _isFellow = value; }
           get { return _isFellow; }
       }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FansCount
        {
            set { _fansCount = value; }
            get { return _fansCount; }
        }
        /// <summary>
       /// 喜欢数
       /// </summary>
       public int FavouritesCount
       {
           set { _favouritesCount = value; }
           get { return _favouritesCount; }
       }
       /// <summary>
       /// 宝贝数
       /// </summary>
       public int ProductsCount
       {
           set { _productsCount = value; }
           get { return _productsCount; }
       }
        /// <summary>
        /// 个性签名
        /// </summary>
       public string Singature
       {
           set { _singature = value; }
           get { return _singature; }
       }
   }

   public class ViewStar : Model.SNS.Star
   {
       public ViewStar(Model.SNS.Star sr)
       {
           this.CreatedDate = sr.CreatedDate;
           this.ID = sr.ID;
           this.ExpiredDate = sr.ExpiredDate;
           this.ApplyReason = sr.ApplyReason;
           this.NickName = sr.NickName;
           this.Status = sr.Status;
           this.TypeID = sr.TypeID;
           this.UserID = sr.UserID;
           this.UserGravatar = sr.UserGravatar;
       }

       private int _fansCount;

       private int _favouritesCount;

       private int _productsCount;

       private string _singature;

       private bool _isFellow;

       /// <summary>
       /// 是否被关注
       /// </summary>
       public bool IsFellow
       {
           set { _isFellow = value; }
           get { return _isFellow; }
       }
       /// <summary>
       /// 粉丝数
       /// </summary>
       public int FansCount
       {
           set { _fansCount = value; }
           get { return _fansCount; }
       }
       /// <summary>
       /// 喜欢数
       /// </summary>
       public int FavouritesCount
       {
           set { _favouritesCount = value; }
           get { return _favouritesCount; }
       }
       /// <summary>
       /// 宝贝数
       /// </summary>
       public int ProductsCount
       {
           set { _productsCount = value; }
           get { return _productsCount; }
       }
       /// <summary>
       /// 个性签名
       /// </summary>
       public string Singature
       {
           set { _singature = value; }
           get { return _singature; }
       }

       private bool _isUserDPI;
       /// <summary>
       /// 是否实名认证
       /// </summary>
       public bool IsUserDPI
       {
           get { return _isUserDPI; }
           set { _isUserDPI = value; }
       }
   }
}
