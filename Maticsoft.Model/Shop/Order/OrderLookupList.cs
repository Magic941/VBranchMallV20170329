using System;
namespace Maticsoft.Model.Shop.Order
{
	/// <summary>
	/// OrderLookupList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderLookupList
	{
		public OrderLookupList()
		{}
		#region Model
		private int _lookuplistid;
		private string _name;
		private int _selectmode;
		private string _description;
		/// <summary>
		/// 可选项ID
		/// </summary>
		public int LookupListId
		{
			set{ _lookuplistid=value;}
			get{return _lookuplistid;}
		}
		/// <summary>
		/// 可选项名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 选择方式 1：表示下拉列表 2：表示单选按钮
		/// </summary>
		public int SelectMode
		{
			set{ _selectmode=value;}
			get{return _selectmode;}
		}
		/// <summary>
		/// 可选项描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		#endregion Model

	}
}

