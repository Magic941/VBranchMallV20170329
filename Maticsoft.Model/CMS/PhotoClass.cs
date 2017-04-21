using System;

namespace Maticsoft.Model.CMS
{
	/// <summary>
	/// PhotoClass:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PhotoClass
	{
		public PhotoClass()
		{}
		#region Model
		private int _classid;
		private string _classname;
		private int? _parentid=0;
		private int? _sequence;
		private string _path;
		private int? _depth;
		/// <summary>
		/// 
		/// </summary>
		public int ClassID
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		/// <summary>
		/// 类别名称
		/// </summary>
		public string ClassName
		{
			set{ _classname=value;}
			get{return _classname;}
		}
		/// <summary>
		/// 上级类别
		/// </summary>
		public int? ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 顺序
		/// </summary>
		public int? Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 路径
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		/// <summary>
		/// 层级
		/// </summary>
		public int? Depth
		{
			set{ _depth=value;}
			get{return _depth;}
		}
		#endregion Model

	}
}

