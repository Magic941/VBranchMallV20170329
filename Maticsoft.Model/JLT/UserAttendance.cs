/**
* UserAttendance.cs
*
* 功 能： N/A
* 类 名： UserAttendance
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/20 16:07:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.JLT
{
	/// <summary>
	/// 考勤信息表
	/// </summary>
	[Serializable]
	public partial class UserAttendance
	{
		public UserAttendance()
		{}
		#region Model
		private int _id;
		private int? _enterpriseid;
		private int _userid;
		private string _username;
		private string _truename;
		private string _latitude;
		private string _longitude;
		private string _address;
		private int? _kilometers;
		private int _typeid;
		private DateTime _createddate= DateTime.Now;
		private DateTime _attendancedate;
		private string _description;
		private string _imagepath;
		private int _score=0;
		private int _status=1;
		private int? _revieweduserid;
		private string _revieweddescription;
		private DateTime? _revieweddate;
		private int? _reviewedstatus;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EnterpriseID
		{
			set{ _enterpriseid=value;}
			get{return _enterpriseid;}
		}
		/// <summary>
		/// 出勤人ID
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 出勤人用户名（冗余字段）
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 出勤人真实姓名（冗余字段）
		/// </summary>
		public string TrueName
		{
			set{ _truename=value;}
			get{return _truename;}
		}
		/// <summary>
		/// 纬度
		/// </summary>
		public string Latitude
		{
			set{ _latitude=value;}
			get{return _latitude;}
		}
		/// <summary>
		/// 经度
		/// </summary>
		public string Longitude
		{
			set{ _longitude=value;}
			get{return _longitude;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Kilometers
		{
			set{ _kilometers=value;}
			get{return _kilometers;}
		}
		/// <summary>
		/// 出勤类型（外键）
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 出勤时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 出勤日期（年,月,日 冗余字段方便统计）
		/// </summary>
		public DateTime AttendanceDate
		{
			set{ _attendancedate=value;}
			get{return _attendancedate;}
		}
		/// <summary>
		/// 出勤描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 出勤照片
		/// </summary>
		public string ImagePath
		{
			set{ _imagepath=value;}
			get{return _imagepath;}
		}
		/// <summary>
		/// 绩效评分
		/// </summary>
		public int Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 状态 0：已作废 1：正常
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public int? ReviewedUserID
		{
			set{ _revieweduserid=value;}
			get{return _revieweduserid;}
		}
		/// <summary>
		/// 审核说明
		/// </summary>
		public string ReviewedDescription
		{
			set{ _revieweddescription=value;}
			get{return _revieweddescription;}
		}
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? ReviewedDate
		{
			set{ _revieweddate=value;}
			get{return _revieweddate;}
		}
		/// <summary>
		/// 出勤状态(审核状态) 0：旷工，1：正常，2：迟到，3：早退，4：请假半天，5：请假一天，6：出差，7：周六、日
		/// </summary>
		public int? ReviewedStatus
		{
			set{ _reviewedstatus=value;}
			get{return _reviewedstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

