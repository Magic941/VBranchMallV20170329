/**
* SupplierCategories.cs
*
* 功 能： N/A
* 类 名： SupplierCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:47   Ben    初版
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
	/// 供应商(店铺)分类
	/// </summary>
	[Serializable]
	public partial class SupplierCategories
	{
		public SupplierCategories()
		{}
		#region Model
		private int _categoryid;
		private string _name;
		private int _displaysequence;
		private string _meta_title;
		private string _meta_description;
		private string _meta_keywords;
		private string _description;
		private int? _parentcategoryid;
		private int _depth;
		private string _path;
		private string _imageurl;
		private string _theme;
		private bool _haschildren= false;
		private string _seourl;
		private string _seoimagealt;
		private string _seoimagetitle;
		private int _createduserid;
		private int _supplierid;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int CategoryId
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 供应商分类名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int DisplaySequence
		{
			set{ _displaysequence=value;}
			get{return _displaysequence;}
		}
		/// <summary>
		/// SEO页面标题
		/// </summary>
		public string Meta_Title
		{
			set{ _meta_title=value;}
			get{return _meta_title;}
		}
		/// <summary>
		/// SEO页面说明
		/// </summary>
		public string Meta_Description
		{
			set{ _meta_description=value;}
			get{return _meta_description;}
		}
		/// <summary>
		/// SEO页面关键字
		/// </summary>
		public string Meta_Keywords
		{
			set{ _meta_keywords=value;}
			get{return _meta_keywords;}
		}
		/// <summary>
		/// 说明
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 父分类
		/// </summary>
		public int? ParentCategoryId
		{
			set{ _parentcategoryid=value;}
			get{return _parentcategoryid;}
		}
		/// <summary>
		/// 层级
		/// </summary>
		public int Depth
		{
			set{ _depth=value;}
			get{return _depth;}
		}
		/// <summary>
		/// 节点路径
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		/// <summary>
		/// 图片
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 样式
		/// </summary>
		public string Theme
		{
			set{ _theme=value;}
			get{return _theme;}
		}
		/// <summary>
		/// 是否有子分类
		/// </summary>
		public bool HasChildren
		{
			set{ _haschildren=value;}
			get{return _haschildren;}
		}
		/// <summary>
		/// SEO Url地址优化
		/// </summary>
		public string SeoUrl
		{
			set{ _seourl=value;}
			get{return _seourl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SeoImageAlt
		{
			set{ _seoimagealt=value;}
			get{return _seoimagealt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SeoImageTitle
		{
			set{ _seoimagetitle=value;}
			get{return _seoimagetitle;}
		}
		/// <summary>
		/// 创建用户
		/// </summary>
		public int CreatedUserId
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 供应商Id
		/// </summary>
		public int SupplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

