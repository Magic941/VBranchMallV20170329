using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 淘宝分类
	/// </summary>
	[Serializable]
	public partial class CategorySource
	{
		public CategorySource()
		{}
		#region Model
		private int _sourceid;
		private int _categoryid;
		private string _name;
		private string _description;
		private int _parentid;
		private string _path;
		private int _depth;
		private int _sequence;
		private bool _haschildren;
		private bool _ismenu;
		private int _type;
		private bool _menuisshow;
		private int _menusequence;
		private int _createduserid;
		private DateTime _createddate;
		private int _status;
		private int? _snscategoryid;
		/// <summary>
		/// 
		/// </summary>
		public int SourceId
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CategoryId
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 父级id
		/// </summary>
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		/// 深度
		/// </summary>
		public int Depth
		{
			set{ _depth=value;}
			get{return _depth;}
		}
		/// <summary>
		/// 顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 是否有子级
		/// </summary>
		public bool HasChildren
		{
			set{ _haschildren=value;}
			get{return _haschildren;}
		}
		/// <summary>
		/// 是否是菜单
		/// </summary>
		public bool IsMenu
		{
			set{ _ismenu=value;}
			get{return _ismenu;}
		}
		/// <summary>
		/// 类型
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 菜单是否显示
		/// </summary>
		public bool MenuIsShow
		{
			set{ _menuisshow=value;}
			get{return _menuisshow;}
		}
		/// <summary>
		/// 菜单显示的顺序
		/// </summary>
		public int MenuSequence
		{
			set{ _menusequence=value;}
			get{return _menusequence;}
		}
		/// <summary>
		/// 创建者用户名
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 日期
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 状态 0:不可用 1：可用
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SnsCategoryId
		{
			set{ _snscategoryid=value;}
			get{return _snscategoryid;}
		}
		#endregion Model

	}
}

