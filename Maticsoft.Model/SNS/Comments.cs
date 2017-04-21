/**
* Comments.cs
*
* 功 能： N/A
* 类 名： Comments
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:40   N/A    初版
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
	/// 评论
	/// </summary>
	[Serializable]
	public partial class Comments
	{
		public Comments()
		{}
		#region Model
		private int _commentid;
		private int _type;
		private int _targetid;
		private int? _parentid=0;
		private int _createduserid;
		private string _creatednickname;
		private bool _hasreferuser;
		private string _description;
		private bool _isread= false;
		private int _status=1;
		private int _replycount;
		private string _userip;
		private DateTime _createddate= DateTime.Now;
		/// <summary>
		/// 主键id
		/// </summary>
		public int CommentID
		{
			set{ _commentid=value;}
			get{return _commentid;}
		}
		/// <summary>
        ///评论目标的类型 0：是一般动态的评论 1：照片的评论 2 是商品的评论 3. 专辑的评论 4.长微博的评论
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 对应的上面type的id
		/// </summary>
		public int TargetId
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 父评论的id
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 发表评论者的ID
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 发表评论者用户名
		/// </summary>
		public string CreatedNickName
		{
			set{ _creatednickname=value;}
			get{return _creatednickname;}
		}
		/// <summary>
		/// 是否提到某人
		/// </summary>
		public bool HasReferUser
		{
			set{ _hasreferuser=value;}
			get{return _hasreferuser;}
		}
		/// <summary>
		/// 评论的内容
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 是否已读 0为未读 1为已读
		/// </summary>
		public bool IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		/// <summary>
        ///状态：0:未审核 1：已审核 2：审核未通过
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ReplyCount
		{
			set{ _replycount=value;}
			get{return _replycount;}
		}
		/// <summary>
		/// IP
		/// </summary>
		public string UserIP
		{
			set{ _userip=value;}
			get{return _userip;}
		}
		/// <summary>
		/// 创建的时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		#endregion Model

        #region 扩展属性
        public  Maticsoft.Model.SNS.UserBlog UserBlog=new UserBlog();
        #endregion 
    }
}

