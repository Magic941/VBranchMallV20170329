using System;
namespace Maticsoft.Model.CMS
{
	/// <summary>
	/// ContentClass:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ContentClass
	{
		public ContentClass()
		{}
        #region Model
        private int _classid;
        private string _classname;
        private string _classindex;
        private int _sequence;
        private int? _parentid;
        private int _state;
        private bool _allowsubclass;
        private bool _allowaddcontent;
        private string _imageurl;
        private string _description;
        private string _keywords;
        private int _classtypeid;
        private int _classmodel;
        private string _pagemodelname;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private string _path;
        private int? _depth;
        private string _remark;
        private string _meta_title;
        private string _meta_description;
        private string _meta_keywords;
        private string _seourl;
        private string _seoimagealt;
        private string _seoimagetitle;
        private string _indexchar;
        /// <summary>
        /// 
        /// </summary>
        public int ClassID
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassIndex
        {
            set { _classindex = value; }
            get { return _classindex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool AllowSubclass
        {
            set { _allowsubclass = value; }
            get { return _allowsubclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool AllowAddContent
        {
            set { _allowaddcontent = value; }
            get { return _allowaddcontent; }
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
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Keywords
        {
            set { _keywords = value; }
            get { return _keywords; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ClassTypeID
        {
            set { _classtypeid = value; }
            get { return _classtypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ClassModel
        {
            set { _classmodel = value; }
            get { return _classmodel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PageModelName
        {
            set { _pagemodelname = value; }
            get { return _pagemodelname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CreatedUserID
        {
            set { _createduserid = value; }
            get { return _createduserid; }
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
        /// 层级
        /// </summary>
        public int? Depth
        {
            set { _depth = value; }
            get { return _depth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
        public string SeoUrl
        {
            set { _seourl = value; }
            get { return _seourl; }
        }
        /// <summary>
        /// SEO 图片Alt信息
        /// </summary>
        public string SeoImageAlt
        {
            set { _seoimagealt = value; }
            get { return _seoimagealt; }
        }
        /// <summary>
        /// SEO 图片Title信息
        /// </summary>
        public string SeoImageTitle
        {
            set { _seoimagetitle = value; }
            get { return _seoimagetitle; }
        }
        /// <summary>
        /// 栏目路径名称
        /// </summary>
        public string IndexChar
        {
            set { _indexchar = value; }
            get { return _indexchar; }
        }
        #endregion Model

        #region 扩展属性
        private string _namepath;
        /// <summary>
        /// 栏目名路径
        /// </summary>
        public string NamePath
        {
            set { _namepath = value; }
            get { return _namepath; }
        }
        #endregion
    }
}

