using System;
namespace Maticsoft.Model.Shop.Gift
{
	/// <summary>
	/// Gifts:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Gifts
	{
		public Gifts()
		{}
		#region Model
		private int _giftid;
		private int _categoryid;
		private string _name;
		private string _shortdescription;
		private string _unit;
		private int _weight;
		private string _longdescription;
		private string _title;
		private string _meta_description;
		private string _meta_keywords;
		private string _thumbnailsurl;
		private string _infocusimageurl;
		private decimal? _costprice;
		private decimal? _marketprice;
		private decimal? _saleprice;
		private int? _stock;
		private int _needpoint=0;
		private int _needgrade;
		private int _salecounts;
		private DateTime _createdate;
		private bool _enabled;
		/// <summary>
		/// 
		/// </summary>
		public int GiftId
		{
			set{ _giftid=value;}
			get{return _giftid;}
		}
		/// <summary>
		/// 分类ID
		/// </summary>
		public int CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 礼品名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 礼品简介
		/// </summary>
		public string ShortDescription
		{
			set{ _shortdescription=value;}
			get{return _shortdescription;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string Unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 重量
		/// </summary>
		public int Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 礼品详细信息
		/// </summary>
		public string LongDescription
		{
			set{ _longdescription=value;}
			get{return _longdescription;}
		}
		/// <summary>
		/// 礼品主题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Meta_Description
		{
			set{ _meta_description=value;}
			get{return _meta_description;}
		}
		/// <summary>
		/// 关键字
		/// </summary>
		public string Meta_Keywords
		{
			set{ _meta_keywords=value;}
			get{return _meta_keywords;}
		}
		/// <summary>
		/// 礼品图片
		/// </summary>
		public string ThumbnailsUrl
		{
			set{ _thumbnailsurl=value;}
			get{return _thumbnailsurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InFocusImageUrl
		{
			set{ _infocusimageurl=value;}
			get{return _infocusimageurl;}
		}
		/// <summary>
		/// 成本价
		/// </summary>
		public decimal? CostPrice
		{
			set{ _costprice=value;}
			get{return _costprice;}
		}
		/// <summary>
		/// 市场价
		/// </summary>
		public decimal? MarketPrice
		{
			set{ _marketprice=value;}
			get{return _marketprice;}
		}
		/// <summary>
		/// 销售价
		/// </summary>
		public decimal? SalePrice
		{
			set{ _saleprice=value;}
			get{return _saleprice;}
		}
		/// <summary>
		/// 库存
		/// </summary>
		public int? Stock
		{
			set{ _stock=value;}
			get{return _stock;}
		}
		/// <summary>
		/// 需要兑换积分
		/// </summary>
		public int NeedPoint
		{
			set{ _needpoint=value;}
			get{return _needpoint;}
		}
		/// <summary>
		/// 兑换所需等级 备注：预留字段（等级与积分没有关联关系时使用）
		/// </summary>
		public int NeedGrade
		{
			set{ _needgrade=value;}
			get{return _needgrade;}
		}
		/// <summary>
		/// 兑换量
		/// </summary>
		public int SaleCounts
		{
			set{ _salecounts=value;}
			get{return _salecounts;}
		}
		/// <summary>
		/// 上架时间
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 是否可用
		/// </summary>
		public bool Enabled
		{
			set{ _enabled=value;}
			get{return _enabled;}
		}
		#endregion Model

	}
}

