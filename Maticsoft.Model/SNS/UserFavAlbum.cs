/**
* UserFavAlbum.cs
*
* 功 能： N/A
* 类 名： UserFavAlbum
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:03   N/A    初版
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
	/// 用户关注专辑
	/// </summary>
	[Serializable]
	public partial class UserFavAlbum
	{
		public UserFavAlbum()
		{}
		#region Model
		private int _id;
		private int _albumid;
		private int _userid;
		private string _tags;
		private DateTime _createddate;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 专辑ID
		/// </summary>
		public int AlbumID
		{
			set{ _albumid=value;}
			get{return _albumid;}
		}
		/// <summary>
		/// 用户id
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		#endregion Model

	}
}

