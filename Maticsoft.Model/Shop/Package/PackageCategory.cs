using System;
namespace Maticsoft.Model.Shop.Package
{
	/// <summary>
	/// PackageCategory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PackageCategory
	{
		public PackageCategory()
		{}
		#region Model
		private int _categoryid;
		private string _name;
		private DateTime? _createddate= DateTime.Now;
		private int? _status=0;
		private string _remark;
		/// <summary>
		/// id
		/// </summary>
		public int CategoryId
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 类别名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 创建的时间
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 类别的状态
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

