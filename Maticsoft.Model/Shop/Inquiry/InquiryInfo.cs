/**
* Inquiry.cs
*
* 功 能： N/A
* 类 名： Inquiry
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/4 19:23:28   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Inquiry
{
	/// <summary>
	/// Inquiry:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class InquiryInfo
	{
		public InquiryInfo()
		{}
		#region Model
		private long _inquiryid;
		private long _parentid;
		private int _userid=0;
		private string _username;
		private string _email;
		private string _cellphone;
		private string _telephone;
		private int _regionid=0;
		private string _company;
		private string _address;
		private string _qq;
		private int _status;
		private string _leavemsg;
		private string _replymsg;
		private decimal? _marketprice;
		private decimal _amount=0M;
		private DateTime _createddate;
		private DateTime? _updateddate;
		private int  _updateduserid=0;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public long InquiryId
		{
			set{ _inquiryid=value;}
			get{return _inquiryid;}
		}
		/// <summary>
		/// 父评论
		/// </summary>
		public long ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 用户ID
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 用户邮箱
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
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
		/// 座机
		/// </summary>
		public string Telephone
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		/// <summary>
		/// 所在省市区ID
		/// </summary>
		public int RegionId
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		/// <summary>
		/// 公司
		/// </summary>
		public string Company
		{
			set{ _company=value;}
			get{return _company;}
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
		/// 
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// 状态 0未处理 1已处理
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 留言
		/// </summary>
		public string LeaveMsg
		{
			set{ _leavemsg=value;}
			get{return _leavemsg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReplyMsg
		{
			set{ _replymsg=value;}
			get{return _replymsg;}
		}
		/// <summary>
		/// 市场价
		/// </summary>
		public decimal? MarketPrice
		{
			set{ _marketprice=value;}
			get{return _marketprice;}
		}
		/// <summary>
		/// 最终金额(支付)
		/// </summary>
		public decimal Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 更新日期
		/// </summary>
		public DateTime? UpdatedDate
		{
			set{ _updateddate=value;}
			get{return _updateddate;}
		}
		/// <summary>
		/// 更新用户
		/// </summary>
		public int UpdatedUserId
		{
			set{ _updateduserid=value;}
			get{return _updateduserid;}
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

