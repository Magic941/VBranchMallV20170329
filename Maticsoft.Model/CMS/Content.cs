using System;
namespace Maticsoft.Model.CMS
{
	/// <summary>
	/// Content:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public partial class Content
	{
		public Content()
		{}
        #region Model
        private int _contentid;
        private string _title;
        private string _subtitle;
        private string _summary;
        private string _description;
        private string _imageurl;
        private string _thumbimageurl;
        private string _normalimageurl;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private int? _lastedituserid;
        private DateTime? _lasteditdate;
        private string _linkurl;
        private int _pvcount;
        private int _state;
        private int _classid;
        private string _keywords;
        private int _sequence;
        private bool _isrecomend;
        private bool _ishot;
        private bool _iscolor;
        private bool _istop;
        private string _attachment;
        private string _remary;
        private int _totalcomment = 0;
        private int _totalsupport = 0;
        private int _totalfav = 0;
        private int _totalshare = 0;
        private string _befrom;
        private string _filename;
        private string _meta_title;
        private string _meta_description;
        private string _meta_keywords;
        private string _seourl;
        private string _seoimagealt;
        private string _seoimagetitle;
        private string _staticurl;
        /// <summary>
        /// ���ݱ��
        /// </summary>
        public int ContentID
        {
            set { _contentid = value; }
            get { return _contentid; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// �ӱ���
        /// </summary>
        public string SubTitle
        {
            set { _subtitle = value; }
            get { return _subtitle; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// ������Ƶ
        /// </summary>
        public string ThumbImageUrl
        {
            set { _thumbimageurl = value; }
            get { return _thumbimageurl; }
        }
        /// <summary>
        /// ������Ƶ
        /// </summary>
        public string NormalImageUrl
        {
            set { _normalimageurl = value; }
            get { return _normalimageurl; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public int CreatedUserID
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }
        /// <summary>
        /// ���༭��
        /// </summary>
        public int? LastEditUserID
        {
            set { _lastedituserid = value; }
            get { return _lastedituserid; }
        }
        /// <summary>
        /// ���༭ʱ��
        /// </summary>
        public DateTime? LastEditDate
        {
            set { _lasteditdate = value; }
            get { return _lasteditdate; }
        }
        /// <summary>
        /// �ⲿ���ӵ�ַ
        /// </summary>
        public string LinkUrl
        {
            set { _linkurl = value; }
            get { return _linkurl; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public int PvCount
        {
            set { _pvcount = value; }
            get { return _pvcount; }
        }
        /// <summary>
        /// ���״̬
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        public int ClassID
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// �ؼ���
        /// </summary>
        public string Keywords
        {
            set { _keywords = value; }
            get { return _keywords; }
        }
        /// <summary>
        /// ˳��
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// �Ƿ��Ƽ�
        /// </summary>
        public bool IsRecomend
        {
            set { _isrecomend = value; }
            get { return _isrecomend; }
        }
        /// <summary>
        /// �Ƿ��ȵ�
        /// </summary>
        public bool IsHot
        {
            set { _ishot = value; }
            get { return _ishot; }
        }
        /// <summary>
        /// �Ƿ���Ŀ
        /// </summary>
        public bool IsColor
        {
            set { _iscolor = value; }
            get { return _iscolor; }
        }
        /// <summary>
        /// �Ƿ��ö�
        /// </summary>
        public bool IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remary
        {
            set { _remary = value; }
            get { return _remary; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public int TotalComment
        {
            set { _totalcomment = value; }
            get { return _totalcomment; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int TotalSupport
        {
            set { _totalsupport = value; }
            get { return _totalsupport; }
        }
        /// <summary>
        /// �ղ���
        /// </summary>
        public int TotalFav
        {
            set { _totalfav = value; }
            get { return _totalfav; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public int TotalShare
        {
            set { _totalshare = value; }
            get { return _totalshare; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BeFrom
        {
            set { _befrom = value; }
            get { return _befrom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
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
        /// <summary>
        /// 
        /// </summary>
        public string StaticUrl
        {
            set { _staticurl = value; }
            get { return _staticurl; }
        }
        #endregion Model
        private string _createdusername;
        /// <summary>
        /// �����û�
        /// </summary>
        public string CreatedUserName
        {
            get { return _createdusername; }
            set { _createdusername = value; }
        }

        private string _classname;
        /// <summary>
        /// ������Ŀ�������
        /// </summary>
        public string ClassName
        {
            get { return _classname; }
            set { _classname = value; }
        }

	    public int ComCount=0;

	}
}

