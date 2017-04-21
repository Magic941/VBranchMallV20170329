/**
* SupplierConfig.cs
*
* 功 能： N/A
* 类 名： SupplierConfig
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:48   Ben    初版
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
	/// 供应商(店铺)配置
	/// </summary>
	[Serializable]
	public partial class SupplierConfig
	{
		public SupplierConfig()
		{}
		#region Model
		private int _id;
		private string _keyname;
		private string _value;
		private int? _keytype;
		private string _description;
		private int _supplierid;
		/// <summary>
		/// 流水Id
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KeyName
		{
			set{ _keyname=value;}
			get{return _keyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		/// <summary>
		/// 参数类别 0：CMS 1:Shop 2:SNS
		/// </summary>
		public int? KeyType
		{
			set{ _keytype=value;}
			get{return _keytype;}
		}
		/// <summary>
		/// 说明
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
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

