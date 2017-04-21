/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Brands.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/06/12 10:02:40
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
    /// BrandInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
	public partial class BrandInfo
	{
		public BrandInfo()
		{}
		#region Model
		private int _brandid;
		private string _brandname;
		private string _brandspell;
		private string _meta_description;
		private string _meta_keywords;
		private string _logo;
		private string _companyurl;
		private string _description;
		private int _displaysequence;
		private string _theme;
        private string _BrandsThumbs;
        //缩小图
        public string BrandsThumbs
        {
            get { return _BrandsThumbs; }
            set { _BrandsThumbs = value; }
        }
        private IList<int> _productTypeIdOrBrandsId;

        public IList<int> ProductTypeIdOrBrandsId
        {
            get { return _productTypeIdOrBrandsId; }
            set { _productTypeIdOrBrandsId = value; }
        }

        private IList<int> productTypes;
		/// <summary>
		/// 
		/// </summary>
		public int BrandId
		{
			set{ _brandid=value;}
			get{return _brandid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BrandName
		{
			set{ _brandname=value;}
			get{return _brandname;}
		}
		/// <summary>
		/// 拼音
		/// </summary>
		public string BrandSpell
		{
			set{ _brandspell=value;}
			get{return _brandspell;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Meta_Description
		{
			set{ _meta_description=value;}
			get{return _meta_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Meta_Keywords
		{
			set{ _meta_keywords=value;}
			get{return _meta_keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyUrl
		{
			set{ _companyurl=value;}
			get{return _companyurl;}
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
		public int DisplaySequence
		{
			set{ _displaysequence=value;}
			get{return _displaysequence;}
		}
		/// <summary>
        /// 品牌的样式
		/// </summary>
		public string Theme
		{
			set{ _theme=value;}
			get{return _theme;}
		}

        public IList<int> ProductTypes
        {
            get
            {
                if (this.productTypes == null)
                {
                    this.productTypes = new List<int>();
                }
                return this.productTypes;
            }
            set
            {
                this.productTypes = value;
            }
        }
		#endregion Model

	}
}

