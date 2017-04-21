/**  版本信息模板在安装目录下，可自行修改。
* UserCard.cs
*
* 功 能： N/A
* 类 名： UserCard
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/17 19:10:22   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Members
{
	/// <summary>
	/// UserCard:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserCard
	{
		public UserCard()
		{}
		#region Model
		private string _cardcode;
		private string _cardpwd;
		private decimal _cardvalue;
		private int _userid;
		private int _status;
		private int _type;
		private DateTime _enddate;
		private DateTime _createddate;
		private string _remark;
		/// <summary>
		/// 卡号
		/// </summary>
		public string CardCode
		{
			set{ _cardcode=value;}
			get{return _cardcode;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string CardPwd
		{
			set{ _cardpwd=value;}
			get{return _cardpwd;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public decimal CardValue
		{
			set{ _cardvalue=value;}
			get{return _cardvalue;}
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
		/// 状态 
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 类型 0:表示金额 1:表示次数2:折扣卡
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 有效期
		/// </summary>
		public DateTime EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
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

