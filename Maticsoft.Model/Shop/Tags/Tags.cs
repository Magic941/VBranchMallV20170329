/**
* Tags.cs
*
* 功 能： N/A
* 类 名： Tags
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年12月14日 10:09:07   Rock    初版
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
    /// Tags:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Tags
    {
        public Tags()
        { }

        #region Model

        private int _tagid;
        private int _tagcategoryid;
        private string _tagname;
        private bool _isrecommand;
        private int _status = 0;
        private string _meta_title;
        private string _meta_description;
        private string _meta_keywords;

        /// <summary>
        /// 标签ID
        /// </summary>
        public int TagID
        {
            set { _tagid = value; }
            get { return _tagid; }
        }

        /// <summary>
        /// 标签分类
        /// </summary>
        public int TagCategoryId
        {
            set { _tagcategoryid = value; }
            get { return _tagcategoryid; }
        }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName
        {
            set { _tagname = value; }
            get { return _tagname; }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsRecommand
        {
            set { _isrecommand = value; }
            get { return _isrecommand; }
        }

        /// <summary>
        /// 0：不可用；1：可用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
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

        #endregion Model
    }
}