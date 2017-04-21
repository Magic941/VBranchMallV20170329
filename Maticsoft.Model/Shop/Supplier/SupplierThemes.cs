/**
* SupplierThemes.cs
*
* 功 能： N/A
* 类 名： SupplierThemes
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:51   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Supplier
{
	/// <summary>
	/// 供应商(店铺)模版
	/// </summary>
	[Serializable]
	public partial class SupplierThemes
	{
		public SupplierThemes()
		{}
		#region Model
		private int _themeid;
		private string _name;
		private string _description;
		private string _imageurl;
		private string _author;
		private string _website;
		private string _language;
		private DateTime _createddate= DateTime.Now;
		private DateTime? _updateddate;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int ThemeId
		{
			set{ _themeid=value;}
			get{return _themeid;}
		}
		/// <summary>
		/// 模版名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 模板描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 模版预览图
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 作者
		/// </summary>
		public string Author
		{
			set{ _author=value;}
			get{return _author;}
		}
		/// <summary>
		/// 作者网站
		/// </summary>
		public string WebSite
		{
			set{ _website=value;}
			get{return _website;}
		}
		/// <summary>
		/// 模板语言
		/// </summary>
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime? UpdatedDate
		{
			set{ _updateddate=value;}
			get{return _updateddate;}
		}
		/// <summary>
		/// 模板备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

