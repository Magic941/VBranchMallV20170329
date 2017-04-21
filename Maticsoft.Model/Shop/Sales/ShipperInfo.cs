/**
* ShipperInfo.cs
*
* 功 能： N/A
* 类 名： ShipperInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/19 16:53:39   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Sales
{
	/// <summary>
    /// ShipperInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ShipperInfo
	{
		public ShipperInfo()
		{}
		#region Model
		private int _shipperid;
		private bool _isdefault;
		private string _shippertag;
		private string _shippername;
		private int _regionid;
		private string _address;
		private string _cellphone;
		private string _telphone;
		private string _zipcode;
		private string _remark;
		/// <summary>
		/// 发货人ID
		/// </summary>
		public int ShipperId
		{
			set{ _shipperid=value;}
			get{return _shipperid;}
		}
		/// <summary>
		/// 是否默认
		/// </summary>
		public bool IsDefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		/// <summary>
		/// 发货人标签
		/// </summary>
		public string ShipperTag
		{
			set{ _shippertag=value;}
			get{return _shippertag;}
		}
		/// <summary>
		/// 发货人名称
		/// </summary>
		public string ShipperName
		{
			set{ _shippername=value;}
			get{return _shippername;}
		}
		/// <summary>
		/// 区域
		/// </summary>
		public int RegionId
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		/// <summary>
		/// 地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string CellPhone
		{
			set{ _cellphone=value;}
			get{return _cellphone;}
		}
		/// <summary>
		/// 电话
		/// </summary>
		public string TelPhone
		{
			set{ _telphone=value;}
			get{return _telphone;}
		}
		/// <summary>
		/// 邮编
		/// </summary>
		public string Zipcode
		{
			set{ _zipcode=value;}
			get{return _zipcode;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

