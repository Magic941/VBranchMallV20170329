/**
* TagCategories.cs
*
* 功 能： N/A
* 类 名： TagCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年12月14日 10:08:12   Rock    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;

namespace Maticsoft.Model.Shop.Tags
{
    /// <summary>
    /// TagCategories:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TagCategories
    {
        public TagCategories()
        { }

        #region Model

        private int _id;
        private string _categoryname;
        private int? _parentcategoryid;
        private int _displaysequence;
        private int _depth;
        private string _path;
        private string _meta_title;
        private string _meta_description;
        private string _meta_keywords;
        private bool _haschildren;
        private int? _status;
        private string _remark;

        /// <summary>
        /// 标签分类ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 标签分类名称
        /// </summary>
        public string CategoryName
        {
            set { _categoryname = value; }
            get { return _categoryname; }
        }

        /// <summary>
        /// 上级标签
        /// </summary>
        public int? ParentCategoryId
        {
            set { _parentcategoryid = value; }
            get { return _parentcategoryid; }
        }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplaySequence
        {
            set { _displaysequence = value; }
            get { return _displaysequence; }
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
        /// 分类路径
        /// </summary>
        public string Path
        {
            set { _path = value; }
            get { return _path; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Meta_Title
        {
            set { _meta_title = value; }
            get { return _meta_title; }
        }

        /// <summary>
        ///
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

        /// <summary>
        /// 是否含有子分类
        /// </summary>
        public bool HasChildren
        {
            set { _haschildren = value; }
            get { return _haschildren; }
        }

        /// <summary>
        /// 0：不可用；1：可用
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        #endregion Model
    }
}