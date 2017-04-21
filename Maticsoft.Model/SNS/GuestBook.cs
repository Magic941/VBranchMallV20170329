/**
* GuestBook.cs
*
* 功 能： N/A
* 类 名： GuestBook
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:45   N/A    初版
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
	/// 留言
	/// </summary>
	[Serializable]
	public partial class GuestBook
	{
		public GuestBook()
		{}
		#region Model
		private int _guestbookid;
		private int _createuserid;
		private string _createnickname;
		private int _touserid;
		private string _tonickname;
		private int? _parentid=0;
		private string _description;
		private string _userip;
		private int? _privacy;
		private DateTime _createddate;
		private string _email;
		private string _path;
		private int? _depth;
		/// <summary>
		/// ID
		/// </summary>
		public int GuestBookID
		{
			set{ _guestbookid=value;}
			get{return _guestbookid;}
		}
		/// <summary>
		/// 发表留言者ID
		/// </summary>
		public int CreateUserID
		{
			set{ _createuserid=value;}
			get{return _createuserid;}
		}
		/// <summary>
		/// 发表留言的用户名
		/// </summary>
		public string CreateNickName
		{
			set{ _createnickname=value;}
			get{return _createnickname;}
		}
		/// <summary>
		/// 被留言者ID
		/// </summary>
		public int ToUserID
		{
			set{ _touserid=value;}
			get{return _touserid;}
		}
		/// <summary>
		/// 被留言者的用户名
		/// </summary>
		public string ToNickName
		{
			set{ _tonickname=value;}
			get{return _tonickname;}
		}
		/// <summary>
		/// 父评论的id 如果是0的话表示这条评论不是回复别人的
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		/// 发表者的Ip
		/// </summary>
		public string UserIP
		{
			set{ _userip=value;}
			get{return _userip;}
		}
		/// <summary>
		/// 回复的可见性
		/// </summary>
		public int? Privacy
		{
			set{ _privacy=value;}
			get{return _privacy;}
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
		/// 邮件
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 路径
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		/// <summary>
		/// 深度
		/// </summary>
		public int? Depth
		{
			set{ _depth=value;}
			get{return _depth;}
		}
		#endregion Model

	}
}

