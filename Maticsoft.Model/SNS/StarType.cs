/**
* StarType.cs
*
* 功 能： N/A
* 类 名： StarType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:57   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 达人类型
	/// </summary>
	[Serializable]
	public partial class StarType
	{
		public StarType()
		{}
		#region Model
		private int _typeid;
		private string _typename;
		private string _checkrule;
		private string _remark;
		private int? _status;
		/// <summary>
		/// 达人类型ID
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 达人类型（如新晋达人，最热达人）
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 对应达人类型的审核规则
		/// </summary>
		public string CheckRule
		{
			set{ _checkrule=value;}
			get{return _checkrule;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
        /// 状态 0:不可用 1：可用
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

