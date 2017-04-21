/**
* PostsTopics.cs
*
* 功 能： N/A
* 类 名： PostsTopics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:48   N/A    初版
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
	/// 动态话题
	/// </summary>
	[Serializable]
	public partial class PostsTopics
	{
		public PostsTopics()
		{}
		#region Model
		private string _title;
		private string _description;
		private int _createduserid;
		private string _creatednickname;
		private DateTime _createddate;
		private int _topicscount;
		private bool _isrecommend;
		private int _sequence;
		private string _tags;
		/// <summary>
		/// 标题(主键)
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 发起者id
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 发起者用户名
		/// </summary>
		public string CreatedNickName
		{
			set{ _creatednickname=value;}
			get{return _creatednickname;}
		}
		/// <summary>
		/// 发起的时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 话题的总数
		/// </summary>
		public int TopicsCount
		{
			set{ _topicscount=value;}
			get{return _topicscount;}
		}
		/// <summary>
		/// 是否被管理员推荐
		/// </summary>
		public bool IsRecommend
		{
			set{ _isrecommend=value;}
			get{return _isrecommend;}
		}
		/// <summary>
		/// 显示的顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 话题所对应的标签
		/// </summary>
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}
		#endregion Model

	}
}

