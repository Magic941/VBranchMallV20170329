/**
* HotWords.cs
*
* 功 能： N/A
* 类 名： HotWords
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:53   N/A    初版
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
	/// 搜素关键字
	/// </summary>
	[Serializable]
	public partial class HotWords
	{
		public HotWords()
		{}
		#region Model
		private int _id;
		private string _keyword;
		private DateTime _createddate;
		private bool _isrecommend;
		private int _sequence;
		private int _status;
		/// <summary>
		/// 流水id
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 关键字
		/// </summary>
		public string KeyWord
		{
			set{ _keyword=value;}
			get{return _keyword;}
		}
		/// <summary>
		/// 产生的日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 是否被推荐
		/// </summary>
		public bool IsRecommend
		{
			set{ _isrecommend=value;}
			get{return _isrecommend;}
		}
		/// <summary>
		/// 推荐时显示的顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
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

