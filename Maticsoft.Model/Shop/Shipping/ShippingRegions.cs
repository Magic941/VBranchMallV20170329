/**
* ShippingRegions.cs
*
* 功 能： N/A
* 类 名： ShippingRegions
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/8 18:17:32   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Shipping
{
	/// <summary>
	/// ShippingRegions:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShippingRegions
	{
		public ShippingRegions()
		{}
		#region Model
		private int _modeid;
		private int _groupid;
		private int _regionid;
		/// <summary>
		/// 物流类型ID
		/// </summary>
		public int ModeId
		{
			set{ _modeid=value;}
			get{return _modeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int GroupId
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 区域Id
		/// </summary>
		public int RegionId
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		#endregion Model

	}
}

