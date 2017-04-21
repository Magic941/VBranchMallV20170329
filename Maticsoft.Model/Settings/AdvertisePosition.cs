/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：AdvertisePosition.cs
// 文件功能描述：
// 
// 创建标识： [孙鹏]  2012/05/31 13:22:19
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Settings
{
	/// <summary>
	/// AdvertisePosition:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AdvertisePosition
	{
		public AdvertisePosition()
		{}
		#region Model
		private int _advpositionid;
		private string _advpositionname;
		private int? _showtype;
		private int? _repeatcolumns;
		private int? _width;
		private int? _height;
		private string _advhtml;
		private bool _isone;
		private int? _timeinterval;
		private DateTime? _createddate;
		private int? _createduserid;
		/// <summary>
		/// 广告位编号

		/// </summary>
		public int AdvPositionId
		{
			set{ _advpositionid=value;}
			get{return _advpositionid;}
		}
		/// <summary>
		/// 广告位名称
		/// </summary>
		public string AdvPositionName
		{
			set{ _advpositionname=value;}
			get{return _advpositionname;}
		}
		/// <summary>
		/// 0：纵向平铺  1：横向平铺  2：层叠显示 3：交替显示  4：自定义广告代码
		/// </summary>
		public int? ShowType
		{
			set{ _showtype=value;}
			get{return _showtype;}
		}
		/// <summary>
		/// 横向平铺时 行显示个数
		/// </summary>
		public int? RepeatColumns
		{
			set{ _repeatcolumns=value;}
			get{return _repeatcolumns;}
		}
		/// <summary>
		/// 宽度
		/// </summary>
		public int? Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		/// <summary>
		/// 高度
		/// </summary>
		public int? Height
		{
			set{ _height=value;}
			get{return _height;}
		}
		/// <summary>
		/// 自定义代码
		/// </summary>
		public string AdvHtml
		{
			set{ _advhtml=value;}
			get{return _advhtml;}
		}
		/// <summary>
		/// 是否单一广告，还是循环广告
		/// </summary>
		public bool IsOne
		{
			set{ _isone=value;}
			get{return _isone;}
		}
		/// <summary>
		/// 循环广告 循环间隔

		/// </summary>
		public int? TimeInterval
		{
			set{ _timeinterval=value;}
			get{return _timeinterval;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		#endregion Model

	}
}

