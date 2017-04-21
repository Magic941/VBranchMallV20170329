using System;
namespace Maticsoft.Model.Shop.Package
{
	/// <summary>
	/// ProductPackage:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductPackage
	{
		public ProductPackage()
		{}
		#region Model
		private long _productid;
		private int _packageid;
		/// <summary>
		/// 产品的ID
		/// </summary>
		public long ProductId
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 对应包装的ID
		/// </summary>
		public int PackageId
		{
			set{ _packageid=value;}
			get{return _packageid;}
		}
		#endregion Model

	}
}

