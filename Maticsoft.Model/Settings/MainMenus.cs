/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。 
//
// 文件名：WebMenuConfig.cs
// 文件功能描述：网站菜单实体层
// 
// 创建标识：2012年5月23日 16:32:03
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Settings
{
    /// <summary>
    /// 导航菜单数据表
    /// </summary>
    [Serializable]
    public partial class MainMenus
    {
        public MainMenus()
        { }
        #region Model
        private int _menuid;
        private string _menuname;
        private string _navurl;
        private string _menutitle;
        private int? _menutype;
        private int? _target;
        private bool _isused;
        private int _sequence = 0;
        private int _visible = 1;
        private int _navarea = 0;
        private int _urltype = 0;
        private string _navtheme;
        /// <summary>
        /// 编号 
        /// </summary>
        public int MenuID
        {
            set { _menuid = value; }
            get { return _menuid; }
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            set { _menuname = value; }
            get { return _menuname; }
        }
        /// <summary>
        /// 页面地址
        /// </summary>
        public string NavURL
        {
            set { _navurl = value; }
            get { return _navurl; }
        }
        /// <summary>
        /// 菜单提示
        /// </summary>
        public string MenuTitle
        {
            set { _menutitle = value; }
            get { return _menutitle; }
        }
        /// <summary>
        /// 系统菜单 0：系统 1：用户自定义添加
        /// </summary>
        public int? MenuType
        {
            set { _menutype = value; }
            get { return _menutype; }
        }
        /// <summary>
        /// 打开方式 0：本窗口打开 1：新窗口打开
        /// </summary>
        public int? Target
        {
            set { _target = value; }
            get { return _target; }
        }
        /// <summary>
        /// 是否可用 1：可用 0：不可用
        /// </summary>
        public bool IsUsed
        {
            set { _isused = value; }
            get { return _isused; }
        }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 可见权限(备用字段)
        /// </summary>
        public int Visible
        {
            set { _visible = value; }
            get { return _visible; }
        }
        /// <summary>
        /// 区域类型 0：表示CMS 1：表示SNS 2：表示Shop
        /// </summary>
        public int NavArea
        {
            set { _navarea = value; }
            get { return _navarea; }
        }
        /// <summary>
        /// 链接地址类型 0：表示自定义  1：表示栏目 2：表示商品分类 3：表示专辑分类  4:表示商城商品分类
        /// </summary>
        public int URLType
        {
            set { _urltype = value; }
            get { return _urltype; }
        }
        /// <summary>
        /// 模版名称
        /// </summary>
        public string NavTheme
        {
            set { _navtheme = value; }
            get { return _navtheme; }
        }
        #endregion Model
    }
}