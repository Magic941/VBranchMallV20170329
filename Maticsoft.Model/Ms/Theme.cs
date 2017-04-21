/**
* Theme.cs
*
* 功 能： N/A
* 类 名： Theme
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/12/27 15:56:14   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// Theme:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Theme
	{
		public Theme()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _description;
		private string _previewphotosrc;
		private string _zippackagesrc;
		private int? _themesize;
		private string _author;
		private bool _iscurrent;
		private string _language;
		private DateTime? _createddate= DateTime.Now;
		private string _remark;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		public string PreviewPhotoSrc
		{
			set{ _previewphotosrc=value;}
			get{return _previewphotosrc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZipPackageSrc
		{
			set{ _zippackagesrc=value;}
			get{return _zippackagesrc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ThemeSize
		{
			set{ _themesize=value;}
			get{return _themesize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Author
		{
			set{ _author=value;}
			get{return _author;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsCurrent
		{
			set{ _iscurrent=value;}
			get{return _iscurrent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

