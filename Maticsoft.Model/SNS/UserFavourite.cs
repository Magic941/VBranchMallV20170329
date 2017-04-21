/**
* UserFavourite.cs
*
* 功 能： N/A
* 类 名： UserFavourite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:04   N/A    初版
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
	/// 用户的喜欢
	/// </summary>
	[Serializable]
	public partial class UserFavourite
	{
		public UserFavourite()
		{}
		#region Model
		private int _favouriteid;
		private int _targetid;
		private int _type;
		private int _createduserid;
		private string _creatednickname;
		private int? _owneruserid;
		private string _ownernickname;
		private string _description;
		private string _tags;
		private DateTime _createddate;
		/// <summary>
		/// id
		/// </summary>
		public int FavouriteID
		{
			set{ _favouriteid=value;}
			get{return _favouriteid;}
		}
		/// <summary>
		/// 目标的id
		/// </summary>
		public int TargetID
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 类型（图片或商品）
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 创建者
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 创建者姓名
		/// </summary>
		public string CreatedNickName
		{
			set{ _creatednickname=value;}
			get{return _creatednickname;}
		}
		/// <summary>
		/// 图片或商品首发者id]
		/// </summary>
		public int? OwnerUserID
		{
			set{ _owneruserid=value;}
			get{return _owneruserid;}
		}
		/// <summary>
		/// 图片或商品首发者姓名
		/// </summary>
		public string OwnerNickName
		{
			set{ _ownernickname=value;}
			get{return _ownernickname;}
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
		/// 标签
		/// </summary>
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}
		/// <summary>
		/// 创建的日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		#endregion Model

	}
}

