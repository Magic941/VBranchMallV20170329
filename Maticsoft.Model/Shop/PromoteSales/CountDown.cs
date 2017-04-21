/**
* CountDown.cs
*
* 功 能： N/A
* 类 名： CountDown
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/11 18:45:36   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.PromoteSales
{
	/// <summary>
	/// CountDown:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CountDown
	{
		public CountDown()
		{}
		#region Model
		private int _countdownid;
		private long _productid;
		private DateTime _enddate;
		private string _description;
		private int _sequence=0;
		private decimal _price;
		private int _status;
		/// <summary>
		/// 
		/// </summary>
		public int CountDownId
		{
			set{ _countdownid=value;}
			get{return _countdownid;}
		}
		/// <summary>
		/// 商品ID
		/// </summary>
		public long ProductId
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 活动说明
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 秒杀价格
		/// </summary>
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 状态 0：下架 1：上架
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

