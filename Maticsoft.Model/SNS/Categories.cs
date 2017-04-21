/**
* Categories.cs
*
* 功 能： N/A
* 类 名： Categories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:40   N/A    初版
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
	/// 专辑类型
	/// </summary>
	[Serializable]
	public partial class Categories
	{
		public Categories()
		{}
        #region Model
        private int _categoryid;
        private string _name;
        private string _description;
        private int _parentid;
        private string _path;
        private int _depth;
        private int _sequence;
        private bool _haschildren;
        private bool _ismenu;
        private int _type;
        private bool _menuisshow;
        private int _menusequence;
        private string _fontcolor;
        private int _createduserid;
        private DateTime _createddate = DateTime.Now;
        private int _status;
        private string _seourl;
        private string _meta_title;
        private string _meta_description;
        private string _meta_keywords;
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            set { _path = value; }
            get { return _path; }
        }
        /// <summary>
        /// 深度
        /// </summary>
        public int Depth
        {
            set { _depth = value; }
            get { return _depth; }
        }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 是否有子级
        /// </summary>
        public bool HasChildren
        {
            set { _haschildren = value; }
            get { return _haschildren; }
        }
        /// <summary>
        /// 是否是菜单
        /// </summary>
        public bool IsMenu
        {
            set { _ismenu = value; }
            get { return _ismenu; }
        }
        /// <summary>
        /// 类型 0:商品分类 1：为图片分类
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 菜单是否显示
        /// </summary>
        public bool MenuIsShow
        {
            set { _menuisshow = value; }
            get { return _menuisshow; }
        }
        /// <summary>
        /// 菜单显示的顺序
        /// </summary>
        public int MenuSequence
        {
            set { _menusequence = value; }
            get { return _menusequence; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FontColor
        {
            set { _fontcolor = value; }
            get { return _fontcolor; }
        }
        /// <summary>
        /// 创建者用户名
        /// </summary>
        public int CreatedUserID
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 状态 0:不可用 1：可用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// SeoUrl 优化地址
        /// </summary>
        public string SeoUrl
        {
            set { _seourl = value; }
            get { return _seourl; }
        }
        /// <summary>
        /// SEO 标题
        /// </summary>
        public string Meta_Title
        {
            set { _meta_title = value; }
            get { return _meta_title; }
        }
        /// <summary>
        /// SEO 描述
        /// </summary>
        public string Meta_Description
        {
            set { _meta_description = value; }
            get { return _meta_description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Meta_Keywords
        {
            set { _meta_keywords = value; }
            get { return _meta_keywords; }
        }
        #endregion Model

	}
}

