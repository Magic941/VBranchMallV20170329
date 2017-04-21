/**
* VideoAlbum.cs
*
* 功 能： 
* 类 名： VideoAlbum
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/22 16:28:49  蒋海滨    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.CMS
{
	/// <summary>
	/// 视频专辑表
	/// </summary>
	[Serializable]
	public partial class VideoAlbum
	{
		public VideoAlbum()
		{}
		#region Model
		private int _albumid;
		private string _albumname;
		private string _covervideo="AlbumDefaultPictures.jpg";
		private string _description;
		private int _createduserid;
        private string _createdusername;
		private DateTime _createddate;
		private int? _lastupdateuserid;
		private DateTime? _lastupdateddate;
        private string _lastupdateusername;
		private int _state;
		private int _sequence;
		private int? _privacy;
		private int _pvcount=0;
		/// <summary>
		/// 专辑编号
		/// </summary>
		public int AlbumID
		{
			set{ _albumid=value;}
			get{return _albumid;}
		}
		/// <summary>
		/// 专辑名称
		/// </summary>
		public string AlbumName
		{
			set{ _albumname=value;}
			get{return _albumname;}
		}
		/// <summary>
		/// 专辑封面
		/// </summary>
		public string CoverVideo
		{
			set{ _covervideo=value;}
			get{return _covervideo;}
		}
		/// <summary>
		/// 专辑描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 专辑创建者
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
        /// <summary>
        /// 专辑创建者
        /// </summary>
        public string CreatedUserName
        {
            get { return _createdusername; }
            set { _createdusername = value; }
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
		/// 专辑最后编辑者
		/// </summary>
		public int? LastUpdateUserID
		{
			set{ _lastupdateuserid=value;}
			get{return _lastupdateuserid;}
		}
        /// <summary>
        /// 专辑最后编辑者
        /// </summary>
        public string LastUpdateUserName
        {
            get { return _lastupdateusername; }
            set { _lastupdateusername = value; }
        }
		/// <summary>
		/// 专辑最后编辑时间
		/// </summary>
		public DateTime? LastUpdatedDate
		{
			set{ _lastupdateddate=value;}
			get{return _lastupdateddate;}
		}
		/// <summary>
		/// 专辑状态
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 专辑顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 专辑权限
		/// </summary>
		public int? Privacy
		{
			set{ _privacy=value;}
			get{return _privacy;}
		}
		/// <summary>
		/// 专辑浏览量
		/// </summary>
		public int PvCount
		{
			set{ _pvcount=value;}
			get{return _pvcount;}
		}
		#endregion Model

	}
}

