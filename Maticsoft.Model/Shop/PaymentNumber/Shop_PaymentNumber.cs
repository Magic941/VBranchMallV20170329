/**  版本信息模板在安装目录下，可自行修改。
* Shop_PaymentNumber.cs
*
* 功 能： N/A
* 类 名： Shop_PaymentNumber
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/8/5 15:08:00   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.PaymentNumber
{
	/// <summary>
	/// Shop_PaymentNumber:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Shop_PaymentNumber
	{
		public Shop_PaymentNumber()
		{}
		#region Model
		private int _id;
		private long _orderid;
		private string _ordercode;
		private long _parentorderid;
		private string _swiftnumber;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderCode
		{
			set{ _ordercode=value;}
			get{return _ordercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ParentOrderId
		{
			set{ _parentorderid=value;}
			get{return _parentorderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SwiftNumber
		{
			set{ _swiftnumber=value;}
			get{return _swiftnumber;}
		}
		#endregion Model

	}
}

