/**
* BalanceDrawRequest.cs
*
* 功 能： N/A
* 类 名： BalanceDrawRequest
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/16 18:31:09   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Pay
{
	/// <summary>
	/// BalanceDrawRequest:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BalanceDrawRequest
	{
		public BalanceDrawRequest()
		{}
		#region Model
		private long _journalnumber;
		private DateTime _requesttime;
		private decimal _amount;
		private int _userid;
		private string _truename;
		private string _bankname;
		private string _bankcard;
		private int _cardtypeid;
		private int _requeststatus=1;
		private int _requesttype=1;
		private int _targetid=-1;
		private string _remark;
		/// <summary>
		/// 流水号
		/// </summary>
		public long JournalNumber
		{
			set{ _journalnumber=value;}
			get{return _journalnumber;}
		}
		/// <summary>
		/// 请求时间
		/// </summary>
		public DateTime RequestTime
		{
			set{ _requesttime=value;}
			get{return _requesttime;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public decimal Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 申请人
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 开户姓名
		/// </summary>
		public string TrueName
		{
			set{ _truename=value;}
			get{return _truename;}
		}
		/// <summary>
		/// 银行名称
		/// </summary>
		public string BankName
		{
			set{ _bankname=value;}
			get{return _bankname;}
		}
		/// <summary>
		/// 银行卡号 或 支付宝号
		/// </summary>
		public string BankCard
		{
			set{ _bankcard=value;}
			get{return _bankcard;}
		}
		/// <summary>
		/// 卡号类型   1: 银行卡号   2:支付宝帐号
		/// </summary>
		public int CardTypeID
		{
			set{ _cardtypeid=value;}
			get{return _cardtypeid;}
		}
		/// <summary>
		/// 请求状态  1:未审核   2:审核失败  3:审核通过
		/// </summary>
		public int RequestStatus
		{
			set{ _requeststatus=value;}
			get{return _requeststatus;}
		}
		/// <summary>
		/// 1 用户提现 | 2 商家提现
		/// </summary>
		public int RequestType
		{
			set{ _requesttype=value;}
			get{return _requesttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TargetId
		{
			set{ _targetid=value;}
			get{return _targetid;}
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

