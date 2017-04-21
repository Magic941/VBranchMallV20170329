/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SkuItemRelation.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/08/14 15:40:40
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
	/// SkuItemRelation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
	public partial class SKURelation
	{
		public SKURelation()
		{}
		#region Model
		private long _specid;
		private long _skuid;
		private long _productid;
		/// <summary>
		/// 
		/// </summary>
		public long SpecId
		{
			set{ _specid=value;}
			get{return _specid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long SkuId
		{
			set{ _skuid=value;}
			get{return _skuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ProductId
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		#endregion Model

	}
}

