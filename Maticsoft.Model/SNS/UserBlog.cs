/**
* UserBlog.cs
*
* 功 能： N/A
* 类 名： UserBlog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/3 12:08:16   N/A    初版
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
	/// UserBlog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserBlog
	{
		public UserBlog()
		{}
		#region Model
		private int _blogid;
		private string _title;
		private string _summary;
		private string _description;
		private int _userid;
		private string _username;
		private string _linkurl;
		private int _status=1;
		private string _keywords;
		private int _recomend;
		private string _attachment;
		private string _remark;
		private int _pvcount;
		private int _totalcomment;
		private int _totalfav;
		private int _totalshare;
		private string _meta_title;
		private string _meta_description;
		private string _meta_keywords;
		private string _seourl;
		private string _staticurl;
		private DateTime _createddate;
		/// <summary>
		/// 
		/// </summary>
		public int BlogID
		{
			set{ _blogid=value;}
			get{return _blogid;}
		}
		/// <summary>
		/// 长微博标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 文章简介
		/// </summary>
		public string Summary
		{
			set{ _summary=value;}
			get{return _summary;}
		}
		/// <summary>
		/// 文章内容
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 用户ID
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 外链URL
		/// </summary>
		public string LinkUrl
		{
			set{ _linkurl=value;}
			get{return _linkurl;}
		}
		/// <summary>
		/// 0:未审核 1：已审核  
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Keywords
		{
			set{ _keywords=value;}
			get{return _keywords;}
		}
		/// <summary>
		/// 0:不推荐 1:推荐到首页 2:推荐到频道
		/// </summary>
		public int Recomend
		{
			set{ _recomend=value;}
			get{return _recomend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Attachment
		{
			set{ _attachment=value;}
			get{return _attachment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 浏览数
		/// </summary>
		public int PvCount
		{
			set{ _pvcount=value;}
			get{return _pvcount;}
		}
		/// <summary>
		/// 评论数
		/// </summary>
		public int TotalComment
		{
			set{ _totalcomment=value;}
			get{return _totalcomment;}
		}
		/// <summary>
		/// 喜欢数
		/// </summary>
		public int TotalFav
		{
			set{ _totalfav=value;}
			get{return _totalfav;}
		}
		/// <summary>
		/// 分享数
		/// </summary>
		public int TotalShare
		{
			set{ _totalshare=value;}
			get{return _totalshare;}
		}
		/// <summary>
		/// SEO 标题
		/// </summary>
		public string Meta_Title
		{
			set{ _meta_title=value;}
			get{return _meta_title;}
		}
		/// <summary>
		/// SEO 描述
		/// </summary>
		public string Meta_Description
		{
			set{ _meta_description=value;}
			get{return _meta_description;}
		}
		/// <summary>
		/// SEO 关键字
		/// </summary>
		public string Meta_Keywords
		{
			set{ _meta_keywords=value;}
			get{return _meta_keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SeoUrl
		{
			set{ _seourl=value;}
			get{return _seourl;}
		}
		/// <summary>
		/// 静态化地址
		/// </summary>
		public string StaticUrl
		{
			set{ _staticurl=value;}
			get{return _staticurl;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		#endregion Model

	}
}

