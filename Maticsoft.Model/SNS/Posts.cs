/**
* Posts.cs
*
* 功 能： N/A
* 类 名： Posts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:47   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 动态
	/// </summary>
	[Serializable]
	public partial class Posts
	{
		public Posts()
		{}
		#region Model
		private int _postid;
		private int _createduserid;
		private string _creatednickname;
		private int _originalid=0;
		private int? _forwardedid=0;
		private string _description;
		private bool _hasreferusers= false;
		private int _commentcount=0;
		private int _forwardcount=0;
		private int? _type=0;
		private string _postexurl;
		private string _videourl;
		private string _audiourl;
		private string _imageurl;
		private int _targetid=0;
		private string _topictitle= "0";
		private decimal? _price;
		private string _productlinkurl;
		private string _productname;
		private int? _favcount;
		private string _userip;
		private int _status=1;
		private DateTime _createddate= DateTime.Now;
		private bool _isrecommend;
		private int _sequence;
		private string _tags;
		/// <summary>
		/// 主键
		/// </summary>
		public int PostID
		{
			set{ _postid=value;}
			get{return _postid;}
		}
		/// <summary>
		/// 发表者
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 发表者昵称
		/// </summary>
		public string CreatedNickName
		{
			set{ _creatednickname=value;}
			get{return _creatednickname;}
		}
		/// <summary>
		/// 原始动态的ID
		/// </summary>
		public int OriginalID
		{
			set{ _originalid=value;}
			get{return _originalid;}
		}
		/// <summary>
		/// 被转发动态的ID
		/// </summary>
		public int? ForwardedID
		{
			set{ _forwardedid=value;}
			get{return _forwardedid;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 有没有引用别人 如@qihuqiang
		/// </summary>
		public bool HasReferUsers
		{
			set{ _hasreferusers=value;}
			get{return _hasreferusers;}
		}
		/// <summary>
		/// 回复的个数
		/// </summary>
		public int CommentCount
		{
			set{ _commentcount=value;}
			get{return _commentcount;}
		}
		/// <summary>
		/// 转发的数量
		/// </summary>
		public int ForwardCount
		{
			set{ _forwardcount=value;}
			get{return _forwardcount;}
		}
		/// <summary>
		/// 动态的类型(动态1.一般类型，2.图片(搭配和晒货)类型，3商品类型)
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 其他的url
		/// </summary>
		public string PostExUrl
		{
			set{ _postexurl=value;}
			get{return _postexurl;}
		}
		/// <summary>
		/// 视频的url
		/// </summary>
		public string VideoUrl
		{
			set{ _videourl=value;}
			get{return _videourl;}
		}
		/// <summary>
		/// 音频的url
		/// </summary>
		public string AudioUrl
		{
			set{ _audiourl=value;}
			get{return _audiourl;}
		}
		/// <summary>
		/// 图片
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 图片或者商品的id,对应的是photo表和商品表
		/// </summary>
		public int TargetId
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 话题的内容
		/// </summary>
		public string TopicTitle
		{
			set{ _topictitle=value;}
			get{return _topictitle;}
		}
		/// <summary>
		/// 如果是商品的话,有相应的价格
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 如果是商品的话,有相应的链接地址
		/// </summary>
		public string ProductLinkUrl
		{
			set{ _productlinkurl=value;}
			get{return _productlinkurl;}
		}
		/// <summary>
		/// 商品的名称
		/// </summary>
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FavCount
		{
			set{ _favcount=value;}
			get{return _favcount;}
		}
		/// <summary>
		/// IP地址
		/// </summary>
		public string UserIP
		{
			set{ _userip=value;}
			get{return _userip;}
		}
		/// <summary>
        /// 状态 0:未审核 1：已审核  3：审核未通过 
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 是否被推荐
		/// </summary>
		public bool IsRecommend
		{
			set{ _isrecommend=value;}
			get{return _isrecommend;}
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
		/// 标签
		/// </summary>
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}
		#endregion Model

	}
}

