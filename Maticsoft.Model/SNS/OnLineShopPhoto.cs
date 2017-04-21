/**
* OnLineShopPhoto.cs
*
* 功 能： N/A
* 类 名： OnLineShopPhoto
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:46   N/A    初版
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
	/// 网购实拍
	/// </summary>
	[Serializable]
	public partial class OnLineShopPhoto
	{
		public OnLineShopPhoto()
		{}
		#region Model
		private int _photoid;
		private int _productid;
		private int _createduserid;
		private string _creatednickname;
		private DateTime _createddate;
		private int _status;
		/// <summary>
		/// 网购实拍的照片
		/// </summary>
		public int PhotoID
		{
			set{ _photoid=value;}
			get{return _photoid;}
		}
		/// <summary>
		/// 所对应的商品的id
		/// </summary>
		public int ProductID
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 创建者id
		/// </summary>
		public int CreatedUserId
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
		/// 创建者日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
        /// 状态 0:不可用 1：可用
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

