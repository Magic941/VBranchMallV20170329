/**
* AgentRank.cs
*
* 功 能： N/A
* 类 名： AgentRank
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/28 18:17:11   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Ms.Agent
{
	/// <summary>
	/// 代理商(店铺)等级
	/// </summary>
	[Serializable]
	public partial class AgentRank
	{
		public AgentRank()
		{}
		#region Model
		private int _rankid;
		private string _name;
		private int _productcount=-1;
		private int _imagecount=-1;
		private decimal _price=0M;
		private bool _isdefault;
		private bool _isapproval;
		private string _description;
		private decimal _rankmin=-1M;
		private decimal _rankmax=-1M;
		/// <summary>
		/// 
		/// </summary>
		public int RankId
		{
			set{ _rankid=value;}
			get{return _rankid;}
		}
		/// <summary>
		/// 等级名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 可发布商品数量
		/// </summary>
		public int ProductCount
		{
			set{ _productcount=value;}
			get{return _productcount;}
		}
		/// <summary>
		/// 可上传图片总数
		/// </summary>
		public int ImageCount
		{
			set{ _imagecount=value;}
			get{return _imagecount;}
		}
		/// <summary>
		/// 价格
		/// </summary>
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 是否默认
		/// </summary>
		public bool IsDefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		/// <summary>
		/// 是否需要审核
		/// </summary>
		public bool IsApproval
		{
			set{ _isapproval=value;}
			get{return _isapproval;}
		}
		/// <summary>
		/// 申请说明
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal RankMin
		{
			set{ _rankmin=value;}
			get{return _rankmin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal RankMax
		{
			set{ _rankmax=value;}
			get{return _rankmax;}
		}
		#endregion Model

	}
}

