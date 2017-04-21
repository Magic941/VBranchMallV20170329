using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// RegionRec:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RegionRec
	{
		public RegionRec()
		{}
		#region Model
		private int _id;
		private int _regionid;
		private string _regionname;
		private int _displaysequence;
		private int _type;
		/// <summary>
		/// 主键 流水账ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 地区ID
		/// </summary>
		public int RegionId
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		/// <summary>
		/// 地区名字
		/// </summary>
		public string RegionName
		{
			set{ _regionname=value;}
			get{return _regionname;}
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
		/// 
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion Model

	}
}

