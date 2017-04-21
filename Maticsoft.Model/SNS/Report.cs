/**
* Report.cs
*
* 功 能： N/A
* 类 名： Report
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
	/// 举报
	/// </summary>
	[Serializable]
	public partial class Report
	{
		public Report()
		{}
		#region Model
		private int _id;
		private int _reporttypeid;
		private int _targettype;
		private int _targetid;
		private int _createduserid;
		private string _creatednickname;
		private string _description;
		private DateTime _createddate;
		private int? _status;
		/// <summary>
		/// id
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 检举的类型
		/// </summary>
		public int ReportTypeID
		{
			set{ _reporttypeid=value;}
			get{return _reporttypeid;}
		}
		/// <summary>
		/// 举报内容的类型(图片或者商品或者动态)
		/// </summary>
		public int TargetType
		{
			set{ _targettype=value;}
			get{return _targettype;}
		}
		/// <summary>
		/// 举报内容的id
		/// </summary>
		public int TargetID
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 检举人id
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 检举人姓名
		/// </summary>
		public string CreatedNickName
		{
			set{ _creatednickname=value;}
			get{return _creatednickname;}
		}
		/// <summary>
		/// 举报原因
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 举报产生的日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 状态(0：未处理,1： 已处理)
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

