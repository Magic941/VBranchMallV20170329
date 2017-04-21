/**
* ProductSources.cs
*
* 功 能： N/A
* 类 名： ProductSources
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:50   N/A    初版
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
	/// 商品来源
	/// </summary>
	[Serializable]
	public partial class ProductSources
	{
		public ProductSources()
		{}
		#region Model
		private int _id;
		private string _websitename;
		private string _websiteurl;
		private string _websitelogo;
		private string _categorytags;
		private string _pricetags;
		private string _imagestag;
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
		/// 商品来源网站的名称
		/// </summary>
		public string WebSiteName
		{
			set{ _websitename=value;}
			get{return _websitename;}
		}
		/// <summary>
		/// 商品来源网站的url
		/// </summary>
		public string WebSiteUrl
		{
			set{ _websiteurl=value;}
			get{return _websiteurl;}
		}
		/// <summary>
		/// 网站的log,在单品也链接到此网站时需要相应的logo
		/// </summary>
		public string WebSiteLogo
		{
			set{ _websitelogo=value;}
			get{return _websitelogo;}
		}
		/// <summary>
		/// 采集时商品类别匹配的正则表达式
		/// </summary>
		public string CategoryTags
		{
			set{ _categorytags=value;}
			get{return _categorytags;}
		}
		/// <summary>
		/// 采集时商品价格匹配的正则表达式
		/// </summary>
		public string PriceTags
		{
			set{ _pricetags=value;}
			get{return _pricetags;}
		}
		/// <summary>
		/// 采集时图片匹配的正则表达式
		/// </summary>
		public string ImagesTag
		{
			set{ _imagestag=value;}
			get{return _imagestag;}
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

