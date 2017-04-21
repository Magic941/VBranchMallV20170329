using System;
namespace Maticsoft.Model.Poll
{
	/// <summary>
	/// 投票用户信息
	/// </summary>
	[Serializable]
	public class PollUsers
	{
		
		#region Model
		private int _userid;
		private string _username;
		private byte[] _password;
		private string _truename;
		private int? _age;
		private string _sex;
		private string _phone;
		private string _email;
		private string _usertype;
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TrueName
		{
			set{ _truename=value;}
			get{return _truename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Age
		{
			set{ _age=value;}
			get{return _age;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserType
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		#endregion Model

	}
}

