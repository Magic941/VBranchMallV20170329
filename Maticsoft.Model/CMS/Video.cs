/**
* Video.cs
*
* 功 能： 
* 类 名： Video
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/22 16:28:49  蒋海滨    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.CMS
{
	/// <summary>
	/// 视频信息表
	/// </summary>
	[Serializable]
	public partial class Video
	{
		public Video()
		{}
		#region Model
		private int _videoid;
		private string _title;
		private string _description;
		private int? _albumid;
		private int _createduserid;
        private string _createdusername;
		private DateTime _createddate;
		private int? _lastupdateuserid;
        private string _lastupdateusername;
		private DateTime? _lastupdatedate;
		private int _sequence;
		private int? _videoclassid;
		private bool _isrecomend;
		private string _imageurl;
		private string _thumbimageurl;
		private string _normaimageurl;
		private int? _totaltime=0;
		private int _totalcomment=0;
		private int _totalfav=0;
		private int _totalup=0;
		private int _reference=0;
		private string _tags;
		private string _videourl;
		private int _urltype;
		private string _videoformat;
		private string _domain;
		private int _grade=0;
		private string _attachment;
		private int _privacy;
		private int _state;
		private string _remark;
        private int _pvcount;

		/// <summary>
		/// 视频编号
		/// </summary>
		public int VideoID
		{
			set{ _videoid=value;}
			get{return _videoid;}
		}
		/// <summary>
		/// 视频标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 专辑编号
		/// </summary>
		public int? AlbumID
		{
			set{ _albumid=value;}
			get{return _albumid;}
		}
		/// <summary>
		/// 视频发布者
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
        /// <summary>
        /// 视频创建者
        /// </summary>
        public string CreatedUserName
        {
            get { return _createdusername; }
            set { _createdusername = value; }
        }
		/// <summary>
		/// 视频发布时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 视频最后编辑者
		/// </summary>
		public int? LastUpdateUserID
		{
			set{ _lastupdateuserid=value;}
			get{return _lastupdateuserid;}
		}
        /// <summary>
        /// 视频最后编辑者
        /// </summary>
        public string LastUpdateUserName
        {
            get { return _lastupdateusername; }
            set { _lastupdateusername = value; }
        }
		/// <summary>
		/// 视频最后编辑时间
		/// </summary>
		public DateTime? LastUpdateDate
		{
			set{ _lastupdatedate=value;}
			get{return _lastupdatedate;}
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
		/// 视频分类
		/// </summary>
		public int? VideoClassID
		{
			set{ _videoclassid=value;}
			get{return _videoclassid;}
		}
		/// <summary>
		/// 是否推荐
		/// </summary>
		public bool IsRecomend
		{
			set{ _isrecomend=value;}
			get{return _isrecomend;}
		}
		/// <summary>
		/// 视频截图
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 视频缩略图
		/// </summary>
		public string ThumbImageUrl
		{
			set{ _thumbimageurl=value;}
			get{return _thumbimageurl;}
		}
		/// <summary>
		/// 视频备选缩略图
		/// </summary>
		public string NormalImageUrl
		{
			set{ _normaimageurl=value;}
			get{return _normaimageurl;}
		}
		/// <summary>
		/// 视频时长
		/// </summary>
		public int? TotalTime
		{
			set{ _totaltime=value;}
			get{return _totaltime;}
		}
		/// <summary>
		/// 视频评论数
		/// </summary>
		public int TotalComment
		{
			set{ _totalcomment=value;}
			get{return _totalcomment;}
		}
		/// <summary>
		/// 视频收藏数
		/// </summary>
		public int TotalFav
		{
			set{ _totalfav=value;}
			get{return _totalfav;}
		}
		/// <summary>
		/// 视频顶数
		/// </summary>
		public int TotalUp
		{
			set{ _totalup=value;}
			get{return _totalup;}
		}
		/// <summary>
		/// 视频引用数、分享数
		/// </summary>
		public int Reference
		{
			set{ _reference=value;}
			get{return _reference;}
		}
		/// <summary>
		/// 视频标签
		/// </summary>
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}
		/// <summary>
		/// 视频地址
		/// </summary>
		public string VideoUrl
		{
			set{ _videourl=value;}
			get{return _videourl;}
		}
		/// <summary>
		/// 视频类型
		/// </summary>
        public int UrlType
		{
			set{ _urltype=value;}
			get{return _urltype;}
		}
		/// <summary>
		/// 视频格式
		/// </summary>
		public string VideoFormat
		{
			set{ _videoformat=value;}
			get{return _videoformat;}
		}
		/// <summary>
		/// 网络视频域名
		/// </summary>
		public string Domain
		{
			set{ _domain=value;}
			get{return _domain;}
		}
		/// <summary>
		/// 评分
		/// </summary>
		public int Grade
		{
			set{ _grade=value;}
			get{return _grade;}
		}
		/// <summary>
		/// 附件
		/// </summary>
		public string Attachment
		{
			set{ _attachment=value;}
			get{return _attachment;}
		}
		/// <summary>
		/// 视频权限
		/// </summary>
		public int Privacy
		{
			set{ _privacy=value;}
			get{return _privacy;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// 浏览量
        /// </summary>
        public int PvCount
        {
            get { return _pvcount; }
            set { _pvcount = value; }
        }
		#endregion Model

	}
}

