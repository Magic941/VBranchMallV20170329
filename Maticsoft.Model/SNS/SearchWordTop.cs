/**
* SearchWordTop.cs
*
* 功 能： N/A
* 类 名： SearchWordTop
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:45   N/A    初版
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
	/// 热搜排行
	/// </summary>
	[Serializable]
	public partial class SearchWordTop
	{
		public SearchWordTop()
		{}
		#region Model
		private int _id;
		private string _hotword;
		private int _timeunit;
		private DateTime? _datestart;
		private DateTime? _dateend;
		private int _searchcount;
		private DateTime _createddate;
		private int _status;
		/// <summary>
		/// 主键
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 热词
		/// </summary>
		public string HotWord
		{
			set{ _hotword=value;}
			get{return _hotword;}
		}
		/// <summary>
		/// 0: 天(DateStart DateEnd的值相等) 1:周 2:月 3:年
		/// </summary>
		public int TimeUnit
		{
			set{ _timeunit=value;}
			get{return _timeunit;}
		}
		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime? DateStart
		{
			set{ _datestart=value;}
			get{return _datestart;}
		}
		/// <summary>
		/// 截止日期
		/// </summary>
		public DateTime? DateEnd
		{
			set{ _dateend=value;}
			get{return _dateend;}
		}
		/// <summary>
		/// 搜索的数量
		/// </summary>
		public int SearchCount
		{
			set{ _searchcount=value;}
			get{return _searchcount;}
		}
		/// <summary>
		/// 创建的日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
        /// 状态 0:不可用 1：可用
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

