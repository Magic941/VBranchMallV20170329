/**
* Constant.cs
*
* 功 能： N/A
* 类 名： Constant
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/7 12:27:59   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop
{
	/// <summary>
	/// Constant:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Constant
	{
		public Constant()
		{}
		#region Model
		private int _type;
		private DateTime _datadate;
		private int _maxvalue;
		private string _remark;
		/// <summary>
		/// 类型 1：订单
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 日期
		/// </summary>
		public DateTime DataDate
		{
			set{ _datadate=value;}
			get{return _datadate;}
		}
		/// <summary>
		/// 值
		/// </summary>
		public int MaxValue
		{
			set{ _maxvalue=value;}
			get{return _maxvalue;}
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

