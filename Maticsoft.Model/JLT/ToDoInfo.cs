/**
* ToDoInfo.cs
*
* 功 能： N/A
* 类 名： ToDoInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/24 16:24:58   N/A    初版
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
	/// ToDoInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ToDoInfo
	{
		public ToDoInfo()
		{}
		#region Model
		private int _id;
		private int? _enterpriseid;
		private int _userid;
		private string _username;
		private string _title;
		private string _content;
		private string _touserid;
		private int _totype;
		private int _status;
		private int? _parentid;
		private int _createduserid;
		private DateTime _createddate= DateTime.Now;
		private DateTime _tododate= DateTime.Now;
		private int? _revieweduserid;
		private string _reviewedcontent;
		private DateTime? _revieweddate;
		private string _filenames;
		private string _filedatapath;
		private string _remark;
		/// <summary>
		/// 待办编号
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
		/// 待办执行人
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
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
		/// 发送对象：用户编号列表，多个用户用逗号分隔。 冗余字段
		/// </summary>
		public string ToUserId
		{
			set{ _touserid=value;}
			get{return _touserid;}
		}
		/// <summary>
		/// 发送类型 0：本人 1：下属  2：所有人 3: 指定用户发送, 使用ToUserId
		/// </summary>
		public int ToType
		{
			set{ _totype=value;}
			get{return _totype;}
		}
		/// <summary>
		/// 0：未办 1：已办 2：未通过 3:  已通过
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 回复的待办编号(父级)  预留字段未使用
		/// </summary>
		public int? ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 创建用户ID
		/// </summary>
		public int CreatedUserId
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
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
		/// 待办日期 冗余字段 方便统计
		/// </summary>
		public DateTime ToDoDate
		{
			set{ _tododate=value;}
			get{return _tododate;}
		}
		/// <summary>
		/// 批复人
		/// </summary>
		public int? ReviewedUserID
		{
			set{ _revieweduserid=value;}
			get{return _revieweduserid;}
		}
		/// <summary>
		/// 批复内容
		/// </summary>
		public string ReviewedContent
		{
			set{ _reviewedcontent=value;}
			get{return _reviewedcontent;}
		}
		/// <summary>
		/// 批复时间
		/// </summary>
		public DateTime? ReviewedDate
		{
			set{ _revieweddate=value;}
			get{return _revieweddate;}
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
		/// 文件路径占位符方式, 如 ID_{0}, {0}是FileNames
		/// </summary>
		public string FileDataPath
		{
			set{ _filedatapath=value;}
			get{return _filedatapath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

