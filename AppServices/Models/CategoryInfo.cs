using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppServices.Models
{
    public class CategoryInfo
    {
        public CategoryInfo()
        { }
        #region Model
        private int _categoryid;
        private int _displaysequence;
        private string _name;
        private string _meta_title;
        private string _meta_description;
        private string _meta_keywords;
        private string _description;
        private int _parentcategoryid = 0;
        private int _depth;
        private string _path;
        private string _rewritename;
        private string _skuprefix;
        private int? _associatedproducttype;
        private string _imageurl;
        private string _notes1;
        private string _notes2;
        private string _notes3;
        private string _notes4;
        private string _notes5;
        private string _theme;
        private bool _haschildren = false;
        private string _seourl;
        private string _seoimagealt;
        private string _seoimagetitle;
        private int _ProductCount;

        public int ProductCount
        {
            get { return _ProductCount; }
            set { _ProductCount = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DisplaySequence
        {
            set { _displaysequence = value; }
            get { return _displaysequence; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
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
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ParentCategoryId
        {
            set { _parentcategoryid = value; }
            get { return _parentcategoryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Depth
        {
            set { _depth = value; }
            get { return _depth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            set { _path = value; }
            get { return _path; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RewriteName
        {
            set { _rewritename = value; }
            get { return _rewritename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SKUPrefix
        {
            set { _skuprefix = value; }
            get { return _skuprefix; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AssociatedProductType
        {
            set { _associatedproducttype = value; }
            get { return _associatedproducttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Notes1
        {
            set { _notes1 = value; }
            get { return _notes1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Notes2
        {
            set { _notes2 = value; }
            get { return _notes2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Notes3
        {
            set { _notes3 = value; }
            get { return _notes3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Notes4
        {
            set { _notes4 = value; }
            get { return _notes4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Notes5
        {
            set { _notes5 = value; }
            get { return _notes5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Theme
        {
            set { _theme = value; }
            get { return _theme; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HasChildren
        {
            set { _haschildren = value; }
            get { return _haschildren; }
        }
        /// <summary>
        /// SEO Url地址优化
        /// </summary>
        public string SeoUrl
        {
            set { _seourl = value; }
            get { return _seourl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SeoImageAlt
        {
            set { _seoimagealt = value; }
            get { return _seoimagealt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SeoImageTitle
        {
            set { _seoimagetitle = value; }
            get { return _seoimagetitle; }
        }
        #endregion Model
    }
}