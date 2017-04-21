/**
* SuppProductStatModes.cs
*
* 功 能： N/A
* 类 名： SuppProductStatModes
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/27 18:11:59   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Supplier
{
	/// <summary>
	/// SuppProductStatModes:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SuppProductStatModes
	{
		public SuppProductStatModes()
		{}
		#region Model
		private int _stationid;
		private long _productid;
		private int _displaysequence=0;
		private int _type;
		private int _supplierid;
		/// <summary>
		/// 
		/// </summary>
		public int StationId
		{
			set{ _stationid=value;}
			get{return _stationid;}
		}
		/// <summary>
		/// 商品Id
		/// </summary>
		public long ProductId
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 排序
		/// </summary>
		public int DisplaySequence
		{
			set{ _displaysequence=value;}
			get{return _displaysequence;}
		}
		/// <summary>
		/// 0:推荐 1:热卖 2:特价 3:最新
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 供应商Id
		/// </summary>
		public int SupplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		#endregion Model

	}
}

