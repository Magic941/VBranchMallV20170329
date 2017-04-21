/**  版本信息模板在安装目录下，可自行修改。
* Services.cs
*
* 功 能： N/A
* 类 名： Services
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/2 17:36:20   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Appt
{
	/// <summary>
	/// Services:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Services
	{
		public Services()
		{}
		#region Model
		private int _serviceid;
		private string _name;
		private DateTime _startdate;
		private DateTime _enddate;
		private bool _ispay;
		private int _servicetype;
		private int _ruletype;
		private int? _maxcount;
		private string _summary;
		private string _description;
		private string _imageurl;
		private string _thumbnailurl;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int ServiceId
		{
			set{ _serviceid=value;}
			get{return _serviceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ServiceType
		{
			set{ _servicetype=value;}
			get{return _servicetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RuleType
		{
			set{ _ruletype=value;}
			get{return _ruletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MaxCount
		{
			set{ _maxcount=value;}
			get{return _maxcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Summary
		{
			set{ _summary=value;}
			get{return _summary;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ThumbnailUrl
		{
			set{ _thumbnailurl=value;}
			get{return _thumbnailurl;}
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

