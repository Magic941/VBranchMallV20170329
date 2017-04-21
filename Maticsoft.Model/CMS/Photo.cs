using System;

namespace Maticsoft.Model.CMS
{
	/// <summary>
	/// Photo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Photo
	{
		public Photo()
		{}
		#region Model
		private int _photoid;
		private string _photoname;
		private string _imageurl;
		private string _description;
		private int _albumid;
		private int _state=0;
		private int _createduserid;
		private DateTime _createddate;
		private int _pvcount=0;
		private int _classid;
		private string _thumbimageurl;
		private string _normalimageurl;
		private int? _sequence;
		private bool? _isrecomend= false;
		private int? _commentcount=0;
		private string _tags;
        private string _createdusername;
	    private int _favouritecount;
		/// <summary>
		/// 
		/// </summary>
		public int PhotoID
		{
			set{ _photoid=value;}
			get{return _photoid;}
		}
		/// <summary>
		/// 图片名称
		/// </summary>
		public string PhotoName
		{
			set{ _photoname=value;}
			get{return _photoname;}
		}
		/// <summary>
		/// 原图文件名（大）
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 简介
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 相册ID
		/// </summary>
		public int AlbumID
		{
			set{ _albumid=value;}
			get{return _albumid;}
		}
		/// <summary>
        /// 状态：0：未审核；1：审核通过
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 创建用户ID
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 浏览量
		/// </summary>
		public int PVCount
		{
			set{ _pvcount=value;}
			get{return _pvcount;}
		}
		/// <summary>
		/// 图片分类
		/// </summary>
		public int ClassID
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		/// <summary>
		/// 缩略图（小）
		/// </summary>
		public string ThumbImageUrl
		{
			set{ _thumbimageurl=value;}
			get{return _thumbimageurl;}
		}
		/// <summary>
		/// 正常图（中）
		/// </summary>
		public string NormalImageUrl
		{
			set{ _normalimageurl=value;}
			get{return _normalimageurl;}
		}
		/// <summary>
		/// 顺序
		/// </summary>
		public int? Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 是否推荐
		/// </summary>
		public bool? IsRecomend
		{
			set{ _isrecomend=value;}
			get{return _isrecomend;}
		}
		/// <summary>
		/// 评论数
		/// </summary>
		public int? CommentCount
		{
			set{ _commentcount=value;}
			get{return _commentcount;}
		}
		/// <summary>
		/// 标签
		/// </summary>
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}
        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreatedUserName
        {
            get { return _createdusername; }
            set { _createdusername = value; }
        }
        /// <summary>
        /// 喜欢数
        /// </summary>
        public int FavouriteCount
	    {
            get { return _favouritecount; }
            set { _favouritecount = value; }
	    }
		#endregion Model

	}
}

