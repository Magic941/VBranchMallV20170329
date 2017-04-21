using System;
namespace Maticsoft.Model.Members
{
	/// <summary>
	/// FeedbackType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class FeedbackType
	{
		public FeedbackType()
		{}
		#region Model
		private int _typeid;
		private string _typename;
		private string _description;
		/// <summary>
		/// 反馈类型ID
		/// </summary>
		public int TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 类型名称
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		#endregion Model

	}
}

