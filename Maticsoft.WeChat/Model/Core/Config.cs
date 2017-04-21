/**  版本信息模板在安装目录下，可自行修改。
* Config.cs
*
* 功 能： N/A
* 类 名： Config
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/21 18:25:39   N/A    初版
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
	/// Config:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Config
	{
		public Config()
		{}
		#region Model
		private int _id;
		private string _keyname;
		private string _value;
		private string _description;
		private int _targetid;
		private string _usertype;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// Key
		/// </summary>
		public string KeyName
		{
			set{ _keyname=value;}
			get{return _keyname;}
		}
		/// <summary>
		/// 值
		/// </summary>
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 外键ID
		/// </summary>
		public int TargetId
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 用户类型  SP 商家 | AG代理商
		/// </summary>
		public string UserType
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		#endregion Model

	}
}

