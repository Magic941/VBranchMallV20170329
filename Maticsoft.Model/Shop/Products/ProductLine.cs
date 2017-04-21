/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductLines.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:26
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
	/// ProductLines:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductLine
	{
		public ProductLine()
		{}
		#region Model
		private int _lineid;
		private string _linename;
		/// <summary>
		/// 
		/// </summary>
		public int LineId
		{
			set{ _lineid=value;}
			get{return _lineid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LineName
		{
			set{ _linename=value;}
			get{return _linename;}
		}
		#endregion Model

	}
}

