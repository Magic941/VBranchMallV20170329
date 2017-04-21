/**
* InquiryItem.cs
*
* 功 能： N/A
* 类 名： InquiryItem
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/4 19:23:30   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Inquiry
{
	/// <summary>
	/// InquiryItem:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class InquiryItem
	{
		public InquiryItem()
		{}
		#region Model
		private long _itemid;
		private long _inquiryid;
		private long _targetid;
		private int _type;
		private string _productcode;
		private string _sku;
		private string _name;
		private string _thumbnailsurl;
		private string _description;
		private int _quantity;
		private decimal _costprice;
		private decimal _sellprice;
		private decimal _adjustedprice;
		private string _attribute;
		private string _remark;
		private int _weight;
		private decimal? _deduct;
		private int _points;
		private int? _productlineid;
		private int? _supplierid;
		private string _suppliername;
		/// <summary>
		/// 订单项目ID
		/// </summary>
		public long ItemId
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long InquiryId
		{
			set{ _inquiryid=value;}
			get{return _inquiryid;}
		}
		/// <summary>
		/// 商品ID
		/// </summary>
		public long TargetId
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 1 商品  2作品
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 商品条码
		/// </summary>
		public string ProductCode
		{
			set{ _productcode=value;}
			get{return _productcode;}
		}
		/// <summary>
		/// 商品SKU
		/// </summary>
		public string SKU
		{
			set{ _sku=value;}
			get{return _sku;}
		}
		/// <summary>
		/// 商品名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 商品缩略图
		/// </summary>
		public string ThumbnailsUrl
		{
			set{ _thumbnailsurl=value;}
			get{return _thumbnailsurl;}
		}
		/// <summary>
		/// 商品说明
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 商品数量
		/// </summary>
		public int Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 成本价
		/// </summary>
		public decimal CostPrice
		{
			set{ _costprice=value;}
			get{return _costprice;}
		}
		/// <summary>
		/// 原价
		/// </summary>
		public decimal SellPrice
		{
			set{ _sellprice=value;}
			get{return _sellprice;}
		}
		/// <summary>
		/// 调整后的价格
		/// </summary>
		public decimal AdjustedPrice
		{
			set{ _adjustedprice=value;}
			get{return _adjustedprice;}
		}
		/// <summary>
		/// 商品属性
		/// </summary>
		public string Attribute
		{
			set{ _attribute=value;}
			get{return _attribute;}
		}
		/// <summary>
		/// 项目备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 商品重量
		/// </summary>
		public int Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 扣除金额
		/// </summary>
		public decimal? Deduct
		{
			set{ _deduct=value;}
			get{return _deduct;}
		}
		/// <summary>
		/// 商品所赠的积分
		/// </summary>
		public int Points
		{
			set{ _points=value;}
			get{return _points;}
		}
		/// <summary>
		/// 商品线ID
		/// </summary>
		public int? ProductLineId
		{
			set{ _productlineid=value;}
			get{return _productlineid;}
		}
		/// <summary>
		/// 供货商ID
		/// </summary>
		public int? SupplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 供货商名称
		/// </summary>
		public string SupplierName
		{
			set{ _suppliername=value;}
			get{return _suppliername;}
		}
		#endregion Model

	}
}

