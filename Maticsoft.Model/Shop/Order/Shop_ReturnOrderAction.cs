/**  版本信息模板在安装目录下，可自行修改。
* Shop_ReturnOrderAction.cs
*
* 功 能： N/A
* 类 名： Shop_ReturnOrderAction
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/5 10:22:06   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Order
{
	/// <summary>
	/// Shop_ReturnOrderAction:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Shop_ReturnOrderAction
	{
		public Shop_ReturnOrderAction()
		{}
		#region Model
		private long _actionid;
		private long _returnorderid;
		private string _returnordercode;
		private int _userid;
		private string _username;
		private string _actioncode;
		private DateTime _actiondate;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public long ActionId
		{
			set{ _actionid=value;}
			get{return _actionid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ReturnOrderId
		{
			set{ _returnorderid=value;}
			get{return _returnorderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReturnOrderCode
		{
			set{ _returnordercode=value;}
			get{return _returnordercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActionCode
		{
			set{ _actioncode=value;}
			get{return _actioncode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ActionDate
		{
			set{ _actiondate=value;}
			get{return _actiondate;}
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

