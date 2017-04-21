/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Distributors.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:23
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
	/// Distributors:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Distributor
	{
		public Distributor()
		{}
		#region Model
		private int _distributorid;
		private string _distributorname;
		/// <summary>
		/// 
		/// </summary>
		public int DistributorId
		{
			set{ _distributorid=value;}
			get{return _distributorid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DistributorName
		{
			set{ _distributorname=value;}
			get{return _distributorname;}
		}
		#endregion Model

	}
}

