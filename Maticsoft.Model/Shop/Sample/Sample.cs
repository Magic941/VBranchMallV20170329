using System;
namespace Maticsoft.Model.Shop.Sample
{
	/// <summary>
	/// Sample:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sample
	{
		public Sample()
		{}
		#region Model
		private int _sampleid;
		private string _tiltle;
		private string _eleccoverimageurl;
		private string _normaleleccoverimageurl;
		private string _thumbleleccoverimageurl;
		private string _pdfcoverimageurl;
		private string _normalpdfcoverimageurl;
		private string _thumbpdfcoverimageurl;
		private int? _sequence;
		private int? _status;
		private DateTime? _createddate;
		private string _remark;
		private string _meta_title;
		private string _meta_description;
		private string _meta_keywords;
		private string _seourl;
		private string _seoimagealt;
		private string _seoimagetitle;
		/// <summary>
		/// 样本ID
		/// </summary>
		public int SampleId
		{
			set{ _sampleid=value;}
			get{return _sampleid;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Tiltle
		{
			set{ _tiltle=value;}
			get{return _tiltle;}
		}
		/// <summary>
		/// 此电子样本的封面
		/// </summary>
		public string ElecCoverImageUrl
		{
			set{ _eleccoverimageurl=value;}
			get{return _eleccoverimageurl;}
		}
		/// <summary>
		/// 电子样本封面缩略图
		/// </summary>
		public string NormalElecCoverImageUrl
		{
			set{ _normaleleccoverimageurl=value;}
			get{return _normaleleccoverimageurl;}
		}
		/// <summary>
		/// 电子样本封面缩略小图
		/// </summary>
		public string ThumblElecCoverImageUrl
		{
			set{ _thumbleleccoverimageurl=value;}
			get{return _thumbleleccoverimageurl;}
		}
		/// <summary>
		/// 此样本Pdf的封面图片
		/// </summary>
		public string PdfCoverImageUrl
		{
			set{ _pdfcoverimageurl=value;}
			get{return _pdfcoverimageurl;}
		}
		/// <summary>
		/// 此样本Pdf的封面缩略图
		/// </summary>
		public string NormalPdfCoverImageUrl
		{
			set{ _normalpdfcoverimageurl=value;}
			get{return _normalpdfcoverimageurl;}
		}
		/// <summary>
		/// 此样本Pdf的封面缩略小图
		/// </summary>
		public string ThumbPdfCoverImageUrl
		{
			set{ _thumbpdfcoverimageurl=value;}
			get{return _thumbpdfcoverimageurl;}
		}
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int? Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 状态（0：未发布 1：已发布）
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 添加日期
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Meta_Title
		{
			set{ _meta_title=value;}
			get{return _meta_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Meta_Description
		{
			set{ _meta_description=value;}
			get{return _meta_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Meta_KeyWords
		{
			set{ _meta_keywords=value;}
			get{return _meta_keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SeoUrl
		{
			set{ _seourl=value;}
			get{return _seourl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SeoImageAlt
		{
			set{ _seoimagealt=value;}
			get{return _seoimagealt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SeoImageTitle
		{
			set{ _seoimagetitle=value;}
			get{return _seoimagetitle;}
		}
		#endregion Model

	}
}

