/**
* ShippingPayment.cs
*
* 功 能： N/A
* 类 名： ShippingPayment
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
	/// ShippingPayment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShippingPayment
	{
		public ShippingPayment()
		{}
		#region Model
		private int _shippingmodeid;
		private int _paymentmodeid;
		/// <summary>
		/// 物流类型
		/// </summary>
		public int ShippingModeId
		{
			set{ _shippingmodeid=value;}
			get{return _shippingmodeid;}
		}
		/// <summary>
		/// 支付方式
		/// </summary>
		public int PaymentModeId
		{
			set{ _paymentmodeid=value;}
			get{return _paymentmodeid;}
		}
		#endregion Model

	}
}

