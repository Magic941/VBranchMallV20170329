using System;
namespace Maticsoft.Model.Shop.Order
{
	/// <summary>
	/// OrderLookupItems:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderLookupItems
	{
		public OrderLookupItems()
		{}
		#region Model
		private int _lookupitemid;
		private int _lookuplistid;
		private string _name;
		private bool _isinputrequired;
		private string _inputtitle;
		private decimal? _appendmoney;
		private int? _calculatemode;
		private string _remark;
		/// <summary>
		/// 可选项值ID
		/// </summary>
		public int LookupItemId
		{
			set{ _lookupitemid=value;}
			get{return _lookupitemid;}
		}
		/// <summary>
		/// 可选项
		/// </summary>
		public int LookupListId
		{
			set{ _lookuplistid=value;}
			get{return _lookuplistid;}
		}
		/// <summary>
		/// 值名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 是否需要用户填写信息
		/// </summary>
		public bool IsInputRequired
		{
			set{ _isinputrequired=value;}
			get{return _isinputrequired;}
		}
		/// <summary>
		/// 填写信息标题
		/// </summary>
		public string InputTitle
		{
			set{ _inputtitle=value;}
			get{return _inputtitle;}
		}
		/// <summary>
		/// 附加金额
		/// </summary>
		public decimal? AppendMoney
		{
			set{ _appendmoney=value;}
			get{return _appendmoney;}
		}
		/// <summary>
		/// 附加金额方式 1：表示固定金额，2：表示百分比
		/// </summary>
		public int? CalculateMode
		{
			set{ _calculatemode=value;}
			get{return _calculatemode;}
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

