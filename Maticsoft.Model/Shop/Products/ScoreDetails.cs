/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ScoreDetails.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/08/27 14:50:44
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
	/// 评分记录表
	/// </summary>
	[Serializable]
	public partial class ScoreDetails
	{
		public ScoreDetails()
		{}
		#region Model
		private int _scoreid;
		private int _reviewid;
		private int? _userid;
		private long? _productid;
		private int? _score;
		private DateTime? _createddate;
		/// <summary>
		/// 
		/// </summary>
		public int ScoreId
		{
			set{ _scoreid=value;}
			get{return _scoreid;}
		}
		/// <summary>
		/// 评论ID
		/// </summary>
		public int ReviewId
		{
			set{ _reviewid=value;}
			get{return _reviewid;}
		}
		/// <summary>
		/// 评论人
		/// </summary>
		public int? UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 评论商品
		/// </summary>
		public long? ProductId
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 分数
		/// </summary>
		public int? Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		#endregion Model

	}
}

