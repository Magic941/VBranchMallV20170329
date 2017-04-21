/**
* KeyValue.cs
*
* 功 能： N/A
* 类 名： KeyValue
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:26   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Core
{
	/// <summary>
	/// KeyValue:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class KeyValue
	{
		public KeyValue()
		{}
		#region Model
		private int _valueid;
		private int _ruleid=-1;
		private string _value;
		private int _matchtype=0;
		/// <summary>
		/// 
		/// </summary>
		public int ValueId
		{
			set{ _valueid=value;}
			get{return _valueid;}
		}
		/// <summary>
		/// 关键字规则名称
		/// </summary>
		public int RuleId
		{
			set{ _ruleid=value;}
			get{return _ruleid;}
		}
		/// <summary>
		/// 关键字值
		/// </summary>
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// 匹配类型  0:模糊匹配 1：全匹配
		/// </summary>
		public int MatchType
		{
			set{ _matchtype=value;}
			get{return _matchtype;}
		}
		#endregion Model

	}
}

