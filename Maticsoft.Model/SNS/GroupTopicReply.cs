/**
* GroupTopicReply.cs
*
* 功 能： N/A
* 类 名： GroupTopicReply
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:42   N/A    初版
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
	/// 主题回复
	/// </summary>
	[Serializable]
	public partial class GroupTopicReply
	{
		public GroupTopicReply()
		{}
		#region Model
		private int _replyid;
		private int _groupid;
		private int _replytype;
		private string _replynickname;
		private int _replyuserid;
		private int _originalid;
		private string _orginaldes;
		private int _orginaluserid;
		private string _orgianlnickname;
		private int _topicid;
		private string _description;
		private bool _hasreferusers;
		private string _photourl;
		private int? _targetid;
		private int? _type;
		private string _producturl;
		private string _productname;
		private string _replyexurl;
		private string _productlinkurl;
		private int _favcount;
		private decimal? _price;
		private string _userip;
		private int _status;
		private DateTime _createddate;
		/// <summary>
		/// 回复主题的id
		/// </summary>
		public int ReplyID
		{
			set{ _replyid=value;}
			get{return _replyid;}
		}
		/// <summary>
		/// 对于的小组id
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 回复的类型（根据一般文字或有商品或图片分类）
		/// </summary>
		public int ReplyType
		{
			set{ _replytype=value;}
			get{return _replytype;}
		}
		/// <summary>
		/// 回应者用户名
		/// </summary>
		public string ReplyNickName
		{
			set{ _replynickname=value;}
			get{return _replynickname;}
		}
		/// <summary>
		/// 回应着ID
		/// </summary>
		public int ReplyUserID
		{
			set{ _replyuserid=value;}
			get{return _replyuserid;}
		}
		/// <summary>
		/// 原始ID
		/// </summary>
		public int OriginalID
		{
			set{ _originalid=value;}
			get{return _originalid;}
		}
		/// <summary>
		/// 原始回应的内容
		/// </summary>
		public string OrginalDes
		{
			set{ _orginaldes=value;}
			get{return _orginaldes;}
		}
		/// <summary>
		/// 原始回应者ID
		/// </summary>
		public int OrginalUserID
		{
			set{ _orginaluserid=value;}
			get{return _orginaluserid;}
		}
		/// <summary>
		/// 原始回应者name
		/// </summary>
		public string OrgianlNickName
		{
			set{ _orgianlnickname=value;}
			get{return _orgianlnickname;}
		}
		/// <summary>
		/// 主题id
		/// </summary>
		public int TopicID
		{
			set{ _topicid=value;}
			get{return _topicid;}
		}
		/// <summary>
		/// 回复的内容
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 是否提到某人
		/// </summary>
		public bool HasReferUsers
		{
			set{ _hasreferusers=value;}
			get{return _hasreferusers;}
		}
		/// <summary>
		/// 图片的url
		/// </summary>
		public string PhotoUrl
		{
			set{ _photourl=value;}
			get{return _photourl;}
		}
		/// <summary>
		/// 对应图片或商品的id(后加的)
		/// </summary>
		public int? TargetId
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 对应图片或商品的类型(0为图片1.为商品)(后加的)
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 商品图片的url
		/// </summary>
		public string ProductUrl
		{
			set{ _producturl=value;}
			get{return _producturl;}
		}
		/// <summary>
		/// 如果是商品，商品的名称
		/// </summary>
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 其他的url(预留字段)
		/// </summary>
		public string ReplyExUrl
		{
			set{ _replyexurl=value;}
			get{return _replyexurl;}
		}
		/// <summary>
		/// 商品链接的地址
		/// </summary>
		public string ProductLinkUrl
		{
			set{ _productlinkurl=value;}
			get{return _productlinkurl;}
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
		/// 商品的价格
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// ip
		/// </summary>
		public string UserIP
		{
			set{ _userip=value;}
			get{return _userip;}
		}
		/// <summary>
        /// 状态0:未审核：1：已审核 2：审核未通过
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 发表的日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		#endregion Model

	}
}

