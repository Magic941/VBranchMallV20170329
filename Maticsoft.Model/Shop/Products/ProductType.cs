/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductTypes.cs
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
using System.Collections.Generic;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
	/// ProductTypes:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductType
	{
		public ProductType()
		{}
		#region Model
		private int _typeid;
		private string _typename;
        private string _remark;
        private IList<int> _brandsTypes;

        public IList<int> BrandsTypes
        {
            get { return _brandsTypes; }
            set { _brandsTypes = value; }
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
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

