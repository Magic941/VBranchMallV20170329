/**
* ExpressTemplates.cs
*
* 功 能： N/A
* 类 名： ExpressTemplates
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/18 19:00:30   Ben    初版
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
	/// ExpressTemplates:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ExpressTemplate
	{
		public ExpressTemplate()
		{}
		#region Model
		private int _expressid;
		private string _expressname;
		private string _xmlfile;
		private bool _isuse;
		/// <summary>
		/// 
		/// </summary>
		public int ExpressId
		{
			set{ _expressid=value;}
			get{return _expressid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExpressName
		{
			set{ _expressname=value;}
			get{return _expressname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XmlFile
		{
			set{ _xmlfile=value;}
			get{return _xmlfile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsUse
		{
			set{ _isuse=value;}
			get{return _isuse;}
		}
		#endregion Model

	}
}

