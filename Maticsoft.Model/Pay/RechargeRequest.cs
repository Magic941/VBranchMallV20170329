/**  版本信息模板在安装目录下，可自行修改。
* RechargeRequest.cs
*
* 功 能： N/A
* 类 名： RechargeRequest
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/3 14:45:12   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Pay
{
	/// <summary>
	/// RechargeRequest:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RechargeRequest
	{
		public RechargeRequest()
		{}
		#region Model
		private long _rechargeid;
		private DateTime _tradedate;
		private decimal _rechargeblance;
		private int _userid;
		private int? _sellerid;
		private int _status=0;
		private int? _tradetype;
		private int _paymenttypeid;
		private string _paymentgateway;
		/// <summary>
		/// 充值流水号
		/// </summary>
		public long RechargeId
		{
			set{ _rechargeid=value;}
			get{return _rechargeid;}
		}
		/// <summary>
		/// 充值时间
		/// </summary>
		public DateTime TradeDate
		{
			set{ _tradedate=value;}
			get{return _tradedate;}
		}
		/// <summary>
		/// 应付金额
		/// </summary>
		public decimal RechargeBlance
		{
			set{ _rechargeblance=value;}
			get{return _rechargeblance;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SellerId
		{
			set{ _sellerid=value;}
			get{return _sellerid;}
		}
		/// <summary>
		/// 0：无效  1：有效
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 交易类型: 1 充值
		/// </summary>
		public int? Tradetype
		{
			set{ _tradetype=value;}
			get{return _tradetype;}
		}
		/// <summary>
		/// 支付ID
		/// </summary>
		public int PaymentTypeId
		{
			set{ _paymenttypeid=value;}
			get{return _paymenttypeid;}
		}
		/// <summary>
		/// 支付网关名称
		/// </summary>
		public string PaymentGateway
		{
			set{ _paymentgateway=value;}
			get{return _paymentgateway;}
		}
		#endregion Model

	}
}

