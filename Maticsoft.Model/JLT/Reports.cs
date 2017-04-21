/**
* Reports.cs
*
* 功 能： N/A
* 类 名： Reports
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/24 15:53:42   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.JLT
{
	/// <summary>
	/// Reports:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Reports
	{
		public Reports()
		{}
		#region Model
		private int _id;
		private int? _enterpriseid;
		private int _userid;
		private string _title;
		private string _content;
		private int _type;
		private int _status=1;
		private string _filenames;
		private string _filedatapath;
		private DateTime _createddate= DateTime.Now;
		private DateTime _reportdate= DateTime.Now;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EnterpriseID
		{
			set{ _enterpriseid=value;}
			get{return _enterpriseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 简报类型：0：文字，1：图片，2：声音
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 0无效 1有效
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 文件名用 | 分割 与 FileDataPath组合使用
		/// </summary>
		public string FileNames
		{
			set{ _filenames=value;}
			get{return _filenames;}
		}
		/// <summary>
		/// 文件路径占位符方式, 如 简报ID_{0}, {0}是FileNames
		/// </summary>
		public string FileDataPath
		{
			set{ _filedatapath=value;}
			get{return _filedatapath;}
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
		/// 简报日期 冗余字段 方便统计
		/// </summary>
		public DateTime ReportDate
		{
			set{ _reportdate=value;}
			get{return _reportdate;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

