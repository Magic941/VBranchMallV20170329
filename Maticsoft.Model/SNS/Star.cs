/**
* Star.cs
*
* 功 能： N/A
* 类 名： Star
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:55   N/A    初版
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
	/// 达人
	/// </summary>
	[Serializable]
	public partial class Star
	{
		public Star()
		{}
		#region Model
		private int _id;
		private int _userid;
		private int _typeid;
		private string _nickname;
		private string _usergravatar;
		private string _applyreason;
		private DateTime _createddate;
		private DateTime? _expireddate;
		private int? _status;
		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户名id
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 对应达人的类型(是搭配达人或是别的达人)
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string NickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 达人的图片
		/// </summary>
		public string UserGravatar
		{
			set{ _usergravatar=value;}
			get{return _usergravatar;}
		}
		/// <summary>
		/// 申请达人的理由
		/// </summary>
		public string ApplyReason
		{
			set{ _applyreason=value;}
			get{return _applyreason;}
		}
		/// <summary>
		/// 申请的日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 达人过期时间
		/// </summary>
		public DateTime? ExpiredDate
		{
			set{ _expireddate=value;}
			get{return _expireddate;}
		}
		/// <summary>
		/// 状态(0,未审核 1.已审核 2,申请不成功,3 申请通过)
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

