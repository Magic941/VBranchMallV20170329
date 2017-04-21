/**  版本信息模板在安装目录下，可自行修改。
* CustUserMsg.cs
*
* 功 能： N/A
* 类 名： CustUserMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/22 11:50:22   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Core
{
	/// <summary>
	/// CustUserMsg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CustUserMsg
	{
		public CustUserMsg()
		{}
		#region Model
		private long _msgid;
		private string _username;
		/// <summary>
		/// 
		/// </summary>
		public long MsgId
		{
			set{ _msgid=value;}
			get{return _msgid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		#endregion Model

	}
}

