/**  版本信息模板在安装目录下，可自行修改。
* LinkLog.cs
*
* 功 能： N/A
* 类 名： LinkLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/9 18:22:16   N/A    初版
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
	/// LinkLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LinkLog
	{
		public LinkLog()
		{}
		#region Model
		private string _wechatlink;
		private DateTime _createddate;
		/// <summary>
		/// 微信过来的链接
		/// </summary>
		public string WeChatLink
		{
			set{ _wechatlink=value;}
			get{return _wechatlink;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		#endregion Model

	}
}

