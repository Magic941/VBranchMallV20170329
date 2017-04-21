/**
* GroupTopics.cs
*
* 功 能： N/A
* 类 名： GroupTopics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:43   N/A    初版
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
	/// 小组话题
	/// </summary>
	[Serializable]
	public partial class GroupTopics
	{
		public GroupTopics()
		{}
		#region Model
		private int _topicid;
		private int _createduserid;
		private string _creatednickname;
		private int _groupid;
		private string _groupname;
		private string _title;
		private string _description;
		private int _isrecomend;
		private int _sequence;
		private int _replycount;
		private int _pvcount;
		private int _dingcount;
		private int _status;
		private int _istop;
		private bool _isactive;
		private bool _isadminrecommend;
		private int? _channelsequence;
		private bool _hasreferusers;
		private string _postexurl;
		private string _imageurl;
		private string _videourl;
		private string _audiourl;
		private string _productname;
		private decimal? _price;
		private string _productlinkurl;
		private int _type;
		private int? _targetid;
		private int _favcount;
		private DateTime _createddate;
		private int? _lastreplyuserid;
		private string _lastreplynickname;
		private DateTime? _lastposttime;
		private string _tags;
		/// <summary>
		/// 话题的id
		/// </summary>
		public int TopicID
		{
			set{ _topicid=value;}
			get{return _topicid;}
		}
		/// <summary>
		/// 话题发表者ID
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 话题发表者姓名
		/// </summary>
		public string CreatedNickName
		{
			set{ _creatednickname=value;}
			get{return _creatednickname;}
		}
		/// <summary>
		/// 对应的小组id
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 小组的名称
		/// </summary>
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 话题的标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
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
		/// 是否被小组管理员推荐
		/// </summary>
		public int IsRecomend
		{
			set{ _isrecomend=value;}
			get{return _isrecomend;}
		}
		/// <summary>
		/// 显示的顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 回复数量
		/// </summary>
		public int ReplyCount
		{
			set{ _replycount=value;}
			get{return _replycount;}
		}
		/// <summary>
		/// 浏览数量（热度）
		/// </summary>
		public int PvCount
		{
			set{ _pvcount=value;}
			get{return _pvcount;}
		}
		/// <summary>
		/// 顶的次数
		/// </summary>
		public int DingCount
		{
			set{ _dingcount=value;}
			get{return _dingcount;}
		}
		/// <summary>
        /// 状态 0:未审核 1：已审核 2：审核未通过
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 是否被顶置
		/// </summary>
		public int IsTop
		{
			set{ _istop=value;}
			get{return _istop;}
		}
		/// <summary>
		/// 是否是活动
		/// </summary>
		public bool IsActive
		{
			set{ _isactive=value;}
			get{return _isactive;}
		}
		/// <summary>
		/// 管理员是否推荐在小组首页的热门话题中显示(后加的)
		/// </summary>
		public bool IsAdminRecommend
		{
			set{ _isadminrecommend=value;}
			get{return _isadminrecommend;}
		}
		/// <summary>
		/// 在相应频道显示的顺序
		/// </summary>
		public int? ChannelSequence
		{
			set{ _channelsequence=value;}
			get{return _channelsequence;}
		}
		/// <summary>
		/// 主题中是否提到某人
		/// </summary>
		public bool HasReferUsers
		{
			set{ _hasreferusers=value;}
			get{return _hasreferusers;}
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
		/// 图片的路径(包括商品和照片的)
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
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
		/// 商品的名称
		/// </summary>
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 商品的价格
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 商品的购买链接
		/// </summary>
		public string ProductLinkUrl
		{
			set{ _productlinkurl=value;}
			get{return _productlinkurl;}
		}
		/// <summary>
		/// 0 :照片 1:商品
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 对应照片或商品的id
		/// </summary>
		public int? TargetID
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 喜欢的次数
		/// </summary>
		public int FavCount
		{
			set{ _favcount=value;}
			get{return _favcount;}
		}
		/// <summary>
		/// 话题发表时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LastReplyUserId
		{
			set{ _lastreplyuserid=value;}
			get{return _lastreplyuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LastReplyNickName
		{
			set{ _lastreplynickname=value;}
			get{return _lastreplynickname;}
		}
		/// <summary>
		/// 最后回应的时间
		/// </summary>
		public DateTime? LastPostTime
		{
			set{ _lastposttime=value;}
			get{return _lastposttime;}
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

