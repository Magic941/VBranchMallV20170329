/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：UsersApprove.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/10/25 15:36:34
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Members
{
	/// <summary>
	/// UsersApprove:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UsersApprove
	{
		public UsersApprove()
		{}
		#region Model
		private int _approveid;
		private int _userid;
		private string _truename;
		private string _idcardnum;
		private string _frontview;
		private string _rearview;
		private DateTime? _duedate;
		private int _status;
		private int _approveuserid;
		private int? _usertype;
		private DateTime _createddate= DateTime.Now;
		private DateTime? _approvedate;
		/// <summary>
		/// 认证ID
		/// </summary>
		public int ApproveID
		{
			set{ _approveid=value;}
			get{return _approveid;}
		}
		/// <summary>
		/// 认证用户ID
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 真实姓名
		/// </summary>
		public string TrueName
		{
			set{ _truename=value;}
			get{return _truename;}
		}
		/// <summary>
		/// 身份证号码
		/// </summary>
		public string IDCardNum
		{
			set{ _idcardnum=value;}
			get{return _idcardnum;}
		}
		/// <summary>
		/// 身份证正面照
		/// </summary>
		public string FrontView
		{
			set{ _frontview=value;}
			get{return _frontview;}
		}
		/// <summary>
		/// 身份证背面照
		/// </summary>
		public string RearView
		{
			set{ _rearview=value;}
			get{return _rearview;}
		}
		/// <summary>
		/// 身份证过期时间
		/// </summary>
		public DateTime? DueDate
		{
			set{ _duedate=value;}
			get{return _duedate;}
		}
		/// <summary>
		/// 审核状态：0：未审核 1：审核通过  2：审核失败
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 审核人ID
		/// </summary>
		public int ApproveUserID
		{
			set{ _approveuserid=value;}
			get{return _approveuserid;}
		}
		/// <summary>
		/// 用户类型 0：会员 1 ：商户
		/// </summary>
		public int? UserType
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		/// <summary>
		/// 提交认证日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? ApproveDate
		{
			set{ _approvedate=value;}
			get{return _approvedate;}
		}
		#endregion Model

	}
}

