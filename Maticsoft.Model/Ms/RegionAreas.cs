/**
* Area.cs
*
* 功 能： N/A
* 类 名： Area
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/4/27 18:28:15   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// Area:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RegionAreas
	{
		public RegionAreas()
		{}
		#region Model
		private int _areaid;
		private string _name;
		/// <summary>
		/// 地区ID
		/// </summary>
		public int AreaId
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 地区名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		#endregion Model

	}
}

