/**
* SalesItem.cs
*
* 功 能： N/A
* 类 名： SalesItem
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 18:54:17   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Sales
{
	/// <summary>
	/// SalesItem:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SalesItem
	{
		public SalesItem()
		{}
		#region Model
		private int _itemid;
		private int _ruleid;
		private int _itemtype;
		private int _unitvalue;
		private int _ratevalue;
		/// <summary>
		/// 规则项ID
		/// </summary>
		public int ItemId
		{
			set{ _itemid=value;}
			get{return _itemid;}
		}
		/// <summary>
		/// 对应规则ID
		/// </summary>
		public int RuleId
		{
			set{ _ruleid=value;}
			get{return _ruleid;}
		}
		/// <summary>
		/// 规则类型 0：打折 1：减价 2：固定价格
		/// </summary>
		public int ItemType
		{
			set{ _itemtype=value;}
			get{return _itemtype;}
		}
		/// <summary>
		/// 单位数值 比如  100个或者 100元
		/// </summary>
		public int UnitValue
		{
			set{ _unitvalue=value;}
			get{return _unitvalue;}
		}
		/// <summary>
		/// 优惠数值
		/// </summary>
		public int RateValue
		{
			set{ _ratevalue=value;}
			get{return _ratevalue;}
		}
		#endregion Model

	}
}

