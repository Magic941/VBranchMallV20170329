/**
* SupplierMenus.cs
*
* 功 能： N/A
* 类 名： SupplierMenus
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:48   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Supplier
{
	/// <summary>
	/// 供应商(店铺)导航
	/// </summary>
	[Serializable]
	public partial class SupplierMenus
	{
		public SupplierMenus()
		{}
		#region Model
		private int _menuid;
		private string _menuname;
		private string _navurl;
		private string _menutitle;
		private int? _menutype;
		private int? _target;
		private bool _isused;
		private int _sequence=0;
		private int _visible=1;
		private int _urltype=0;
		private string _navtheme="";
		private int _supplierid;
		/// <summary>
		/// 编号 
		/// </summary>
		public int MenuId
		{
			set{ _menuid=value;}
			get{return _menuid;}
		}
		/// <summary>
		/// 菜单名称
		/// </summary>
		public string MenuName
		{
			set{ _menuname=value;}
			get{return _menuname;}
		}
		/// <summary>
		/// 页面地址
		/// </summary>
		public string NavURL
		{
			set{ _navurl=value;}
			get{return _navurl;}
		}
		/// <summary>
		/// 菜单提示
		/// </summary>
		public string MenuTitle
		{
			set{ _menutitle=value;}
			get{return _menutitle;}
		}
		/// <summary>
		/// 系统菜单 0：系统 1：用户自定义添加
		/// </summary>
		public int? MenuType
		{
			set{ _menutype=value;}
			get{return _menutype;}
		}
		/// <summary>
		/// 打开方式 0：本窗口打开 1：新窗口打开
		/// </summary>
		public int? Target
		{
			set{ _target=value;}
			get{return _target;}
		}
		/// <summary>
		/// 是否可用 1：可用 0：不可用
		/// </summary>
		public bool IsUsed
		{
			set{ _isused=value;}
			get{return _isused;}
		}
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 可见权限(备用字段)
		/// </summary>
		public int Visible
		{
			set{ _visible=value;}
			get{return _visible;}
		}
		/// <summary>
        /// 链接地址类型 0：自定义 1：商品分类 2：栏目 
		/// </summary>
		public int URLType
		{
			set{ _urltype=value;}
			get{return _urltype;}
		}
		/// <summary>
		/// 模版名称
		/// </summary>
		public string NavTheme
		{
			set{ _navtheme=value;}
			get{return _navtheme;}
		}
		/// <summary>
		/// 供应商Id
		/// </summary>
		public int SupplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		#endregion Model

	}
}

