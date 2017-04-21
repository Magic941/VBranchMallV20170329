/**  版本信息模板在安装目录下，可自行修改。
* Reservation.cs
*
* 功 能： N/A
* 类 名： Reservation
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/2 17:36:20   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Appt
{
	/// <summary>
	/// Reservation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Reservation
	{
		public Reservation()
		{}
		#region Model
		private int _reservalid;
		private string _name;
		private int _regionid;
		private string _contactname;
		private string _contactphone;
		private DateTime _reservaldate;
		private string _content;
		private string _address;
		private string _contactemail;
		private int _status;
		private DateTime _createddate;
		private int _createduserid;
		private int _supplierid;
		private int _serviceid;
		private string _ordercode;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int ReservalId
		{
			set{ _reservalid=value;}
			get{return _reservalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RegionId
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactName
		{
			set{ _contactname=value;}
			get{return _contactname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactPhone
		{
			set{ _contactphone=value;}
			get{return _contactphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ReservalDate
		{
			set{ _reservaldate=value;}
			get{return _reservaldate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactEmail
		{
			set{ _contactemail=value;}
			get{return _contactemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CreatedUserId
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SupplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ServiceId
		{
			set{ _serviceid=value;}
			get{return _serviceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderCode
		{
			set{ _ordercode=value;}
			get{return _ordercode;}
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

