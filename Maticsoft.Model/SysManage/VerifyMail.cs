using System;
namespace Maticsoft.Model.SysManage
{
	/// <summary>
	/// VerifyMail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class VerifyMail
	{
		public VerifyMail()
		{}
		#region Model
		private string _username;
		private string _keyvalue;
		private DateTime _createddate;
		private int _status;
		private int? _validitytype;

		/// <summary>
		/// 用户ID 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 认证/验证码
		/// </summary>
		public string KeyValue
		{
			set{ _keyvalue=value;}
			get{return _keyvalue;}
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
		/// 0:邮箱验证未通过1：邮箱验证通过2：已过期
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}

		/// <summary>
		/// 0：邮箱验证 1：密码找回
		/// </summary>
		public int? ValidityType
		{
			set{ _validitytype=value;}
			get{return _validitytype;}
		}
		#endregion Model

	}
}

