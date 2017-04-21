/**
* UserAlbumDetail.cs
*
* 功 能： N/A
* 类 名： UserAlbumDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:00   N/A    初版
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
	/// 用户专辑详情
	/// </summary>
	[Serializable]
	public partial class UserAlbumDetail
	{
		public UserAlbumDetail()
		{}
		#region Model
		private int _id;
		private int _albumid;
		private int _targetid;
		private int _type;
		private string _description;
        private int _albumuserid;
		/// <summary>
		/// 流水号
		/// </summary>


        public int AlbumUserId
        {
            get { return _albumuserid; }
            set { _albumuserid = value; }
        }      
        public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 专辑的名称（专辑中有对于的userid）
		/// </summary>
		public int AlbumID
		{
			set{ _albumid=value;}
			get{return _albumid;}
		}
		/// <summary>
		/// 图片或商品的id
		/// </summary>
		public int TargetID
		{
			set{ _targetid=value;}
			get{return _targetid;}
		}
		/// <summary>
		/// 类型：0为图片，1为商品
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		#endregion Model

	}
}

