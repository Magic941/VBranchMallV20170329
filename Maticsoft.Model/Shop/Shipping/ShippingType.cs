/**
* ShippingType.cs
*
* 功 能： N/A
* 类 名： ShippingType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 10:24:45   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Shipping
{
	/// <summary>
	/// ShippingType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShippingType
	{
		public ShippingType()
		{}
		#region Model
		private int _modeid;
		private string _name;
		private int _weight;
		private int? _addweight;
		private decimal _price;
		private decimal? _addprice;
		private string _description;
		private int _displaysequence;
		private string _expresscompanyname;
		private string _expresscompanyen;
		/// <summary>
		/// 物流方式
		/// </summary>
		public int ModeId
		{
			set{ _modeid=value;}
			get{return _modeid;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		/// 增重
		/// </summary>
		public int? AddWeight
		{
			set{ _addweight=value;}
			get{return _addweight;}
		}
		/// <summary>
		/// 价格
		/// </summary>
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 增价
		/// </summary>
		public decimal? AddPrice
		{
			set{ _addprice=value;}
			get{return _addprice;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
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
		/// 物流公司名称
		/// </summary>
		public string ExpressCompanyName
		{
			set{ _expresscompanyname=value;}
			get{return _expresscompanyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExpressCompanyEn
		{
			set{ _expresscompanyen=value;}
			get{return _expresscompanyen;}
		}
		#endregion Model

	}
}

