using System;
namespace Maticsoft.Model.Members
{
	/// <summary>
	/// Feedback:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Feedback
	{
		public Feedback()
		{}
		#region Model
		private int _feedbackid;
		private int _typeid;
		private string _description;
		private string _username;
		private string _usersex;
		private string _useremail;
		private string _phone;
		private string _telphone;
		private string _usercompany;
		private string _userip;
		private bool _issolved= false;
		private DateTime _createddate;
		private string _result;
		private int? _status;
		private string _remark;
		private string _extdata;
		/// <summary>
		/// 
		/// </summary>
		public int FeedbackId
		{
			set{ _feedbackid=value;}
			get{return _feedbackid;}
		}
		/// <summary>
		/// 反馈类型ID
		/// </summary>
		public int TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 反馈内容
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 用户性别
		/// </summary>
		public string UserSex
		{
			set{ _usersex=value;}
			get{return _usersex;}
		}
		/// <summary>
		/// 用户邮箱
		/// </summary>
		public string UserEmail
		{
			set{ _useremail=value;}
			get{return _useremail;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string TelPhone
		{
			set{ _telphone=value;}
			get{return _telphone;}
		}
		/// <summary>
		/// 用户公司名
		/// </summary>
		public string UserCompany
		{
			set{ _usercompany=value;}
			get{return _usercompany;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserIP
		{
			set{ _userip=value;}
			get{return _userip;}
		}
		/// <summary>
		/// 是否已解决
		/// </summary>
		public bool IsSolved
		{
			set{ _issolved=value;}
			get{return _issolved;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 反馈结果
		/// </summary>
		public string Result
		{
			set{ _result=value;}
			get{return _result;}
		}
		/// <summary>
		/// 状态 是否公开 0：不公开 1：公开
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
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
		/// 反馈的附件 如：上传的图片，文件等
		/// </summary>
		public string ExtData
		{
			set{ _extdata=value;}
			get{return _extdata;}
		}
		#endregion Model

	}
}

