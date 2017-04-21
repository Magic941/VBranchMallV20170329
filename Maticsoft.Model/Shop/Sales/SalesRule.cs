/**
* SalesRule.cs
*
* 功 能： N/A
* 类 名： SalesRule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 18:54:56   N/A    初版
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
	/// SalesRule:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SalesRule
	{
		public SalesRule()
		{}
		#region Model
		private int _ruleid;
		private string _rulename;
		private int _rulemode;
		private int _ruleunit;
		private int _status;
		private DateTime _createddate;
		private int _createduserid;
		/// <summary>
		/// 规则ID
		/// </summary>
		public int RuleId
		{
			set{ _ruleid=value;}
			get{return _ruleid;}
		}
		/// <summary>
		/// 规则名称
		/// </summary>
		public string RuleName
		{
			set{ _rulename=value;}
			get{return _rulename;}
		}
		/// <summary>
		/// 应用方式  0：单个商品 1：商品总计
		/// </summary>
		public int RuleMode
		{
			set{ _rulemode=value;}
			get{return _rulemode;}
		}
		/// <summary>
		/// 规则单位 0：个 1：元
		/// </summary>
		public int RuleUnit
		{
			set{ _ruleunit=value;}
			get{return _ruleunit;}
		}
		/// <summary>
		/// 状态 0：不启用，1：启用
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		#endregion Model

	}
}

