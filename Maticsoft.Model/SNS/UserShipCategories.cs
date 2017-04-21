/**
* UserShipCategories.cs
*
* 功 能： N/A
* 类 名： UserShipCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:07   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 用户的关注分类
	/// </summary>
	[Serializable]
	public partial class UserShipCategories
	{
		public UserShipCategories()
		{}
		#region Model
		private int _categoryid;
		private int _createduserid;
		private string _categoryname;
		private string _description="";
		private int _sequence=0;
		private DateTime? _lastupdateddate= DateTime.Now;
		private DateTime _createddate= DateTime.Now;
		private int _privacy=30;
		/// <summary>
		/// 主键
		/// </summary>
		public int CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 创建者ID
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 类型名称
		/// </summary>
		public string CategoryName
		{
			set{ _categoryname=value;}
			get{return _categoryname;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 最后的修改时间
		/// </summary>
		public DateTime? LastUpdatedDate
		{
			set{ _lastupdateddate=value;}
			get{return _lastupdateddate;}
		}
		/// <summary>
		/// 创建的时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 隐私设置
		/// </summary>
		public int Privacy
		{
			set{ _privacy=value;}
			get{return _privacy;}
		}
		#endregion Model

	}
}

