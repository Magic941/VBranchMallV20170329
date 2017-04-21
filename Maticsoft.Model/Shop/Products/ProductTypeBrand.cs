/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductTypeBrands.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:30
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
	/// ProductTypeBrands:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductTypeBrand
	{
		public ProductTypeBrand()
		{}
		#region Model
		private int _producttypeid;
		private int _brandid;
		/// <summary>
		/// 
		/// </summary>
		public int ProductTypeId
		{
			set{ _producttypeid=value;}
			get{return _producttypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BrandId
		{
			set{ _brandid=value;}
			get{return _brandid;}
		}
		#endregion Model

	}
}

