/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductConsultationsType.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/08/24 17:43:17
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
	/// 商品咨询类别表
	/// </summary>
	[Serializable]
	public partial class ProductConsultsType
	{
		public ProductConsultsType()
		{}
		#region Model
		private int _typeid;
		private string _typename;
		private DateTime? _createddate;
		private bool _isactive= true;
		/// <summary>
		/// 
		/// </summary>
		public int TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 咨询类别
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 是否可用
		/// </summary>
		public bool IsActive
		{
			set{ _isactive=value;}
			get{return _isactive;}
		}
		#endregion Model

	}
}

