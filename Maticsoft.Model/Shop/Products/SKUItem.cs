/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SKUItems.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:32
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
	/// SKUItems:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
	public partial class SKUItem
	{
		public SKUItem()
		{}
		#region Model
		private long _skuid;
		private long _attributeid;
		private long _valueid;
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
		public long AttributeId
		{
			set{ _attributeid=value;}
			get{return _attributeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ValueId
		{
			set{ _valueid=value;}
			get{return _valueid;}
		}
		#endregion Model

	}
}

