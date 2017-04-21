/**
* SalesRuleProduct.cs
*
* 功 能： N/A
* 类 名： SalesRuleProduct
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 18:54:58   N/A    初版
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
	/// SalesRuleProduct:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SalesRuleProduct
	{
		public SalesRuleProduct()
		{}
		#region Model
		private int _ruleid;
		private long _productid;
		private string _productname;
		/// <summary>
		/// 批发规则ID
		/// </summary>
		public int RuleId
		{
			set{ _ruleid=value;}
			get{return _ruleid;}
		}
		/// <summary>
		/// 对应的产品ID
		/// </summary>
		public long ProductId
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 产品名称
		/// </summary>
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		#endregion Model

	}
}

