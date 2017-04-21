/**
* SupplierRankThemes.cs
*
* 功 能： N/A
* 类 名： SupplierRankThemes
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:49   Ben    初版
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
	/// 供应商(店铺)等级与模版关系
	/// </summary>
	[Serializable]
	public partial class SupplierRankThemes
	{
		public SupplierRankThemes()
		{}
		#region Model
		private int _rankid;
		private int _themeid;
		/// <summary>
		/// 
		/// </summary>
		public int RankId
		{
			set{ _rankid=value;}
			get{return _rankid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ThemeId
		{
			set{ _themeid=value;}
			get{return _themeid;}
		}
		#endregion Model

	}
}

