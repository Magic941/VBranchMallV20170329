using System;
namespace Maticsoft.Model.Shop.Sample
{
	/// <summary>
	/// SampleDetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SampleDetail
	{
		public SampleDetail()
		{}
		#region Model
		private int _id;
		private int _sampleid;
		private string _title;
		private int _type=0;
		private string _imageurl;
		private string _normalimageurl;
		private string _thumbimageurl;
		private string _pdfurl;
		private DateTime? _createddate;
		private int? _status;
		private string _remark;
		/// <summary>
		/// id
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 所对应样本的Id
		/// </summary>
		public int SampleId
		{
			set{ _sampleid=value;}
			get{return _sampleid;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 类型( 0：图片 1：pdf)
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 图片原图
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 图片的缩略图
		/// </summary>
		public string NormalImageUrl
		{
			set{ _normalimageurl=value;}
			get{return _normalimageurl;}
		}
		/// <summary>
		/// 图片的缩略小图
		/// </summary>
		public string ThumbImageUrl
		{
			set{ _thumbimageurl=value;}
			get{return _thumbimageurl;}
		}
		/// <summary>
		/// pdf的Url
		/// </summary>
		public string PdfUrl
		{
			set{ _pdfurl=value;}
			get{return _pdfurl;}
		}
		/// <summary>
		/// 上传时间
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

