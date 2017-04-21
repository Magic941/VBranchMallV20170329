/**
* FellowTopics.cs
*
* 功 能： N/A
* 类 名： FellowTopics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:41   N/A    初版
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
	/// 用户关注的话题
	/// </summary>
	[Serializable]
	public partial class FellowTopics
	{
		public FellowTopics()
		{}
		#region Model
		private int _id;
		private string _topictitle;
		private int _createduserid;
		private DateTime _createddate;
		private int _status;
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
		public string TopicTitle
		{
			set{ _topictitle=value;}
			get{return _topictitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CreatedUserId
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
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
        /// 状态 0：不可用 1：可用
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

