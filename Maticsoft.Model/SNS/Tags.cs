/**
* Tags.cs
*
* 功 能： N/A
* 类 名： Tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:58   N/A    初版
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
	/// 具体标签
	/// </summary>
	[Serializable]
	public partial class Tags
	{
		public Tags()
		{}
		#region Model
		private int _tagid;
		private string _tagname;
		private int _typeid;
        private int _isrecommand;
		private int _status;
		/// <summary>
		/// 标签的id
		/// </summary>
		public int TagID
		{
			set{ _tagid=value;}
			get{return _tagid;}
		}
        public int IsRecommand
        {
            set {_isrecommand = value;}
            get {return _isrecommand; }
        }
		/// <summary>
		/// 标签的名称
		/// </summary>
		public string TagName
		{
			set{ _tagname=value;}
			get{return _tagname;}
		}
		/// <summary>
		/// 标签的类别
		/// </summary>
		public int TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
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

