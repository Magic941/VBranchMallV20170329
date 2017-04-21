/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SKUMemberPrice.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:33
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
	/// SKUMemberPrice:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SKUMemberPrice
	{
		public SKUMemberPrice()
		{}
		#region Model
		private long _skuid;
		private int _gradeid;
		private decimal _membersaleprice;
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
		public int GradeId
		{
			set{ _gradeid=value;}
			get{return _gradeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal MemberSalePrice
		{
			set{ _membersaleprice=value;}
			get{return _membersaleprice;}
		}
		#endregion Model

	}
}

