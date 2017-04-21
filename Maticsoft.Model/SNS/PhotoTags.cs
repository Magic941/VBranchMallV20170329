using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 图片标签
	/// </summary>
	[Serializable]
	public partial class PhotoTags
	{
		public PhotoTags()
		{}
		#region Model
		private int _tagid;
		private string _tagname;
		private int? _isrecommand;
		private int? _status;
		private DateTime? _createddate;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int TagID
		{
			set{ _tagid=value;}
			get{return _tagid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TagName
		{
			set{ _tagname=value;}
			get{return _tagname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsRecommand
		{
			set{ _isrecommand=value;}
			get{return _isrecommand;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

