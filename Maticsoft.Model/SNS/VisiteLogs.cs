/**
* VisiteLogs.cs
*
* 功 能： N/A
* 类 名： VisiteLogs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:09   N/A    初版
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
	/// 访问日志
	/// </summary>
	[Serializable]
	public partial class VisiteLogs
	{
		public VisiteLogs()
		{}
		#region Model
		private int _visitid;
		private int _fromuserid;
		private string _fromnickname;
		private int _touserid;
		private string _tonickname;
		private int? _visittimes=0;
		private DateTime _lastvisittime= DateTime.Now;
		/// <summary>
		/// 主键
		/// </summary>
		public int VisitID
		{
			set{ _visitid=value;}
			get{return _visitid;}
		}
		/// <summary>
		/// 访问者ID
		/// </summary>
		public int FromUserID
		{
			set{ _fromuserid=value;}
			get{return _fromuserid;}
		}
		/// <summary>
		/// 访问者的名字
		/// </summary>
		public string FromNickName
		{
			set{ _fromnickname=value;}
			get{return _fromnickname;}
		}
		/// <summary>
		/// 被访问者ID
		/// </summary>
		public int ToUserID
		{
			set{ _touserid=value;}
			get{return _touserid;}
		}
		/// <summary>
		/// 被访问者姓名
		/// </summary>
		public string ToNickName
		{
			set{ _tonickname=value;}
			get{return _tonickname;}
		}
		/// <summary>
		/// 访问的次数
		/// </summary>
		public int? VisitTimes
		{
			set{ _visittimes=value;}
			get{return _visittimes;}
		}
		/// <summary>
		/// 最后一次访问
		/// </summary>
		public DateTime LastVisitTime
		{
			set{ _lastvisittime=value;}
			get{return _lastvisittime;}
		}
		#endregion Model

	}
}

