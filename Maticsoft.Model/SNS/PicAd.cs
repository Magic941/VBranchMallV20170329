using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// PicAd:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PicAd
	{
		public PicAd()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _src;
		private string _href;
		private string _title;
		private string _alt;
		private bool _isshow;
		private int _orders;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Src
		{
			set{ _src=value;}
			get{return _src;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Href
		{
			set{ _href=value;}
			get{return _href;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Alt
		{
			set{ _alt=value;}
			get{return _alt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Orders
		{
			set{ _orders=value;}
			get{return _orders;}
		}
		#endregion Model

	}
}

