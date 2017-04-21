/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：AttributeValues.cs
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
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
	/// AttributeValues:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AttributeValue
	{
		public AttributeValue()
		{}
		#region Model
		private long _valueid;
		private long _attributeid;
		private int _displaysequence;
		private string _valuestr;
		private string _imageurl;
		/// <summary>
		/// 
		/// </summary>
		public long ValueId
		{
			set{ _valueid=value;}
			get{return _valueid;}
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
		public int DisplaySequence
		{
			set{ _displaysequence=value;}
			get{return _displaysequence;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ValueStr
		{
			set{ _valuestr=value;}
			get{return _valuestr;}
		}

        public string SelfValue { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		#endregion Model

	}
}

