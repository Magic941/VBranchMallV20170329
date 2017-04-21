/**
* WeiBoTaskMsg.cs
*
* 功 能： N/A
* 类 名： WeiBoTaskMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/22 20:27:24   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// WeiBoTaskMsg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class WeiBoTaskMsg
	{
		public WeiBoTaskMsg()
		{}
		#region Model
		private int _weibotaskid;
		private string _weibomsg;
		private string _imageurl;
		private DateTime _createdate;
		private DateTime? _publishdate;
		/// <summary>
		/// 
		/// </summary>
		public int WeiBoTaskId
		{
			set{ _weibotaskid=value;}
			get{return _weibotaskid;}
		}
		/// <summary>
		/// 微博消息
		/// </summary>
		public string WeiboMsg
		{
			set{ _weibomsg=value;}
			get{return _weibomsg;}
		}
		/// <summary>
		/// 微博图片
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PublishDate
		{
			set{ _publishdate=value;}
			get{return _publishdate;}
		}
		#endregion Model

	}
}

