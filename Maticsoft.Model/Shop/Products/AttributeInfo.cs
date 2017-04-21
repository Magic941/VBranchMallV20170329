/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Attributes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:22
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
    /// AttributeInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
	public partial class AttributeInfo
	{
		public AttributeInfo()
		{}
		#region Model
		private long _attributeid;
		private string _attributename;
		private int _displaysequence;
		private int _typeid;
		private int _usagemode;
		private bool _useattributeimage;

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
		public string AttributeName
		{
			set{ _attributename=value;}
			get{return _attributename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DisplaySequence
		{
			set{ _displaysequence=value;}
			get{return _displaysequence;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
        /// 0:单选 1:多选 2:自定义填写 3:规格
		/// </summary>
		public int UsageMode
		{
			set{ _usagemode=value;}
			get{return _usagemode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool UseAttributeImage
		{
			set{ _useattributeimage=value;}
			get{return _useattributeimage;}
		}
		#endregion Model

	}
}

