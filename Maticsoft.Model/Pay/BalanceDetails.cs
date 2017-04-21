/**  版本信息模板在安装目录下，可自行修改。
* BalanceDetails.cs
*
* 功 能： N/A
* 类 名： BalanceDetails
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
	/// BalanceDetails:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BalanceDetails
	{
		public BalanceDetails()
		{}
		#region Model
		private long _journalnumber;
		private int _userid;
		private DateTime _tradedate;
		private int _tradetype;
		private decimal? _income;
		private decimal? _expenses;
		private decimal _balance;
		private int? _payer;
		private int? _payee;
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
		/// 用户ID
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 交易日期
		/// </summary>
		public DateTime TradeDate
		{
			set{ _tradedate=value;}
			get{return _tradedate;}
		}
		/// <summary>
        /// 交易类型   1:收入 2:支出
		/// </summary>
		public int TradeType
		{
			set{ _tradetype=value;}
			get{return _tradetype;}
		}
		/// <summary>
		/// 收入
		/// </summary>
		public decimal? Income
		{
			set{ _income=value;}
			get{return _income;}
		}
		/// <summary>
		/// 费用
		/// </summary>
		public decimal? Expenses
		{
			set{ _expenses=value;}
			get{return _expenses;}
		}
		/// <summary>
		/// 余额
		/// </summary>
		public decimal Balance
		{
			set{ _balance=value;}
			get{return _balance;}
		}
		/// <summary>
		/// 付款人
		/// </summary>
		public int? Payer
		{
			set{ _payer=value;}
			get{return _payer;}
		}
		/// <summary>
		/// 收款人
		/// </summary>
		public int? Payee
		{
			set{ _payee=value;}
			get{return _payee;}
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

