using System;
namespace Maticsoft.Model.Comment
{
	/// <summary>
	/// Comment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Comment
	{
		public Comment()
		{}
		#region Model
		private int _id;
		private int? _topicid;
		private string _description;
		private DateTime _createddate= DateTime.Now;
		private int _createduserid;
		private int _replycount;
		private int _parentid;
		private int _typeid;
		private bool _state;
		private bool _isread;
		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 主题
		/// </summary>
		public int? ContentId
		{
			set{ _topicid=value;}
			get{return _topicid;}
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
		/// 发布时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 发布人
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 回复数
		/// </summary>
		public int ReplyCount
		{
			set{ _replycount=value;}
			get{return _replycount;}
		}
		/// <summary>
		/// 父评论
		/// </summary>
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 审核状态
		/// </summary>
		public bool State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 是否已读
		/// </summary>
		public bool IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		#endregion Model

	}
}

