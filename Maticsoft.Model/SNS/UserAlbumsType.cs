/**
* UserAlbumsType.cs
*
* 功 能： N/A
* 类 名： UserAlbumsType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:02   N/A    初版
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
	/// 用户专辑类型
	/// </summary>
	[Serializable]
	public partial class UserAlbumsType
	{
		public UserAlbumsType()
		{}
		#region Model
		private int _albumsid;
		private int _typeid;
		private int? _albumsuserid;
		/// <summary>
		/// 
		/// </summary>
		public int AlbumsID
		{
			set{ _albumsid=value;}
			get{return _albumsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AlbumsUserID
		{
			set{ _albumsuserid=value;}
			get{return _albumsuserid;}
		}
		#endregion Model

	}
}

