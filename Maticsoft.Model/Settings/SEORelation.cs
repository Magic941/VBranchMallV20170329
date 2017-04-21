/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SEORelation.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/10/15 10:50:22
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
	/// SEORelation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SEORelation
	{
		public SEORelation()
		{}
		#region Model
		private int _relationid;
		private string _keyname;
		private string _linkurl;
		private bool _iscms;
		private bool _isshop;
		private bool _issns;
		private bool _iscomment;
		private DateTime? _createddate= DateTime.Now;
		private bool _isactive;
		/// <summary>
		/// 
		/// </summary>
		public int RelationID
		{
			set{ _relationid=value;}
			get{return _relationid;}
		}
		/// <summary>
		/// 链接文字
		/// </summary>
		public string KeyName
		{
			set{ _keyname=value;}
			get{return _keyname;}
		}
		/// <summary>
		/// 链接地址
		/// </summary>
		public string LinkURL
		{
			set{ _linkurl=value;}
			get{return _linkurl;}
		}
		/// <summary>
		/// CMS
		/// </summary>
		public bool IsCMS
		{
			set{ _iscms=value;}
			get{return _iscms;}
		}
		/// <summary>
		/// Shop
		/// </summary>
		public bool IsShop
		{
			set{ _isshop=value;}
			get{return _isshop;}
		}
		/// <summary>
		/// SNS
		/// </summary>
		public bool IsSNS
		{
			set{ _issns=value;}
			get{return _issns;}
		}
		/// <summary>
		/// 评论
		/// </summary>
		public bool IsComment
		{
			set{ _iscomment=value;}
			get{return _iscomment;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 是否有效
		/// </summary>
		public bool IsActive
		{
			set{ _isactive=value;}
			get{return _isactive;}
		}
		#endregion Model

	}
}

