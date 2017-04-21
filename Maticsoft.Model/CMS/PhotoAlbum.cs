using System;

namespace Maticsoft.Model.CMS
{
	/// <summary>
	/// PhotoAlbum:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PhotoAlbum
	{
		public PhotoAlbum()
		{}
		#region Model
		private int _albumid;
		private string _albumname;
		private string _description;
		private int? _coverphoto;
		private int _state=0;
		private int _createduserid;
		private DateTime _createddate;
		private int _pvcount;
		private int _sequence;
		private int _privacy=0;
		private DateTime _lastupdateddate= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int AlbumID
		{
			set{ _albumid=value;}
			get{return _albumid;}
		}
		/// <summary>
		/// 相册名称
		/// </summary>
		public string AlbumName
		{
			set{ _albumname=value;}
			get{return _albumname;}
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
		/// 相册封面，取相册图片中的图片ID
		/// </summary>
		public int? CoverPhoto
		{
			set{ _coverphoto=value;}
			get{return _coverphoto;}
		}
		/// <summary>
		/// 审核状态
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
		/// 顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 0.公开；1仅自己可见； 2.仅好友可见。

		/// </summary>
		public int Privacy
		{
			set{ _privacy=value;}
			get{return _privacy;}
		}
		/// <summary>
		/// 最后更新日期
		/// </summary>
		public DateTime LastUpdatedDate
		{
			set{ _lastupdateddate=value;}
			get{return _lastupdateddate;}
		}
		#endregion Model

	}
}

