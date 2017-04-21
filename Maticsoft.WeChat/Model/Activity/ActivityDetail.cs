/**  版本信息模板在安装目录下，可自行修改。
* ActivityDetail.cs
*
* 功 能： N/A
* 类 名： ActivityDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:16   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Activity
{
	/// <summary>
	/// ActivityDetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ActivityDetail
	{
		public ActivityDetail()
		{}
		#region Model
		private long _detailid;
		private string _username;
		private int _activityid;
		private string _activityname;
		private DateTime _createdate;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public long DetailId
		{
			set{ _detailid=value;}
			get{return _detailid;}
		}
		/// <summary>
		/// 微信用户ID
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 活动ID
		/// </summary>
		public int ActivityId
		{
			set{ _activityid=value;}
			get{return _activityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActivityName
		{
			set{ _activityname=value;}
			get{return _activityname;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
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

