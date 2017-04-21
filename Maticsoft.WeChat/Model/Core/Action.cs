/**
* Action.cs
*
* 功 能： N/A
* 类 名： Action
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:10   N/A    初版
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
	/// Action:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Action
	{
		public Action()
		{}
		#region Model
		private int _actionid;
		private string _name;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int ActionId
		{
			set{ _actionid=value;}
			get{return _actionid;}
		}
		/// <summary>
		/// 行为名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

