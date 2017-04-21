/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductAttributes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:25
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
	/// ProductAttributes:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
	public partial class ProductAttribute
	{
		public ProductAttribute()
		{}
		#region Model
		private long _productid;
		private long _attributeid;
		private int _valueid;
		/// <summary>
		/// 
		/// </summary>
		public long ProductId
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long AttributeId
		{
			set{ _attributeid=value;}
			get{return _attributeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ValueId
		{
			set{ _valueid=value;}
			get{return _valueid;}
		}
		#endregion Model

	}
}

