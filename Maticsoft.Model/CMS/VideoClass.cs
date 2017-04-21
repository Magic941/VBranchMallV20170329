/**
* VideoClass.cs
*
* 功 能： 
* 类 名： VideoClass
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
	/// 视频分类表
	/// </summary>
	[Serializable]
	public partial class VideoClass
	{
		public VideoClass()
		{}
		#region Model
		private int _videoclassid;
		private string _videoclassname;
		private int? _parentid;
		private int _sequence;
		private string _path;
		private int _depth;
		/// <summary>
		/// 视频分类编号
		/// </summary>
		public int VideoClassID
		{
			set{ _videoclassid=value;}
			get{return _videoclassid;}
		}
		/// <summary>
		/// 视频分类名称
		/// </summary>
		public string VideoClassName
		{
			set{ _videoclassname=value;}
			get{return _videoclassname;}
		}
		/// <summary>
		/// 父级类别
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 路径
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		/// <summary>
		/// 层级
		/// </summary>
		public int Depth
		{
			set{ _depth=value;}
			get{return _depth;}
		}
		#endregion Model

	}
}

