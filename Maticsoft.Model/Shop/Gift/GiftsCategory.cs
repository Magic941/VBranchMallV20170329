using System;
namespace Maticsoft.Model.Shop.Gift
{
	/// <summary>
	/// GiftsCategory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GiftsCategory
	{
		public GiftsCategory()
		{}
		#region Model
		private int _categoryid;
		private int? _parentcategoryid;
		private string _name;
		private int _depth;
		private string _path;
		private int _displaysequence;
		private string _description;
		private string _theme;
		private bool _haschildren;
		/// <summary>
		/// 礼品分类ID
		/// </summary>
		public int CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 父分类ID
		/// </summary>
		public int? ParentCategoryId
		{
			set{ _parentcategoryid=value;}
			get{return _parentcategoryid;}
		}
		/// <summary>
		/// 礼品分类名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		/// 路径
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int DisplaySequence
		{
			set{ _displaysequence=value;}
			get{return _displaysequence;}
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
		/// 主题
		/// </summary>
		public string Theme
		{
			set{ _theme=value;}
			get{return _theme;}
		}
		/// <summary>
		/// 是否有子分类
		/// </summary>
		public bool HasChildren
		{
			set{ _haschildren=value;}
			get{return _haschildren;}
		}
		#endregion Model

	}
}

