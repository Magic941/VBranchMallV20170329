using System;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
	/// ProductQA:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductQA
	{
		public ProductQA()
		{}
		#region Model
		private int _qaid;
		private int? _parentid;
		private int _productid;
		private int _userid;
		private string _username;
		private string _question;
		private int _state;
		private DateTime? _createddate;
		private string _replycontent;
		private DateTime? _replydate;
		private int? _replyuserid;
		private string _replyusername;
		/// <summary>
		/// ID
		/// </summary>
		public int QAId
		{
			set{ _qaid=value;}
			get{return _qaid;}
		}
		/// <summary>
		/// 父ID
		/// </summary>
		public int? ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 产品ID
		/// </summary>
		public int ProductId
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 提问用户ID
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 提问用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 问题
		/// </summary>
		public string Question
		{
			set{ _question=value;}
			get{return _question;}
		}
		/// <summary>
		/// 状态 0：未审核 1：审核通过 2：审核不通过
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 提问时间
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 回复内容
		/// </summary>
		public string ReplyContent
		{
			set{ _replycontent=value;}
			get{return _replycontent;}
		}
		/// <summary>
		/// 回复时间
		/// </summary>
		public DateTime? ReplyDate
		{
			set{ _replydate=value;}
			get{return _replydate;}
		}
		/// <summary>
		/// 回复用户ID
		/// </summary>
		public int? ReplyUserId
		{
			set{ _replyuserid=value;}
			get{return _replyuserid;}
		}
		/// <summary>
		/// 回复用户名
		/// </summary>
		public string ReplyUserName
		{
			set{ _replyusername=value;}
			get{return _replyusername;}
		}
		#endregion Model

	}
}

