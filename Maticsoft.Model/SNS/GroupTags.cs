/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：GroupTags.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/10/25 11:53:32
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 小组标签
	/// </summary>
	[Serializable]
	public partial class GroupTags
	{
		public GroupTags()
		{}
		#region Model
		private int _tagid;
		private string _tagname;
		private int _isrecommand;
		private int _status;
		/// <summary>
		/// 编号
		/// </summary>
		public int TagID
		{
			set{ _tagid=value;}
			get{return _tagid;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string TagName
		{
			set{ _tagname=value;}
			get{return _tagname;}
		}
		/// <summary>
		/// 是否推荐到分类标签页
		/// </summary>
		public int IsRecommand
		{
			set{ _isrecommand=value;}
			get{return _isrecommand;}
		}
		/// <summary>
		/// 状态 0:不可用 1：可用
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

