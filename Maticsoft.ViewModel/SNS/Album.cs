using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Maticsoft.ViewModel.SNS
{

    /// <summary>
    /// 首页专辑对象
    /// </summary>
    public class AlbumIndex : Model.SNS.UserAlbums
    {
        public AlbumIndex(Model.SNS.UserAlbums ua)
        {
            this.AlbumID = ua.AlbumID;
            this.AlbumName = ua.AlbumName;
            this.ChannelSequence = ua.ChannelSequence;
            this.CoverPhotoUrl = ua.CoverPhotoUrl;
            this.CoverTargetID = ua.CoverTargetID;
            this.CoverTargetType = ua.CoverTargetType;
            this.CreatedDate = ua.CreatedDate;
            this.CreatedNickName = ua.CreatedNickName;
            this.CreatedUserID = ua.CreatedUserID;
            this.Description = ua.Description;
            this.FavouriteCount = ua.FavouriteCount;
            this.IsRecommend = ua.IsRecommend;
            this.LastUpdatedDate = ua.LastUpdatedDate;
            this.PhotoCount = ua.PhotoCount;
            this.Privacy = ua.Privacy;
            this.PVCount = ua.PVCount;
            this.Sequence = ua.Sequence;
            this.Status = ua.Status;
            this.Tags = ua.Tags;
        }

        private List<string> _topimages;

        /// <summary>
        /// 专辑封面的图片
        /// </summary>
        public List<string> TopImages
        {
            set { _topimages = value; }
            get { return _topimages; }
        }
    }

    public class CreateAlbum
    {
        [Required(ErrorMessage = "请输入专辑名称")]
        [Display(Name = "专辑名称")]
        public string AlbumName { get; set; }

        [Display(Name = "专辑描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "专辑类型")]
        public string Type { get; set; }

        [Display(Name = "专辑标签")]
        public string Tags { get; set; }

        [Required(ErrorMessage = "请选择专辑类型")]
        [Display(Name = "专辑类型")]
        public List<SelectListItem> TypeList { get; set; }
    }
}
