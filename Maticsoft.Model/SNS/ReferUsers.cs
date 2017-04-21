/**
* ReferUsers.cs
*
* 功 能： N/A
* 类 名： ReferUsers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:51   N/A    初版
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
	/// 提到某人的记录
	/// </summary>
	[Serializable]
	public partial class ReferUsers
	{
		public ReferUsers()
		{}
		#region Model
		private int _id;
		private int _tagetid;
		private int _type;
		private int _referuserid;
		private string _refernickname;
		private DateTime _createddate;
		private bool _isread;
		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 对应评论或者是动态的id
		/// </summary>
		public int TagetID
		{
			set{ _tagetid=value;}
			get{return _tagetid;}
		}
		/// <summary>
		/// 0:动态 1:评论
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 提到某人的id
		/// </summary>
		public int ReferUserID
		{
			set{ _referuserid=value;}
			get{return _referuserid;}
		}
		/// <summary>
		/// 提到某人的用户名
		/// </summary>
        public string ReferNickName
		{
			set{ _refernickname=value;}
			get{return _refernickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		#endregion Model

	}
}

